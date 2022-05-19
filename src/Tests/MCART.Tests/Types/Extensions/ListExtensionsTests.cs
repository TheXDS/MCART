/*
ListExtensionsTests.cs

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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

public class ListExtensionsTests
{
    [Test]
    public void RemoveOf_Test()
    {
        Exception? e = new();
        DateTime d = DateTime.Now;
        object? o = new();
        InvalidOperationException? e2 = new();
        List<object>? c = new object[] { e, o, 5, d, e2 }.ToList();
        c.RemoveOf<Exception>();

        Assert.Contains(d, c);
        Assert.Contains(o, c);
        Assert.Contains(5, c);
        Assert.False(c.Contains(e));
        Assert.False(c.Contains(e2));
    }

    [Test]
    public void Shuffle_Test()
    {
        List<int>? l = Enumerable.Range(0, 100).ToList();
        l.Shuffle();
        Assert.False(l.All(p => p == l.FindIndexOf(p)));
        l = Enumerable.Range(0, 100).ToList();
        l.Shuffle(30, 59);
        foreach (int p in l.Take(30))
        {
            Assert.AreEqual(p, l.FindIndexOf(p));
        }
        Assert.False(l.Skip(30).Take(30).All(p => l.FindIndexOf(p) == p));
        foreach (int p in l.Skip(60))
        {
            Assert.AreEqual(p, l.FindIndexOf(p));
        }
    }

    [Test]
    public void Shuffle_Contract_Test()
    {
        List<int>? l = Enumerable.Range(0, 100).ToList();
        Assert.IsAssignableFrom<EmptyCollectionException>(Assert.Throws<InvalidOperationException>(() => Array.Empty<int>().ToList().Shuffle())!.InnerException);
        Assert.Throws<IndexOutOfRangeException>(() => l.Shuffle(-1, 100, 1));
        Assert.Throws<IndexOutOfRangeException>(() => l.Shuffle(0, 1000, 1));
        Assert.AreEqual("deepness", Assert.Throws<ArgumentOutOfRangeException>(() => l.Shuffle(10, 50, 1000))!.ParamName);
        Assert.Throws<ArgumentException>(() => l.Shuffle(10, 0, 1));
    }

    [Test]
    public void ApplyRotate_Test()
    {
        List<int>? l = Enumerable.Range(0, 10).ToList();
        l.ApplyRotate(-2);
        Assert.AreEqual(8, l[0]);
        Assert.AreEqual(9, l[1]);
        Assert.AreEqual(0, l[2]);
        Assert.AreEqual(1, l[3]);
        Assert.AreEqual(2, l[4]);
        Assert.AreEqual(3, l[5]);
        Assert.AreEqual(4, l[6]);
        Assert.AreEqual(5, l[7]);
        Assert.AreEqual(6, l[8]);
        Assert.AreEqual(7, l[9]);
        l.ApplyRotate(5);
        Assert.AreEqual(3, l[0]);
        Assert.AreEqual(4, l[1]);
        Assert.AreEqual(5, l[2]);
        Assert.AreEqual(6, l[3]);
        Assert.AreEqual(7, l[4]);
        Assert.AreEqual(8, l[5]);
        Assert.AreEqual(9, l[6]);
        Assert.AreEqual(0, l[7]);
        Assert.AreEqual(1, l[8]);
        Assert.AreEqual(2, l[9]);
    }

    [Test]
    public void ApplyShift_Test()
    {
        List<int>? l = Enumerable.Range(0, 10).ToList();
        l.ApplyShift(-2);
        Assert.AreEqual(0, l[0]);
        Assert.AreEqual(0, l[1]);
        Assert.AreEqual(0, l[2]);
        Assert.AreEqual(1, l[3]);
        Assert.AreEqual(2, l[4]);
        Assert.AreEqual(3, l[5]);
        Assert.AreEqual(4, l[6]);
        Assert.AreEqual(5, l[7]);
        Assert.AreEqual(6, l[8]);
        Assert.AreEqual(7, l[9]);
        l.ApplyShift(5);
        Assert.AreEqual(3, l[0]);
        Assert.AreEqual(4, l[1]);
        Assert.AreEqual(5, l[2]);
        Assert.AreEqual(6, l[3]);
        Assert.AreEqual(7, l[4]);
        Assert.AreEqual(0, l[5]);
        Assert.AreEqual(0, l[6]);
        Assert.AreEqual(0, l[7]);
        Assert.AreEqual(0, l[8]);
        Assert.AreEqual(0, l[9]);
    }

    [Test]
    public async Task Locked_Test()
    {
        IList l = new[] { 1, 2, 3, 4 }.ToList();
        bool f1 = false;
        Task? t1 = Task.Run(() => l.Locked(i =>
        {
            Thread.Sleep(500);
            f1 = true;
            Assert.IsAssignableFrom<List<int>>(i);
        }));
        // Este debe ser tiempo suficiente para que la tarea t1 inicie ejecución.
        await Task.Delay(250);
        Task? t2 = Task.Run(() => l.Locked(i =>
        {
            Assert.AreSame(l, i);
            Assert.True(f1);
        }));
        await Task.WhenAll(t1, t2);
    }

    [Test]
    public void Swap_Test()
    {
        List<string>? l = new[] { "a", "b", "c", "d" }.ToList();
        l.Swap("b", "c");
        Assert.AreEqual(new[] { "a", "c", "b", "d" }, l.ToArray());
        Assert.Throws<InvalidOperationException>(() => l.Swap("b", "e"));
    }
}
