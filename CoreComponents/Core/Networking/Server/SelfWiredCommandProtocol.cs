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
    ///     Clase base para crear protocolos simples con bytes de comandos
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public abstract class SelfWiredCommandProtocol<TClient, TCommand, TResponse> : Protocol<TClient>
        where TClient : Client where TCommand : struct, Enum where TResponse : struct, Enum
    {
        /// <summary>
        ///     Describe la firma de un comando del protocolo.
        /// </summary>
        public delegate void CommandCallback(object instance, BinaryReader br, TClient client, Server<TClient> server);

        private static readonly TResponse? ErrResponse;
        private static readonly MethodInfo MakeRsp;
        private static readonly TResponse? NotMappedResponse;
        private static readonly MethodInfo ReadCmd;
        private static readonly TResponse? UnkResponse;

        private readonly Dictionary<TCommand, CommandCallback> _commands =
            new Dictionary<TCommand, CommandCallback>();

        static SelfWiredCommandProtocol()
        {
            var tCmd = typeof(TCommand).GetEnumUnderlyingType();
            var tRsp = typeof(TResponse).GetEnumUnderlyingType();
            ReadCmd = typeof(BinaryReader).GetMethods().FirstOrDefault(p =>
                          p.Name.StartsWith("Read")
                          && p.GetParameters().Length == 0
                          && p.ReturnType == tCmd)
                      ?? throw new PlatformNotSupportedException();

            if (tRsp == typeof(byte))
                MakeRsp = new Func<byte, byte[]>(BypassByte).Method;
            else
                MakeRsp = typeof(BitConverter).GetMethods().FirstOrDefault(p =>
                {
                    var pars = p.GetParameters();
                    return p.Name == nameof(BitConverter.GetBytes)
                           && pars.Length == 1
                           && pars[0].ParameterType == tRsp;
                }) ?? throw new PlatformNotSupportedException();

            var vals = Enum.GetValues(typeof(TResponse)).OfType<TResponse?>().ToArray();

            ErrResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            UnkResponse = vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>());
            NotMappedResponse = vals.FirstOrDefault(p => p.HasAttr<NotMappedResponseAttribute>());
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="SelfWiredCommandProtocol{TClient,TCommand,TResponse}" />
        /// </summary>
        protected SelfWiredCommandProtocol()
        {
            var tCmdAttr = Objects.GetTypes<IValueAttribute<TCommand>>(true).FirstOrDefault() ??
                           throw new MissingTypeException(typeof(IValueAttribute<TCommand>));
            foreach (var j in
                GetType().GetMethods().WithSignature<CommandCallback>()
                    .Concat(this.PropertiesOf<CommandCallback>())
                    .Concat(this.FieldsOf<CommandCallback>()))
            foreach (var k in j.Method.GetCustomAttributes(tCmdAttr, false).OfType<IValueAttribute<TCommand>>())
            {
                if (_commands.ContainsKey(k.Value)) throw new DataAlreadyExistsException();
                _commands.Add(k.Value, j as CommandCallback);
            }
        }

        private static byte[] BypassByte(byte b)
        {
            return new[] {b};
        }

        /// <summary>
        ///     Genera un arreglo de bytes con la respuesta de este servidor.
        /// </summary>
        /// <param name="response">Respuesta a partir de la cual crear el arreglo de bytes.</param>
        /// <returns>
        ///     Un arreglo de bytes con la respuesta, al cual se pueden concatenar más datos.
        /// </returns>
        public static byte[] MakeResponse(TResponse response)
        {
            return (byte[]) MakeRsp.Invoke(null, new object[] {response});
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
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public override void ClientAttendant(TClient client, Server<TClient> server, byte[] data)
        {
            using (var br = new BinaryReader(new MemoryStream(data)))
            {
                var c = ReadCommand(br);

                if (!Enum.IsDefined(typeof(TCommand), c))
                    client.Send(MakeResponse(UnkResponse ?? ErrResponse ?? throw new InvalidOperationException()));

                if (_commands.ContainsKey(c))
                    try
                    {
                        _commands[c](this, br, client, server);
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