using System;
using System.IO;
using System.Text;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class BinaryWriterExtensionsTests
    {
        [Fact]
        public void DynamicWrite_Test()
        {
            var g = Guid.NewGuid();
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.DynamicWrite(1000000);
                bw.DynamicWrite(g);
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.Equal(1000000,br.ReadInt32());
                Assert.Equal(g,br.ReadGuid());
            }
        }

        [Fact]
        public void DynamicWrite_Contract_Test()
        {
            BinaryWriter? bw = null;
            
            Assert.Throws<ArgumentNullException>(() => bw!.DynamicWrite(1));
            using var ms = new MemoryStream();
            using (bw = new BinaryWriter(ms))
            {
                Assert.Throws<ArgumentNullException>(() => bw.DynamicWrite(null!));
                Assert.Throws<InvalidOperationException>(() => bw.DynamicWrite(new Random()));
            }
        }
    }
}