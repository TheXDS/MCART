/*
ValueConverters.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Windows.Data;
using System.Windows.Media;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.Types;
using MT = TheXDS.MCART.Types;
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.MCART.ValueConverters
{

    /// <summary>
    /// Convierte valores desde y hacia objetos de tipo
    /// <see cref="MT.Color"/> y <see cref="System.Windows.Media.Color"/>.
    /// </summary>
    public sealed class McartColor2MediaColorConverter : IValueConverter<MT.Color, System.Windows.Media.Color>
    {
        /// <summary>
        /// Convierte un <see cref="MT.Color"/> en un <see cref="System.Windows.Media.Color"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter" />.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        /// Un <see cref="System.Windows.Media.Color"/> equivalente al <see cref="MT.Color"/> original.
        /// </returns>
        public System.Windows.Media.Color Convert(MT.Color value, object? parameter, CultureInfo? culture)
        {
            return WpfColorExtensions.Color(value);
        }

        /// <summary>
        /// Convierte un <see cref="System.Windows.Media.Color"/> en un 
        /// <see cref="MT.Color"/>.
        /// </summary>
        /// <param name="value">Objeto a convertir.</param>
        /// <param name="parameter">
        /// Parámetros personalizados para este <see cref="IValueConverter" />.
        /// </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> a utilizar para la conversión.
        /// </param>
        /// <returns>
        /// Un <see cref="MT.Color"/> creado a partir del
        /// valor del objeto, o <see langword="null"/> si no es posible
        /// realizar la conversión.
        /// </returns>
        public MT.Color ConvertBack(System.Windows.Media.Color value, object? parameter, CultureInfo? culture)
        {
            return value.ToMcartColor();
        }
    }

    public sealed class McartColor2BrushConverter : IValueConverter<MT.Color, System.Windows.Media.Brush>
    {
        public System.Windows.Media.Brush Convert(MT.Color value, object? parameter, CultureInfo? culture)
        {
            return WpfColorExtensions.Brush(value);
        }

        public MT.Color ConvertBack(System.Windows.Media.Brush value, object parameter, CultureInfo culture)
        {
            return value switch
            {
                SolidColorBrush b => b.Color.ToMcartColor(),
                GradientBrush g => g.Blend(),
                _ => default
            };
        }
    }

    public sealed class McartColor2IntConverter : IValueConverter<MT.Color, int>
    {
        public int Convert(MT.Color value, object? parameter, CultureInfo? culture)
        {
            return new ABGR32().To(value);
        }

        public MT.Color ConvertBack(int value, object parameter, CultureInfo culture)
        {
            return new ABGR32().From(value);
        }
    }

    public class McartColor2DrawingColorConverter : IValueConverter<MT.Color, System.Drawing.Color>
    {
        public System.Drawing.Color Convert(MT.Color value, object? parameter, CultureInfo? culture) => value;

        public MT.Color ConvertBack(System.Drawing.Color value, object parameter, CultureInfo culture) => value;
    }
}