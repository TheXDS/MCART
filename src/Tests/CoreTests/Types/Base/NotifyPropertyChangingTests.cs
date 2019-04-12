/*
NotifyPropertyChangedTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#pragma warning disable CS1591

using System.ComponentModel;
using TheXDS.MCART.Types.Base;
using Xunit;

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
                set
                {
                    if (value == _value) return;
                    OnPropertyChanging();
                    _value = value;
                }
            }
        }

        [Fact]
        public void OnPropertyChangingTest()
        {
            var x = new TestClass();
            PropertyChangingEventHandler handler = null;

            var evt = Assert.Raises<PropertyChangingEventArgs>(
                h =>
                {
                    handler = new PropertyChangingEventHandler(h);
                    x.PropertyChanging += handler;
                },
                h => x.PropertyChanging -= handler,
                () => x.Value = 1);

            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(x, evt.Sender));
            Assert.Equal(nameof(TestClass.Value), evt.Arguments.PropertyName);
        }
    }
}