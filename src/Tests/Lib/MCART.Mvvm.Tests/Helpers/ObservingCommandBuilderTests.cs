// ObservingCommandBuilderTests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2023 César Andrés Morgan
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

using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Component;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Mvvm.Tests.Helpers
{
    public class ObservingCommandBuilderTests
    {
        [ExcludeFromCodeCoverage]
        private class ObservableTestClass : NotifyPropertyChanged
        {
            private double _doubleProperty;
            private float _floatProperty;
            private int _intProperty;
            private double? _nullableDoubleProperty;
            private float? _nullableFloatProperty;
            private string? _stringProperty;

            public int IntProperty
            {
                get => _intProperty;
                set => Change(ref _intProperty, value);
            }

            public string? StringProperty
            {
                get => _stringProperty;
                set => Change(ref _stringProperty, value);
            }

            public float FloatProperty
            {
                get => _floatProperty;
                set => Change(ref _floatProperty, value);
            }

            public double? NullableDoubleProperty
            {
                get => _nullableDoubleProperty;
                set => Change(ref _nullableDoubleProperty, value);
            }

            public double DoubleProperty
            {
                get => _doubleProperty;
                set => Change(ref _doubleProperty, value);
            }

            public float? NullableFloatProperty
            {
                get => _nullableFloatProperty;
                set => Change(ref _nullableFloatProperty, value);
            }

            public bool IsIntEven => IntProperty % 2 == 0;

            public bool IsStringLengthEven => (StringProperty?.Length ?? 0) % 2 == 0;

            public ObservableTestClass()
            {
                RegisterPropertyChangeBroadcast(nameof(IntProperty), nameof(IsIntEven));
                RegisterPropertyChangeBroadcast(nameof(StringProperty), nameof(IsStringLengthEven));
            }
        }

        [Test]
        public void Create_returns_ConfiguredObservingCommand_builder_Test()
        {
            Assert.That(ObservingCommandBuilder.Create(new ObservableTestClass(), () => { }), Is.InstanceOf<ObservingCommandBuilder<ObservableTestClass>>());
            Assert.That(ObservingCommandBuilder.Create(new ObservableTestClass(), _ => { }), Is.InstanceOf<ObservingCommandBuilder<ObservableTestClass>>());
            Assert.That(ObservingCommandBuilder.Create(new ObservableTestClass(), () => Task.CompletedTask), Is.InstanceOf<ObservingCommandBuilder<ObservableTestClass>>());
            Assert.That(ObservingCommandBuilder.Create(new ObservableTestClass(), _ => Task.CompletedTask), Is.InstanceOf<ObservingCommandBuilder<ObservableTestClass>>());
        }

        [Test]
        public void IsBuilt_remains_false_until_ObservingCommand_is_built_test()
        {
            var command = ObservingCommandBuilder.Create(new ObservableTestClass(), () => { });
            Assert.That(command.IsBuilt, Is.False);
            command.Build();
            Assert.That(command.IsBuilt, Is.True);
        }

        [Test]
        public void ListensTo_simple_property_registration_test()
        {
            var command = ObservingCommandBuilder.Create(new ObservableTestClass(), () => { });
            command.ListensTo(p => p.IntProperty);
            Assert.That(command.Build().ObservedProperties, Contains.Item(nameof(ObservableTestClass.IntProperty)));
        }

        [Test]
        public void ListensTo_multiple_property_registration_test()
        {
            var command = ObservingCommandBuilder.Create(new ObservableTestClass(), () => { });
            command.ListensTo(p => p.IntProperty, p => p.StringProperty);
            Assert.That(command.Build().ObservedProperties, Contains.Item(nameof(ObservableTestClass.IntProperty)));
            Assert.That(command.Build().ObservedProperties, Contains.Item(nameof(ObservableTestClass.StringProperty)));
        }

        [Test]
        public void ListensToCanExecute_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .ListensToCanExecute(p => p.IsIntEven)
                .Build();

            observed.IntProperty = 1;
            Assert.That(command.CanExecute(null), Is.False);
            observed.IntProperty = 2;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecute_with_argument_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecute(x => x is not null)
                .Build();

            Assert.That(command.CanExecute(null), Is.False);
            Assert.That(command.CanExecute(new object()), Is.True);
        }

        [Test]
        public void CanExecute_configures_command_test()
        {
            var flag = false;
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecute(() => flag)
                .Build();

            Assert.That(command.CanExecute(null), Is.False);
            flag = true;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfNotNull_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfNotNull(p => p.StringProperty)
                .Build();

            Assert.That(command.CanExecute(null), Is.False);
            observed.StringProperty = "Test";
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfNotDefault_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfNotDefault(p => p.IntProperty)
                .Build();

            observed.IntProperty = 0;
            Assert.That(command.CanExecute(null), Is.False);
            observed.IntProperty = 1;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfFilled_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfFilled(p => p.StringProperty)
                .Build();

            observed.StringProperty = null;
            Assert.That(command.CanExecute(null), Is.False);
            observed.StringProperty = "";
            Assert.That(command.CanExecute(null), Is.False);
            observed.StringProperty = "Test";
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfValid_with_float_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfValid(p => p.FloatProperty)
                .Build();

            observed.FloatProperty = float.NaN;
            Assert.That(command.CanExecute(null), Is.False);
            observed.FloatProperty = float.NegativeInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.FloatProperty = float.PositiveInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.FloatProperty = 0.0f;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfValid_with_nullable_float_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfValid(p => p.NullableFloatProperty)
                .Build();

            observed.NullableFloatProperty = null;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableFloatProperty = float.NaN;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableFloatProperty = float.NegativeInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableFloatProperty = float.PositiveInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableFloatProperty = 0.0f;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfValid_with_double_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfValid(p => p.DoubleProperty)
                .Build();

            observed.DoubleProperty = double.NaN;
            Assert.That(command.CanExecute(null), Is.False);
            observed.DoubleProperty = double.NegativeInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.DoubleProperty = double.PositiveInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.DoubleProperty = 0.0f;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfValid_with_nullable_double_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfValid(p => p.NullableDoubleProperty)
                .Build();

            observed.NullableDoubleProperty = null;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableDoubleProperty = double.NaN;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableDoubleProperty = double.NegativeInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableDoubleProperty = double.PositiveInfinity;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableDoubleProperty = 0.0;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfNotZero_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfNotDefault(p => p.IntProperty, p => p.FloatProperty, p => p.DoubleProperty)
                .Build();

            Assert.That(command.CanExecute(null), Is.False);
            observed.IntProperty = 1;
            Assert.That(command.CanExecute(null), Is.False);
            observed.FloatProperty = 1;
            Assert.That(command.CanExecute(null), Is.False);
            observed.DoubleProperty = 1;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CanExecuteIfObservedIsFilled_configures_command_test()
        {
            var observed = new ObservableTestClass();
            var command = ObservingCommandBuilder
                .Create(observed, () => { })
                .CanExecuteIfObservedIsFilled()
                .Build();

            Assert.That(command.CanExecute(null), Is.False);
            observed.IntProperty = 1;
            Assert.That(command.CanExecute(null), Is.False);
            observed.FloatProperty = 1.0f;
            Assert.That(command.CanExecute(null), Is.False);
            observed.DoubleProperty = 1.0;
            Assert.That(command.CanExecute(null), Is.False);
            observed.StringProperty = "Test";
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableFloatProperty = 1.0f;
            Assert.That(command.CanExecute(null), Is.False);
            observed.NullableDoubleProperty = 1.0;
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void Build_returns_same_instance_test()
        {
            var builder = ObservingCommandBuilder.Create(new ObservableTestClass(), () => { });
            var command1 = builder.Build();
            var command2 = builder.Build();
            Assert.That(command1, Is.SameAs(command2));
        }

        [Test]
        public void Implicit_conversion_builds_ObservingCommand_test()
        {
            var builder = ObservingCommandBuilder.Create(new ObservableTestClass(), () => { });
            Assert.That(builder.IsBuilt, Is.False);
            ObservingCommand command = builder;
            Assert.That(builder.IsBuilt, Is.True);
            Assert.That(command, Is.InstanceOf<ObservingCommand>());
        }
    }
}
