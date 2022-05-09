/*
TypeFactoryTests.cs

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

using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Tests;

public class TypeFactoryTests
{
    private class TestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
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

    private static readonly TypeFactory _factory = new($"{typeof(TypeFactoryTests).FullName}._Generated");
    [Test]
    public void Build_Simple_Type_Test()
    {
        System.Reflection.Emit.TypeBuilder? t = _factory.NewClass("GreeterClass");
        PropertyBuildInfo? nameProp = t.AddAutoProperty<string>("Name");
        t.AddComputedProperty<string>("Greeting", p => p
            .LoadConstant("Hello, ")
            .LoadProperty(nameProp)
            .Call<Func<string?, string?, string>>(string.Concat)
            .Return());

        object? greeterInstance = t.New();
        ((dynamic)greeterInstance).Name = "Jhon";

        Assert.AreEqual("TheXDS.MCART.Tests.TypeFactoryTests._Generated", t.Namespace);
        Assert.AreEqual("Jhon", (string)((dynamic)greeterInstance).Name);
        Assert.AreEqual("Hello, Jhon", (string)((dynamic)greeterInstance).Greeting);
    }

    [Test]
    public void Build_Npc_Type_Test()
    {
        ITypeBuilder<NotifyPropertyChanged>? t = _factory.NewType<NotifyPropertyChanged>("NpcTestClass");
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
        ITypeBuilder<NpcBaseClass>? t = _factory.NewType<NpcBaseClass>("NpcBaseTestClass");
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
}
