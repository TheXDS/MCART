/*
TweenTests.cs

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
using Xunit;
using static TheXDS.MCART.Math.Tween;

namespace TheXDS.MCART.Tests.Math
{
    [CLSCompliant(false)]
    public class TweenTests
    {
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(0.25, 0.25)]
        [InlineData(0.5, 0.5)]        
        [InlineData(0.75, 0.75)]
        [InlineData(1.0, 1.0)]
        public void LinearTest(in double input, in double output)
        {
            Assert.Equal(output, Linear(input));
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(0.25, 0.1)]
        [InlineData(0.5, 0.5)]        
        [InlineData(0.75, 0.9)]
        [InlineData(1.0, 1.0)]
        public void QuadraticTest(in double input, in double output)
        {
            Assert.Equal(output, Quadratic(input));
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(0.25, 0.035714285714)]
        [InlineData(0.5, 0.5)]        
        [InlineData(0.75, 0.964285714286)]
        [InlineData(1.0, 1.0)]
        public void CubicTest(in double input, in double output)
        {
            Assert.Equal(output, System.Math.Round(Cubic(input), 12));
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(0.25, 0.68359375)]
        [InlineData(0.5, 0.9375)]        
        [InlineData(0.75, 0.99609375)]
        [InlineData(1.0, 1.0)]
        public void QuarticTest(in double input, in double output)
        {
            Assert.Equal(output, System.Math.Round(Quartic(input),12));
        }
        
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(0.1, 1.987688340595)]
        [InlineData(0.2, 0.048943483705)]
        [InlineData(0.3, 1.891006524188)]
        [InlineData(0.4, 0.190983005625)]
        [InlineData(0.5, 1.707106781187)]
        [InlineData(0.6, 0.412214747708)]
        [InlineData(0.7, 1.453990499740)]
        [InlineData(0.8, 0.690983005625)]
        [InlineData(0.9, 1.156434465040)]
        [InlineData(1.0, 1.0)]
        public void ShakyTest(in double input, in double output)
        {
            Assert.Equal(output, System.Math.Round(Shaky(input), 12));
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(0.1, 1.250105790668)]
        [InlineData(0.2, 0.817765433958)]
        [InlineData(0.3, 1.139719345509)]
        [InlineData(0.4, 0.891779529237)]
        [InlineData(0.5, 1.082995956795)]
        [InlineData(0.6, 0.938142705985)]
        [InlineData(0.7, 1.043605092429)]
        [InlineData(0.8, 0.972492472466)]
        [InlineData(0.9, 1.013083718634)]
        [InlineData(1.0, 1.0)]
        public void BouncyTest(in double input, in double output)
        {
            Assert.Equal(output, System.Math.Round(Bouncy(input),12));
        }
    }
}