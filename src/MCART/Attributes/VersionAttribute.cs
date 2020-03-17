/*
VersionAttribute.cs

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
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <summary>
    /// Especifica la versión del elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class VersionAttribute : VersionAttributeBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="VersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public VersionAttribute(int major, int minor) : base(major, minor, int.MaxValue, int.MaxValue)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="VersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public VersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev)
        {
        }
    }
}