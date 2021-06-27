using System;
using System.IO;
using System.Text;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class BinaryReaderExtensionstests
    {
        [Fact]
        public void GetBinaryReadMethod_Test()
        {
            var e = ReflectionHelpers.GetMethod<BinaryReader, Func<int>>(o => o.Read);
            Assert.Equal(e, BinaryReaderExtensions.GetBinaryReadMethod(typeof(int)));
        }

        [Fact]
        public void ReadEnum_Test()
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(DayOfWeek.Tuesday);
            }
            ms.Seek(0,SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.Equal(DayOfWeek.Tuesday,br.ReadEnum<DayOfWeek>());
            }
        }

        [Fact]
        public void ReadGuid_Test()
        {
            var g = Guid.NewGuid();
            
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
            }
            ms.Seek(0,SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.Equal(g,br.ReadGuid());
            }
        }
        
        [Fact]
        public void ReadDateTime_Test()
        {
            var g = DateTime.Now;
            
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
            }
            ms.Seek(0,SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.Equal(g,br.ReadDateTime());
            }
        }
        
        [Fact]
        public void ReadTimeSpan_Test()
        {
            var g = TimeSpan.FromSeconds(130015);
            
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
            }
            ms.Seek(0,SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.Equal(g,br.ReadTimeSpan());
            }
        }
        
        [Fact]
        public void Read_Generic_Test()
        {
            var g = TimeSpan.FromSeconds(130015);
            
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
                bw.Write(DayOfWeek.Tuesday);
            }
            ms.Seek(0,SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.Equal(g,br.Read<TimeSpan>());
                Assert.Equal(DayOfWeek.Tuesday, br.Read<DayOfWeek>());
            }
        }
    }
}