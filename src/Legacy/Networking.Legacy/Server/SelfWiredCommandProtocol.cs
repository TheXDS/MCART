/*
SelfWiredCommandProtocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Clase base para crear protocolos simples con bytes de comandos.
    /// </summary>
    /// <typeparam name="TClient">
    /// Tipo de clientes a atender.
    /// </typeparam>
    /// <typeparam name="TCommand">
    /// Tipo de la enumeración de comandos que el protocolo aceptará.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo de la enumeración de las respuestas que este protocolo
    /// devolverá a los clientes conectados.
    /// </typeparam>
    [Obsolete("Este es un componente legado.")]
    public abstract class SelfWiredCommandProtocol<TClient, TCommand, TResponse> : ServerProtocol<TClient>
        where TClient : Client where TCommand : struct, Enum where TResponse : struct, Enum
    {
        /// <summary>
        /// Describe la firma de un comando del protocolo.
        /// </summary>
        public delegate void CommandCallback(object instance, BinaryReader br, TClient client, Server<TClient> server);

        private static readonly TResponse? _errResponse;
        private static readonly TResponse? _notMappedResponse;
        private static readonly MethodInfo _readCmd;
        private static readonly TResponse? _unkResponse;

        private readonly Dictionary<TCommand, CommandCallback> _commands =
            new();

        private static Func<TResponse, byte[]> ToResponse { get; }

        /// <summary>
        /// Inicializa la clase
        /// <see cref="SelfWiredCommandProtocol{TClient,TCommand,TResponse}" />.
        /// </summary>
        /// <remarks>
        /// Este método realiza inicializaciones especiales, como
        /// determinar el método a utilizar para leer y escribir valores de
        /// enumeración desde y hacia un <see cref="Stream" /> o un arreglo
        /// de bytes. Además, inicializa respuestas predeterminadas si las
        /// mismas se encuentran definidas en la enumeración de respuestas
        /// de <typeparamref name="TResponse" />.
        /// </remarks>
        static SelfWiredCommandProtocol()
        {
            ToResponse = EnumExtensions.ToBytes<TResponse>();

            Type? tCmd = typeof(TCommand).GetEnumUnderlyingType();
            _readCmd = typeof(BinaryReader).GetMethods().FirstOrDefault(p =>
                          p.Name.StartsWith("Read")
                          && p.GetParameters().Length == 0
                          && p.ReturnType == tCmd)
                      ?? throw new PlatformNotSupportedException();
            TResponse[]? vals = Enum.GetValues(typeof(TResponse)).OfType<TResponse>().ToArray();
            _errResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            _unkResponse = vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>());
            _notMappedResponse = vals.FirstOrDefault(p => p.HasAttr<NotMappedResponseAttribute>());
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SelfWiredCommandProtocol{TClient,TCommand,TResponse}" />
        /// </summary>
        /// <remarks>
        /// En este método se realiza el mapeo entre comandos aceptados por
        /// el protocolo y sus respectivos métodos de atención.
        /// </remarks>
        protected SelfWiredCommandProtocol()
        {
            Type? tCmdAttr = Objects.GetTypes<IValueAttribute<TCommand>>(true).FirstOrDefault() ??
                           throw new MissingTypeException(typeof(IValueAttribute<TCommand>));
            foreach (SelfWiredCommandProtocol<TClient, TCommand, TResponse>.CommandCallback? j in
                GetType().GetMethods().WithSignature<CommandCallback>()
                    .Concat(this.PropertiesOf<CommandCallback>())
                    .Concat(this.FieldsOf<CommandCallback>()))
            {
                List<IValueAttribute<TCommand>>? attrs = j.Method.GetCustomAttributes(tCmdAttr, false).OfType<IValueAttribute<TCommand>>().ToList();
                if (attrs.Any()) // Mapeo por configuración
                {
                    foreach (IValueAttribute<TCommand>? k in attrs)
                    {
                        if (_commands.ContainsKey(k.Value)) throw Errors.DuplicateData(k.Value);
                        _commands.Add(k.Value, j);
                    }
                }
                else // Mapeo por convención
                {
                    if (!Enum.TryParse(j.Method.Name, out TCommand k)) continue;
                    if (_commands.ContainsKey(k)) throw Errors.DuplicateData(k);
                    _commands.Add(k, j);
                }
            }
        }

        /// <summary>
        /// Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <param name="response">
        /// Valor a partir del cual generar la respuesta.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes con la respuesta, al cual se pueden
        /// concatenar más datos.
        /// </returns>
        public static byte[]? MakeResponse(TResponse response)
        {
            return ToResponse?.Invoke(response);
        }

        /// <summary>
        /// Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <param name="response">
        /// Valor a partir del cual generar la respuesta.
        /// </param>
        /// <param name="data">
        /// Arreglo de bytes con datos adicionales a adjuntar al datagrama
        /// de respuesta.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes con la respuesta, al cual se pueden
        /// concatenar más datos.
        /// </returns>
        public static byte[]? MakeResponse(TResponse response, IEnumerable<byte> data)
        {
            return MakeResponse(response)?.Concat(data).ToArray();
        }

        /// <summary>
        /// Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <param name="response">
        /// Valor a partir del cual generar la respuesta.
        /// </param>
        /// <param name="data">
        /// Cadena de texto a adjuntar al datagrama de respuesta.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes con la respuesta, al cual se pueden
        /// concatenar más datos.
        /// </returns>
        public static byte[]? MakeResponse(TResponse response, string data)
        {
            return MakeResponse(response, new[] { data });
        }

        /// <summary>
        /// Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <param name="response">
        /// Valor a partir del cual generar la respuesta.
        /// </param>
        /// <param name="data">
        /// Enumeración con múltiples cadenas de texto adicionales a
        /// adjuntar al datagrama de respuesta.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes con la respuesta, al cual se pueden
        /// concatenar más datos.
        /// </returns>
        public static byte[]? MakeResponse(TResponse response, IEnumerable<string> data)
        {
            using MemoryStream? ms = new();
            using BinaryWriter? bw = new(ms);
            foreach (string? j in data) bw.Write(j);
            return MakeResponse(response, ms);
        }

        /// <summary>
        /// Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <param name="response">
        /// Valor a partir del cual generar la respuesta.
        /// </param>
        /// <param name="data">
        /// Flujo de datos que contiene la información a adjuntar al
        /// datagrama de respuesta.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes con la respuesta, al cual se pueden
        /// concatenar más datos.
        /// </returns>
        public static byte[]? MakeResponse(TResponse response, MemoryStream data)
        {
            return MakeResponse(response, data.ToArray());
        }

        /// <summary>
        /// Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <param name="response">
        /// Valor a partir del cual generar la respuesta.
        /// </param>
        /// <param name="data">
        /// Flujo de datos que contiene la información a adjuntar al
        /// datagrama de respuesta.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes con la respuesta, al cual se pueden
        /// concatenar más datos.
        /// </returns>
        public static byte[]? MakeResponse(TResponse response, Stream data)
        {
            if (!data.CanRead) throw new InvalidOperationException();
            if (data.CanSeek)
                using (BinaryReader? sr = new(data))
                {
                    return MakeResponse(response, sr.ReadBytes((int)data.Length));
                }

            using MemoryStream? ms = new();
            data.CopyTo(ms);
            return MakeResponse(response, ms.ToArray());
        }

        /// <summary>
        /// Lee un comando desde el <see cref="BinaryReader" /> especificado.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader" /> desde el cual leer la información.</param>
        /// <returns>
        /// Un comando leído desde el <see cref="BinaryReader" /> especificado.
        /// </returns>
        public static TCommand ReadCommand(BinaryReader br)
        {
            return (TCommand)Enum.ToObject(typeof(TCommand), _readCmd.Invoke(br, Array.Empty<object>())!);
        }

        /// <summary>
        /// Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        /// <exception cref="InvalidOperationException">
        /// Se produce cuando el servidor encuentra un problema y no
        /// existen respuestas mapeadas de error, comando inválido o
        /// comando no mapeado.
        /// </exception>
        public override void ClientAttendant(TClient client, byte[] data)
        {
            using BinaryReader? br = new(new MemoryStream(data));
            TCommand c = ReadCommand(br);

            if (!Enum.IsDefined(typeof(TCommand), c))
                client.Send(ToResponse(_unkResponse ?? _errResponse ?? throw new InvalidOperationException()));

            if (_commands.ContainsKey(c))
                try
                {
                    _commands[c](this, br, client, Server);
                }
                catch
                {
                    client.Send(ToResponse(_errResponse ?? throw new InvalidOperationException()));
                }
            else
                client.Send(
                    ToResponse(_notMappedResponse ?? _errResponse ?? throw new InvalidOperationException()));
        }
    }
}