﻿/*
CommandBaseTests.cs

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
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class CommandBaseTests
{
    [ExcludeFromCodeCoverage]
    private class TestClass : CommandBase
    {
        private bool canExecuteField;
        public object? CexParameter;

        public bool CanExecuteField
        {
            get => canExecuteField;
            set
            {
                canExecuteField = value;
                RaiseCanExecuteChanged();
            }
        }

        public TestClass(Action<object?> action) : base(action)
        {
        }

        public override bool CanExecute(object? parameter)
        {
            Assert.That(CexParameter, Is.SameAs(parameter));
            return CanExecuteField;
        }
    }

    [Test]
    public void Ctor_contract_test()
    {
        Assert.Throws<ArgumentNullException>(() => _ = new TestClass(null!));
    }

    [Test]
    public void CanExecute_test()
    {
        var c = new TestClass(NoAction)
        {
            CexParameter = null,
            CanExecuteField = true
        };
        Assert.That(c.CanExecute());
        c.CanExecuteField = false;
        Assert.That(c.CanExecute(), Is.False);
    }

    [Test]
    public void Execute_test()
    {
        bool actionRan = false;
        var param = new object();
        var c = new TestClass(p =>
        {
            Assert.That(p, Is.SameAs(param));
            actionRan = true;
        })
        {
            CanExecuteField = true
        };
        c.Execute(param);
        Assert.That(actionRan);
        param = null;
        actionRan = false;
        c.Execute();
        Assert.That(actionRan);
    }

    [Test]
    public void TryExecute_test()
    {
        bool actionRan = false;
        var param = new object();
        var c = new TestClass(p =>
        {
            Assert.That(p, Is.SameAs(param));
            actionRan = true;
        })
        {
            CanExecuteField = false,
            CexParameter = param
        };
        Assert.That(c.TryExecute(param),Is.False);
        Assert.That(actionRan, Is.False);
        c.CanExecuteField = true;
        Assert.That(c.TryExecute(param));
        Assert.That(actionRan);
        param = null;
        actionRan = false;
        c.CanExecuteField = false;
        c.CexParameter = null;
        Assert.That(c.TryExecute(), Is.False);
        Assert.That(actionRan, Is.False);
        c.CanExecuteField = true;
        Assert.That(c.TryExecute());
        Assert.That(actionRan);
    }

    [Test]
    public void CanExecuteChanged_test()
    {
        bool eventFired = false;
        var c = new TestClass(NoAction);

        void C_CanExecuteChanged(object? sender, EventArgs e)
        {
            eventFired = true;
            Assert.That(c, Is.SameAs(sender));
            Assert.That(EventArgs.Empty, Is.EqualTo(e));
        }

        c.CanExecuteChanged += C_CanExecuteChanged;
        c.CanExecuteField = true;
        Assert.That(eventFired);
        c.CanExecuteChanged -= C_CanExecuteChanged;
    }

    [ExcludeFromCodeCoverage]
    private static void NoAction(object? arg)
    {
    }
}
