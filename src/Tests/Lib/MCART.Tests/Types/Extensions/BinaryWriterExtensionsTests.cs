/*
BinaryReaderExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Diagnostics.CodeAnalysis;
using System.Text;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class BinaryWriterExtensionsTests
{
    [ExcludeFromCodeCoverage]
    private struct SimpleTestStruct
    {
        public int? NullableIntField;
    }

    [Test]
    public void WriteStruct_contract_test()
    {
        using MemoryStream? ms = new();
        using BinaryWriter? bw = new(ms, Encoding.Default, true);
        Assert.That(() => bw.WriteStruct(new SimpleTestStruct() { NullableIntField = null }), Throws.InstanceOf<NullReferenceException>());
    }

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
        Assert.That(1000000, Is.EqualTo(br.ReadInt32()));
        Assert.That(g, Is.EqualTo(br.ReadGuid()));

        TestStruct v = br.Read<TestStruct>();
        Assert.That(1000000, Is.EqualTo(v.Int32Value));
        Assert.That(v.BoolValue);
        Assert.That("test", Is.EqualTo(v.StringValue));
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
    public void MarshalWriteStructArray_Test()
    {
        using var ms = new MemoryStream();
        using (var bw = new BinaryWriter(ms))
        {
            Assert.That(bw.MarshalWriteStructArray([1, 2, 3, 4]), Is.EqualTo(16));
        }
        Assert.That(ms.ToArray(), Is.EquivalentTo(new byte[] { 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0 }));
    }

    [Test]
    public void MarshalWriteStructArray_with_bigEndian_struct_Test()
    {
        var sourceArray = new BigEndianTestStruct[]
        {
            new() { LittleEndianValue = 1, BigEndianValue = 1 },
            new() { LittleEndianValue = 2, BigEndianValue = 2 },
            new() { LittleEndianValue = 3, BigEndianValue = 3 },
            new() { LittleEndianValue = 4, BigEndianValue = 4 }
        };

        using var ms = new MemoryStream();
        using (var bw = new BinaryWriter(ms))
        {
            Assert.That(bw.MarshalWriteStructArray(sourceArray), Is.EqualTo(32));
        }
        Assert.That(ms.ToArray(), Is.EquivalentTo(new byte[]
        {
            /*
                LE     |    BE
            -----------+-----------*/
            1, 0, 0, 0, 0, 0, 0, 1,
            2, 0, 0, 0, 0, 0, 0, 2,
            3, 0, 0, 0, 0, 0, 0, 3,
            4, 0, 0, 0, 0, 0, 0, 4
        }));
    }

    [Test]
    public void MarshalWriteStruct_Test()
    {
        var source = new SimpleStruct() { IntValue = 1 };
        using var ms = new MemoryStream();
        using (var bw = new BinaryWriter(ms))
        {
            Assert.That(bw.MarshalWriteStruct(source), Is.EqualTo(4));
        }
        Assert.That(ms.ToArray(), Is.EquivalentTo(new byte[] { 1, 0, 0, 0 }));
    }

    [Test]
    public void MarshalWriteStruct_with_endianess_Test()
    {
        var source = new BigEndianTestStruct() { LittleEndianValue = 1, BigEndianValue = 1 };
        using var ms = new MemoryStream();
        using (var bw = new BinaryWriter(ms))
        {
            Assert.That(bw.MarshalWriteStruct(source), Is.EqualTo(8));
        }
        Assert.That(ms.ToArray(), Is.EquivalentTo(new byte[] { 1, 0, 0, 0, 0, 0, 0, 1 }));
    }

    [Test]
    public void WriteNullTerminatedString_Test()
    {
        using var ms = new MemoryStream();
        using (var bw = new BinaryWriter(ms))
        {
            bw.WriteNullTerminatedString("Test");
        }
        Assert.That(ms.ToArray(), Is.EquivalentTo("Test\0"u8.ToArray()));
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

    private struct BigEndianTestStruct
    {
        [Endianness(Endianness.LittleEndian)]
        public int LittleEndianValue;
        [Endianness(Endianness.BigEndian)]
        public int BigEndianValue;
    }

    private struct SimpleStruct
    {
        public int IntValue;
    }
}
