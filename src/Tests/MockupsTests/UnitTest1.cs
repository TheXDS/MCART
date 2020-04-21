using System;
using Xunit;
using TheXDS.MCART.Mockups;

namespace TheXDS.MCART.Tests.Mockups
{
    public class MockupBuilderTests
    {
        private interface ITest
        {
            int ComputedIntValue { get; }
            int AutoIntValue { get; set; }
            void AMethod();
            bool AFunction(int parameter);
        }

        public void BuildASimpleMockupTest()
        {
            var builder = new MockupBuilder<ITest>();
            var instance = builder.Build();
            Assert.NotNull(instance);
            Assert.IsAssignableFrom<ITest>(instance);
        }

        //public void BuildASimpleMockupStaticallyTest()
        //{
        //    var instance = MockupBuilder<ITest>.Build();
        //    Assert.NotNull(instance);
        //    Assert.IsAssignableFrom<ITest>(instance);
        //}

        //public void BuildUsefulMockupTest()
        //{
        //    var builder = new MockupBuilder<ITest>();
        //    builder.DefineProperty(p => p.ComputedIntValue, 5);
        //    builder.DefineProperty(p => p.AutoIntValue, 3);
        //}
    }
}
