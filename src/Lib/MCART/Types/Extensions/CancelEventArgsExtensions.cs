/*
CancelEventArgsExtensions.cs

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

using System.ComponentModel;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for all elements of type
/// <see cref="CancelEventArgs"/>
/// </summary>
public static class CancelEventArgsExtensions
{
    /// <summary>
    /// Executes an event handler with these arguments, and
    /// continues with another call if the event has not been canceled.
    /// </summary>
    /// <typeparam name="TCancelEventArgs">
    /// Type that derives from <see cref="CancelEventArgs"/> for which
    /// this method is an extension.
    /// </typeparam>
    /// <param name="evtArgs">
    /// Arguments to use in the call of the cancelable event.
    /// </param>
    /// <param name="handler">
    /// Cancelable event to execute.
    /// </param>
    /// <param name="sender">
    /// Object that is the source of the event to generate.
    /// </param>
    /// <param name="continuation">
    /// Continuation call in case the event has not been canceled.
    /// </param>
    public static void Attempt<TCancelEventArgs>(this TCancelEventArgs evtArgs, EventHandler<TCancelEventArgs>? handler, object? sender, Action<TCancelEventArgs> continuation) where TCancelEventArgs : CancelEventArgs
    {
        handler?.Invoke(sender, evtArgs);
        if (!evtArgs.Cancel)
        {
            continuation(evtArgs);
        }
    }
}
