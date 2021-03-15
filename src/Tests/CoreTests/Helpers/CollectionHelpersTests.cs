/*
CollectionHelpersTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

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
using System.Threading.Tasks;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests
{
    public class CollectionHelpersTests
    {
        [Fact]
        public void OrTest_bool()
        {
            var data = new bool[10];
            Assert.False(data.Or());
            data[5] = true;
            Assert.True(data.Or());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Or((IEnumerable<bool>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new byte[] { 1, 2, 4 }, 7)]
        [InlineData(new byte[] { 1, 2, 4, 8, 16, 32, 64, 128 }, 255)]
        [InlineData(new byte[] { 128, 255 }, 255)]
        public void OrTest_byte(byte[] array, byte orValue)
        {
            Assert.Equal(orValue, array.Or());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Or((IEnumerable<byte>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new short[] { 1, 2, 4 }, 7)]
        [InlineData(new short[] { 128, 255, 16384 }, 16639)]
        public void OrTest_Int16(short[] array, short orValue)
        {
            Assert.Equal(orValue, array.Or());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Or((IEnumerable<short>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new char[] { '\x0001', '\x0002', '\x0004' }, '\x0007')]
        [InlineData(new char[] { '\x0080', '\x00FF', '\x1000' }, '\x10FF')]
        public void OrTest_char(char[] array, char orValue)
        {
            Assert.Equal(orValue, array.Or());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Or((IEnumerable<char>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new int[] { 1, 2, 4 }, 7)]
        [InlineData(new int[] { 128, 255, 131072 }, 131327)]
        public void OrTest_Int32(int[] array, int orValue)
        {
            Assert.Equal(orValue, array.Or());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Or((IEnumerable<int>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new long[] { 1, 2, 4 }, 7)]
        [InlineData(new long[] { 128, 255, 131072 }, 131327)]
        public void OrTest_Int64(long[] array, long orValue)
        {
            Assert.Equal(orValue, array.Or());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Or((IEnumerable<long>)null!));
        }

        [Fact]
        public void AndTest_bool()
        {
            var data = new bool[10];
            Assert.False(data.And());
            data[5] = true;
            Assert.False(data.And());
            for (int j = 0; j < 10; j++) data[j] = true;
            Assert.True(data.And());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.And((IEnumerable<bool>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new byte[] { 1, 2, 4, 8, 16, 32, 64, 128 }, 0)]
        [InlineData(new byte[] { 128, 255 }, 128)]
        public void AndTest_byte(byte[] array, byte orValue)
        {
            Assert.Equal(orValue, array.And());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.And((IEnumerable<byte>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new short[] { 1, 2, 4 }, 0)]
        [InlineData(new short[] { 0x10F0, 0x100F }, 0x1000)]
        public void AndTest_Int16(short[] array, short orValue)
        {
            Assert.Equal(orValue, array.And());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.And((IEnumerable<short>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new char[] { '\x0001', '\x0002', '\x0004' }, '\x0000')]
        [InlineData(new char[] { '\x10F0', '\x100F' }, '\x1000')]
        public void AndTest_char(char[] array, char orValue)
        {
            Assert.Equal(orValue, array.And());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.And((IEnumerable<char>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new int[] { 1, 2, 4 }, 0)]
        [InlineData(new int[] { 0x10F0, 0x100F }, 0x1000)]
        public void AndTest_Int32(int[] array, int orValue)
        {
            Assert.Equal(orValue, array.And());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.And((IEnumerable<int>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new long[] { 1, 2, 4 }, 0)]
        [InlineData(new long[] { 0x10F0, 0x100F }, 0x1000)]
        public void AndTest_Int64(long[] array, long orValue)
        {
            Assert.Equal(orValue, array.And());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.And((IEnumerable<long>)null!));
        }

        [Fact]
        public void XorTest_bool()
        {
            var data = new bool[10];
            Assert.False(data.Xor());
            data[5] = true;
            Assert.True(data.Xor());
            data[6] = true;
            Assert.False(data.Xor());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Xor((IEnumerable<bool>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new byte[] { 131, 140 }, 15)]
        public void XorTest_byte(byte[] array, byte orValue)
        {
            Assert.Equal(orValue, array.Xor());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Xor((IEnumerable<byte>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new short[] { 131, 140 }, 15)]
        [InlineData(new short[] { 0x10F0, 0x100F }, 0x00FF)]
        public void XorTest_Int16(short[] array, short orValue)
        {
            Assert.Equal(orValue, array.Xor());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Xor((IEnumerable<short>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new char[] { (char)131,(char) 140 }, (char)15)]
        [InlineData(new char[] { '\x10F0', '\x100F' }, '\x00FF')]
        public void XorTest_char(char[] array, char orValue)
        {
            Assert.Equal(orValue, array.Xor());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Xor((IEnumerable<char>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new int[] { 131, 140 }, 15)]
        [InlineData(new int[] { 0x10F0, 0x100F }, 0x00FF)]
        public void XorTest_Int32(int[] array, int orValue)
        {
            Assert.Equal(orValue, array.Xor());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Xor((IEnumerable<int>)null!));
        }

        [Theory]
        [CLSCompliant(false)]
        [InlineData(new long[] { 131, 140 }, 15)]
        [InlineData(new long[] { 0x10F0, 0x100F }, 0x00FF)]
        public void XorTest_Int64(long[] array, long orValue)
        {
            Assert.Equal(orValue, array.Xor());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.Xor((IEnumerable<long>)null!));
        }

        [Fact]
        public void AllEmptyTest()
        {
            Assert.False(new[] { "A", "", null }.AllEmpty());
            Assert.True(new[] { "", null }.AllEmpty());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.AllEmpty(null!));
        }

        [Fact]
        public async Task AllEmptyAsyncTest()
        {
            Assert.False(await new[] { "A", "", null }.YieldAsync(_ => Task.CompletedTask).AllEmptyAsync());
            Assert.True(await new[] { "", null }.YieldAsync(_ => Task.CompletedTask).AllEmptyAsync());
            await Assert.ThrowsAsync<ArgumentNullException>(() => CollectionHelpers.AllEmptyAsync(null!));
        }

        [Fact]
        public void AnyEmptyTest()
        {
            Assert.False(new[] { "A", "B", "C" }.AnyEmpty());
            Assert.True(new[] { "A", "" }.AnyEmpty());
            Assert.True(new[] { "A", null }.AnyEmpty());
            Assert.True(new[] { "", null }.AnyEmpty());
            Assert.True(new[] { "A", "", null }.AnyEmpty());
            Assert.Throws<ArgumentNullException>(() => CollectionHelpers.AnyEmpty(null!));
        }

        [Fact]
        public async Task AnyEmptyAsyncTest()
        {
            Assert.False(await new[] { "A", "B", "C" }.YieldAsync(_ => Task.CompletedTask).AnyEmptyAsync());
            Assert.True(await new[] { "A", "" }.YieldAsync(_ => Task.CompletedTask).AnyEmptyAsync());
            Assert.True(await new[] { "A", null }.YieldAsync(_ => Task.CompletedTask).AnyEmptyAsync());
            Assert.True(await new[] { "", null }.YieldAsync(_ => Task.CompletedTask).AnyEmptyAsync());
            Assert.True(await new[] { "A", "", null }.YieldAsync(_ => Task.CompletedTask).AnyEmptyAsync());
            await Assert.ThrowsAsync<ArgumentNullException>(() => CollectionHelpers.AnyEmptyAsync(null!));
        }

        [Fact]
        public void ToPercentTestDouble()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, c.ToPercentDouble());
            Assert.Equal(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, c.ToPercentDouble(true));
            Assert.Equal(new[] { 0.0, 0.25, 0.5, 0.75, 1.0 }, c.ToPercentDouble(false));
            Assert.Equal(new[] { 0.1, 0.2, 0.3, 0.4, 0.5 }, c.ToPercentDouble(10));
            Assert.Equal(new[] { 0.0, 0.25, 0.5, 0.75, 1.0 }, c.ToPercentDouble(1, 5));
            Assert.Throws<InvalidOperationException>(() => c.ToPercentDouble(1, 1).ToList());
        }

        [Fact]
        public void ToPercentTestSingle()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, c.ToPercentSingle());
            Assert.Equal(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, c.ToPercentSingle(true));
            Assert.Equal(new[] { 0.0f, 0.25f, 0.5f, 0.75f, 1.0f }, c.ToPercentSingle(false));
            Assert.Equal(new[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f }, c.ToPercentSingle(10));
            Assert.Equal(new[] { 0.0f, 0.25f, 0.5f, 0.75f, 1.0f }, c.ToPercentSingle(1, 5));
            Assert.Throws<InvalidOperationException>(() => c.ToPercentSingle(1, 1).ToList());
        }

        [Fact]
        public void NotEmpty_string_Test()
        {
            var i = new[] { "1", "2", null, "", "3" };
            var o = new[] { "1", "2", "3" };

            Assert.Equal(o, i.NotEmpty());
        }
    }
}