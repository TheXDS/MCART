/*
DisposableTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#pragma warning disable CS1591

using TheXDS.MCART.Types.Base;
using Xunit;

namespace TheXDS.MCART.Tests.Types.Base
{
    public class DisposableTests
    {
        private class DisposableOne : Disposable
        {
            protected override void OnDispose()
            {
                /* No hacer nada. */
            }
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
        }

        [Fact]
        public void DisposeVsFinalizeTest()
        {
            var m1 = new DisposableOne();
            using (m1)
            {
                Assert.False(m1.Disposed);
            }
            Assert.True(m1.Disposed);

            var m2 = new DisposableTwo();
            using (m2)
            {
                Assert.False(m2.Disposed);
            }
            Assert.True(m2.Disposed);
        }
    }
}
