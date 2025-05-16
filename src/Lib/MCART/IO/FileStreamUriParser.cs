/*
FileStreamUriParser.cs

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

using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO;

/// <summary>
/// Retrieves a <see cref="Stream"/> from a file path specified by a 
/// <see cref="Uri"/>.
/// </summary>
public class FileStreamUriParser : SimpleStreamUriParser
{
    /// <summary>
    /// Enumerates the schemes supported by this 
    /// <see cref="StreamUriParser"/>.
    /// </summary>
    protected override IEnumerable<string> SchemeList { get; } = ["file"];

    /// <summary>
    /// Opens a <see cref="Stream"/> from the specified 
    /// <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// The <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by 
    /// the specified <see cref="Uri"/> can be read.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the resource pointed to by <paramref name="uri"/> 
    /// does not exist.
    /// </exception>
    /// <exception cref="System.Security.SecurityException">
    /// Thrown if sufficient permissions are not available to perform 
    /// this operation.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown if there is an input/output issue when opening the 
    /// resource pointed to by <paramref name="uri"/>.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown if the directory specified in the path of 
    /// <paramref name="uri"/> does not exist.
    /// </exception>
    /// <exception cref="PathTooLongException">
    /// Thrown if the file path length exceeds the system's allowed 
    /// limits.
    /// </exception>
    public override Stream? Open(Uri uri)
    {
        if (uri.OriginalString.StartsWith("file://"))
        {
            uri = new Uri(uri.OriginalString.Replace("file://", "", StringComparison.OrdinalIgnoreCase));
        }
        if (!File.Exists(uri.OriginalString)) return null;
        return new FileStream(uri.OriginalString, FileMode.Open);
    }
}
