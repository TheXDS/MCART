using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Xunit;
using static MCART.Objects;
using static MCART.Resources.RTInfo;

namespace CoreTests.Modules
{
    [AttrTest]
    public class ObjectsTest
    {
        /// <summary>
        /// Atributo de prueba.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
        sealed class AttrTestAttribute : Attribute { }
        [Fact]
        public void AnyAssignableFromTest()
        {
            Assert.True(MCART.Objects.AnyAssignableFrom(
                new[]
                {
                    typeof(int),
                    typeof(EventArgs),
                    typeof(ResolveEventArgs)
                }, typeof(ResolveEventArgs), out int? index));
            Assert.Equal(1, index);
        }
        [Fact]
        public void AreAssignableFromTest()
        {
            Assert.True(MCART.Objects.AreAssignableFrom(
                new[]
                {
                    typeof(EventArgs),
                    typeof(ResolveEventArgs)
                }, typeof(ResolveEventArgs)));
        }
        [Fact]
        public void IsAnyNullTest()
        {
            Assert.True(IsAnyNull(0, 1, null));
            Assert.False(IsAnyNull(0, 1, 2, 3));
            Assert.True(IsAnyNull(out int index, 0, 1, null));
            Assert.Equal(2, index);
        }
        [Fact]
        public void AreAllNullTest()
        {
            Assert.True(AreAllNull(null, null, null));
            Assert.False(AreAllNull(0, null));
        }
        [Fact]
        public void ItselfTest()
        {
            ApplicationException ex = new ApplicationException();
            Assert.Same(ex, ex.Itself());
            Assert.NotSame(ex, new ApplicationException());
            Assert.NotSame(ex, null);
        }
        [Fact]
        public void IsTest()
        {
            EventArgs ev = EventArgs.Empty;
            EventArgs e = ev;
            Assert.True(e.Is(ev));
        }
        [Fact]
        public void IsNotTest()
        {
            EventArgs ev = EventArgs.Empty;
            EventArgs e = new MCART.Events.ExceptionEventArgs(null);
            Assert.True(e.IsNot(ev));
        }
        [Fact]
        public void IsEitherTest()
        {
            Type t = typeof(int);
            Assert.True(t.IsEither(typeof(bool), typeof(int)));
            Assert.False(t.IsEither(typeof(bool), typeof(float)));
        }
        [Fact]
        public void IsNeitherTest()
        {
            Type t = typeof(int);
            Assert.True(t.IsNeither(typeof(bool), typeof(float)));
            Assert.False(t.IsNeither(typeof(bool), typeof(int)));
        }
        [Fact]
        public void GetTypesTest()
        {
            Assert.True(GetTypes<IComparable>().Count() > 2);
        }
        [Fact]
        public void ToTypesTest()
        {
            Type[] x = (new object[] { 1, "Test", 2.5f }).ToTypes().ToArray();
            IEnumerator y = x.GetEnumerator();
            y.Reset();
            y.MoveNext();
            Assert.Same(typeof(int), y.Current);
            y.MoveNext();
            Assert.Same(typeof(string), y.Current);
            y.MoveNext();
            Assert.Same(typeof(float), y.Current);
        }
        [Fact]
        public void NewTest()
        {
            Assert.NotNull(New<ResolveEventArgs>(new object[] { "Test" }));
            Assert.NotNull(typeof(ResolveEventArgs).New("Test"));
        }
        [Fact]
        public void GetAttrTest()
        {
            Assert.NotNull(RTAssembly.GetAttr<AssemblyTitleAttribute>());
            Assert.NotNull(MethodBase.GetCurrentMethod().GetAttr<FactAttribute>());
            Assert.NotNull(GetAttr<AttrTestAttribute, ObjectsTest>());
            Assert.NotNull(typeof(ObjectsTest).GetAttr<AttrTestAttribute>());
        }
    }
}