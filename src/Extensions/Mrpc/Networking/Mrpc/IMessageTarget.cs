/*
NameAttribute.cs

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

namespace TheXDS.MCART.Networking.Mrpc
{
    /// <summary>
    ///     Define una serie de miembros a implementar por una tipo que provea
    ///     de métodos por medio de los cuales se pueda notificar a la
    ///     instancia sobre la recepción de mensajes desde un servicio remoto.
    /// </summary>
    internal interface IMessageTarget
    {
        /// <summary>
        ///     Notifica a esta instancia sobre la recepción de un mensaje.
        /// </summary>
        /// <param name="data">
        ///     Mensaje que ha sido enviado por el servicio remoto.
        /// </param>
        void Recieve(byte[] data);
    }
}
