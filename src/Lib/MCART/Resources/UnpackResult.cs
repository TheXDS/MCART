/*
IAsyncUnpacker.cs

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

using System.Diagnostics.CodeAnalysis;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Contains the result of the async operation to unpack a resource.
/// </summary>
/// <param name="Success"> Indicates whether the unpack operation succeeded. </param>
/// <param name="Value"> Gets the result of the async unpack operation. </param>
/// <typeparam name="T">Type of resource to be unpacked.</typeparam>
public sealed record UnpackResult<T>([property: MemberNotNullWhen(true, "Value")] bool Success, T? Value)
{
    /// <summary>
    /// Implicitly converts an object of type <see cref="UnpackResult{T}"/>
    /// into a value tuple of <see cref="bool"/> and <typeparamref name="T"/>.
    /// </summary>
    /// <param name="result">Object converted to a tuple.</param>
    public static implicit operator (bool success, T? value)(UnpackResult<T> result) => (result.Success, result.Value);
}