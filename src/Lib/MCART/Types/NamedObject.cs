﻿/*
NamedObject.cs

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

using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Defines some useful methods that can be used to create objects of type
/// <see cref="NamedObject{T}"/>.
/// </summary>
public static class NamedObject
{
    /// <summary>
    /// Enumerates all enum values as named objects.
    /// </summary>
    /// <typeparam name="T">
    /// Type of Enum to get a collection of named objects for.
    /// </typeparam>
    /// <returns>
    /// An enumeration of <see cref="NamedObject{T}"/> from the values of the
    /// specified <see cref="Enum"/> type.
    /// </returns>
    public static IEnumerable<NamedObject<T>> FromEnum<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>().Select(j => new NamedObject<T>(j.NameOf(), j));
    }
}
