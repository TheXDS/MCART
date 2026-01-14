/*
WpfIcons.cs

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

using Ers = TheXDS.MCART.Wpf.Resources.Strings.WpfErrors;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Contains members that create exceptions that may occur within MCART.WPF.
/// </summary>
public static class WpfErrors
{
    /// <summary>
    /// Gets an error that occurs when an Id corresponding to a requested
    /// resource is not found.
    /// </summary>
    /// <param name="id">Id of the resource that was not found.</param>
    /// <param name="argName">Name of the argument that failed.</param>
    /// <returns>
    /// A new instance of the <see cref="ArgumentException"/> class.
    /// </returns>
    public static Exception ResourceNotFound(string id, string argName)
    {
        return new ArgumentException(string.Format(Ers.ResourceNotFound, id), argName);
    }
}
