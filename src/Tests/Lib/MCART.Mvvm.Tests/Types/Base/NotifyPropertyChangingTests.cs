/*
NotifyPropertyChangedTests.cs

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

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using NUnit.Framework;

public class NotifyPropertyChangingTests
{
    [ExcludeFromCodeCoverage]
    private class TestClass : NotifyPropertyChanging
    {
        private int _value;
        private object? _obj;

        public int Value
        {
            get => _value;
            set => Change(ref _value, value);
        }

        public int BrokenProperty
        {
            set => Change(ref _value, value, null!);
        }

        public object? Obj
        {
            get => _obj;
            set => Change(ref _obj, value);
        }
    }

    [Test]
    public void OnPropertyChangingTest()
    {
        TestClass x = new();
        bool risen = false;
        (object? Sender, PropertyChangingEventArgs Arguments)? evt = null;

        void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
        {
            risen = true;
            evt = (sender, e);
        }

        x.PropertyChanging += OnPropertyChanging;
        x.Value = 1;
        x.PropertyChanging -= OnPropertyChanging;

        Assert.That(risen);
        Assert.That(evt, Is.Not.Null);
        Assert.That(ReferenceEquals(x, evt!.Value.Sender));
        Assert.That(nameof(TestClass.Value), Is.EqualTo(evt!.Value.Arguments.PropertyName));
        Assert.That(1, Is.EqualTo(x.Value));
    }

    [Test]
    public void Change_Contract_Test()
    {
        TestClass x = new();
        Assert.Throws<ArgumentNullException>(() => x.BrokenProperty = 2);
    }

    [Test]
    public void Change_With_Unchanging_Values_Test()
    {
        TestClass x = new() { Obj = null, Value = 0 };
        bool risen = false;
        void OnPropertyChanging(object? sender, PropertyChangingEventArgs e) => risen = true;
        x.PropertyChanging += OnPropertyChanging;
        x.Obj = null;
        Assert.That(risen, Is.False);
        Assert.That(x.Obj, Is.Null);
        object o = new();
        x.Obj = o;
        Assert.That(risen);
        Assert.That(o, Is.SameAs(x.Obj));
        risen = false;
        x.Obj = o;
        Assert.That(risen, Is.False);
        Assert.That(o, Is.SameAs(x.Obj));
        x.Value = 0;
        Assert.That(risen, Is.False);
        Assert.That(0, Is.EqualTo(x.Value));
        x.Value = 1;
        Assert.That(risen);
        Assert.That(1, Is.EqualTo(x.Value));
        x.PropertyChanging -= OnPropertyChanging;
    }
}
