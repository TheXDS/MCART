/*
ConstantLoader.cs

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

using System.Reflection.Emit;

namespace TheXDS.MCART.Types.Extensions.ConstantLoaders;

/// <summary>
/// Abstract class that defines an object capable of loading a constant
/// value into the MSIL instruction sequence.
/// </summary>
/// <typeparam name="T">Type of constant to load.</typeparam>
public abstract class ConstantLoader<T> : IConstantLoader, IEquatable<IConstantLoader>
{
    /// <inheritdoc/>
    public bool CanLoadConstant(object? value) => value is T;

    /// <summary>
    /// Gets a reference to the constant type that this instance can
    /// load into the MSIL instruction sequence.
    /// </summary>
    public Type ConstantType => typeof(T);

    /// <summary>
    /// Loads a constant value into the MSIL instruction sequence.
    /// </summary>
    /// <param name="il">
    /// Code generator to use.
    /// </param>
    /// <param name="value">
    /// Constant value to load. Must be of type <typeparamref name="T"/>.
    /// </param>
    public void Emit(ILGenerator il, object? value)
    {
        Emit(il, (T)value!);
    }

    /// <summary>
    /// Loads a <typeparamref name="T"/> constant value into the MSIL
    /// instruction sequence.
    /// </summary>
    /// <param name="il">The IL generator to use.</param>
    /// <param name="value">
    /// The constant value to load into the instruction sequence.
    /// </param>
    public abstract void Emit(ILGenerator il, T value);

    /// <summary>
    /// Determines whether this instance and another
    /// <see cref="IConstantLoader"/> are equal based on the constant
    /// type that they can both load.
    /// </summary>
    /// <param name="other">
    /// An instance of <see cref="IConstantLoader"/> to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both <see cref="IConstantLoader"/>
    /// instances can load the same type of values,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public bool Equals(IConstantLoader? other)
    {
        return other is { ConstantType: { } o } && ConstantType.Equals(o);
    }
}
