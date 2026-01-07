/*
ProgressionEventArgs.cs

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

using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Events;

/// <summary>
/// Includes event informatino for any event that includes data on the progress
/// of an operation.
/// </summary>
public class ProgressionEventArgs : ValueEventArgs<double>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressionEventArgs"/>
    /// class.
    /// </summary>
    /// <param name="progress">
    /// Progress value. It must be either a <see cref="double" /> value between
    /// <c>0.0</c> and <c>1.0</c>, <see cref="double.NaN" />,
    /// <see cref="double.PositiveInfinity" /> or
    /// <see cref="double.NegativeInfinity" />.
    /// </param>
    /// <param name="helpText">
    /// Optional parameter. Description of the current state of progress.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="progress" /> is a real number that is not
    /// between <c>0.0</c> and <c>1.0</c>.
    /// </exception>
    public ProgressionEventArgs(double progress, string? helpText) : base(progress)
    {
        RangeCheck(progress, 0, 1);        
        HelpText = helpText;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgressionEventArgs"/>
    /// class.
    /// </summary>
    /// <param name="progress">
    /// Progress value. It must be either a <see cref="double" /> value between
    /// <c>0.0</c> and <c>1.0</c>, <see cref="double.NaN" />,
    /// <see cref="double.PositiveInfinity" /> or
    /// <see cref="double.NegativeInfinity" />.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="progress" /> is a real number that is not
    /// between <c>0.0</c> and <c>1.0</c>.
    /// </exception>
    public ProgressionEventArgs(double progress) : this(progress, null)
    {
    }

    /// <summary>
    /// Gets the description for the current state of progress.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> with a message that describes the current state
    /// of progress.
    /// </returns>
    public string? HelpText { get; }
}
