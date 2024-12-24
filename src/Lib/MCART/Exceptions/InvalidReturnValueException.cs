/*
InvalidReturnValueException.cs

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

using TheXDS.MCART.Resources.Strings;

using static TheXDS.MCART.Resources.Strings.Errors;

namespace TheXDS.MCART.Exceptions;

/// <summary>
/// Exception that is thrown when it's detected that a function returned an
/// incorrect value without generating an exception itself.
/// </summary>
[Serializable]
public class InvalidReturnValueException : Exception
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    public InvalidReturnValueException() : base(InvalidReturnValue)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    /// <param name="call">
    /// <see cref="Delegate" /> whose result produced the exception.
    /// </param>
    public InvalidReturnValueException(Delegate call) : this(call.Method.Name)
    {
        OffendingFunction = call;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    /// <param name="methodName">
    /// Name of the method whose result produced this exception.
    /// </param>
    public InvalidReturnValueException(string methodName) : base(string.Format(InvalidFuncReturnValue, methodName))
    {
        OffendingFunctionName = methodName;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    /// <param name="call">
    /// <see cref="Delegate" /> whose result produced the exception.
    /// </param>
    /// <param name="returnValue">
    /// Invalid value returned by <paramref name="call" />.
    /// </param>
    public InvalidReturnValueException(Delegate call, object? returnValue) : this(call.Method.Name, returnValue)
    {
        OffendingFunction = call;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    /// <param name="methodName">
    /// Name of the method whose result produced this exception.
    /// </param>
    /// <param name="returnValue">
    /// Invalid value returned by the method.
    /// </param>
    public InvalidReturnValueException(string methodName, object? returnValue) : this(methodName, returnValue, null)
    {
        OffendingFunctionName = methodName;
        OffendingReturnValue = returnValue;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    /// <param name="message">
    /// Message that describes the exception.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public InvalidReturnValueException(string message, Exception? inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    /// <param name="call">
    /// <see cref="Delegate" /> whose result produced the exception.
    /// </param>
    /// <param name="returnValue">
    /// Invalid value returned by <paramref name="call" />.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public InvalidReturnValueException(Delegate call, object? returnValue, Exception? inner) : this(call.Method.Name, returnValue, inner)
    {
        OffendingFunction = call;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="InvalidReturnValueException" /> class.
    /// </summary>
    /// <param name="methodName">
    /// Name of the method whose result produced this exception.
    /// </param>
    /// <param name="returnValue">
    /// Invalid value returned by the method.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> that is the cause of this exception.
    /// </param>
    public InvalidReturnValueException(string methodName, object? returnValue, Exception? inner) : base(string.Format(InvalidFuncXReturnValue, methodName, returnValue ?? Common.Null), inner)
    {
        OffendingFunctionName = methodName;
        OffendingReturnValue = returnValue;
    }

    /// <summary>
    /// Gets a reference to the delegate that is the cause of the exception.
    /// </summary>
    public Delegate? OffendingFunction { get; }

    /// <summary>
    /// Gets the name of the method that is the cause of the exception.
    /// </summary>
    public string? OffendingFunctionName { get; }

    /// <summary>
    /// Gets the value returned by the method that is the cause of the exception.
    /// </summary>
    public object? OffendingReturnValue { get; }
}
