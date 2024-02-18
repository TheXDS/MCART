// TypeBuilderExtensions_Tests.cs
// 
// This file is part of Morgan's CLR Advanced Runtime (MCART)
// 
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
// 
// Released under the MIT License (MIT)
// Copyright © 2011 - 2024 César Andrés Morgan
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
        ev.AddEventHandler(obj, (EventHandler)((object? sender, EventArgs e) =>
        {
            triggered = true;
            Assert.That(sender, Is.SameAs(obj));
        }));
        ev.RaiseMethod!.Invoke(obj, new[] { obj, EventArgs.Empty });

        Assert.That(triggered, Is.True);
    }
}
