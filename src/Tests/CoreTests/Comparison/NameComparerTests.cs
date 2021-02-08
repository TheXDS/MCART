﻿/*
NameComparerTests.cs

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Comparison;
using TheXDS.MCART.Types.Base;
using Xunit;

namespace TheXDS.MCART.Tests.Comparison
{

    public class NameComparerTests : ComparerTestBase<INameable, NameComparer>
    {
        [Theory]
        [ClassData(typeof(NameComparerDataGenerator))]
        public void NameComparerTest([AllowNull]INameable x, [AllowNull] INameable y, bool equal)
        {
            RunTest(x, y, equal);
        }
    }
    public class TypeComparerTests : ComparerTestBase<object, TypeComparer>
    {
        [Theory]
        [ClassData(typeof(TypeComparerDataGenerator))]
        public void TypeComparerTest([AllowNull] object x, [AllowNull] object y, bool equal)
        {
            RunTest(x, y, equal);
        }
    }
}