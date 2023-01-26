﻿/*
Inverter.cs

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

using System;
using System.Globalization;
using System.Windows.Data;

namespace TheXDS.MCART.ValueConverters.Base;

/// <summary>
/// Clase base para crear convertidores de valores que inviertan el valor
/// de una propiedad de dependencia.
/// </summary>
/// <typeparam name="T">Tipo de valor a invertir.</typeparam>
public abstract class Inverter<T> : IValueConverter where T : struct
{
    private readonly T _nay;
    private readonly T _yay;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="Inverter{T}" />.
    /// </summary>
    /// <param name="yayValue">Valor invertible.</param>
    /// <param name="nayValue">
    /// Valor inverso de <paramref name="yayValue" />.
    /// </param>
    protected Inverter(T yayValue, T nayValue)
    {
        _yay = yayValue;
        _nay = nayValue;
    }

    /// <summary>
    /// Invierte el valor de  un <typeparamref name="T" />.
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
    /// Un <typeparamref name="T" /> cuyo valor es el inverso de
    /// <paramref name="value" />.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is T v)
            return v.Equals(_yay) ? _nay : _yay;
        return null;
    }

    /// <summary>
    /// Invierte el valor de  un <typeparamref name="T" />.
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
    /// Un <typeparamref name="T" /> cuyo valor es el inverso de
    /// <paramref name="value" />.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is T v)
            return v.Equals(_yay) ? _nay : _yay;
        return null;
    }
}
