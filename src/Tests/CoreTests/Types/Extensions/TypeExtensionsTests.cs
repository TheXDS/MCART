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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using Xunit;

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
            Assert.Equal(typeof(string), Assert.Throws<ClassNotInstantiableException>(() => typeof(string).New(new Exception())).OffendingObject);
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
            Assert.True(typeof(Exception).IsInstantiable((IEnumerable<Type>?)null));
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

        [Fact]
        public void GetCollectionType_Test()
        {
            Assert.Equal(typeof(int), typeof(int[]).GetCollectionType());
            Assert.Equal(typeof(int), typeof(IEnumerable<int>).GetCollectionType());
            Assert.Equal(typeof(object), typeof(System.Collections.IEnumerable).GetCollectionType());
            Assert.Equal(typeof(string), typeof(Dictionary<int, string>).GetCollectionType());
        }

        [Fact]
        public void IsAnyAssignable_Test()
        {
            Assert.True(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(ArgumentNullException)));
            Assert.False(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(System.IO.Stream)));
        }

        [Fact]
        public void Implements_Test()
        {
            Assert.True(typeof(int[]).Implements(typeof(IEnumerable<>),typeof(int)));
            Assert.True(typeof(int[]).Implements(typeof(IEnumerable<>)));
            Assert.True(typeof(string).Implements(typeof(IEnumerable<char>)));
            Assert.True(typeof(int[]).Implements(typeof(IEnumerable<int>)));
            Assert.True(typeof(IEnumerable<float>).Implements(typeof(IEnumerable<>)));
            
            Assert.False(typeof(float[]).Implements(typeof(IEnumerable<>),typeof(int)));
            Assert.False(typeof(Exception).Implements(typeof(IEnumerable<>)));
            Assert.False(typeof(ValueTask<string>).Implements(typeof(IEnumerable<>)));
        }
    }

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void EpochTest()
        {
            var e = DateTimeExtensions.Epoch(1970);
            
            Assert.Equal(1,e.Day);
            Assert.Equal(1,e.Month);
            Assert.Equal(1970,e.Year);
        }

        [Fact]
        public void Epochs_Test()
        {
            Assert.Equal(1900,DateTimeExtensions.CenturyEpoch.Year);
            Assert.Equal(2000,DateTimeExtensions.Y2KEpoch.Year);
            Assert.Equal(1970,DateTimeExtensions.UnixEpoch.Year);
        }

        [Fact]
        public void ToUnixTimestamp_Test()
        {
            var t = new DateTime(2038, 1, 19, 3, 14, 7);
            Assert.Equal(int.MaxValue,t.ToUnixTimestamp());
        }
        
        [Fact]
        public void ToUnixTimestampMs_Test()
        {
            var t = new DateTime(2012, 5, 19, 19, 35, 0);
            Assert.Equal(1337456100000,t.ToUnixTimestampMs());
        }
        
        [Fact]
        public void FromUnixTimestamp_Test()
        {
            var t = new DateTime(2038, 1, 19, 3, 14, 7);
            Assert.Equal(t,DateTimeExtensions.FromUnixTimestamp(int.MaxValue));
        }
        
        [Fact]
        public void FromUnixTimestampMs_Test()
        {
            var t = new DateTime(2012, 5, 19, 19, 35, 0);
            Assert.Equal(t,1337456100000.FromUnixTimestampMs());
        }
 
        [Fact]
        public void MonthName_Test()
        {
            Assert.Equal("August",DateTimeExtensions.MonthName(8, CultureInfo.CreateSpecificCulture("en-us")));
            Assert.Throws <ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(0, CultureInfo.CurrentCulture));
            Assert.Throws <ArgumentOutOfRangeException>(() => DateTimeExtensions.MonthName(13, CultureInfo.CurrentCulture));
            
            var t = DateTime.Today;
            Assert.Equal(t.ToString("MMMM"), DateTimeExtensions.MonthName(t.Month));
        }
    }
}