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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Helpers
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
        private static void FieldsOf_Contract(IEnumerable<FieldInfo> fields, object? instance)
        {
            NullCheck(fields, nameof(fields));
            if (fields.IsAnyNull()) throw new NullItemException();
            if (instance is { } obj)
            {
                FieldInfo[]? f = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
                foreach (FieldInfo? j in fields.Where(p => !p.IsStatic))
                {
                    if (!f.Contains(j)) throw new MissingFieldException(obj.GetType().Name, j.Name);
                }
            }
        }
    }
}