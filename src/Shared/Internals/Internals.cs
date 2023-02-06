﻿/*
Internals.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Misc;

[ExcludeFromCodeCoverage]
internal static class Internals
{
    internal static bool HasLicense(object obj)
    {
        return obj.HasAttribute<LicenseAttributeBase>();
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void NullCheck(object? o, string name)
    {
        if (o is null) throw new ArgumentNullException(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T NullChecked<T>(T o, string name)
    {
        NullCheck(o, name);
        return o;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string EmptyChecked(string str, string name)
    {
        EmptyCheck(str, name);
        return str;
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void EmptyCheck(string? str, string name)
    {
        if (NullChecked(str, name).IsEmpty()) throw Errors.InvalidValue(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T CheckDefinedEnum<T>(T value, string argName)
        where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value)) throw Errors.UndefinedEnum(argName, value);
        return value;
    }

    internal static T TamperCast<T>(object? value) where T : notnull
    {
        return value is T v ? v : throw new TamperException();
    }
}
