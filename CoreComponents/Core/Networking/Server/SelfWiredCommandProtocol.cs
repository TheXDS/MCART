/*
SelfWiredCommandProtocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

#region Configuración de ReSharper

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable EventNeverSubscribedTo.Global

#endregion

namespace TheXDS.MCART.Networking.Server
{
    /// <inheritdoc />
    /// <summary>
    ///     Clase base para crear protocolos simples con bytes de comandos.
    /// </summary>
    /// <typeparam name="TClient">
    ///     Tipo de clientes a atender.
    /// </typeparam>
    /// <typeparam name="TCommand">
    ///     Tipo de la enumeración de comandos que el protocolo aceptará.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     Tipo de la enumeración de las respuestas que este protocolo
    ///     devolverá a los clientes conectados.
    /// </typeparam>
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public abstract class SelfWiredCommandProtocol<TClient, TCommand, TResponse> : ServerProtocol<TClient>
        where TClient : Client where TCommand : struct, Enum where TResponse : struct, Enum
    {
        /// <summary>
        ///     Describe la firma de un comando del protocolo.
        /// </summary>
        public delegate void CommandCallback(object instance, BinaryReader br, TClient client, Server<TClient> server);

        private static readonly TResponse? ErrResponse;
        private static readonly TResponse? NotMappedResponse;
        private static readonly MethodInfo ReadCmd;
        private static readonly TResponse? UnkResponse;

        private readonly Dictionary<TCommand, CommandCallback> _commands =
            new Dictionary<TCommand, CommandCallback>();

        /// <summary>
        ///     Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <returns>
        ///     Un arreglo de bytes con la respuesta, al cual se pueden concatenar más datos.
        /// </returns>
        public static Func<TResponse, byte[]> MakeResponse { get; }

        /// <summary>
        ///     Inicializa la clase
        ///     <see cref="SelfWiredCommandProtocol{TClient,TCommand,TResponse}" />.
        /// </summary>
        /// <remarks>
        ///     Este método realiza inicializaciones especiales, como
        ///     determinar el método a utilizar para leer y escribir valores de
        ///     enumeración desde y hacia un <see cref="Stream" /> o un arreglo
        ///     de bytes. Además, inicializa respuestas predeterminadas si las
        ///     mismas se encuentran definidas en la enumeración de respuestas
        ///     de <typeparamref name="TResponse" />.
        /// </remarks>
        static SelfWiredCommandProtocol()
        {
            MakeResponse = EnumExtensions.ToBytes<TResponse>();

            var tCmd = typeof(TCommand).GetEnumUnderlyingType();
            ReadCmd = typeof(BinaryReader).GetMethods().FirstOrDefault(p =>
                          p.Name.StartsWith("Read")
                          && p.GetParameters().Length == 0
                          && p.ReturnType == tCmd)
                      ?? throw new PlatformNotSupportedException();
            var vals = Enum.GetValues(typeof(TResponse)).OfType<TResponse?>().ToArray();
            ErrResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            UnkResponse = vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>());
            NotMappedResponse = vals.FirstOrDefault(p => p.HasAttr<NotMappedResponseAttribute>());
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="SelfWiredCommandProtocol{TClient,TCommand,TResponse}" />
        /// </summary>
        /// <remarks>
        ///     En este método se realiza el mapeo entre comandos aceptados por
        ///     el protocolo y sus respectivos métodos de atención.
        /// </remarks>
        protected SelfWiredCommandProtocol()
        {
            var tCmdAttr = Objects.GetTypes<IValueAttribute<TCommand>>(true).FirstOrDefault() ??
                           throw new MissingTypeException(typeof(IValueAttribute<TCommand>));
            foreach (var j in
                GetType().GetMethods().WithSignature<CommandCallback>()
                    .Concat(this.PropertiesOf<CommandCallback>())
                    .Concat(this.FieldsOf<CommandCallback>()))
            {
                var attrs = j.Method.GetCustomAttributes(tCmdAttr, false).OfType<IValueAttribute<TCommand>>().ToList();
                if (attrs.Any()) // Mapeo por configuración
                {
                    foreach (var k in attrs)
                    {
                        if (_commands.ContainsKey(k.Value)) throw new DataAlreadyExistsException();
                        _commands.Add(k.Value, j);
                    }
                }
                else // Mapeo por convención
                {
                    if (!Enum.TryParse(j.Method.Name, out TCommand k)) continue;
                    if (_commands.ContainsKey(k)) throw new DataAlreadyExistsException();
                    _commands.Add(k, j);
                }
            }
        }

        /// <summary>
        ///     Lee un comando desde el <see cref="BinaryReader" /> especificado.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader" /> desde el cual leer la información.</param>
        /// <returns>
        ///     Un comando leído desde el <see cref="BinaryReader" /> especificado.
        /// </returns>
        public static TCommand ReadCommand(BinaryReader br)
        {
            return (TCommand) Enum.ToObject(typeof(TCommand), ReadCmd.Invoke(br, new object[0]));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        /// <exception cref="InvalidOperationException">
        ///     Se produce cuando el servidor encuentra un problema y no
        ///     existen respuestas mapeadas de error, comando inválido o
        ///     comando no mapeado.
        /// </exception>
        public override void ClientAttendant(TClient client, byte[] data)
        {
            using (var br = new BinaryReader(new MemoryStream(data)))
            {
                var c = ReadCommand(br);

                if (!Enum.IsDefined(typeof(TCommand), c))
                    client.Send(MakeResponse(UnkResponse ?? ErrResponse ?? throw new InvalidOperationException()));

                if (_commands.ContainsKey(c))
                    try
                    {
                        _commands[c](this, br, client, Server);
                    }
                    catch
                    {
                        client.Send(MakeResponse(ErrResponse ?? throw new InvalidOperationException()));
                    }
                else
                    client.Send(
                        MakeResponse(NotMappedResponse ?? ErrResponse ?? throw new InvalidOperationException()));
            }
        }
    }
}