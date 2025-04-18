﻿/*
RangeConverter.cs

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

using System.ComponentModel;

namespace TheXDS.MCART.Types.Converters;

/// <summary>
/// Clase base para los convertidores de valor que permitan obtener
/// objetos de tipo <see cref="Range{T}" /> a partir de una cadena.
/// </summary>
/// <typeparam name="T">
/// Tipo del rango a obtener.
/// </typeparam>
/// <typeparam name="TConverter">
/// Tipo de convertidor a utilizar para cargar los valores de tipo
/// <typeparamref name="T"/> que luego se utilizarán para crear una nueva
/// instancia de la estructura <see cref="Range{T}"/>.
/// </typeparam>
public abstract class RangeConverter<T, TConverter> : BasicParseConverter<Range<T>> where T : IComparable<T> where TConverter : TypeConverter, new()
{
    private static readonly TypeConverter Converter = new TConverter();

    /// <summary>
    /// Ejecuta la conversión de la cadena al tipo de este
    /// <see cref="BasicParseConverter{T}" />.
    /// </summary>
    /// <param name="value">Cadena a convertir.</param>
    /// <returns>
    /// Un valor de tipo <see cref="Range{T}"/> creado a partir de la
    /// cadena especificada.
    /// </returns>
    protected override Range<T> ConvertFrom(string? value)
    {
        return Range<T>.Parse(Converter, value ?? throw new ArgumentNullException(nameof(value)));
    }
}
