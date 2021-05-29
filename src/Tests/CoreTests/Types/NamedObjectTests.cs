/*
NamedObjectTests.cs

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
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using Xunit;
using static TheXDS.MCART.Types.Extensions.NamedObjectExtensions;

namespace TheXDS.MCART.Tests.Types
{
    public class NamedObjectTests
    {
        private enum TestEnum
        {
            [Name("Elemento A")] A,
            [Name("Elemento B")] B,
            [Name("Elemento C")] C
        }
        
        [Fact]
        public void AsNamedEnumTest()
        {
            var x = typeof(TestEnum).AsNamedEnum();
            Assert.Equal("Elemento A", x.First());

            Assert.Throws<ArgumentNullException>(() => ((Type) null!).AsNamedEnum());
            Assert.Throws<InvalidTypeException>(() => typeof(string).AsNamedEnum());
            Assert.Throws<InvalidTypeException>(() => typeof(int?).AsNamedEnum());
        }

        [Fact]
        public void FromEnumTest()
        {
            var x = NamedObject<TestEnum>.FromEnum();
            Assert.Equal("Elemento A", x.First());

            Assert.Throws<InvalidTypeException>(NamedObject<string>.FromEnum);
        }

        [Fact]
        public void AsNamedObjectTest()
        {
            var x = AsNamedObject<TestEnum>();
            Assert.Equal("Elemento A", x.First());
        }
    }
}
