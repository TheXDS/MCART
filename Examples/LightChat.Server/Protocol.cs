//
//  Protocol.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
                switch ((Command)br.ReadByte())
                {
                    case Command.Login:
                        DoLogin(br, client, server);
                        break;
                    case Command.Logout:
                        DoLogout(br, client, server);
                        break;
                    case Command.List:
                        DoList(br, client, server);
                        break;
                    case Command.Say:
                        DoSay(br, client, server);
                        break;
                    case Command.SayTo:
                        DoSayTo(br, client, server);
                        break;
                    default:
                        // Comando desconocido. Devolver error.
                        client.Send(NewErr(ErrCodes.InvalidCommand));
                        break;
                }
            }
        }
        /// <summary>
        /// Realiza funciones de bienvenida al cliente que se acaba de
        /// conectar a este servidor.
        /// </summary>
        /// <param name="client">Cliente que acaba de conectarse.</param>
        /// <param name="server">
        /// Instancia del servidor al cual el cliente se ha conectado.
        /// </param>
        /// <returns>
        /// <c>true</c> para indicar que el cliente ha sido aceptado por el
        /// protocolo, <c>false</c> para indicar lo contrario.
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
        /// <summary>
        /// Ejecuta acciones adicionales cuando un cliente se desconecta.
        /// </summary>
        /// <param name="client">Cliente que se desconectará.</param>
        /// <param name="server">
        /// Servidor al cual el cliente se encontraba conectado.
        /// </param>
        public void ClientBye(Client<string> client, Server<Client<string>> server) { }
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
                server.Broadcast(NewMsg($"{(client?.userObj ?? "Un usuario")} se ha desconectado inesperadamente."), client);
            try { client?.Disconnect(); }
            finally { client = null; }
        }
    }
}