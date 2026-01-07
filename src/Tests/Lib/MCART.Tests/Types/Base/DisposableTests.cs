/*
DisposableTests.cs

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.Tests.Types.Base;

public class DisposableTests
{
    [ExcludeFromCodeCoverage]
    private class DisposableOne : Disposable
    {
        protected override void OnDispose()
        {
            DidOnDisposeRun = true;
        }

        public bool ShouldFinalize => GetType().GetMethod(nameof(OnFinalize), Instance | NonPublic)!.IsOverride();

        public bool DidOnDisposeRun { get; set; }
    }

    [ExcludeFromCodeCoverage]
    private class DisposableTwo : Disposable
    {
        protected override void OnDispose()
        {
            /* No hacer nada. */
        }

        protected override void OnFinalize()
        {
            /* No hacer nada. */
        }

        public bool ShouldFinalize => GetType().GetMethod(nameof(OnFinalize), Instance | NonPublic)!.IsOverride();
    }

    [Test]
    public void OnDisposeExecutionTest()
    {
        DisposableOne m1 = new();
        using (m1)
        {
            Assert.That(m1.DidOnDisposeRun, Is.False);
        }
        Assert.That(m1.IsDisposed);
        Assert.That(m1.DidOnDisposeRun);
    }

    [Test]
    public void Call_Dispose_multiple_times()
    {
        DisposableOne m1 = new();
        m1.Dispose();
        Assert.That(m1.IsDisposed);
        Assert.That(m1.DidOnDisposeRun);
        m1.Dispose();
        m1.DidOnDisposeRun = false;
        Assert.That(m1.IsDisposed);
        Assert.That(m1.DidOnDisposeRun, Is.False);
    }

    [Test]
    public void DisposeVsFinalizeTest()
    {
        DisposableOne m1 = new();
        using (m1)
        {
            Assert.That(m1.IsDisposed, Is.False);
            Assert.That(m1.ShouldFinalize, Is.False);
        }
        Assert.That(m1.IsDisposed);

        DisposableTwo m2 = new();
        using (m2)
        {
            Assert.That(m2.IsDisposed, Is.False);
            Assert.That(m2.ShouldFinalize);
        }
        Assert.That(m2.IsDisposed);
    }
}
