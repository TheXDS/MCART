/*
CollectionHelpers_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Math;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Helpers
{
    public static partial class CollectionHelpers
    {
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
        private static void ToPercent_Contract<T>(object? collection, in T min, in T max) where T : struct, IEquatable<T>
        {
            NullCheck(collection, nameof(collection));
            if (min.Equals(max)) throw new InvalidOperationException();
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
        private static void And_Contract<T>(IEnumerable<T> collection)
        {
            NullCheck(collection, nameof(collection));
            if (!collection.Any()) throw new EmptyCollectionException(collection);
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void Xor_Contract<T>(IEnumerable<T> collection)
        {
            NullCheck(collection, nameof(collection));
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
        private static void AllEmpty_Contract(IAsyncEnumerable<string?> stringCollection)
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
        private static void AnyEmpty_Contract(IAsyncEnumerable<string?> stringCollection)
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
        private static void ToPercent_Contract<T>(IEnumerable<T> collection)
        {
            NullCheck(collection, nameof(collection));
            if (!collection.Any()) throw new EmptyCollectionException(collection);
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
    }
}