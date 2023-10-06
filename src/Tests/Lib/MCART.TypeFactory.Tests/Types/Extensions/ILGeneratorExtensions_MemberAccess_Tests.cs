// ILGeneratorExtensions_MemberAccess_Tests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2023 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using NUnit.Framework;
using System.Reflection;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.ILGeneratorExtensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public partial class ILGeneratorExtensions_Tests : TypeFactoryTestClassBase
{
    [Test]
    public void Field_access_test()
    {
        var tb = Factory.NewClass("Field_access_test_Class");
        var field = tb.DefineField("test", typeof(int), FieldAttributes.Public);
        tb.DefineMethod("TestSet", MethodAttributes.Public, null, new[] { typeof(int) }).GetILGenerator()
            .LoadArg0().StoreField(field, ILGeneratorExtensions.LoadArg1).Return();
        tb.DefineMethod("TestGet", MethodAttributes.Public, typeof(int), Type.EmptyTypes).GetILGenerator()
            .LoadArg0().LoadField(field).Return();
        var obj = tb.New();

        var setMethod = obj.GetType().GetMethod("TestSet")!;
        var getMethod = obj.GetType().GetMethod("TestGet")!;

        Assert.That(getMethod.Invoke(obj, Array.Empty<object>()), Is.EqualTo(default(int)));
        setMethod.Invoke(obj, new object[] { 3 });
        Assert.That(getMethod.Invoke(obj, Array.Empty<object>()), Is.EqualTo(3));
    }

    [Test]
    public void Property_Access_test()
    {
        var tb = NewClass();
        var prop = tb.AddAutoProperty<int>("TestProperty");
        tb.DefineMethod("TestSet", MethodAttributes.Public, null, new[] { typeof(int) }).GetILGenerator()
            .StoreProperty(prop, ILGeneratorExtensions.LoadArg1).Return();
        tb.DefineMethod("TestGet", MethodAttributes.Public, typeof(int), Type.EmptyTypes).GetILGenerator()
            .LoadProperty(prop).Return();
        var obj = tb.New();
        var setMethod = obj.GetType().GetMethod("TestSet")!;
        var getMethod = obj.GetType().GetMethod("TestGet")!;
        Assert.That(getMethod.Invoke(obj, Array.Empty<object>()), Is.EqualTo(default(int)));
        setMethod.Invoke(obj, new object[] { 3 });
        Assert.That(getMethod.Invoke(obj, Array.Empty<object>()), Is.EqualTo(3));
    }
}
