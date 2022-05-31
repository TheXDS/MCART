/*
HeatBrushConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.ValueConverters;
using System;
using System.Globalization;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ValueConverters.Base;
using static TheXDS.MCART.Math.Common;

/// <summary>
/// Obtiene un <see cref="System.Windows.Media.Brush" /> correspondiente a la salud expresada
/// com porcentaje.
/// </summary>
public sealed class HeatBrushConverter : FloatConverterBase, IValueConverter
{
    /// <summary>Convierte un valor.</summary>
    /// <param name="value">
    ///   Valor generado por el origen de enlace.
    /// </param>
    /// <param name="targetType">
    ///   El tipo de la propiedad del destino de enlace.
    /// </param>
    /// <param name="parameter">
    ///   Parámetro de convertidor que se va a usar.
    /// </param>
    /// <param name="culture">
    ///   Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns>
    ///   Valor convertido.
    ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Types.Color.BlendHeat(GetFloat(value).Clamp(0f, 1f)).Brush();
    }

    /// <summary>Convierte un valor.</summary>
    /// <param name="value">
    ///   Valor generado por el destino de enlace.
    /// </param>
    /// <param name="targetType">Tipo al que se va a convertir.</param>
    /// <param name="parameter">
    ///   Parámetro de convertidor que se va a usar.
    /// </param>
    /// <param name="culture">
    ///   Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns>
    ///   Valor convertido.
    ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
