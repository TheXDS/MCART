/*
IUnpacker.cs

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

namespace TheXDS.MCART.Resources;

/// <summary>
/// Defines a series of methods to be implemented by a class that allows
/// getting and extracting resources.
/// </summary>
/// <typeparam name="T">Type of resources to get.</typeparam>
public interface IUnpacker<T>
{
    /// <summary>
    /// Gets an identifiable resource.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <returns>A resource of type <typeparamref name="T"/>.</returns>
    T Unpack(string id);

    /// <summary>
    /// Extracts a compressed resource using the compressor with the
    /// specified identifier.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> to use for extracting the
    /// resource.
    /// </param>
    /// <returns>
    /// An uncompressed resource of type <typeparamref name="T"/>.</returns>
    T Unpack(string id, ICompressorGetter compressor);

    /// <summary>
    /// Tries to get an identifiable resource.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <param name="result">
    /// Out parameter. A resource of type
    /// <typeparamref name="T"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the resource was successfully extracted,
    /// <see langword="false"/> otherwise.
    /// </returns>
    bool TryUnpack(string id, out T result);

    /// <summary>
    /// Tries to get an identifiable resource.
    /// </summary>
    /// <param name="id">Identifier of the resource.</param>
    /// <param name="result">
    /// Out parameter. A resource of type
    /// <typeparamref name="T"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the resource was successfully extracted,
    /// <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> to use for extracting the
    /// resource.
    /// </param>
    bool TryUnpack(string id, ICompressorGetter compressor, out T result);
}
