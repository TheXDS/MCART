using System;
using System.Reflection;
using System.Reflection.Emit;
using Xunit;
using TheXDS.MCART.Types;

namespace TypeFactoryTests
{
    public class UnitTest1
    {
        [Fact]
        public void BuildSimpleTypeTest()
        {
            void GreetingGetter(ILGenerator il, MemberInfo[] members)
            {

            }




            TypeFactory f = new TypeFactory("TheXDS.MCART.Tests._Generated");
            TypeGenerator t = f.NewClass("TestClass");

            PropertyBuilderInfo name = t.AddAutoProperty<string>("Name");


            t.AddProperty("Greeting", GreetingGetter);

        }
    }
}
