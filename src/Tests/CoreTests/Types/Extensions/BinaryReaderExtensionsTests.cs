/*
BinaryReaderExtensionsTests.cs

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
using System.IO;
using System.Text;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class BinaryReaderExtensionsTests
    {
        [Test]
        public void GetBinaryReadMethod_Test()
        {
            var e = ReflectionHelpers.GetMethod<BinaryReader, Func<int>>(o => o.ReadInt32);
            Assert.AreEqual(e, BinaryReaderExtensions.GetBinaryReadMethod(typeof(int)));
        }

        [Test]
        public void ReadEnum_Test()
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(DayOfWeek.Tuesday);
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms, Encoding.Default, true))
            {
                Assert.AreEqual(DayOfWeek.Tuesday, br.ReadEnum<DayOfWeek>());
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms, Encoding.Default))
            {
                Assert.AreEqual(DayOfWeek.Tuesday, br.ReadEnum(typeof(DayOfWeek)));
            }
        }

        [Test]
        public void ReadGuid_Test()
        {
            var g = Guid.NewGuid();

            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.AreEqual(g, br.ReadGuid());
            }
        }

        [Test]
        public void ReadDateTime_Test()
        {
            var g = DateTime.Now;

            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.AreEqual(g, br.ReadDateTime());
            }
        }

        [Test]
        public void ReadTimeSpan_Test()
        {
            var g = TimeSpan.FromSeconds(130015);

            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                Assert.AreEqual(g, br.ReadTimeSpan());
            }
        }

        [Test]
        public void Read_Generic_Test()
        {
            var g = TimeSpan.FromSeconds(130015);

            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.Write(g);
                bw.Write(DayOfWeek.Tuesday);
            }
            ms.Seek(0, SeekOrigin.Begin);
            using var br = new BinaryReader(ms);
            Assert.AreEqual(g, br.Read<TimeSpan>());
            Assert.AreEqual(DayOfWeek.Tuesday, br.Read<DayOfWeek>());
        }

        [Test]
        public void MarshalReadStruct_Test()
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.MarshalWriteStruct(123456.789m);
            }
            ms.Seek(0, SeekOrigin.Begin);
            using var br = new BinaryReader(ms);

            var v = br.MarshalReadStruct<decimal>();
            Assert.AreEqual(123456.789m, v);
        }

        [Test]
        public void FieldReadStruct_Test()
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms, Encoding.Default, true))
            {
                bw.WriteStruct(new TestStruct
                {
                    Int32Value = 1000000,
                    BoolValue = true,
                    StringValue = "test"
                });
            }
            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms, Encoding.Default, true))
            {
                var v = br.Read<TestStruct>();
                Assert.AreEqual(1000000, v.Int32Value);
                Assert.True(v.BoolValue);
                Assert.AreEqual("test", v.StringValue);
            }

            ms.Seek(0, SeekOrigin.Begin);
            using (var br = new BinaryReader(ms))
            {
                var v = br.ReadStruct<TestStruct>();
                Assert.AreEqual(1000000, v.Int32Value);
                Assert.True(v.BoolValue);
                Assert.AreEqual("test", v.StringValue);
            }
        }

        private struct TestStruct
        {
            public int Int32Value;
            public bool BoolValue;
            public string StringValue;
        }
    }
}