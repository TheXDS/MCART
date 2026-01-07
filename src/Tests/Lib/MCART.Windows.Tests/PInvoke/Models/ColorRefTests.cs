// ColorRefTests.cs
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.PInvoke.Models;

namespace TheXDS.MCART.Windows.Tests.PInvoke.Models;

internal class ColorRefTests
{
    [Test]
    public void Struct_memory_layout_test()
    {
        Assert.That(Marshal.SizeOf<ColorRef>(), Is.EqualTo(4));
        var x = new ColorRef();
        Assert.That(x.R, Is.TypeOf<byte>());
        Assert.That(x.G, Is.TypeOf<byte>());
        Assert.That(x.B, Is.TypeOf<byte>());
        Assert.That(x.A, Is.TypeOf<byte>());
    }

    [Test]
    public void Ctor_initializes_fields()
    {
        var x = new ColorRef(0x55, 0x33, 0x0f);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(x.R, Is.EqualTo(0x55));
            Assert.That(x.G, Is.EqualTo(0x33));
            Assert.That(x.B, Is.EqualTo(0x0f));
            Assert.That(x.A, Is.EqualTo(0x00));
        }
    }
}
