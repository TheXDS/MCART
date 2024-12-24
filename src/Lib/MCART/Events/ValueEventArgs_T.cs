/*
ValueEventArgs_T.cs

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
/// Includes event information for any event that includes some value
/// associated with it, as well as being a base class for any such event args.
/// </summary>
/// <typeparam name="T">
/// Type of value.
/// </typeparam>
/// <param name="value">Valor asociado al evento generado.</param>
public class ValueEventArgs<T>(T value) : EventArgs
{
    /// <summary>
    /// Implicitly converts a value ot type <typeparamref name="T"/> into a
    /// value of type <see cref="ValueEventArgs{T}"/>.
    /// </summary>
    /// <param name="value">Value to be converted.</param>
    public static implicit operator ValueEventArgs<T>(T value) => new(value);

    /// <summary>
    /// Gets the value associated with an event.
    /// </summary>
    /// <returns>
    /// A value of type <typeparamref name="T" /> associated with the event.
    /// </returns>
    public T Value { get; } = value;
}
