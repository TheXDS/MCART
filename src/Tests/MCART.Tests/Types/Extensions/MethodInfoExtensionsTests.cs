/*
MethodInfoExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Types.Extensions.MethodInfoExtensions;

public class MethodInfoExtensionsTests
{
    private static string TestStatic() => "TestStatic";
    private string TestInstance() => "TestInstance";

    [Test]
    public void ToDelegateTest()
    {
        Func<string>? ts = ReflectionHelpers.GetMethod<Func<string>>(() => TestStatic).ToDelegate<Func<string>>();
        Func<string>? ti = ReflectionHelpers.GetMethod<Func<string>>(() => TestInstance).ToDelegate<Func<string>>(this);

        Assert.NotNull(ts);
        Assert.IsAssignableFrom<Func<string>>(ts);
        Assert.AreEqual("TestStatic", ts!.Invoke());

        Assert.NotNull(ti);
        Assert.IsAssignableFrom<Func<string>>(ti);
        Assert.AreEqual("TestInstance", ti!.Invoke());
    }

    [Test]
    public void ToDelegate_Contract_Test()
    {
        Assert.Throws<MemberAccessException>(() =>
            ReflectionHelpers.GetMethod<Func<string>>(() => TestStatic).ToDelegate<Func<string>>(this));
        Assert.Throws<MemberAccessException>(() =>
            ReflectionHelpers.GetMethod<Func<string>>(() => TestInstance).ToDelegate<Func<string>>());
    }

    [Test]
    public void IsVoidTest()
    {
        Assert.True(ReflectionHelpers.GetMethod<Action>(() => IsVoidTest).IsVoid());
        Assert.False(ReflectionHelpers.GetMethod<Func<object, object, bool>>(() => Equals).IsVoid());
    }
}