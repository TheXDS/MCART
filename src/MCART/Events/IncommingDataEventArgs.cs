﻿/*
IncommingDataEventArgs.cs

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

namespace TheXDS.MCART.Events
{
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de
    /// recepción de datos.
    /// </summary>
    public class IncommingDataEventArgs : ValueEventArgs<byte[]>
    {
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con los datos
        /// recibidos.
        /// </summary>
        /// <param name="data">
        /// Colección de <see cref="byte" /> con los datos recibidos.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Se produce si <paramref name="data"/> es <see langword="null"/>.
        /// </exception>
        public IncommingDataEventArgs(byte[] data) : base(data)
        {
            Misc.Internals.NullCheck(data, nameof(data));
        }
    }
}