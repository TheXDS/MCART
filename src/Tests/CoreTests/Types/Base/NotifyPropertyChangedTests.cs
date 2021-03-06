﻿/*
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
using Xunit;

namespace TheXDS.MCART.Tests.Types.Base
{
    public class NotifyPropertyChangedTests
    {
        private class TestClass : NotifyPropertyChanged
        {
            private int _value;

            public int Value
            {
                get => _value;
                set
                {
                    Change(ref _value, value);
                }
            }
        }
        
        [Fact]
        public void OnPropertyChangedTest()
        {
            var x = new TestClass();
            PropertyChangedEventHandler? handler = null;
            var evt = Assert.Raises<PropertyChangedEventArgs>(
                h =>
                {
                    handler = new PropertyChangedEventHandler(h);
                    x.PropertyChanged += handler;
                },
                h => x.PropertyChanged -= handler,
                () => x.Value = 1);
            
            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(x, evt.Sender));
            Assert.Equal(nameof(TestClass.Value), evt.Arguments.PropertyName);
            Assert.Equal(1, x.Value);
        }
    }
}
