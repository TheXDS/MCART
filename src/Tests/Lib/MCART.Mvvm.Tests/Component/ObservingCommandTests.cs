/*
ObservingCommandTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using NUnit.Framework;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Mvvm.Tests.Component;

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

    [Test]
    public void SetCanExecute_removes_callback_test()
    {
        TestNpcClass? i = new();
        ObservingCommand? obs = new ObservingCommand(i, NoAction)
            .SetCanExecute((a, b) =>
            {
                Assert.Fail();
                return false;
            })
            .RegisterObservedProperty(nameof(TestNpcClass.TestString));

        obs.SetCanExecute((Func<INotifyPropertyChanged, object?, bool>?)null);
        Assert.IsTrue(obs.CanExecute(null));
    }

    [ExcludeFromCodeCoverage]
    private static void NoAction() { }
}