/*
TamperException.cs

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

using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Exception that is thrown by a supported method under one of the following
/// circumstances:
/// <list type="bullet">
/// <item>
///     <description>
///         Return values being altered unexpectedly.
///     </description> 
/// </item>
/// <item>
///     <description>
///         Internal object state corruption.
///     </description> 
/// </item>
/// <item>
///     <description>
///         Function return value outside the expected/possible range.
///     </description>
/// </item>
/// <item>
///     <description>
///         Memory corruption not captured by the CLR.
///     </description>
/// </item>
/// <item>
///     <description>
///         External modification of internal values in a type.
///     </description>
/// </item>
/// <item>
///     <description>
///         Unexpected access to private methods in a type.
///     </description>
/// </item>
/// <item>
///     <description>
///         Dereference of <see langword="null"/> in places known to not have a
///         possible reference to <see langword="null"/>.
///     </description>
/// </item>
/// <item>
///     <description>
///         Escapes in parameter validation.
///     </description>
/// </item>
/// </list>
/// </summary>
[Serializable]
public class TamperException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TamperException" /> class.
    /// </summary>
    public TamperException() : base(Errors.TamperDetected)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TamperException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    public TamperException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TamperException" /> class.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public TamperException(Exception inner) : this(Errors.TamperDetected, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TamperException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public TamperException(string message, Exception inner) : base(message, inner)
    {
    }
}
