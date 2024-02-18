/*
TypeExtensionsTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.TypeBuilderHelpers;

namespace TheXDS.MCART.TypeFactory.Tests.Helpers;

public class TypeBuilderHelpersTests
{
    [ExcludeFromCodeCoverage]
    private class PrivateClass { }

    [TestCase("I", "_i")]
    [TestCase("Test", "_test")]
    [TestCase("CamelCase", "_camelCase")]
    public void UndName_test(string input, string result)
    {
        Assert.That(UndName(input), Is.EqualTo(result));
    }

    [Test]
    public void UndName_contract_test()
    {
        Assert.That(() => UndName(null!), Throws.ArgumentNullException);
    }

    [TestCase("IEnumerable", "Enumerable")]
    [TestCase("ICamelCase", "CamelCase")]
    [TestCase("Test", "TestImplementation")]
    [TestCase("CamelCase", "CamelCaseImplementation")]
    public void NoIfaceName_test(string input, string result)
    {
        Assert.That(NoIfaceName(input), Is.EqualTo(result));
    }

    [Test]
    public void NoIfaceName_contract_test()
    {
        Assert.That(() => NoIfaceName(null!), Throws.ArgumentNullException);
    }

    [TestCase(MemberAccess.Private, MethodAttributes.Private)]
    [TestCase(MemberAccess.Protected, MethodAttributes.Family)]
    [TestCase(MemberAccess.Internal, MethodAttributes.Assembly)]
    [TestCase(MemberAccess.Public, MethodAttributes.Public)]
    public void Access_test(MemberAccess input, MethodAttributes expected)
    {
        Assert.That(Access(input), Is.EqualTo(expected));
    }

    [Test]
    public void Access_contract_test()
    {
        Assert.That(() => Access((MemberAccess)240), Throws.InstanceOf<NotImplementedException>());
    }
}
