/*
AllVisibleConverter.cs

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
using TheXDS.MCART.ValueConverters.Base;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Implements a value converter that converts objects between types
/// <c><see cref="bool"/>?</c> and <c><see cref="bool"/></c>.
/// </summary>
public class NullableBoolToBoolConverter : IValueConverter<bool?, bool>
{
    /// <summary>
    /// Converts a nullable bool to a bool.
    /// </summary>
    /// <param name="value">The nullable bool value.</param>
    /// <param name="parameter">Optional parameter (not used).</param>
    /// <param name="culture">Optional culture (not used).</param>
    /// <returns>False if value is null; otherwise, the bool value.</returns>
    public bool Convert(bool? value, object? parameter, CultureInfo? culture)
    {
        return value ?? false;
    }

    /// <summary>
    /// Converts a bool to a nullable bool.
    /// </summary>
    /// <param name="value">The bool value.</param>
    /// <param name="parameter">Optional parameter (not used).</param>
    /// <param name="culture">Optional culture (not used).</param>
    /// <returns>The nullable bool representation of the value.</returns>
    public bool? ConvertBack(bool value, object? parameter, CultureInfo? culture)
    {
        return value;
    }
}