/*
DictionaryExtensionsTests.cs

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
using System.Collections.Generic;
using TheXDS.MCART.Types.Extensions;

public class DictionaryExtensionsTests
{
    [Test]
    public void CheckCircularRef_Test()
    {
        Dictionary<char, IEnumerable<char>>? d = new()
        {
            { 'a', new[] { 'b', 'c' } },
            { 'b', new[] { 'c', 'd' } },
            { 'c', new[] { 'd', 'e' } },
        };

        Assert.False(d.CheckCircularRef('a'));
        Assert.False(((IEnumerable<KeyValuePair<char, IEnumerable<char>>>)d).CheckCircularRef('a'));
        d.Add('d', new[] { 'e', 'a' });
        Assert.True(d.CheckCircularRef('a'));
        Assert.True(((IEnumerable<KeyValuePair<char, IEnumerable<char>>>)d).CheckCircularRef('a'));
    }

    [Test]
    public void CheckCircularRef_Test2()
    {
        Dictionary<char, ICollection<char>>? d = new()
        {
            { 'a', new[] { 'b', 'c' } },
            { 'b', new[] { 'c', 'd' } },
            { 'c', new[] { 'd', 'e' } },
        };

        Assert.False(d.CheckCircularRef('a'));
        Assert.False(((IEnumerable<KeyValuePair<char, ICollection<char>>>)d).CheckCircularRef('a'));
        d.Add('d', new[] { 'e', 'a' });
        Assert.True(((IEnumerable<KeyValuePair<char, ICollection<char>>>)d).CheckCircularRef('a'));
        Assert.True(d.CheckCircularRef('a'));
    }

    [Test]
    public void Push_Test()
    {
        Dictionary<int, string>? d = new();
        Assert.IsAssignableFrom<string>(d.Push(1, "test"));
    }

    [Test]
    public void Pop_Test()
    {
        Dictionary<int, string>? d = new()
        {
            { 1, "test" },
            { 2, "test2" }
        };

        Assert.True(d.Pop(1, out string? s));
        Assert.AreEqual("test", s);
        Assert.False(d.ContainsKey(1));
        Assert.False(d.ContainsValue("test"));
        Assert.False(d.Pop(3, out string? s2));
        Assert.Null(s2);
    }
}
