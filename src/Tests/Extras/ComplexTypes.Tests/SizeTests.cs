﻿/*
SizeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the “Software”), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/

using NUnit.Framework;
using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Ext.ComplexTypes.Tests;

public class SizeTests
{
    [Test]
    public void Ctor_test()
    {
        Size c1 = new() { Width = 15, Height = 8 };
        Size c2 = new(15, 8);
        Assert.AreEqual(c1, c2);
    }

    [Test]
    public void ComplexTypeToNormalTypeTest()
    {
        Size c1 = new() { Width = 15, Height = 8 };
        Types.Size c2 = new(15, 8);

        Assert.AreEqual(c2, (Types.Size)c1);
        Assert.AreEqual(c1, (Size)c2);
    }

    [Test]
    public void GetHashCode_test()
    {
        Size c1 = new() { Width = 15, Height = 8 };
        Size c2 = new() { Width = 15, Height = 8 };

        Assert.AreNotSame(c1, c2);
        Assert.AreEqual(c1.GetHashCode(), c2.GetHashCode());
    }

    [Test]
    public void Equals_obj_test()
    {
        Size c1 = new() { Width = 15, Height = 8 };
        Size c2 = new() { Width = 15, Height = 8 };
        Size c3 = new() { Width = 12, Height = 7 };

        Assert.IsFalse(c1.Equals((object?)null));
        Assert.IsFalse(c1!.Equals((object?)c3));
        Assert.IsFalse(c1.Equals((object?)new object()));
        Assert.IsTrue(c1.Equals((object?)c1));
        Assert.IsTrue(c1.Equals((object?)c2));
    }

    [Test]
    public void Equals_size_test()
    {
        Size c1 = new() { Width = 15, Height = 8 };
        Size c2 = new() { Width = 15, Height = 8 };
        Size c3 = new() { Width = 12, Height = 7 };

        Assert.IsFalse(c1.Equals((Size?)null));
        Assert.IsFalse(c1!.Equals((Size?)c3));
        Assert.IsTrue(c1.Equals((Size?)c1));
        Assert.IsTrue(c1.Equals((Size?)c2));
        Assert.IsTrue(c1 == c2);
        Assert.IsTrue(c1 != c3);
    }
}