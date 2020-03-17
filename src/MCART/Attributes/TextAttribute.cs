/*
TextAttribute.cs

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
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <summary>
    /// Agrega un elemento textual genérico a un elemento, además de ser la
    /// clase base para los atributos que describan un valor representable como
    /// <see cref="string" /> para un elemento.
    /// </summary>
    [AttributeUsage(All), Serializable]
    public class TextAttribute : Attribute, IValueAttribute<string?>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TextAttribute" />.
        /// </summary>
        /// <param name="text">Valor de este atributo.</param>
        protected TextAttribute(string? text)
        {
            Value = text;
        }

        /// <summary>
        /// Obtiene el valor asociado a este atributo.
        /// </summary>
        /// <value>El valor de este atributo.</value>
        public string? Value { get; }
    }
}