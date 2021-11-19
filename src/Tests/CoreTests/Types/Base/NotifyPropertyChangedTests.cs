/*
NotifyPropertyChangedTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Base
{
    public class NotifyPropertyChangedTests
    {
        [ExcludeFromCodeCoverage]
        private class TestClass : NotifyPropertyChanged
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
                get => _value;
                set => Change(ref _value, value, null!);
            }
            public int BrokenProperty2
            {
                get => _value;
                set => Change(ref _value, value, string.Empty);
            }

            public object? SelfFalseTestingProperty
            {
                get => _obj;
                set => Assert.False(Change(ref _obj, value));
            }

            public object? SelfTrueTestingProperty
            {
                get => _obj;
                set => Assert.True(Change(ref _obj, value));
            }
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            var x = new TestClass();
            var risen = false;
            (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;

            void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                risen = true;
                evt = (sender, e);
            }

            x.PropertyChanged += OnPropertyChanged;
            x.Value = 1;
            x.PropertyChanged -= OnPropertyChanged;

            Assert.True(risen);
            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(x, evt!.Value.Sender));
            Assert.AreEqual(nameof(TestClass.Value), evt!.Value.Arguments.PropertyName);
            Assert.AreEqual(1, x.Value);
        }

        [Test]
        public void Property_Change_Forward_Test()
        {
            var x = new TestClass();
            var y = new TestClass();
            var risen = false;
            (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
            void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                risen = true;
                evt = (sender, e);
            }

            x.ForwardChange(y);
            y.PropertyChanged += OnPropertyChanged;
            x.Value = 1;
            y.PropertyChanged -= OnPropertyChanged;

            Assert.True(risen);
            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(y, evt!.Value.Sender));
        }

        [Test]
        public void Change_Contract_Test()
        {
            var x = new TestClass();
            Assert.Throws<ArgumentNullException>(() => x.BrokenProperty = 1);
            Assert.Throws<ArgumentNullException>(() => x.BrokenProperty2 = 1);
            x.SelfFalseTestingProperty = null;
            x.SelfTrueTestingProperty = 33;
            x.SelfFalseTestingProperty = 33;
        }
    }
}
