/*
NullBoolConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Clase base para crear convertidores de valores booleanos que pueden ser
/// <see langword="null" />.
/// </summary>
/// <typeparam name="T">Tipo de valores a convertir.</typeparam>
public sealed class NullBoolConverter<T> : IValueConverter
{
    /// <summary>
    /// Obtiene o establece el valor que equivale a <see langword="false" /> en este
    /// <see cref="NullBoolConverter{T}" />.
    /// </summary>
    public T False { get; set; }

    /// <summary>
    /// Obtiene o establece el valor que equivale a <see langword="null" /> en este
    /// <see cref="NullBoolConverter{T}" />.
    /// </summary>
    public T Null { get; set; }

    /// <summary>
    /// Obtiene o establece el valor que equivale a <see langword="true" /> en este
    /// <see cref="NullBoolConverter{T}" />.
    /// </summary>
    public T True { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullBoolConverter{T}" />, configurando los valores que
    /// corresponderán a <see langword="true" /> y <see langword="false" />.
    /// </summary>
    /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
    /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
    public NullBoolConverter(T trueValue, T falseValue = default!)
    {
        True = trueValue;
        Null = False = falseValue!;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullBoolConverter{T}" />, configurando los valores que
    /// corresponderán a <see langword="true" /> y <see langword="false" />.
    /// </summary>
    /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
    /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
    /// <param name="nullValue">Valor equivalente a <see langword="null" />.</param>
    public NullBoolConverter(T trueValue, T falseValue, T nullValue)
    {
        True = trueValue;
        False = falseValue;
        Null = nullValue;
    }

    /// <summary>
    /// Convierte un valor a <see cref="Nullable{T}" />.
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
    /// <see cref="NullBoolConverter{T}.True" /> si <paramref name="value" /> es
    /// <see langword="true" />,
    /// <see cref="NullBoolConverter{T}.False" /> si <paramref name="value" /> es
    /// <see langword="false" />,
    /// <see cref="NullBoolConverter{T}.Null" /> si <paramref name="value" /> es
    /// <see langword="null" />.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            bool b => b ? True : False,
            null => Null,
            _ => default!,
        };
    }

    /// <summary>
    /// Convierte un <see cref="bool" /> al tipo establecido para este
    /// <see cref="BooleanConverter{T}" />.
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
    /// <see langword="true" /> si <paramref name="value" /> es
    /// <see cref="NullBoolConverter{T}.True" />,
    /// <see langword="false" /> si <paramref name="value" /> es
    /// <see cref="NullBoolConverter{T}.False" />,
    /// <see langword="null" /> si <paramref name="value" /> es
    /// <see cref="NullBoolConverter{T}.Null" />.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;
        if (value.Equals(Null) && !ReferenceEquals(Null, False)) return null;
        return ((T)value).Equals(True);
    }
}
