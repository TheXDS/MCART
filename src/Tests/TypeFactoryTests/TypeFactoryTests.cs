using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Annotations;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using Xunit;

#nullable enable

namespace TypeFactoryTests
{
    public class TypeFactoryTests
    {
        public abstract class NpcBaseClass : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected void OnPropertyChanged([CallerMemberName] string? propertyName = null!)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public class TestImpl : NpcBaseClass
        {
            private string? _name;

            public string? Name
            {
                get => _name;
                set
                {
                    if ((!(_name is null) && _name.Equals(value)) || value is null) return;
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private static TypeFactory factory = new TypeFactory("TheXDS.MCART.Tests._Generated");
        [Fact]
        public void BuildSimpleTypeTest()
        {
            var t = factory.NewClass("GreeterClass");
            var nameProp = t.AddAutoProperty<string>("Name");            
            var grettingProp = t.AddComputedProperty<string>("Greeting", p => p
                .LoadConstant("Hello, ")
                .LoadProperty(nameProp.Property)
                .Call<Func<string?, string?, string>>(() => string.Concat)
                .Return());

            var greeterClass = t.CreateType();
            var greeterInstance = t.New();
            ((dynamic)greeterInstance).Name = "Jhon";
            
            Assert.Equal("TheXDS.MCART.Tests._Generated", t.Namespace);
            Assert.Equal("Jhon", (string)((dynamic)greeterInstance).Name);
            Assert.Equal("Hello, Jhon", (string)((dynamic)greeterInstance).Greeting);
        }

        [Fact]
        public void BuildNpcTypeTest()
        {
            var t = factory.NewClass<NotifyPropertyChanged>("NpcTestClass");
            ((ITypeBuilder<NotifyPropertyChangeBase>)t).AddNpcProperty<string>("Name");
            var npcTestClass = t.Builder.CreateType()!;
            dynamic npcInstance = npcTestClass.New();
            PropertyChangedEventHandler evth = null!;

            var evt = Assert.Raises<PropertyChangedEventArgs>(
                p => ((NotifyPropertyChanged)npcInstance).PropertyChanged += evth = (s, e) => p(s, e),
                _ => ((NotifyPropertyChanged)npcInstance).PropertyChanged -= evth,
                () => npcInstance.Name = "Test");

            Assert.Equal("Name", evt.Arguments.PropertyName);
            Assert.Equal("Test", npcInstance.Name);
        }
        [Fact]
        public void BuildNpcTypeWithBaseClassTest()
        {
            var t = factory.NewClass<NpcBaseClass>("NpcBaseTestClass");
            t.AddNpcProperty<string>("Name");
            t.AddNpcProperty<int>("Age");
            var npcTestClass = t.Builder.CreateType()!;
            dynamic npcInstance = npcTestClass.New();
            PropertyChangedEventHandler evth = null!;

            var evt = Assert.Raises<PropertyChangedEventArgs>(
                p => ((NpcBaseClass)npcInstance).PropertyChanged += evth = (s, e) => p(s, e),
                _ => ((NpcBaseClass)npcInstance).PropertyChanged -= evth,
                () => npcInstance.Name = "Test");

            Assert.Equal("Name", evt.Arguments.PropertyName);
            Assert.Equal("Test", npcInstance.Name);
        }
    }
}
