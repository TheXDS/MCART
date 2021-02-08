/*
EnumValueProvider_Contracts.cs

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
using System.Runtime.CompilerServices;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Misc.Internals;


namespace TheXDS.MCART.Wpf.Component
{
    public partial class EnumValueProvider
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void EnumValueProvider_Contract(Type enumType)
        {
            NullCheck(enumType, nameof(enumType));
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("A type that inherits from System.Enum was expected.", new InvalidTypeException(enumType));
            }
        }
    }
}