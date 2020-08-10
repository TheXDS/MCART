/*
Internals.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Misc
{
    internal static class Internals
    {
        internal static bool HasLicense(object obj)
        {
            return obj.HasAttr<LicenseTextAttribute>()
                   || obj.HasAttr<EmbeddedLicenseAttribute>()
                   || obj.HasAttr<LicenseUriAttribute>();
        }

        internal static MethodBase? GetCallOutsideMcart(bool @throw = true)
        {
            MethodBase? m;
            var c = 1;
            do
            {
                m = ReflectionHelpers.GetCallingMethod(++c);
                if (m is null)
                {
                    if (@throw) throw new Exceptions.StackUnderflowException();
                    else break;
                }
            } while (m!.DeclaringType!.Assembly.HasAttr<McartComponentAttribute>());

            return m;
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void NullCheck(object? o, string name)
        {
            if (o is null) throw new System.ArgumentNullException(name);
        }
    }
}
