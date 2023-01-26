/*
BoolFlagConverter.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Clase base para crear convertidores de valores booleanos que analizan
/// banderas de una enumeración.
/// </summary>
/// <typeparam name="T">
/// Tipo de valores a convertir. Deben ser enumeraciones.
/// </typeparam>
public class BoolFlagConverter<T> : IValueConverter
{
    /// <summary>
    /// Obtiene o establece el valor que equivale a <see langword="true" /> en este
    /// <see cref="BoolFlagConverter{T}" />.
    /// </summary>
    public T True { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="BoolFlagConverter{T}" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el tipo especificado al instanciar esta clase no es
    /// una enumeración.
    /// </exception>
    public BoolFlagConverter()
    {
        if (!typeof(T).IsEnum) throw new InvalidOperationException();
        True = (T)typeof(T).Default()!;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="BoolFlagConverter{T}" />, configurando el valor que
    /// corresponderá a <see langword="true" />.
    /// </summary>
    /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
    /// <exception cref="InvalidOperationException">
    /// Se produce si el tipo especificado al instanciar esta clase no es
    /// una enumeración.
    /// </exception>
    public BoolFlagConverter(T trueValue)
    {
        if (!typeof(T).IsEnum) throw new InvalidOperationException();
        True = trueValue;
    }

    /// <summary>
    /// Convierte un valor a <see cref="bool" />.
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
    /// Si no se ha establecido un valor para <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, se
    /// devolverá <see langword="true" /> si hay alguna bandera activa, o <see langword="false" />
    /// en caso contrario. Si se estableció un valor para
    /// <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, se devolverá <see langword="true" /> solo si
    /// dicha(s)
    /// bandera(s) se encuentra(n) activa(s), <see langword="false" /> en caso
    /// contrario.
    /// </returns>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is T v) return (True?.Equals(default) ?? true) ? !v.Equals(True) : v.Equals(True);
        return null;
    }

    /// <summary>
    /// Convierte un <see cref="bool" /> al tipo establecido para este
    /// <see cref="BoolFlagConverter{T}" />.
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
    /// Si <paramref name="value" /> es <see langword="true" />, se devuelve la(s)
    /// bandera(s) a ser detectada(s), en caso de haberse establecido un
    /// valor para <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, o en caso contrario, se devolverá
    /// <c>default</c>.
    /// </returns>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.Equals(true) ?? false ? True : default;
    }
}
