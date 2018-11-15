/*
TextNotEmpty.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Windows;
using System.Windows.Controls;
using TheXDS.MCART.Types;
using St = TheXDS.MCART.Resources.Strings;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.ViewModel.ValidationRules
{
    /// <inheritdoc />
    /// <summary>
    ///     Regla que verifica que un valor de texto no se encuentre vacío.
    /// </summary>
    public class TextNotEmpty : ValidationRule
    {
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
        ///   Objeto <see cref="T:System.Windows.Controls.ValidationResult" />.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(!value?.ToString().IsEmpty() ?? false, St.RequiredField);
        }
    }
    /// <inheritdoc />
    /// <summary>
    ///     Regla que verifica que un valor sea numérico.
    /// </summary>
    public class IsNumber : ValidationRule
    {
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
        ///   Objeto <see cref="T:System.Windows.Controls.ValidationResult" />.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(Objects.IsNumericType(value?.GetType()), St.RequiredNumber);
        }
    }

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
        ///   Objeto <see cref="T:System.Windows.Controls.ValidationResult" />.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is T v)) return new ValidationResult(false, St.InvalidValue);
            return new ValidationResult(Range.IsWithin(v), St.ValueMustBeBetween(Range));
        }
    }
    public sealed class ByteInRange : InRange<byte> { }
    public sealed class CharInRange : InRange<char> { }
    public sealed class ShortInRange : InRange<short> { }
    public sealed class IntInRange : InRange<int> { }
    public sealed class LongInRange : InRange<long> { }
    public sealed class FloatInRange : InRange<float> { }
    public sealed class DoubleInRange : InRange<double> { }
    public sealed class DecimalInRange : InRange<decimal> { }
    public sealed class StringInRange : InRange<string> { }
}