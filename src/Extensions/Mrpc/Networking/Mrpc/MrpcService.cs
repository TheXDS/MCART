/*
NameAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.PropertyInfoExtensions;
using TheXDS.MCART.Resources;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Networking.Mrpc.Serializers;
using System.Reflection;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Networking.Mrpc
{
    public class MrpcService<T> where T : class
    {
        private IPEndPoint _endpoint;
        private T? _instance;

        public MrpcService(IPEndPoint endPoint, T instance)
        {
            _endpoint = endPoint;
            _instance = instance;
        }
    }

    internal enum ObjectKind
    {
        Null,
        Simple,
        Array,
        CircPointer,
        Ref, // Seguido de otro tipo de argumento.
        Out // Seguido de otro tipo de argumento
    }

    /// <summary>
    ///     Contiene información sobre una llamada específica a un procedimiento remoto.
    /// </summary>
    public class CallLock
    {
        internal CallLock(MethodBase method)
        {
            Method = method.FullName();
        }

        internal TaskCompletionSource<byte[]> Waiter { get; } = new TaskCompletionSource<byte[]>();

        /// <summary>
        ///     Obtiene el nombre completo del método que ha sido llamado.
        /// </summary>
        public string Method { get; }

        /// <summary>
        ///     Obtiene la marca de tiempo que indica el momento en el que se
        ///     ha realizado una llamada al método remoto.
        /// </summary>
        public DateTime Timestamp { get; } = DateTime.Now;

        /// <summary>
        ///     Cancela la llamada remota.
        /// </summary>
        public void Abort()
        {
            Waiter.SetResult(Array.Empty<byte>());
        }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public abstract class MrpcCaller : IMessageTarget //internal
    {
        private delegate bool SerializationMethod(object obj, BinaryWriter writer, List<object> processed);
        //private delegate bool DeserializationMethod(Type type, BinaryReader reader, List<object> processed);

        private static readonly List<IDataSerializer> _serializers = Objects.FindAllObjects<IDataSerializer>().ToList();
        private static readonly List<SerializationMethod> _serializationFuncs = new List<SerializationMethod>();
        //private static readonly List<DeserializationMethod> _deserializationFuncs = new List<DeserializationMethod>();

        static MrpcCaller()
        {
            _serializationFuncs.AddRange(new SerializationMethod[]
            { 
                SerializeNull,
                SerializeCircularRef,
                SerializeWithIDataSerializer,
                SerializeCollections,
                SerializeModels
            });
        }

        private static bool SerializeModels(object obj, BinaryWriter writer, List<object> processed)
        {
            var t = obj.GetType();
            if (!t.GetProperties().Any(PropertyInfoExtensions.IsReadWrite)) return false;
            
            processed.Add(obj);
            foreach (var p in t.GetProperties().Where(PropertyInfoExtensions.IsReadWrite))
            {
                Serialize(p.GetValue(obj), writer, processed);
            }
            return true;
        }

        private static bool SerializeCollections(object obj, BinaryWriter writer, List<object> processed)
        {
            if (!(obj is IEnumerable e)) return false;

            var en = e.GetEnumerator();
            writer.Write(ObjectKind.Array);
            while (en.MoveNext()) Serialize(en.Current, writer, processed);
            return true;
        }

        private static bool SerializeWithIDataSerializer(object obj, BinaryWriter writer, List<object> processed)
        {
            var t = obj.GetType();
            if (!(_serializers.FirstOrDefault(p => p.Handles(t)) is { } s)) return false;
            
            writer.Write(ObjectKind.Simple);
            s.Write(obj, writer);
            return true;
        }

        private static bool SerializeCircularRef(object obj, BinaryWriter writer, List<object> processed)
        {
            if (!processed.Contains(obj)) return false;
            
            writer.Write(ObjectKind.CircPointer);
            writer.Write(processed.IndexOf(obj));
            return true;
        }

        private static bool SerializeNull(object? obj, BinaryWriter writer, List<object> processed)
        {
            if (!(obj is null)) return false;            
            writer.Write(ObjectKind.Null);
            return true;
        }






        private static void Serialize(object j, BinaryWriter bw, List<object> processed)
        {
            if (_serializationFuncs.Any(p => p(j, bw, processed))) return;
            throw new UntransmittableObjectException(j);
        }




        private readonly Dictionary<Guid, CallLock> _callPool = new Dictionary<Guid, CallLock>();
        private readonly IMrpcChannel _channel;

        public MrpcCaller(IMrpcChannel channel)
        {
            _channel = channel;
        }







        private MemoryStream BuildPayload(MethodInfo callee, object?[] args, out Guid? guid)
        {
            var payload = new MemoryStream();

            using var bw = new BinaryWriter(payload);
            guid = WriteHeader(bw, callee, args);
            WriteArgs(bw, callee, args);

            return payload;
        }

        private Guid? WriteHeader(BinaryWriter bw, MethodInfo callee, object?[] args)
        {
            Guid? guid = null;
            if (!callee.IsVoid())
            {
                bw.Write(true);
                guid = Guid.NewGuid();
                bw.Write(guid.Value);
            }
            else
            {
                bw.Write(false);
            }

            bw.Write(callee.FullName());
            bw.Write(args.Length);
            return guid;
        }

        private void WriteArgs(BinaryWriter bw, MethodInfo callee, object?[] args)
        {
            var refArgs = callee.GetParameters().Select(p => p.IsRetval ? true : (p.IsOut ? (bool?)false : null)).ToArray();
            var c = 0;
            foreach (var j in args)
            {
                if (refArgs[c] == false)
                {
                    bw.Write(ObjectKind.Out);
                    continue;
                }

                if (refArgs[c].HasValue) bw.Write(ObjectKind.Ref);
                Serialize(j!, bw, new List<object>());
                c++;
            }
        }


        protected void RemoteCall(params object?[] args)
        {
            InternalRemoteCall<object?>(args).GetAwaiter().GetResult();
        }

        protected T RemoteCall<T>(params object?[] args)
        {
            return InternalRemoteCall<T>(args).GetAwaiter().GetResult();
        }

        protected Task RemoteCallAsync(params object?[] args)
        {
            return InternalRemoteCall<object?>(args);
        }

        protected Task<T> RemoteCallAsync<T>(params object?[] args)
        {
            return InternalRemoteCall<T>(args);
        }







        private async Task<T> InternalRemoteCall<T>(object?[] args)
        {
            var callee = ReflectionHelpers.GetCallingMethod(2)!;
            var payload = BuildPayload(callee, args, out var guid);
            
            if (guid is { } g) 
            { 
                var l = new CallLock(callee);
                _callPool.Add(g, l);
                _channel.Send(payload.ToArray());

                var result = await l.Waiter.Task;
                if (result.Length == 0) return default!;




            }
            else
            {
                _channel.Send(payload.ToArray());
                return default!;
            }
        }

        void IMessageTarget.Recieve(byte[] data)
        {
            
        }
    }

    /// <summary>
    ///     Cliente de llamada a procedimiento remoto (RPC) de MCART.
    /// </summary>
    /// <typeparam name="T">
    ///     Interfaz que describe el contrato remoto de funciones disponibles.
    /// </typeparam>
    public class MrpcClient<T>
    {
        /// <summary>
        ///     Inicializa la clase <see cref="MrpcClient{T}"/>
        /// </summary>
        static MrpcClient()
        {
            if (!typeof(T).IsInterface) throw new InvalidTypeException(string.Format(MrpcStrings.ErrInterfaceExpected, typeof(T).Name), typeof(T));
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="MrpcClient{T}"/>, especificando el canal de 
        ///     comunicación a utilizar.
        /// </summary>
        /// <param name="channel">
        ///     Canal de comunicación a utilizar.
        /// </param>
        public MrpcClient(IMrpcChannel channel)
        {
            (_channel = channel).MessageTarget = this;

            //TODO: compilar e instanciar cliente Mrpc.
            Call = default!;
        }

        /// <summary>
        ///     Expone el contrato de métodos remotos que pueden ser llamados utilizando esta instancia.
        /// </summary>
        public T Call { get; }
    }
}
