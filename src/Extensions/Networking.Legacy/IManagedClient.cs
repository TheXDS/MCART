/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene definiciones misceláneas y compartidas en el espacio de
nombres TheXDS.MCART.Networking.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System;

namespace TheXDS.MCART.Networking.Legacy
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que
    /// represente a un cliente de red administrado. 
    /// </summary>
    public interface IManagedClient
    {
        /// <summary>
        /// Obtiene un valor que indica que esta conexión está encriptada.
        /// </summary>
        bool Encrypted { get; }
        
        /// <summary>
        /// Obtiene un valor que indica si la conexión utiliza compresión.
        /// </summary>
        bool Compressed { get; }

        /// <summary>
        /// Obtiene un valor que indica si el cliente espera la bandera de
        /// presencia de Guid, y el Guid correspondiente.
        /// </summary>
        bool ExpectsGuid { get; }

        /// <summary>
        /// Obtiene un valor que indica si el cliente envía comandos junto
        /// con un <see cref="Guid"/> de identificación.
        /// </summary>
        bool SendsGuid { get; }
    }
}