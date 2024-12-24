/*
LoggingEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Events;

/// <summary>
/// Contains event information for any event that includes logging data.
/// </summary>
public class LoggingEventArgs : ValueEventArgs<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingEventArgs"/> without specifying the subject.
    /// </summary>
    /// <param name="message">Log entry message.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="message"/> is <see langword="null"/> or
    /// an empty string.
    /// </exception>
    public LoggingEventArgs(string message) : base(message)
    {
        EmptyCheck(message);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingEventArgs"/> class.
    /// </summary>
    /// <param name="subject">
    /// Object that is related to the log entry message.</param>
    /// <param name="message">Log entry message.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="message"/> is <see langword="null"/> or
    /// an empty string.
    /// </exception>
    public LoggingEventArgs(object? subject, string message) : this(message)
    {
        Subject = subject;
    }

    /// <summary>
    /// Objeto relacionado a esta entrada de log.
    /// </summary>
    public object? Subject { get; }
}
