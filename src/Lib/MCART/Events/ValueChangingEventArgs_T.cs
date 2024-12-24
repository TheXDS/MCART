/*
ValueChangingEventArgs_T.cs

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

using System.ComponentModel;

namespace TheXDS.MCART.Events;

/// <summary>
/// Includes event information for any event where the value of an element
/// changes.
/// </summary>
/// <typeparam name="T">
/// Type of value that will be changed.
/// </typeparam>
/// <param name="oldValue">
/// Old value.
/// </param>
/// <param name="newValue">
/// New value.
/// </param>
public class ValueChangingEventArgs<T>(T oldValue, T newValue) : CancelEventArgs
{
    /// <summary>
    /// Implicitly converts an object of type
    /// <see cref="ValueChangingEventArgs{T}" /> to anobject of type
    /// <see cref="ValueEventArgs{T}" />.
    /// </summary>
    /// <param name="fromValue">Object to be converted.</param>
    /// <returns>
    /// A new <see cref="ValueChangingEventArgs{T}" /> with the same event
    /// information as the original <see cref="ValueChangingEventArgs{T}" />
    /// value.
    /// </returns>
    public static explicit operator ValueEventArgs<T>(ValueChangingEventArgs<T> fromValue)
    {
        return new(fromValue.NewValue);
    }

    /// <summary>
    /// Gets the original value.
    /// </summary>
    /// <returns>
    /// The original value of type <typeparamref name="T" /> associated with
    /// the value change.
    /// </returns>
    public T OldValue { get; } = oldValue;

    /// <summary>
    /// Gets the new value.
    /// </summary>
    /// <returns>
    /// The new value of type <typeparamref name="T" /> associated with the
    /// value change.
    /// </returns>
    public T NewValue { get; } = newValue;
}
