/*
Protocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene las implementaciones de la interfaz IProtocol<TClient>
con un cliente que únicamente contiene el nombre de inicio de sesión. Toda la
información pertinente del mismo es manejada dentro del protocolo en el
servidor.

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

using System.Collections.Generic;
using System.IO;
using TheXDS.MCART.Networking.Server;

namespace LightChat
{
    public partial class LightChat : IProtocol<Client<string>>
    {
        /// <summary>
        /// Lista de usuarios registrados para este protocolo.
        /// </summary>
        public Dictionary<string, UserRegistry> Users = new Dictionary<string, UserRegistry>();

        /// <inheritdoc />
        /// <summary>
        /// Atiende al cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public void ClientAttendant(Client<string> client, Server<Client<string>> server, byte[] data)
        {
            using (var br = new BinaryReader(new MemoryStream(data)))
            {
                var c = (Command) br.ReadByte();
                if (_commands.ContainsKey(c))
                    _commands[c](br, client, server);
                else
                    client.Send(NewErr(ErrCodes.InvalidCommand));
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// Realiza funciones de bienvenida al cliente que se acaba de
        /// conectar a este servidor.
        /// </summary>
        /// <param name="client">Cliente que acaba de conectarse.</param>
        /// <param name="server">
        /// Instancia del servidor al cual el cliente se ha conectado.
        /// </param>
        /// <returns>
        /// <see langword="true" /> para indicar que el cliente ha sido aceptado por el
        /// protocolo, <see langword="false" /> para indicar lo contrario.
        /// </returns>
        public bool ClientWelcome(Client<string> client, Server<Client<string>> server)
        {
            /*
            Bonito lugar para verificar si la IP no se encuentra baneada, o para
            obtener la clave pública RSA del cliente y enviar la del servidor en
            caso de utilizar un canal cifrado para las comunicaciones.

            Actualmente, la implementación de LightChat procura ser simple,
            aunque insegura. Se acepta al nuevo cliente sin protesto.
            */
            // TODO: Implementar cifrado RSA para las comunicaciones.
            return true;
        }
        /// <inheritdoc />
        /// <summary>
        /// Ejecuta acciones adicionales cuando un cliente se desconecta.
        /// </summary>
        /// <param name="client">Cliente que se desconectará.</param>
        /// <param name="server">
        /// Servidor al cual el cliente se encontraba conectado.
        /// </param>
        public void ClientBye(Client<string> client, Server<Client<string>> server) { }
        /// <inheritdoc />
        /// <summary>
        /// Ejecuta acciones adicionales cuando se pierde la conectividad con un
        /// cliente.
        /// </summary>
        /// <param name="client">
        /// Cliente con el que se ha perdido la conectividad.
        /// </param>
        /// <param name="server">
        /// Servidor al cual el cliente se encontraba conectado.
        /// </param>
        public void ClientDisconnect(Client<string> client, Server<Client<string>> server)
        {
            if (server.IsAlive)
                server.Broadcast(NewMsg($"{client?.ClientData ?? "Un usuario"} se ha desconectado inesperadamente."), client);
            try { client?.Disconnect(); }
            finally { client = null; }
        }
    }
}