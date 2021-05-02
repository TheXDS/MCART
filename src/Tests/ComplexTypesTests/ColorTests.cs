using Xunit;
using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Tests
{
    public class ColorTests
    {
        [Fact]
        public void ComplexTypeToNormalTypeTest()
        {
            var c1 = new Color {A = 255, B = 128, G = 192, R = 240};
            var c2 = new TheXDS.MCART.Types.Color(240, 192, 128, 255);
            
            Assert.Equal(c2, (TheXDS.MCART.Types.Color) c1);
            Assert.Equal(c1, (Color) c2);
        }
    }
}