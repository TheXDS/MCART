/*
MemberBuildInfo.cs

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

using System.Reflection;
using System.Reflection.Emit;

namespace TheXDS.MCART.Types;

/// <summary>
/// Base class containing shared information for building a class member.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MemberBuildInfo{T}"/> class.
/// </remarks>
/// <param name="typeBuilder">
/// TypeBuilder in which this member was defined.
/// </param>
/// <param name="member">
/// Reference to the member that has been defined.
/// </param>
public abstract class MemberBuildInfo<T>(TypeBuilder typeBuilder, T member) where T : MemberInfo
{
    /// <summary>
    /// Reference to the TypeBuilder where this member was defined.
    /// </summary>
    public TypeBuilder TypeBuilder { get; } = typeBuilder;

    /// <summary>
    /// Reference to the MemberInfo used to build this member.
    /// </summary>
    public T Member { get; } = member;

    /// <summary>
    /// Implicitly converts a <see cref="MemberBuildInfo{T}"/> to its
    /// underlying member.
    /// </summary>
    /// <param name="buildInfo">
    /// <see cref="MemberBuildInfo{T}"/> from which to extract the member.
    /// </param>
    public static implicit operator T(MemberBuildInfo<T> buildInfo) => buildInfo.Member;
}
