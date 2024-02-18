// ObservingCommandExtensions_Tests.cs
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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Component;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Mvvm.Tests.Types.Extensions
{
    public class ObservingCommandExtensions_Tests
    {
        [ExcludeFromCodeCoverage]
        private class TestViewModel : ViewModelBase
        {
            private string? _stringProperty;
            private int _intProperty;
            private bool _canExecuteProperty;
        
            public string? StringProperty
            {
                get => _stringProperty;
                set => Change(ref _stringProperty, value);
            }
        
            public int IntProperty
            {
                get => _intProperty;
                set => Change(ref _intProperty, value);
            }
        
            public bool CanExecuteProperty
            {
                get => _canExecuteProperty;
                set => Change(ref _canExecuteProperty, value);
            }

            public bool CanExecuteMethod() => CanExecuteProperty;
        
            public bool CanExecuteMethod(object? _) => CanExecuteProperty;

            public bool InvalidCanExecuteMethod(object? _, object? __) => false;
        }
    
        [Test]
        public void ListensToProperty_T_binds_to_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToProperty<TestViewModel>(p => p.StringProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty) }));
        }

        [Test]
        public void ListensToProperty_binds_to_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToProperty(() => vm.StringProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty) }));
        }
    
        [Test]
        public void ListensToProperties_binds_to_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToProperties(() => vm.StringProperty, () => vm.IntProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty), nameof(TestViewModel.IntProperty) }));
        }
    
        [Test]
        public void ListensToProperties_T_binds_to_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToProperties<TestViewModel>(p => p.StringProperty, p => p.IntProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty), nameof(TestViewModel.IntProperty) }));
        }
    
        [Test]
        public void ListensToProperties_TProperty_binds_to_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToProperties<object?>(() => vm.StringProperty, () => vm.IntProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty), nameof(TestViewModel.IntProperty) }));
        }
    
        [Test]
        public void ListensToProperties_T_TProperty_binds_to_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToProperties<TestViewModel, object?>(p => p.StringProperty, p => p.IntProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty), nameof(TestViewModel.IntProperty) }));
        }
    
        [Test]
        public void ListensToCanExecute_T_with_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToCanExecute<TestViewModel>(p => p.CanExecuteProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.CanExecuteProperty) }));
            Assert.That(obs.CanExecute(), Is.False);
            vm.CanExecuteProperty = true;
            Assert.That(obs.CanExecute(), Is.True);
        }
    
        [Test]
        public void ListensToCanExecute_with_property_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToCanExecute(() => vm.CanExecuteProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.CanExecuteProperty) }));
            Assert.That(obs.CanExecute(), Is.False);
            vm.CanExecuteProperty = true;
            Assert.That(obs.CanExecute(), Is.True);
        }
        
        [Test]
        public void ListensToCanExecute_with_parameterless_method_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToCanExecute(() => vm.CanExecuteMethod());
            Assert.That(obs.CanExecute(), Is.False);
            vm.CanExecuteProperty = true;
            Assert.That(obs.CanExecute(), Is.True);
        }
    
        [Test]
        public void ListensToCanExecute_T_with_parameterless_method_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToCanExecute<TestViewModel>(p => p.CanExecuteMethod());
            Assert.That(obs.CanExecute(), Is.False);
            vm.CanExecuteProperty = true;
            Assert.That(obs.CanExecute(), Is.True);
        }
            
        [Test]
        public void ListensToCanExecute_with_parameterized_method_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToCanExecute(() => vm.CanExecuteMethod(null));
            Assert.That(obs.CanExecute(), Is.False);
            vm.CanExecuteProperty = true;
            Assert.That(obs.CanExecute(), Is.True);
        }
    
        [Test]
        public void ListensToCanExecute_T_with_parameterized_method_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .ListensToCanExecute<TestViewModel>(p => p.CanExecuteMethod(null));
            Assert.That(obs.CanExecute(), Is.False);
            vm.CanExecuteProperty = true;
            Assert.That(obs.CanExecute(), Is.True);
        }
    
        [Test]
        public void ListensToCanExecute_T_contract_test()
        {
            Assert.That(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToCanExecute<TestViewModel>(p => p.InvalidCanExecuteMethod(null, null)), Throws.InstanceOf<InvalidArgumentException>());
            Assert.That(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToCanExecute<TestViewModel>(_ => new MemoryStream().CanRead), Throws.InstanceOf<MissingMemberException>());
        }
        
        [Test]
        public void ListensToCanExecute_contract_test()
        {
            Assert.That(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToCanExecute(() => new TestViewModel().InvalidCanExecuteMethod(null, null)), Throws.InstanceOf<InvalidArgumentException>());
            Assert.That(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToCanExecute(() => new MemoryStream().CanRead), Throws.InstanceOf<MissingMemberException>());
        }

        [Test]
        public void CanExecuteIfNotNull_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .CanExecuteIfNotNull(() => vm.StringProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty) }));
            Assert.That(obs.CanExecute(), Is.False);
            vm.StringProperty = "Test";
            Assert.That(obs.CanExecute(), Is.True);
        }
    
        [Test]
        public void CanExecuteIfNotDefault_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .CanExecuteIfNotDefault(() => vm.IntProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.IntProperty) }));
            Assert.That(obs.CanExecute(), Is.False);
            vm.IntProperty = 1;
            Assert.That(obs.CanExecute(), Is.True);
        }

        [Test]
        public void CanExecuteIfNotNull_T_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .CanExecuteIfNotNull<TestViewModel>(p => p.StringProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.StringProperty) }));
            Assert.That(obs.CanExecute(), Is.False);
            vm.StringProperty = "Test";
            Assert.That(obs.CanExecute(), Is.True);
        }
    
        [Test]
        public void CanExecuteIfNotDefault_T_test()
        {
            var vm = new TestViewModel();
            var obs = new ObservingCommand(vm, NoAction)
                .CanExecuteIfNotDefault<TestViewModel>(p => p.IntProperty);
            Assert.That(obs.ObservedProperties, Is.EquivalentTo(new[] { nameof(TestViewModel.IntProperty) }));
            Assert.That(obs.CanExecute(), Is.False);
            vm.IntProperty = 1;
            Assert.That(obs.CanExecute(), Is.True);
        }
    
        [Test]
        public void ListensToProperty_contract_test()
        {
            Assert.That((TestDelegate)(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToProperty(() => new Exception().Message)), Throws.InstanceOf<MissingMemberException>());
        }
        
        [Test]
        public void ListensToProperty_T_contract_test()
        {
            Assert.That((TestDelegate)(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToProperty<TestViewModel>(_ => new Exception().Message)), Throws.InstanceOf<MissingMemberException>());
        }
    
        [Test]
        public void ListensToProperties_contract_test()
        {
            Assert.That((TestDelegate)(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToProperties()), Throws.InstanceOf<InvalidOperationException>());
            Assert.That((TestDelegate)(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToProperties(() => new Exception().Message)), Throws.InstanceOf<MissingMemberException>());
        }
        
        [Test]
        public void ListensToProperties_T_contract_test()
        {
            Assert.That((TestDelegate)(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToProperties<TestViewModel, object?>()), Throws.InstanceOf<InvalidOperationException>());
            Assert.That((TestDelegate)(() => new ObservingCommand(new TestViewModel(), NoAction)
                .ListensToProperties<TestViewModel>(_ => new Exception().Message)), Throws.InstanceOf<MissingMemberException>());
        }
    
        [ExcludeFromCodeCoverage]
        private static void NoAction()
        {
        }
    }
}
