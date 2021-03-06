﻿/*
TaskExtensionsTest.cs

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

#pragma warning disable CS1591

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static TheXDS.MCART.Types.Extensions.TaskExtensions;

namespace TheXDS.MCART.Tests.Types.Extensions
{
    public class TaskExtensionsTest
    {
        [Fact]
        public async Task WithCancellation_Void_Test()
        {
            /* Los temporizadores en esta prueba se han seleccionado con
             * muchísimo delta, debido a la posibilidad de fallos al ejecutarse
             * en entornos que puedan presentar muchísima latencia el ejecutar,
             * como un servidor de CI.
             */
            var t = new System.Diagnostics.Stopwatch();
            
            t.Start();
            await Assert.ThrowsAsync<TaskCanceledException>(() => Task.Delay(100000).WithCancellation(new CancellationTokenSource(500).Token));
            t.Stop();
            Assert.True(t.ElapsedMilliseconds < 100000);

            t.Start();
            await Task.Delay(500).WithCancellation(new CancellationTokenSource(100000).Token);
            t.Stop();
            Assert.True(t.ElapsedMilliseconds < 5000);
        }

        [Fact]
        public async Task WithCancellation_RetVal_Test()
        {
            /* Los temporizadores en esta prueba se han seleccionado con
             * muchísimo delta, debido a la posibilidad de fallos al ejecutarse
             * en entornos que puedan presentar muchísima latencia el ejecutar,
             * como un servidor de CI.
             */
            static async Task<int> Get5Async(int delay)
            {
                await Task.Delay(delay);
                return 5;
            }

            var t = new System.Diagnostics.Stopwatch();

            t.Start();
            await Assert.ThrowsAsync<TaskCanceledException>(() => Get5Async(100000).WithCancellation(new CancellationTokenSource(500).Token));
            t.Stop();
            Assert.True(t.ElapsedMilliseconds < 100000);

            t.Start();
            Assert.Equal(5, await Get5Async(500).WithCancellation(new CancellationTokenSource(100000).Token));
            t.Stop();
            Assert.True(t.ElapsedMilliseconds < 5000);
        }

    }
}