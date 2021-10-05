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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using Xunit;
using static TheXDS.MCART.Types.Extensions.NamedObjectExtensions;

namespace TheXDS.MCART.Tests.Types
{
    public class NamedObjectTests
    {
        [ExcludeFromCodeCoverage]
        private class NameableClass : INameable
        {
            [Name("Name property")] public string Name => "Nameable class";
        }

        private class TestClass2
        {
            public override string ToString() => null!;
        }
        
        [Name("Test enum")]
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

        [Fact]
        public void Infer_Test()
        {
            var x = new NameableClass();
            Assert.Equal("Nameable class", NamedObject<object?>.Infer(x));
            Assert.Equal("Name property",NamedObject<object?>.Infer(ReflectionHelpers.GetProperty(() => x.Name)));
            Assert.Equal("Test enum",NamedObject<object?>.Infer(typeof(TestEnum)));
            Assert.Equal("Exception",NamedObject<object?>.Infer(typeof(Exception)));
            Assert.Equal("Elemento A",NamedObject<object?>.Infer(TestEnum.A));
            Assert.Equal("TestClass2",NamedObject<object?>.Infer(new TestClass2()));
            Assert.Throws<ArgumentNullException>(() => NamedObject<object?>.Infer(null));
        }

        [Fact]
        public void Implicit_Operators_Test()
        {
            var v = NamedObject<TestEnum>.FromEnum().ToArray()[1];
            Assert.Equal(TestEnum.B, (TestEnum)v);
            Assert.Equal("Elemento B", ((KeyValuePair<string, TestEnum>)v).Key);
            Assert.Equal(TestEnum.B, ((KeyValuePair<string, TestEnum>)v).Value);
            Assert.Equal("Elemento B", (NamedObject<TestEnum>)new KeyValuePair<string, TestEnum>("Elemento B", TestEnum.B));
        }

        [Fact]
        public void Equals_Test()
        {
            var v = NamedObject<TestEnum>.FromEnum().ToArray()[1];
            Assert.True(v.Equals(NamedObject<TestEnum>.FromEnum().ToArray()[1]));
            Assert.False(v.Equals(NamedObject<TestEnum>.FromEnum().ToArray()[2]));
            Assert.True(v.Equals(TestEnum.B));
            Assert.False(v.Equals(TestEnum.C));
            Assert.True(new NamedObject<int?>(null, "Test").Equals(null));
            Assert.False(v.Equals(null));
            Assert.False(v.Equals(new Exception()));
        }

        [Fact]
        public void Equals_Operator_Test()
        {
            var v1 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
            var v2 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
            var v3 = NamedObject<TestEnum>.FromEnum().ToArray()[2];
            
            Assert.True(v1 == v2);
            Assert.False(v1 == v3);
            Assert.False(v1 != v2);
            Assert.True(v1 != v3);
        }
        
        [Fact]
        public void GetHashCode_Test()
        {
            var v1 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
            var v2 = NamedObject<TestEnum>.FromEnum().ToArray()[1];
            var v3 = NamedObject<TestEnum>.FromEnum().ToArray()[2];
            
            Assert.True(v1.GetHashCode() == v2.GetHashCode());
            Assert.False(v1.GetHashCode() == v3.GetHashCode());
        }
    }
}
