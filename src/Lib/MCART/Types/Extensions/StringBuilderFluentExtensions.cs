/*
StringExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Text;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for the <see cref="StringBuilder"/> class.
/// </summary>
public static class StringBuilderFluentExtensions
{
    /// <summary>
    /// Appends the specified text followed by the default line terminator to the end of this instance only if the string is not <see langword="null"/>.
    /// </summary>
    /// <param name="sb">
    /// Instance of <see cref="StringBuilder"/> to perform the operation on.
    /// </param>
    /// <param name="text">
    /// Text to append. If it's <see langword="null"/>, no action will be taken.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="sb"/>.
    /// </returns>
    public static StringBuilder AppendLineIfNotNull(this StringBuilder sb, string? text)
    {
        if (text is { }) sb.AppendLine(text);
        return sb;
    }

    /// <summary>
    /// Appends a string after applying an operation that wraps the string into lines of up to <paramref name="width"/> characters.
    /// </summary>
    /// <param name="sb">
    /// Instance of <see cref="StringBuilder"/> to perform the operation on.
    /// </param>
    /// <param name="text">
    /// Text to append. If it's <see langword="null"/>, no action will be taken.
    /// </param>
    /// <param name="width">
    /// Maximum length of text lines to add.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="sb"/>.
    /// </returns>
    public static StringBuilder AppendAndWrap(this StringBuilder sb, string? text, int width)
    {
        if (text is { } && string.Join(Environment.NewLine, text.TextWrap(width).NotEmpty()).OrNull() is { } txt) sb.Append(txt);
        return sb;
    }
}
