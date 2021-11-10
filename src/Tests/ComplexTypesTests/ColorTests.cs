using NUnit.Framework;
using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Tests
{
    public class ColorTests
    {
        [Test]
        public void ComplexTypeToNormalTypeTest()
        {
            var c1 = new Color {A = 255, B = 128, G = 192, R = 240};
            var c2 = new Types.Color(240, 192, 128, 255);
            
            Assert.AreEqual(c2, (Types.Color) c1);
            Assert.AreEqual(c1, (Color) c2);
        }
    }
}