/*
DelegateExtensionsTests.cs

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

public class DelegateExtensionsTests
{
    [Name("Method 1")]
    [ExcludeFromCodeCoverage]
    private static void Method1() { }

    [ExcludeFromCodeCoverage]
    private static void Method2() { }

    [Test]
    public void NameOf_Test()
    {
        static Delegate Get(Expression<Func<Action>> sel) => Delegate.CreateDelegate(typeof(Action), ReflectionHelpers.GetMethod(sel));

        Assert.AreEqual("Method 1", Get(() => Method1).NameOf());
        Assert.AreEqual("Method2", Get(() => Method2).NameOf());
    }
}
