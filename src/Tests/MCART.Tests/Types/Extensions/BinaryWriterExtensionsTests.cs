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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using TheXDS.MCART.Types.Extensions;

public class BinaryWriterExtensionsTests
{
    [Test]
    public void DynamicWrite_Test()
    {
        Guid g = Guid.NewGuid();
        using MemoryStream? ms = new();
        using (BinaryWriter? bw = new(ms, Encoding.Default, true))
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
        using BinaryReader? br = new(ms);
        Assert.AreEqual(1000000, br.ReadInt32());
        Assert.AreEqual(g, br.ReadGuid());

        TestStruct v = br.Read<TestStruct>();
        Assert.AreEqual(1000000, v.Int32Value);
        Assert.True(v.BoolValue);
        Assert.AreEqual("test", v.StringValue);
    }

    [Test]
    public void DynamicWrite_Contract_Test()
    {
        BinaryWriter? bw = null;

        Assert.Throws<ArgumentNullException>(() => bw!.DynamicWrite(1));
        using MemoryStream? ms = new();
        using (bw = new BinaryWriter(ms))
        {
            Assert.Throws<ArgumentNullException>(() => bw.DynamicWrite(null!));
            Assert.Throws<InvalidOperationException>(() => bw.DynamicWrite(new Random()));
        }
    }

    [Test]
    public void WriteStruct_Contract_Test()
    {
        using MemoryStream? ms = new();
        using BinaryWriter? bw = new(ms);
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
