/*
EnumerableExtensions_Contracts.cs

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

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Misc.Internals;

public static partial class EnumerableExtensions
{
    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void FirstOf_OfType_Contract<T>(Type? type)
    {
        NullCheck(type, nameof(type));
        if (!typeof(T).IsAssignableFrom(type)) throw new InvalidTypeException(type!);
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerNonUserCode]
    private static void Count_Contract(System.Collections.IEnumerable e)
    {
        NullCheck(e, nameof(e));
    }
}
