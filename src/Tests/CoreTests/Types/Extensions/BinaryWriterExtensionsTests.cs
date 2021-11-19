using System;
using System.IO;
using System.Text;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class BinaryWriterExtensionsTests
    {
        [Test]
        public void DynamicWrite_Test()
        {
            var g = Guid.NewGuid();
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.DynamicWrite(1000000);
                bw.DynamicWrite(g);
                bw.DynamicWrite(new TestStruct
                {
                    Int32Value = 1000000,
                    BoolValue = true,
                    StringValue = "test"
                });
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.AreEqual(1000000, br.ReadInt32());
                Assert.AreEqual(g, br.ReadGuid());

                var v = br.Read<TestStruct>();
                Assert.AreEqual(1000000, v.Int32Value);
                Assert.True(v.BoolValue);
                Assert.AreEqual("test", v.StringValue);
            }
        }

        [Test]
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

        [Test]
        public void WriteStruct_Contract_Test()
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            Assert.Throws<NotSupportedException>(() => bw.WriteStruct(1));
            Assert.Throws<NotSupportedException>(() => bw.WriteStruct(Guid.NewGuid()));
        }

        private struct TestStruct
        {
            public int Int32Value;
            public bool BoolValue;
            public string StringValue;
        }
    }
}