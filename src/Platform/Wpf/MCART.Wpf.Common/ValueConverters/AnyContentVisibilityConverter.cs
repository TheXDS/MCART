/*
AnyContentVisibilityConverter.cs

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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// For a <see cref="ContentControl"/> or a <see cref="Panel"/>,
/// returns a <see cref="Visibility.Visible"/> value if at least one
/// direct child control is visible.
/// </summary>
public sealed class AnyContentVisibilityConverter : IValueConverter
{
    /// <summary>
    /// Returns a <see cref="Visibility.Visible"/> value if at least one
    /// direct child control is visible.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Type of the target binding.</param>
    /// <param name="parameter">
    /// Converter parameter that will be used.
    /// </param>
    /// <param name="culture">
    /// Culture used for the conversion.
    /// </param>
    /// <returns>
    /// A new <see cref="Visibility.Visible"/> if at least one direct child
    /// of the control is visible, otherwise <see cref="Visibility.Collapsed"/>.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            Decorator decorator => decorator.Child?.Visibility ?? Visibility.Collapsed,
            ContentControl contentControl => (contentControl.Content as FrameworkElement)?.Visibility ??
                                             Visibility.Collapsed,
            Panel panel => panel.Children.Cast<object?>()
                .Any(j => j is FrameworkElement { Visibility: Visibility.Visible })
                ? Visibility.Visible
                : Visibility.Collapsed,
            _ => Visibility.Visible
        };
    }

    /// <summary>
    /// Implements <see cref="IMultiValueConverter.ConvertBack"/>.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    /// <param name="targetType">Type of the target binding.</param>
    /// <param name="parameter">
    /// Converter parameter that will be used.
    /// </param>
    /// <param name="culture">
    /// Culture used for the conversion.
    /// </param>
    /// <returns>
    /// This method always throws an <see cref="InvalidOperationException"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// This method always throws this exception when called.
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
