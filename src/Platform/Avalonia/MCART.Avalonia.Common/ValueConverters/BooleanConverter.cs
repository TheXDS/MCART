/*
BooleanConverter.cs

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
/// Clase base para crear convertidores de valores booleanos.
/// </summary>
/// <typeparam name="T">
/// Tipo de valores a convertir. Deben ser estructuras o enumeraciones.
/// </typeparam>
public class BooleanConverter<T> : IValueConverter where T : struct
{
    /// <summary>
    /// Obtiene o establece el valor que equivale a <see langword="false" /> en este
    /// <see cref="BooleanConverter{T}" />.
    /// </summary>
    public T False { get; set; }

    /// <summary>
    /// Obtiene o establece el valor que equivale a <see langword="true" /> en este
    /// <see cref="BooleanConverter{T}" />.
    /// </summary>
    public T True { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="BooleanConverter{T}" />, configurando los valores que
    /// corresponderán a <see langword="true" /> y <see langword="false" />.
    /// </summary>
    /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
    /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
    public BooleanConverter(T trueValue, T falseValue = default)
    {
        True = trueValue;
        False = falseValue;
    }

    /// <summary>
    /// Convierte un <see cref="bool" /> a un objeto.
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
    /// <see cref="P:System.Windows.Converters.BooleanConverter`1.True" />, si el objeto es de tipo
    /// <see cref="bool" /> y su
    /// valor es <see langword="true" />; en caso contrario, se devuelve
    /// <see cref="P:System.Windows.Converters.BooleanConverter`1.False" />.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is bool b) return b ? True : False;
        return null;
    }

    /// <summary>
    /// Convierte un objeto en un <see cref="bool" />.
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
    /// <see langword="true" /> si el objeto es igual a <see cref="P:System.Windows.Converters.BooleanConverter`1.True" />;
    /// <see langword="false" /> en caso contrario.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value?.Equals(True) ?? false) return true;
        if (value?.Equals(False) ?? false) return false;
        return null;
    }
}
