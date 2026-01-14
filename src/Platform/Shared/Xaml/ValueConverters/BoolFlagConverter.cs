/*
BoolFlagConverter.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Base class for creating boolean converters that parse flag enums.
/// </summary>
/// <typeparam name="T">
/// Type of values to convert. Must be an enumeration.
/// </typeparam>
/// <param name="trueValue">Value equivalent to <see langword="true"/>.
/// </param>
/// <exception cref="InvalidOperationException">
/// Thrown if the type specified when instantiating this class is not
/// an enumeration.
/// </exception>
public partial class BoolFlagConverter<T>(T trueValue) where T : Enum
{
    /// <summary>
    /// Gets or sets the value that corresponds to <see langword="true"/>
    /// in this <see cref="BoolFlagConverter{T}"/>.
    /// </summary>
    public T True { get; set; } = trueValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoolFlagConverter{T}"/> class.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the type specified when instantiating this class is not
    /// an enumeration.
    /// </exception>
    public BoolFlagConverter() : this((T)typeof(T).Default()!)
    {
    }

    /// <summary>
    /// Converts a value to <see cref="bool"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type of the conversion.</param>
    /// <param name="parameter">
    /// Custom parameters used for the conversion.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use during the conversion.
    /// </param>
    /// <returns>
    /// If no value has been set for <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True"/>,
    /// returns <see langword="true"/> if any flag is active, otherwise
    /// <see langword="false"/>. If a value has been set for
    /// <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True"/>, returns
    /// <see langword="true"/> only when that flag (or flags) is active,
    /// otherwise <see langword="false"/>.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is T v) return True.Equals(default(T)) ? !v.Equals(True) : v.Equals(True);
        return null;
    }

    /// <summary>
    /// Converts a <see cref="bool"/> to the type defined for this
    /// <see cref="BoolFlagConverter{T}"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Target type of the conversion.</param>
    /// <param name="parameter">
    /// Custom parameters to use for the conversion.
    /// </param>
    /// <param name="culture">
    /// <see cref="CultureInfo"/> to use during the conversion.
    /// </param>
    /// <returns>
    /// If <paramref name="value"/> is <see langword="true"/>, returns the
    /// flag(s) to be detected; if a value has been set for
    /// <see cref="P:System.Windows.Converters.BoolFlagConverter`1.True"/>, otherwise returns
    /// <c>default</c>.
    /// </returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.Equals(true) ?? false ? True : default;
    }
}
