/*
NameAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using static TheXDS.MCART.Types.Extensions.PropertyInfoExtensions;
using PIE = TheXDS.MCART.Types.Extensions.PropertyInfoExtensions;
using TheXDS.MCART.Mrpc.Resources;

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

    /// <summary>
    /// Clase base para una implementación de Mrpc que ejecute llamadas remotas
    /// desde una aplicación cliente.
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public abstract class MrpcCaller : IMessageTarget
    {
        private static readonly List<IDataSerializer> _serializers = Objects.FindAllObjects<IDataSerializer>().ToList();


        private static bool SerializeModels(object obj, BinaryWriter writer, List<object> processed)
        {
            var t = obj.GetType();
            if (!t.GetProperties().Any(PIE.IsReadWrite)) return false;
            
            processed.Add(obj);
            foreach (var p in t.GetProperties().Where(PIE.IsReadWrite))
            {
                Serialize(p.GetValue(obj)!, writer, processed);
            }
            return true;
        }


        private static bool SerializeWithIDataSerializer(object obj, BinaryWriter writer, List<object> processed)
        {
            var t = obj.GetType();
            if (!(_serializers.FirstOrDefault(p => p.Handles(t)) is { } s)) return false;
            
            processed.Add(obj);
            writer.Write(ObjectKind.Blob);
            s.Write(obj, writer);
            return true;
        }





        private static void SerializeArray(Array obj, BinaryWriter writer, List<object> processed)
        {
            processed.Add(obj);
            writer.Write(ObjectKind.Array);
            writer.Write(obj.Rank);
            var indices = new int[obj.Rank];
            for (var j = 0; j < obj.Rank; j++)
            {
                writer.Write(obj.GetLowerBound(j));
                writer.Write(obj.GetUpperBound(j));

                for (var k = obj.GetLowerBound(j); k <= obj.GetUpperBound(j); k++)
                {
                    indices[j] = k;
                    Serialize(obj.GetValue(indices), writer, processed);
                }
            }
        }


        private static void Serialize(object j, BinaryWriter bw, List<object> processed)
        {
            switch (j)
            {
                case null:
                    bw.Write(ObjectKind.Null);
                    break;
                case Array arr:
                    SerializeArray(arr, bw, processed);
                    break;
                default:
                    if (processed.Contains(j))
                    {
                        bw.Write(ObjectKind.Pointer);
                        bw.Write(processed.IndexOf(j));
                        break;
                    }

                    break;
            }





            if (_serializationFuncs.Any(p => p(j, bw, processed))) return;
            throw new UntransmittableObjectException(j);
        }

        private static object? Deserialize(Type t, BinaryReader br, List<object> processed)
        {
            switch (br.ReadEnum<ObjectKind>())
            {
                case ObjectKind.Null:
                    return t.Default();
                case ObjectKind.Blob:
                    if (_serializers.FirstOrDefault(p => p.Handles(t)) is { } s)
                        return s.Read(br).PushInto(processed);
                    
                    var obj = t.New().PushInto(processed);

                    foreach (var p in t.GetProperties().Where(PIE.IsReadWrite))                    
                        p.SetValue(obj, Deserialize(p.PropertyType, br, processed));
                    
                    return obj;
                case ObjectKind.Array:
                    var count = br.ReadInt32();
                    var l = new List<object?>();                    
                    for (var j = 0; j < count; j++)
                    {
                        l.Add(Deserialize(t.ResolveCollectionType(), br, processed));
                    }
                    return l.ToArray().PushInto(processed);
                case ObjectKind.Pointer:
                    return processed[br.ReadInt32()];
                default: throw new NotImplementedException();
            }
        }











        private void WriteTypeTree(BinaryWriter bw, Type t)
        {
            bw.Write(t.CleanFullName());
            bw.Write(t.GenericTypeArguments.Length);
            foreach (var j in t.GenericTypeArguments)
                WriteTypeTree(bw, j);
        }

        private TypeExpressionTree ReadTypeTree(BinaryReader br)
        {
            var t = new TypeExpressionTree(br.ReadString());
            var c = br.ReadInt32();
            for (int j = 0; j < c; j++)
            {
                t.GenericArgs.Add(ReadTypeTree(br));
            }
            return t;
        }


        private readonly Dictionary<Guid, CallLock> _callPool = new Dictionary<Guid, CallLock>();
        private readonly IMrpcChannel _channel;

        public MrpcCaller(IMrpcChannel channel)
        {
            (_channel = channel).MessageTarget = this;
        }

        private MemoryStream BuildPayload(MethodInfo callee, object?[] args, out Guid? guid)
        {
            var payload = new MemoryStream();

            using var bw = new BinaryWriter(payload);
            guid = WriteHeader(bw, callee);
            WriteArgs(bw, callee, args);

            return payload;
        }

        private Guid? WriteHeader(BinaryWriter bw, MethodInfo callee)
        {
            Guid? guid = null;
            if (callee.IsVoid())
            {
                bw.Write(false);
            }
            else
            {
                bw.Write(true);
                guid = Guid.NewGuid();
                bw.Write(guid.Value);
            }

            // Nombre del método
            bw.Write(callee.Name);            

            // Argumentos genéricos
            bw.Write(callee.GetGenericArguments().Length);
            foreach (var j in callee.GetGenericArguments()) WriteTypeTree(bw, j);

            // Parámetros
            bw.Write(callee.GetParameters().Length);
            foreach (var j in callee.GetParameters()) WriteTypeTree(bw, j.ParameterType);

            return guid;
        }

        private void WriteArgs(BinaryWriter bw, MethodInfo callee, object?[] args)
        {
            var refArgs = callee.GetParameters().Select(p => p.IsRetval ? true : (p.IsOut ? (bool?)false : null)).ToArray();
            var c = 0;
            foreach (var j in args)
            {
                Serialize(j!, bw, new List<object>());
                c++;
            }
        }
        
        /// <summary>
        /// Ejecuta una llamada remota a la implementación del método
        /// actualmente en ejecución.
        /// </summary>
        /// <param name="args">Argumentos del método.</param>
        protected void RemoteCall(params object?[] args)
        {
            InternalRemoteCall<object?>(args).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Ejecuta una llamada remota a la implementación del método
        /// actualmente en ejecución.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de objeto devuelto por el método.
        /// </typeparam>
        /// <param name="args">Argumentos del método.</param>
        /// <returns>
        /// El resultado del método remoto.
        /// </returns>
        protected T RemoteCall<T>(params object?[] args)
        {
            return InternalRemoteCall<T>(args).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Ejecuta una llamada remota a la implementación asíncrona del método
        /// actualmente en ejecución.
        /// </summary>
        /// <param name="args">Argumentos del método.</param>
        /// <returns>
        /// Una tarea que esperará al resultado del método asíncrono remoto.
        /// </returns>
        protected Task RemoteCallAsync(params object?[] args)
        {
            return InternalRemoteCall<object?>(args);
        }

        /// <summary>
        /// Ejecuta una llamada remota a la implementación asíncrona del método
        /// actualmente en ejecución.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de objeto devuelto por la tarea.
        /// </typeparam>
        /// <param name="args">Argumentos del método.</param>
        /// <returns>
        /// Una tarea que esperará al resultado del método asíncrono remoto.
        /// </returns>
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

                using var ms = new MemoryStream(result);
                using var br = new BinaryReader(ms);
                return (T)Deserialize(l.ReturnType!, br, new List<object>())!;
            }
            else
            {
                _channel.Send(payload.ToArray());
                return default!;
            }
        }

        void IMessageTarget.Recieve(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);

            if (br.ReadBoolean())
            {
                var g = br.ReadGuid();
                if (_callPool.Pop(g, out var l))
                {
                    l.Waiter.SetResult(br.ReadBytes((int)ms.RemainingBytes()));
                }
            }
        }
    }

    /// <summary>
    /// Cliente de llamada a procedimiento remoto (RPC) de MCART.
    /// </summary>
    /// <typeparam name="T">
    /// Interfaz que describe el contrato remoto de funciones disponibles.
    /// </typeparam>
    public class MrpcClient<T>
    {
        private readonly IMrpcChannel _channel;

        /// <summary>
        /// Inicializa la clase <see cref="MrpcClient{T}"/>
        /// </summary>
        static MrpcClient()
        {
            if (!typeof(T).IsInterface) throw MrpcErrors.IFaceExpected<T>();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="MrpcClient{T}"/>, especificando el canal de 
        /// comunicación a utilizar.
        /// </summary>
        /// <param name="channel">
        /// Canal de comunicación a utilizar.
        /// </param>
        public MrpcClient(IMrpcChannel channel)
        {
            (_channel = channel).MessageTarget = this;

            //TODO: compilar e instanciar cliente Mrpc.
            Call = default!;
        }

        /// <summary>
        /// Expone el contrato de métodos remotos que pueden ser llamados utilizando esta instancia.
        /// </summary>
        public T Call { get; }
    }


}
