/*
StringExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Text;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones de la clase <see cref="StringBuilder"/>.
/// </summary>
public static class StringBuilderFluentExtensions
{
    /// <summary>
    /// Concatena el texto especificado seguido del terminador de línea
    /// predeterminado al final de esta instancia solamente si la
    /// cadena no es <see langword="null"/>.
    /// </summary>
    /// <param name="sb">
    /// Instancia de <see cref="StringBuilder"/> sobre la cual realizar
    /// la operación.
    /// </param>
    /// <param name="text">
    /// Texto a concatenar. Si es <see langword="null"/>, no se
    /// realizará ninguna acción.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="sb"/>.
    /// </returns>
    public static StringBuilder AppendLineIfNotNull(this StringBuilder sb, string? text)
    {
        if (text is { }) sb.AppendLine(text);
        return sb;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="text"></param>
    /// <param name="width"></param>
    /// <returns></returns>
    public static StringBuilder AppendAndWrap(this StringBuilder sb, string? text, int width)
    {
        sb.Append(string.Join(Environment.NewLine, text?.TextWrap(width).NotEmpty()!));
        return sb;
    }
}
