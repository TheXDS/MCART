using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MCART.Objects;
using System.Linq;
using System.Collections;
using System.Security;
using System.Reflection;
using static MCART.Resources.RTInfo;

namespace CoreTests.Modules
{
    [TestClass]
    public class ObjectsTest
    {
        [TestMethod]
        public void AnyAssignableFromTest()
        {
            Assert.IsTrue(MCART.Objects.AnyAssignableFrom(
                new[]
                {
                    typeof(int),
                    typeof(EventArgs),
                    typeof(ResolveEventArgs)
                }, typeof(ResolveEventArgs), out int? index));
            Assert.AreEqual(index, 1);
        }
        [TestMethod]
        public void AreAssignableFromTest()
        {
            Assert.IsTrue(MCART.Objects.AreAssignableFrom(
                new[]
                {
                    typeof(EventArgs),
                    typeof(ResolveEventArgs)
                }, typeof(ResolveEventArgs)));
        }
        [TestMethod]
        public void IsAnyNullTest()
        {
            Assert.IsTrue(IsAnyNull(0, 1, null));
            Assert.IsFalse(IsAnyNull(0, 1, 2, 3));
            Assert.IsTrue(IsAnyNull(out int index, 0, 1, null));
            Assert.AreEqual(index, 2);
        }
        [TestMethod]
        public void AreAllNullTest()
        {
            Assert.IsTrue(AreAllNull(null, null, null));
            Assert.IsFalse(AreAllNull(0, null));
        }
        [TestMethod]
        public void IsNullTest()
        {
            Array x = null;
            Assert.IsTrue(x.IsNull());
            Assert.IsFalse((15).IsNull());
        }
        [TestMethod]
        public void ItselfTest()
        {
            ApplicationException ex = new ApplicationException();
            Assert.AreSame(ex, ex.Itself());
            Assert.AreNotSame(ex, new ApplicationException());
            Assert.AreNotSame(ex, null);
        }
        [TestMethod]
        public void IsTest()
        {
            EventArgs ev = EventArgs.Empty;
            EventArgs e = ev;
            Assert.IsTrue(e.Is(ev));
        }
        [TestMethod]
        public void IsNotTest()
        {
            EventArgs ev = EventArgs.Empty;
            EventArgs e = new MCART.Events.ExceptionEventArgs(null);
            Assert.IsTrue(e.IsNot(ev));
        }
        [TestMethod]
        public void IsEitherTest()
        {
            Type t = typeof(int);
            Assert.IsTrue(t.IsEither(typeof(bool), typeof(int)));
            Assert.IsFalse(t.IsEither(typeof(bool), typeof(float)));
        }
        [TestMethod]
        public void IsNeitherTest()
        {
            Type t = typeof(int);
            Assert.IsTrue(t.IsNeither(typeof(bool), typeof(float)));
            Assert.IsFalse(t.IsNeither(typeof(bool), typeof(int)));
        }
        [TestMethod]
        public void GetTypesTest()
        {
            Assert.IsTrue(GetTypes<IComparable>().Count() > 2);
        }
        [TestMethod]
        public void ToTypesTest()
        {
            Type[] x = (new object[] { 1, "Test", 2.5f }).ToTypes().ToArray();
            IEnumerator y = x.GetEnumerator();
            y.Reset();
            y.MoveNext();
            Assert.AreSame(typeof(int), y.Current);
            y.MoveNext();
            Assert.AreSame(typeof(string), y.Current);
            y.MoveNext();
            Assert.AreSame(typeof(float), y.Current);
        }
        [TestMethod]
        public void NewTest()
        {
            Assert.IsNotNull(New<ResolveEventArgs>(new object[] { "Test" }));
            Assert.IsNotNull(typeof(ResolveEventArgs).New("Test"));
        }
        [TestMethod]
        public void GetAttrTest()
        {
            Assert.IsNotNull(RTAssembly.GetAttr<AssemblyTitleAttribute>());
            Assert.IsNotNull(MethodBase.GetCurrentMethod().GetAttr<TestMethodAttribute>());
            Assert.IsNotNull(GetAttr<TestClassAttribute, ObjectsTest>());
            Assert.IsNotNull(typeof(ObjectsTest).GetAttr<TestClassAttribute>());
        }
    }
}