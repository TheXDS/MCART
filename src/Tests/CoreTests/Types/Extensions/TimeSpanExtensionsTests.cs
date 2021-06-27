using System;
using System.Globalization;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class TimeSpanExtensionsTests
    {
        [Fact]
        public void VerboseTest()
        {
            Assert.Equal(Strings.Seconds(15),TimeSpan.FromSeconds(15).Verbose());
            Assert.Equal(Strings.Minutes(3),TimeSpan.FromSeconds(180).Verbose());
            Assert.Equal(Strings.Hours(2),TimeSpan.FromSeconds(7200).Verbose());
            Assert.Equal(Strings.Days(5),TimeSpan.FromDays(5).Verbose());

            Assert.Equal(
                $"{Strings.Minutes(1)}, {Strings.Seconds(5)}",
                TimeSpan.FromSeconds(65).Verbose());
            
            Assert.Equal(
                $"{Strings.Days(2)}, {Strings.Hours(5)}, {Strings.Minutes(45)}, {Strings.Seconds(23)}",
                (TimeSpan.FromDays(2) + TimeSpan.FromHours(5) + TimeSpan.FromMinutes(45) + TimeSpan.FromSeconds(23)).Verbose());
        }

        [Fact]
        public void AsTimeTest()
        {
            var t = TimeSpan.FromSeconds(60015);
            var c = CultureInfo.GetCultureInfo("en-UK");
            var r = t.AsTime(c);
            Assert.Equal("4:40 p.\u00A0m.", r);
            Assert.Equal(
                string.Format($"{{0:{CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern}}}",
                    DateTime.MinValue.Add(t)), t.AsTime());
        }
    }
}