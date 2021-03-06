﻿/*
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

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using Xunit;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.Tests.Types.Base
{
    public class DisposableTests
    {
        private class DisposableOne : Disposable
        {
            protected override void OnDispose()
            {
                DidOnDisposeRun = true;
            }

            public bool ShouldFinalize => GetType().GetMethod(nameof(OnFinalize), Instance | NonPublic)!.IsOverride();

            public bool DidOnDisposeRun { get; private set; }
        }

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

        [Fact]
        public void OnDisposeExecutionTest()
        {
            var m1 = new DisposableOne();
            using (m1)
            {
                Assert.False(m1.DidOnDisposeRun);
            }
            Assert.True(m1.IsDisposed);
            Assert.True(m1.DidOnDisposeRun);
        }

        [Fact]
        public void DisposeVsFinalizeTest()
        {
            var m1 = new DisposableOne();
            using (m1)
            {
                Assert.False(m1.IsDisposed);
                Assert.False(m1.ShouldFinalize);
            }
            Assert.True(m1.IsDisposed);

            var m2 = new DisposableTwo();
            using (m2)
            {
                Assert.False(m2.IsDisposed);
                Assert.True(m2.ShouldFinalize);
            }
            Assert.True(m2.IsDisposed);
        }
    }
}
