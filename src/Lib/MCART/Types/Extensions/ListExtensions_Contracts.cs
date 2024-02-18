/*
ListExtensions_Contracts.cs

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

using System.Diagnostics;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions;

public static partial class ListExtensions
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Shuffle_Contract<T>(IList<T> toShuffle, in int firstIdx, in int lastIdx, in int deepness, Random random)
    {
        NullCheck(toShuffle, nameof(toShuffle));
        NullCheck(random, nameof(random));
        if (!toShuffle.Any()) throw Errors.EmptyCollection(toShuffle);
        if (!firstIdx.IsBetween(0, toShuffle.Count - 1)) throw new IndexOutOfRangeException();
        if (!lastIdx.IsBetween(0, toShuffle.Count - 1)) throw new IndexOutOfRangeException();
        if (firstIdx > lastIdx) throw Errors.MinGtMax();
        if (!deepness.IsBetween(1, lastIdx - firstIdx)) throw new ArgumentOutOfRangeException(nameof(deepness));
    }
}
