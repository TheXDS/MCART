/*
McartComponentAttribute.cs

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

using System;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Attributes
{
    /// <summary>
    /// Marca un ensamblado como un componente de MCART.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class McartComponentAttribute : Attribute, IValueAttribute<RtInfo.ComponentKind>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="McartComponentAttribute" />.
        /// </summary>
        /// <param name="kind">
        /// Tipo de componente que este ensamblado es.
        /// </param>
        public McartComponentAttribute(RtInfo.ComponentKind kind)
        {
            Kind = kind;
        }

        /// <summary>
        /// Obtiene el tipo de componente que es este ensamblado de MCART.
        /// </summary>
        public RtInfo.ComponentKind Kind { get; }

        /// <summary>
        /// Obtiene el valor de este atributo.
        /// </summary>
        RtInfo.ComponentKind IValueAttribute<RtInfo.ComponentKind>.Value => Kind;
    }
}