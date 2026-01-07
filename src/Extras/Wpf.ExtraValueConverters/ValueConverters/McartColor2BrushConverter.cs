/*
McartColor2BrushConverter.cs

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
using System.Windows.Media;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ValueConverters.Base;
using MT = TheXDS.MCART.Types;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Converts values between the <see cref="MT.Color"/> and <see cref="Brush"/>
/// types.
/// </summary>
public sealed class McartColor2BrushConverter : IValueConverter<MT.Color, Brush>
{
    /// <inheritdoc/>
    public Brush Convert(MT.Color value, object? parameter, CultureInfo? culture)
    {
        return WpfColorExtensions.Brush(value);
    }

    /// <inheritdoc/>
    public MT.Color ConvertBack(Brush value, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            SolidColorBrush b => b.Color.ToMcartColor(),
            GradientBrush g => g.Blend(),
            _ => default
        };
    }
}
