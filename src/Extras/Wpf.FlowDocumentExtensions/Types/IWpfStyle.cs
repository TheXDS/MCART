/*
IWpfStyle.cs

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

using System.Windows;
using System.Windows.Media;

namespace TheXDS.MCART.Types;

/// <summary>
/// Defines a series of members to be implemented by a class that
/// describes styles for WPF elements.
/// </summary>
public interface IWpfStyle
{
    /// <summary>
    /// Gets or sets a <see cref="Brush"/> to be used for
    /// filling the background of a WPF element.
    /// </summary>
    Brush? Background { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> to be used for
    /// filling the foreground of a WPF element.
    /// </summary>
    Brush? Foreground { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="Brush"/> to be used for
    /// drawing the borders of a WPF element.
    /// </summary>
    Brush? BorderBrush { get; set; }

    /// <summary>
    /// Gets or sets the thickness of the borders to be drawn on the
    /// WPF element.
    /// </summary>
    Thickness? BorderThickness { get; set; }
}
