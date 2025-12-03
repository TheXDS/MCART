/*
TypeBuilderHelpers.cs

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
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.MethodAttributes;

namespace TheXDS.MCART.Types;

/// <summary>
/// Contains helper functions for building types at runtime.
/// </summary>
public static class TypeBuilderHelpers
{
    internal static string UndName(string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        return name.Length > 1
            ? $"_{name[..1].ToLower()}{name[1..]}"
            : $"_{name.ToLower()}";
    }

    internal static string NoIfaceName(string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        return name[0] != 'I' ? $"{name}Implementation" : name[1..];
    }

    /// <summary>
    /// Retrieves a method attribute based on the specified access level.
    /// </summary>
    /// <param name="access">
    /// Desired access level for the method.
    /// </param>
    /// <returns>
    /// A <see cref="MethodAttributes"/> value with the specified access
    /// level.
    /// </returns>
    public static MethodAttributes Access(MemberAccess access)
    {
        return access switch
        {
            MemberAccess.Private => Private,
            MemberAccess.Protected => Family,
            MemberAccess.Internal => MethodAttributes.Assembly,
            MemberAccess.Public => Public,
            _ => throw new NotImplementedException(),
        };
    }
}
