/*
MemberInfoExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones varias para objetos <see cref="MemberInfo" />.
/// </summary>
public static partial class MemberInfoExtensions
{
    /// <summary>
    /// Obtiene un nombre personalizado para un miembro.
    /// </summary>
    /// <param name="member">
    /// <see cref="MemberInfo" /> del cual obtener el nombre.
    /// </param>
    /// <returns>
    /// Un nombre amigable para <paramref name="member" />, o el nombre
    /// definido para <paramref name="member" /> si no se ha definido
    /// un nombre amigable por medio del atributo
    /// <see cref="NameAttribute"/>.
    /// </returns>
    public static string NameOf(this MemberInfo member)
    {
        return member.GetAttribute<NameAttribute>()?.Value ?? member.Name;
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="member">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
    {
        return HasAttribute<T>(member, out _);
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="member">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <param name="attribute">
    /// Parámetro de salida. Si un atributo de tipo
    /// <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
    /// Se devolverá <see langword="null" /> si el miembro no posee el atributo
    /// especificado.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttributes<T>(this MemberInfo member, out IEnumerable<T> attribute) where T : Attribute
    {
        HasAttrs_Contract(member);
        attribute = Attribute.GetCustomAttributes(member, typeof(T)).OfType<T>();
        return attribute.Any();
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="member">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <param name="attribute">
    /// Parámetro de salida. Si un atributo de tipo
    /// <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
    /// Se devolverá <see langword="null" /> si el miembro no posee el atributo
    /// especificado.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttribute<T>(this MemberInfo member, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        bool retVal = HasAttributes(member, out IEnumerable<T>? attributes);
        attribute = attributes.FirstOrDefault();
        return retVal;
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="TValue">
    /// Tipo de valor a devolver.
    /// </typeparam>
    /// <typeparam name="TAttribute">
    /// Tipo de atributo a buscar. Debe heredar de
    /// <see cref="Attribute"/> y de <see cref="IValueAttribute{T}"/>.
    /// </typeparam>
    /// <param name="member">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <param name="value">
    /// Parámetro de salida. Si un atributo de tipo
    /// <typeparamref name="TAttribute" /> ha sido encontrado, el valor
    /// del mismo es devuelto.
    /// Se devolverá <see langword="default" /> si el miembro no posee el atributo
    /// especificado.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
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
    /// Devuelve el atributo asociado a la declaración del objeto
    /// especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="member">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del miembro; o <see langword="null" /> en caso de
    /// no encontrarse el atributo especificado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="member"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static T? GetAttribute<T>(this MemberInfo member) where T : Attribute
    {
        HasAttribute(member, out T? attribute);
        return attribute;
    }

    /// <summary>
    /// Devuelve el atributo asociado al ensamblado especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="member">
    /// <see cref="MemberInfo" /> del cual se extraerá el
    /// atributo.
    /// </param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del ensamblado; o <see langword="null" /> en caso
    /// de no encontrarse el atributo especificado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="member"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static IEnumerable<T> GetAttributes<T>(this MemberInfo member) where T : Attribute
    {
        HasAttributes(member, out IEnumerable<T> attributes);
        return attributes;
    }
}
