/*
FromStringConverter.cs

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

using System.Globalization;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Convierte un valor a su representación como un <see cref="string" />.
/// </summary>
public sealed partial class FromStringConverter
{
    /// <summary>
    /// Intenta una conversión de <see cref="string" /> a un objeto del tipo
    /// de destino especificado.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados a utilizar para la conversión.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Si la conversión desde <see cref="string" /> tuvo éxito, se
    /// devolverá al objeto, se devolverá <see langword="null" /> en caso contrario.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            return targetType?.GetMethod("Parse", [typeof(string)])?.Invoke(null, [value]);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Convierte cualquier objeto en un <see cref="string" />
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    /// <param name="targetType">Tipo del destino.</param>
    /// <param name="parameter">
    /// Parámetros personalizados a utilizar para la conversión.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo" /> a utilizar para la conversión.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> que representa al objeto.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString();
    }
}
