/*
BindDirection.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be
useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Describe el tipo de enlace a crear.
    /// </summary>
    [Flags]
    public enum BindDirection : byte
    {
        /// <summary>
        /// Inferir enlace basado en el tipo de objetos del enlace.
        /// </summary>
        Infer,

        /// <summary>
        /// Enlace de un sentido.
        /// </summary>
        OneWay,

        /// <summary>
        /// ENlace de un sentido apuntando al origen.
        /// </summary>
        OneWayToSource,

        /// <summary>
        /// Enlace de doble sentido.
        /// </summary>
        TwoWay
    }
}