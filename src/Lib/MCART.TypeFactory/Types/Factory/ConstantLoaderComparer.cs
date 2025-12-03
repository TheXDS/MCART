/*
ConstantLoaderComparer.cs

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

using TheXDS.MCART.Types.Extensions.ConstantLoaders;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Allows comparing equality between two
/// <see cref="IConstantLoader"/> based on the type of constant that
/// both can load.
/// </summary>
public class ConstantLoaderComparer : IEqualityComparer<IConstantLoader>
{
    /// <summary>
    /// Compares two <see cref="IConstantLoader"/> instances.
    /// </summary>
    /// <param name="x">
    /// First object to compare.
    /// </param>
    /// <param name="y">
    /// Second object to compare.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both objects load constants of the same
    /// type, <see langword="false"/> otherwise.
    /// </returns>
    public bool Equals(IConstantLoader? x, IConstantLoader? y)
    {
        return x?.ConstantType.Equals(y?.ConstantType) ?? y is null;
    }

    /// <summary>
    /// Gets the hash code of an <see cref="IConstantLoader"/> instance
    /// that can be used to compare the type of constant that the object
    /// is capable of loading.
    /// </summary>
    /// <param name="obj">
    /// Object from which to obtain the hash code.
    /// </param>
    /// <returns>
    /// The hash code of the constant type that the object can load.
    /// </returns>
    public int GetHashCode(IConstantLoader obj)
    {
        return obj.ConstantType.GetHashCode();
    }
}
