﻿/*
SimpleCommandTests.cs

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
using System.Threading.Tasks;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.UI.Tests.ViewModel;

public class SimpleCommandTests
{
    [Test]
    public void Ctor_1_test()
    {
        bool wiredup = false;
        void a() => wiredup = true;
        var c = new SimpleCommand(a);
        Assert.IsTrue(c.CanExecute());
        c.Execute();
        Assert.IsTrue(wiredup);
    }

    [Test]
    public void Ctor_2_test()
    {
        bool wiredup = false;
        void a() => wiredup = true;
        var c = new SimpleCommand(a, false);
        Assert.IsFalse(c.CanExecute());
        c.SetCanExecute(true);
        Assert.IsTrue(c.CanExecute());
        c.Execute();
        Assert.IsTrue(wiredup);
    }

    [Test]
    public void Ctor_3_test()
    {
        bool wiredup = false;
        object x = new();
        void a(object? o)
        {
            wiredup = true;
            Assert.AreSame(o, x);
        }
        var c = new SimpleCommand(a);
        Assert.IsTrue(c.CanExecute());
        c.Execute(x);
        Assert.IsTrue(wiredup);
    }

    [Test]
    public void Ctor_4_test()
    {
        bool wiredup = false;
        object x = new();
        void a(object? o)
        {
            wiredup = true;
            Assert.AreSame(o, x);
        }
        var c = new SimpleCommand(a, false);
        Assert.IsFalse(c.CanExecute());
        c.SetCanExecute(true);
        Assert.IsTrue(c.CanExecute());
        c.Execute(x);
        Assert.IsTrue(wiredup);
    }

    [Test]
    public void Ctor_5_test()
    {
        bool wiredup = false;
        Task a()
        {
            wiredup = true;
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a);
        Assert.IsTrue(c.CanExecute());
        c.Execute();
        Assert.IsTrue(wiredup);
    }

    [Test]
    public void Ctor_6_test()
    {
        bool wiredup = false;
        Task a()
        {
            wiredup = true;
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a, false);
        Assert.IsFalse(c.CanExecute());
        c.SetCanExecute(true);
        Assert.IsTrue(c.CanExecute());
        c.Execute();
        Assert.IsTrue(wiredup);
    }

    [Test]
    public void Ctor_7_test()
    {
        bool wiredup = false;
        object x = new();
        Task a(object? o)
        {
            wiredup = true;
            Assert.AreSame(o, x);
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a);
        Assert.IsTrue(c.CanExecute());
        c.Execute(x);
        Assert.IsTrue(wiredup);
    }

    [Test]
    public void Ctor_8_test()
    {
        bool wiredup = false;
        object x = new();
        Task a(object? o)
        {
            wiredup = true;
            Assert.AreSame(o, x);
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a, false);
        Assert.IsFalse(c.CanExecute());
        c.SetCanExecute(true);
        Assert.IsTrue(c.CanExecute());
        c.Execute(x);
        Assert.IsTrue(wiredup);
    }
}