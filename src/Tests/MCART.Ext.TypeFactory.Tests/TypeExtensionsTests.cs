using System;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Factory;
using NUnit.Framework;

namespace TheXDS.MCART.Tests
{
    public class TypeExtensionsTests
    {
        private static readonly TypeFactory _factory = new("TheXDS.MCART.Tests.TypeExtensionsTests._Generated");

        [Test]
        public void ResolveToDefinedType_Test()
        {
            System.Reflection.Emit.TypeBuilder? t = _factory.NewClass("GreeterClass");
            PropertyBuildInfo? nameProp = t.AddAutoProperty<string>("Name");
            t.AddComputedProperty<string>("Greeting", p => p
                .LoadConstant("Hello, ")
                .LoadProperty(nameProp)
                .Call<Func<string?, string?, string>>(string.Concat)
                .Return());
            Assert.AreEqual(typeof(int), typeof(int).ResolveToDefinedType());
            Assert.AreEqual(typeof(object), t.New().GetType().ResolveToDefinedType());
        }
    }
}
