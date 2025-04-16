/*
ProtocolFormatAttribute.cs

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

using System.Diagnostics;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Sets a protocol format to open a link through
/// the operating system.
/// </summary>
/// <param name="format">Mask to apply.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
[Serializable]
public sealed class ProtocolFormatAttribute(string format) : TextAttribute(format), IValueAttribute<string>
{
    /// <summary>
    /// Protocol call format.
    /// </summary>
    public string Format { get; } = EmptyChecked(format);

    /// <summary>
    /// Gets the value of this attribute.
    /// </summary>
    string IValueAttribute<string>.Value => Format;

    /// <summary>
    /// Opens a URL with this formatted protocol.
    /// </summary>
    /// <param name="url">
    /// URL of the resource to open using the protocol defined by
    /// this attribute.
    /// </param>
    /// <returns>
    /// An instance of the class <see cref="Process"/> that represents the
    /// operating system process that was loaded when opening the 
    /// specified <paramref name="url"/>.
    /// </returns>
    public Process? Open(string url)
    {
        return string.IsNullOrWhiteSpace(url) ? null : InvokeProcess(string.Format(Format, url));
    }

    private static Process? InvokeProcess(string url)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true,
        };
        return Process.Start(processStartInfo);
    }
}
