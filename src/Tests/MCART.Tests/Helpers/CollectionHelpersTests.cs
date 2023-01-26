/*
CollectionHelpersTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene todas las pruebas pertenecientes a la clase estática
TheXDS.MCART.Common.

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

namespace TheXDS.MCART.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

public class CollectionHelpersTests
{
    [Test]
    public void Or_Test_bool()
    {
        bool[]? data = new bool[10];
        Assert.False(data.Or());
        data[5] = true;
        Assert.True(data.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<bool>)null!).Or());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new byte[] { 1, 2, 4 }, 7)]
    [TestCase(new byte[] { 1, 2, 4, 8, 16, 32, 64, 128 }, 255)]
    [TestCase(new byte[] { 128, 255 }, 255)]
    public void Or_Test_byte(byte[] array, byte orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<byte>)null!).Or());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new sbyte[] { 1, 2, 4 }, 7)]
    [TestCase(new sbyte[] { 1, 2, 4, 8, 16, 32, 64 }, 127)]
    [TestCase(new sbyte[] { -128, 127 }, -1)]
    public void Or_Test_Sbyte(sbyte[] array, sbyte orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<sbyte>)null!).Or());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new ushort[] { 1, 2, 4 }, (ushort)7)]
    [TestCase(new ushort[] { 128, 255, 16384 }, (ushort)16639)]
    public void Or_Test_UInt16(ushort[] array, ushort orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<ushort>)null!).Or());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new short[] { 1, 2, 4 }, 7)]
    [TestCase(new short[] { 128, 255, 16384 }, 16639)]
    [TestCase(new short[] { -32768, 32767 }, (short)-1)]
    public void Or_Test_Int16(short[] array, short orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<short>)null!).Or());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new[] { '\x1001', '\x1002', '\x1004' }, '\x1007')]
    [TestCase(new[] { '\x1080', '\x10FF', '\x1000' }, '\x10FF')]
    public void Or_Test_Char(char[] array, char orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<char>)null!).Or());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new[] { 1, 2, 4 }, 7)]
    [TestCase(new[] { 128, 255, 131072 }, 131327)]
    [TestCase(new[] { -2147483648, 2147483647 }, -1)]
    public void Or_Test_Int32(int[] array, int orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null!).Or());
    }

    [CLSCompliant(false)]
    [TestCase(new uint[] { 1, 2, 4 }, (uint)7)]
    [TestCase(new uint[] { 128, 255, 131072 }, (uint)131327)]
    public void Or_Test_UInt32(uint[] array, uint orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<uint>)null!).Or());
    }
    
    [CLSCompliant(false)]
    [TestCase(new [] { 1L, 2L, 4L }, 7L)]
    [TestCase(new [] { 128L, 255L, 131072L }, 131327L)]
    [TestCase(new [] { -9223372036854775808L, 9223372036854775807L }, -1L)]
    public void Or_Test_Int64(long[] array, long orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<long>)null!).Or());
    }

    [CLSCompliant(false)]
    [TestCase(new ulong[] { 1, 2, 4 }, (ulong)7)]
    [TestCase(new ulong[] { 128, 255, 131072 }, (ulong)131327)]
    public void Or_Test_UInt64(ulong[] array, ulong orValue)
    {
        Assert.AreEqual(orValue, array.Or());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<ulong>)null!).Or());
    }
    
    [Test]
    public void And_Test_bool()
    {
        bool[]? data = new bool[10];
        Assert.False(data.And());
        data[5] = true;
        Assert.False(data.And());
        for (int j = 0; j < 10; j++) data[j] = true;
        Assert.True(data.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<bool>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<bool>().And());
    }

    [CLSCompliant(false)]
    [TestCase(new byte[] { 1, 2, 4, 8, 16, 32, 64, 128 }, 0)]
    [TestCase(new byte[] { 128, 255 }, 128)]
    public void And_Test_byte(byte[] array, byte orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<byte>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<byte>().And());
    }

    [CLSCompliant(false)]
    [TestCase(new sbyte[] { 1, 2, 4, 8, 16, 32, 64 }, 0)]
    [TestCase(new sbyte[] { 64, 127 }, 64)]
    public void And_Test_sbyte(sbyte[] array, sbyte orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<sbyte>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<sbyte>().And());
    }
        
    [CLSCompliant(false)]
    [TestCase(new ushort[] { 1, 2, 4 }, (ushort)0)]
    [TestCase(new ushort[] { 0x10F0, 0x100F }, (ushort)0x1000)]
    public void And_Test_UInt16(ushort[] array, ushort orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<ushort>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<ushort>().And());
    }
    
    [CLSCompliant(false)]
    [TestCase(new short[] { 1, 2, 4 }, 0)]
    [TestCase(new short[] { 0x10F0, 0x100F }, 0x1000)]
    [TestCase(new short[] { -1, 32767 }, 32767)]
    public void And_Test_Int16(short[] array, short orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<short>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<short>().And());
    }

    [CLSCompliant(false)]
    [TestCase(new[] { '\x1001', '\x1002', '\x1004' }, '\x1000')]
    [TestCase(new[] { '\x10F0', '\x100F' }, '\x1000')]
    public void And_Test_char(char[] array, char orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<char>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<char>().And());
    }

    [CLSCompliant(false)]
    [TestCase(new uint[] { 1, 2, 4 }, (uint)0)]
    [TestCase(new uint[] { 0x10F0, 0x100F }, (uint)0x1000)]
    public void And_Test_UInt32(uint[] array, uint orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<uint>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<uint>().And());
    }
    
    [CLSCompliant(false)]
    [TestCase(new[] { 1, 2, 4 }, 0)]
    [TestCase(new[] { 0x10F0, 0x100F }, 0x1000)]
    [TestCase(new[] { -1, 2147483647 }, 2147483647)]
    public void And_Test_Int32(int[] array, int orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<int>().And());
    }

    
    [CLSCompliant(false)]
    [TestCase(new ulong[] { 1, 2, 4 }, (ulong)0)]
    [TestCase(new ulong[] { 0x10F0, 0x100F }, (ulong)0x1000)]
    public void And_Test_UInt64(ulong[] array, ulong orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<ulong>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<ulong>().And());
    }

    [CLSCompliant(false)]
    [TestCase(new long[] { 1, 2, 4 }, 0)]
    [TestCase(new long[] { 0x10F0, 0x100F }, 0x1000)]
    [TestCase(new [] { -1L, 9223372036854775807L }, 9223372036854775807L)]
    public void And_Test_Int64(long[] array, long orValue)
    {
        Assert.AreEqual(orValue, array.And());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<long>)null!).And());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<long>().And());
    }

    [Test]
    public void Xor_Test_bool()
    {
        bool[]? data = new bool[10];
        Assert.False(data.Xor());
        data[5] = true;
        Assert.True(data.Xor());
        data[6] = true;
        Assert.False(data.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<bool>)null!).Xor());
    }

    [CLSCompliant(false)]
    [TestCase(new byte[] { 131, 140 }, 15)]
    public void Xor_Test_byte(byte[] array, byte orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<byte>)null!).Xor());
    }
    
    [CLSCompliant(false)]
    [TestCase(new sbyte[] { 85, 15 }, 90)]
    public void Xor_Test_Sbyte(sbyte[] array, sbyte orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<sbyte>)null!).Xor());
    }

    [CLSCompliant(false)]
    [TestCase(new ushort[] { 131, 140 }, (ushort)15)]
    [TestCase(new ushort[] { 0x10F0, 0x100F }, (ushort)0x00FF)]
    public void Xor_Test_UInt16(ushort[] array, ushort orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<ushort>)null!).Xor());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new short[] { 131, 140 }, 15)]
    [TestCase(new short[] { 0x10F0, 0x100F }, 0x00FF)]
    [TestCase(new short[] { -3856, -21846 }, 23130)]
    public void Xor_Test_Int16(short[] array, short orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<short>)null!).Xor());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new[] { '\x1083', '\x118c' }, '\x010f')]
    [TestCase(new[] { '\x10F0', '\x110F' }, '\x01FF')]
    public void Xor_Test_char(char[] array, char orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<char>)null!).Xor());
    }

    [Theory]
    [CLSCompliant(false)]
    [TestCase(new uint[] { 131, 140 }, (uint)15)]
    [TestCase(new uint[] { 0x10F0, 0x100F }, (uint)0x00FF)]
    public void Xor_Test_UInt32(uint[] array, uint orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<uint>)null!).Xor());
    }
    
    [CLSCompliant(false)]
    [TestCase(new[] { 131, 140 }, 15)]
    [TestCase(new[] { 0x10F0, 0x100F }, 0x00FF)]
    public void Xor_Test_Int32(int[] array, int orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null!).Xor());
    }

    [CLSCompliant(false)]
    [TestCase(new ulong[] { 131, 140 }, (ulong)15)]
    [TestCase(new ulong[] { 0x10F0, 0x100F }, (ulong)0x00FF)]
    public void Xor_Test_UInt64(ulong[] array, ulong orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<ulong>)null!).Xor());
    }
    
    [CLSCompliant(false)]
    [TestCase(new long[] { 131, 140 }, 15)]
    [TestCase(new long[] { 0x10F0, 0x100F }, 0x00FF)]
    public void Xor_Test_Int64(long[] array, long orValue)
    {
        Assert.AreEqual(orValue, array.Xor());
        Assert.Throws<ArgumentNullException>(() => ((IEnumerable<long>)null!).Xor());
    }

    [Test]
    public void AllEmpty_Test()
    {
        Assert.False(new[] { "A", "", null }.AllEmpty());
        Assert.True(new[] { "", null }.AllEmpty());
        Assert.Throws<ArgumentNullException>(() => CollectionHelpers.AllEmpty((IEnumerable<string?>)(null!)));
    }

    [Test]
    public async Task AllEmptyAsync_Test()
    {
        Assert.False(await new[] { "A", "", null }.YieldAsync(_ => Task.CompletedTask).AllEmpty());
        Assert.True(await new[] { "", null }.YieldAsync(_ => Task.CompletedTask).AllEmpty());
        Assert.ThrowsAsync<ArgumentNullException>(() => CollectionHelpers.AllEmpty((IAsyncEnumerable<string?>)(null!)));
    }

    [Test]
    public void AnyEmpty_Test()
    {
        Assert.False(new[] { "A", "B", "C" }.AnyEmpty());
        Assert.True(new[] { "A", "" }.AnyEmpty());
        Assert.True(new[] { "A", null }.AnyEmpty());
        Assert.True(new[] { "", null }.AnyEmpty());
        Assert.True(new[] { "A", "", null }.AnyEmpty());
        Assert.Throws<ArgumentNullException>(() => CollectionHelpers.AnyEmpty((IEnumerable<string?>)(null!)));
    }

    [Test]
    public async Task AnyEmptyAsync_Test()
    {
        Assert.False(await new[] { "A", "B", "C" }.YieldAsync(_ => Task.CompletedTask).AnyEmpty());
        Assert.True(await new[] { "A", "" }.YieldAsync(_ => Task.CompletedTask).AnyEmpty());
        Assert.True(await new[] { "A", null }.YieldAsync(_ => Task.CompletedTask).AnyEmpty());
        Assert.True(await new[] { "", null }.YieldAsync(_ => Task.CompletedTask).AnyEmpty());
        Assert.True(await new[] { "A", "", null }.YieldAsync(_ => Task.CompletedTask).AnyEmpty());
        Assert.ThrowsAsync<ArgumentNullException>(() => CollectionHelpers.AnyEmpty((IAsyncEnumerable<string?>)(null!)));
    }

    [Test]
    public void ToPercent_Test_Double()
    {
        int[]? c = new[] { 1, 2, 3, 4, 5 };

        Assert.AreEqual(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, c.ToPercentDouble());
        Assert.AreEqual(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, c.ToPercentDouble(true));
        Assert.AreEqual(new[] { 0.0, 0.25, 0.5, 0.75, 1.0 }, c.ToPercentDouble(false));
        Assert.AreEqual(new[] { 0.1, 0.2, 0.3, 0.4, 0.5 }, c.ToPercentDouble(10));
        Assert.AreEqual(new[] { 0.0, 0.25, 0.5, 0.75, 1.0 }, c.ToPercentDouble(1, 5));
        Assert.Throws<InvalidOperationException>(() => c.ToPercentDouble(1, 1).ToList());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<int>().ToPercentDouble().ToList());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<double>().ToPercent().ToList());
        Assert.AreEqual(
            new[] { 0, 0.25, 0.5, 0.75, 1.0 },
            new[] { 1.0, 2.0, 3.0, 4.0, 5.0 }.ToPercent());

        Assert.AreEqual(
            new[] { 0, 0.25, 0.5, 0.75, 1.0 },
            new[] { 1.0, 2.0, 3.0, 4.0, 5.0 }.ToPercent(false));

        Assert.AreEqual(
            new[] { 0.25, 0.5, 0.75, 1.0 },
            new[] { 1.0, 2.0, 3.0, 4.0 }.ToPercent(true));

        Assert.AreEqual(
            new[] { 0.1, 0.2, 0.3, 0.4 },
            new[] { 1.0, 2.0, 3.0, 4.0 }.ToPercent(10.0));

        Assert.AreEqual(
            new[] { -0.8, double.NaN, -0.4, -0.2 },
            new[] { 1.0, double.NaN, 3.0, 4.0 }.ToPercent(5.0, 10.0));

        Assert.Throws<ArgumentException>(() => new[] { 1.0 }.ToPercent(double.NaN, double.NaN).ToList());
        Assert.Throws<ArgumentException>(() => new[] { 1.0 }.ToPercent(0.0, double.NaN).ToList());
        Assert.Throws<InvalidOperationException>(() => new[] { 1.0, 1.0 }.ToPercent(1.0, 1.0).ToList());
    }

    [Test]
    public async Task ToPercentAsync_Test_Double()
    {
        Assert.AreEqual(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, await Read(GetValuesAsync<int>().ToPercentDouble(5)));
        Assert.AreEqual(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, await Read(GetValuesAsync<double>().ToPercent(5)));
        Assert.AreEqual(new[] { 0.2, 0.4, 0.6, 0.8, 1.0 }, await Read(GetValuesAsync<double>().ToPercent(0, 5)));
        Assert.AreEqual(new[] { 0.2, 0.4, 0.6, 0.8, 1.0, double.NaN }, await Read(GetValuesAsync<double>(double.NaN).ToPercent(0, 5)));
    }

    private static async IAsyncEnumerable<T> GetValuesAsync<T>(T? tail = null) where T : struct
    {
        yield return (T)Convert.ChangeType(1, typeof(T));
        await Task.CompletedTask;
        yield return (T)Convert.ChangeType(2, typeof(T));
        await Task.CompletedTask;
        yield return (T)Convert.ChangeType(3, typeof(T));
        await Task.CompletedTask;
        yield return (T)Convert.ChangeType(4, typeof(T));
        await Task.CompletedTask;
        yield return (T)Convert.ChangeType(5, typeof(T));
        await Task.CompletedTask;
        if (tail is { } t) yield return t;
    }

    private static async Task<T[]> Read<T>(IAsyncEnumerable<T> d)
    {
        List<T>? l = new();
        await foreach (T? j in d) l.Add(j);
        return l.ToArray();
    }

    [Test]
    public async Task ToPercentAsync_Test_Single()
    {
        Assert.AreEqual(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, await Read(GetValuesAsync<int>().ToPercentSingle(5)));
        Assert.AreEqual(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, await Read(GetValuesAsync<float>().ToPercent(5)));
        Assert.AreEqual(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, await Read(GetValuesAsync<float>().ToPercent(0, 5)));
        Assert.AreEqual(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f, float.NaN }, await Read(GetValuesAsync<float>(float.NaN).ToPercent(0, 5)));
    }

    [Test]
    public void ToPercent_Test_Single()
    {
        int[]? c = new[] { 1, 2, 3, 4, 5 };

        Assert.AreEqual(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, c.ToPercentSingle());
        Assert.AreEqual(new[] { 0.2f, 0.4f, 0.6f, 0.8f, 1.0f }, c.ToPercentSingle(true));
        Assert.AreEqual(new[] { 0.0f, 0.25f, 0.5f, 0.75f, 1.0f }, c.ToPercentSingle(false));
        Assert.AreEqual(new[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f }, c.ToPercentSingle(10));
        Assert.AreEqual(new[] { 0.0f, 0.25f, 0.5f, 0.75f, 1.0f }, c.ToPercentSingle(1, 5));
        Assert.Throws<InvalidOperationException>(() => c.ToPercentSingle(1, 1).ToList());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<int>().ToPercentSingle().ToList());
        Assert.AreEqual(
            new[] { 0f, 0.25f, 0.5f, 0.75f, 1.0f },
            new[] { 1f, 2f, 3f, 4f, 5f }.ToPercent());

        Assert.AreEqual(
            new[] { 0f, 0.25f, 0.5f, 0.75f, 1.0f },
            new[] { 1f, 2f, 3f, 4f, 5f }.ToPercent(false));

        Assert.AreEqual(
            new[] { 0.25f, 0.5f, 0.75f, 1.0f },
            new[] { 1f, 2f, 3f, 4f }.ToPercent(true));

        Assert.AreEqual(
            new[] { 0.1f, 0.2f, 0.3f, 0.4f },
            new[] { 1f, 2f, 3f, 4f }.ToPercent(10f));

        Assert.AreEqual(
            new[] { -0.8f, float.NaN, -0.4f, -0.2f },
            new[] { 1f, float.NaN, 3f, 4f }.ToPercent(5f, 10f));

        Assert.Throws<ArgumentException>(() => new[] { 1f }.ToPercent(float.NaN, float.NaN).ToList());
        Assert.Throws<ArgumentException>(() => new[] { 1f }.ToPercent(0f, float.NaN).ToList());
        Assert.Throws<InvalidOperationException>(() => new[] { 1f, 1f }.ToPercent(1f, 1f).ToList());
        Assert.Throws<EmptyCollectionException>(() => Array.Empty<float>().ToPercent().ToList());
    }

    [Test]
    public void NotEmpty_string_Test()
    {
        string?[]? i = new[] { "1", "2", null, "", "3" };
        string[]? o = new[] { "1", "2", "3" };

        Assert.AreEqual(o, i.NotEmpty());
    }

    [Test]
    public void GetTypes_Test()
    {
        Assert.True(AppDomain.CurrentDomain.GetAssemblies().GetTypes<Exception>().ToArray().Length > 10);
    }

    [Test]
    public void WithSignature_Test()
    {
        MethodInfo a = typeof(TestMethods).GetMethod("A")!;
        MethodInfo b = typeof(TestMethods).GetMethod("B")!;
        MethodInfo c = typeof(TestMethods).GetMethod("C")!;
        MethodInfo[] l = typeof(TestMethods).GetMethods().WithSignature<Func<byte>>(new TestMethods()).Select(p => p.Method).ToArray();
        Assert.AreEqual(2, l.Length);
        Assert.True(l.Contains(a));
        Assert.False(l.Contains(b));
        Assert.True(l.Contains(c));
    }
    
    [Test]
    public void WithSignature_static_Test()
    {
        MethodInfo a = typeof(StaticTestMethods).GetMethod("A")!;
        MethodInfo b = typeof(StaticTestMethods).GetMethod("B")!;
        MethodInfo c = typeof(StaticTestMethods).GetMethod("C")!;
        MethodInfo[] l = typeof(StaticTestMethods).GetMethods().WithSignature<Func<byte>>().Select(p => p.Method).ToArray();
        Assert.AreEqual(2, l.Length);
        Assert.True(l.Contains(a));
        Assert.False(l.Contains(b));
        Assert.True(l.Contains(c));
    }
    
    [ExcludeFromCodeCoverage]
    private class TestMethods
    {
#pragma warning disable CA1822
        [ExcludeFromCodeCoverage]public byte A() => 0;
        [ExcludeFromCodeCoverage]public double B() => 0.0;
        [ExcludeFromCodeCoverage]public byte C() => 1;
#pragma warning restore CA1822
    }
    
    [ExcludeFromCodeCoverage]
    private static class StaticTestMethods
    {
        [ExcludeFromCodeCoverage]public static byte A() => 0;
        [ExcludeFromCodeCoverage]public static double B() => 0.0;
        [ExcludeFromCodeCoverage]public static byte C() => 1;
    }

}
