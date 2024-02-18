/*
ThresholdConverter.cs

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

using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Clase base para un convertidor de valores que digitalice un valor
/// arbitrario de entrada.
/// </summary>
/// <typeparam name="TIn">
/// Tipo de valores de entrada del <see cref="IValueConverter" />.
/// </typeparam>
/// <typeparam name="TOut">
/// Tipo de valores de salida del <see cref="IValueConverter" />.
/// </typeparam>
public class ThresholdConverter<TIn, TOut> : IValueConverter where TIn : IComparable<TIn> where TOut : struct
{
    /// <summary>
    /// Valor a devolver cuando el valor de entrada sea mayor al umbral.
    /// </summary>
    public TOut AboveValue { get; }

    /// <summary>
    /// Valor a devolver cuando el valor de entrada sea igual al umbral.
    /// </summary>
    public TOut? AtValue { get; }

    /// <summary>
    /// Valor a devolver cuando el valor de entrada sea menor al umbral.
    /// </summary>
    public TOut BelowValue { get; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ThresholdConverter{TIn, TOut}" />
    /// </summary>
    /// <param name="belowValue">
    /// Valor a devolver cuando el valor de entrada sea menor o igual al
    /// umbral.
    /// </param>
    /// <param name="aboveValue">
    /// Valor a devolver cuando el valor de entrada sea mayor al umbral.
    /// </param>
    public ThresholdConverter(TOut belowValue, TOut aboveValue) : this(belowValue, aboveValue, null)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ThresholdConverter{TIn,TOut}" />
    /// </summary>
    /// <param name="belowValue">
    /// Valor a devolver cuando el valor de entrada sea menor al umbral.
    /// </param>
    /// <param name="aboveValue">
    /// Valor a devolver cuando el valor de entrada sea mayor al umbral.
    /// </param>
    /// <param name="atValue">
    /// Valor a devolver cuando el valor de entrada sea igual al umbral.
    /// </param>
    public ThresholdConverter(TOut belowValue, TOut aboveValue, TOut? atValue)
    {
        BelowValue = belowValue;
        AboveValue = aboveValue;
        AtValue = atValue;
    }

    /// <summary>
    /// Realiza una comparación del valor de entrada contra un valor especificado como el umbral.
    /// </summary>
    /// <param name="value">Valor actual a comparar.</param>
    /// <param name="targetType">
    /// Tipo de campo objetivo. debe ser de tipo <typeparamref name="TOut" />.
    /// </param>
    /// <param name="parameter">
    /// Valor de umbral. debe ser del mismo tipo que <paramref name="value" />.
    /// </param>
    /// <param name="culture">
    /// Este parámetro es ignorado.
    /// Referencia cultural que se va a usar en el convertidor.
    /// </param>
    /// <returns></returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            TIn v => v.CompareTo(GetCurrentValue(parameter, culture)) switch
            {
                -1 => BelowValue,
                0 => AtValue ?? BelowValue,
                1 => AboveValue,
                _ => throw new InvalidReturnValueException(nameof(IComparable<TIn>.CompareTo)),
            },
            null => throw new ArgumentNullException(nameof(value)),
            _ => throw Errors.InvalidValue(nameof(value), value)
        };
    }

    private static TIn GetCurrentValue(object? parameter, CultureInfo culture)
    {
        return parameter switch
        {
            TIn tin => tin,
            string str => ConvertFromString(str, culture),
            null => throw new ArgumentNullException(nameof(parameter)),
            _ => throw Errors.InvalidValue(nameof(parameter), parameter),
        };
    }

    private static TIn ConvertFromString(string str, CultureInfo culture)
    {
        TypeConverter? typeConverter = GetConverter() ?? throw new InvalidCastException();
        try
        {
            return (TIn)typeConverter.ConvertTo(null, culture, str, typeof(TIn))!;
        }
        catch (Exception ex)
        {
            throw Errors.InvalidValue("parameter", str, ex);
        }
    }

    private static TypeConverter? GetConverter()
    {
        if (typeof(string).HasAttribute(out TypeConverterAttribute? tc))
        {
            IEnumerable<Type>? converters = ReflectionHelpers.PublicTypes<TypeConverter>().Where(TypeExtensions.IsInstantiable);
            return converters.FirstOrDefault(p => p.AssemblyQualifiedName == tc.ConverterTypeName)?.New<TypeConverter>();
        }
        else
        {
            return Common.FindConverter<string, TIn>();
        }
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
    /// <returns>
    /// Este método siempre genera un <see cref="InvalidOperationException" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
