/*
BinaryReaderExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

public class BinaryReaderExtensionsTests
{
    [Test]
    public void GetBinaryReadMethod_Test()
    {
        System.Reflection.MethodInfo e = ReflectionHelpers.GetMethod<BinaryReader, Func<int>>(o => o.ReadInt32);
        Assert.AreEqual(e, BinaryReaderExtensions.GetBinaryReadMethod(typeof(int)));
    }

    [Test]
    public void ReadEnum_Test()
    {
        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.Write(DayOfWeek.Tuesday);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using (BinaryReader br = new(ms, Encoding.Default, true))
        {
            Assert.AreEqual(DayOfWeek.Tuesday, br.ReadEnum<DayOfWeek>());
        }
        ms.Seek(0, SeekOrigin.Begin);
        using (BinaryReader br = new(ms, Encoding.Default))
        {
            Assert.AreEqual(DayOfWeek.Tuesday, br.ReadEnum(typeof(DayOfWeek)));
        }
    }

    [Test]
    public void ReadArray_multi_dimentional_Test()
    {
        int[,] a = { { 9, 99 }, { 3, 33 }, { 5, 55 } };
        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.DynamicWrite(a);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using BinaryReader br = new(ms);
        Assert.AreEqual(a, br.ReadArray(typeof(int[,])));
    }

    [Test]
    public void ReadArray_Test()
    {
        int[] a = { 9, 8, 7, 1, 2, 3 };
        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.DynamicWrite(a);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using BinaryReader br = new(ms);
        Assert.AreEqual(a, br.ReadArray<int>());
    }

    [Test]
    public void ReadGuid_Test()
    {
        Guid g = Guid.NewGuid();

        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.Write(g);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using BinaryReader br = new(ms);
        Assert.AreEqual(g, br.ReadGuid());
    }

    [Test]
    public void ReadDateTime_Test()
    {
        DateTime g = DateTime.Now;

        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.Write(g);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using BinaryReader br = new(ms);
        Assert.AreEqual(g, br.ReadDateTime());
    }

    [Test]
    public void ReadTimeSpan_Test()
    {
        TimeSpan g = TimeSpan.FromSeconds(130015);

        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.Write(g);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using BinaryReader br = new(ms);
        Assert.AreEqual(g, br.ReadTimeSpan());
    }

    [Test]
    public void Read_Generic_Test()
    {
        TimeSpan g = TimeSpan.FromSeconds(130015);

        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.Write(g);
            bw.Write(DayOfWeek.Tuesday);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using BinaryReader br = new(ms);
        Assert.AreEqual(g, br.Read<TimeSpan>());
        Assert.AreEqual(DayOfWeek.Tuesday, br.Read<DayOfWeek>());
    }

    [Test]
    public void MarshalReadStruct_Test()
    {
        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.MarshalWriteStruct(123456.789m);
        }
        ms.Seek(0, SeekOrigin.Begin);
        using BinaryReader br = new(ms);

        decimal v = br.MarshalReadStruct<decimal>();
        Assert.AreEqual(123456.789m, v);
    }

    [Test]
    public void FieldReadStruct_Test()
    {
        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.WriteStruct(new TestStruct
            {
                Int32Value = 1000000,
                BoolValue = true,
                StringValue = "test"
            });
        }
        ms.Seek(0, SeekOrigin.Begin);
        using (BinaryReader br = new(ms, Encoding.Default, true))
        {
            TestStruct v = br.Read<TestStruct>();
            Assert.AreEqual(1000000, v.Int32Value);
            Assert.True(v.BoolValue);
            Assert.AreEqual("test", v.StringValue);
        }

        ms.Seek(0, SeekOrigin.Begin);
        using (BinaryReader br = new(ms))
        {
            TestStruct v = br.ReadStruct<TestStruct>();
            Assert.AreEqual(1000000, v.Int32Value);
            Assert.True(v.BoolValue);
            Assert.AreEqual("test", v.StringValue);
        }
    }

    [Test]
    public void ReadStruct_with_ctor()
    {
        using MemoryStream ms = new();
        using (BinaryWriter bw = new(ms, Encoding.Default, true))
        {
            bw.WriteStruct(new TestStruct2(5120, "test"));
        }
        ms.Seek(0, SeekOrigin.Begin);
        using (BinaryReader br = new(ms, Encoding.Default, true))
        {
            TestStruct2 v = br.ReadStruct<TestStruct2>();
            Assert.AreEqual(5120, v.IntProp);
            Assert.AreEqual("test", v.StrProp);
        }
    }

    [ExcludeFromCodeCoverage]
    private struct TestStruct
    {
        public int Int32Value;
        public bool BoolValue;
        public string StringValue;
    }

    [ExcludeFromCodeCoverage]
    private struct TestStruct2
    {
        public int IntProp { get; }
        public string StrProp { get; }

        private TestStruct2(int intProp) : this(intProp, "")
        {
        }
        
        public TestStruct2(int intProp, string strProp)
        {
            IntProp = intProp;
            StrProp = strProp;
        }
    }
}
