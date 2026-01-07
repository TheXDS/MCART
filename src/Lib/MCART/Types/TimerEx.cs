/*
TimerEx.cs

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

using System.Timers;
using TheXDS.MCART.Types.Base;
using S = System.Timers;

namespace TheXDS.MCART.Types;

/// <summary>
/// Extension of the <see cref="TimerEx" /> class. Provides all previously
/// available functionality, and includes some useful extensions.
/// </summary>
public class TimerEx : S.Timer, IDisposableEx
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimerEx" />.
    /// </summary>
    public TimerEx()
    {
        Elapsed += Tmr_Elapsed;
        Disposed += TimerEx_Disposed;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimerEx" /> and 
    /// sets the <see cref="S.Timer.Interval" /> property
    /// to the specified number of milliseconds.
    /// </summary>
    /// <param name="interval">
    /// Time, in milliseconds, between events. This value must be greater
    /// than zero and less than <see cref="F:System.Int32.MaxValue" />.
    /// </param>
    public TimerEx(double interval) : base(interval) { }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="TimerEx"/>
    /// should raise the <see cref="S.Timer.Elapsed"/> event.
    /// </summary>
    public new bool Enabled
    {
        get => base.Enabled;
        set
        {
            StartTime = value ? DateTime.Now : null;
            base.Enabled = value;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this object has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Indicates the start time of this <see cref="TimerEx"/>.
    /// </summary>
    public DateTime? StartTime { get; private set; }

    /// <summary>
    /// Indicates the amount of time remaining before meeting the 
    /// interval set in 
    /// <see cref="S.Timer.Interval"/>.
    /// </summary>
    public TimeSpan? TimeLeft
    {
        get
        {
            if (StartTime is not null) return TimeSpan.FromMilliseconds(Interval) - (DateTime.Now - StartTime);
            return null;
        }
    }

    /// <summary>
    /// Starts generating the 
    /// <see cref="S.Timer.Elapsed"/> event by setting 
    /// <see cref="Enabled"/> to <see langword="true"/>.
    /// </summary>
    public new void Start()
    {
        StartTime = DateTime.Now;
        base.Start();
    }

    /// <summary>
    /// Stops generating the <see cref="S.Timer.Elapsed"/>
    /// event by setting <see cref="Enabled"/> to <see langword="false"/>.
    /// </summary>
    public new void Stop()
    {
        StartTime = null;
        base.Stop();
    }

    /// <summary>
    /// Resets this <see cref="TimerEx"/>.
    /// </summary>
    public void Reset()
    {
        Stop();
        Start();
    }

    private void Tmr_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (AutoReset) StartTime = DateTime.Now;
    }

    private void TimerEx_Disposed(object? sender, EventArgs e)
    {
        IsDisposed = true;
    }
}
