﻿/*
EnumerableExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using static TheXDS.MCART.Types.Extensions.EnumerableExtensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void RangeTest()
        {
            var c = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            Assert.Equal(new[] { 1, 2, 3 }, c.Range(0, 3));
            Assert.Equal(new[] { 4, 5, 6 }, c.Range(3, 3));
            Assert.Equal(new[] { 7, 8, 9 }, c.Range(6, 3));
        }

        [Fact]
        public void ToExtendedListTest()
        {
            var c = new[] {1, 2, 3}.ToExtendedList();

            Assert.IsType<TheXDS.MCART.Types.Extensions.List<int>>(c);
            Assert.Equal(3, c.Count);
        }

        [Fact]
        public void ShiftTest()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 2, 3, 4, 5, 0 }, c.Shift(1));
            Assert.Equal(new[] { 3, 4, 5, 0, 0 }, c.Shift(2));
            Assert.Equal(new[] { 0, 1, 2, 3, 4 }, c.Shift(-1));
            Assert.Equal(new[] { 0, 0, 1, 2, 3 }, c.Shift(-2));
        }

        [Fact]
        public void RotateTest()
        {
            var c = new[] { 1, 2, 3, 4, 5 };

            Assert.Equal(new[] { 2, 3, 4, 5, 1 }, c.Rotate(1));
            Assert.Equal(new[] { 3, 4, 5, 1, 2 }, c.Rotate(2));
            Assert.Equal(new[] { 5, 1, 2, 3, 4 }, c.Rotate(-1));
            Assert.Equal(new[] { 4, 5, 1, 2, 3 }, c.Rotate(-2));
        }
        [Fact]
        public void ExceptForTest_WithValues()
        {
            var array = new[] {1, 2, 3, 4, 5};
            var exclusions = new[] {2, 4};
            var result = new[] {1, 3, 5};

            Assert.Equal(result, array.ExceptFor(exclusions));
        }
        [Fact]
        public void ExceptForTest_WithObjects()
        {
            var array = new object[5];
            for(var j = 0; j < 5; j++) array[j] = new object();
            var exclusions = array.Range(3,2).ToArray();
            var result = array.Range(0,3).ToArray();

            Assert.Equal(result, array.ExceptFor(exclusions).ToArray());
        }

    }
}