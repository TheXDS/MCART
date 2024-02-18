// ObservableListWrap_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2024 César Andrés Morgan
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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Tests;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Tests.EventTestHelpers;

namespace TheXDS.MCART.Mvvm.Tests.Types;

public class NotifyPropertyChange_Tests
{
    [ExcludeFromCodeCoverage]
    private class TestNpcClass : NotifyPropertyChange
    {
        private int _IntProperty;
        private string? _StringProperty;

        public int IntProperty
        {
            get => _IntProperty;
            set => Change(ref _IntProperty, value);
        }

        public int BrokenIntProperty
        {
            get => _IntProperty;
            set => Change(ref _IntProperty, value, "Test");
        }

        public string? StringProperty
        {
            get => _StringProperty;
            set => Change(ref _StringProperty, value);
        }

        public void InvalidChangeCall()
        {
            Change(ref _IntProperty, int.MaxValue);
        }

        public void CallOnPropertyChanging(string? propertyName)
        {
            OnPropertyChanging(propertyName!);
        }

        public void CallOnPropertyChanged(string? propertyName)
        {
            OnPropertyChanged(propertyName!);
        }
    }

    [Test]
    public void Change_basic_test()
    {
        static void SetIntProperty(TestNpcClass obj) => obj.IntProperty = 1;
        var obj = new TestNpcClass();
        TestEvents(obj, SetIntProperty,
            new EventTestEntry<TestNpcClass, PropertyChangingEventHandler, PropertyChangingEventArgs>(nameof(TestNpcClass.PropertyChanging),
                e => Assert.That(e.PropertyName, Is.EqualTo(nameof(TestNpcClass.IntProperty)))),
            new EventTestEntry<TestNpcClass, PropertyChangedEventHandler, PropertyChangedEventArgs>(nameof(TestNpcClass.PropertyChanged),
                e => Assert.That(e.PropertyName, Is.EqualTo(nameof(TestNpcClass.IntProperty))))
            );
        Assert.That(obj.IntProperty, Is.EqualTo(1));
    }

    [Test]
    public void Change_skips_events_for_same_value_test()
    {
        var obj = new TestNpcClass() { IntProperty = default };

        TestEvents(obj, o => o.IntProperty = default,
            new EventTestEntry<TestNpcClass, PropertyChangingEventHandler, PropertyChangingEventArgs>(nameof(TestNpcClass.PropertyChanging), false),
            new EventTestEntry<TestNpcClass, PropertyChangedEventHandler, PropertyChangedEventArgs>(nameof(TestNpcClass.PropertyChanged), false)
        );
    }

    [Test]
    public void Change_skips_events_for_both_null_test()
    {
        var obj = new TestNpcClass() { StringProperty = null };

        TestEvents(obj, o => o.StringProperty = null,
            new EventTestEntry<TestNpcClass, PropertyChangingEventHandler, PropertyChangingEventArgs>(nameof(TestNpcClass.PropertyChanging), false),
            new EventTestEntry<TestNpcClass, PropertyChangedEventHandler, PropertyChangedEventArgs>(nameof(TestNpcClass.PropertyChanged), false)
        );
    }

    [Test]
    public void PropertyChangeObserver_gets_called_test()
    {
        bool pcoWasCalled = false;
        var obj = new TestNpcClass();
        obj.Subscribe((o, p) =>
        {
            pcoWasCalled = true;
            Assert.That(o, Is.SameAs(obj));
            Assert.That(p.Name, Is.EqualTo(nameof(TestNpcClass.IntProperty)));
        });
        obj.IntProperty = 1;
        Assert.That(pcoWasCalled);
    }

    [Test]
    public void OnPropertyChanging_contract_test()
    {
        var obj = new TestNpcClass();
        Assert.That(() => obj.CallOnPropertyChanging(null), Throws.ArgumentNullException);
    }

    [Test]
    public void OnPropertyChanged_contract_test()
    {
        var obj = new TestNpcClass();
        Assert.That(() => obj.CallOnPropertyChanged(null), Throws.ArgumentNullException);
    }

    [Test]
    public void PropertyChange_forwards_changes_test()
    {
        var source = new TestNpcClass();
        var target = new TestNpcClass();
        source.ForwardChange(target);
        TestEvents(target, _ => source.IntProperty = 3,
            new EventTestEntry<TestNpcClass, PropertyChangedEventHandler, PropertyChangedEventArgs>(nameof(TestNpcClass.PropertyChanged),
                e => Assert.That(e.PropertyName, Is.EqualTo(nameof(TestNpcClass.IntProperty))))
            );
    }

    [Test]
    public void PropertyChange_notifies_registered_observers_test()
    {
        bool observerCalled = false;
        var source = new TestNpcClass();
        source.Subscribe((o, e) => {
            observerCalled = true;
            Assert.That(o, Is.SameAs(source));
            Assert.That(e.Name, Is.EqualTo(nameof(TestNpcClass.IntProperty)));
        });
        source.IntProperty = 5;
        Assert.That(observerCalled);
    }

    [Test]
    public void Subscriptions_can_be_removed_test()
    {
        static void TestDelegate(object instance, PropertyInfo property) => Assert.Fail();
        var source = new TestNpcClass();
        source.Subscribe(TestDelegate);
        source.Unsubscribe(TestDelegate);
        source.IntProperty = 5;
    }

#if EnforceContracts
    [Test]
    public void Change_throws_if_called_incorrectly_test()
    {
        var obj = new TestNpcClass();
        Assert.That(obj.InvalidChangeCall, Throws.InvalidOperationException);
        Assert.That((TestDelegate)(() => obj.BrokenIntProperty = default), Throws.InvalidOperationException);
    }
#endif
}
