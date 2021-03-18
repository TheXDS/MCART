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
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Math;
using static TheXDS.MCART.Misc.Internals;
using St = TheXDS.MCART.Resources.Strings;

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
            if (!min.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, min.ToString(CultureInfo.CurrentCulture))), nameof(min));
            if (!max.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, max.ToString(CultureInfo.CurrentCulture))), nameof(max));
            if (min == max) throw new InvalidOperationException();
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ToPercent_Contract(object? collection, in double min, in double max)
        {
            NullCheck(collection, nameof(collection));
            if (!min.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, min.ToString(CultureInfo.CurrentCulture))), nameof(min));
            if (!max.IsValid())
                throw new ArgumentException(
                    St.XIsInvalid(St.XYQuotes(St.TheValue, max.ToString(CultureInfo.CurrentCulture))), nameof(max));
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
    }
}