/*
Internals.cs

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

namespace TheXDS.MCART.Misc;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;

[ExcludeFromCodeCoverage]
internal static class Internals
{
    internal static bool HasLicense(object obj)
    {
        return obj.HasAttr<LicenseAttributeBase>();
    }

    internal static MethodBase? GetCallOutsideMcart(bool @throw = true)
    {
        MethodBase? m;
        int c = 1;
        do
        {
            m = ReflectionHelpers.GetCallingMethod(++c);
            if (m is null)
            {
                if (@throw) throw new StackUnderflowException();
                else break;
            }
        } while (m!.DeclaringType!.Assembly.HasAttr<McartComponentAttribute>());

        return m;
    }

    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void NullCheck(object? o, string name)
    {
        if (o is null) throw new ArgumentNullException(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T NullChecked<T>(T o, string name)
    {
        NullCheck(o, name);
        return o;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string EmptyChecked(string str, string name)
    {
        EmptyCheck(str, name);
        return str;
    }


    [Conditional("EnforceContracts")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void EmptyCheck(string? str, string name)
    {
        if (NullChecked(str, name).IsEmpty()) throw Errors.InvalidValue(name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T CheckDefinedEnum<T>(T value, string argName)
        where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), value)) throw Errors.UndefinedEnum(argName, value);
        return value;
    }

    internal static T TamperCast<T>(object? value) where T : notnull
    {
        return value is T v ? v : throw new TamperException();
    }
}
