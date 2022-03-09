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
                set => Change(ref _value, value, null);
            }
            public int BrokenProperty2
            {
                set => OnPropertyChanging(null);
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

            Assert.True(risen);
            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(x, evt!.Value.Sender));
            Assert.AreEqual(nameof(TestClass.Value), evt!.Value.Arguments.PropertyName);
            Assert.AreEqual(1, x.Value);
        }

        [Test]
        public void Change_Contract_Test()
        {
            TestClass x = new();
            Assert.Throws<ArgumentNullException>(() => x.BrokenProperty = 2);
        }
        
        [Test]
        public void OnPropertyChanging_Contract_Test()
        {
            TestClass x = new();
            Assert.Throws<ArgumentNullException>(() => x.BrokenProperty2 = 2);
        }
        
        [Test]
        public void Change_With_Unchanging_Values_Test()
        {
            TestClass x = new() { Obj = null, Value = 0 };
            bool risen = false;
            void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
            {
                risen = true;
            }
            x.PropertyChanging += OnPropertyChanging;
            x.Obj = null;
            Assert.False(risen);
            Assert.IsNull(x.Obj);
            object o = new();
            x.Obj = o;
            Assert.True(risen);
            Assert.AreSame(o, x.Obj);
            risen = false;
            x.Obj = o;
            Assert.False(risen);
            Assert.AreSame(o, x.Obj);
            x.Value = 0;
            Assert.False(risen);
            Assert.AreEqual(0,x.Value);
            x.Value = 1;
            Assert.True(risen);
            Assert.AreEqual(1, x.Value);
            x.PropertyChanging -= OnPropertyChanging;
        }

        [Test]
        public void OnPropertyChanging_Forwards_Notifications_Test()
        {
            bool risen = false;
            void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
            {
                risen = true;
            }
            TestClass x = new();
            TestClass y = new();
            x.ForwardChange(y);
            y.PropertyChanging += OnPropertyChanging;
            x.Value = 1;
            Assert.True(risen);
            y.PropertyChanging -= OnPropertyChanging;
        }
    }
}