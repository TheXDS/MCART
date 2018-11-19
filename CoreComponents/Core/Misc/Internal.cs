/*
Internal.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Misc
{
    internal static class Internal
    {
        public static string ReadLicense(object asm, bool returnNull = true)
        {
            try
            {
                return asm.GetAttr<LicenseTextAttribute>()
                           ?.Value.OrNull() ??
                       asm.GetAttr<EmbeddedLicenseAttribute>()
                           ?.ReadLicense((asm as Assembly ?? (asm as Type)?.Assembly) ??
                                         throw new InvalidOperationException())
                           .OrNull() ??
                       asm.GetAttr<LicenseFileAttribute>()
                           ?.ReadLicense()
                           .OrNull() ??
                       (returnNull
                           ? null
                           : St.Warn(St.UnspecLicense));
            }
            catch (Exception e)
            {
                return returnNull 
                    ? null 
                    : $"{e.Message}\n-------------------------\n{e.StackTrace}";
            }
        }

        public static bool HasLicense(object obj)
        {
            return obj.HasAttr<LicenseTextAttribute>()
                   || obj.HasAttr<EmbeddedLicenseAttribute>()
                   || obj.HasAttr<LicenseFileAttribute>();
        }

        public static IEnumerable<KeyValuePair<string, TField>> List<TField>(Type source, object instance)
        {
            return source.GetFields(BindingFlags.Public)
                .Where(f => f.FieldType.Implements<TField>())
                .Select(p => new KeyValuePair<string, TField>(p.NameOf(), (TField)p.GetValue(instance)));
        }

        public static IEnumerable<KeyValuePair<string, TField>> List<TField>(Type source)
        {
            return source.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => f.FieldType.Implements<TField>())
                .Select(p => new KeyValuePair<string, TField>(p.NameOf(), (TField)p.GetValue(null)));
        }

    }
}