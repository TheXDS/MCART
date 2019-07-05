/*
SelfWiredCommandClient.cs

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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Client
{
    /// <inheritdoc />
    /// <summary>
    ///     Clase base para clientes auto-cableados de atención a protocolos de
    ///     comandos simples basados en la clase
    ///     <see cref="T:TheXDS.MCART.Networking.Server.SelfWiredCommandProtocol`3" />.
    /// </summary>
    /// <typeparam name="TCommand">
    ///     Tipo de enumeración de los comandos enviados por este cliente.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     Tipo de enumeración de las respuestas manejadas por este cliente.
    /// </typeparam>
    /// <remarks>
    ///     Debido a las limitaciones actuales de C#, para poder implementar
    ///     este protocolo es necesario crear un atributo aplicable a los
    ///     métodos miembros de la clase que derive de
    ///     <see cref="T:TheXDS.MCART.Networking.Client.SelfWiredCommandClient`2" />, dicho
    ///     atributo deberá implementar <see cref="T:TheXDS.MCART.Attributes.IValueAttribute`1" /> y ser
    ///     aplicado a cada método que pueda manejar respuestas del servidor.
    ///     Tales métodos deberán a su vez, ser compatibles con el delegado
    ///     <see cref="T:TheXDS.MCART.Networking.Client.SelfWiredCommandClient`2.ResponseCallBack" />.
    ///     Los comandos y las respuestas son enumeraciones comunes.
    /// </remarks>
    /// <example>
    ///     Este ejemplo define un protocolo sencillo que imprime mensajes enviados por el servidor.
    ///     <code language="cs" source="..\..\Documentation\Examples\Networking\Client\SelfWiredCommandClient.cs"
    ///         region="example1" />
    ///     <code language="vb" source="..\..\Documentation\Examples\Networking\Client\SelfWiredCommandClient.vb"
    ///         region="example1" />
    /// </example>
    [Obsolete(Resources.InternalStrings.LegacyComponent)]
    public abstract class SelfWiredCommandClient<TCommand, TResponse> : ActiveClient
        where TCommand : struct, Enum where TResponse : struct, Enum
    {
        /// <summary>
        ///     Describe la firma de una respuesta del protocolo.
        /// </summary>
        public delegate void ResponseCallBack(object instance, BinaryReader br);

        private static readonly TResponse? _errResponse;
        private static readonly MethodInfo _readRsp;
        private static readonly TResponse? _unkResponse;

        private readonly ConcurrentQueue<ResponseCallBack> _interrupts =
            new ConcurrentQueue<ResponseCallBack>();

        private readonly Dictionary<TResponse, ResponseCallBack> _responses =
            new Dictionary<TResponse, ResponseCallBack>();

        /// <summary>
        ///     Genera un arreglo de bytes de comando al servidor a partir del
        ///     valor especificado.
        /// </summary>
        /// <returns>
        ///     Un arreglo de bytes que contiene los bytes que representan al
        ///     comando, a partir del cual se pueden concatenar más datos para
        ///     construir una solicitud completa.
        /// </returns>
        private static Func<TCommand, byte[]> ToCommand { get; }

        static SelfWiredCommandClient()
        {
            ToCommand = EnumExtensions.ToBytes<TCommand>();

            var tRsp = typeof(TResponse).GetEnumUnderlyingType();
            _readRsp = typeof(BinaryReader).GetMethods().FirstOrDefault(p =>
                          p.Name.StartsWith("Read")
                          && p.GetParameters().Length == 0
                          && p.ReturnType == tRsp)
                      ?? throw new PlatformNotSupportedException();

            var vals = Enum.GetValues(typeof(TResponse)).OfType<TResponse?>().ToArray();
            _errResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            _unkResponse = vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>());
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="SelfWiredCommandClient{TCommand,TResponse}" />.
        /// </summary>
        protected SelfWiredCommandClient()
        {
            var tCmdAttr = Objects.GetTypes<IValueAttribute<TResponse>>(true).FirstOrDefault() ??
                           throw new MissingTypeException(typeof(IValueAttribute<TResponse>));
            foreach (var j in
                GetType().GetMethods().WithSignature<ResponseCallBack>()
                    .Concat(this.PropertiesOf<ResponseCallBack>())
                    .Concat(this.FieldsOf<ResponseCallBack>()))
            {
                var attrs = j.Method.GetCustomAttributes(tCmdAttr, false).OfType<IValueAttribute<TResponse>>().ToList();
                if (attrs.Any()) // Mapeo por configuración
                {
                    foreach (var k in attrs)
                    {
                        if (_responses.ContainsKey(k.Value)) throw new DataAlreadyExistsException();
                        _responses.Add(k.Value, j);
                    }
                }
                else // Mapeo por convención
                {
                    if (!Enum.TryParse(j.Method.Name, out TResponse k)) continue;
                    if (_responses.ContainsKey(k)) throw new DataAlreadyExistsException();
                    _responses.Add(k, j);
                }
            }
        }

        /// <summary>
        ///     Genera un arreglo de bytes con la solicitud de este cliente.
        /// </summary>
        /// <param name="command">
        ///     Valor a partir del cual generar la solicitud.
        /// </param>
        /// <returns>
        ///     Un arreglo de bytes con la solicitud, al cual se pueden
        ///     concatenar más datos.
        /// </returns>
        public static byte[] MakeCommand(TCommand command)
        {
            return ToCommand?.Invoke(command);
        }

        /// <summary>
        ///     Genera un arreglo de bytes con la solicitud de este cliente.
        /// </summary>
        /// <param name="command">
        ///     Valor a partir del cual generar la solicitud.
        /// </param>
        /// <param name="data">
        ///     Arreglo de bytes con datos adicionales a adjuntar al datagrama
        ///     de respuesta.
        /// </param>
        /// <returns>
        ///     Un arreglo de bytes con la solicitud, al cual se pueden
        ///     concatenar más datos.
        /// </returns>
        public static byte[] MakeCommand(TCommand command, IEnumerable<byte> data)
        {
            return MakeCommand(command)?.Concat(data).ToArray();
        }

        /// <summary>
        ///     Genera un arreglo de bytes con la solicitud de este cliente.
        /// </summary>
        /// <param name="command">
        ///     Valor a partir del cual generar la solicitud.
        /// </param>
        /// <param name="data">
        ///     Cadena de texto a adjuntar al datagrama de solicitud.
        /// </param>
        /// <returns>
        ///     Un arreglo de bytes con la solicitud, al cual se pueden
        ///     concatenar más datos.
        /// </returns>
        public static byte[] MakeCommand(TCommand command, string data)
        {
            return MakeCommand(command, new[] {data});
        }

        /// <summary>
        ///     Genera un arreglo de bytes con la solicitud de este cliente.
        /// </summary>
        /// <param name="command">
        ///     Valor a partir del cual generar la solicitud.
        /// </param>
        /// <param name="data">
        ///     Enumeración con múltiples cadenas de texto adicionales a
        ///     adjuntar al datagrama de solicitud.
        /// </param>
        /// <returns>
        ///     Un arreglo de bytes con la solicitud, al cual se pueden
        ///     concatenar más datos.
        /// </returns>
        public static byte[] MakeCommand(TCommand command, IEnumerable<string> data)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            foreach (var j in data) bw.Write(j);
            return MakeCommand(command, ms);
        }

        /// <summary>
        ///     Genera un arreglo de bytes con la solicitud de este cliente.
        /// </summary>
        /// <param name="command">
        ///     Valor a partir del cual generar la solicitud.
        /// </param>
        /// <param name="data">
        ///     Flujo de datos que contiene la información a adjuntar al
        ///     datagrama de solicitud.
        /// </param>
        /// <returns>
        ///     Un arreglo de bytes con la solicitud, al cual se pueden
        ///     concatenar más datos.
        /// </returns>
        public static byte[] MakeCommand(TCommand command, MemoryStream data)
        {
            return MakeCommand(command, data.ToArray());
        }

        /// <summary>
        ///     Genera un arreglo de bytes con la solicitud de este cliente.
        /// </summary>
        /// <param name="command">
        ///     Valor a partir del cual generar la solicitud.
        /// </param>
        /// <param name="data">
        ///     Flujo de datos que contiene la información a adjuntar al
        ///     datagrama de solicitud.
        /// </param>
        /// <returns>
        ///     Un arreglo de bytes con la solicitud, al cual se pueden
        ///     concatenar más datos.
        /// </returns>
        public static byte[] MakeCommand(TCommand command, Stream data)
        {
            if (!data.CanRead) throw new InvalidOperationException();
            if (data.CanSeek)
            {
                using var sr = new BinaryReader(data);
                return MakeCommand(command, sr.ReadBytes((int)data.Length));
            }

            using var ms = new MemoryStream();
            data.CopyTo(ms);
            return MakeCommand(command, ms.ToArray());
        }

        /// <inheritdoc />
        /// <summary>
        ///     Atiende una solicitud realizada por el servidor cuando no
        ///     existe un método mapeado a la respuesta recibida.
        /// </summary>
        /// <param name="data">Datos recibidos desde el servidor.</param>
        /// <remarks>
        ///     De forma predeterminada, este método no realiza ninguna acción.
        ///     Invalide este método en caso de que la implementación no mapee
        ///     todas las respuestas que el servidor podría enviar, en cuyo
        ///     caso, serán atendidas aquí.
        /// </remarks>
        public override void AttendServer(byte[] data)
        {
            /* No realizar ninguna acción. */
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicia la escucha activa del servidor.
        /// </summary>
        protected override async sealed void PostConnection()
        {
            while (GetNs() is NetworkStream ns)
            {
                var outp = await GetDataAsync(ns);
                var ms = new MemoryStream(outp);
                var br = new BinaryReader(ms);
                if (_interrupts.TryDequeue(out var callback))
                {
#pragma warning disable 4014
                    Task.Run(() =>
                    {
                        callback.Invoke(this, br);
                        br.Dispose();
                        ms.Dispose();
                    });
#pragma warning restore 4014
                }
                else
                {
                    var cmd = ReadResponse(br);
                    if (_errResponse.Equals(cmd)) ServerError?.Invoke(this, EventArgs.Empty);
                    if (_unkResponse.Equals(cmd)) UnknownCommandIssued?.Invoke(this, EventArgs.Empty);
                    if (_responses.ContainsKey(cmd))
                    {
#pragma warning disable 4014
                        Task.Run(() =>
                        {
                            _responses[cmd](this, br);
                            br.Dispose();
                            ms.Dispose();
                        });
                    }
#pragma warning restore 4014
                    else if (outp.Any())
                    {
                        br.Dispose();
                        ms.Dispose();
                        AttendServer(outp);
                    }
                    else
                    {
                        br.Dispose();
                        ms.Dispose();
                    }
                }
            }
        }

        private NetworkStream GetNs()
        {
            try
            {
                return Connection.GetStream();
            }
            catch
            {
                RaiseConnectionLost();
                return null;
            }
        }

        /// <summary>
        ///     Obtiene una respuesta a partir de un <see cref="BinaryReader" />
        ///     activo.
        /// </summary>
        /// <param name="br">
        ///     <see cref="BinaryReader" /> desde el cual se obtendrá la
        ///     información.
        /// </param>
        /// <returns>
        ///     Un <typeparamref name="TResponse" /> con la respuesta enviada
        ///     por el servidor.
        /// </returns>
        public TResponse ReadResponse(BinaryReader br)
        {
            if (br.BaseStream.CanSeek && br.BaseStream.Length == 0) return default;
            return (TResponse) Enum.ToObject(typeof(TResponse), _readRsp.Invoke(br, new object[0]));
        }

        /// <summary>
        ///     Se produce cuando el servidor envía un mensaje indicando estado
        ///     de error.
        /// </summary>
        public event EventHandler ServerError;

        /// <summary>
        ///     Envía un comando al servidor, y ejecuta un método de atención
        ///     cuando el servidor responda, evitando el hilo de atención
        ///     normal.
        /// </summary>
        /// <param name="data">
        ///     Datos adicionales a concatenar a la solicitud.
        /// </param>
        /// <param name="callback">
        ///     Llamada a ejecutar cuando el servidor responda.
        /// </param>
        public void TalkToServer(byte[] data, ResponseCallBack callback)
        {
            if (!(data?.Length > 0)) throw new EmptyCollectionException();
            if (callback is null) throw new ArgumentNullException(nameof(callback));
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            _interrupts.Enqueue(callback);
            ns.Write(data, 0, data.Length);
        }

        /// <summary>
        ///     Envía un comando al servidor, y ejecuta un método de atención
        ///     cuando el servidor responda, evitando el hilo de atención
        ///     normal.
        /// </summary>
        /// <param name="command">Comando a enviar al servidor.</param>
        /// <param name="callback">
        ///     Llamada a ejecutar cuando el servidor responda.
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     Se produce cuando se intenta enviar un comando a un servidor
        ///     cuando la conexión está cerrada.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Se produce si <paramref name="callback" /> es <see langword="null" />.
        /// </exception>
        [DebuggerStepThrough]
        public void TalkToServer(TCommand command, ResponseCallBack callback)
        {
            if (callback is null) throw new ArgumentNullException(nameof(callback));
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            _interrupts.Enqueue(callback);
            var msg = MakeCommand(command);
            ns.Write(msg, 0, msg.Length);
        }

        /// <summary>
        ///     Envía un comando al servidor.
        /// </summary>
        /// <param name="command">Comando a enviar al servidor.</param>
        /// <exception cref="InvalidOperationException">
        ///     Se produce cuando se intenta enviar un comando a un servidor
        ///     cuando la conexión está cerrada.
        /// </exception>
        [DebuggerStepThrough]
        public void TalkToServer(TCommand command)
        {
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            var msg = MakeCommand(command);
            ns.Write(msg, 0, msg.Length);
        }

        /// <summary>
        ///     Envía un comando al servidor de forma asíncrona, y ejecuta un
        ///     método de atención cuando el servidor responda, evitando el
        ///     hilo de atención normal.
        /// </summary>
        /// <param name="data">
        ///     Datos adicionales a concatenar a la solicitud.
        /// </param>
        /// <param name="callback">
        ///     Llamada a ejecutar cuando el servidor responda.
        /// </param>
        /// <returns>
        ///     Un <see cref="Task" /> que permite monitorear la operación.
        /// </returns>
        [DebuggerStepThrough]
        public async Task TalkToServerAsync(byte[] data, ResponseCallBack callback)
        {
            if (!(data?.Length > 0)) throw new ArgumentNullException();
            if (callback is null) throw new ArgumentNullException(nameof(callback));

            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            await ns.WriteAsync(data, 0, data.Length);
            _interrupts.Enqueue(callback);
        }

        /// <summary>
        ///     Envía un comando al servidor de forma asíncrona, y ejecuta un
        ///     método de atención cuando el servidor responda, evitando el
        ///     hilo de atención normal.
        /// </summary>
        /// <param name="command">Comando a enviar al servidor.</param>
        /// <param name="callback">
        ///     Llamada a ejecutar cuando el servidor responda.
        /// </param>
        /// <returns>
        ///     Un <see cref="Task" /> que permite monitorear la operación.
        /// </returns>
        public async Task TalkToServerAsync(TCommand command, ResponseCallBack callback)
        {
            if (callback is null) throw new ArgumentNullException(nameof(callback));
            var msg = MakeCommand(command).ToArray();
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            await ns.WriteAsync(msg, 0, msg.Length);
            _interrupts.Enqueue(callback);
        }

        /// <summary>
        ///     Envía un comando al servidor de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando a enviar al servidor.</param>
        /// <returns>
        ///     Un <see cref="Task" /> que permite monitorear la operación.
        /// </returns>
        public Task TalkToServerAsync(TCommand command)
        {
            var msg = MakeCommand(command).ToArray();
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            return ns.WriteAsync(msg, 0, msg.Length);
        }

        /// <summary>
        ///     Se produce cuando el servidor envía un mensaje indicando que no
        ///     reconoce al comando que se le ha enviado.
        /// </summary>
        public event EventHandler UnknownCommandIssued;
    }
}