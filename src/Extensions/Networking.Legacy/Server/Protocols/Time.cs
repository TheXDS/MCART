/*
Time.cs

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

using System;
using System.Net.Sockets;
using System.Text;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Legacy.Server.Protocols
{
    /// <summary>
    /// Protocolo de sincronización de hora basado en el estándar RFC 868.
    /// </summary>
    [Port(37)]
    public class Time : RfcSimpleProtocol
    {
        /// <summary>
        /// Envía la hora actual al cliente.
        /// </summary>
        /// <param name="client">Cliente al cual enviar la hora.</param>
        protected sealed override void Send(Client client)
        {
            unchecked
            {
                var l = (int)DateTime.Now.ToTimestamp(DateTimeExtensions.CenturyEpoch);
                if (BitConverter.IsLittleEndian) l = l.FlipEndianess();
                client.Send(BitConverter.GetBytes(l));
            }
        }
    }
}

#endif