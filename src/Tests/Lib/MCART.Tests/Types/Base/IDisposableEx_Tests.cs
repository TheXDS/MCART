/*
IDisposableEx_Tests.cs

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

using Moq;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Tests.Types.Base;

public class IDisposableEx_Tests
{
    [Test]
    public void TryDispose_calls_implementation()
    {
        var tm = new Mock<IDisposableEx>() { CallBase = true };
        tm.Setup(a => a.Dispose()).Verifiable(Times.Once);
        Assert.That(tm.Object.TryDispose());
        tm.Verify();
    }

    [Test]
    public void TryDispose_skips_if_disposed()
    {
        var tm = new Mock<IDisposableEx>() { CallBase = true };
        tm.Setup(a => a.IsDisposed).Returns(true);
        tm.Setup(a => a.Dispose()).Verifiable(Times.Never);
        Assert.That(tm.Object.TryDispose(), Is.False);
        tm.Verify();
    }

    [Test]
    public void TryDispose_returns_false_on_exception()
    {
        var tm = new Mock<IDisposableEx>() { CallBase = true };
        tm.Setup(a => a.Dispose()).Throws<Exception>();
        Assert.That(!tm.Object.TryDispose());
    }
}
