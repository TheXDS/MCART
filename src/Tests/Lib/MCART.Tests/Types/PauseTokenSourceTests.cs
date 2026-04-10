/*
PauseTokenSourceTests.cs

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
using System.Threading.Tasks;
using NUnit.Framework;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Tests.Types;

public class PauseTokenSourceTests
{
    [Test]
    public void IsPaused_Property_Test()
    {
        var source = new PauseTokenSource();
        Assert.That(source.IsPaused, Is.False);
        
        source.IsPaused = true;
        Assert.That(source.IsPaused, Is.True);
        
        source.IsPaused = false;
        Assert.That(source.IsPaused, Is.False);
    }

    [Test]
    public void Token_Property_Test()
    {
        var source = new PauseTokenSource();
        var token = source.Token;
        Assert.That(token, Is.Not.Null);
        Assert.That(token.IsPaused, Is.False);
    }

    [Test]
    public async Task PauseToken_WaitWhilePausedAsync_Test()
    {
        var source = new PauseTokenSource();
        var token = source.Token;
        
        // Test that when not paused, WaitWhilePausedAsync returns completed task
        var task1 = token.WaitWhilePausedAsync();
        Assert.That(task1.IsCompleted, Is.True);
        Assert.That(task1, Is.EqualTo(Task.CompletedTask));
        
        // Test that when paused, WaitWhilePausedAsync returns an uncompleted task
        source.IsPaused = true;
        var task2 = token.WaitWhilePausedAsync();
        Assert.That(task2.IsCompleted, Is.False);
        
        // When unpausing, the task should complete
        source.IsPaused = false;
        await task2; // Should complete now
        Assert.That(task2.IsCompleted, Is.True);
    }
}
