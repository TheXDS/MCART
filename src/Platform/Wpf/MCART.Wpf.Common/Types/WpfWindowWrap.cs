/*
WpfWindowWrap.cs

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
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

/// <summary>
/// Wraps a Windows Presentation Framework <see cref="Window"/> to provide
/// additional window‑management services.
/// </summary>
/// <param name="window">Window to wrap.</param>
public class WpfWindowWrap(Window window) : IWpfWindow
{
    private readonly Window _window = window;

    /// <inheritdoc/>
    public WindowState WindowState => _window.WindowState;

    Window IWpfWindow.Itself => _window;

    /// <summary>
    /// Implicitly converts a <see cref="WpfWindowWrap"/> to a <see cref="Window"/>.
    /// </summary>
    /// <param name="wrap">Object to convert.</param>
    public static implicit operator Window(WpfWindowWrap wrap)
    {
        return wrap._window;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Window"/> to a <see cref="WpfWindowWrap"/>.
    /// </summary>
    /// <param name="window">Object to convert.</param>
    public static implicit operator WpfWindowWrap(Window window)
    {
        return new WpfWindowWrap(window);
    }
}
