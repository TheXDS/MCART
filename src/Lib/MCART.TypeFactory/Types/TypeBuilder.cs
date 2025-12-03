/*
TypeBuilder.cs

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

using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// TypeBuilder that includes strongly typed information about its base
/// class.
/// </summary>
/// <typeparam name="T">
/// Base class of the type to be built.
/// </typeparam>
public class TypeBuilder<T> : ITypeBuilder<T>
{
    /// <summary>
    /// Underlying TypeBuilder of this instance.
    /// </summary>
    public TypeBuilder Builder { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeBuilder{T}"/> class
    /// by specifying the underlying <see cref="TypeBuilder"/> to associate.
    /// </summary>
    /// <param name="builder">
    /// Underlying <see cref="TypeBuilder"/> to associate.
    /// </param>
    public TypeBuilder(TypeBuilder builder) : this(builder, true)
    {
    }

    internal TypeBuilder(TypeBuilder builder, bool checkBaseType)
    {
        if (checkBaseType && builder is { BaseType: { } t } && !t.Implements<T>())
        {
            throw TypeFactoryErrors.TypeBuilderTypeMismatch<T>(builder);
        }
        Builder = builder;
    }

    /// <summary>
    /// Implicitly converts a <see cref="TypeBuilder{T}"/> into a
    /// <see cref="TypeBuilder"/>.
    /// </summary>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> to convert.
    /// </param>
    public static implicit operator TypeBuilder(TypeBuilder<T> builder) => builder.Builder;

    /// <summary>
    /// Implicitly converts a <see cref="TypeBuilder"/> into a
    /// <see cref="TypeBuilder{T}"/>.
    /// </summary>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> to convert.
    /// </param>
    public static implicit operator TypeBuilder<T>(TypeBuilder builder) => new(builder);

    /// <summary>
    /// Creates a new instance of the specified runtime type.
    /// </summary>
    /// <returns>The new instance of the specified type.</returns>
    [DebuggerStepThrough]
    [Sugar]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T New() => (T)Builder.New();
}
