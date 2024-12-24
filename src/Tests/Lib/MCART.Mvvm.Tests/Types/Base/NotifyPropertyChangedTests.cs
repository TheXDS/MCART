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

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class NotifyPropertyChangedTests : NotifyPropertyChanged
{
    private int _value;
    private object? _obj;

    [ExcludeFromCodeCoverage]
    public int Value
    {
        get => _value;
        set => Change(ref _value, value);
    }

    [ExcludeFromCodeCoverage]
    public object? Obj
    {
        get => _obj;
        set => Change(ref _obj, value);
    }

    [ExcludeFromCodeCoverage]
    public int BrokenProperty
    {
        set => Change(ref _value, value, null!);
    }
    [ExcludeFromCodeCoverage]
    public int BrokenProperty2
    {
        set => Change(ref _value, value, string.Empty);
    }

    [ExcludeFromCodeCoverage]
    public object? SelfFalseTestingProperty
    {
        set => Assert.That(Change(ref _obj, value), Is.False);
    }

    [ExcludeFromCodeCoverage]
    public object? SelfTrueTestingProperty
    {
        set => Assert.That(Change(ref _obj, value));
    }

    [ExcludeFromCodeCoverage]
    public int Prop1 { get; set; }
    [ExcludeFromCodeCoverage]
    public int Prop2 { get; set; }
    [ExcludeFromCodeCoverage]
    public int Prop3 { get; set; }

    [Test]
    public void OnPropertyChangedTest()
    {
        bool risen = false;
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;

        void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            risen = true;
            evt = (sender, e);
        }

        PropertyChanged += TestPropertyChanged;
        Value = 1;
        PropertyChanged -= TestPropertyChanged;

        Assert.That(risen);
        Assert.That(evt, Is.Not.Null);
        Assert.That(ReferenceEquals(this, evt!.Value.Sender));
        Assert.That(nameof(Value), Is.EqualTo(evt!.Value.Arguments.PropertyName));
        Assert.That(1, Is.EqualTo(Value));
    }

    [Test]
    public void Change_Contract_Test()
    {
        Assert.Throws<ArgumentNullException>(() => BrokenProperty = 1);
        Assert.Throws<ArgumentException>(() => BrokenProperty2 = 1);
        SelfFalseTestingProperty = null;
        SelfTrueTestingProperty = 33;
        SelfFalseTestingProperty = 33;
    }
}
