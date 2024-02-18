/*
ViewModelBaseTests.cs

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class ViewModelBaseTests
{
    [ExcludeFromCodeCoverage]
    private class TestViewModel : ViewModelBase
    {
        private int _Prop;
        public bool PropChangedExecuted = false;

        public int Prop
        {
            get => _Prop;
            set => Change(ref _Prop, value);
        }

        public TestViewModel()
        {
        }

        public void WireUpBySelector()
        {
            Observe(() => Prop, OnPropChanged);
        }

        public void WireUpByName()
        {
            Observe(nameof(Prop), OnPropChanged);
        }

        public void WireUpByName(Action action)
        {
            Observe(nameof(Prop), action);
        }

        private void OnPropChanged()
        {
            PropChangedExecuted = true;
        }

        public void BusyOpTest(Action action)
        {
            BusyOp(action);
        }

        public Task BusyOpTest(Task task)
        {
            return BusyOp(task);
        }

        public T BusyOpTest<T>(Func<T> action)
        {
            return BusyOp(action);
        }

        public Task<T> BusyOpTest<T>(Task<T> task)
        {
            return BusyOp(task);
        }
    }

    [Test]
    public void Observe_with_prop_selector_test()
    {
        TestViewModel vm = new();
        vm.WireUpBySelector();
        vm.Prop = 1;
        Assert.That(vm.PropChangedExecuted);
    }

    [Test]
    public void Observe_with_name_test()
    {
        bool wiredUp = false;
        void Test() => wiredUp = true;
        TestViewModel vm = new();
        vm.WireUpByName();
        vm.WireUpByName(Test);
        vm.Prop = 1;
        Assert.That(vm.PropChangedExecuted);
        Assert.That(wiredUp);
    }

    [Test]
    public void BusyOp_void_test()
    {
        TestViewModel vm = new();
        Assert.That(vm.IsBusy, Is.False);
        vm.BusyOpTest(() => Assert.That(vm.IsBusy));
        Assert.That(vm.IsBusy, Is.False);
    }

    [Test]
    public void BusyOp_task_test()
    {
        TestViewModel vm = new();
        Task testTask() => Task.CompletedTask;
        Assert.That(vm.IsBusy, Is.False);
        vm.BusyOpTest(testTask());
        Assert.That(vm.IsBusy, Is.False);
    }

    [Test]
    public void BusyOp_T_test()
    {
        TestViewModel vm = new();
        Assert.That(vm.IsBusy, Is.False);
        var r = vm.BusyOpTest(() =>
        {
            Assert.That(vm.IsBusy);
            return 1;
        });
        Assert.That(vm.IsBusy, Is.False);
        Assert.That(1, Is.EqualTo(r));
    }

    [Test]
    public async Task BusyOp_task_T_test()
    {
        TestViewModel vm = new();
        Task<int> testTask() => Task.FromResult(1);
        Assert.That(vm.IsBusy, Is.False);
        var r = await vm.BusyOpTest(testTask());
        Assert.That(vm.IsBusy, Is.False);
        Assert.That(1, Is.EqualTo(r));
    }
}
