/*
NotifyPropertyChangedTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types.Base;

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
        
        
        public NpcTestClass()
        {
            RegisterPropertyChangeBroadcast(nameof(Id), new []{ nameof(IdAsString) }.AsEnumerable());
            RegisterPropertyChangeTrigger(nameof(IdAsString), nameof(Prefix));
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
        Assert.IsTrue(risen);
        Assert.AreEqual("test 1", c.IdAsString);
        
        c.Prefix = "Test";
        Assert.IsTrue(risen);
        Assert.AreEqual("Test 1", c.IdAsString);
        
        c.PropertyChanged -= TestPropertyChanged;
    }
}

public class NotifyPropertyChangedTests : NotifyPropertyChanged
{
    private int _value;
    private object? _obj;

    [ExcludeFromCodeCoverage]
    public int Value
    {
        get => _value;
        set => Change(ref _value, value);
    }
    
    [ExcludeFromCodeCoverage]
    public object? Obj
    {
        get => _obj;
        set => Change(ref _obj, value);
    }
    
    [ExcludeFromCodeCoverage]
    public int BrokenProperty
    {
        set => Change(ref _value, value, null!);
    }
    [ExcludeFromCodeCoverage]
    public int BrokenProperty2
    {
        set => Change(ref _value, value, string.Empty);
    }

    [ExcludeFromCodeCoverage]
    public object? SelfFalseTestingProperty
    {
        set => Assert.False(Change(ref _obj, value));
    }

    [ExcludeFromCodeCoverage]
    public object? SelfTrueTestingProperty
    {
        set => Assert.True(Change(ref _obj, value));
    }
    
    [ExcludeFromCodeCoverage]
    public int Prop1 { get; set; }
    [ExcludeFromCodeCoverage]
    public int Prop2 { get; set; }
    [ExcludeFromCodeCoverage]
    public int Prop3 { get; set; }

    [Test]
    public void RegisterPropertyChangeBroadcast_Contract_Test()
    {
        Assert.AreEqual("property", Assert.Throws<ArgumentNullException>(
            () => RegisterPropertyChangeBroadcast(null!, "Prop1"))?.ParamName);
        Assert.AreEqual("property", Assert.Throws<ArgumentException>(
            () => RegisterPropertyChangeBroadcast(string.Empty, "Prop1"))?.ParamName);
        Assert.AreEqual("property", Assert.Throws<ArgumentException>(
            () => RegisterPropertyChangeBroadcast(" ", "Prop1"))?.ParamName);
        Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentNullException>(
            () => RegisterPropertyChangeBroadcast("Prop1", null!))?.ParamName);

        string[] arr = Array.Empty<string>();
        Assert.AreSame(arr, ((EmptyCollectionException?)Assert.Throws<InvalidOperationException>(
            () => RegisterPropertyChangeBroadcast("Prop1", arr))?.InnerException)?.OffendingObject);

        Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentException>(
            () => RegisterPropertyChangeBroadcast("Prop1", string.Empty))?.ParamName);
        Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentException>(
            () => RegisterPropertyChangeBroadcast("Prop1", (string)null!))?.ParamName);
        Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentException>(
            () => RegisterPropertyChangeBroadcast("Prop1", " "))?.ParamName);
        
        RegisterPropertyChangeBroadcast(nameof(Prop1), nameof(Prop2));
        RegisterPropertyChangeBroadcast(nameof(Prop1), "Prop4");
        RegisterPropertyChangeBroadcast(nameof(Prop2), nameof(Prop3));
        Assert.Throws<InvalidOperationException>(() =>
            RegisterPropertyChangeBroadcast(nameof(Prop3), nameof(Prop1)));
    }

    [Test]
    public void ObserveTree_Test()
    {
        Assert.False(ObserveTree.ContainsKey(nameof(Prop1)));
        Assert.IsAssignableFrom<ReadOnlyDictionary<string, ICollection<string>>>(ObserveTree);
        RegisterPropertyChangeBroadcast(nameof(Prop1), nameof(Prop2));
        RegisterPropertyChangeBroadcast(nameof(Prop1), nameof(Prop3));
        Assert.True(ObserveTree.ContainsKey(nameof(Prop1)));
        Assert.AreEqual(new[]{nameof(Prop2), nameof(Prop3)}, ObserveTree[nameof(Prop1)].ToArray());
    }
    
    [Test]
    public void OnPropertyChangedTest()
    {
        bool risen = false;
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;

        void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            risen = true;
            evt = (sender, e);
        }

        PropertyChanged += TestPropertyChanged;
        Value = 1;
        PropertyChanged -= TestPropertyChanged;

        Assert.True(risen);
        Assert.NotNull(evt);
        Assert.True(ReferenceEquals(this, evt!.Value.Sender));
        Assert.AreEqual(nameof(Value), evt!.Value.Arguments.PropertyName);
        Assert.AreEqual(1, Value);
    }

    [Test]
    public void Property_Change_Forward_Test()
    {
        NotifyPropertyChangedTests source = new();
        NotifyPropertyChangedTests other = new();
        bool risen = false;
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            risen = true;
            evt = (sender, e);
        }
        source.ForwardChange(other);
        other.PropertyChanged += TestPropertyChanged;
        source.Value = 1;
        other.PropertyChanged -= TestPropertyChanged;
        Assert.True(risen);
        Assert.NotNull(evt);
        Assert.True(ReferenceEquals(other, evt!.Value.Sender));
        source.RemoveForwardChange(other);
    }

    [Test]
    public void NotifyRegistroir_Test()
    {
        bool risen = false;
        void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Obj)) risen = true;
        }
        RegisterPropertyChangeBroadcast(
            nameof(Value),
            nameof(Obj));
        PropertyChanged += TestPropertyChanged;
        Value = 2;
        UnregisterPropertyChangeBroadcast(nameof(Value));
        Assert.IsTrue(risen);
        PropertyChanged -= TestPropertyChanged;
    }

    [Test]
    public void Change_Contract_Test()
    {
        Assert.Throws<ArgumentNullException>(() => BrokenProperty = 1);
        Assert.Throws<ArgumentException>(() => BrokenProperty2 = 1);
        SelfFalseTestingProperty = null;
        SelfTrueTestingProperty = 33;
        SelfFalseTestingProperty = 33;
    }
}
