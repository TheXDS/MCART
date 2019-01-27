/*
VersionAttributeBase.cs

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

using System;
#if NETFX_CORE
using System.Runtime.Serialization;
#endif

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Especifica la versión de un elemento, además de ser la clase base para
    ///     los atributos que describan un valor <see cref="Version" /> para un
    ///     elemento.
    /// </summary>
#if NETFX_CORE
    [DataContract]
#else
    [Serializable]
#endif
    public abstract class VersionAttributeBase : Attribute, IValueAttribute<Version>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="VersionAttributeBase" />.
        /// </summary>
        /// <param name="major">Número de versión mayor.</param>
        /// <param name="minor">Número de versión menor.</param>
        /// <param name="build">Número de compilación.</param>
        /// <param name="rev">Número de revisión.</param>
        protected VersionAttributeBase(int major, int minor, int build, int rev)
        {
            Value = new Version(major, minor, build, rev);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
#if NETFX_CORE
        [DataMember]
#endif
        public Version Value { get; }
    }
}