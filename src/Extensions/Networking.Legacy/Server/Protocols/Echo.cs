﻿/*
Echo.cs

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

#if ExtrasBuiltIn

namespace TheXDS.MCART.Networking.Legacy.Server.Protocols
{
    /// <summary>
    /// Protocolo simple de eco definido según el estándar RFC 862.
    /// </summary>
    /// <remarks>
    /// Este protocolo utiliza TCP/IP, no IGMP.
    /// </remarks>
    [Port(7)]
    public class Echo : SimpleProtocol
    {
        /// <summary>
        /// Protocolo de atención normal.
        /// </summary>
        /// <param name="client">
        /// Cliente que está siendo atendido debido a una solicitud.
        /// </param>
        /// <param name="data">
        /// Datos que <paramref name="client" /> ha enviado como parte de la
        /// solicitud de atención.
        /// </param>
        public override void ClientAttendant(Client client, byte[] data)
        {
            client.Send(data);
        }
    }
}

#endif