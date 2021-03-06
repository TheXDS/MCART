﻿/*
MinMcartVersionAttribute.cs

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

using System;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <summary>
    /// Especifica la versión mínima de MCART requerida por el elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    public sealed class MinMcartVersionAttribute : VersionAttributeBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MinMcartVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public MinMcartVersionAttribute(int major, int minor) : base(major, minor, 0, 0)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MinMcartVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public MinMcartVersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev)
        {
        }
    }
}