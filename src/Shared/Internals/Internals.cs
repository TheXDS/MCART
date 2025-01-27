/*
Internals.cs

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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using Err = TheXDS.MCART.Resources.Errors;

namespace TheXDS.MCART.Misc;

[ExcludeFromCodeCoverage]
internal static class Internals
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void CheckPopulatedCollection(IEnumerable<object?> collection, [CallerArgumentExpression(nameof(collection))] string name = null!)
    {
        ArgumentNullException.ThrowIfNull(collection, name);
        if (!collection.Any()) throw Err.EmptyCollection(collection);
        if (collection.IsAnyNull(out int index)) throw Err.NullItem(index);
    }
    
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void CheckPopulatedCollection<T>(IEnumerable<T> collection, [CallerArgumentExpression(nameof(collection))] string name = null!)
    {
        ArgumentNullException.ThrowIfNull(collection, name);
        if (!collection.Any()) throw Err.EmptyCollection(collection);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T NullChecked<T>(T o, [CallerArgumentExpression(nameof(o))] string name = null!)
    {
        ArgumentNullException.ThrowIfNull(o, name);
        return o;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string EmptyChecked(string str, [CallerArgumentExpression(nameof(str))] string name = null!)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(str, name);
        return str;
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void EmptyCheck(string? str, [CallerArgumentExpression(nameof(str))] string name = null!)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(str, name);        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T CheckDefinedEnum<T>(T value, [CallerArgumentExpression(nameof(value))] string argName = null!)
        where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value)) throw Err.UndefinedEnum(argName, value);
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T TamperCast<T>(object? value) where T : notnull
    {
        return value is T v ? v : throw new TamperException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T RangeChecked<T>(T value, T min, T max, [CallerArgumentExpression(nameof(value))] string argName = null!) where T : IComparable<T>
    {
        RangeCheck(value, min, max, argName);
        return value;
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void RangeCheck<T>(T value, T min, T max, [CallerArgumentExpression(nameof(value))] string argName = null!) where T : IComparable<T>
    {
        if (!value.IsBetween(min, max)) throw Err.ValueOutOfRange(argName, min, max);
    }
}
