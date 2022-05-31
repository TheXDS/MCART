﻿/*
ErrorManager.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.ValueConverters;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Implements a valueConverter which gets validation error text for
/// properties. It requires a <see cref="INotifyDataErrorInfo"/> instance
/// for value and a <see cref="string"/> as a parameter defining the name
/// of the property to check.
/// </summary>
public class ErrorManager : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return null;
        if (value is not INotifyDataErrorInfo vm) throw new ArgumentException(null, nameof(value));
        if (parameter is not string pn) throw new ArgumentException(null, nameof(parameter));
        System.Collections.Generic.List<string>? e = vm.GetErrors(pn).OfType<string>().ToList();
        return e.Any() ? string.Join(Environment.NewLine, e) : null;
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
