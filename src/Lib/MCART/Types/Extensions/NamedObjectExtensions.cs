/*
NamedObjectExtensions.cs

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Miscellaneous functions and extensions for all elements of
/// the <see cref="NamedObject{T}"/> type.
/// </summary>
public static partial class NamedObjectExtensions
{
    /// <summary>
    /// Enumerates all enumeration values of the specified type as a
    /// <see cref="NamedObject{T}"/> .
    /// </summary>
    /// <typeparam name="T">Type of enumeration to convert.</typeparam>
    /// <returns>
    /// An enumeration of <see cref="NamedObject{T}"/> from the enumeration
    /// values of the specified type.
    /// </returns>
    [Sugar]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static IEnumerable<NamedObject<T>> AsNamedObject<T>() where T : struct, Enum
    {
        return NamedObject.FromEnum<T>();
    }

    /// <summary>
    /// Enumerates all enumeration values of the specified type as a
    /// <see cref="NamedObject{T}"/> .
    /// </summary>
    /// <param name="t">Type of enumeration to convert.</param>
    /// <returns>
    /// An enumeration of <see cref="NamedObject{T}"/> from the enumeration
    /// values of the specified type.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static IEnumerable<NamedObject<Enum>> AsNamedEnum(this Type t)
    {
        Type? q = AsNamedEnum_Contract(t);
        return q.GetEnumValues().OfType<Enum>().Select(p => new NamedObject<Enum>(p.NameOf(), p));
    }
}
