/*
ExceptionEventArgs.cs

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

namespace TheXDS.MCART.Events;

/// <summary>
/// Includes event information for any event that includes exception
/// information.
/// </summary>
public class ExceptionEventArgs : ValueEventArgs<Exception?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionEventArgs"/>
    /// class without specifying the exception that has been produced.
    /// </summary>
    public ExceptionEventArgs() : base(null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionEventArgs"/>
    /// class specifying the exception that has been produced.
    /// </summary>
    /// <param name="ex">
    /// <see cref="Exception" /> to associate with this instance.
    /// </param>
    public ExceptionEventArgs(Exception? ex) : base(ex)
    {
    }

    /// <summary>
    /// Implicitly converts an object of type <see cref="Exception"/> into an
    /// object of type <see cref="ExceptionEventArgs"/>.
    /// </summary>
    /// <param name="ex">Value to be converted.</param>
    public static implicit operator ExceptionEventArgs(Exception ex) => new(ex);
}
