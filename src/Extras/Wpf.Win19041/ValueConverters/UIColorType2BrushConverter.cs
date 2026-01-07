/*
UIColorType2BrushConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using System.Windows.Data;
using System.Windows.Media;
using TheXDS.MCART.ValueConverters.Base;
using TheXDS.MCART.Types.Extensions;
using Windows.UI.ViewManagement;
using MT = TheXDS.MCART.Types;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Converts values to and from objects of type
/// <see cref="MT.Color"/> and <see cref="Brush"/>.
/// </summary>
[CLSCompliant(false)]
public sealed class UIColorType2BrushConverter : IOneWayValueConverter<UIColorType, Brush>
{
    /// <summary>
    /// Converts a <see cref="UIColorType"/> to a <see cref="Brush"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="parameter">
    /// Custom parameters for this <see cref="IValueConverter"/>.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use for the conversion.
    /// </param>
    /// <returns>
    /// A <see cref="Brush"/> equivalent to the original <see cref="UIColorType"/>.
    /// </returns>
    public Brush Convert(UIColorType value, object? parameter, CultureInfo? culture)
    {
        return value.ToMediaBrush();
    }
}
