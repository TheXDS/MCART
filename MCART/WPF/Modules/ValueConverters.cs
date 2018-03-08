//
//  ValueConverters.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using TheXDS.MCART;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace System.Windows.Converters
{
    #region Converters biendeables en código
    /// <summary>
    /// Clase base para crear convertidores de valores que inviertan el valor
    /// de una propiedad de dependencia.
    /// </summary>
    /// <typeparam name="T">Tipo de valor a invertir.</typeparam>
    public abstract class Inverter<T> : IValueConverter where T : struct
    {
        private T yay, nay;
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="Inverter{T}"/>.
        /// </summary>
        /// <param name="yayValue">Valor invertible.</param>
        /// <param name="nayValue">
        /// Valor inverso de <paramref name="yayValue"/>.
        /// </param>
        protected Inverter(T yayValue, T nayValue) { yay = yayValue; nay = nayValue; }
        /// <summary>
        /// Invierte el valor de  un <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un <typeparamref name="T"/> cuyo valor es el inverso de
        /// <paramref name="value"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            T v = (T)value;
            return v.Equals(yay) ? nay : yay;
        }
        /// <summary>
        /// Invierte el valor de  un <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un <typeparamref name="T"/> cuyo valor es el inverso de
        /// <paramref name="value"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            T v = (T)value;
            return v.Equals(yay) ? nay : yay;
        }
    }
    /// <summary>
    /// Clase base para crear convertidores de valores booleanos.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valores a convertir. Deben ser estructuras o enumeraciones.
    /// </typeparam>
    public class BooleanConverter<T> : IValueConverter where T : struct
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BooleanConverter{T}"/>, configurando los valores que
        /// corresponderán a <see langword="true"/> y <see langword="false"/>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <see langword="true"/>.</param>
        /// <param name="FalseValue">Valor equivalente a <see langword="false"/>.</param>
        public BooleanConverter(T TrueValue, T FalseValue = default)
        {
            True = TrueValue;
            False = FalseValue;
        }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="true"/> en este
        /// <see cref="BooleanConverter{T}"/>.
        /// </summary>
        public T True { get; set; }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="false"/> en este
        /// <see cref="BooleanConverter{T}"/>.
        /// </summary>
        public T False { get; set; }
        /// <summary>
        /// Convierte un <see cref="bool"/> a un objeto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see cref="True"/>, si el objeto es de tipo <see cref="bool"/> y su
        /// valor es <see langword="true"/>; en caso contrario, se devuelve 
        /// <see cref="False"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool) return (bool)value ? True : False;
            return null;
        }
        /// <summary>
        /// Convierte un objeto en un <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see langword="true"/> si el objeto es igual a <see cref="True"/>; 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is T && ((T)value).Equals(True));
        }
    }
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
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BoolFlagConverter{T}"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el tipo especificado al instanciar esta clase no es
        /// una enumeración.
        /// </exception>
        public BoolFlagConverter()
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = default;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BoolFlagConverter{T}"/>, configurando el valor que
        /// corresponderá a <see langword="true"/>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <see langword="true"/>.</param>
        /// <exception cref="InvalidOperationException">
        /// Se produce si el tipo especificado al instanciar esta clase no es
        /// una enumeración.
        /// </exception>
        public BoolFlagConverter(T TrueValue)
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = TrueValue;
        }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="true"/> en este
        /// <see cref="BoolFlagConverter{T}"/>.
        /// </summary>
        public T True { get; set; }
        /// <summary>
        /// Convierte un valor a <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Si no se ha establecido un valor para <see cref="True"/>, se
        /// devolverá <see langword="true"/> si hay alguna bandera activa, o <see langword="false"/> 
        /// en caso contrario. Si se estableció un valor para 
        /// <see cref="True"/>, se devolverá <see langword="true"/> solo si dicha(s) 
        /// bandera(s) se encuentra(n) activa(s), <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return True.Equals(default) ? !value.Equals(True) : value.Equals(True);
        }
        /// <summary>
        /// Convierte un <see cref="bool"/> al tipo establecido para este
        /// <see cref="BoolFlagConverter{T}"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Si <paramref name="value"/> es <see langword="true"/>, se devuelve la(s) 
        /// bandera(s) a ser detectada(s), en caso de haberse establecido un 
        /// valor para <see cref="True"/>, o en caso contrario, se devolverá
        /// <c>default</c>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? True : default;
        }
    }
    /// <summary>
    /// Convierte un valor <see cref="int"/>  a <see cref="Visibility"/> 
    /// </summary>
    public class CountVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver en caso
        /// de que la cuenta de elementos sea mayor a cero.
        /// </summary>
        public Visibility WithItems;
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver en caso
        /// de que la cuenta de elementos sea igual a cero.
        /// </summary>
        public Visibility WithoutItems;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="CountVisibilityConverter"/>.
        /// </summary>
        /// <param name="withItems">
        /// <see cref="Visibility"/> a utilizar en caso de que la cuenta sea 
        /// mayor a cero.
        /// </param>
        /// <param name="withoutItems">
        /// <see cref="Visibility"/> a utilizar en caso de que la cuenta sea 
        /// igual a cero.
        /// </param>
        public CountVisibilityConverter(Visibility withItems = Visibility.Visible, Visibility withoutItems = Visibility.Collapsed)
        {
            WithItems = withItems;
            WithoutItems = withoutItems;
        }
        /// <summary>
        /// Obtiene un <see cref="Visibility"/> a partir de un valor de cuenta.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see cref="WithItems"/> si <paramref name="value"/> es mayor a
        /// cero, <see cref="WithoutItems"/> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)value > 0 ? WithItems : WithoutItems;
        /// <summary>
        /// Infiere una cuenta de elementos basado en el <see cref="Visibility"/> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <c>1</c> si <paramref name="value"/> es igual a 
        /// <see cref="WithItems"/>, <c>0</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == WithItems ? 1 : 0;
    }
    /// <summary>
    /// Convierte un valor <see cref="double"/> a <see cref="Visibility"/>.
    /// </summary>
    public class DoubleVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver cuando
        /// el valor sea mayor a cero.
        /// </summary>
        public Visibility Positives;
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver cuando
        /// el valor sea menor o igual a cero.
        /// </summary>
        public Visibility ZeroOrNegatives;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DoubleVisibilityConverter"/>.
        /// </summary>
        /// <param name="positives">
        /// <see cref="Visibility"/> a utilizar para valores positivos.
        /// </param>
        /// <param name="zeroOrNegatives">
        /// <see cref="Visibility"/> a utilizar para cero y valores negativos.
        /// </param>
        public DoubleVisibilityConverter(Visibility positives = Visibility.Visible, Visibility zeroOrNegatives = Visibility.Collapsed)
        {
            Positives = positives;
            ZeroOrNegatives = zeroOrNegatives;
        }
        /// <summary>
        /// Obtiene un <see cref="Visibility"/> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see cref="Positives"/> si <paramref name="value"/> es mayor a
        /// cero, <see cref="ZeroOrNegatives"/> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)value > 0.0 ? Positives : ZeroOrNegatives;
        /// <summary>
        /// Infiere un valor basado en el <see cref="Visibility"/> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <c>1.0</c> si <paramref name="value"/> es igual a 
        /// <see cref="Positives"/>, <c>0.0</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == Positives ? 1.0 : 0.0;
    }
    /// <summary>
    /// Convierte un valor <see cref="float"/> a <see cref="Visibility"/>.
    /// </summary>
    public class FloatVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver cuando
        /// el valor sea mayor a cero.
        /// </summary>
        public Visibility Positives;
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver cuando
        /// el valor sea menor o igual a cero.
        /// </summary>
        public Visibility ZeroOrNegatives;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="FloatVisibilityConverter"/>.
        /// </summary>
        /// <param name="positives">
        /// <see cref="Visibility"/> a utilizar para valores positivos.
        /// </param>
        /// <param name="zeroOrNegatives">
        /// <see cref="Visibility"/> a utilizar para cero y valores negativos.
        /// </param>
        public FloatVisibilityConverter(Visibility positives = Visibility.Visible, Visibility zeroOrNegatives = Visibility.Collapsed)
        {
            Positives = positives;
            ZeroOrNegatives = zeroOrNegatives;
        }
        /// <summary>
        /// Obtiene un <see cref="Visibility"/> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see cref="Positives"/> si <paramref name="value"/> es mayor a
        /// cero, <see cref="ZeroOrNegatives"/> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (float)value > 0.0 ? Positives : ZeroOrNegatives;
        /// <summary>
        /// Infiere un valor basado en el <see cref="Visibility"/> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <c>1.0f</c> si <paramref name="value"/> es igual a 
        /// <see cref="Positives"/>, <c>0.0f</c> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == Positives ? 1.0f : 0.0f;
    }
    /// <summary>
    /// Convierte un <see cref="string"/> a un <see cref="Visibility"/>.
    /// </summary>
    public class StringVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver cuando
        /// el <see cref="string"/> esté vacío.
        /// </summary>
        public Visibility Empty;
        /// <summary>
        /// Obtiene o establece el <see cref="Visibility"/> a devolver cuando
        /// el <see cref="string"/> no esté vacío.
        /// </summary>
        public Visibility NotEmpty;
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StringVisibilityConverter"/>.
        /// </summary>
        /// <param name="empty">
        /// <see cref="Visibility"/> a devolver cuando la cadena esté vacía.
        /// </param>
        /// <param name="notEmpty">
        /// <see cref="Visibility"/> a devolver cuando la cadena no esté vacía.
        /// </param>
        public StringVisibilityConverter(Visibility empty, Visibility notEmpty = Visibility.Visible)
        {
            NotEmpty = notEmpty;
            Empty = empty;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StringVisibilityConverter"/>.
        /// </summary>
        /// <param name="inverted">
        /// Inversión de valores de <see cref="Visibility"/>.
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
        /// Inicializa una nueva instancia de la clase
        /// <see cref="StringVisibilityConverter"/>.
        /// </summary>
        public StringVisibilityConverter()
        {
            NotEmpty = Visibility.Visible;
            Empty = Visibility.Collapsed;
        }
        /// <summary>
        /// Obtiene un <see cref="Visibility"/> a partir del valor.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see cref="Empty"/> si <paramref name="value"/> es una cadena
        /// vacía, <see cref="NotEmpty"/> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((string)value).IsEmpty() ? Empty : NotEmpty;
        /// <summary>
        /// Infiere un valor basado en el <see cref="Visibility"/> provisto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <c>value.ToString()</c> si <paramref name="value"/> no es una
        /// cadena vacía, <see cref="String.Empty"/> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == NotEmpty ? value.ToString() : string.Empty;
    } 
    #endregion

    /// <summary>
    /// Clase base para crear convertidores de valores booleanos que pueden ser
    /// <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de valores a convertir.</typeparam>
    public class NullBoolConverter<T> : IValueConverter
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NullBoolConverter{T}"/>, configurando los valores que
        /// corresponderán a <see langword="true"/> y <see langword="false"/>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <see langword="true"/>.</param>
        /// <param name="FalseValue">Valor equivalente a <see langword="false"/>.</param>
        public NullBoolConverter(T TrueValue, T FalseValue = default)
        {
            True = TrueValue;
            False = FalseValue;
            Null = False;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NullBoolConverter{T}"/>, configurando los valores que
        /// corresponderán a <see langword="true"/> y <see langword="false"/>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <see langword="true"/>.</param>
        /// <param name="FalseValue">Valor equivalente a <see langword="false"/>.</param>
        /// <param name="NullValue">Valor equivalente a <see langword="null"/>.</param>
        public NullBoolConverter(T TrueValue, T FalseValue, T NullValue)
        {
            True = TrueValue;
            False = FalseValue;
            Null = NullValue;
        }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="true"/> en este
        /// <see cref="NullBoolConverter{T}"/>.
        /// </summary>
        public T True { get; set; }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="false"/> en este
        /// <see cref="NullBoolConverter{T}"/>.
        /// </summary>
        public T False { get; set; }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <see langword="null"/> en este
        /// <see cref="NullBoolConverter{T}"/>.
        /// </summary>
        public T Null { get; set; }
        /// <summary>
        /// Convierte un valor a <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see cref="True"/> si <paramref name="value"/> es <see langword="true"/>,
        /// <see cref="False"/> si <paramref name="value"/> es <see langword="false"/>,
        /// <see cref="Null"/> si <paramref name="value"/> es <see langword="null"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool) return (bool)value ? True : False;
            if (value is bool?)
            {
                if (value is null) return Null;
                return (bool)value ? True : False;
            }
            return null;
        }
        /// <summary>
        /// Convierte un <see cref="bool"/> al tipo establecido para este
        /// <see cref="BooleanConverter{T}"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="value"/> es <see cref="True"/>,
        /// <see langword="false"/> si <paramref name="value"/> es <see cref="False"/>,
        /// <see langword="null"/> si <paramref name="value"/> es <see cref="Null"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(Null) && !ReferenceEquals(Null, False)) return null;
            return (((T)value).Equals(True));
        }
    }
    /// <summary>
    /// Invierte un valor de <see cref="Visibility"/>.
    /// </summary>
    public sealed class VisibilityInverter : Inverter<Visibility>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="VisibilityInverter"/>.
        /// </summary>
        public VisibilityInverter() : base(Visibility.Visible, Visibility.Collapsed) { }
    }
    /// <summary>
    /// Inverso de <see cref="BooleanToVisibilityConverter"/> 
    /// </summary>
    public sealed class BooleanToInvVisibilityConverter : BooleanConverter<Visibility>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="BooleanToInvVisibilityConverter"/>.
        /// </summary>
        public BooleanToInvVisibilityConverter() : base(Visibility.Collapsed, Visibility.Visible) { }
    }
    /// <summary>
    /// Convierte un valor a su representación como un <see cref="string"/>.
    /// </summary>
    public sealed class ToStringConverter : IValueConverter
    {
        /// <summary>
        /// Convierte cualquier objeto en un <see cref="string"/>
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un <see cref="string"/> que representa al objeto.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
        /// <summary>
        /// Intenta una conversión de <see cref="string"/> a un objeto del tipo
        /// de destino especificado.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Si la conversión desde <see cref="string"/> tuvo éxito, se 
        /// devolverá al objeto, se devolverá <see langword="null"/> en caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return targetType?.GetMethod(
                    "Parse", new Type[] { typeof(string) })?
                    .Invoke(null, new object[] { value });
            }
            catch { return null; }
        }
    }
    /// <summary>
    /// Permite la adición de propiedades numéricas.
    /// </summary>
    public sealed class AddConverter : IValueConverter
    {
        /// <summary>
        /// Devuelve la suma entre <paramref name="value"/> y 
        /// <paramref name="parameter"/>.
        /// </summary>
        /// <param name="value">Primer operando de la suma.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">Segundo operando de la suma.</param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>La suma de <paramref name="value"/> y el operando especificado.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().IsPrimitive && parameter.GetType().IsPrimitive)
                return (dynamic)value + (dynamic)parameter;
            throw new ArgumentException();
        }
        /// <summary>
        /// Revierte la operación de suma aplicada a <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// El valor de <paramref name="value"/> antes de la suma.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().IsPrimitive && parameter.GetType().IsPrimitive)
                return (dynamic)value - (dynamic)parameter;
            throw new ArgumentException();
        }
    }
    /// <summary>
    /// Permite la multiplicación de propiedades numéricas.
    /// </summary>
    public sealed class MultiplyConverter : IValueConverter
    {
        /// <summary>
        /// Devuelve la multiplicación entre <paramref name="value"/> y 
        /// <paramref name="parameter"/>.
        /// </summary>
        /// <param name="value">Primer operando de la multiplicación.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">Segundo operando de la multiplicación.</param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>La multiplicación de <paramref name="value"/> y el operando especificado.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().IsPrimitive && parameter.GetType().IsPrimitive)
                return (dynamic)value * (dynamic)parameter;
            throw new ArgumentException();
        }
        /// <summary>
        /// Revierte la operación de suma aplicada a <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// El valor de <paramref name="value"/> antes de la suma.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().IsPrimitive && parameter.GetType().IsPrimitive)
                return (dynamic)value / (dynamic)parameter;
            throw new ArgumentException();
        }
    }
    /// <summary>
    /// Convierte un <see cref="double"/> en un <see cref="Thickness"/>.
    /// </summary>
    public sealed class DoubleMarginConverter : IValueConverter
    {
        /// <summary>
        /// Convierte un <see cref="double"/> en un <see cref="Thickness"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Función opcional de transformación de valor. Dene ser de tipo
        /// <see cref="Func{T1, TResult}"/> donde el tipo de argumento y el
        /// tipo devuelto sean ambos <see cref="double"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un <see cref="Thickness"/> uniforme cuyos valores de grosor son
        /// iguales al valor especificado.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<double, double> f = parameter as Func<double, double>;
            double v = (double)value;
            return new Thickness(f?.Invoke(v) ?? v);
        }
        /// <summary>
        /// Revierte la conversión realizada por este objeto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Función opcional de transformación de valor.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un <see cref="double"/> cuyo valor es el promedio del grosor
        /// establecido en el <see cref="Thickness"/> especificado.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness v = (Thickness)value;
            return (v.Top + v.Bottom + v.Left + v.Right) / 4.0;
        }
    }
    /// <summary>
    /// Convierte un <see cref="double"/> en un <see cref="string"/>,
    /// opcionalmente mostrando una etiqueta si el valor es inferior a cero.
    /// </summary>
    public sealed class LabeledDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Convierte un <see cref="double"/> en un <see cref="string"/>,
        /// opcionalmente mostrando una etiqueta si el valor es inferior a cero.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Etiqueta a mostrar en caso que el valor sea inferior a cero.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un <see cref="Thickness"/> uniforme cuyos valores de grosor son
        /// iguales al valor especificado.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double v)
            {
                if (parameter is null) parameter = v.ToString();
                if (parameter is string label)
                    return v > 0 ? v.ToString() : label;
            }
            throw new InvalidCastException();
        }
        /// <summary>
        /// Revierte la conversión realizada por este objeto.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Función opcional de transformación de valor.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un <see cref="double"/> cuyo valor es el promedio del grosor
        /// establecido en el <see cref="Thickness"/> especificado.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double) return value;
            if (value is string s)
            {
                bool ok = double.TryParse(s, out double r);
                return ok ? r : 0.0;
            }
            throw new InvalidCastException();
        }
    }
    /// <summary>
    /// Permite compartir un recurso de <see cref="Brush"/> entre controles,
    /// ajustando la opacidad del enlace de datos.
    /// </summary>
    public sealed class BrushOpacityAdjust : IValueConverter
    {
        /// <summary>
        /// Aplica la nueva opacidad al <see cref="Brush"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un nuevo <see cref="Brush"/> con la opacidad establecida en este
        /// <see cref="BrushOpacityAdjust"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is double opacity)
            {
                if (!opacity.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(opacity));
                Brush b = ((Brush)value).Clone();
                b.Opacity = opacity;
                return b;
            }
            throw new ArgumentException(string.Empty, nameof(parameter));
        }
        /// <summary>
        /// Devuelve un <see cref="Brush"/> con 100% opacidad.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// Un nuevo <see cref="Brush"/> con la opacidad al 100%.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush b = ((Brush)value).Clone();
            b.Opacity = 1;
            return b;
        }
    }
    /// <summary>
    /// Convierte directamente un número a <see cref="bool"/> 
    /// </summary>
    public sealed class NumberBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convierte un <see cref="int"/> en un <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="value"/> es distinto de cero,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)value != 0;
        /// <summary>
        /// Infiere un valor <see cref="int"/>a partir de un <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="targetType">Tipo del destino.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter"/>.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo"/> a utilizar para la conversión.</param>
        /// <returns>
        /// <c>-1</c> si <paramref name="value"/> es <see langword="true"/>, <c>0</c> en
        /// caso contrario.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? -1 : 0;
    }
    /// <summary>
    /// Invierte un valor booleano
    /// </summary>
    public sealed class BooleanInverter : Inverter<bool>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BooleanInverter"/>.
        /// </summary>
        public BooleanInverter() : base(true, false) { }
    }
}