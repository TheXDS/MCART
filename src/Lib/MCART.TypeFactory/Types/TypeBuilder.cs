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
/// <see cref="TypeBuilder"/> que incluye información fuertemente tipeada
/// sobre su clase base.
/// </summary>
/// <typeparam name="T">
/// Clase base del tipo a construir.
/// </typeparam>
public class TypeBuilder<T> : ITypeBuilder<T>
{
    /// <summary>
    /// <see cref="TypeBuilder"/> subyacente de esta instancia.
    /// </summary>
    public TypeBuilder Builder { get; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    ///  <see cref="TypeBuilder{T}"/> especificando al
    ///  <see cref="TypeBuilder"/> subyacente a asociar.
    /// </summary>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> subyacente a asociar.
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
    /// Convierte implícitamente un <see cref="TypeBuilder{T}"/> en un
    /// <see cref="TypeBuilder"/>.
    /// </summary>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> a convertir.
    /// </param>
    public static implicit operator TypeBuilder(TypeBuilder<T> builder) => builder.Builder;

    /// <summary>
    /// Convierte implícitamente un <see cref="TypeBuilder"/> en un
    /// <see cref="TypeBuilder{T}"/>.
    /// </summary>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> a convertir.
    /// </param>
    public static implicit operator TypeBuilder<T>(TypeBuilder builder) => new(builder);

    /// <summary>
    /// Inicializa una nueva instancia del tipo en runtime especificado.
    /// </summary>
    /// <returns>La nueva instancia del tipo especificado.</returns>
    [DebuggerStepThrough]
    [Sugar]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T New() => (T)Builder.New();
}
