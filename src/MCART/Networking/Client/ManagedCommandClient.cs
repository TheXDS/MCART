/*
ManagedCommandClient.cs

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
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using System.IO.Compression;
using TheXDS.MCART.Security.Cryptography;

namespace TheXDS.MCART.Networking.Client
{
    /// <summary>
    /// Describe un cliente de comandos administrado por MCART.
    /// </summary>
    /// <typeparam name="TCommand">
    /// <see cref="Enum"/> con los comandos generados.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// <see cref="Enum"/> con las respuestas aceptadas.
    /// </typeparam>
    public abstract class ManagedCommandClient<TCommand, TResult> : ActiveClient, IManagedClient where TCommand : struct, Enum where TResult : struct, Enum
    {
        /// <summary>
        /// Describe la firma de una respuesta del protocolo.
        /// </summary>
        public delegate void ResponseCallback(TResult response, BinaryReader br);

        private static readonly Func<TCommand, byte[]> _toCommand = EnumExtensions.ToBytes<TCommand>();
        private static readonly TResult? _errResponse;
        private static readonly TResult? _notMappedResponse;
        private static readonly TResult? _unkResponse;
        private static readonly MethodInfo _readResp = BinaryReaderExtensions.GetBinaryReadMethod(typeof(TResult).GetEnumUnderlyingType())!;

        /// <summary>
        /// Activa o desactiva el escaneo del tipo de instancia a construir
        /// en busca de funciones de manejo de comandos del protocolo.
        /// </summary>
        public static bool ScanTypeOnCtor { get; set; } = true;

        /// <summary>
        /// Activa o desactiva el mapeo de funciones por convención de
        /// nombres para el manejo de comandos del protocolo.
        /// </summary>
        public static bool EnableMapByName { get; set; } = true;

        /// <summary>
        /// Activa o desactiva el salto de comandos mapeados, efectivamente
        /// causando el efecto inverso en el mecanismo de protección anti
        /// mapeo duplicado.
        /// </summary>
        public static bool SkipMapped { get; set; } = true;

        /// <summary>
        /// Inicializa la clase
        /// <see cref="ManagedCommandClient{TCommand, TResponse}"/>.
        /// </summary>
        static ManagedCommandClient()
        {
            var vals = Enum.GetValues(typeof(TResult)).OfType<TResult>().ToArray();
            _errResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            _unkResponse = (TResult?)vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>()) ?? _errResponse;
            _notMappedResponse = (TResult?)vals.FirstOrDefault(p => p.HasAttr<NotMappedResponseAttribute>()) ?? _unkResponse;
        }

        private readonly HashSet<ManualResetEventSlim> _cmdWaiters = new HashSet<ManualResetEventSlim>();
        private readonly Dictionary<Guid, ResponseCallback> _requests = new Dictionary<Guid, ResponseCallback>();
        private readonly Dictionary<TResult, ResponseCallback> _responses = new Dictionary<TResult, ResponseCallback>();
        private RSACryptoServiceProvider? _rsa = null;

        private static IEnumerable<ResponseCallback> ScanType(ManagedCommandClient<TCommand, TResult> instance)
        {
            return instance.GetType().GetMethods().WithSignature<ResponseCallback>()
                .Concat(instance.PropertiesOf<ResponseCallback>())
                .Concat(instance.FieldsOf<ResponseCallback>());
        }
        private static TResult ReadResponse(BinaryReader br)
        {
            return (TResult)Enum.ToObject(typeof(TResult), _readResp.Invoke(br, Array.Empty<object>())!);
        }

        /// <summary>
        /// Ocurre cuando el servidor informa de un comando válido que no
        /// pudo ser manejado debido a que no se configuró una función de
        /// control para el comando.
        /// </summary>
        public event EventHandler<ValueEventArgs<TCommand>>? NotMappedCommandIssued;

        /// <summary>
        /// Ocurre cuando el servidor informa que se ha enviado un comando
        /// desconocido.
        /// </summary>
        public event EventHandler<ValueEventArgs<TCommand>>? UnknownCommandIssued;

        /// <summary>
        /// Ocurre cuando el servidor ha encontrado un error general.
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? ServerError;

        /// <summary>
        /// Obtiene un valor que indica si esta conexión está encriptada.
        /// </summary>
        public bool Encrypted => !(_rsa is null);

        /// <summary>
        /// Obtiene la clave pública de esta conexión encriptada.
        /// </summary>
        public byte[] GetPubKey() => _rsa?.ExportCspBlob(false) ?? Array.Empty<byte>();

        /// <summary>
        /// Obtiene un valor que indica si la conexión utilizará
        /// compresión.
        /// </summary>
        public bool Compressed { get; protected set; }

        /// <summary>
        /// Obtiene un valor que indica si este cliente espera que las
        /// respuestas enviadas por el servidor a los comandos enviados por
        /// este cliente incluyan un <see cref="Guid"/> de identificación.
        /// </summary>
        public bool ExpectsGuid { get; protected set; } = true;

        /// <summary>
        /// Obtiene un valor que indica si este cliente envía comandos con
        /// un <see cref="Guid"/> de identificación al servidor.
        /// </summary>
        public bool SendsGuid { get; protected set; } = true;

        /// <summary>
        /// Obtiene o establece un valor que indica si el cliente espera
        /// que las respuestas del servidor incluyan un Guid
        /// </summary>
        protected bool IncludesGuid { get; set; } = true;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ManagedCommandClient{TCommand, TResponse}"/>.
        /// </summary>
        protected ManagedCommandClient()
        {
            if (!ScanTypeOnCtor) return;
            foreach (var j in ScanType(this))
            {
                var attrs = j.Method.GetCustomAttributes(false).OfType<IValueAttribute<TResult>>().ToList();
                if (attrs.Any()) // Mapeo por configuración
                {
                    foreach (var k in attrs)
                    {
                        if (_responses.ContainsKey(k.Value)) throw new DataAlreadyExistsException();
                        WireUp(k.Value, j);
                    }
                }
                else if (EnableMapByName) // Mapeo por convención
                {
                    if (!Enum.TryParse(j.Method.Name, out TResult k)) continue;
                    WireUp(k, j);
                }
            }
            foreach (var l in WireUp())
            {
                WireUp(l.Key, l.Value);
            }
            ServerError += (_, e) => AbortCommands();
        }

        /// <summary>
        /// Envía un comando al servidor.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        protected void Send(in TCommand command)
        {
            Send(command, (ResponseCallback?)null);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        protected void Send(in TCommand command, ResponseCallback? callback)
        {
            Send(command, Array.Empty<byte>(), callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// El valor producido por la función <paramref name="callback"/>.
        /// </returns>
        protected T Send<T>(in TCommand command, Func<TResult, BinaryReader, T> callback)
        {
            return Send(command, Array.Empty<byte>(), callback);
        }

        /// <summary>
        /// Envía un comando al servidor.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="rawData">
        /// Datos a enviar al servidor.
        /// </param>
        protected void Send(in TCommand command, IEnumerable<byte> rawData)
        {
            Send(command, rawData, null);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="rawData">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        protected void Send(in TCommand command, IEnumerable<byte> rawData, ResponseCallback? callback)
        {
            var d = MkResp(command, out var guid).Concat(rawData).ToArray();
            if (!(callback is null))
                EnqueueRequest(guid, callback);

            DoSend(d);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="rawData">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// El valor producido por la función <paramref name="callback"/>.
        /// </returns>
        protected T Send<T>(in TCommand command, IEnumerable<byte> rawData, Func<TResult, BinaryReader, T> callback)
        {
            var d = MkResp(command, out var guid);
            var waiting = _cmdWaiters.Push(new ManualResetEventSlim());
            T retVal = default!;
            EnqueueRequest(guid, (r, br) =>
            {
                retVal = callback(r, br);
                waiting.Set();
            });
            DoSend(d.Concat(rawData).ToArray());
            waiting.Wait();
            _cmdWaiters.Remove(waiting);
            return retVal;
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// El valor producido por la función <paramref name="callback"/>.
        /// </returns>
        protected T Send<T>(in TCommand command, MemoryStream data, Func<TResult, BinaryReader, T> callback)
        {
            return Send(command, data.ToArray(), callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        protected void Send(in TCommand command, MemoryStream data)
        {
            Send(command, data.ToArray());
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        protected void Send(in TCommand command, MemoryStream data, ResponseCallback? callback)
        {
            Send(command, data.ToArray(), callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="dataStream">
        /// Datos a enviar al servidor.
        /// </param>
        protected void Send(in TCommand command, Stream dataStream)
        {
            Send(command, dataStream, null);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="dataStream">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        protected void Send(in TCommand command, Stream dataStream, ResponseCallback? callback)
        {
            if (!dataStream.CanRead) throw new InvalidOperationException();
            if (dataStream.CanSeek)
            {
                using var sr = new BinaryReader(dataStream);
                Send(command, sr.ReadBytes((int)dataStream.Length), callback);
                return;
            }
            using var ms = new MemoryStream();
            dataStream.CopyTo(ms);
            Send(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="dataStream">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// El valor producido por la función <paramref name="callback"/>.
        /// </returns>
        protected T Send<T>(in TCommand command, Stream dataStream, Func<TResult, BinaryReader, T> callback)
        {
            if (!dataStream.CanRead) throw new InvalidOperationException();
            if (dataStream.CanSeek)
            {
                using var sr = new BinaryReader(dataStream);
                return Send(command, sr.ReadBytes((int)dataStream.Length), callback);
            }
            using var ms = new MemoryStream();
            dataStream.CopyTo(ms);
            return Send(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        protected void Send(in TCommand command, IEnumerable<string> data)
        {
            Send(command, data, null);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        protected void Send(in TCommand command, IEnumerable<string> data, ResponseCallback? callback)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            foreach (var j in data) bw.Write(j);
            Send(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// El valor producido por la función <paramref name="callback"/>.
        /// </returns>
        protected T Send<T>(in TCommand command, IEnumerable<string> data, Func<TResult, BinaryReader, T> callback)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            foreach (var j in data) bw.Write(j);
            return Send(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        protected void Send(in TCommand command, string data)
        {
            Send(command, new[] { data });
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        protected void Send(in TCommand command, string data, ResponseCallback callback)
        {
            Send(command, new[] { data }, callback);
        }

        /// <summary>
        /// Envía un comando al servidor, y ejecuta una acción al recibir
        /// la respuesta.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        protected T Send<T>(in TCommand command, string data, Func<TResult, BinaryReader, T> callback)
        {
            return Send(command, new[] { data }, callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command)
        {
            return SendAsync(command);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, ResponseCallback? callback)
        {
            var d = MkResp(command, out var guid);
            if (!(callback is null)) EnqueueRequest(guid, callback);
            return DoSendAsync(d);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task<T> SendAsync<T>(TCommand command, Func<TResult, BinaryReader, T> callback)
        {
            return Task.Run(() => Send(command, Array.Empty<byte>(), callback));
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="rawData">
        /// Datos a enviar al servidor.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, IEnumerable<byte> rawData)
        {
            return SendAsync(command, rawData, null);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="rawData">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, IEnumerable<byte> rawData, ResponseCallback? callback)
        {
            var d = MkResp(command, out var guid);
            if (!(callback is null)) EnqueueRequest(guid, callback);
            return DoSendAsync(d.Concat(rawData).ToArray());
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="rawData">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task<T> SendAsync<T>(TCommand command, IEnumerable<byte> rawData, Func<TResult, BinaryReader, T> callback)
        {
            return Task.Run(() => Send(command, rawData, callback));
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, MemoryStream data)
        {
            return SendAsync(command, data, null);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, MemoryStream data, ResponseCallback? callback)
        {
            return SendAsync(command, data.ToArray(), callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task<T> SendAsync<T>(in TCommand command, MemoryStream data, Func<TResult, BinaryReader, T> callback)
        {
            return SendAsync(command, data.ToArray(), callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="dataStream">
        /// Datos a enviar al servidor.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, Stream dataStream)
        {
            return SendAsync(command, dataStream, null);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="dataStream">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, Stream dataStream, ResponseCallback? callback)
        {
            if (!dataStream.CanRead) throw new InvalidOperationException();
            if (dataStream.CanSeek)
            {
                using var sr = new BinaryReader(dataStream);
                return SendAsync(command, sr.ReadBytes((int)dataStream.Length), callback);
            }
            using var ms = new MemoryStream();
            dataStream.CopyTo(ms);
            return SendAsync(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="dataStream">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task<T> SendAsync<T>(in TCommand command, Stream dataStream, Func<TResult, BinaryReader, T> callback)
        {
            if (!dataStream.CanRead) throw new InvalidOperationException();
            if (dataStream.CanSeek)
            {
                using var sr = new BinaryReader(dataStream);
                return SendAsync(command, sr.ReadBytes((int)dataStream.Length), callback);
            }
            using var ms = new MemoryStream();
            dataStream.CopyTo(ms);
            return SendAsync(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, IEnumerable<string> data)
        {
            return SendAsync(command, data, null);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, IEnumerable<string> data, ResponseCallback? callback)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            foreach (var j in data) bw.Write(j);
            return SendAsync(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task<T> SendAsync<T>(in TCommand command, IEnumerable<string> data, Func<TResult, BinaryReader, T> callback)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            foreach (var j in data) bw.Write(j);
            return SendAsync(command, ms, callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, string data)
        {
            return SendAsync(command, data, null);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task SendAsync(in TCommand command, string data, ResponseCallback? callback)
        {
            return SendAsync(command, new[] { data }, callback);
        }

        /// <summary>
        /// Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">
        /// Comando a enviar.
        /// </param>
        /// <param name="data">
        /// Datos a enviar al servidor.
        /// </param>
        /// <param name="callback">
        /// Llamada a realizar cuando el servidor responda.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para vigilar el
        /// estado de la operación.
        /// </returns>
        protected Task<T> SendAsync<T>(in TCommand command, string data, Func<TResult, BinaryReader, T> callback)
        {
            return SendAsync(command, new[] { data }, callback);
        }

        /// <summary>
        /// Indica al cliente que debe encriptar la conexión.
        /// </summary>
        protected void GoEncrypted()
        {
            _rsa = new RSACryptoServiceProvider();
        }

        /// <summary>
        /// Cancela todos los hilos de espera de los comandos enviados al
        /// servidor.
        /// </summary>
        protected void AbortCommands()
        {
            foreach (var j in _cmdWaiters) j.Set();
        }

        /// <summary>
        /// Cuando se invalida, permite obtener una colección con los
        /// métodos que serán llamados al recibir la respuesta
        /// especificada.
        /// </summary>
        /// <returns>
        /// Una enumeración de <see cref="KeyValuePair{TKey, TValue}"/>
        /// cuya llave es una respuesta y cuyo valor es el 
        /// <see cref="ResponseCallback"/> a ejecutar al recibir dicha
        /// respuesta.
        /// </returns>
        protected virtual IEnumerable<KeyValuePair<TResult, ResponseCallback>> WireUp()
        {
            yield break;
        }

        /// <summary>
        /// Conecta manualmente una respuesta con un
        /// <see cref="ResponseCallback"/> de atención.
        /// </summary>
        /// <param name="command">
        /// Respuesta a conectar.
        /// </param>
        /// <param name="action">
        /// Acción a ejecutar al recibir la respuesta.
        /// </param>
        protected void WireUp(in TResult command, ResponseCallback action)
        {
            if (_responses.ContainsKey(command))
            {
                if (SkipMapped) return;
                throw new DataAlreadyExistsException();
            }
            _responses.Add(command, action);
        }

        /// <inheritdoc />
        /// <summary>
        /// Atiende una solicitud realizada por el servidor cuando no
        /// existe un método mapeado a la respuesta recibida.
        /// </summary>
        /// <param name="data">Datos recibidos desde el servidor.</param>
        /// <remarks>
        /// De forma predeterminada, este método no realiza ninguna acción.
        /// Invalide este método en caso de que la implementación no mapee
        /// todas las respuestas que el servidor podría enviar, en cuyo
        /// caso, serán atendidas aquí.
        /// </remarks>
        public override void AttendServer(byte[] data)
        {
            /* No realizar ninguna acción. */
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicia la escucha activa del servidor.
        /// </summary>
        protected sealed override async void PostConnection()
        {
            while (GetStream() is NetworkStream ns)
            {
                try
                {
                    byte[] data = Array.Empty<byte>();
                    try
                    {
                        data = await GetDataAsync(ns);
                    }
                    catch
                    {
                        RaiseConnectionLost();
                        break;
                    }

                    if (data.Length == 0)
                    {
                        CloseConnection();
                        break;
                    }

                    using var ms = new MemoryStream(data);
                    using var br = new BinaryReader(ms);
                    if (IncludesGuid && ms.Length > 17 && br.ReadBoolean())
                    {
                        var guid = br.ReadGuid();
                        if (_requests.TryGetValue(guid, out var callback))
                        {
                            callback.Invoke(ReadResponse(br), br);
                            _requests.Remove(guid);
                            continue;
                        }
                    }

                    var c = ReadResponse(br);
                    if (_responses.TryGetValue(c, out var response))
                    {
                        response.Invoke(c, br);
                    }
                    else if (_notMappedResponse.Equals(c))
                    {
                        NotMappedCommandIssued?.Invoke(this, br.ReadEnum<TCommand>());
                    }
                    else if (_unkResponse.Equals(c))
                    {
                        UnknownCommandIssued?.Invoke(this, br.ReadEnum<TCommand>());
                    }
                    else if (_errResponse.Equals(c))
                    {
                        ServerError?.Invoke(this, new ExceptionEventArgs());
                    }
                    else
                    {
                        AttendServer(br.ReadBytes((int)(ms.Length - ms.Position)));
                    }
                }
                catch (Exception ex) { ServerError?.Invoke(this, ex); }
            }
        }

        /// <summary>
        /// Agrega un manejador de respuesta de un comando enviado con el 
        /// <see cref="Guid"/> especificado.
        /// </summary>
        /// <param name="guid">
        /// Guid de la respuesta esperada.
        /// </param>
        /// <param name="callback">
        /// Método a llamar al recibir la respuesta esperada.
        /// </param>
        protected void EnqueueRequest(in Guid guid, ResponseCallback callback)
        {
            if (!_requests.ContainsKey(guid))
                _requests.Add(guid, callback);
        }

        private void DoSend(IEnumerable<byte> data)
        {
            var d = data.ToArray();
            if (Compressed)
            {
                using var ms = new MemoryStream();
                using var ds = new DeflateStream(ms,CompressionLevel.Optimal);
                ds.Write(d, 0, d.Length);
                d = ms.ToArray();
            }
            if (Encrypted)
            {
                using var ms = new MemoryStream();
                using var cs = RSACryptoTransform.ToStream(ms, _rsa ?? throw new TamperException());
                cs.Write(d, 0, d.Length);
                d = ms.ToArray();
            }
            TalkToServer(d);
        }
        private async Task DoSendAsync(IEnumerable<byte> data)
        {
            var d = data.ToArray();
            if (Compressed)
            {
                using var ms = new MemoryStream();
                using var ds = new DeflateStream(ms, CompressionLevel.Optimal);
                await ds.WriteAsync(d, 0, d.Length);
                d = ms.ToArray();
            }
            if (Encrypted)
            {
                using var ms = new MemoryStream();
                using var cs = RSACryptoTransform.ToStream(ms, _rsa ?? throw new TamperException());
                await cs.WriteAsync(d, 0, d.Length);
                d = ms.ToArray();
            }
            await TalkToServerAsync(d);
        }
        private byte[] MkResp(in TCommand cmd, out Guid guid)
        {
            if (!SendsGuid)
            {
                guid = default;
                return _toCommand(cmd).ToArray();
            }
            guid = Guid.NewGuid();
            return guid.ToByteArray().Concat(_toCommand(cmd)).ToArray();
        }
    }
}