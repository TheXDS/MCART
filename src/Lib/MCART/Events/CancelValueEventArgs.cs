/*
CancelValueEventArgs.cs

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

namespace TheXDS.MCART.Events;

/// <summary>
/// Includes event information for any class that includes cancellable events
/// with related event values, as well as being a balse class for any
/// <see cref="EventArgs"/> that can support setting a cancellation flag with a
/// relared event value.
/// </summary>
/// <typeparam name="T">
/// Typeof value associated with the event being generated.
/// </typeparam>
/// <param name="value">
/// Value associated with thte event being generated.
/// </param>
public class CancelValueEventArgs<T>(T value) : ValueEventArgs<T>(value)
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation should be
    /// cancelled or not.
    /// </summary>
    public bool Cancel { get; set; }
}
