// ProgressionEventArgsTests.cs
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

public class ProgressionEventArgsTests
{
    [Test]
    public void Class_inherits_from_ValueEventArgs_double()
    {
        var evt = new ProgressionEventArgs(0.0);
        Assert.That(evt, Is.AssignableTo<ValueEventArgs<double>>());
    }

    [Test]
    public void Event_includes_Value_property()
    {
        var evt = new ProgressionEventArgs(0.5);
        Assert.That(evt.Value, Is.EqualTo(0.5));
        Assert.That(evt.HelpText, Is.Null);
    }

    [Test]
    public void Event_includes_HelpText_property()
    {
        var evt = new ProgressionEventArgs(1.0, "Test");
        Assert.That(evt.Value, Is.EqualTo(1.0));
        Assert.That(evt.HelpText, Is.EqualTo("Test"));
    }

    [Test]
    public void Ctor_contract_test()
    {
        Assert.That(() => new ProgressionEventArgs(double.NaN), Throws.InstanceOf<ArgumentOutOfRangeException>());
        Assert.That(() => new ProgressionEventArgs(1.1), Throws.InstanceOf<ArgumentOutOfRangeException>());
        Assert.That(() => new ProgressionEventArgs(-0.1), Throws.InstanceOf<ArgumentOutOfRangeException>());
        Assert.That(() => new ProgressionEventArgs(double.PositiveInfinity), Throws.InstanceOf<ArgumentOutOfRangeException>());
        Assert.That(() => new ProgressionEventArgs(double.NegativeInfinity), Throws.InstanceOf<ArgumentOutOfRangeException>());
    }
}