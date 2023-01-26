/*
TaskExtensionsTest.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

namespace TheXDS.MCART.Tests.Types.Extensions;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Types.Extensions.TaskExtensions;

public class TaskExtensionsTest
{
    [Test]
    public async Task WithCancellation_Void_Test()
    {
        /* Los temporizadores en esta prueba se han seleccionado con
         * muchísimo delta, debido a la posibilidad de fallos al ejecutarse
         * en entornos que puedan presentar muchísima latencia el ejecutar,
         * como un servidor de CI.
         */
        Stopwatch? t = new();

        t.Start();
        Assert.ThrowsAsync<TaskCanceledException>(() => Task.Delay(100000).WithCancellation(new CancellationTokenSource(500).Token));
        t.Stop();
        Assert.True(t.ElapsedMilliseconds < 100000);

        t.Start();
        await Task.Delay(500).WithCancellation(new CancellationTokenSource(100000).Token);
        t.Stop();
        Assert.True(t.ElapsedMilliseconds < 5000);
    }

    [Test]
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

        Stopwatch? t = new();

        t.Start();
        Assert.ThrowsAsync<TaskCanceledException>(() => Get5Async(100000).WithCancellation(new CancellationTokenSource(500).Token));
        t.Stop();
        Assert.True(t.ElapsedMilliseconds < 100000);

        t.Start();
        Assert.AreEqual(5, await Get5Async(500).WithCancellation(new CancellationTokenSource(100000).Token));
        t.Stop();
        Assert.True(t.ElapsedMilliseconds < 5000);
    }

    [Test]
    public void Yield_Simple_Test()
    {
        static async Task<int> GetValueAsync()
        {
            await Task.Delay(1100);
            return 1;
        }
        Stopwatch? t = new();
        t.Start();
        int v = GetValueAsync().Yield();
        t.Stop();

        Assert.True(t.ElapsedMilliseconds >= 1000);
        Assert.AreEqual(1, v);
    }

    [Test]
    public async Task Yield_With_Timeout_Test()
    {
        static async Task<int> GetValueAsync()
        {
            await Task.Delay(2000);
            return 1;
        }
        Stopwatch? t = new();
        t.Start();
        Task<int>? task = GetValueAsync();
        _ = task.Yield(1250);
        t.Stop();
        Assert.True(t.ElapsedMilliseconds.IsBetween(500, 1500));
        Assert.AreEqual(1, await task);

        t.Restart();
        task = GetValueAsync();
        _ = task.Yield(TimeSpan.FromSeconds(1));
        t.Stop();
        Assert.True(t.ElapsedMilliseconds.IsBetween(500, 1250));
        Assert.AreEqual(1, await task);
    }

    [Test]
    public async Task Yield_With_CancellationToken_Test()
    {
        static async Task<int> GetValueAsync()
        {
            await Task.Delay(1500);
            return 1;
        }
        Stopwatch? t = new();
        t.Start();
        int v = GetValueAsync().Yield(new CancellationTokenSource(2000).Token);
        t.Stop();
        Assert.True(t.ElapsedMilliseconds.IsBetween(1250, 1750));
        Assert.AreEqual(1, v);
        t.Restart();
        Task<int>? task = GetValueAsync();
        _ = task.Yield(new CancellationTokenSource(1000).Token);
        t.Stop();
        Assert.True(t.ElapsedMilliseconds <= 1250);
        Assert.AreEqual(1, await task);
    }

    [Test]
    public async Task Yield_Full_Test()
    {
        static async Task<int> GetValueAsync()
        {
            await Task.Delay(2000);
            return 1;
        }
        Stopwatch? t = new();
        t.Start();
        Task<int>? task = GetValueAsync();
        _ = task.Yield(1250, new CancellationTokenSource(3000).Token);
        t.Stop();
        Assert.True(t.ElapsedMilliseconds.IsBetween(500, 1500));
        Assert.AreEqual(1, await task);

        t.Restart();
        task = GetValueAsync();
        _ = task.Yield(2000, new CancellationTokenSource(1000).Token);
        t.Stop();
        Assert.True(t.ElapsedMilliseconds <= 1250);
        Assert.AreEqual(1, await task);
    }
}
