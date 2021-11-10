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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using NUnit.Framework;
using System.Reflection;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class TypeExtensionsTests
    {
        private class ThrowingTest
        {
            public ThrowingTest()
            {
                throw new InvalidOperationException();
            }
        }        
        
        [Test]
        public void AnyAssignableFrom_Test()
        {
            Assert.True(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(EventArgs), typeof(Exception)).First() == typeof(EventArgs));
            Assert.False(typeof(ResolveEventArgs).Assignables(typeof(int), typeof(Version), typeof(Exception)).Any());
        }

        [Test]
        public void AreAssignableFrom_Test()
        {
            Assert.True(typeof(ResolveEventArgs).AreAllAssignable(typeof(EventArgs), typeof(ResolveEventArgs)));
            Assert.False(typeof(ResolveEventArgs).AreAllAssignable(typeof(AppContext), typeof(ResolveEventArgs)));
        }

        [Test]
        public void New_Test()
        {
            Assert.NotNull(typeof(Exception).New());
            Assert.NotNull(typeof(Exception).New<Exception>());
            Assert.NotNull(typeof(ResolveEventArgs).New("Test"));
            Assert.AreEqual(typeof(string), Assert.Throws<ClassNotInstantiableException>(() => typeof(string).New(new Exception()))!.OffendingObject);
            Assert.Null(typeof(ThrowingTest).New<ThrowingTest>(false, null));
            Assert.IsInstanceOf<InvalidOperationException>(Assert.Throws<TargetInvocationException>(() => typeof(ThrowingTest).New<ThrowingTest>(true, null))!.InnerException);
        }

        [Test]
        public void NotNullable_Test()
        {
            Assert.AreEqual(typeof(int), typeof(int).NotNullable());
            Assert.AreEqual(typeof(int), typeof(int?).NotNullable());
            Assert.Throws<ArgumentNullException>(() => ((Type)null!).NotNullable());
        }

        [Test]
        public void IsInstantiable_Test()
        {
            Assert.True(typeof(Exception).IsInstantiable());
            Assert.True(typeof(Exception).IsInstantiable((IEnumerable<Type>?)null));
            Assert.True(typeof(Exception).IsInstantiable(typeof(string)));
            Assert.False(typeof(Exception).IsInstantiable(typeof(int)));
        }

        [Test]
        public void ToNamedEnum_Test()
        {
            Assert.IsInstanceOf<IEnumerable<NamedObject<Enum>>>(typeof(DayOfWeek).ToNamedEnum());
            Assert.Throws<ArgumentNullException>(() => _ = TheXDS.MCART.Types.Extensions.TypeExtensions.ToNamedEnum(null!));
            Assert.Throws<ArgumentException>(() => _ = typeof(string).ToNamedEnum());
        }

        [Test]
        public void Default_Test()
        {
            Assert.AreEqual(0, typeof(int).Default());
            Assert.AreEqual(0L, typeof(long).Default());
            Assert.AreEqual(0f, typeof(float).Default());
            Assert.AreEqual(0.0, typeof(double).Default());
            Assert.AreEqual(0m, typeof(decimal).Default());
            Assert.AreEqual(Guid.Empty, typeof(Guid).Default());
            Assert.Null(typeof(string).Default());
            Assert.Null(typeof(object).Default());
        }

        [Test]
        public void IsStruct_Test()
        {
            Assert.True(typeof(Guid).IsStruct());
            Assert.False(typeof(int).IsStruct());
            Assert.False(typeof(string).IsStruct());
        }

        [Test]
        public void IsCollectionType_Test()
        {
            Assert.False(typeof(int).IsCollectionType());
            Assert.False(typeof(Exception).IsCollectionType());
            Assert.True(typeof(string).IsCollectionType());
            Assert.True(typeof(int[]).IsCollectionType());
            Assert.True(typeof(List<bool>).IsCollectionType());
        }

        [Test]
        public void Derivates_Test()
        {
            var t = typeof(Exception).Derivates(typeof(Exception).Assembly).ToArray();
            Assert.Contains(typeof(ArgumentNullException), t);
            Assert.False(t.Contains(typeof(TamperException)));
            Assert.False(t.Contains(typeof(int)));
            Assert.False(t.Contains(typeof(Guid)));
            Assert.False(t.Contains(typeof(string)));
            Assert.False(t.Contains(typeof(Enum)));
            Assert.False(t.Contains(typeof(AppDomain)));
            Assert.False(t.Contains(typeof(object)));
        }

        [Test]
        public void GetCollectionType_Test()
        {
            Assert.AreEqual(typeof(int), typeof(int[]).GetCollectionType());
            Assert.AreEqual(typeof(int), typeof(IEnumerable<int>).GetCollectionType());
            Assert.AreEqual(typeof(object), typeof(IEnumerable).GetCollectionType());
            Assert.AreEqual(typeof(string), typeof(Dictionary<int, string>).GetCollectionType());
        }

        [Test]
        public void IsAnyAssignable_Test()
        {
            Assert.True(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(ArgumentNullException)));
            Assert.False(typeof(Exception).IsAnyAssignable(typeof(int), typeof(DayOfWeek), typeof(System.IO.Stream)));
        }

        [Test]
        public void ResolveCollectionType_Test()
        {
            Assert.AreEqual(typeof(int), typeof(List<int>).ResolveCollectionType());
            Assert.AreEqual(typeof(int), typeof(int).ResolveCollectionType());
        }

        [Test]
        public void Implements_Test()
        {
            Assert.True(typeof(ArgumentException).Implements(typeof(Exception), Type.EmptyTypes));
            Assert.True(typeof(int[]).Implements(typeof(IEnumerable<>),typeof(int)));
            Assert.True(typeof(int[]).Implements(typeof(IEnumerable<>)));
            Assert.True(typeof(string).Implements(typeof(IEnumerable<char>)));
            Assert.True(typeof(int[]).Implements(typeof(IEnumerable<int>)));
            Assert.True(typeof(List<int>).Implements(typeof(IEnumerable)));
            Assert.True(typeof(Dictionary<int, string>).Implements(typeof(IDictionary<,>)));
            Assert.True(typeof(Array).Implements(typeof(IEnumerable)));
            Assert.True(typeof(List<int>).Implements(typeof(IEnumerable<int>)));
            Assert.True(typeof(IEnumerable<float>).Implements(typeof(IEnumerable<>)));
            Assert.False(typeof(float[]).Implements(typeof(IEnumerable<>),typeof(int)));
            Assert.False(typeof(Exception).Implements(typeof(IEnumerable<>)));
            Assert.False(typeof(ValueTask<string>).Implements(typeof(IEnumerable<>)));
            Assert.True(typeof(List<string>).Implements(new[] {
                typeof(IEnumerable),
                typeof(ICollection<string>),
                typeof(IList)
            }));
            Assert.True(typeof(List<int>).Implements(new[] {
                typeof(IEnumerable),
                typeof(ICollection<>),
                typeof(IList)
            }));
            Assert.True(typeof(List<string>).Implements(new[] {
                typeof(IEnumerable),
                typeof(ICollection<string>),
                typeof(IList)
            }));
        }

        [Test]
        public void TryInstance_Test()
        {
            Assert.True(typeof(Exception).TryInstance(out Exception? ex, "message"));
            Assert.NotNull(ex);
            Assert.False(typeof(Exception).TryInstance(out Exception? ex2, 1, 2, 3, 4));
            Assert.Null(ex2);
            Assert.False(typeof(ICloneable).TryInstance<ICloneable>(out var x));
            Assert.Null(x);
            Assert.True(typeof(decimal).TryInstance<decimal>(out var d));
            Assert.AreEqual(default(decimal), d);
            Assert.False(typeof(ThrowingTest).TryInstance<ThrowingTest>(out var tt));
            Assert.Null(tt);
        }

        [Test]
        public void ImplementsOperator_Test()
        {
            Assert.True(typeof(int).ImplementsOperator(Expression.Add));
            Assert.True(typeof(int).ImplementsOperator(Expression.Subtract));
            Assert.True(typeof(int).ImplementsOperator(Expression.Multiply));
            Assert.True(typeof(int).ImplementsOperator(Expression.Divide));
            Assert.True(typeof(int).ImplementsOperator(Expression.Modulo));
            Assert.False(typeof(object).ImplementsOperator(Expression.Add));
            Assert.False(typeof(object).ImplementsOperator(Expression.Subtract));
        }

        [Test]
        public void CSharpName_Test()
        {
            Assert.AreEqual("System.Collections.Generic.List<System.String>", typeof(List<string>).CSharpName());
            Assert.AreEqual("System.Collections.Generic.Dictionary<System.Int32, System.String>", typeof(Dictionary<int, string>).CSharpName());

        }
    }
}