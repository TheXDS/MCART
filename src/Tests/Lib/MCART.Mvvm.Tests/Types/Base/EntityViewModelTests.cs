/*
EntityViewModelTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Tests.EventTestHelpers;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

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
        Assert.That(vm.Entity, Is.Null);
        vm.Entity = new TestModel();
        Assert.That(vm.Entity, Is.InstanceOf<TestModel>());
    }

    [Test]
    public void Update_test()
    {
        TestViewModel vm = new() { Entity = new() { Prop = 1 } };
        TestModel m = new() { Prop = 2 };
        TestEvent<TestViewModel, PropertyChangedEventHandler, PropertyChangedEventArgs>(vm, nameof(TestViewModel.PropertyChanged), p => p.Update(m));
        Assert.That(2, Is.EqualTo(vm.Entity.Prop));
    }

    [Test]
    public void Refresh_on_null_entity_test()
    {
        TestViewModel vm = new();
        Assert.That(vm.Entity, Is.Null);
        TestEvent<TestViewModel, PropertyChangedEventHandler, PropertyChangedEventArgs>(vm, nameof(TestViewModel.PropertyChanged), p => p.Refresh(), false);
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
        Assert.That((TestModel)vm, Is.InstanceOf<TestModel>());
    }

    [Test]
    public void IEntityViewModel_default_implementation_test()
    {
        IEntityViewModel vm = new TestViewModel();
        Assert.That(vm.Entity, Is.Null);
        vm.Entity = new TestModel();
        Assert.That(vm.Entity, Is.InstanceOf<TestModel>());
    }

    [Test]
    public void IEntityViewModel_T_default_implementation_test()
    {
        IEntityViewModel<TestModel> vm = new TestViewModel();
        Assert.That(vm.Entity, Is.Null);
        vm.Entity = new TestModel();
        Assert.That(vm.Entity, Is.InstanceOf<TestModel>());
    }
}
