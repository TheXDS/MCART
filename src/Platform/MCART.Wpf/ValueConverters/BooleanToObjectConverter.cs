/*
BooleanToObjectConverter.cs

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
using System.Globalization;
using System.Windows.Data;
using System;

/// <summary>
/// Obtiene un objeto solamente si el valor original a convertir es 
/// igual a <see langword="true"/>.
/// </summary>
public sealed class BooleanToObjectConverter : IValueConverter
{
    /// <summary>
    /// Devuelve un objeto de acuerdo a un valor booleano.
    /// </summary>
    /// <param name="value">
    /// Valor booleano a comprobar.
    /// </param>
    /// <param name="targetType">
    /// Tipo objetivo de la conversión.
    /// </param>
    /// <param name="parameter">
    /// Objeto a devolver si <c><paramref name="value"/> == <see langword="true"/></c>.
    /// </param>
    /// <param name="culture">
    /// Cultura a utilizar durante la conversión.
    /// </param>
    /// <returns>
    /// <paramref name="parameter"/> si <c><paramref name="value"/> == <see langword="true"/></c>,
    /// <see langword="null"/> en caso contrario.
    /// </returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value is bool b && b && targetType.IsInstanceOfType(parameter)) ? parameter : null;
    }

    /// <summary>
    /// Devuelve <see langword="true"/> si un objeto no es 
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="value">
    /// Objeto a comprobar.
    /// </param>
    /// <param name="targetType">
    /// Tipo objetivo.
    /// </param>
    /// <param name="parameter">
    /// Parámetro de conversión.
    /// </param>
    /// <param name="culture">
    /// Cultura a utilizar durante la conversión.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si el objeto no es
    /// <see langword="null"/>, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is not null;
    }
}
