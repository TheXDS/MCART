/*
Echo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

namespace TheXDS.MCART.Networking.Server.Protocols
{
    /// <inheritdoc />
    /// <summary>
    /// Protocolo simple de descarte de datos definido según el estándar
    /// RFC 863.
    /// </summary>
    [Port(9)]
    public class Discard : SimpleProtocol
    {
        /// <inheritdoc />
        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public override void ClientAttendant(Client client, byte[] data)
        {
            /* No hacer nada. Descartar los datos. */
        }
    }
}
#endif