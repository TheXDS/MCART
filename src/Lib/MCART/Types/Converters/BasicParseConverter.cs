/*
BasicParseConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<TValue>, la cual permite representar rangos
de valores.

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

using System.ComponentModel;
using System.Globalization;

namespace TheXDS.MCART.Types.Converters;

/// <summary>
/// Clase base para un
/// <see cref="TypeConverter" /> básico que
/// permita transformar un valor de <see cref="string" /> en
/// un <typeparamref name="T" />.
/// </summary>
/// <typeparam name="T">
/// Tipo de elemento de salida de este
/// <see cref="TypeConverter" />.
/// </typeparam>
public abstract class BasicParseConverter<T> : TypeConverter
{
    /// <summary>
    ///   Devuelve si este convertidor puede convertir un objeto del tipo especificado al tipo de este convertidor, mediante el contexto especificado.
    /// </summary>
    /// <param name="context">
    ///   <see cref="ITypeDescriptorContext" /> que ofrece un contexto de formato.
    /// </param>
    /// <param name="sourceType">
    ///   Un <see cref="Type" /> que representa el tipo que desea convertir.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> si este convertidor puede realizar la conversión; en caso contrario, <see langword="false" />.
    /// </returns>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    /// <summary>
    ///   Devuelve si este convertidor puede convertir el objeto al tipo especificado, con el contexto especificado.
    /// </summary>
    /// <param name="context">
    ///   Interfaz <see cref="ITypeDescriptorContext" /> que ofrece un contexto de formato.
    /// </param>
    /// <param name="destinationType">
    ///   <see cref="Type" /> que representa el tipo al que se quiere convertir.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> si este convertidor puede realizar la conversión; en caso contrario, <see langword="false" />.
    /// </returns>
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(T);
    }

    /// <summary>
    ///   Convierte el objeto determinado al tipo de este convertidor usando el contexto especificado y la información de referencia cultural.
    /// </summary>
    /// <param name="context">
    ///   <see cref="ITypeDescriptorContext" /> que ofrece un contexto de formato.
    /// </param>
    /// <param name="culture">
    ///   <see cref="CultureInfo" /> que se va a usar como referencia cultural actual.
    /// </param>
    /// <param name="value">
    ///   <see cref="object" /> que se va a convertir.
    /// </param>
    /// <returns>
    ///   Un <see cref="object" /> que representa el valor convertido.
    /// </returns>
    /// <exception cref="NotSupportedException">
    ///   No se puede realizar la conversión.
    /// </exception>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return ConvertFrom(value?.ToString());
    }

    /// <summary>
    /// Ejecuta la conversión de la cadena al tipo de este <see cref="BasicParseConverter{T}"/>.
    /// </summary>
    /// <param name="value">Cadena a convertir.</param>
    /// <returns>
    /// Un valor de tipo <typeparamref name="T"/> creado a partir de la cadena especificada.
    /// </returns>
    protected abstract T ConvertFrom(string? value);

    /// <summary>
    ///   Convierte el objeto de valor determinado al tipo especificado usando el contexto y la información de referencia cultural especificados.
    /// </summary>
    /// <param name="context">
    ///   <see cref="ITypeDescriptorContext" /> que proporciona un contexto de formato.
    /// </param>
    /// <param name="culture">
    ///   Objeto <see cref="CultureInfo" />.
    ///    Si se pasa <see langword="null" />, se supone que se va a usar la referencia cultural actual.
    /// </param>
    /// <param name="value">
    ///   <see cref="object" /> que se va a convertir.
    /// </param>
    /// <param name="destinationType">
    ///   El <see cref="Type" /> para convertir el <paramref name="value" /> parámetro.
    /// </param>
    /// <returns>
    ///   Un <see cref="object" /> que representa el valor convertido.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   El parámetro <paramref name="destinationType" /> es <see langword="null" />.
    /// </exception>
    /// <exception cref="NotSupportedException">
    ///   No se puede realizar la conversión.
    /// </exception>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is T v) return ConvertTo(v);
        return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// Ejecuta la conversión de la cadena al tipo de este <see cref="BasicParseConverter{T}"/>.
    /// </summary>
    /// <param name="value">Cadena a convertir.</param>
    /// <returns>
    /// La representación como cadena del objeto especificado.
    /// </returns>
    protected virtual string? ConvertTo(T value)
    {
        return value?.ToString();
    }
}
