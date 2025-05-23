﻿// NotifyPropertyChangeBaseExtensions_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Mvvm.Tests.Types.Extensions;

public class NotifyPropertyChangeBaseExtensions_Tests
{
    private class TestClass : ViewModelBase
    {
        private int _testProperty;
        private int _test2Property;

        public int TestProperty
        {
            get => _testProperty;
            set => Change(ref _testProperty, value);
        }

        public int Test2Property
        {
            get => _test2Property;
            set => Change(ref _test2Property, value);
        }
    }

    private class Test2Class : TestClass;

    [Test]
    public void Subscribe_triggers_callback()
    {
        var test = new TestClass();
        bool triggered = false;
        test.Subscribe(x => x.TestProperty, (i, p, n) => triggered = true);
        test.TestProperty = 1;
        Assert.That(triggered, Is.True);
        Assert.That(test.TestProperty, Is.EqualTo(1));
    }

    [Test]
    public void Subscribe_triggers_callback_on_derivate_class()
    {
        var test = new Test2Class();
        bool triggered = false;
        test.Subscribe(x => x.TestProperty, (i, p, n) => triggered = true);
        test.TestProperty = 1;
        Assert.That(triggered, Is.True);
        Assert.That(test.TestProperty, Is.EqualTo(1));
    }

    [Test]
    public void Subscribe_does_not_trigger_on_unrelated_prop()
    {
        var test = new Test2Class();
        test.Subscribe(x => x.TestProperty, (i, p, n) => Assert.Fail());
        test.Test2Property = 1;
        Assert.That(test.Test2Property, Is.EqualTo(1));
    }

    [Test]
    public void Unsubscribe_removes_subscription()
    {
        var test = new Test2Class();
        test.Subscribe(x => x.TestProperty, (i, p, n) => Assert.Fail());
        test.Unsubscribe(x => x.TestProperty);
        test.TestProperty = 1;
        Assert.That(test.TestProperty, Is.EqualTo(1));
    }
}
