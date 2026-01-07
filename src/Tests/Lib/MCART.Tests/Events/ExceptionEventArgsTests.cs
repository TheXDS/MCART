// ExceptionEventArgsTests.cs
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

using TheXDS.MCART.Events;

namespace TheXDS.MCART.Tests.Events;

public class ExceptionEventArgsTests
{
    [Test]
    public void Class_inherits_from_ValueEventArgs_Exception()
    {
        var evt = new ExceptionEventArgs();
        Assert.That(evt, Is.AssignableTo<ValueEventArgs<Exception>>());
    }

    [Test]
    public void Value_property_is_null_on_default_ctor()
    {
        var evt = new ExceptionEventArgs();
        Assert.That(evt.Value, Is.Null);
    }

    [Test]
    public void Value_property_equals_exception_from_ctor()
    {
        var ex = new Exception();
        var evt = new ExceptionEventArgs(ex);
        Assert.That(evt.Value, Is.SameAs(ex));
    }

    [Test]
    public void Class_supports_implicit_conversion_from_ex()
    {
        var ex = new IOException();
        ExceptionEventArgs evt = ex;
        Assert.That(evt.Value, Is.SameAs(ex));
    }
}
