/*
SimpleCommandTests.cs

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

using System.Windows.Input;
using TheXDS.MCART.Component;

namespace TheXDS.MCART.Mvvm.Tests.Component;

public class SimpleCommandTests
{
    [Test]
    public void Ctor_1_test()
    {
        bool wiredUp = false;
        void a() => wiredUp = true;
        var c = new SimpleCommand(a);
        Assert.That(c.CanExecute());
        c.Execute();
        Assert.That(wiredUp);
    }

    [Test]
    public void Ctor_2_test()
    {
        bool wiredUp = false;
        void a() => wiredUp = true;
        var c = new SimpleCommand(a, false);
        Assert.That(c.CanExecute(), Is.False);
        c.SetCanExecute(true);
        Assert.That(c.CanExecute());
        c.Execute();
        Assert.That(wiredUp);
    }

    [Test]
    public void Ctor_3_test()
    {
        bool wiredUp = false;
        object x = new();
        void a(object? o)
        {
            wiredUp = true;
            Assert.That(o, Is.SameAs(x));
        }
        var c = new SimpleCommand(a);
        Assert.That(c.CanExecute());
        c.Execute(x);
        Assert.That(wiredUp);
    }

    [Test]
    public void Ctor_4_test()
    {
        bool wiredUp = false;
        object x = new();
        void a(object? o)
        {
            wiredUp = true;
            Assert.That(o, Is.SameAs(x));
        }
        var c = new SimpleCommand(a, false);
        Assert.That(c.CanExecute(), Is.False);
        c.SetCanExecute(true);
        Assert.That(c.CanExecute());
        c.Execute(x);
        Assert.That(wiredUp);
    }

    [Test]
    public void Ctor_5_test()
    {
        bool wiredUp = false;
        Task a()
        {
            wiredUp = true;
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a);
        Assert.That(c.CanExecute());
        c.Execute();
        Assert.That(wiredUp);
    }

    [Test]
    public void Ctor_6_test()
    {
        bool wiredUp = false;
        Task a()
        {
            wiredUp = true;
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a, false);
        Assert.That(c.CanExecute(), Is.False);
        c.SetCanExecute(true);
        Assert.That(c.CanExecute());
        c.Execute();
        Assert.That(wiredUp);
    }

    [Test]
    public void Ctor_7_test()
    {
        bool wiredUp = false;
        object x = new();
        Task a(object? o)
        {
            wiredUp = true;
            Assert.That(o, Is.SameAs(x));
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a);
        Assert.That(c.CanExecute());
        c.Execute(x);
        Assert.That(wiredUp);
    }

    [Test]
    public void Ctor_8_test()
    {
        bool wiredUp = false;
        object x = new();
        Task a(object? o)
        {
            wiredUp = true;
            Assert.That(o, Is.SameAs(x));
            return Task.CompletedTask;
        }
        var c = new SimpleCommand(a, false);
        Assert.That(c.CanExecute(), Is.False);
        c.SetCanExecute(true);
        Assert.That(c.CanExecute());
        c.Execute(x);
        Assert.That(wiredUp);
    }
}

public class SimpleCommand_T_Tests
{
    [Test]
    public void Ctor_with_Action_T_Bool()
    {
        var cmd = new SimpleCommand<int>(_ => { }, true);
        Assert.That(cmd, Is.AssignableTo<ICommand>());
        Assert.That(cmd.CanExecute(), Is.True);
    }

    [Test]
    public void Ctor_with_Action_T()
    {
        var cmd = new SimpleCommand<int>(_ => { });
        Assert.That(cmd, Is.AssignableTo<ICommand>());
        Assert.That(cmd.CanExecute(), Is.True);
    }
    [Test]
    public void Ctor_with_Task_Bool()
    {
        var cmd = new SimpleCommand<int>(_ => Task.CompletedTask, true);
        Assert.That(cmd, Is.AssignableTo<ICommand>());
        Assert.That(cmd.CanExecute(), Is.True);
    }

    [Test]
    public void Ctor_with_Task_T()
    {
        var cmd = new SimpleCommand<int>(_ => Task.CompletedTask);
        Assert.That(cmd, Is.AssignableTo<ICommand>());
        Assert.That(cmd.CanExecute(), Is.True);
    }

    [Test]
    public void Execute_action_passes_value()
    {
        bool executed = false;
        var cmd = new SimpleCommand<int>(x =>
        {
            executed = true;
            Assert.That(x, Is.EqualTo(5));
        }, true);
        cmd.Execute(5);
        Assert.That(executed, Is.True);
    }

    [Test]
    public void Execute_action_with_no_params_passes_default()
    {
        bool executed = false;
        var cmd = new SimpleCommand<int>(x =>
        {
            executed = true;
            Assert.That(x, Is.EqualTo(default(int)));
        }, true);
        cmd.Execute();
        Assert.That(executed, Is.True);
    }

    [Test]
    public void Execute_Task_passes_value()
    {
        bool executed = false;
        var cmd = new SimpleCommand<int>(x =>
        {
            executed = true;
            Assert.That(x, Is.EqualTo(5));
            return Task.CompletedTask;
        }, true);
        cmd.Execute(5);
        Assert.That(executed, Is.True);
    }

    [Test]
    public void Execute_Task_with_no_params_passes_default()
    {
        bool executed = false;
        var cmd = new SimpleCommand<int>(x =>
        {
            executed = true;
            Assert.That(x, Is.EqualTo(default(int)));
            return Task.CompletedTask;
        }, true);
        cmd.Execute();
        Assert.That(executed, Is.True);
    }
}
