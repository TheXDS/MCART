/*
InRange.cs

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

using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using TheXDS.MCART.Resources.Strings;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.ValidationRules;

/// <summary>
/// Checks that a value is within a specified range of values.
/// </summary>
/// <typeparam name="T">Type of value to check.</typeparam>
[DefaultProperty(nameof(Range))]
public class InRange<T> : ValidationRule where T : IComparable<T>
{
    /// <summary>
    /// Range of allowed values.
    /// </summary>
    public Range<T> Range { get; set; }

    /// <summary>
    /// If overridden in a derived class, performs validation checks on a value.
    /// </summary>
    /// <param name="value">
    /// Value of the binding target that will be checked.
    /// </param>
    /// <param name="cultureInfo">
    /// Culture information used by this rule.
    /// </param>
    /// <returns>
    /// ValidationResult instance.
    /// </returns>
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is not T v) return new ValidationResult(false, Errors.InvalidValue);
        return new ValidationResult(Range.IsWithin(v), string.Format(Errors.ValueMustBeBetweenXandY, Range.Minimum, Range.Maximum));
    }
}
