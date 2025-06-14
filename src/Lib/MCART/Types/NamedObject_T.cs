/*
NamedObject.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Structure that allows labeling any object.
/// </summary>
/// <typeparam name="T">Object type.</typeparam>
/// <param name="name">Object's label.</param>
/// <param name="value">Object to label.</param>
public readonly struct NamedObject<T>(string name, T value) : INameable
{
    /// <summary>
    /// Initializes a new instance of the structure <see cref="NamedObject{T}" />
    /// by setting a value along with an auto-generated label based on <see cref="object.ToString()" />.
    /// </summary>
    /// <param name="value">Object to label.</param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public NamedObject(T value) : this(Infer(value), value)
    {
    }

    /// <summary>
    /// Initialies a new instance of the structure <see cref="NamedObject{T}" />
    /// </summary>
    /// <param name="value">Object to label.</param>
    /// <param name="name">Object's label.</param>
    [Obsolete(AttributeErrorMessages.NamedObjectDeprecatedCtor)]
    public NamedObject(T value, string name) : this(name, value)
    {
    }

    /// <summary>
    /// Object's value.
    /// </summary>
    public T Value { get; } = value;

    /// <summary>
    /// Object's label.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Implicitly converts a <typeparamref name="T" /> into a <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="obj">Object to convert.</param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static implicit operator NamedObject<T>(T obj)
    {
        return new(obj);
    }

    /// <summary>
    /// Implicitly converts a <see cref="NamedObject{T}" /> into a <typeparamref name="T" />.
    /// </summary>
    /// <param name="namedObj">Object to convert.</param>
    public static implicit operator T(NamedObject<T> namedObj)
    {
        return namedObj.Value;
    }

    /// <summary>
    /// Implicitly converts a <see cref="NamedObject{T}" /> into a <see cref="string" />.
    /// </summary>
    /// <param name="namedObj">Object to convert.</param>
    public static implicit operator string(NamedObject<T> namedObj)
    {
        return namedObj.Name;
    }

    /// <summary>
    /// Implicitly converts a <see cref="NamedObject{T}" /> into a <see cref="KeyValuePair{TKey,TValue}" />.
    /// </summary>
    /// <param name="namedObj">Object to convert.</param>
    public static implicit operator KeyValuePair<string, T>(NamedObject<T> namedObj)
    {
        return new(namedObj.Name, namedObj.Value);
    }

    /// <summary>
    /// Implicitly converts a <see cref="KeyValuePair{TKey,TValue}" /> into a <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="keyValuePair">Object to convert.</param>
    public static implicit operator NamedObject<T>(KeyValuePair<string, T> keyValuePair)
    {
        return new(keyValuePair.Key, keyValuePair.Value);
    }

    /// <summary>
    /// Implicitly converts a <see cref="ValueTuple{T1, T2}" /> into a <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="tuple">Object to convert.</param>
    public static implicit operator NamedObject<T>(ValueTuple<string, T> tuple)
    {
        return new(tuple.Item1, tuple.Item2);
    }

    /// <summary>
    /// Implicitly converts a <see cref="ValueTuple{T1, T2}" /> into a <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="tuple">Object to convert.</param>
    public static implicit operator NamedObject<T>(ValueTuple<T, string> tuple)
    {
        return new(tuple.Item2, tuple.Item1);
    }

    /// <summary>
    /// Implicitly converts a <see cref="ValueTuple{T1, T2}" /> into a <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    public static implicit operator ValueTuple<string, T>(NamedObject<T> value)
    {
        return new(value.Name, value.Value);
    }

    /// <summary>
    /// Implicitly converts a <see cref="ValueTuple{T1, T2}" /> into a <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="value">Object to convert.</param>
    public static implicit operator ValueTuple<T, string>(NamedObject<T> value)
    {
        return new(value.Value, value.Name);
    }

    /// <summary>
    /// Compares equality between two instances of <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="left">
    /// Object to compare.
    /// </param>
    /// <param name="right">
    /// Object to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both instances are considered equal, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator ==(NamedObject<T> left, NamedObject<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks if two instances of <see cref="NamedObject{T}"/> are considered different.
    /// </summary>
    /// <param name="left">
    /// Object to compare.
    /// </param>
    /// <param name="right">
    /// Object to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both instances are considered different, <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator !=(NamedObject<T> left, NamedObject<T> right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Infers the label for an object.
    /// </summary>
    /// <param name="obj">Object to infer a label for.</param>
    /// <returns>
    /// The inferred name of the object or <see cref="object.ToString()"/> if no appropriate label can be inferred.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static string Infer(T obj)
    {
        return obj switch
        {
            INameable n => n.Name,
            MemberInfo m => Extensions.MemberInfoExtensions.NameOf(m),
            Enum e => EnumExtensions.NameOf(e),
            null => throw new ArgumentNullException(nameof(obj)),
            _ => obj.GetAttribute<NameAttribute>()?.Value ?? obj.ToString() ?? obj.GetType().NameOf()
        };
    }

    /// <summary>
    /// Compares equality between this <see cref="NamedObject{T}"/> and another object.
    /// </summary>
    /// <param name="obj">
    /// Object to compare against this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both instances are considered equal, <see langword="false"/> otherwise.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            NamedObject<T> other => Equals(other.Value),
            T other => Value?.Equals(other) ?? false,
            null => Value is null,
            _ => false
        };
    }

    /// <summary>
    /// Gets the hash code for this instance.
    /// </summary>
    /// <returns>The hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? base.GetHashCode();
    }
}
