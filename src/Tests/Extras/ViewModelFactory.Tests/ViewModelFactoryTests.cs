/*
ViewModelFactoryTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.MCART.Tests;

public class ViewModelFactoryTests
{
    [ExcludeFromCodeCoverage]
    public abstract class NpcBaseClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NpcChangeInvocator]
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class NpcBaseClass2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [ExcludeFromCodeCoverage]
    public class TestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    private static readonly TypeFactory _factory = new($"{typeof(ViewModelFactoryTests).FullName}._Generated");

    [Test]
    public void Build_Npc_Type_Test_from_NotifyPropertyChanged()
    {
        ITypeBuilder<NotifyPropertyChanged> t = _factory.NewType<NotifyPropertyChanged>("NpcTestClass");
        t.AddNpcProperty<string>("Name");
        Test_Npc_Type(t.New());
    }

    [Test]
    public void Build_Npc_Type_Test_from_NotifyPropertyChangeBase()
    {
        ITypeBuilder<NotifyPropertyChangeBase> t = _factory.NewType<NotifyPropertyChanged>("NpcTestClass");
        t.AddNpcProperty<string>("Name");
        Test_Npc_Type(t.New());
    }

    [Test]
    public void Build_Npc_Type_Test_from_INotifyPropertyChanged()
    {
        ITypeBuilder<INotifyPropertyChanged> t = _factory.NewType<NpcBaseClass2>("NpcTestClass");
        t.AddNpcProperty<string>("Name", GetMethod<NpcBaseClass2, Action<string>>(p => p.OnPropertyChanged));
        Test_Npc_Type(t.New());
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
        Assert.That(evt, Is.Not.Null);
        Assert.That("Name", Is.EqualTo(evt!.Value.Arguments.PropertyName));
        Assert.That("Test", Is.EqualTo((string)npcInstance.Name));
    }

    [Test]
    public void Build_Npc_class_from_Model()
    {
        var t = _factory.CreateNpcClass<TestModel>();
        object instance = t.New();
        Assert.That(instance, Is.InstanceOf<INotifyPropertyChanged>());
        ((NotifyPropertyChanged)instance).PropertyChanged += OnPropertyChanged;
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);
        foreach (var j in instance.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            j.SetValue(instance, "Test");
            Assert.That(j.Name, Is.EqualTo(evt!.Value.Arguments.PropertyName));
            Assert.That("Test", Is.EqualTo(j.GetValue(instance)));
        }
        ((NotifyPropertyChanged)instance).PropertyChanged -= OnPropertyChanged;
    }

    [Test]
    public void ViewModel_fabrication()
    {
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);

        var t = _factory.CreateEntityViewModelClass<TestModel>();
        var instance = t.New();
        Assert.That(instance, Is.Not.Null);
        TestModel m = new();
        instance.PropertyChanged += OnPropertyChanged;

        instance.Entity = m;
        Assert.That(m, Is.SameAs(instance.Entity));
        Assert.That(nameof(EntityViewModel<TestModel>.Entity), Is.EqualTo(evt!.Value.Arguments.PropertyName));

        ((dynamic)instance).Name = "Test";
        Assert.That("Name", Is.EqualTo(evt!.Value.Arguments.PropertyName));
        Assert.That("Test", Is.EqualTo(((dynamic)instance).Name));
        Assert.That("Test", Is.EqualTo(instance.Entity.Name));

        ((dynamic)instance).Description = "Test";
        Assert.That("Description", Is.EqualTo(evt!.Value.Arguments.PropertyName));
        Assert.That("Test", Is.EqualTo(((dynamic)instance).Description));
        Assert.That("Test", Is.EqualTo(instance.Entity.Description));

        instance.PropertyChanged -= OnPropertyChanged;
    }

    private static void Test_Npc_Type(dynamic npcInstance)
    {
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);
        ((INotifyPropertyChanged)npcInstance).PropertyChanged += OnPropertyChanged;
        npcInstance.Name = "Test";
        ((INotifyPropertyChanged)npcInstance).PropertyChanged -= OnPropertyChanged;
        Assert.That(evt, Is.Not.Null);
        Assert.That("Name", Is.EqualTo(evt!.Value.Arguments.PropertyName));
        Assert.That("Test", Is.EqualTo((string)npcInstance.Name));
    }
}
