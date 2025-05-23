﻿/*
ScColorTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using TheXDS.MCART.Types.Entity;

namespace TheXDS.MCART.Ext.ComplexTypes.Tests;

public class ScColorTests
{
    [Test]
    public void GetHashCode_test()
    {
        ScColor c1 = new() { ScA = 1f, ScB = 0.25f, ScG = 0.5f, ScR = 0.75f };
        ScColor c2 = new() { ScA = 1f, ScB = 0.25f, ScG = 0.5f, ScR = 0.75f };
        ScColor c3 = new() { ScA = 1f, ScB = 0.5f, ScG = 0.5f, ScR = 0.5f };

        Assert.That(c1, Is.Not.SameAs(c2));
        Assert.That(c1.GetHashCode(), Is.EqualTo(c2.GetHashCode()));
        Assert.That(c1.GetHashCode(), Is.Not.EqualTo(c3.GetHashCode()));
    }

    [Test]
    public void ComplexTypeToNormalTypeTest()
    {
        ScColor c1 = new() { ScA = 1f, ScB = 0.25f, ScG = 0.5f, ScR = 0.75f };
        Types.Color c2 = new(0.75f, 0.5f, 0.25f, 1f);

        Assert.That(c2, Is.EqualTo((Types.Color)c1));
        Assert.That(c1, Is.EqualTo((ScColor)c2));
    }

    [Test]
    public void Equals_obj_test()
    {
        ScColor c1 = new() { ScA = 1f, ScB = 0.25f, ScG = 0.5f, ScR = 0.75f };
        ScColor c2 = new() { ScA = 1f, ScB = 0.25f, ScG = 0.5f, ScR = 0.75f };
        ScColor c3 = new() { ScA = 1f, ScB = 0.5f, ScG = 0.5f, ScR = 0.5f };

        Assert.That(c1.Equals((object?)null), Is.False);
        Assert.That(c1!.Equals((object?)c3), Is.False);
        Assert.That(c1.Equals((object?)new object()), Is.False);
        Assert.That(c1.Equals((object?)c1));
        Assert.That(c1.Equals((object?)c2));
    }

    [Test]
    public void Equals_color_test()
    {
        ScColor c1 = new() { ScA = 1f, ScB = 0.25f, ScG = 0.5f, ScR = 0.75f };
        ScColor c2 = new() { ScA = 1f, ScB = 0.25f, ScG = 0.5f, ScR = 0.75f };
        ScColor c3 = new() { ScA = 1f, ScB = 0.5f, ScG = 0.5f, ScR = 0.5f };

        Assert.That(c1.Equals(null), Is.False);
        Assert.That(c1!.Equals((ScColor?)c3), Is.False);
        Assert.That(c1.Equals((ScColor?)c1));
        Assert.That(c1.Equals((ScColor?)c2));
    }
}
