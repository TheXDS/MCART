/*
MemberInfoExtensions.cs

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
using System.Reflection;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Includes various extensions for the <see cref="MemberInfo" /> class.
/// </summary>
public static partial class MemberInfoExtensions
{
    /// <summary>
    /// Gets a friendly name for a member.
    /// </summary>
    /// <param name="member">
    /// The <see cref="MemberInfo"/> from which to get the name.
    /// </param>
    /// <returns>
    /// A friendly name for <paramref name="member"/>, or the name
    /// defined for <paramref name="member"/> if a friendly name has not been
    /// defined using the <see cref="NameAttribute"/> attribute.
    /// </returns>
    public static string NameOf(this MemberInfo member)
    {
        return member.GetAttribute<NameAttribute>()?.Value ?? member.Name;
    }

    /// <summary>
    /// Determines if a member has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="member">
    /// The member from which to extract the attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member has the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
    {
        return HasAttribute<T>(member, out _);
    }

    /// <summary>
    /// Determines if a member has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="member">
    /// The member from which to extract the attribute.
    /// </param>
    /// <param name="attribute">
    /// Output parameter. If an attribute of type <typeparamref name="T"/> has
    /// been found, it is returned. It will be returned as <see langword="null"/> if
    /// the member does not have the specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member has the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttributes<T>(this MemberInfo member, out IEnumerable<T> attribute) where T : Attribute
    {
        HasAttrs_Contract(member);
        attribute = Attribute.GetCustomAttributes(member, typeof(T)).OfType<T>();
        return attribute.Any();
    }

    /// <summary>
    /// Determines if a member has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="member">
    /// The member from which to extract the attribute.
    /// </param>
    /// <param name="attribute">
    /// Output parameter. If an attribute of type <typeparamref name="T"/> has
    /// been found, it is returned. It will be returned as <see langword="null"/> if
    /// the member does not have the specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member has the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttribute<T>(this MemberInfo member, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        bool retVal = HasAttributes(member, out IEnumerable<T>? attributes);
        attribute = attributes.FirstOrDefault();
        return retVal;
    }

    /// <summary>
    /// Determines if a member has a defined attribute.
    /// </summary>
    /// <typeparam name="TAttribute">
    /// Type of attribute to return. Must inherit from
    /// <see cref="Attribute"/> and from <see cref="IValueAttribute{T}"/>
    /// </typeparam>
    /// <typeparam name="TValue">
    /// Type of value to return.
    /// </typeparam>
    /// <param name="member">
    /// The member from which to extract the attribute.
    /// </param>
    /// <param name="value">
    /// Output parameter. If an attribute of type <typeparamref name="TAttribute"/> has
    /// been found, the value of the same is returned.
    /// It will be returned as <see langword="default"/> if the member does not have the
    /// specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member has the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttrValue<TAttribute, TValue>(this MemberInfo member, out TValue value)
        where TAttribute : Attribute, IValueAttribute<TValue>
    {
        bool retVal = HasAttributes(member, out IEnumerable<TAttribute>? attributes);
        TAttribute? a = attributes.FirstOrDefault();
        value = a is not null ? a.Value : default!;
        return retVal;
    }

    /// <summary>
    /// Returns the attribute associated with the declaration of the
    /// specified object.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="member">
    /// The member from which to extract the attribute.
    /// </param>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the data
    /// associated in the declaration of the member; or <see langword="null"/> if the
    /// specified attribute is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="member"/> is
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static T? GetAttribute<T>(this MemberInfo member) where T : Attribute
    {
        return Attribute.GetCustomAttribute(member, typeof(T)) as T;
    }

    /// <summary>
    /// Returns the attribute associated with the assembly specified.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="member">
    /// <see cref="MemberInfo"/> from which to extract the
    /// attribute.
    /// </param>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the data
    /// associated in the declaration of the assembly; or <see langword="null"/> if the
    /// specified attribute is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="member"/> is
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static IEnumerable<T> GetAttributes<T>(this MemberInfo member) where T : Attribute
    {
        return Attribute.GetCustomAttributes(member, typeof(T)).Cast<T>();
    }
}
