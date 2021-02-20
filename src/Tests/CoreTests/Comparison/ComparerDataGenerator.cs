/*
ComparerDataGenerator.cs

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

using System.Collections;
using System.Collections.Generic;

namespace TheXDS.MCART.Tests.Comparison
{
    public abstract class ComparerDataGenerator<T> : IEnumerable<object[]>
    {
        protected abstract IEnumerable<(T a, T b, bool equal)> GetSequences();

        private IEnumerable<object[]> Transform()
        {
            foreach (var j in GetSequences())
            {
                yield return new object[] { j.a, j.b, j.equal };
            }
        }

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator() => Transform().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Transform()).GetEnumerator();
    }
}
