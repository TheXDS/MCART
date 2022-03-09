/*
TransparentColorAdapter.cs

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

using System.Globalization;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;
using System;
using MC = System.Windows.Media.Color;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.ValueConverters
{
    /// <summary>
    /// Convierte un valor <see cref="TransparentColor"/> a un color utilizable por WPF.
    /// </summary>
    public class TransparentColorAdapter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TransparentColor c && targetType == typeof(MC) ? (MC)c : targetType.Default()!;
        }

        /// <inheritdoc/>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not MC mc ? null : (object)new TransparentColor
            {
                BaseColor = new MC
                {
                    A = 255,
                    ScR = mc.ScR,
                    ScG = mc.ScG,
                    ScB = mc.ScB,
                }.ToMcartColor(),
                UseAlpha = mc.A != 255,
                Transparency = mc.ScA
            };
        }
    }
}