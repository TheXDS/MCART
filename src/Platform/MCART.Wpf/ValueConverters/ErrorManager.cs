/*
ErrorManager.cs

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
