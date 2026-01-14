// WinFormsWindowWrap.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2026 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

/// <summary>
/// Wraps a Windows Forms <see cref="Form"/> to provide additional
/// window management services.
/// </summary>
/// <param name="window">Form to wrap.</param>
public class WinFormsWindowWrap(Form window) : IWinFormsWindow
{
    private readonly Form _window = window;

    /// <inheritdoc/>
    public FormWindowState WindowState => _window.WindowState;

    Form IWinFormsWindow.Itself => _window;

    /// <summary>
    /// Implicitly converts a <see cref="WinFormsWindowWrap"/> to a
    /// <see cref="Form"/>.
    /// </summary>
    /// <param name="wrap">Object to convert.</param>
    public static implicit operator Form(WinFormsWindowWrap wrap)
    {
        return wrap._window;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Form"/> to a
    /// <see cref="WinFormsWindowWrap"/>.
    /// </summary>
    /// <param name="window">Object to convert.</param>
    public static implicit operator WinFormsWindowWrap(Form window)
    {
        return new WinFormsWindowWrap(window);
    }
}
