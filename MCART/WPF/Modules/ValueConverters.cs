/*
ValueConverters.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using TheXDS.MCART;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Math.Common;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;

#region Configuración de ReSharper

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable once CheckNamespace

#endregion

namespace System.Windows.Converters
{
    #region Clases base

    /// <summary>
    ///     Clase base que incluye un método para obtener un <see cref="float"/>.
    /// </summary>
    public abstract class FloatConverterBase
    {
        /// <summary>
        ///     Convierte un valor a un <see cref="float"/>.
        /// </summary>
        /// <param name="value">
        ///     Valor desde el cual obtener un <see cref="float"/>.
        /// </param>
        /// <returns>
        ///     Un <see cref="float"/> obtenido a partir del valor brindado.
        /// </returns>
        protected static float GetFloat(object value)
        {
            var v = 0f;
            switch (value)
            {
                case float f:
                    v = f;
                    break;
                case double d:
                    v = (float)d;
                    break;
                case byte b:
                    v = (float)b / 100;
                    break;
                case short s:
                    v = (float)s / 100;
                    break;
                case int i:
                    v = (float)i / 100;
                    break;
                case long l:
                    v = (float)l / 100;
                    break;
                case string str:
                    float.TryParse(str, out v);
                    break;
#if !CLSCompliance
                case sbyte sb:
                    v = (float)sb / 100;
                    break;
                case ushort us:
                    v = (float)us / 100;
                    break;
                case uint ui:
                    v = (float)ui / 100;
                    break;
                case ulong ul:
                    v = (float)ul / 100;
                    break;
#endif
            }
            return v;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para crear convertidores de valores que inviertan el valor
    ///     de una propiedad de dependencia.
    /// </summary>
    /// <typeparam name="T">Tipo de valor a invertir.</typeparam>
    public abstract class Inverter<T> : IValueConverter where T : struct
    {
        private readonly T _nay;
        private readonly T _yay;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="Inverter{T}" />.
        /// </summary>
        /// <param name="yayValue">Valor invertible.</param>
        /// <param name="nayValue">
        ///     Valor inverso de <paramref name="yayValue" />.
        /// </param>
        protected Inverter(T yayValue, T nayValue)
        {
            _yay = yayValue;
            _nay = nayValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Invierte el valor de  un <typeparamref name="T" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <typeparamref name="T" /> cuyo valor es el inverso de
        ///     <paramref name="value" />.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T v)
                return v.Equals(_yay) ? _nay : _yay;
            throw new InvalidCastException();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Invierte el valor de  un <typeparamref name="T" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <typeparamref name="T" /> cuyo valor es el inverso de
        ///     <paramref name="value" />.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T v)
                return v.Equals(_yay) ? _nay : _yay;
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para convertidores de valores que tomen un valor <see cref="T:System.Windows.Size" /> para determinar un
    ///     valor de tipo <see cref="T:System.Windows.Visibility" /> basado en un umbral.
    /// </summary>
    public abstract class SizeVisibilityConverter : IValueConverter
    {
        private readonly Visibility _above;
        private readonly Visibility _below;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="SizeVisibilityConverter" />.
        /// </summary>
        /// <param name="below">Valor a devolver por debajo del umbral.</param>
        /// <param name="above">Valor a devolver por encima del umbral.</param>
        protected SizeVisibilityConverter(Visibility below, Visibility above)
        {
            _below = below;
            _above = above;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Convierte un <see cref="Size" /> a un <see cref="Visibility" /> basado en un valor de umbral.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     El valor de <see cref="Visibility" /> calculado a partir del tamaño especificado.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(Visibility))) throw new InvalidCastException();
            Size size;
            switch (parameter)
            {
                case string str:
                    try
                    {
                        size = Size.Parse(str);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException(string.Empty, nameof(parameter), e);
                    }

                    break;
                case Size sz:
                    size = sz;
                    break;
                case double d:
                    size = new Size(d, d);
                    break;
                case int d:
                    size = new Size(d, d);
                    break;
                default:
                    throw new ArgumentException(string.Empty, nameof(parameter));
            }

            switch (value)
            {
                case FrameworkElement f:
                    return f.ActualWidth < size.Width && f.ActualHeight < size.Height ? _below : _above;
                case Size sz:
                    return sz.Width < size.Width && sz.Height < size.Height ? _below : _above;
            }

            return _above;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Infiere un valor basado en el <see cref="T:System.Windows.Visibility" /> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un valor de <see cref="Size" /> inferido a partir del valor de entrada.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility v && v == _above ? parameter : new Size();
        }
    }

    #endregion

    #region Converters bindeables en código

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para crear convertidores de valores booleanos.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de valores a convertir. Deben ser estructuras o enumeraciones.
    /// </typeparam>
    public class BooleanConverter<T> : IValueConverter where T : struct
    {
        /// <summary>
        ///     Obtiene o establece el valor que equivale a <see langword="false" /> en este
        ///     <see cref="BooleanConverter{T}" />.
        /// </summary>
        public T False { get; set; }

        /// <summary>
        ///     Obtiene o establece el valor que equivale a <see langword="true" /> en este
        ///     <see cref="BooleanConverter{T}" />.
        /// </summary>
        public T True { get; set; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="BooleanConverter{T}" />, configurando los valores que
        ///     corresponderán a <see langword="true" /> y <see langword="false" />.
        /// </summary>
        /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
        /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
        public BooleanConverter(T trueValue, T falseValue = default)
        {
            True = trueValue;
            False = falseValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Convierte un <see cref="T:System.Boolean" /> a un objeto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="P:System.Windows.Converters.BooleanConverter`1.True" />, si el objeto es de tipo
        ///     <see cref="T:System.Boolean" /> y su
        ///     valor es <see langword="true" />; en caso contrario, se devuelve
        ///     <see cref="P:System.Windows.Converters.BooleanConverter`1.False" />.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? True : False;
            return null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Convierte un objeto en un <see cref="T:System.Boolean" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el objeto es igual a <see cref="P:System.Windows.Converters.BooleanConverter`1.True" />;
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T variable && variable.Equals(True);
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para crear convertidores de valores booleanos que analizan
    ///     banderas de una enumeración.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de valores a convertir. Deben ser enumeraciones.
    /// </typeparam>
    public class BoolFlagConverter<T> : IValueConverter
    {
        /// <summary>
        ///     Obtiene o establece el valor que equivale a <see langword="true" /> en este
        ///     <see cref="BoolFlagConverter{T}" />.
        /// </summary>
        public T True { get; set; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="BoolFlagConverter{T}" />.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Se produce si el tipo especificado al instanciar esta clase no es
        ///     una enumeración.
        /// </exception>
        public BoolFlagConverter()
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = default;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="BoolFlagConverter{T}" />, configurando el valor que
        ///     corresponderá a <see langword="true" />.
        /// </summary>
        /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
        /// <exception cref="InvalidOperationException">
        ///     Se produce si el tipo especificado al instanciar esta clase no es
        ///     una enumeración.
        /// </exception>
        public BoolFlagConverter(T trueValue)
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = trueValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Convierte un valor a <see cref="T:System.Boolean" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Si no se ha establecido un valor para <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, se
        ///     devolverá <see langword="true" /> si hay alguna bandera activa, o <see langword="false" />
        ///     en caso contrario. Si se estableció un valor para
        ///     <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, se devolverá <see langword="true" /> solo si
        ///     dicha(s)
        ///     bandera(s) se encuentra(n) activa(s), <see langword="false" /> en caso
        ///     contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is T v) return True.Equals(default) ? !v.Equals(True) : v.Equals(True);
            return null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Convierte un <see cref="T:System.Boolean" /> al tipo establecido para este
        ///     <see cref="T:System.Windows.Converters.BoolFlagConverter`1" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Si <paramref name="value" /> es <see langword="true" />, se devuelve la(s)
        ///     bandera(s) a ser detectada(s), en caso de haberse establecido un
        ///     valor para <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True" />, o en caso contrario, se devolverá
        ///     <c>default</c>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(true) ?? false ? True : default;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para un convertidor de valores que digitalice un valor
    ///     arbitrario de entrada.
    /// </summary>
    /// <typeparam name="TIn">
    ///     Tipo de valores de entrada del <see cref="T:System.Windows.Data.IValueConverter" />.
    /// </typeparam>
    /// <typeparam name="TOut">
    ///     Tipo de valores de salida del <see cref="T:System.Windows.Data.IValueConverter" />.
    /// </typeparam>
    public class ThresholdConverter<TIn, TOut> : IValueConverter where TIn : IComparable<TIn> where TOut : struct
    {
        /// <summary>
        ///     Valor a devolver cuando el valor de entrada sea mayor al umbral.
        /// </summary>
        public TOut AboveValue { get; }

        /// <summary>
        ///     Valor a devolver cuando el valor de entrada sea igual al umbral.
        /// </summary>
        public TOut? AtValue { get; }

        /// <summary>
        ///     Valor a devolver cuando el valor de entrada sea menor al umbral.
        /// </summary>
        public TOut BelowValue { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:System.Windows.Converters.ThresholdConverter`2" />
        /// </summary>
        /// <param name="belowValue">
        ///     Valor a devolver cuando el valor de entrada sea menor o igual al
        ///     umbral.
        /// </param>
        /// <param name="aboveValue">
        ///     Valor a devolver cuando el valor de entrada sea mayor al umbral.
        /// </param>
        public ThresholdConverter(TOut belowValue, TOut aboveValue) : this(belowValue, aboveValue, null)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="ThresholdConverter{TIn,TOut}" />
        /// </summary>
        /// <param name="belowValue">
        ///     Valor a devolver cuando el valor de entrada sea menor al umbral.
        /// </param>
        /// <param name="aboveValue">
        ///     Valor a devolver cuando el valor de entrada sea mayor al umbral.
        /// </param>
        /// <param name="atValue">
        ///     Valor a devolver cuando el valor de entrada sea igual al umbral.
        /// </param>
        public ThresholdConverter(TOut belowValue, TOut aboveValue, TOut? atValue)
        {
            BelowValue = belowValue;
            AboveValue = aboveValue;
            AtValue = atValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Realiza una comparación del valor de entrada contra un valor especificado como el umbral.
        /// </summary>
        /// <param name="value">Valor actual a comparar.</param>
        /// <param name="targetType">
        ///     Tipo de campo objetivo. debe ser de tipo <typeparamref name="TOut" />.
        /// </param>
        /// <param name="parameter">
        ///     Valor de umbral. debe ser del mismo tipo que <paramref name="value" />.
        /// </param>
        /// <param name="culture">
        ///     Este parámetro es ignorado.
        ///     Referencia cultural que se va a usar en el convertidor.
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
                    TypeConverter typeConverter;

                    if (parameter.GetType().HasAttr(out TypeConverterAttribute tc))
                    {
                        var converters = Objects.PublicTypes<TypeConverter>().Where(TypeExtensions.IsInstantiable);
                        typeConverter = converters.FirstOrDefault(p => p.AssemblyQualifiedName == tc.ConverterTypeName)
                            .New<TypeConverter>();
                    }
                    else { typeConverter = Common.FindConverter<string, TIn>(); }

                    if (typeConverter is null) throw new ArgumentException(string.Empty, nameof(parameter));

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

            switch (value)
            {
                case TIn v:
                    switch (v.CompareTo(currentValue))
                    {
                        case -1:
                            return BelowValue;
                        case 0:
                            return AtValue ?? BelowValue;
                        case 1:
                            return AboveValue;
                        default: throw new InvalidReturnValueException(nameof(IComparable<TIn>.CompareTo));
                    }
                case null:
                    throw new ArgumentNullException(nameof(value));
            }

            throw new ArgumentException(nameof(value));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    #endregion

    /// <inheritdoc cref="FloatConverterBase"/>
    /// <summary>
    ///     Obtiene un <see cref="T:System.Windows.Media.Brush" /> correspondiente a la salud expresada
    ///     com porcentaje.
    /// </summary>
    public sealed class HealthBrushConverter : FloatConverterBase, IValueConverter
    {
        /// <inheritdoc />
        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        ///   Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        ///   El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///   Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///   Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///   Valor convertido.
        ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(TheXDS.MCART.Types.Color.BlendHealth(GetFloat(value).Clamp(0f, 1f)));
        }

        /// <inheritdoc />
        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        ///   Valor generado por el destino de enlace.
        /// </param>
        /// <param name="targetType">Tipo al que se va a convertir.</param>
        /// <param name="parameter">
        ///   Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///   Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///   Valor convertido.
        ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc cref="FloatConverterBase"/>
    /// <summary>
    ///     Obtiene un <see cref="T:System.Windows.Media.Brush" /> correspondiente a la salud expresada
    ///     com porcentaje.
    /// </summary>
    public sealed class HeatBrushConverter : FloatConverterBase, IValueConverter
    {
        /// <inheritdoc />
        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        ///   Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        ///   El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///   Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///   Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///   Valor convertido.
        ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(TheXDS.MCART.Types.Color.BlendHeat(GetFloat(value).Clamp(0f, 1f)));
        }

        /// <inheritdoc />
        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        ///   Valor generado por el destino de enlace.
        /// </param>
        /// <param name="targetType">Tipo al que se va a convertir.</param>
        /// <param name="parameter">
        ///   Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///   Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///   Valor convertido.
        ///    Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un valor <see cref="T:System.Int32" />  a <see cref="T:System.Windows.Visibility" />
    /// </summary>
    public sealed class CountVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver en caso
        ///     de que la cuenta de elementos sea mayor a cero.
        /// </summary>
        public Visibility WithItems { get; set; }

        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver en caso
        ///     de que la cuenta de elementos sea igual a cero.
        /// </summary>
        public Visibility WithoutItems { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.CountVisibilityConverter" />.
        /// </summary>
        public CountVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.CountVisibilityConverter" />.
        /// </summary>
        /// <param name="withItems">
        ///     <see cref="T:System.Windows.Visibility" /> a utilizar en caso de que la cuenta sea
        ///     mayor a cero.
        /// </param>
        public CountVisibilityConverter(Visibility withItems = Visibility.Visible) : this(withItems,
            Visibility.Collapsed)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="CountVisibilityConverter" />.
        /// </summary>
        /// <param name="withItems">
        ///     <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
        ///     mayor a cero.
        /// </param>
        /// <param name="withoutItems">
        ///     <see cref="Visibility" /> a utilizar en caso de que la cuenta sea
        ///     igual a cero.
        /// </param>
        public CountVisibilityConverter(Visibility withItems, Visibility withoutItems)
        {
            WithItems = withItems;
            WithoutItems = withoutItems;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir de un valor de cuenta.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="P:System.Windows.Converters.CountVisibilityConverter.WithItems" /> si <paramref name="value" /> es mayor
        ///     a
        ///     cero, <see cref="P:System.Windows.Converters.CountVisibilityConverter.WithoutItems" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int v) return v > 0 ? WithItems : WithoutItems;
            throw new InvalidCastException();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Infiere una cuenta de elementos basado en el <see cref="T:System.Windows.Visibility" /> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <c>1</c> si <paramref name="value" /> es igual a
        ///     <see cref="P:System.Windows.Converters.CountVisibilityConverter.WithItems" />, <c>0</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return v == WithItems ? 1 : 0;
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un valor <see cref="T:System.Double" /> a <see cref="T:System.Windows.Visibility" />.
    /// </summary>
    public sealed class DoubleVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        ///     el valor sea mayor a cero.
        /// </summary>
        public Visibility Positives { get; set; }

        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        ///     el valor sea menor o igual a cero.
        /// </summary>
        public Visibility ZeroOrNegatives { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.DoubleVisibilityConverter" />.
        /// </summary>
        public DoubleVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.DoubleVisibilityConverter" />.
        /// </summary>
        /// <param name="positives">
        ///     <see cref="T:System.Windows.Visibility" /> a utilizar para valores positivos.
        /// </param>
        public DoubleVisibilityConverter(Visibility positives) : this(positives, Visibility.Collapsed)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="DoubleVisibilityConverter" />.
        /// </summary>
        /// <param name="positives">
        ///     <see cref="Visibility" /> a utilizar para valores positivos.
        /// </param>
        /// <param name="zeroOrNegatives">
        ///     <see cref="Visibility" /> a utilizar para cero y valores negativos.
        /// </param>
        public DoubleVisibilityConverter(Visibility positives, Visibility zeroOrNegatives)
        {
            Positives = positives;
            ZeroOrNegatives = zeroOrNegatives;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="P:System.Windows.Converters.DoubleVisibilityConverter.Positives" /> si <paramref name="value" /> es
        ///     mayor a
        ///     cero, <see cref="P:System.Windows.Converters.DoubleVisibilityConverter.ZeroOrNegatives" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double v) return v > 0.0 ? Positives : ZeroOrNegatives;
            throw new InvalidCastException();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Infiere un valor basado en el <see cref="T:System.Windows.Visibility" /> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <c>1.0</c> si <paramref name="value" /> es igual a
        ///     <see cref="P:System.Windows.Converters.DoubleVisibilityConverter.Positives" />, <c>0.0</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return v == Positives ? 1.0 : 0.0;
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un valor <see cref="T:System.Single" /> a <see cref="T:System.Windows.Visibility" />.
    /// </summary>
    public sealed class FloatVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        ///     el valor sea mayor a cero.
        /// </summary>
        public Visibility Positives { get; set; }

        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        ///     el valor sea menor o igual a cero.
        /// </summary>
        public Visibility ZeroOrNegatives { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.FloatVisibilityConverter" />.
        /// </summary>
        public FloatVisibilityConverter() : this(Visibility.Visible, Visibility.Collapsed)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.FloatVisibilityConverter" />.
        /// </summary>
        /// <param name="positives">
        ///     <see cref="T:System.Windows.Visibility" /> a utilizar para valores positivos.
        /// </param>
        public FloatVisibilityConverter(Visibility positives) : this(positives, Visibility.Visible)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="FloatVisibilityConverter" />.
        /// </summary>
        /// <param name="positives">
        ///     <see cref="Visibility" /> a utilizar para valores positivos.
        /// </param>
        /// <param name="zeroOrNegatives">
        ///     <see cref="Visibility" /> a utilizar para cero y valores negativos.
        /// </param>
        public FloatVisibilityConverter(Visibility positives, Visibility zeroOrNegatives)
        {
            Positives = positives;
            ZeroOrNegatives = zeroOrNegatives;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="P:System.Windows.Converters.FloatVisibilityConverter.Positives" /> si <paramref name="value" /> es mayor
        ///     a
        ///     cero, <see cref="P:System.Windows.Converters.FloatVisibilityConverter.ZeroOrNegatives" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float v) return v > 0.0f ? Positives : ZeroOrNegatives;
            throw new InvalidCastException();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Infiere un valor basado en el <see cref="T:System.Windows.Visibility" /> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <c>1.0f</c> si <paramref name="value" /> es igual a
        ///     <see cref="P:System.Windows.Converters.FloatVisibilityConverter.Positives" />, <c>0.0f</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return v == Positives ? 1.0f : 0.0f;
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un <see cref="T:System.String" /> a un <see cref="T:System.Windows.Visibility" />.
    /// </summary>
    public sealed class StringVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        ///     el <see cref="string" /> esté vacío.
        /// </summary>
        public Visibility Empty { get; set; }

        /// <summary>
        ///     Obtiene o establece el <see cref="Visibility" /> a devolver cuando
        ///     el <see cref="string" /> no esté vacío.
        /// </summary>
        public Visibility NotEmpty { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.StringVisibilityConverter" />.
        /// </summary>
        /// <param name="empty">
        ///     <see cref="T:System.Windows.Visibility" /> a devolver cuando la cadena esté vacía.
        /// </param>
        public StringVisibilityConverter(Visibility empty) : this(empty, Visibility.Visible)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="StringVisibilityConverter" />.
        /// </summary>
        /// <param name="empty">
        ///     <see cref="Visibility" /> a devolver cuando la cadena esté vacía.
        /// </param>
        /// <param name="notEmpty">
        ///     <see cref="Visibility" /> a devolver cuando la cadena no esté vacía.
        /// </param>
        public StringVisibilityConverter(Visibility empty, Visibility notEmpty)
        {
            NotEmpty = notEmpty;
            Empty = empty;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="StringVisibilityConverter" />.
        /// </summary>
        /// <param name="inverted">
        ///     Inversión de valores de <see cref="Visibility" />.
        /// </param>
        public StringVisibilityConverter(bool inverted)
        {
            if (inverted)
            {
                NotEmpty = Visibility.Visible;
                Empty = Visibility.Collapsed;
            }
            else
            {
                NotEmpty = Visibility.Collapsed;
                Empty = Visibility.Visible;
            }
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="StringVisibilityConverter" />.
        /// </summary>
        public StringVisibilityConverter()
        {
            NotEmpty = Visibility.Visible;
            Empty = Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="P:System.Windows.Converters.StringVisibilityConverter.Empty" /> si <paramref name="value" /> es una
        ///     cadena
        ///     vacía, <see cref="P:System.Windows.Converters.StringVisibilityConverter.NotEmpty" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string) value)?.IsEmpty() ?? false ? Empty : NotEmpty;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Infiere un valor basado en el <see cref="T:System.Windows.Visibility" /> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <c>value.ToString()</c> si <paramref name="value" /> no es una
        ///     cadena vacía, <see cref="F:System.String.Empty" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
                return v == NotEmpty ? value.ToString() : string.Empty;
            return null;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Devuelve <see cref="F:System.Windows.Visibility.Visible" /> si el elemento a convertir es <see langword="null" />
    /// </summary>
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="Visibility.Visible" /> si el elemento es
        ///     <see langword="null" />, <see cref="Visibility.Collapsed" /> en caso
        ///     contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Una nueva instancia de tipo <paramref name="targetType" /> si <paramref name="value" /> se evalúa como
        ///     <see cref="Visibility.Visible" />, <see langword="null" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility b) return b == Visibility.Visible ? null : targetType.New();
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Devuelve <see cref="F:System.Windows.Visibility.Visible" /> si el elemento a convertir no es
    ///     <see langword="null" />
    /// </summary>
    public sealed class NotNullToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="Visibility.Collapsed" /> si el elemento es
        ///     <see langword="null" />, <see cref="Visibility.Visible" /> en caso
        ///     contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Una nueva instancia de tipo <paramref name="targetType" /> si <paramref name="value" /> se evalúa distinto a
        ///     <see cref="Visibility.Visible" />, <see langword="null" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility b) return b != Visibility.Visible ? null : targetType.New();
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Devuelve <see langword="true" /> si el elemento a convertir es <see langword="null" />
    /// </summary>
    public sealed class NullToBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="Visibility.Visible" /> si el elemento es
        ///     <see langword="null" />, <see cref="Visibility.Collapsed" /> en caso
        ///     contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="bool" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Una nueva instancia de tipo <paramref name="targetType" /> si <paramref name="value" /> se evalúa como
        ///     <see langword="true" />, <see langword="null" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? null : targetType.New();
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Devuelve <see langword="true" /> si el elemento a convertir no es <see langword="null" />
    /// </summary>
    public sealed class NotNullToBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un <see cref="T:System.Windows.Visibility" /> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="Visibility.Collapsed" /> si el elemento es
        ///     <see langword="null" />, <see cref="Visibility.Visible" /> en caso
        ///     contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is null);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="bool" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Una nueva instancia de tipo <paramref name="targetType" /> si <paramref name="value" /> se evalúa como
        ///     <see langword="false" />, <see langword="null" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b) return b ? targetType.New() : null;
            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Invierte un valor de <see cref="T:System.Windows.Visibility" />.
    /// </summary>
    public sealed class VisibilityInverter : Inverter<Visibility>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.VisibilityInverter" />.
        /// </summary>
        public VisibilityInverter() : base(Visibility.Visible, Visibility.Collapsed)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Inverso de <see cref="T:System.Windows.Controls.BooleanToVisibilityConverter" />
    /// </summary>
    public sealed class BooleanToInvVisibilityConverter : BooleanConverter<Visibility>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.BooleanToInvVisibilityConverter" />.
        /// </summary>
        public BooleanToInvVisibilityConverter() : base(Visibility.Collapsed)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Clase base para crear convertidores de valores booleanos que pueden ser
    ///     <see langword="null" />.
    /// </summary>
    /// <typeparam name="T">Tipo de valores a convertir.</typeparam>
    public sealed class NullBoolConverter<T> : IValueConverter
    {
        /// <summary>
        ///     Obtiene o establece el valor que equivale a <see langword="false" /> en este
        ///     <see cref="NullBoolConverter{T}" />.
        /// </summary>
        public T False { get; set; }

        /// <summary>
        ///     Obtiene o establece el valor que equivale a <see langword="null" /> en este
        ///     <see cref="NullBoolConverter{T}" />.
        /// </summary>
        public T Null { get; set; }

        /// <summary>
        ///     Obtiene o establece el valor que equivale a <see langword="true" /> en este
        ///     <see cref="NullBoolConverter{T}" />.
        /// </summary>
        public T True { get; set; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NullBoolConverter{T}" />, configurando los valores que
        ///     corresponderán a <see langword="true" /> y <see langword="false" />.
        /// </summary>
        /// <param name="trueValue">Valor equivalente a <see langword="true" />.</param>
        /// <param name="falseValue">Valor equivalente a <see langword="false" />.</param>
        public NullBoolConverter(T trueValue, T falseValue = default)
        {
            True = trueValue;
            False = falseValue;
            Null = False;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NullBoolConverter{T}" />, configurando los valores que
        ///     corresponderán a <see langword="true" /> y <see langword="false" />.
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

        /// <inheritdoc />
        /// <summary>
        ///     Convierte un valor a <see cref="T:System.Nullable`1" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see cref="P:System.Windows.Converters.NullBoolConverter`1.True" /> si <paramref name="value" /> es
        ///     <see langword="true" />,
        ///     <see cref="P:System.Windows.Converters.NullBoolConverter`1.False" /> si <paramref name="value" /> es
        ///     <see langword="false" />,
        ///     <see cref="P:System.Windows.Converters.NullBoolConverter`1.Null" /> si <paramref name="value" /> es
        ///     <see langword="null" />.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case bool b:
                    return b ? True : False;
                case null:
                    return Null;
            }

            return null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Convierte un <see cref="T:System.Boolean" /> al tipo establecido para este
        ///     <see cref="T:System.Windows.Converters.BooleanConverter`1" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="value" /> es
        ///     <see cref="P:System.Windows.Converters.NullBoolConverter`1.True" />,
        ///     <see langword="false" /> si <paramref name="value" /> es
        ///     <see cref="P:System.Windows.Converters.NullBoolConverter`1.False" />,
        ///     <see langword="null" /> si <paramref name="value" /> es
        ///     <see cref="P:System.Windows.Converters.NullBoolConverter`1.Null" />.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            if (value.Equals(Null) && !ReferenceEquals(Null, False)) return null;
            return ((T) value).Equals(True);
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un valor a su representación como un <see cref="T:System.String" />.
    /// </summary>
    public sealed class ToStringConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Convierte cualquier objeto en un <see cref="T:System.String" />
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <see cref="T:System.String" /> que representa al objeto.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Intenta una conversión de <see cref="T:System.String" /> a un objeto del tipo
        ///     de destino especificado.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Si la conversión desde <see cref="T:System.String" /> tuvo éxito, se
        ///     devolverá al objeto, se devolverá <see langword="null" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return targetType?.GetMethod(
                        "Parse", new[] {typeof(string)})?
                    .Invoke(null, new[] {value});
            }
            catch
            {
                return null;
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un valor a su representación como un <see cref="T:System.String" />.
    /// </summary>
    public sealed class FromStringConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Intenta una conversión de <see cref="T:System.String" /> a un objeto del tipo
        ///     de destino especificado.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Si la conversión desde <see cref="T:System.String" /> tuvo éxito, se
        ///     devolverá al objeto, se devolverá <see langword="null" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return targetType?.GetMethod(
                        "Parse", new[] {typeof(string)})?
                    .Invoke(null, new[] {value});
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Convierte cualquier objeto en un <see cref="T:System.String" />
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <see cref="T:System.String" /> que representa al objeto.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Invierte un valor booleano
    /// </summary>
    public sealed class BooleanInverter : Inverter<bool>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:System.Windows.Converters.BooleanInverter" />.
        /// </summary>
        public BooleanInverter() : base(true, false)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un <see cref="T:System.Double" /> en un <see cref="T:System.Windows.Thickness" />.
    /// </summary>
    public sealed class DoubleMarginConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Convierte un <see cref="T:System.Double" /> en un <see cref="T:System.Windows.Thickness" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Función opcional de transformación de valor. Debe ser de tipo
        ///     <see cref="T:System.Func`2" /> donde el tipo de argumento y el
        ///     tipo devuelto sean ambos <see cref="T:System.Double" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <see cref="T:System.Windows.Thickness" /> uniforme cuyos valores de grosor son
        ///     iguales al valor especificado.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var f = parameter as Func<double, double>;
            return value is double v ? (object) new Thickness(f?.Invoke(v) ?? v) : null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Revierte la conversión realizada por este objeto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Función opcional de transformación de valor.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <see cref="T:System.Double" /> cuyo valor es el promedio del grosor
        ///     establecido en el <see cref="T:System.Windows.Thickness" /> especificado.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Thickness v ? (object) ((v.Top + v.Bottom + v.Left + v.Right) / 4.0) : null;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte directamente un número a <see cref="T:System.Boolean" />
    /// </summary>
    public sealed class NumberToBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Convierte un <see cref="T:System.Int32" /> en un <see cref="T:System.Boolean" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="value" /> es distinto de cero,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int i && i != 0;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Infiere un valor <see cref="T:System.Int32" />a partir de un <see cref="T:System.Boolean" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <c>-1</c> si <paramref name="value" /> es <see langword="true" />, <c>0</c> en
        ///     caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool b ? (object) (b ? -1 : 0) : null;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Permite la adición de propiedades numéricas.
    /// </summary>
    public sealed class AddConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la suma entre <paramref name="value" /> y
        ///     <paramref name="parameter" />.
        /// </summary>
        /// <param name="value">Primer operando de la suma.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">Segundo operando de la suma.</param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="ArgumentException">Se produce si no es posible realizar la suma.</exception>
        /// <returns>La suma de <paramref name="value" /> y el operando especificado.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (dynamic) value + (dynamic) parameter;
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Empty, e);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Revierte la operación de suma aplicada a <paramref name="value" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="ArgumentException">Se produce si no es posible realizar la resta.</exception>
        /// <returns>
        ///     El valor de <paramref name="value" /> antes de la suma.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (dynamic) value - (dynamic) parameter;
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Empty, e);
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Permite la multiplicación de propiedades numéricas.
    /// </summary>
    public sealed class MultiplyConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la multiplicación entre <paramref name="value" /> y
        ///     <paramref name="parameter" />.
        /// </summary>
        /// <param name="value">Primer operando de la multiplicación.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">Segundo operando de la multiplicación.</param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>La multiplicación de <paramref name="value" /> y el operando especificado.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (dynamic) value * (dynamic) parameter;
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Empty, e);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Revierte la operación de suma aplicada a <paramref name="value" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     El valor de <paramref name="value" /> antes de la suma.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (dynamic) value / (dynamic) parameter;
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Empty, e);
            }
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un <see cref="T:System.Double" /> en un <see cref="T:System.String" />,
    ///     opcionalmente mostrando una etiqueta si el valor es inferior a cero.
    /// </summary>
    public sealed class LabeledDoubleConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Convierte un <see cref="T:System.Double" /> en un <see cref="T:System.String" />,
        ///     opcionalmente mostrando una etiqueta si el valor es inferior a cero.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Etiqueta a mostrar en caso que el valor sea inferior a cero.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <see cref="T:System.Windows.Thickness" /> uniforme cuyos valores de grosor son
        ///     iguales al valor especificado.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double v)
            {
                if (parameter is null) parameter = v.ToString(CultureInfo.InvariantCulture);
                if (parameter is string label)
                    return v > 0 ? v.ToString(CultureInfo.InvariantCulture) : label;
            }

            throw new InvalidCastException();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Revierte la conversión realizada por este objeto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Función opcional de transformación de valor.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un <see cref="T:System.Double" /> cuyo valor es el promedio del grosor
        ///     establecido en el <see cref="T:System.Windows.Thickness" /> especificado.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case double v:
                    return v;
                case string s:
                    return double.TryParse(s, out var r) ? r : 0.0;
            }

            throw new InvalidCastException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Permite compartir un recurso de <see cref="T:System.Windows.Media.Brush" /> entre controles,
    ///     ajustando la opacidad del enlace de datos.
    /// </summary>
    public sealed class BrushOpacityAdjust : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Aplica la nueva opacidad al <see cref="T:System.Windows.Media.Brush" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un nuevo <see cref="T:System.Windows.Media.Brush" /> con la opacidad establecida en este
        ///     <see cref="T:System.Windows.Converters.BrushOpacityAdjust" />.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Brush brush)) return null;
            if (!(parameter is double opacity || !double.TryParse(value as string, out opacity)))
                throw new ArgumentException(string.Empty, nameof(parameter));
            if (!opacity.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(opacity));
            var b = brush.Clone();
            b.Opacity = opacity;
            return b;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve un <see cref="T:System.Windows.Media.Brush" /> con 100% opacidad.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un nuevo <see cref="T:System.Windows.Media.Brush" /> con la opacidad al 100%.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Brush brush)) return null;
            var b = brush.Clone();
            b.Opacity = 1;
            return b;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Para un <see cref="T:System.Windows.Controls.ContentControl" /> o un <see cref="T:System.Windows.Controls.Panel" />
    ///     , obtiene
    ///     un valor <see cref="F:System.Windows.Visibility.Visible" /> si al menos un control hijo
    ///     directo es visible.
    /// </summary>
    public sealed class AnyContentVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor <see cref="Visibility.Visible" /> si al menos un
        ///     control hijo directo es visible.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Un nuevo <see cref="F:System.Windows.Visibility.Visible" /> si al
        ///     menos un hijo directo del control es visible,
        ///     <see cref="F:System.Windows.Visibility.Collapsed" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ContentControl contentControl:
                    return (contentControl.Content as FrameworkElement)?.Visibility ?? Visibility.Collapsed;
                case Panel panel:
                    foreach (var j in panel.Children)
                        if ((j as FrameworkElement)?.Visibility == Visibility.Visible)
                            return Visibility.Visible;
                    return Visibility.Collapsed;
                default:
                    return Visibility.Visible;
            }
        }

        /// <inheritdoc />
        /// <summary>Convierte un valor.</summary>
        /// <param name="value">
        ///     Valor generado por el destino de enlace.
        /// </param>
        /// <param name="targetType">Tipo al que se va a convertir.</param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///     Valor convertido.
        ///     Si el método devuelve <see langword="null" />, se usa el valor nulo válido.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Determina si un objeto es una instancia del tipo provisto, y devuelve
    ///     <see cref="F:System.Windows.Visibility.Visible" /> si lo es.
    /// </summary>
    public sealed class StrictTypeVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determina si un objeto es una instancia del tipo provisto.
        /// </summary>
        /// <param name="value">
        ///     Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        ///     El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///     <see cref="F:System.Windows.Visibility.Visible" /> si el objeto es una instancia del
        ///     tipo provisto, <see cref="F:System.Windows.Visibility.Collapsed" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Type t)) throw new ArgumentException();
            return t.IsInstanceOfType(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Determina si un objeto es una instancia del tipo provisto o de una clase derivada, y devuelve
    ///     <see cref="F:System.Windows.Visibility.Visible" /> si lo es.
    /// </summary>
    public sealed class TypeVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determina si un objeto es una instancia del tipo provisto o de una clase derivada.
        /// </summary>
        /// <param name="value">
        ///     Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        ///     El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///     <see cref="F:System.Windows.Visibility.Visible" /> si el objeto es una instancia del
        ///     tipo provisto o de una clase derivada, <see cref="F:System.Windows.Visibility.Collapsed" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Type t)) throw new ArgumentException();
            return t.IsAssignableFrom(value?.GetType()) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Determina si un objeto es una instancia del tipo provisto, y devuelve
    ///     <see langword="true" /> si lo es.
    /// </summary>
    public sealed class StrictTypeBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determina si un objeto es una instancia del tipo provisto.
        /// </summary>
        /// <param name="value">
        ///     Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        ///     El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el objeto es una instancia del
        ///     tipo provisto, <see langword="false" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Type t)) throw new ArgumentException();
            return t.IsInstanceOfType(value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Determina si un objeto es una instancia del tipo provisto o de una clase derivada, y devuelve
    ///     <see langword="true" /> si lo es.
    /// </summary>
    public sealed class TypeBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determina si un objeto es una instancia del tipo provisto o de una clase derivada.
        /// </summary>
        /// <param name="value">
        ///     Valor generado por el origen de enlace.
        /// </param>
        /// <param name="targetType">
        ///     El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el objeto es una instancia del
        ///     tipo provisto o de una clase derivada, <see langword="true" /> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is Type t)) throw new ArgumentException();
            return t.IsAssignableFrom(value?.GetType());
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <exception cref="InvalidCastException">
        ///     Se produce si <paramref name="value" /> no es un <see cref="Visibility" />.
        /// </exception>
        /// <exception cref="TypeLoadException">
        ///     Se produce si <paramref name="targetType" /> no es una clase o estructura instanciable con un constructor sin
        ///     parámetros.
        /// </exception>
        /// <returns>
        ///     Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">Este método siempre genera esta excepción al ser llamado.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un valor <see cref="T:System.Double" /> en un <see cref="T:System.Windows.Visibility" />
    /// </summary>
    public sealed class MinValueVisibilityConverter : ThresholdConverter<double, Visibility>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:System.Windows.Converters.MinSizeVisibilityConverter" />.
        /// </summary>
        public MinValueVisibilityConverter() : base(Visibility.Collapsed, Visibility.Visible, Visibility.Visible)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Convierte un valor <see cref="T:System.Double" /> en un <see cref="T:System.Windows.Visibility" />
    /// </summary>
    public sealed class MaxValueVisibilityConverter : ThresholdConverter<double, Visibility>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:System.Windows.Converters.MaxSizeVisibilityConverter" />.
        /// </summary>
        public MaxValueVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Oculta un control si su tamaño es inferior al umbral especificado.
    /// </summary>
    public sealed class MinSizeVisibilityConverter : SizeVisibilityConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.MinSizeVisibilityConverter" />.
        /// </summary>
        public MinSizeVisibilityConverter() : base(Visibility.Collapsed, Visibility.Visible)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Oculta un control si su tamaño es superior al umbral especificado.
    /// </summary>
    public sealed class MaxSizeVisibilityConverter : SizeVisibilityConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:System.Windows.Converters.MaxSizeVisibilityConverter" />.
        /// </summary>
        public MaxSizeVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Determina un valor de visibilidad basado en que exista al menos un
    ///     valor igual a <see cref="F:System.Windows.Visibility.Visible" />.
    /// </summary>
    public sealed class AnyVisibileConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determina un valor de visibilidad basado en que exista al menos
        ///     un valor igual a <see cref="Visibility.Visible" />
        /// </summary>
        /// <param name="values">
        ///     Valores generados por los orígenes de enlace.
        /// </param>
        /// <param name="targetType">
        ///     El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///     <see cref="F:System.Windows.Visibility.Visible" /> si al menos uno de los valores
        ///     es <see cref="F:System.Windows.Visibility.Visible" />,
        ///     <see cref="F:System.Windows.Visibility.Collapsed" /> en caso contrario.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
#if PreferExceptions
            if (targetType != typeof(Visibility)) throw new InvalidCastException();
#endif
            return values.OfType<Visibility>().Any(p => p == Visibility.Visible)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetTypes">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Este método siempre genera esta excepción al ser llamado.
        /// </exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Determina un valor de visibilidad basado en que todos los valores
    ///     sean igual a <see cref="F:System.Windows.Visibility.Visible" />.
    /// </summary>
    public sealed class AllVisibileConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Determina un valor de visibilidad basado en que todos los
        ///     valores sean igual a
        ///     <see cref="F:System.Windows.Visibility.Visible" />.
        /// </summary>
        /// <param name="values">
        ///     Valores generados por los orígenes de enlace.
        /// </param>
        /// <param name="targetType">
        ///     El tipo de la propiedad del destino de enlace.
        /// </param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns>
        ///     <see cref="F:System.Windows.Visibility.Visible" /> si todos los
        ///     valores son igual a
        ///     <see cref="F:System.Windows.Visibility.Visible" />,
        ///     <see cref="F:System.Windows.Visibility.Collapsed" /> en caso contrario.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.OfType<Visibility>().All(p => p == Visibility.Visible)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetTypes">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     Este método siempre genera un <see cref="InvalidOperationException" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Este método siempre genera esta excepción al ser llamado.
        /// </exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Genera un efecto de desenfoque basado en una condición booleana.
    /// </summary>
    public sealed class BooleanToBlurEffectConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///     Genera un efecto de desenfoque basado en una condición
        ///     booleana.
        /// </summary>
        /// <param name="value">
        ///     Valor generado por el origen del enlace.
        /// </param>
        /// <param name="targetType">
        ///     El tipo de la propiedad del destino del enlace.
        /// </param>
        /// <param name="parameter">
        ///     Parámetro de convertidor que se va a usar.
        /// </param>
        /// <param name="culture">
        ///     Referencia cultural que se va a usar en el convertidor.
        /// </param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double radius;
            switch (parameter)
            {
                case string str:
                    if (str.IsEmpty())
                        radius = 5;
                    else if (!double.TryParse(str, out radius))
                        throw new ArgumentException(nameof(parameter));
                    break;
                case byte d:
                    radius = d;
                    break;
                case short d:
                    radius = d;
                    break;
                case int d:
                    radius = d;
                    break;
                case long d:
                    radius = d;
                    break;
                case float d:
                    radius = d;
                    break;
                case double d:
                    radius = d;
                    break;
                case decimal d:
                    radius = (double) d;
                    break;
#if !CLSCompliance
                case sbyte d: radius = d; break;
                case ushort d: radius = d; break;
                case uint d: radius = d; break;
                case ulong d: radius = d; break;
#endif
                case null:
                    radius = 5;
                    break;
                default:
                    throw new ArgumentException(nameof(parameter));
            }

            if (value is bool b && b) return new BlurEffect {Radius = radius};
            return null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa <see cref="IValueConverter.ConvertBack" />.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        ///     Parámetros personalizados para este <see cref="T:System.Windows.Data.IValueConverter" />.
        /// </param>
        /// <param name="culture">
        ///     <see cref="T:System.Globalization.CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el valor es un <see cref="BlurEffect" />,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is BlurEffect;
        }
    }
}