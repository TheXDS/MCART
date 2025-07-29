/*
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class NotifyPropertyChangeBaseTests
{
    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
    private class NpcTestClass2 : NotifyPropertyChanged
    {
        private int _Prop1;

        public int Prop1
        {
            get => _Prop1;
            set => Change(ref _Prop1, value);
        }

        public string Prop1ToString => _Prop1.ToString();

        public string Prop1ToString2 => _Prop1.ToString();

        protected override void OnInitialize(IPropertyBroadcastSetup broadcastSetup)
        {
            broadcastSetup
                .RegisterPropertyChangeBroadcast(() => Prop1, () => Prop1ToString)
                .RegisterPropertyChangeBroadcast(() => Prop1, () => Prop1ToString2);
        }
    }

    [ExcludeFromCodeCoverage]
    private class BrokenNpcTestClass : NotifyPropertyChanged
    {
        public int Prop1 { get; set; }
        public int Prop2 { get; set; }

        protected override void OnInitialize(IPropertyBroadcastSetup broadcastSetup)
        {
            broadcastSetup
                .RegisterPropertyChangeBroadcast(() => Prop1, () => Prop2)
                .RegisterPropertyChangeBroadcast(() => Prop2, () => Prop1);
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

    [Test]
    public void Broadcast_registration_fails_on_circular_registration()
    {
        Assert.That(() => _ = new BrokenNpcTestClass(), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void Broadcast_registration_continues_registration_of_previously_registered_property()
    {
        bool risen1 = false;
        bool risen2 = false;
        NpcTestClass2 c = new();

        void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(c.Prop1ToString)) risen1 = true;
            if (e.PropertyName == nameof(c.Prop1ToString2)) risen2 = true;
        }
        c.PropertyChanged += TestPropertyChanged;
        c.Prop1 = 1;
        Assert.That(risen1);
        Assert.That(risen2);
    }
}
