/*
EntityViewModelTests.cs

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
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.UI.Tests.ViewModel;

public class EntityViewModelTests
{
    [ExcludeFromCodeCoverage]
    private class TestModel
    {
        public int Prop { get; set; }
    }

    [ExcludeFromCodeCoverage]
    private class TestViewModel : EntityViewModel<TestModel>
    {
    }

    [Test]
    public void Entity_test()
    {
        TestViewModel vm = new();
        Assert.IsNull(vm.Entity);
        vm.Entity = new TestModel();
        Assert.IsInstanceOf<TestModel>(vm.Entity);
    }

    [Test]
    public void Update_test()
    {
        bool eventTriggered = false;
        void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            eventTriggered = true;
        }

        TestViewModel vm = new()
        {
            Entity = new()
            {
                Prop = 1
            }
        };

        TestModel m = new()
        {
            Prop = 2
        };

        vm.PropertyChanged += Vm_PropertyChanged;
        vm.Update(m);
        vm.PropertyChanged -= Vm_PropertyChanged;
        Assert.IsTrue(eventTriggered);
        Assert.AreEqual(2, vm.Entity.Prop);
    }

    [Test]
    public void Refresh_on_null_entity_test()
    {
        bool eventTriggered = false;
        void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            eventTriggered = true;
        }
        TestViewModel vm = new();
        Assert.IsNull(vm.Entity);
        vm.PropertyChanged += Vm_PropertyChanged;
        vm.Refresh();
        vm.PropertyChanged -= Vm_PropertyChanged;
        Assert.IsFalse(eventTriggered);
    }

    [Test]
    public void Implicit_conversion_test()
    {
        TestViewModel vm = new()
        {
            Entity = new()
            {
                Prop = 1
            }
        };
        Assert.IsInstanceOf<TestModel>((TestModel)vm);
    }
}
