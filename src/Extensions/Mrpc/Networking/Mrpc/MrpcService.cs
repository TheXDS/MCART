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

    [System.Diagnostics.DebuggerNonUserCode]
    public abstract class MrpcCaller //internal
    {
        private delegate bool SerializationMethod(object obj, BinaryWriter writer, List<object> processed);

        private static readonly List<IDataSerializer> _serializers = Objects.FindAllObjects<IDataSerializer>().ToList();
        private static readonly List<SerializationMethod> _serializationFuncs = new List<SerializationMethod>();

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





        protected T RemoteCall<T>(params object?[] args)
        {
            var callee = ReflectionHelpers.GetCallingMethod()!;
            var refArgs = callee.GetParameters().Select(p => p.IsRetval ? true : (p.IsOut ? (bool?)false : null)).ToArray();
            var c = 0;
            var guid = Guid.NewGuid();
            using var payload = new MemoryStream();
            using var bw = new BinaryWriter(payload);

            bw.Write(guid);
            bw.Write(callee.FullName());

            bw.Write(args.Length);
            foreach (var j in args)
            {
                if (refArgs[c] == false)
                { 
                    bw.Write(ObjectKind.Out);
                    continue;
                }

                if (refArgs[c].HasValue) bw.Write(ObjectKind.Ref);
                Serialize(j!, bw, new List<object>());
            }

            MakeCall(guid).Wait();


        }


    }

    /// <summary>
    ///     Cliente de llamada a procedimiento remoto (RPC) de MCART.
    /// </summary>
    /// <typeparam name="T">
    ///     Interfaz que describe el contrato remoto de funciones disponibles.
    /// </typeparam>
    public class MrpcClient<T> : IMessageTarget
    {
        /// <summary>
        ///     Inicializa la clase <see cref="MrpcClient{T}"/>
        /// </summary>
        static MrpcClient()
        {
            if (!typeof(T).IsInterface) throw new InvalidTypeException(string.Format(MrpcStrings.ErrInterfaceExpected, typeof(T).Name), typeof(T));
        }

        private readonly IMrpcChannel _channel;

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


        public T Call { get; }

        public void Recieve(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
