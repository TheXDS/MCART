/*
ThresholdConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters
{
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TIn currentValue;
            switch (parameter)
            {
                case TIn tin:
                    currentValue = tin;
                    break;
                case string str:
                    TypeConverter? typeConverter;

                    if (parameter.GetType().HasAttr(out TypeConverterAttribute? tc))
                    {
                        var converters = Objects.PublicTypes<TypeConverter>().Where(TypeExtensions.IsInstantiable);
                        typeConverter = converters.FirstOrDefault(p => p.AssemblyQualifiedName == tc!.ConverterTypeName)?.New<TypeConverter>();
                    }
                    else { typeConverter = Common.FindConverter<string, TIn>(); }

                    if (typeConverter is null) throw new InvalidCastException();

                    try
                    {
                        currentValue = (TIn) typeConverter.ConvertTo(str, typeof(TIn));
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(string.Empty, nameof(parameter), ex);
                    }

                    break;
                case null:
                    throw new ArgumentNullException(nameof(parameter));
                default:
                    throw new ArgumentException(string.Empty, nameof(parameter));
            }

            return value switch
            {
                TIn v => v.CompareTo(currentValue) switch
                {
                    -1 => BelowValue,
                    0 => AtValue ?? BelowValue,
                    1 => AboveValue,
                    _ => throw new InvalidReturnValueException(nameof(IComparable<TIn>.CompareTo)),
                },
                null => throw new ArgumentNullException(nameof(value)),

                _ => throw new ArgumentException(null, nameof(value)),
            };
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
        /// Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        /// Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        /// parámetros.
        /// </exception>
        /// <returns>
        /// Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}