/*
CollectionHelpers_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace TheXDS.MCART
{
    public static partial class Common
    {
        [Conditional("EnforceContracts")]
        private static void AllEmpty_Contract(IEnumerable<string> stringArray)
        {
            if (stringArray is null)
            {
                throw new ArgumentNullException(nameof(stringArray));
            }
        }

        [Conditional("EnforceContracts")]
        private static void FindConverter_Contract(Type source, Type target)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
        }

    }
}