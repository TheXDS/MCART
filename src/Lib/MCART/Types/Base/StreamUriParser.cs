/*
StreamUriParser.cs

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

using TheXDS.MCART.Attributes;
using TheXDS.MCART.IO;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for an object that allows creating a <see cref="Stream"/>
/// from a <see cref="Uri"/>.
/// </summary>
public abstract class StreamUriParser : IStreamUriParser
{
    private static StreamUriParser[]? _knownParsers;

    /// <summary>
    /// Gets the appropriate <see cref="StreamUriParser"/> to handle the
    /// specified <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> from which to create a <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// A <see cref="StreamUriParser"/> that can create a <see cref="Stream"/>
    /// from the specified <see cref="Uri"/>, or <see langword="null"/> if no
    /// <see cref="StreamUriParser"/> is capable of handling the
    /// <see cref="Uri"/>.
    /// </returns>
    public static StreamUriParser? Infer(Uri uri)
    {
        return Infer<StreamUriParser>(uri);
    }

    /// <summary>
    /// Gets the appropriate <see cref="StreamUriParser"/> to handle the
    /// specified <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> from which to create a <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// A <see cref="StreamUriParser"/> that can create a <see cref="Stream"/>
    /// from the specified <see cref="Uri"/>, or <see langword="null"/> if no
    /// <see cref="StreamUriParser"/> is capable of handling the
    /// <see cref="Uri"/>.
    /// </returns>
    public static StreamUriParser? Infer(string uri)
    {
        return Infer(new Uri(uri));
    }

    /// <summary>
    /// Directly opens a <see cref="Stream"/> from which to read the
    /// content of the <paramref name="uri"/>.
    /// </summary>
    /// <param name="uri">Identifier of the resource to locate.</param>
    /// <returns>
    /// A <see cref="Stream"/> from which to read the content of the
    /// <paramref name="uri"/>.
    /// </returns>
    [Sugar]
    public static Stream? Get(Uri uri)
    {
        return Infer(uri)?.GetStream(uri);
    }

    /// <summary>
    /// Directly opens a <see cref="Stream"/> asynchronously from which to read the
    /// content of the <paramref name="uri"/>.
    /// </summary>
    /// <param name="uri">Identifier of the resource to locate.</param>
    /// <returns>
    /// A <see cref="Stream"/> from which to read the content of the
    /// <paramref name="uri"/>.
    /// </returns>
    [Sugar]
    public static Task<Stream?> GetAsync(Uri uri)
    {
        return Infer(uri)?.GetStreamAsync(uri) ?? Task.FromResult<Stream?>(null);
    }

    /// <summary>
    /// Gets the appropriate <see cref="StreamUriParser"/> to handle the
    /// specified <see cref="Uri"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of specialized <see cref="StreamUriParser"/> to obtain.
    /// </typeparam>
    /// <param name="uri">
    /// <see cref="Uri"/> from which to create a <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// A <see cref="StreamUriParser"/> that can create a <see cref="Stream"/>
    /// from the specified <see cref="Uri"/>, or <see langword="null"/> if no
    /// <see cref="StreamUriParser"/> is capable of handling the
    /// <see cref="Uri"/>.
    /// </returns>
    public static T? Infer<T>(Uri uri) where T : class, IStreamUriParser
    {
        return (_knownParsers ??= [new FileStreamUriParser(), new HttpStreamUriParser()]).OfType<T>().FirstOrDefault(p => p.Handles(uri));
    }

    /// <summary>
    /// Gets the appropriate <see cref="StreamUriParser"/> to handle the
    /// specified <see cref="Uri"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of specialized <see cref="StreamUriParser"/> to obtain.
    /// </typeparam>
    /// <param name="uri">
    /// <see cref="Uri"/> from which to create a <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// A <see cref="StreamUriParser"/> that can create a <see cref="Stream"/>
    /// from the specified <see cref="Uri"/>, or <see langword="null"/> if no
    /// <see cref="StreamUriParser"/> is capable of handling the
    /// <see cref="Uri"/>.
    /// </returns>
    public static T? Infer<T>(string uri) where T : class, IStreamUriParser
    {
        return Infer<T>(new Uri(uri));
    }

    /// <summary>
    /// Gets a value that determines if the <see cref="Stream"/>
    /// produced by this object requires to be loaded completely into an
    /// in-memory read buffer.
    /// </summary>
    public virtual bool PreferFullTransfer { get; } = false;

    /// <summary>
    /// Gets a <see cref="Stream"/> that links to the requested resource,
    /// selecting the most appropriate method to obtain that data stream.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    public Stream? GetStream(Uri uri)
    {
        return PreferFullTransfer ? OpenFullTransfer(uri) : Open(uri);
    }

    /// <summary>
    /// Gets a <see cref="Stream"/> that links to the requested resource,
    /// selecting the most appropriate method to obtain that data stream.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    public async Task<Stream?> GetStreamAsync(Uri uri)
    {
        return PreferFullTransfer ? (await OpenFullTransferAsync(uri)) : Open(uri);
    }

    /// <summary>
    /// Determines if this <see cref="StreamUriParser"/> can create a
    /// <see cref="Stream"/> from the specified <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this <see cref="StreamUriParser"/>
    /// can create a <see cref="Stream"/> from the specified
    /// <see cref="Uri"/>, <see langword="false"/> otherwise.
    /// </returns>
    public abstract bool Handles(Uri uri);

    /// <summary>
    /// Opens a <see cref="Stream"/> from the specified <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    public abstract Stream? Open(Uri uri);

    /// <summary>
    /// Opens the <see cref="Stream"/> from the specified <see cref="Uri"/>,
    /// and loads it completely into a new intermediate <see cref="Stream"/>
    /// before returning it.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    public virtual Stream? OpenFullTransfer(Uri uri)
    {
        Stream? j = Open(uri);
        if (j is null) return null;
        MemoryStream? ms = new();
        using (j)
        {
            j.CopyTo(ms);
            return ms;
        }
    }

    /// <summary>
    /// Opens the <see cref="Stream"/> from the specified <see cref="Uri"/>,
    /// and loads it completely into a new intermediate <see cref="Stream"/>
    /// before returning it.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> to open for reading.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the resource pointed to by the
    /// <see cref="Uri"/> specified can be read.
    /// </returns>
    public virtual async Task<Stream?> OpenFullTransferAsync(Uri uri)
    {
        Stream? j = Open(uri);
        if (j is null) return null;
        MemoryStream? ms = new();
        await using (j)
        {
            await j.CopyToAsync(ms);
            return ms;
        }
    }
}
