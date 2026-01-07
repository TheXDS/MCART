/*
CopyrightAttributeTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Tests.Attributes;

public class CopyrightAttributeTests
{
    [Test]
    public void Ctor_with_full_copyright_string_passes_string_through()
    {
        CopyrightAttribute? l = new("Copyright (C) Test");
        Assert.That(l.Value, Is.EqualTo("Copyright (C) Test"));
    }

    [Test]
    public void Ctor_with_simple_string_creates_copyright_formatted_string()
    {
        CopyrightAttribute l = new("Test");
        Assert.That(l.Value, Is.EqualTo("Copyright © Test"));
    }

    [Test]
    public void Ctor_with_year_and_simple_string_creates_copyright_formatted_string()
    {
        CopyrightAttribute l = new(1985, "Test");
        Assert.That(l.Value, Is.EqualTo("Copyright © 1985 Test"));
    }

    [Test]
    public void Ctor_with_years_range_and_simple_string_creates_copyright_formatted_string()
    {
        CopyrightAttribute l = new(new Range<int>(1985, 2001), "Test");
        Assert.That(l.Value, Is.EqualTo("Copyright © 1985-2001 Test"));
    }

    [Test]
    public void Ctor_with_years_and_simple_string_creates_copyright_formatted_string()
    {
        CopyrightAttribute l = new(1985, 2001, "Test");
        Assert.That(l.Value, Is.EqualTo("Copyright © 1985-2001 Test"));
    }

    [Test]
    public void Attribute_can_be_cast_to_AssemblyCopyrightAttribute()
    {
        AssemblyCopyrightAttribute l = new CopyrightAttribute("Test");
        Assert.That(l.Copyright, Is.EqualTo("Copyright © Test"));
    }

    [Test]
    public void Attribute_can_be_cast_from_AssemblyCopyrightAttribute()
    {
        CopyrightAttribute l = new AssemblyCopyrightAttribute("Test");
        Assert.That(l.Value, Is.EqualTo("Copyright © Test"));
    }
}
