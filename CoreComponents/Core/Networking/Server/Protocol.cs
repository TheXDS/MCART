/*
Protocol.cs

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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Networking.Server
{
    /// <inheritdoc />
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor.
    /// </summary>
    /// <typeparam name="TClient">
    /// Tipo de clientes que este protocolo es capaz de atender.
    /// </typeparam>
    public abstract class Protocol<TClient> : IProtocol<TClient> where TClient : Client
    {
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(TClient client, Server<TClient> server, byte[] data);
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> si el cliente fue aceptado por el protocolo,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual bool ClientWelcome(TClient client, Server<TClient> server) { return true; }
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual void ClientBye(TClient client, Server<TClient> server) { }
        /// <inheritdoc />
        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        /// <param name="server">Servidor que atiendía al cliente.</param>
        public virtual void ClientDisconnect(TClient client, Server<TClient> server) { }
    }
    /// <inheritdoc />
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor para clientes que no
    /// requieran un objeto de estado.
    /// </summary>
    public abstract class Protocol : Protocol<Client> { }

    public abstract class SelfWiredCommandProtocol<TClient, TCommand> : Protocol<TClient> where TClient: Client where TCommand : Enum
    {
        /// <summary>
        ///     Describe la firma de un comando del protocolo.
        /// </summary>
        private delegate void DoCommand(BinaryReader br, TClient client, Server<TClient> server);
        private readonly Dictionary<TCommand, DoCommand> _commands = new Dictionary<TCommand, DoCommand>();

        /// <inheritdoc />
        /// <summary>
        /// Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public override void ClientAttendant(TClient client, Server<TClient> server, byte[] data)
        {
            using (var br = new BinaryReader(new MemoryStream(data)))
            {
                TCommand c;
                switch (Marshal.SizeOf<TCommand>())
                {
                    case 1:
                        c = (TCommand) Enum.Parse(typeof(TCommand), br.ReadByte().ToString());
                        break;
                    case 2:
                        c = (TCommand)Enum.Parse(typeof(TCommand), br.ReadInt16().ToString());
                        break;
                    case 4:
                        c = (TCommand)Enum.Parse(typeof(TCommand), br.ReadInt32().ToString());
                        break;
                    case 8:
                        c = (TCommand)Enum.Parse(typeof(TCommand), br.ReadInt64().ToString());
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                if (_commands.ContainsKey(c))
                    _commands[c](br, client, server);
                else
                    //client.Send(NewErr(ErrCodes.InvalidCommand));
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SelfWiredCommandProtocol{TClient,TCommand}"/>
        /// </summary>
        public SelfWiredCommandProtocol()
        {
            var tCmdAttr = Objects.GetTypes<IValueAttribute<TCommand>>(true).FirstOrDefault() ??
                           throw new MissingTypeException(typeof(IValueAttribute<TCommand>));
            foreach (var j in GetType().GetMethods().WithSignature<DoCommand>())
            {
                var attr=j.Method.GetCustomAttributes(tCmdAttr,false).OfType<IValueAttribute<TCommand>>().FirstOrDefault();
                if (attr is null) continue;
                    _commands.Add(attr.Value, j as DoCommand);
            }
        }
    }
}