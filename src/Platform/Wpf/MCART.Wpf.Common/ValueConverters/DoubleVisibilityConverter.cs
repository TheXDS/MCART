﻿/*
DoubleVisibilityConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Convierte un valor <see cref="double" /> a <see cref="Visibility" />.
/// </summary>
public sealed class DoubleVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
    /// el valor sea mayor a cero.
    /// </summary>
    public Visibility Positives { get; set; }

    /// <summary>
    /// Obtiene o establece el <see cref="Visibility" /> a devolver cuando
    /// el valor sea menor o igual a cero.
    /// </summary>
    public Visibility ZeroOrNegatives { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="DoubleVisibilityConverter" />.
    /// </summary>
    public DoubleVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="DoubleVisibilityConverter" />.
    /// </summary>
    /// <param name="positives">
    /// <see cref="Visibility" /> a utilizar para valores positivos.
    /// </param>
    public DoubleVisibilityConverter(Visibility positives) : this(positives, Visibility.Collapsed)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="DoubleVisibilityConverter" />.
    /// </summary>
    /// <param name="positives">
    /// <see cref="Visibility" /> a utilizar para valores positivos.
    /// </param>
    /// <param name="zeroOrNegatives">
    /// <see cref="Visibility" /> a utilizar para cero y valores negativos.
    /// </param>
    public DoubleVisibilityConverter(Visibility positives, Visibility zeroOrNegatives)
    {
        Positives = positives;
        ZeroOrNegatives = zeroOrNegatives;
    }

    /// <summary>
    /// Obtiene un <see cref="Visibility" /> a partir del valor.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// <see cref="P:System.Windows.Converters.DoubleVisibilityConverter.Positives" /> si <paramref name="value" /> es
    /// mayor a
    /// cero, <see cref="P:System.Windows.Converters.DoubleVisibilityConverter.ZeroOrNegatives" /> en caso contrario.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double v) return v > 0.0 ? Positives : ZeroOrNegatives;
        throw new InvalidCastException();
    }

    /// <summary>
    /// Infiere un valor basado en el <see cref="Visibility" /> provisto.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// <c>1.0</c> si <paramref name="value" /> es igual a
    /// <see cref="P:System.Windows.Converters.DoubleVisibilityConverter.Positives" />, <c>0.0</c> en caso contrario.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility v) return v == Positives ? 1.0 : 0.0;
        throw new InvalidCastException();
    }
}
