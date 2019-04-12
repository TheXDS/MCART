/*
TypeAttribute.cs

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

#nullable enable

using System;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Agrega un elemento de tipo a un elemento, además de ser la
    ///     clase base para los atributos que describan un valor representable como
    ///     <see cref="Type" /> para un elemento.
    /// </summary>
    [AttributeUsage(All)]
    [Serializable]
    public class TypeAttribute : Attribute, IValueAttribute<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="TypeAttribute" />.
        /// </summary>
        /// <param name="type">Valor de este atributo.</param>
        protected TypeAttribute(Type type)
        {
            Value = type;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public Type Value { get; }
    }
}