/*
ViewModelFactoryTests.cs

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
using System.Linq;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

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
    public class TestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    private static readonly TypeFactory _factory = new($"{typeof(ViewModelFactoryTests).FullName}._Generated");

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
    public void ViewModel_fabrication()
    {
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);

        var t = _factory.CreateEntityViewModelClass<TestModel>();
        var instance = t.New();
        Assert.IsNotNull(instance);
        TestModel m = new();
        instance.PropertyChanged += OnPropertyChanged;

        instance.Entity = m;
        Assert.AreSame(m, instance.Entity);
        Assert.AreEqual(nameof(EntityViewModel<TestModel>.Entity), evt!.Value.Arguments.PropertyName);

        ((dynamic)instance).Name = "Test";
        Assert.AreEqual("Name", evt!.Value.Arguments.PropertyName);
        Assert.AreEqual("Test", ((dynamic)instance).Name);
        Assert.AreEqual("Test", instance.Entity.Name);

        ((dynamic)instance).Description = "Test";
        Assert.AreEqual("Description", evt!.Value.Arguments.PropertyName);
        Assert.AreEqual("Test", ((dynamic)instance).Description);
        Assert.AreEqual("Test", instance.Entity.Description);

        instance.PropertyChanged -= OnPropertyChanged;
    }
}
