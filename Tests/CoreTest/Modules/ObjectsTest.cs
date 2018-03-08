using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using TheXDS.MCART;
using TheXDS.MCART.Resources;
using Xunit;

namespace CoreTest.Modules
{
    [AttrTest]
    public class ObjectsTest
    {
        /// <summary>
        /// Atributo de prueba.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class)]
        sealed class AttrTestAttribute : Attribute { }

        [Fact]
        public void AnyAssignableFromTest()
        {
            Assert.True(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(EventArgs), typeof(Exception)).First() == typeof(EventArgs));
            Assert.False(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(Version), typeof(Exception)).Any());
        }
        [Fact]
        public void AreAssignableFromTest()
        {
            Assert.True(typeof(ResolveEventArgs).AreAllAssignable(typeof(EventArgs), typeof(ResolveEventArgs)));
            Assert.False(typeof(ResolveEventArgs).AreAllAssignable(typeof(AppContext), typeof(ResolveEventArgs)));
        }
        [Fact]
        public void IsAnyNullTest()
        {
            Assert.True(Objects.IsAnyNull(0, 1, null));
            Assert.False(Objects.IsAnyNull(0, 1, 2, 3));
        }
        [Fact]
        public void AreAllNullTest()
        {
            Assert.True(Objects.AreAllNull(null, null, null));
            Assert.False(Objects.AreAllNull(0, null));
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
            EventArgs ev = new TheXDS.MCART.Events.ExceptionEventArgs(null);
            EventArgs e = new TheXDS.MCART.Events.ExceptionEventArgs(null);
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
            Assert.True(Objects.GetTypes<IComparable>().Count() > 2);
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
            Assert.NotNull(typeof(ResolveEventArgs).New("Test"));
        }
        [Fact]
        public void GetAttrTest()
        {
            Assert.NotNull(RTInfo.RTAssembly.GetAttr<AssemblyTitleAttribute>());
            Assert.NotNull(MethodBase.GetCurrentMethod().GetAttr<FactAttribute>());
            Assert.NotNull(Objects.GetAttr<AttrTestAttribute, ObjectsTest>());
            Assert.NotNull(typeof(ObjectsTest).GetAttr<AttrTestAttribute>());
        }
    }
}