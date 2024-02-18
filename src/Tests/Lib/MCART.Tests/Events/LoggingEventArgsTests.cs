﻿// LoggingEventArgsTests.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2024 César Andrés Morgan
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

public class LoggingEventArgsTests
{
    [Test]
    public void Class_inherits_from_ValueEventArgs_string()
    {
        var evt = new LoggingEventArgs("Test");
        Assert.That(evt, Is.AssignableTo<ValueEventArgs<string>>());
    }

    [Test]
    public void Event_includes_Value_property()
    {
        var evt = new LoggingEventArgs("Test");
        Assert.That(evt.Value, Is.EqualTo("Test"));
        Assert.That(evt.Subject, Is.Null);
    }

    [Test]
    public void Event_includes_Subject_property()
    {
        var obj = new object();
        var evt = new LoggingEventArgs(obj, "Obj");
        Assert.That(evt.Value, Is.EqualTo("Obj"));
        Assert.That(evt.Subject, Is.SameAs(obj));
    }
}
