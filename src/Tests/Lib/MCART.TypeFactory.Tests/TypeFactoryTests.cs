﻿/*
TypeFactoryTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Reflection;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.TypeFactory.Tests;

public class TypeFactoryTests
{
    private class TestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    private static readonly MCART.Types.TypeFactory _factory = new($"{typeof(TypeFactoryTests).FullName}._Generated");
    [Test]
    public void Build_Simple_Type_Test()
    {
        System.Reflection.Emit.TypeBuilder t = _factory.NewClass("GreeterClass");
        PropertyBuildInfo nameProp = t.AddAutoProperty<string>("Name");
        //t.AddComputedProperty<string>("Greeting", p => p
        //    .LoadConstant("Hello, ")
        //    .LoadProperty(nameProp)
        //    .Call<Func<string?, string?, string>>(string.Concat)
        //    .Return());

        object greeterInstance = t.New();
        ((dynamic)greeterInstance).Name = "Jhon";

        Assert.That("TheXDS.MCART.TypeFactory.Tests.TypeFactoryTests._Generated", Is.EqualTo(t.Namespace));
        Assert.That("Jhon", Is.EqualTo((string)((dynamic)greeterInstance).Name));
        //Assert.AreEqual("Hello, Jhon", (string)((dynamic)greeterInstance).Greeting);
    }

    [Test]
    public void Factory_exposes_dynamic_assembly()
    {
        Assert.That(_factory.Assembly, Is.InstanceOf<Assembly>());
        Assert.That(_factory.Assembly.IsDynamic);
    }
}
