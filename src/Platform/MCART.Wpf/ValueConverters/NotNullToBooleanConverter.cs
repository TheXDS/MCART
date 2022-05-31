﻿/*
NotNullToBooleanConverter.cs

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
using System.Windows;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Devuelve <see langword="true" /> si el elemento a convertir no es <see langword="null" />
/// </summary>
public sealed class NotNullToBooleanConverter : IValueConverter
{
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
    /// <see cref="Visibility.Collapsed" /> si el elemento es
    /// <see langword="null" />, <see cref="Visibility.Visible" /> en caso
    /// contrario.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is not null;
    }

    /// <summary>
    /// Implementa <see cref="IValueConverter.ConvertBack" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados para este <see cref="IValueConverter" />.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <exception cref="InvalidCastException">
    /// Se produce si <paramref name="value" /> no es un <see cref="bool" />.
    /// </exception>
    /// <exception cref="TypeLoadException">
    /// Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
    /// parámetros.
    /// </exception>
    /// <returns>
    /// Una nueva instancia de tipo <paramref name="targetType" /> si <paramref name="value" /> se evalúa como
    /// <see langword="false" />, <see langword="null" /> en caso contrario.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b) return b ? targetType.New() : null;
        throw new InvalidCastException();
    }
}
