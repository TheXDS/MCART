/*
TypeExtensionsTests.cs

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

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests
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

        private static readonly TypeFactory _factory = new("TheXDS.MCART.Tests.TypeFactoryTests._Generated");

        [Fact]
        public void Build_Simple_Type_Test()
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
            
            Assert.Equal("TheXDS.MCART.Tests.TypeFactoryTests._Generated", t.Namespace);
            Assert.Equal("Jhon", (string)((dynamic)greeterInstance).Name);
            Assert.Equal("Hello, Jhon", (string)((dynamic)greeterInstance).Greeting);
        }

        [Fact]
        public void Build_Npc_Type_Test()
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
        public void Build_Npc_Type_With_Public_Base_Class_Test()
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
