/*
NamedObject.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Estructura que permite asignarle una etiqueta a cualquier objeto.
/// </summary>
/// <typeparam name="T">Tipo de objeto.</typeparam>
/// <param name="value">Objeto a etiquetar.</param>
/// <param name="name">Etiqueta del objeto.</param>
public readonly struct NamedObject<T>(T value, string name) : INameable
{
    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="NamedObject{T}" /> estableciendo un valor junto a una
    /// etiqueta auto-generada a partir de
    /// <see cref="object.ToString()" />.
    /// </summary>
    /// <param name="value">Objeto a etiquetar.</param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public NamedObject(T value) : this(value, Infer(value))
    {
    }

    /// <summary>
    /// Valor del objeto.
    /// </summary>
    public T Value { get; } = value;

    /// <summary>
    /// Etiqueta del objeto.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Convierte implícitamente un <typeparamref name="T" /> en un
    /// <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="obj">Objeto a convertir.</param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static implicit operator NamedObject<T>(T obj)
    {
        return new(obj);
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="NamedObject{T}" /> en un
    /// <typeparamref name="T" />.
    /// </summary>
    /// <param name="namedObj">Objeto a convertir.</param>
    public static implicit operator T(NamedObject<T> namedObj)
    {
        return namedObj.Value;
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="NamedObject{T}" /> en un
    /// <see cref="string" />.
    /// </summary>
    /// <param name="namedObj">Objeto a convertir.</param>
    public static implicit operator string(NamedObject<T> namedObj)
    {
        return namedObj.Name;
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="NamedObject{T}" /> en un
    /// <see cref="KeyValuePair{TKey,TValue}" />.
    /// </summary>
    /// <param name="namedObj">Objeto a convertir.</param>
    public static implicit operator KeyValuePair<string, T>(NamedObject<T> namedObj)
    {
        return new(namedObj.Name, namedObj.Value);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="KeyValuePair{TKey,TValue}" /> en un
    /// <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="keyValuePair">Objeto a convertir.</param>
    public static implicit operator NamedObject<T>(KeyValuePair<string, T> keyValuePair)
    {
        return new(keyValuePair.Value, keyValuePair.Key);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="ValueTuple{T1, T2}" /> en un
    /// <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="tuple">Objeto a convertir.</param>
    public static implicit operator NamedObject<T>(ValueTuple<string, T> tuple)
    {
        return new(tuple.Item2, tuple.Item1);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="ValueTuple{T1, T2}" /> en un
    /// <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="tuple">Objeto a convertir.</param>
    public static implicit operator NamedObject<T>(ValueTuple<T, string> tuple)
    {
        return new(tuple.Item1, tuple.Item2);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="ValueTuple{T1, T2}" /> en un
    /// <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    public static implicit operator ValueTuple<string, T>(NamedObject<T> value)
    {
        return new(value.Name, value.Value);
    }

    /// <summary>
    /// Convierte implícitamente un
    /// <see cref="ValueTuple{T1, T2}" /> en un
    /// <see cref="NamedObject{T}" />.
    /// </summary>
    /// <param name="value">Objeto a convertir.</param>
    public static implicit operator ValueTuple<T, string>(NamedObject<T> value)
    {
        return new(value.Value, value.Name);
    }

    /// <summary>
    /// Compara la igualdad entre dos instancias de
    /// <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <param name="left">
    /// Objeto a comparar.
    /// </param>
    /// <param name="right">
    /// Objeto contra el cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si ambas instancias son consideradas
    /// iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool operator ==(NamedObject<T> left, NamedObject<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Comprueba si ambas instancias de <see cref="NamedObject{T}"/>
    /// son consideradas distintas.
    /// </summary>
    /// <param name="left">
    /// Objeto a comparar.
    /// </param>
    /// <param name="right">
    /// Objeto contra el cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si ambas instancias son consideradas
    /// distintas, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool operator !=(NamedObject<T> left, NamedObject<T> right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Infiere la etiqueta de un objeto.
    /// </summary>
    /// <param name="obj">Objeto para el cual inferir una etiqueta.</param>
    /// <returns>
    /// El nombre inferido del objeto, o
    /// <see cref="object.ToString()" /> de no poderse inferir una
    /// etiqueta adecuada.
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
    /// Compara la igualdad entre este <see cref="NamedObject{T}"/> y
    /// otro objeto.
    /// </summary>
    /// <param name="obj">
    /// Objeto contra el cual comparar esta instancia.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si ambas instancias son consideradas
    /// iguales, <see langword="false"/> en caso contrario.
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
    /// Obtiene el código Hash para esta instancia.
    /// </summary>
    /// <returns>El código Hash para esta instancia.</returns>
    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? base.GetHashCode();
    }
}
