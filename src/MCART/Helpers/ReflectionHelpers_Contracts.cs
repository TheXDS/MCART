/*
ReflectionHelpers_Contracts.cs

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
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART
{
    /// <summary>
    /// Funciones auxiliares de reflexión.
    /// </summary>
    public static partial class ReflectionHelpers
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void GetCallingMethod_Contract(int nCaller)
        {
            if (checked(nCaller++) < 1) throw new ArgumentOutOfRangeException(nameof(nCaller));
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void IsOverriden_Contract(MethodBase method, object thisInstance)
        {
            if (method?.DeclaringType is null) throw new ArgumentNullException(nameof(method));
            if (!(thisInstance?.GetType() ?? throw new ArgumentNullException(nameof(thisInstance))).Implements(method.DeclaringType)) throw new InvalidTypeException(thisInstance.GetType());
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