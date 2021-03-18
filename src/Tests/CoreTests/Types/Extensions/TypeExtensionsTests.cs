/*
TypeExtensionsTests.cs

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
using System.Linq;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using Xunit;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void AnyAssignableFromTest()
        {
            Assert.True(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(EventArgs), typeof(Exception)).First() == typeof(EventArgs));
            Assert.False(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(Version), typeof(Exception)).Any());
        }

        [Fact]
        public void AreAssignableFromTest()
        {
            Assert.True(typeof(ResolveEventArgs).AreAllAssignable(typeof(EventArgs), typeof(ResolveEventArgs)));
            Assert.False(typeof(ResolveEventArgs).AreAllAssignable(typeof(AppContext), typeof(ResolveEventArgs)));
        }

        [Fact]
        public void NewTest()
        {
            Assert.NotNull(typeof(ResolveEventArgs).New("Test"));
        }

        [Fact]
        public void NotNullableTest()
        {
            Assert.Equal(typeof(int), typeof(int).NotNullable());
            Assert.Equal(typeof(int), typeof(int?).NotNullable());
            Assert.Throws<ArgumentNullException>(() => ((Type)null!).NotNullable());
        }

        [Fact]
        public void IsInstantiableTest()
        {
            Assert.True(typeof(Exception).IsInstantiable());
            Assert.True(typeof(Exception).IsInstantiable(typeof(string)));
            Assert.False(typeof(Exception).IsInstantiable(typeof(int)));
        }

        [Fact]
        public void ToNamedEnumTest()
        {
            Assert.IsAssignableFrom<IEnumerable<NamedObject<Enum>>>(typeof(DayOfWeek).ToNamedEnum());
            Assert.Throws<ArgumentNullException>(() => _ = TypeExtensions.ToNamedEnum(null!));
            Assert.Throws<InvalidTypeException>(() => _ = typeof(string).ToNamedEnum());
        }

        [Fact]
        public void DefaultTest()
        {
            Assert.Equal(0, typeof(int).Default());
            Assert.Equal(0L, typeof(long).Default());
            Assert.Equal(0f, typeof(float).Default());
            Assert.Equal(0.0, typeof(double).Default());
            Assert.Equal(0m, typeof(decimal).Default());
            Assert.Equal(Guid.Empty, typeof(Guid).Default());
            Assert.Null(typeof(string).Default());
            Assert.Null(typeof(object).Default());
        }

        [Fact]
        public void IsStructTest()
        {
            Assert.True(typeof(Guid).IsStruct());
            Assert.False(typeof(int).IsStruct());
            Assert.False(typeof(string).IsStruct());
        }

        [Fact]
        public void IsCollectionTypeTest()
        {
            Assert.False(typeof(int).IsCollectionType());
            Assert.False(typeof(Exception).IsCollectionType());
            Assert.True(typeof(string).IsCollectionType());
            Assert.True(typeof(int[]).IsCollectionType());
            Assert.True(typeof(List<bool>).IsCollectionType());
        }

        [Fact]
        public void DerivatesTest()
        {
            var t = typeof(Exception).Derivates(typeof(Exception).Assembly).ToArray();
            Assert.Contains(typeof(ArgumentNullException), t);
            Assert.DoesNotContain(typeof(TamperException), t);
            Assert.DoesNotContain(typeof(int), t);
            Assert.DoesNotContain(typeof(Guid), t);
            Assert.DoesNotContain(typeof(string), t);
            Assert.DoesNotContain(typeof(Enum), t);
            Assert.DoesNotContain(typeof(AppDomain), t);
            Assert.DoesNotContain(typeof(object), t);
        }
    }
}