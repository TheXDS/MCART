/*
DisposableTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Tests.Types.Base;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;

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
            Assert.False(m1.DidOnDisposeRun);
        }
        Assert.True(m1.IsDisposed);
        Assert.True(m1.DidOnDisposeRun);
    }

    [Test]
    public void Call_Dispose_multiple_times()
    {
        DisposableOne m1 = new();
        m1.Dispose();
        Assert.IsTrue(m1.IsDisposed);
        Assert.IsTrue(m1.DidOnDisposeRun);
        m1.Dispose();
        m1.DidOnDisposeRun = false;
        Assert.IsTrue(m1.IsDisposed);
        Assert.IsFalse(m1.DidOnDisposeRun);
    }

    [Test]
    public void DisposeVsFinalizeTest()
    {
        DisposableOne m1 = new();
        using (m1)
        {
            Assert.False(m1.IsDisposed);
            Assert.False(m1.ShouldFinalize);
        }
        Assert.True(m1.IsDisposed);

        DisposableTwo m2 = new();
        using (m2)
        {
            Assert.False(m2.IsDisposed);
            Assert.True(m2.ShouldFinalize);
        }
        Assert.True(m2.IsDisposed);
    }
}
