// FieldBuilderExtensionsTests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2026 César Andrés Morgan
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

using System.Reflection;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.Types;
using TheXDS.MCART.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public class FieldBuilderExtensions_Tests : TypeFactoryTestClassBase
{
    [Test]
    public void InitField_with_primitive_value_test()
    {
        var t = Factory.NewClass("InitField_Test");
        var field = t.DefineField("IntField", typeof(int),FieldAttributes.Public);
        var ctorIl = t.AddPublicConstructor().CallBaseCtor<object>();
        field.InitField(ctorIl, 1);
        ctorIl.Return();
        var obj = t.New();
        var f = obj.GetType().GetField("IntField")!;
        Assert.That(f.GetValue(obj), Is.EqualTo(1));
    }

    [Test]
    public void InitField_with_reference_type_value_test()
    {
        var t = Factory.NewClass("InitField_refType");
        var field = t.DefineField("ObjField", typeof(Random), FieldAttributes.Public);
        var ctorIl = t.AddPublicConstructor().CallBaseCtor<object>();
        field.InitField(ctorIl, typeof(Random));
        ctorIl.Return();
        var obj = t.New();
        var f = obj.GetType().GetField("ObjField")!;
        Assert.That(f.GetValue(obj), Is.InstanceOf<Random>());
    }

    [Test]
    public void InitField_with_value_inference_test()
    {
        var fieldName = $"PointField";
        var t = Factory.NewClass($"Init_infered_{fieldName}_Test");
        var field = t.DefineField(fieldName, typeof(Point), FieldAttributes.Public);
        var ctorIl = t.AddPublicConstructor().CallBaseCtor<object>();
        field.InitField<Point>(ctorIl);
        ctorIl.Return();
        var obj = t.New();
        var f = obj.GetType().GetField(fieldName)!;
        Assert.That(f.GetValue(obj), Is.EqualTo(default(Point)));
    }

    [Test]
    public void InitField_when_type_is_not_instantiable_test()
    {
        var fieldName = $"GuidField";
        var t = Factory.NewClass($"Init_infered_Guid_Test");
        var field = t.DefineField(fieldName, typeof(Guid), FieldAttributes.Public);
        var ctorIl = t.AddPublicConstructor().CallBaseCtor<object>();
        Assert.That(() => field.InitField<Guid>(ctorIl), Throws.InstanceOf<ClassNotInstantiableException>());
    }

    [ExcludeFromCodeCoverage]
    public abstract class AbstractType { }

    [Test]
    public void InitField_when_type_is_abstract_test()
    {
        var fieldName = $"AbstractField";
        var t = Factory.NewClass($"Init_infered_Abstract_Test");
        var field = t.DefineField(fieldName, typeof(AbstractType), FieldAttributes.Public);
        var ctorIl = t.AddPublicConstructor().CallBaseCtor<object>();
        Assert.That(() => field.InitField(ctorIl, typeof(AbstractType)), Throws.InstanceOf<InvalidTypeException>());
    }

    [Test]
    public void InitField_with_value_inference_test2()
    {
        var fieldName = $"RandomField";
        var t = Factory.NewClass($"Init_infered_{fieldName}_Test");
        var field = t.DefineField(fieldName, typeof(Random), FieldAttributes.Public);
        var ctorIl = t.AddPublicConstructor().CallBaseCtor<object>();
        field.InitField<Random>(ctorIl);
        ctorIl.Return();
        var obj = t.New();
        var f = obj.GetType().GetField(fieldName)!;
        Assert.That(f.GetValue(obj), Is.InstanceOf<Random>());
    }
}
