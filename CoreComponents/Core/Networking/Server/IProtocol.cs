//
//  IProtocol.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace TheXDS.MCART.Networking.Server
{
    /// <summary>
    /// Determina una serie de funciones a implementar por una clase que provea
    /// de protocolos a un servidor.
    /// </summary>
    /// <typeparam name="TClient">
    /// Tipo de clientes que este protocolo es capaz de atender.
    /// </typeparam>
    public interface IProtocol<TClient> where TClient : Client
    {
        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        void ClientAttendant(TClient client, Server<TClient> server, byte[] data);
        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns><c>true</c> si el cliente fue aceptado por el protocolo, <c>false</c> en caso contrario.</returns>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        bool ClientWelcome(TClient client, Server<TClient> server);
        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        void ClientBye(TClient client, Server<TClient> server);
        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        /// <param name="server">Servidor que atiendía al cliente.</param>
        void ClientDisconnect(TClient client, Server<TClient> server);
    }
}