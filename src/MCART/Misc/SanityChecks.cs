/*
SanityChecks.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART
{
    /// <summary>
    /// Enumera las distintas opciones de comprobación de sanidad al instanciar
    /// tipos.
    /// </summary>
    [Flags]
    public enum SanityChecks : byte
    {
        /// <summary>
        /// Realizar todas las comprobaciones de sanidad.
        /// </summary>
        Default,

        /// <summary>
        /// Ignorar la presencia del atributo <see cref="DangerousAttribute" />.
        /// </summary>
        IgnoreDanger,

        /// <summary>
        /// Ignorar la presencia del atributo <see cref="UnusableAttribute" />.
        /// </summary>
        IgnoreUnusable,

        /// <summary>
        /// Omite todas las comprobaciones de seguridad.
        /// </summary>
        [Dangerous] IgnoreAll = 255
    }
}