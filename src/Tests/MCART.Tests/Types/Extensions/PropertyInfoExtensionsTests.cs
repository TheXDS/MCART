/*
PropertyInfoExtensionsTests.cs

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
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.MCART.Types.Factory;

public class PropertyInfoExtensionsTests
{
    [ExcludeFromCodeCoverage]
    private class Test : IDisposable
    {
        [DefaultValue(1)] public int? Prop1 { get; set; } = 1;
        public int? Prop2 { get; set; } = 2;
        public int? Prop3 { get; set; }

        public void Dispose()
        {
        }
    }

    [ExcludeFromCodeCoverage]
    private class Test2
    {
        public int Prop1 { get; } = 10;
    }

    [ExcludeFromCodeCoverage]
    private static class Test3
    {
        [DefaultValue(1)]
        public static int? Prop1 { get; set; } = 1;
        public static int? Prop2 { get; set; }
    }

    [Test]
    public void SetDefault_Test()
    {
        Test? o = new() { Prop1 = 9, Prop2 = 9, Prop3 = 9 };
        foreach (System.Reflection.PropertyInfo? j in o.GetType().GetProperties())
        {
            j.SetDefault(o);
        }
        Assert.AreEqual(1, o.Prop1);
        Assert.AreEqual(2, o.Prop2);
        Assert.Null(o.Prop3);
    }

    [Test]
    public void SetDefault_Contract_Test()
    {
        Test2? o = new();
        Assert.Throws<InvalidOperationException>(() => o.GetType().GetProperties()[0].SetDefault(o));
        Assert.Throws<MissingMemberException>(() => typeof(Exception).GetProperty("Message")!.SetDefault(o));
    }

    [Test]
    public void SetDefault_Static_Property_Test()
    {
        Test3.Prop1 = 9;
        Test3.Prop2 = 9;

        foreach (System.Reflection.PropertyInfo? j in typeof(Test3).GetProperties())
        {
            j.SetDefault();
        }
        Assert.AreEqual(1, Test3.Prop1);
        Assert.Null(Test3.Prop2);
    }

    [Test]
    public void IsReadWrite_Test()
    {
        Assert.True(typeof(Test).GetProperties().All(p => p.IsReadWrite()));
        Assert.True(typeof(Test2).GetProperties().All(p => !p.IsReadWrite()));
    }
}
