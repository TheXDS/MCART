/*
PrivateInternals.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Misc
{
    internal static class PrivateInternals
    {
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

        public static IEnumerable<NamedObject<TField>> List<TField>(Type source, object instance)
        {
            return List<TField>(source, BindingFlags.Public, instance);
        }

        public static IEnumerable<NamedObject<TField>> List<TField>(Type source)
        {
            return List<TField>(source, BindingFlags.Static | BindingFlags.Public, null);
        }

        private static IEnumerable<NamedObject<TField>> List<TField>(IReflect source, BindingFlags flags, object? instance)
        {
            return source.GetFields(flags).Where(f => f.FieldType.Implements<TField>())
                .Select(p => new NamedObject<TField>((TField)p.GetValue(instance)!));
        }

        public static bool TryParseValues<TValue, TResult>(string[] separators, string value, in byte items, Func<TValue[], TResult> instancer, out TResult result)
        {
#if DEBUG
            if (separators is null || !separators.Any())
                throw new ArgumentNullException(nameof(separators));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            if (instancer is null)
                throw new ArgumentNullException(nameof(instancer));
            if (items > 2)
                throw new ArgumentOutOfRangeException(nameof(items));
#endif

            var t = Common.FindConverter<TValue>();
            if (!(t is null))
            {
                foreach (var j in separators)
                {
                    var l = value.Split(new[] { j }, StringSplitOptions.RemoveEmptyEntries);
                    if (l.Length != items) continue;
                    try
                    {
                        var n = new List<TValue>();
                        var c = 0;
                        foreach (var k in l)
                        {
                            n.Add((TValue)t.ConvertTo(l[c++].Trim(), typeof(TValue)));
                        }
                        result = instancer(n.ToArray());
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
    }
}