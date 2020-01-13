/*
InRange.cs

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
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.ViewModel.ValidationRules
{
    /// <inheritdoc />
    /// <summary>
    /// Comprueba que un valor se encuentre dentro de un rango de valores.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DefaultProperty(nameof(Range))]
    public class InRange<T> : ValidationRule where T : IComparable<T>
    {
        /// <summary>
        ///    Rango de valores admitidos.
        /// </summary>
        public Range<T> Range { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///   Si se reemplaza en una clase derivada, realiza comprobaciones de validación en un valor.
        /// </summary>
        /// <param name="value">
        ///   Valor del destino de enlace que se comprobará.
        /// </param>
        /// <param name="cultureInfo">
        ///   Referencia cultural que usará en esta regla.
        /// </param>
        /// <returns>
        ///   Objeto <see cref="ValidationResult" />.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is T v)) return new ValidationResult(false, Strings.InvalidValue);
            return new ValidationResult(Range.IsWithin(v), Strings.ValueMustBeBetween(Range));
        }
    }
}