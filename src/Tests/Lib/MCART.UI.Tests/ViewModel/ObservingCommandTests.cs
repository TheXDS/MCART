/*
ObservingCommandTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.UI.Tests.ViewModel;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

public class ObservingCommandTests
{
    private class TestNpcClass : NotifyPropertyChanged
    {
        private string? _TestString;

        public string? TestString
        {
            get => _TestString;
            set => Change(ref _TestString, value);
        }
    }

    [Test]
    public void PropertyChange_Fires_Notification_Test()
    {
        TestNpcClass? i = new();
        ObservingCommand? obs = new ObservingCommand(i, NoAction)
            .SetCanExecute((a, b) => !i.TestString.IsEmpty())
            .RegisterObservedProperty(nameof(TestNpcClass.TestString));

        Assert.False(obs.CanExecute(null));
        i.TestString = "Test";
        Assert.True(obs.CanExecute(null));
    }
    
    [ExcludeFromCodeCoverage]
    private static void NoAction() { }
}

public class CommandBaseTests
{
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
            Assert.AreSame(CexParameter, parameter);
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
        Assert.IsTrue(c.CanExecute());
        c.CanExecuteField = false;
        Assert.IsFalse(c.CanExecute());
    }

    [Test]
    public void Execute_test()
    {
        bool actionRan = false;
        var param = new object();
        var c = new TestClass(p =>
        {
            Assert.AreSame(p, param);
            actionRan = true;
        })
        {
            CanExecuteField = true
        };
        c.Execute(param);
        Assert.IsTrue(actionRan);
        param = null;
        actionRan = false;
        c.Execute();
        Assert.IsTrue(actionRan);
    }

    [Test]
    public void TryExecute_test()
    {
        bool actionRan = false;
        var param = new object();
        var c = new TestClass(p =>
        {
            Assert.AreSame(p, param);
            actionRan = true;
        })
        {
            CanExecuteField = false,
            CexParameter = param
        };
        Assert.IsFalse(c.TryExecute(param));
        Assert.IsFalse(actionRan);
        c.CanExecuteField = true;
        Assert.IsTrue(c.TryExecute(param));
        Assert.IsTrue(actionRan);
        param = null;
        actionRan = false;
        c.CanExecuteField = false;
        c.CexParameter = null;
        Assert.IsFalse(c.TryExecute());
        Assert.IsFalse(actionRan);
        c.CanExecuteField = true;
        Assert.IsTrue(c.TryExecute());
        Assert.IsTrue(actionRan);
    }

    [Test]
    public void CanExecuteChanged_test()
    {
        bool eventFired = false;
        var c = new TestClass(NoAction);

        void C_CanExecuteChanged(object? sender, EventArgs e)
        {
            eventFired = true;
            Assert.AreSame(c, sender);
            Assert.AreEqual(EventArgs.Empty, e);
        }

        c.CanExecuteChanged += C_CanExecuteChanged;
        c.CanExecuteField = true;
        Assert.IsTrue(eventFired);
        c.CanExecuteChanged -= C_CanExecuteChanged;
    }


    [ExcludeFromCodeCoverage]
    private static void NoAction(object? arg)
    {
    }
}