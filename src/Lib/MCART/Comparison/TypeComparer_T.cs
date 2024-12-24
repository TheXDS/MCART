/*
TypeComparer_T.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

namespace TheXDS.MCART.Comparison;

/// <summary>
/// Compares the type of two objects that share a common base type.
/// </summary>
public class TypeComparer<T> : IEqualityComparer<T>
    where T : notnull
{
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the types of both objects are the same; 
    /// <see langword="false"/> otherwise.
    /// </returns>
    public bool Equals([AllowNull] T x, [AllowNull] T y)
    {
        return x?.GetType() == y?.GetType();
    }

    /// <summary>
    /// Gets the hash code for the type of the specified object.
    /// </summary>
    /// <param name="obj">
    /// The object for which to obtain a hash code.
    /// </param>
    /// <returns>
    /// The hash code for the type of the specified object.
    /// </returns>
    public int GetHashCode([DisallowNull] T obj)
    {
        return obj!.GetType().GetHashCode();
    }
}
