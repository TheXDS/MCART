/*
TextUnpacker.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Resources;

/// <summary>
/// <see cref="AssemblyUnpacker{T}" /> that extracts text resources.
/// </summary>
/// <param name="assembly">
/// The <see cref="Assembly"/> from which the embedded resources
/// will be extracted.
/// </param>
/// <param name="path">
/// The path (in namespace format) where the embedded resources
/// will be located.
/// </param>
[RequiresUnreferencedCode(AttributeErrorMessages.ClassScansForTypes)]
[RequiresDynamicCode(AttributeErrorMessages.ClassCallsDynamicCode)]
public class TextUnpacker(Assembly assembly, string path) : AssemblyUnpacker<string>(assembly, path)
{
    /// <summary>
    /// Gets a resource identifiable by its ID.
    /// </summary>
    /// <param name="id">The identifier of the resource.</param>
    /// <returns>
    /// A string with the total content of the resource.
    /// </returns>
    public override string Unpack(string id)
    {
        using StreamReader? sr = new(UnpackStream(id) ?? throw new MissingResourceException(id));
        return sr.ReadToEnd();
    }

    /// <summary>
    /// Extracts a compressed resource using the compressor with the
    /// specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the resource.</param>
    /// <param name="compressor">
    /// The <see cref="ICompressorGetter"/> to use for extracting
    /// the resource.
    /// </param>
    /// <returns>
    /// An uncompressed resource as a string.
    /// </returns>
    public override string Unpack(string id, ICompressorGetter compressor)
    {
        using StreamReader? sr = new(UnpackStream(id, compressor));
        return sr.ReadToEnd();
    }
}
