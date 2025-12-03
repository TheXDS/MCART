/*
ITypeBuilder.cs

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

using System.Reflection.Emit;

namespace TheXDS.MCART.Types;

/// <summary>
/// Defines members to be implemented by a descriptor of TypeBuilder that
/// includes strongly typed information about the base type to inherit.
/// </summary>
/// <typeparam name="T">
/// Base type to inherit by the TypeBuilder.
/// </typeparam>
public interface ITypeBuilder<out T>
{
    /// <summary>
    /// Underlying TypeBuilder used to create the new type.
    /// </summary>
    TypeBuilder Builder { get; }

    /// <summary>
    /// Reference to the strongly typed base type for this TypeBuilder.
    /// </summary>
    Type SpecificBaseType => typeof(T);

    /// <summary>
    /// Reference to the actual base type of the TypeBuilder.
    /// </summary>
    Type ActualBaseType => Builder.BaseType ?? SpecificBaseType;

    /// <summary>
    /// Creates a new instance of the specified type at runtime.
    /// </summary>
    /// <returns>The new instance of the specified type.</returns>
    T New();
}
