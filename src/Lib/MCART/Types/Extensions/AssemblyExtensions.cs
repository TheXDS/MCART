/*
TypeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

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
using System.Reflection;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions various for <see cref="Assembly"/> objects.
/// </summary>
public static partial class AssemblyExtensions
{
    /// <summary>
    /// Gets the attribute associated with the specified assembly.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="assembly">
    /// The <see cref="Assembly"/> from which the attribute will be extracted.
    /// </param>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the data
    /// associated in the assembly declaration; or <see langword="null"/> if the
    /// specified attribute is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="assembly"/> is
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static T? GetAttribute<T>(this Assembly assembly) where T : Attribute
    {
        HasAttribute(assembly, out T? attribute);
        return attribute;
    }

    /// <summary>
    /// Gets the attribute associated with the specified assembly.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="assembly">
    /// The <see cref="Assembly"/> from which the attribute will be extracted.
    /// </param>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the data
    /// associated in the assembly declaration; or <see langword="null"/> if the
    /// specified attribute is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="assembly"/> is
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static IEnumerable<T> GetAttributes<T>(this Assembly assembly) where T : Attribute
    {
        HasAttributes(assembly, out IEnumerable<T>? attribute);
        return attribute;
    }

    /// <summary>
    /// Determines if a member possesses a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="assembly">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="attribute">
    /// Out parameter. If an attribute of type
    /// <typeparamref name="T"/> has been found, it is returned.
    /// <see langword="null"/> will be returned if the member does not possess the
    /// specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttribute<T>(this Assembly assembly, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        bool retVal = HasAttributes(assembly, out IEnumerable<T>? attributes);
        attribute = attributes.FirstOrDefault();
        return retVal;
    }

    /// <summary>
    /// Determines if a member possesses a defined attribute.
    /// </summary>
    /// <typeparam name="TAttribute">
    /// Type of the attribute to search for. Must inherit from
    /// <see cref="Attribute"/> and from <see cref="IValueAttribute{T}"/>.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of the value to return.
    /// </typeparam>
    /// <param name="assembly">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="value">
    /// Out parameter. If an attribute of type
    /// <typeparamref name="TAttribute"/> has been found, the value
    /// of the same is returned.
    /// <see langword="default"/> will be returned if the member does not possess the attribute
    /// specified.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttrValue<TAttribute, TValue>(this Assembly assembly, out TValue value)
        where TAttribute : Attribute, IValueAttribute<TValue>
    {
        bool retVal = HasAttributes(assembly, out IEnumerable<TAttribute>? attributes);
        TAttribute? a = attributes.FirstOrDefault();
        value = a is not null ? a.Value : default!;
        return retVal;
    }

    /// <summary>
    /// Determines if a member possesses a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="assembly">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttribute<T>(this Assembly assembly) where T : Attribute
    {
        return HasAttribute<T>(assembly, out _);
    }

    /// <summary>
    /// Determines if a member possesses a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="assembly">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="attribute">
    /// Out parameter. If an attribute of type
    /// <typeparamref name="T"/> has been found, it is returned.
    /// <see langword="null"/> will be returned if the member does not possess the
    /// specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttributes<T>(this Assembly assembly, out IEnumerable<T> attribute) where T : Attribute
    {
        HasAttrs_Contract(assembly);
        attribute = Attribute.GetCustomAttributes(assembly, typeof(T)).OfType<T>();
        return attribute.Any();
    }
}
