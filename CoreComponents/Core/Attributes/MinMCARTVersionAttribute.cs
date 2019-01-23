﻿/*
MinMCARTVersionAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming

using System;
using static System.AttributeTargets;
#if NETFX_CORE
using System.Runtime.Serialization;
#endif

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Especifica la versión mínima de MCART requerida por el elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
#if NETFX_CORE
    [DataContract]
#else
    [Serializable]
#endif
    public sealed class MinMCARTVersionAttribute : VersionAttributeBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="MinMCARTVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        public MinMCARTVersionAttribute(int major, int minor) : base(major, minor, 0, 0)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="MinMCARTVersionAttribute" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        public MinMCARTVersionAttribute(int major, int minor, int build, int rev) : base(major, minor, build, rev)
        {
        }
    }
}