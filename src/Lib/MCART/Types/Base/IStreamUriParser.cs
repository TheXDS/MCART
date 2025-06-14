/*
IStreamUriParser.cs

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

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Defines a series of members to be implemented by a type that allows
/// interpreting a <see cref="Uri"/> and obtaining a <see cref="Stream"/>
/// from it.
/// </summary>
public interface IStreamUriParser
{
    /// <summary>
    /// Gets a value that determines whether the <see cref="Stream"/>
    /// produced by this object requires being fully loaded into an
    /// in-memory read buffer.
    /// </summary>
    bool PreferFullTransfer { get; }

    /// <summary>
    /// Gets a <see cref="Stream"/> that links to the requested resource,
    /// selecting the most appropriate method for obtaining said data
    /// stream.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    Stream? GetStream(Uri uri);

    /// <summary>
    /// Gets a <see cref="Stream"/> that links to the requested resource,
    /// selecting the most appropriate method for obtaining said data
    /// stream.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    Task<Stream?> GetStreamAsync(Uri uri);

    /// <summary>
    /// Determines whether this <see cref="IStreamUriParser"/> can create a
    /// <see cref="Stream"/> from the specified <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this <see cref="IStreamUriParser"/> can
    /// create a <see cref="Stream"/> from the specified <see cref="Uri"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    bool Handles(Uri uri);

    /// <summary>
    /// Opens a <see cref="Stream"/> from the <see cref="Uri"/>
    /// specified.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    Stream? Open(Uri uri);

    /// <summary>
    /// Opens the <see cref="Stream"/> from the <see cref="Uri"/>
    /// specified, and fully loads it into a new <see cref="Stream"/>
    /// intermediate before returning it.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    Stream? OpenFullTransfer(Uri uri);

    /// <summary>
    /// Opens the <see cref="Stream"/> from the <see cref="Uri"/>
    /// specified, and fully loads it into a new <see cref="Stream"/>
    /// intermediate asynchronously before returning it.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    Task<Stream?> OpenFullTransferAsync(Uri uri);
}
