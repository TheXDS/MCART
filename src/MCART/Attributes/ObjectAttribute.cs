/*
ObjectAttribute.cs

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

namespace TheXDS.MCART.Attributes
{
    /* -= NOTA =-
     * Los atributos no soportan clases genéricas, por lo cual es necesario
     * crear una implementación base para cada posible tipo de atributo con
     * base de valor que pueda ser necesaria.
     */

    /// <summary>
    /// Clase base para los atributos de cualquier tipo.
    /// </summary>
    public abstract class ObjectAttribute : Attribute, IValueAttribute<object?>
    {
        /// <summary>
        /// Crea una nueva isntancia de la clase
        /// <see cref="ObjectAttribute" />.
        /// </summary>
        /// <param name="attributeValue">Valor de este atributo.</param>
        protected ObjectAttribute(object? attributeValue)
        {
            Value = attributeValue;
        }

        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        public object? Value { get; }
    }
}