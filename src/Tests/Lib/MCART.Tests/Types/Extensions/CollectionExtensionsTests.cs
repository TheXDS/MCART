/*
CollectionExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.Collections.ObjectModel;
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

        Assert.That(c.Contains(d));
        Assert.That(c.Contains(o));
        Assert.That(c.Contains(5));
        Assert.That(c.Contains(e), Is.False);
        Assert.That(c.Contains(e2), Is.False);
    }

    [Test]
    public void RemoveAll_With_Predicate_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        List<int> r = new();
        l.RemoveAll(p => p % 2 == 1, p => r.Add(p));
        Assert.That(new[] { 2, 4, 6, 8, 10 }, Is.EqualTo(l.ToArray()));
        Assert.That(new[] { 1, 3, 5, 7, 9 }, Is.EqualTo(r.ToArray()));
    }

    [Test]
    public void RemoveAll_Without_Predicate_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        List<int> r = new();
        l.RemoveAll(p => r.Add(p));
        Assert.That(l, Is.Empty);
        Assert.That(r.ToArray(), Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
    }

    [Test]
    public void RemoveAll_With_Predicate_No_Action_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        MCART.Types.Extensions.CollectionExtensions.RemoveAll(l, p => p % 2 == 1);
        Assert.That(l.ToArray(), Is.EquivalentTo(new[] { 2, 4, 6, 8, 10 }));
    }

    [Test]
    public void RemoveAll_Without_Params_Test()
    {
        List<int>? l = Enumerable.Range(1, 10).ToList();
        l.RemoveAll();
        Assert.That(l, Is.Empty);
    }

    [Test]
    public void Push_Test()
    {
        List<Guid> l = new();
        Guid g = l.Push();
        Assert.That(l, Contains.Item(g));
    }

    [Test]
    public void PushInto_Test()
    {
        List<Guid> l = new();
        Guid g = Guid.NewGuid().PushInto(l);
        Assert.That(l, Contains.Item(g));
    }

    [Test]
    public void Push_With_Base_Type_Test()
    {
        List<object> l = new();
        Guid g = l.Push<Guid, object>();
        Assert.That(l, Contains.Item(g));
    }

    [Test]
    public void AddRange_Test()
    {
        Collection<int> c = new();
        c.AddRange(new[] { 1, 2, 3, 4 });
        Assert.That(c, Is.EquivalentTo(new[] { 1, 2, 3, 4 }));
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

        Assert.That(n, Is.Not.Null);
        Assert.That(o, Is.Not.Null);
        Assert.That(j, Is.Not.SameAs(n));
        Assert.That(k, Is.Not.SameAs(o));
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

        Assert.That(n, Is.Not.Null);
        Assert.That(j, Is.Not.SameAs(n));
    }
}
