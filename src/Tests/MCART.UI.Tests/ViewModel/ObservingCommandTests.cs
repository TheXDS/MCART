/*
ObservingCommandTests.cs

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

namespace TheXDS.MCART.UI.Tests.ViewModel;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Factory;
using TheXDS.MCART.ViewModel;

public class ObservingCommandTests
{
    private class TestNpcClass : NotifyPropertyChanged
    {
        private string? _TestString;

        public string? TestString
        {
            get => _TestString;
            set => Change(ref _TestString, value);
        }
    }

    [Test]
    public void PropertyChange_Fires_Notification_Test()
    {
        TestNpcClass? i = new();
        ObservingCommand? obs = new ObservingCommand(i, NoAction)
            .SetCanExecute((a, b) => !i.TestString.IsEmpty())
            .RegisterObservedProperty(nameof(TestNpcClass.TestString));

        Assert.False(obs.CanExecute(null));
        i.TestString = "Test";
        Assert.True(obs.CanExecute(null));
    }
    
    [ExcludeFromCodeCoverage]
    private static void NoAction() { }
}
