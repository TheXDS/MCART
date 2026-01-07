/*
PropertyInfoExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.MCART.Types.Extensions;

public class PropertyInfoExtensionsTests
{
    [ExcludeFromCodeCoverage]
    private class Test : IDisposable
    {
        [DefaultValue(1)] public int? Prop1 { get; set; } = 1;
        public int? Prop2 { get; set; } = 2;
        public int? Prop3 { get; set; }

        public void Dispose()
        {
        }
    }

    [ExcludeFromCodeCoverage]
    private class Test2
    {
        public int Prop1 { get; } = 10;
    }

    [ExcludeFromCodeCoverage]
    private static class Test3
    {
        [DefaultValue(1)]
        public static int? Prop1 { get; set; } = 1;
        public static int? Prop2 { get; set; }
    }

    [Test]
    public void SetDefault_Test()
    {
        Test? o = new() { Prop1 = 9, Prop2 = 9, Prop3 = 9 };
        foreach (System.Reflection.PropertyInfo? j in o.GetType().GetProperties())
        {
            j.SetDefault(o);
        }
        Assert.That(1, Is.EqualTo(o.Prop1));
        Assert.That(2, Is.EqualTo(o.Prop2));
        Assert.That(o.Prop3, Is.Null);
    }

    [Test]
    public void SetDefault_Contract_Test()
    {
        Test2? o = new();
        Assert.Throws<InvalidOperationException>(() => o.GetType().GetProperties()[0].SetDefault(o));
        Assert.Throws<MissingMemberException>(() => typeof(Exception).GetProperty("Message")!.SetDefault(o));
    }

    [Test]
    public void SetDefault_Static_Property_Test()
    {
        Test3.Prop1 = 9;
        Test3.Prop2 = 9;

        foreach (System.Reflection.PropertyInfo? j in typeof(Test3).GetProperties())
        {
            j.SetDefault();
        }
        Assert.That(1, Is.EqualTo(Test3.Prop1));
        Assert.That(Test3.Prop2, Is.Null);
    }

    [Test]
    public void IsReadWrite_Test()
    {
        Assert.That(typeof(Test).GetProperties().All(p => p.IsReadWrite()));
        Assert.That(typeof(Test2).GetProperties().All(p => !p.IsReadWrite()));
    }
}
