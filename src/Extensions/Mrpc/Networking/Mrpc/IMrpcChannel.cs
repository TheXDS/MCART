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
    /// Define una serie de miembros a implementar por un tipo que provea
    /// de un túnel de comunicación entre la aplicación local y un servicio
    /// remoto de Mrpc.
    /// </summary>
    public interface IMrpcChannel
    {
        /// <summary>
        /// Envía los datos a un servicio remoto.
        /// </summary>
        /// <param name="payload">
        /// Paquete de bytes a enviar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si los datos se enviaron correctamente,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        bool Send(byte[] payload);

        /// <summary>
        /// Obtiene una referencia al objetivo de mensajes que está a la
        /// espera de datos provenientes del servicio remoto.
        /// </summary>
        IMessageTarget MessageTarget { get; internal set; }
    }
}
