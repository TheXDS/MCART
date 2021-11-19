/*
MethodInfoExtensions_Contracts.cs

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

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Types.Extensions
{
    public static partial class MethodInfoExtensions
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ToDelegate_Contract(MethodInfo m, object? targetInstance)
        {
            NullCheck(m, nameof(m));
            if ((targetInstance is null && !m.IsStatic) || (targetInstance is not null && m.IsStatic)) throw new MemberAccessException();
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void IsVoid_Contract(MethodInfo m)
        {
            NullCheck(m, nameof(m));
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void IsOverride_Contract(MethodInfo method)
        {
            NullCheck(method, nameof(method));
        }
    }
}