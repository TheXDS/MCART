/*
TypeFactoryTests.cs

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

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.TypeFactory.Tests;

public class TypeFactoryTests
{
    private class TestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public abstract class NpcBaseClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NpcChangeInvocator]
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    private static readonly Types.TypeFactory _factory = new($"{typeof(TypeFactoryTests).FullName}._Generated");
    [Test]
    public void Build_Simple_Type_Test()
    {
        System.Reflection.Emit.TypeBuilder t = _factory.NewClass("GreeterClass");
        PropertyBuildInfo nameProp = t.AddAutoProperty<string>("Name");
        t.AddComputedProperty<string>("Greeting", p => p
            .LoadConstant("Hello, ")
            .LoadProperty(nameProp)
            .Call<Func<string?, string?, string>>(string.Concat)
            .Return());

        object greeterInstance = t.New();
        ((dynamic)greeterInstance).Name = "Jhon";

        Assert.AreEqual("TheXDS.MCART.TypeFactory.Tests.TypeFactoryTests._Generated", t.Namespace);
        Assert.AreEqual("Jhon", (string)((dynamic)greeterInstance).Name);
        Assert.AreEqual("Hello, Jhon", (string)((dynamic)greeterInstance).Greeting);
    }

    [Test]
    public void Build_Npc_Type_Test()
    {
        ITypeBuilder<NotifyPropertyChanged> t = _factory.NewType<NotifyPropertyChanged>("NpcTestClass");
        t.AddNpcProperty<string>("Name");
        dynamic npcInstance = t.New();
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);
        ((NotifyPropertyChanged)npcInstance).PropertyChanged += OnPropertyChanged;
        npcInstance.Name = "Test";
        ((NotifyPropertyChanged)npcInstance).PropertyChanged -= OnPropertyChanged;
        Assert.NotNull(evt);
        Assert.AreEqual("Name", evt!.Value.Arguments.PropertyName);
        Assert.AreEqual("Test", (string)npcInstance.Name);
    }

    [Test]
    public void Build_Npc_Type_With_Public_Base_Class_Test()
    {
        ITypeBuilder<NpcBaseClass> t = _factory.NewType<NpcBaseClass>("NpcBaseTestClass");
        t.AddNpcProperty<string>("Name");
        t.AddNpcProperty<int>("Age");
        dynamic npcInstance = t.New();
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);
        ((INotifyPropertyChanged)npcInstance).PropertyChanged += OnPropertyChanged;
        npcInstance.Name = "Test";
        ((INotifyPropertyChanged)npcInstance).PropertyChanged -= OnPropertyChanged;
        Assert.NotNull(evt);
        Assert.AreEqual("Name", evt!.Value.Arguments.PropertyName);
        Assert.AreEqual("Test", (string)npcInstance.Name);
    }

    [Test]
    public void Build_Npc_class_from_Model()
    {
        var t = _factory.CreateNpcClass<TestModel>();
        object instance = t.New();
        Assert.IsInstanceOf<INotifyPropertyChanged>(instance);
        ((NotifyPropertyChanged)instance).PropertyChanged += OnPropertyChanged;
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);
        foreach (var j in instance.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            j.SetValue(instance, "Test");
            Assert.AreEqual(j.Name, evt!.Value.Arguments.PropertyName);
            Assert.AreEqual("Test", j.GetValue(instance));
        }
        ((NotifyPropertyChanged)instance).PropertyChanged -= OnPropertyChanged;
    }

    [Test]
    public void Factory_exposes_dynamic_assembly()
    {
        Assert.IsInstanceOf<Assembly>(_factory.Assembly);
        Assert.True(_factory.Assembly.IsDynamic);
    }
}
