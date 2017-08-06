//
//  ValueConverters.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using MCART;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
namespace System.Windows.Converters
{
    /// <summary>
    /// Clase base para crear convertidores de valores booleanos
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valores a convertir. Deben ser estructuras o enumeraciones.
    /// </typeparam>
    public class BooleanConverter<T> : IValueConverter where T : struct
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BooleanConverter{T}"/>, configurando los valores que
        /// corresponderán a <c>true</c> y <c>false</c>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <c>true</c>.</param>
        /// <param name="FalseValue">Valor equivalente a <c>false</c>.</param>
        public BooleanConverter(T TrueValue, T FalseValue = default(T))
        {
            True = TrueValue;
            False = FalseValue;
        }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <c>true</c> en este
        /// <see cref="BooleanConverter{T}"/>.
        /// </summary>
        public T True { get; set; }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <c>false</c> en este
        /// <see cref="BooleanConverter{T}"/>.
        /// </summary>
        public T False { get; set; }
        /// <summary>
        /// Convierte un valor a <see cref="bool"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool) return (bool)value ? True : False;
            return null;
        }
        /// <summary>
        /// Convierte un <see cref="bool"/> al tipo establecido para este
        /// <see cref="BooleanConverter{T}"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
        public BoolFlagConverter()
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = default(T);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="BoolFlagConverter{T}"/>, configurando el valor que
        /// corresponderá a <c>true</c>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <c>true</c>.</param>
        public BoolFlagConverter(T TrueValue)
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            True = TrueValue;
        }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <c>true</c> en este
        /// <see cref="BoolFlagConverter{T}"/>.
        /// </summary>
        public T True { get; set; }
        /// <summary>
        /// Convierte un valor a <see cref="bool"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return True.Equals(default(T)) ? !value.Equals(True) : value.Equals(True);
        }
        /// <summary>
        /// Convierte un <see cref="bool"/> al tipo establecido para este
        /// <see cref="BoolFlagConverter{T}"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? True : default(T);
        }
    }
    /// <summary>
    /// Clase base para crear convertidores de valores booleanos que pueden ser <c>null</c>.
    /// </summary>
    /// <typeparam name="T">Tipo de valores a convertir.</typeparam>
    public class NullBoolConverter<T> : IValueConverter
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NullBoolConverter{T}"/>, configurando los valores que
        /// corresponderán a <c>true</c> y <c>false</c>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <c>true</c>.</param>
        /// <param name="FalseValue">Valor equivalente a <c>false</c>.</param>
        public NullBoolConverter(T TrueValue, T FalseValue = default(T))
        {
            True = TrueValue;
            False = FalseValue;
            Null = False;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NullBoolConverter{T}"/>, configurando los valores que
        /// corresponderán a <c>true</c> y <c>false</c>.
        /// </summary>
        /// <param name="TrueValue">Valor equivalente a <c>true</c>.</param>
        /// <param name="FalseValue">Valor equivalente a <c>false</c>.</param>
        /// <param name="NullValue">Valor equivalente a <c>null</c>.</param>
        public NullBoolConverter(T TrueValue, T FalseValue, T NullValue)
        {
            True = TrueValue;
            False = FalseValue;
            Null = NullValue;
        }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <c>true</c> en este
        /// <see cref="NullBoolConverter{T}"/>.
        /// </summary>
        public T True { get; set; }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <c>false</c> en este
        /// <see cref="NullBoolConverter{T}"/>.
        /// </summary>
        public T False { get; set; }
        /// <summary>
        /// Obtiene o establece el valor que equivale a <c>null</c> en este
        /// <see cref="NullBoolConverter{T}"/>.
        /// </summary>
        public T Null { get; set; }
        /// <summary>
        /// Convierte un valor a <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool) return (bool)value ? True : False;
            if (value is bool?)
            {
                if (value.IsNull()) return Null;
                return (bool)value ? True : False;
            }
            return null;
        }
        /// <summary>
        /// Convierte un <see cref="bool"/> al tipo establecido para este
        /// <see cref="BooleanConverter{T}"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(Null) && !ReferenceEquals(Null, False)) return null;
            return (((T)value).Equals(True));
        }
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
        public BooleanToInvVisibilityConverter(Visibility trueState = Visibility.Collapsed)
            : base(trueState, Visibility.Visible)
        {
            if (trueState == Visibility.Visible) throw new ArgumentException(nameof(trueState));
        }
    }
    /// <summary>
    /// Convierte un valor a su representación como un <see cref="string"/>.
    /// </summary>
    public class ToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// Permite la adición de propiedades numéricas
    /// </summary>
    /// <typeparam name="T">Tipo de valores. Puede ser cualquier tipo de valor numérico.</typeparam>
    public class AddConverter<T> : IValueConverter where T : struct
    {
        dynamic x;
        public AddConverter(T Operand)
        {
            if (!typeof(T).IsPrimitive) throw new InvalidCastException();
            x = Operand;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (T)(value + x);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (T)(value - x);
    }
    /// <summary>
    /// Permite la multiplicación de propiedades numéricas
    /// </summary>
    /// <typeparam name="T">Tipo de valores. Puede ser cualquier tipo de valor numérico.</typeparam>
    public class MultiplyConverter<T> : IValueConverter where T : struct
    {
        dynamic x;
        public MultiplyConverter(T Operand)
        {
            if (!typeof(T).IsPrimitive) throw new InvalidCastException();
            x = Operand;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (T)(x * value);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (T)(x / value);
    }
    /// <summary>
    /// Convierte un valor <see cref="double"/>  a <see cref="Visibility"/> 
    /// </summary>
    public class CountVisibilityConverter : IValueConverter
    {
        Visibility x;
        Visibility y;
        public CountVisibilityConverter(Visibility HasItems = Visibility.Visible, Visibility NoItems = Visibility.Collapsed)
        {
            x = HasItems;
            y = NoItems;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)(value) > 0 ? x : y;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == x ? 1 : 0;
    }
    /// <summary>
    /// Convierte un valor <see cref="double"/> a <see cref="Visibility"/>.
    /// </summary>
    public class DoubleVisibilityConverter : IValueConverter
    {
        Visibility x;
        Visibility y;
        public DoubleVisibilityConverter(Visibility ForPositive = Visibility.Visible, Visibility ZeroOrNegative = Visibility.Collapsed)
        {
            x = ForPositive;
            y = ZeroOrNegative;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)(value) > 0.0 ? x : y;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == x ? 1.0 : 0.0;
    }
    /// <summary>
    /// Convierte un valor <see cref="double"/> a <see cref="Visibility"/>.
    /// </summary>
    public class FloatVisibilityConverter : IValueConverter
    {
        Visibility x;
        Visibility y;
        public FloatVisibilityConverter(Visibility ForPositive = Visibility.Visible, Visibility ZeroOrNegative = Visibility.Collapsed)
        {
            x = ForPositive;
            y = ZeroOrNegative;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (float)(value) > 0.0 ? x : y;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == x ? 1.0f : 0.0f;
    }
    /// <summary>
    /// Permite compartir un recurso de <see cref="Brush"/> entre controles,
    /// ajustando la opacidad del enlace de datos.
    /// </summary>
    public class BrushOpacityAdjust : IValueConverter
    {
        double f;
        public BrushOpacityAdjust(double Factor)
        {
            if (!Factor.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(Factor));
            f = Factor;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush b = ((Brush)value).Clone();
            b.Opacity = f;
            return b;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush b = ((Brush)value).Clone();
            b.Opacity = 1;
            return b;
        }
    }
    /// <summary>
    /// Convierte un <see cref="string"/> a un <see cref="Visibility"/>.
    /// </summary>
    public class StringVisibilityConverter : IValueConverter
    {
        Visibility h;
        Visibility v;
        public StringVisibilityConverter(Visibility Empty, Visibility NotEmpty = Visibility.Visible)
        {
            v = NotEmpty;
            h = Empty;
        }
        public StringVisibilityConverter(bool Inverted = false)
        {
            if (Inverted)
            {
                v = Visibility.Visible;
                h = Visibility.Collapsed;
            }
            else
            {
                v = Visibility.Collapsed;
                h = Visibility.Visible;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (string)value.IsEmpty() ? h : v;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == v ? value.ToString() : string.Empty;
    }
    /// <summary>
    /// Convierte directamente un número a <see cref="bool"/> 
    /// </summary>
    public class NumberBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)value != 0;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? -1 : 0;
    }

    #region Inversores
    /// <summary>
    /// Invierte un valor booleano
    /// </summary>
    public class BooleanInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
    }
    /// <summary>
    /// Invierte los valores de <see cref="Visibility"/> 
    /// </summary>
    public class VisibilityInverter : IValueConverter
    {
        Visibility h;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="VisibilityInverter"/>.
        /// </summary>
        /// <param name="notVisible">
        /// Opcional. Estado a devolver al invertir el valor de
        /// <see cref="Visibility.Visible"/>. Si se omite, se devolverá
        /// <see cref="Visibility.Collapsed"/>.
        /// </param>
        public VisibilityInverter(Visibility notVisible = Visibility.Collapsed) { h = notVisible; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == Visibility.Visible ? h : Visibility.Visible;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (Visibility)value == Visibility.Visible ? h : Visibility.Visible;
    }
    #endregion
}