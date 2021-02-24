/*
TypeComparerDataGenerator.cs

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
using System.Collections.Generic;

namespace TheXDS.MCART.Tests.Comparison
{
    public class TypeComparerDataGenerator : ComparerDataGenerator<object>
    {
        protected override IEnumerable<(object a, object b, bool equal)> GetSequences()
        {
            yield return (new Exception("test 1"), new Exception("test 2"), true);
            yield return (1.0, 2.0, true);
            yield return (new Exception("test 1"), new Uri("about:blank"), false);
            yield return (1.0, 2f, false);
        }
    }
}
