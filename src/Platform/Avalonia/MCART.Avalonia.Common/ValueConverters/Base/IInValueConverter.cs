﻿/*
IInValueConverter.cs

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

using Avalonia.Data.Converters;
using System.Globalization;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters.Base;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que permita
/// convertir valores de un tipo específico de entrada.
/// </summary>
/// <typeparam name="TIn">Tipo de entrada de la conversión.</typeparam>
public interface IInValueConverter<TIn> : IValueConverter
{
    /// <summary>
    /// Obtiene una referencia al tipo de objeto de entrada que puede ser
    /// convertido.
    /// </summary>
    Type SourceType => typeof(TIn);

    /// <summary>
    /// Obtiene una referencia al tipo de salida producido por este 
    /// convertidor.
    /// </summary>
    Type TargetType { get; }

    /// <inheritdoc/>
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        return Convert(value is TIn i ? i : (TIn)typeof(TIn).Default()!, targetType, parameter, culture);
    }

    /// <summary>
    /// Convierte un valor.
    /// </summary>
    /// <param name="value">
    /// Valor producido por el origen del enlace.
    /// </param>
    /// <param name="targetType">El tipo objetivo de la propiedad enlazada.</param>
    /// <param name="parameter">
    /// Parámetro pasado al convertidor a utilizar.
    /// </param>
    /// <param name="culture">
    /// La cultura a utilizar en el convertidor.
    /// </param>
    /// <returns></returns>
    object? Convert(TIn value, Type targetType, object? parameter, CultureInfo? culture);
}
