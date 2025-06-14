/*
PauseTokenSource.cs

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

namespace TheXDS.MCART.Types;

/// <summary>
/// A class that allows for the creation and management of <see cref="PauseToken"/>
/// objects, used for cooperatively pausing task execution.
/// </summary>
public class PauseTokenSource
{
    private volatile TaskCompletionSource<bool>? m_paused;

    /// <summary>
    /// Gets or sets a value indicating whether this pause source is in a paused state.
    /// </summary>
    public bool IsPaused
    {
        get => m_paused != null;
        set
        {
            if (value)
            {
                Interlocked.CompareExchange(ref m_paused, new TaskCompletionSource<bool>(), null);
            }
            else
            {
                while (true)
                {
                    var tcs = m_paused;
                    if (tcs is null) return;
                    if (Interlocked.CompareExchange(ref m_paused, null, tcs) == tcs)
                    {
                        tcs.SetResult(true);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets a new <see cref="PauseToken"/> that will observe the state of this instance.
    /// </summary>
    public PauseToken Token => new(this);

    internal Task WaitWhilePausedAsync()
    {
        var cur = m_paused;
        return cur != null ? cur.Task : Completed;
    }

    private static readonly Task<bool> Completed = Task.FromResult(true);
}
