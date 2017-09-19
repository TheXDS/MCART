//
//  Echo.cs
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

namespace MCART.Networking.Server.Protocols
{
    /// <summary>
    /// Protocolo simple de eco.
    /// </summary>
    /// <remarks>Este protocolo utiliza TCP/IP, no IGMP.</remarks>
    [Port(7)] public class Echo : Protocol
    {
        /// <summary>
        /// Protocolo de atención normal.
        /// </summary>
        public override void ClientAttendant(Client client, Server<Client> server, byte[] data)
        {
            client.Send(data);
        }
    }
}