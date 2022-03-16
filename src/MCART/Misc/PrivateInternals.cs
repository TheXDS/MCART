/*
PrivateInternals.cs

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Factory;

[ExcludeFromCodeCoverage]
internal static class PrivateInternals
{
    public static IEnumerable<NamedObject<TField>> List<TField>(Type source)
    {
        return List<TField>(source, BindingFlags.Static | BindingFlags.Public, null);
    }

    public static IEnumerable<NamedObject<TField>> List<TField>(Type source, object instance)
    {
        return List<TField>(source, BindingFlags.Public, instance);
    }

    public static IEnumerable<Type> SafeGetTypes(this Assembly asm)
    {
        try
        {
            return asm.GetTypes();
        }
        catch
        {
            return Type.EmptyTypes;
        }
    }

    public static bool TryParseValues<TValue, TResult>(string[] separators, string value, in byte items, Func<TValue[], TResult> instancer, out TResult result)
    {
#if EnforceContracts && DEBUG
        if (separators is null || !separators.Any())
            throw new ArgumentNullException(nameof(separators));
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value));
        if (instancer is null)
            throw new ArgumentNullException(nameof(instancer));
        //if (items > 2)
        //    throw new ArgumentOutOfRangeException(nameof(items));
#endif
        foreach (TypeConverter t in Common.FindConverters(typeof(string), typeof(TValue)))
        {
            foreach (string? j in separators)
            {
                string[]? l = value.Split(new[] { j }, StringSplitOptions.RemoveEmptyEntries);
                if (l.Length != items) continue;
                try
                {
                    int c = 0;
                    result = instancer(l.Select(k => (TValue)t.ConvertTo(l[c++].Trim(), typeof(TValue))!).ToArray());
                    return true;
                }
                catch
                {
                    break;
                }
            }
        }
        result = default!;
        return false;
    }

    private static IEnumerable<NamedObject<TField>> List<TField>(IReflect source, BindingFlags flags, object? instance)
    {
        return source.GetFields(flags).Where(f => f.FieldType.Implements<TField>())
            .Select(p => new NamedObject<TField>((TField)p.GetValue(instance)!));
    }
}
