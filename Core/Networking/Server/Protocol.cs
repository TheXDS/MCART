﻿//
//  Protocol.cs
// 
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace MCART.Networking.Server
{
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor.
    /// </summary>
    public abstract class Protocol<TClient> where TClient : Client
    {
        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(TClient client, Server<TClient> server, byte[] data);
        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns><c>true</c> si el cliente fue aceptado por el protocolo; de lo contrario, <c>false</c>.</returns>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual bool ClientWelcome(TClient client, Server<TClient> server) { return true; }
        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual void ClientBye(TClient client, Server<TClient> server) { }
        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        /// <param name="server">Servidor que atiendía al cliente.</param>
        public virtual void ClientDisconnect(TClient client, Server<TClient> server) { }
    }
}