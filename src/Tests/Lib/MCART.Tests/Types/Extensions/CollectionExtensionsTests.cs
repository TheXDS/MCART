/*
CollectionExtensionsTests.cs

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

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests.Types.Extensions;

public class CollectionExtensionsTests
{
    [Test]
    public void RemoveOf_Test()
    {
        Exception? e = new();
        DateTime d = DateTime.Now;
        object? o = new();
        InvalidOperationException? e2 = new();
        ICollection<object> c = new[] { e, o, 5, d, e2 }.ToList();
        c.RemoveOf<object, Exception>();

        Assert.True(c.Contains(d));
        Assert.True(c.Contains(o));
        Assert.True(c.Contains(5));
        Assert.False(c.Contains(e));
        Assert.False(c.Contains(e2));
    }

    [Test]
    public void RemoveAll_With_Predicate_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        List<int> r = new();
        l.RemoveAll(p => p % 2 == 1, p => r.Add(p));
        Assert.AreEqual(new[] { 2, 4, 6, 8, 10 }, l.ToArray());
        Assert.AreEqual(new[] { 1, 3, 5, 7, 9 }, r.ToArray());
    }

    [Test]
    public void RemoveAll_Without_Predicate_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        List<int> r = new();
        l.RemoveAll(p => r.Add(p));
        Assert.IsEmpty(l);
        Assert.AreEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, r.ToArray());
    }

    [Test]
    public void RemoveAll_With_Predicate_No_Action_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        TheXDS.MCART.Types.Extensions.CollectionExtensions.RemoveAll(l, p => p % 2 == 1);
        Assert.AreEqual(new[] { 2, 4, 6, 8, 10 }, l.ToArray());
    }

    [Test]
    public void RemoveAll_Without_Params_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        l.RemoveAll();
        Assert.IsEmpty(l);
    }

    [Test]
    public void Push_Test()
    {
        List<Guid> l = new();
        Guid g = l.Push();
        Assert.Contains(g, l);
    }

    [Test]
    public void PushInto_Test()
    {
        List<Guid> l = new();
        Guid g = Guid.NewGuid().PushInto(l);
        Assert.Contains(g, l);
    }

    [Test]
    public void Push_With_Base_Type_Test()
    {
        List<object> l = new();
        Guid g = l.Push<Guid, object>();
        Assert.Contains(g, l);
    }

    [Test]
    public void AddRange_Test()
    {
        Collection<int> c = new();
        c.AddRange(new[] { 1, 2, 3, 4 });
        Assert.AreEqual(new[] { 1, 2, 3, 4 }, c.ToArray());
    }

    private class CloneableTestClass : ICloneable<CloneableTestClass>
    {
        public int Value { get; set; }
    }

    [Test]
    public void AddClones_Test()
    {
        CloneableTestClass? j = new()
        {
            Value = 1
        };
        CloneableTestClass? k = new()
        {
            Value = 2
        };
        Collection<CloneableTestClass> l = new() { j, k };
        Collection<CloneableTestClass> m = new();
        m.AddClones(l);
        CloneableTestClass? n = m.Single(p => p.Value == j.Value);
        CloneableTestClass? o = m.Single(p => p.Value == k.Value);

        Assert.NotNull(n);
        Assert.NotNull(o);
        Assert.AreNotSame(j, n);
        Assert.AreNotSame(k, o);
    }

    [Test]
    public void AddClone_Test()
    {
        CloneableTestClass? j = new()
        {
            Value = 1
        };
        Collection<CloneableTestClass> m = new();
        m.AddClone(j);
        CloneableTestClass? n = m.Single(p => p.Value == j.Value);

        Assert.NotNull(n);
        Assert.AreNotSame(j, n);
    }
}
