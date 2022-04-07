﻿/*
DictionaryExtensionsTests.cs

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
