using NUnit.Framework;
using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Tests
{
    public class ColorTests
    {
        [Test]
        public void ComplexTypeToNormalTypeTest()
        {
            Color? c1 = new() { A = 255, B = 128, G = 192, R = 240 };
            Types.Color c2 = new(240, 192, 128, 255);

            Assert.AreEqual(c2, (Types.Color)c1);
            Assert.AreEqual(c1, (Color)c2);
        }
    }
}