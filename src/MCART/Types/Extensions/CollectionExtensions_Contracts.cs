/*
CollectionExtensions_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Types.Factory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static TheXDS.MCART.Misc.Internals;

public static partial class CollectionExtensions
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AddClone_Contract<T>(ICollection<T> collection, T item) where T : ICloneable
    {
        NullCheck(collection, nameof(collection));
        NullCheck(item, nameof(item));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AddClones_Contract<T>(this ICollection<T> collection, IEnumerable<T> source)
    {
        NullCheck(collection, nameof(collection));
        NullCheck(source, nameof(source));
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void AddRange_Contract<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        NullCheck(collection, nameof(collection));
        NullCheck(items, nameof(items));
    }

    private static void ToObservable_Contract<T>(this ICollection<T> collection)
    {
        NullCheck(collection, nameof(collection));
    }

    private static void Push_Contract<TCollection>(this ICollection<TCollection> collection)
    {
        NullCheck(collection, nameof(collection));
    }
}
