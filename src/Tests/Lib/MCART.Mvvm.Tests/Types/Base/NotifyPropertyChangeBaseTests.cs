﻿/*
NotifyPropertyChangeBaseTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.ComponentModel;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class NotifyPropertyChangeBaseTests
{
    private class NpcTestClass : NotifyPropertyChanged
    {
        private int _id;
        private string _prefix = "test";

        public int Id
        {
            get => _id;
            set => Change(ref _id, value);
        }

        public string Prefix
        {
            get => _prefix;
            set => Change(ref _prefix, value);
        }

        public string IdAsString => $"{Prefix} {Id}";

        protected override void OnInitialize(IPropertyBroadcastSetup broadcastSetup)
        {
            broadcastSetup
                .RegisterPropertyChangeBroadcast(() => Id, () => IdAsString)
                .RegisterPropertyChangeTrigger(() => IdAsString, () => Prefix);
        }
    }

    [Test]
    public void Broadcast_registration_test()
    {
        bool risen = false;
        NpcTestClass c = new();

        void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(c.IdAsString)) risen = true;
        }

        c.PropertyChanged += TestPropertyChanged;

        c.Id = 1;
        Assert.That(risen);
        Assert.That("test 1", Is.EqualTo(c.IdAsString));

        c.Prefix = "Test";
        Assert.That(risen);
        Assert.That("Test 1", Is.EqualTo(c.IdAsString));

        c.PropertyChanged -= TestPropertyChanged;
    }
}
