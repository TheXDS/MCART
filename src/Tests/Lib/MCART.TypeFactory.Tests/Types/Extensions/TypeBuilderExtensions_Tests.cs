// TypeBuilderExtensions_Tests.cs
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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.TypeFactory.Tests.Types.Extensions;

public class TypeBuilderExtensions_Tests : TypeFactoryTestClassBase
{
    [Test]
    public void AddConstantProperty_Test()
    {
        var t = Factory.NewClass("ConstantPropertyTestClass");
        t.AddConstantProperty("HelloProperty", "Hello");
        var obj = t.New();
        PropertyInfo prop = obj.GetType().GetProperty("HelloProperty")!;
        Assert.That(prop.CanRead);
        Assert.That(prop.CanWrite, Is.False);
        Assert.That((string)prop.GetValue(obj)!, Is.EqualTo("Hello"));
    }

    [Test]
    public void AddEvent_test()
    {
        var t = Factory.NewClass("EventTestClass");
        var e = t.AddEvent("SomeEvent");
        var obj = t.New();
        var triggered = false;

        EventInfo ev = obj.GetType().GetEvent("SomeEvent")!;
        ev.AddEventHandler(obj, (EventHandler)((sender, e) =>
        {
            triggered = true;
            Assert.That(sender, Is.SameAs(obj));
        }));
        ev.RaiseMethod!.Invoke(obj, [obj, EventArgs.Empty]);

        Assert.That(triggered, Is.True);
    }

    [ExcludeFromCodeCoverage]
    public abstract class VirtualMethodTestBase
    {
        protected virtual void VirtualMethod() { }
        protected virtual int GetValue() => 42;
        protected virtual int Add(int a, int b) => a + b;
        protected virtual string GetMessage() => "Base";
    }

    [ExcludeFromCodeCoverage]
    public sealed class ConcreteVirtualMethodTestBase : VirtualMethodTestBase
    {
    }

    [Test]
    public void AddOverride_ReturnsMethodBuildInfo()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassOverride").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var methodBuildInfo = tb.AddOverride(methodToOverride);

        Assert.That(methodBuildInfo, Is.Not.Null);
        Assert.That(methodBuildInfo.Member, Is.Not.Null);
        Assert.That(methodBuildInfo.Member.Name, Is.EqualTo("VirtualMethod"));
    }

    [Test]
    public void AddOverride_CreatesCallableOverride()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassCallableOverride").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        var overrideBuildInfo = tb.AddOverride(methodToOverride);
        overrideBuildInfo.Il
            .Nop()
            .Return();

        var derivedInstance = tb.CreateType()!.New<VirtualMethodTestBase>();
        var overriddenMethod = derivedInstance.GetType().GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        Assert.That(overriddenMethod, Is.Not.Null);
        Assert.That(() => overriddenMethod.Invoke(derivedInstance, []), Throws.Nothing);
    }

    [Test]
    public void AddOverride_WithReturnType()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassWithReturnOverride").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("GetValue", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        var overrideBuildInfo = tb.AddOverride(methodToOverride);
        overrideBuildInfo.Il
            .LoadConstant(100)
            .Return();

        var derivedInstance = tb.CreateType()!.New<VirtualMethodTestBase>();
        var overriddenMethod = derivedInstance.GetType().GetMethod("GetValue", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var result = (int)overriddenMethod.Invoke(derivedInstance, [])!;
        
        Assert.That(result, Is.EqualTo(100));
    }

    [Test]
    public void AddOverride_WithParameters()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassWithParameterOverride").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance, null, [typeof(int), typeof(int)], null)!;
        
        var overrideBuildInfo = tb.AddOverride(methodToOverride);
        overrideBuildInfo.Il
            .LoadArg1()
            .LoadArg2()
            .Multiply()
            .Return();

        var derivedInstance = tb.CreateType()!.New<VirtualMethodTestBase>();
        var overriddenMethod = derivedInstance.GetType().GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var result = (int)overriddenMethod.Invoke(derivedInstance, [5, 3])!;
        
        Assert.That(result, Is.EqualTo(15));
    }

    [Test]
    public void AddOverride_OverrideIsVirtual()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassVirtualCheck").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        var overrideBuildInfo = tb.AddOverride(methodToOverride);
        overrideBuildInfo.Il
            .Nop()
            .Return();

        var derivedType = tb.CreateType()!;

        var overriddenMethod = derivedType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        Assert.That(overriddenMethod.IsVirtual, Is.True);
    }

    [Test]
    public void AddOverride_MethodNamePreserved()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassMethodNameTest").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        var overrideBuildInfo = tb.AddOverride(methodToOverride);
        overrideBuildInfo.Il
            .Nop()
            .Return();

        var derivedType = tb.CreateType()!;

        var method = derivedType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.That(method, Is.Not.Null);
        Assert.That(method!.Name, Is.EqualTo("VirtualMethod"));
    }

    [Test]
    public void AddOverride_ILGeneratorAvailable()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassILGeneratorTest").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        var overrideBuildInfo = tb.AddOverride(methodToOverride);

        Assert.That(overrideBuildInfo.Il, Is.Not.Null);
        Assert.That(overrideBuildInfo.Member, Is.Not.Null);
    }

    [Test]
    public void AddOverride_PolymorphicBehavior()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassPoly").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride = baseType.GetMethod("GetMessage", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        var overrideBuildInfo = tb.AddOverride(methodToOverride);
        overrideBuildInfo.Il
            .LoadConstant("Derived")
            .Return();

        var baseInstance = new ConcreteVirtualMethodTestBase();
        var derivedInstance = tb.CreateType()!.New<VirtualMethodTestBase>();
        
        var baseMethod = baseType.GetMethod("GetMessage", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var baseResult = (string)baseMethod.Invoke(baseInstance, [])!;
        var derivedResult = (string)baseMethod.Invoke(derivedInstance, [])!;
        
        Assert.That(baseResult, Is.EqualTo("Base"));
        Assert.That(derivedResult, Is.EqualTo("Derived"));
    }

    [Test]
    public void AddOverride_MultipleOverridesInSameClass()
    {
        var baseType = typeof(VirtualMethodTestBase);
        var tb = Factory.NewClass<VirtualMethodTestBase>("DerivedClassMultiOverride").Builder;
        tb.AddPublicConstructor().CallBaseCtor<VirtualMethodTestBase>().Return();

        var methodToOverride1 = baseType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var methodToOverride2 = baseType.GetMethod("GetValue", BindingFlags.NonPublic | BindingFlags.Instance)!;
        
        var override1 = tb.AddOverride(methodToOverride1);
        override1.Il
            .Nop()
            .Return();

        var override2 = tb.AddOverride(methodToOverride2);
        override2.Il
            .LoadConstant(99)
            .Return();

        var derivedType = tb.CreateType()!;
        var derivedInstance = derivedType.New<VirtualMethodTestBase>();
        
        var method1Result = derivedType.GetMethod("VirtualMethod", BindingFlags.NonPublic | BindingFlags.Instance);
        var method2Result = derivedType.GetMethod("GetValue", BindingFlags.NonPublic | BindingFlags.Instance);
        
        Assert.That(method1Result, Is.Not.Null);
        Assert.That(method2Result, Is.Not.Null);

        method1Result!.Invoke(derivedInstance, []);
        var value = (int)method2Result!.Invoke(derivedInstance, [])!;
        Assert.That(value, Is.EqualTo(99));
    }
}
