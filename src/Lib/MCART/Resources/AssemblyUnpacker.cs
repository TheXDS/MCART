/*
AssemblyUnpacker.cs

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

using System.Reflection;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Base class for defining an <see cref="IUnpacker{T}"/> that extracts
/// embedded resources from an <see cref="Assembly"/>.
/// </summary>
/// <typeparam name="T">Type of resources to extract.</typeparam>
/// <param name="assembly">
/// <see cref="Assembly"/> from which embedded resources will be
/// extracted.
/// </param>
/// <param name="path">
/// Path (in namespace format) where the embedded resources will be
/// located.
/// </param>
public abstract partial class AssemblyUnpacker<T>(Assembly assembly, string path) : IUnpacker<T>
{
    private readonly string _path = path ?? throw new ArgumentNullException(nameof(path));
    private readonly Assembly _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

    /// <summary>
    /// Gets a <see cref="Stream"/> from which to read an embedded resource.
    /// </summary>
    /// <param name="id">Identifier of the embedded resource.</param>
    /// <returns>
    /// A <see cref="Stream"/> from which to read an embedded resource.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="id"/> is an empty string or null.
    /// </exception>
    protected Stream? UnpackStream(string id)
    {
        UnpackStream_Contract(id);
        return _assembly.GetManifestResourceStream($"{_path}.{id}");
    }

    /// <summary>
    /// Gets a <see cref="Stream"/> from which to extract a compressed
    /// embedded resource.
    /// </summary>
    /// <param name="id">Identifier of the embedded resource.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> to use to extract the resource,
    /// or null to not use a decompression extractor.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which to read an uncompressed
    /// embedded resource.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="id"/> is an empty string or null.
    /// </exception>
    /// <exception cref="MissingResourceException">
    /// Thrown if an embedded resource with the specified ID is not found
    /// in the path defined for this resource extractor.
    /// </exception>
    protected Stream UnpackStream(string id, ICompressorGetter? compressor)
    {
        UnpackStream_Contract(id);
        ICompressorGetter? c = compressor ?? new NullCompressorGetter();
        return c.GetCompressor(_assembly?.GetManifestResourceStream($"{_path}.{id}{c.Extension}") ?? throw new MissingResourceException(id));
    }

    /// <summary>
    /// Gets an identifiable resource.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <returns>A resource of type <typeparamref name="T"/>.</returns>
    public abstract T Unpack(string id);

    /// <summary>
    /// Extracts a compressed resource using the specified compressor.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> to use to extract the resource.
    /// </param>
    /// <returns>
    /// An uncompressed resource of type <typeparamref name="T"/>.
    /// </returns>
    public abstract T Unpack(string id, ICompressorGetter compressor);

    /// <summary>
    /// Attempts to get an identifiable resource.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <param name="result">
    /// Output parameter. A resource of type
    /// <typeparamref name="T"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the resource was successfully
    /// extracted, <see langword="false"/> otherwise.
    /// </returns>
    public virtual bool TryUnpack(string id, out T result)
    {
        return AssemblyUnpacker<T>.InternalTryUnpack(() => Unpack(id), out result);
    }

    /// <summary>
    /// Attempts to get an identifiable resource.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <param name="result">
    /// Output parameter. A resource of type
    /// <typeparamref name="T"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the resource was successfully
    /// extracted, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> to use to extract the resource.
    /// </param>
    public virtual bool TryUnpack(string id, ICompressorGetter compressor, out T result)
    {
        return AssemblyUnpacker<T>.InternalTryUnpack(() => Unpack(id, compressor), out result);
    }

    private static bool InternalTryUnpack(Func<T> function, out T result)
    {
        try
        {
            result = function();
            return true;
        }
        catch
        {
            result = default!;
            return false;
        }
    }
}
