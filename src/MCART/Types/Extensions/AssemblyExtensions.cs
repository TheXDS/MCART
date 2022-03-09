/*
TypeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;

/// <summary>
/// Extensiones varias para objetos <see cref="Assembly" />.
/// </summary>
public static partial class AssemblyExtensions
{
    /// <summary>
    /// Devuelve el atributo asociado al ensamblado especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="assembly">
    /// <see cref="Assembly" /> del cual se extraerá el
    /// atributo.
    /// </param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del ensamblado; o <see langword="null" /> en caso
    /// de no encontrarse el atributo especificado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="assembly"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static T? GetAttr<T>(this Assembly assembly) where T : Attribute
    {
        HasAttr<T>(assembly, out T? attr);
        return attr;
    }

    /// <summary>
    /// Devuelve el atributo asociado al ensamblado especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="assembly">
    /// <see cref="Assembly" /> del cual se extraerá el
    /// atributo.
    /// </param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del ensamblado; o <see langword="null" /> en caso
    /// de no encontrarse el atributo especificado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="assembly"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static IEnumerable<T> GetAttrs<T>(this Assembly assembly) where T : Attribute
    {
        HasAttrs(assembly, out IEnumerable<T> attr);
        return attr;
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="assembly">
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
    public static bool HasAttr<T>(this Assembly assembly, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        bool retVal = HasAttrs(assembly, out IEnumerable<T>? attrs);
        attribute = attrs.FirstOrDefault();
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
    /// <param name="assembly">
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
    public static bool HasAttrValue<TAttribute, TValue>(this Assembly assembly, out TValue value)
        where TAttribute : Attribute, IValueAttribute<TValue>
    {
        bool retVal = HasAttrs<TAttribute>(assembly, out IEnumerable<TAttribute>? attrs);
        TAttribute? a = attrs.FirstOrDefault();
        value = a is not null ? a.Value : default!;
        return retVal;
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="assembly">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttr<T>(this Assembly assembly) where T : Attribute
    {
        return HasAttr<T>(assembly, out _);
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="assembly">
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
    public static bool HasAttrs<T>(this Assembly assembly, out IEnumerable<T> attribute) where T : Attribute
    {
        HasAttrs_Contract(assembly);
        attribute = Attribute.GetCustomAttributes(assembly, typeof(T)).OfType<T>();
        return attribute.Any();
    }
}
