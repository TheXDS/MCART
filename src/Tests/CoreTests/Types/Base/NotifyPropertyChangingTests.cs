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

using System.ComponentModel;
using TheXDS.MCART.Types.Base;
using NUnit.Framework;

namespace TheXDS.MCART.Tests.Types.Base
{
    public class NotifyPropertyChangingTests
    {
        private class TestClass : NotifyPropertyChanging
        {
            private int _value;

            public int Value
            {
                get => _value;
                set => Change(ref _value, value);
            }
        }

        [Test]
        public void OnPropertyChangingTest()
        {
            TestClass? x = new();
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
    }
}