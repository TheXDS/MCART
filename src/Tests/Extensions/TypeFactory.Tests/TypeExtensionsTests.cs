/*
TypeExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     C�sar Andr�s Morgan <xds_xps_ivx@hotmail.com>

Copyright � 2011 - 2021 C�sar Andr�s Morgan

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
using NUnit.Framework;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.TypeFactory.Tests;

public class TypeExtensionsTests
{
    private static readonly Types.TypeFactory _factory = new("TheXDS.MCART.Tests.TypeExtensionsTests._Generated");

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
