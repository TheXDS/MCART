﻿/*
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
