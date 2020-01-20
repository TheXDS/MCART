using System;
using System.Reflection;
using System.Reflection.Emit;
using Xunit;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

#nullable enable

namespace TypeFactoryTests
{
    public class UnitTest1
    {
        [Fact]
        public void BuildSimpleTypeTest()
        {
            var f = new TypeFactory("TheXDS.MCART.Tests._Generated");
            var t = f.NewClass("GreeterClass");
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
    }
}
