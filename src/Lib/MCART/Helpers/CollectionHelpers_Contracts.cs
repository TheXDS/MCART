/*
CollectionHelpers_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System.Diagnostics;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Helpers;

public static partial class CollectionHelpers
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AllEmpty_Contract(IAsyncEnumerable<string?> stringCollection)
    {
        NullCheck(stringCollection, nameof(stringCollection));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AllEmpty_Contract(IEnumerable<string?> stringCollection)
    {
        NullCheck(stringCollection, nameof(stringCollection));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void And_Contract<T>(IEnumerable<T> collection)
    {
        NullCheck(collection, nameof(collection));
        if (!collection.Any()) throw new EmptyCollectionException(collection);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AnyEmpty_Contract(IAsyncEnumerable<string?> stringCollection)
    {
        NullCheck(stringCollection, nameof(stringCollection));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AnyEmpty_Contract(IEnumerable<string?> stringCollection)
    {
        NullCheck(stringCollection, nameof(stringCollection));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void NotEmpty_Contract(IEnumerable<string?> stringCollection)
    {
        NullCheck(stringCollection, nameof(stringCollection));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Or_Contract<T>(IEnumerable<T> collection)
    {
        NullCheck(collection, nameof(collection));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ToPercent_Contract(object? collection, in double min, in double max)
    {
        NullCheck(collection, nameof(collection));
        if (!min.IsValid()) throw Errors.InvalidValue(nameof(min), min);
        if (!max.IsValid()) throw Errors.InvalidValue(nameof(max), max);
        if (min == max) throw new InvalidOperationException();
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ToPercent_Contract(object? collection, in float min, in float max)
    {
        NullCheck(collection, nameof(collection));
        if (!min.IsValid()) throw Errors.InvalidValue(nameof(min), min);
        if (!max.IsValid()) throw Errors.InvalidValue(nameof(max), max);
        if (min == max) throw new InvalidOperationException();
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ToPercent_Contract<T>(IEnumerable<T> collection)
    {
        NullCheck(collection, nameof(collection));
        if (!collection.Any()) throw new EmptyCollectionException(collection);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ToPercent_Contract<T>(object? collection, in T min, in T max) where T : struct, IEquatable<T>
    {
        NullCheck(collection, nameof(collection));
        if (min.Equals(max)) throw new InvalidOperationException();
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ToPercentDouble_Contract<T>(IEnumerable<T> collection)
    {
        NullCheck(collection, nameof(collection));
        if (!collection.Any()) throw new EmptyCollectionException(collection);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void ToPercentSingle_Contract<T>(IEnumerable<T> collection)
    {
        NullCheck(collection, nameof(collection));
        if (!collection.Any()) throw new EmptyCollectionException(collection);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void WhichAreNull_Contract(IEnumerable<object?> collection)
    {
        NullCheck(collection, nameof(collection));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Xor_Contract<T>(IEnumerable<T> collection)
    {
        NullCheck(collection, nameof(collection));
    }
}
