using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TypeFactoryTests
{
    public class TypeFactoryTests
    {
        public abstract class NpcBaseClass : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            [NpcChangeInvocator]
            protected void OnPropertyChanged([CallerMemberName] string? propertyName = null!)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static readonly TypeFactory _factory = new("TheXDS.MCART.Tests._Generated");

        [Fact]
        public void BuildSimpleTypeTest()
        {
            var t = _factory.NewClass("GreeterClass");
            var nameProp = t.AddAutoProperty<string>("Name");            
            t.AddComputedProperty<string>("Greeting", p => p
                .LoadConstant("Hello, ")
                .LoadProperty(nameProp)
                .Call<Func<string?, string?, string>>(string.Concat)
                .Return());

            var greeterInstance = t.New();
            ((dynamic)greeterInstance).Name = "Jhon";
            
            Assert.Equal("TheXDS.MCART.Tests._Generated", t.Namespace);
            Assert.Equal("Jhon", (string)((dynamic)greeterInstance).Name);
            Assert.Equal("Hello, Jhon", (string)((dynamic)greeterInstance).Greeting);
        }

        [Fact]
        public void BuildNpcTypeTest()
        {
            var t = _factory.NewType<NotifyPropertyChanged>("NpcTestClass");
            t.AddNpcProperty<string>("Name");
            dynamic npcInstance = t.New();
            PropertyChangedEventHandler? evth = null;

            var evt = Assert.Raises<PropertyChangedEventArgs>(
                p => ((NotifyPropertyChanged)npcInstance).PropertyChanged += evth = (s, e) => p(s, e),
                _ => ((NotifyPropertyChanged)npcInstance).PropertyChanged -= evth,
                () => npcInstance.Name = "Test");

            Assert.Equal("Name", evt.Arguments.PropertyName);
            Assert.Equal("Test", (string)npcInstance.Name);
        }

        [Fact]
        public void BuildNpcTypeWithPublicBaseClassTest()
        {
            var t = _factory.NewType<NpcBaseClass>("NpcBaseTestClass");
            t.AddNpcProperty<string>("Name");
            t.AddNpcProperty<int>("Age");
            dynamic npcInstance = t.New();
            PropertyChangedEventHandler? evth = null;

            var evt = Assert.Raises<PropertyChangedEventArgs>(
                p => ((NpcBaseClass)npcInstance).PropertyChanged += evth = (s, e) => p(s, e),
                _ => ((NpcBaseClass)npcInstance).PropertyChanged -= evth,
                () => npcInstance.Name = "Test");

            Assert.Equal("Name", evt.Arguments.PropertyName);
            Assert.Equal("Test", (string)npcInstance.Name);
        }
    }
}
