﻿/*
McartColor2IntConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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
using System.Windows.Data;
using TheXDS.MCART.Types;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Convierte valores desde y hacia objetos de tipo
/// <see cref="Color"/> y <see cref="int"/>.
/// </summary>
public sealed class McartColor2IntConverter : IValueConverter<Color, int>
{
    /// <summary>
    /// Convierte un <see cref="Color"/> en un <see cref="int"/>.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un <see cref="int"/> equivalente al <see cref="Color"/> original.
    /// </returns>
    public int Convert(Color value, object? parameter, CultureInfo? culture)
    {
        return new Abgr32ColorParser().To(value);
    }

    /// <summary>
    /// Convierte un <see cref="int"/> en un 
    /// <see cref="Color"/>.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un <see cref="Color"/> creado a partir del
    /// valor del objeto, o <see langword="null"/> si no es posible
    /// realizar la conversión.
    /// </returns>
    public Color ConvertBack(int value, object? parameter, CultureInfo culture)
    {
        return new Abgr32ColorParser().From(value);
    }
}
