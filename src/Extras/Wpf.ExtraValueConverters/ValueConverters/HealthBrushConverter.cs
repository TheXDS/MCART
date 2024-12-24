/*
HealthBrushConverter.cs

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

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ValueConverters.Base;
using static TheXDS.MCART.Math.Common;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Gets a <see cref="Brush"/> corresponding to health expressed
/// as a percentage.
/// </summary>
public sealed class HealthBrushConverter : FloatConverterBase, IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object? parameter, CultureInfo? culture)
    {
        return Types.Color.BlendHealth(GetFloat(value).Clamp(0f, 1f)).Brush();
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object? parameter, CultureInfo? culture)
    {
        throw new InvalidOperationException();
    }
}
