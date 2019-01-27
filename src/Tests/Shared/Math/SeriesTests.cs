using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static TheXDS.MCART.Math.Series;
namespace Common.Math
{
    public class SeriesTests
    {
        [Fact]
        public void FibonacciTest()
        {
            var a = Fibonacci().Take(16).ToArray();
            var b = new long[]
            {
                0,
                1,
                1,
                2,
                3,
                5,
                8,
                13,
                21,
                34,
                55,
                89,
                144,
                233,
                377,
                610
            };
            Assert.Equal(b,a);
        }
    }
}
