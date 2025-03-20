/*
AutoDictionary_Tests.cs

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

using TheXDS.MCART.Types;

namespace TheXDS.MCART.Tests.Types;

public class AutoDictionary_Tests
{
    [Test]
    public void Dictionary_creates_new_keys_if_not_found()
    {
        var dict = new AutoDictionary<string, Random>();
        Assert.That(dict.Count, Is.EqualTo(0));
        var r = dict["test"];
        Assert.That(dict.Count, Is.EqualTo(1));
        Assert.That(r, Is.InstanceOf<Random>());
    }

    [Test]
    public void Dictionary_forwards_set_to_base_class()
    {
        var dict = new AutoDictionary<string, Random>();
        dict["test"] = new Random();
        Assert.That(dict.Count, Is.EqualTo(1));
    }
}

public class OpenList_Tests
{
    [Test]
    public void OpenList_adds_items()
    {
        var list = new OpenList<string>();
        Assert.That(list.Count, Is.EqualTo(0));
        list.Add("test");
        Assert.That(list.Count, Is.EqualTo(1));
    }

    [Test]
    public void OpenList_removes_items()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        list.Remove("test");
        Assert.That(list.Count, Is.EqualTo(0));
    }

    [Test]
    public void OpenList_clears_items()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        list.Clear();
        Assert.That(list.Count, Is.EqualTo(0));
    }

    [Test]
    public void OpenList_enumerates_items()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        foreach (var i in list)
        {
            Assert.That(i, Is.EqualTo("test"));
        }
    }

    [Test]
    public void OpenList_contains_items()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list.Contains("test"), Is.True);
    }
    
    [Test]
    public void OpenList_indexer()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.That(list[0], Is.EqualTo("test"));
    }

    [Test]
    public void OpenList_indexer_set()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        list[0] = "test2";
        Assert.That(list[0], Is.EqualTo("test2"));
    }

    [Test]
    public void OpenList_indexer_out_of_range()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = list[1]);
    }

    [Test]
    public void OpenList_indexer_set_out_of_range()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => list[1] = "test2");
    }

    [Test]
    public void OpenList_indexer_negative()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = list[-1]);
    }

    [Test]
    public void OpenList_indexer_set_negative()
    {
        var list = new OpenList<string>
        {
            "test"
        };
        Assert.That(list.Count, Is.EqualTo(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => list[-1] = "test2");
    }
}