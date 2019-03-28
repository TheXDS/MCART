/*
MaximumAttribute.cs

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

#nullable enable

using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Establece un valor máximo al cual se deben limitar los campos y
    ///     propiedades.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaximumAttribute : ObjectAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia del atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.MinimumAttribute" /> 
        ///     estableciendo el valor máximo a representar.
        /// </summary>
        /// <param name="attributeValue">Valor del atributo.</param>
        public MaximumAttribute(object? attributeValue) : base(attributeValue)
        {
        }
    }
}