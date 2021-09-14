using System;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using Xunit;

namespace TheXDS.MCART.Tests
{
    public class TypeExtensionsTests
    {
        private static readonly TypeFactory _factory = new("TheXDS.MCART.Tests.TypeExtensionsTests._Generated");

        [Fact]
        public void ResolveToDefinedType_Test()
        {
            var t = _factory.NewClass("GreeterClass");
            var nameProp = t.AddAutoProperty<string>("Name");            
            t.AddComputedProperty<string>("Greeting", p => p
                .LoadConstant("Hello, ")
                .LoadProperty(nameProp)
                .Call<Func<string?, string?, string>>(string.Concat)
                .Return());
            Assert.Equal(typeof(int), typeof(int).ResolveToDefinedType());
            Assert.Equal(typeof(object), t.New().GetType().ResolveToDefinedType());
        }
    }
}