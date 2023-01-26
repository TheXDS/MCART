/*
ObjectExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Misc.Internals;
using static TheXDS.MCART.Types.Extensions.EnumerableExtensions;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la clase <see cref="object" />.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Determina si un objeto es cualquiera de los indicados.
    /// </summary>
    /// <returns>
    /// <see langword="true" />si <paramref name="obj" /> es cualquiera de los
    /// objetos especificados, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="obj">Objeto a comprobar.</param>
    /// <param name="objects">Lista de objetos a comparar.</param>
    public static bool IsEither(this object obj, params object[] objects)
    {
        return objects.Any(p => p?.Is(obj) ?? obj is null);
    }

    /// <summary>
    /// Determina si un objeto es cualquiera de los indicados.
    /// </summary>
    /// <returns>
    /// <see langword="true" />si <paramref name="obj" /> es cualquiera de los
    /// objetos especificados, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="obj">Objeto a comprobar.</param>
    /// <param name="objects">Lista de objetos a comparar.</param>
    public static bool IsEither(this object obj, IEnumerable objects)
    {
        return objects.ToGeneric().Any(obj.Is);
    }

    /// <summary>
    /// Determina si un objeto no es ninguno de los indicados.
    /// </summary>
    /// <returns>
    /// <see langword="true" />si <paramref name="obj" /> no es ninguno de los
    /// objetos especificados, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="obj">Objeto a comprobar.</param>
    /// <param name="objects">Lista de objetos a comparar.</param>
    public static bool IsNeither(this object obj, params object[] objects)
    {
        return obj.IsNeither(objects.AsEnumerable());
    }

    /// <summary>
    /// Determina si un objeto no es ninguno de los indicados.
    /// </summary>
    /// <returns>
    /// <see langword="true" />si <paramref name="obj" /> no es ninguno de los
    /// objetos especificados, <see langword="false" /> en caso contrario.
    /// </returns>
    /// <param name="obj">Objeto a comprobar.</param>
    /// <param name="objects">Lista de objetos a comparar.</param>
    public static bool IsNeither(this object obj, IEnumerable objects)
    {
        return objects.ToGeneric().All(p => !p.Is(obj));
    }



    /// <summary>
    /// Determina si <paramref name="obj1" /> es la misma instancia en
    /// <paramref name="obj2" />.
    /// </summary>
    /// <param name="obj1">Objeto a comprobar.</param>
    /// <param name="obj2">Objeto contra el cual comparar.</param>
    /// <returns>
    /// <see langword="true" /> si la instancia de <paramref name="obj1" /> es la misma
    /// que <paramref name="obj2" />, <see langword="false" /> en caso contrario.
    /// </returns>
    [Sugar]
    public static bool Is(this object? obj1, object? obj2)
    {
        return ReferenceEquals(obj1, obj2);
    }

    /// <summary>
    /// Devuelve el atributo asociado a la declaración del objeto especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="obj">Objeto del cual se extraerá el atributo.</param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del objeto; o <see langword="null" /> en caso de no
    /// encontrarse el atributo especificado.
    /// </returns>
    [Sugar]
    public static T? GetAttr<T>(this object obj) where T : Attribute
    {
        HasAttr<T>(obj, out T? attr);
        return attr;
    }

    /// <summary>
    /// Determina si <paramref name="obj1" /> es una instancia diferente a
    /// <paramref name="obj2" />.
    /// </summary>
    /// <param name="obj1">Objeto a comprobar.</param>
    /// <param name="obj2">Objeto contra el cual comparar.</param>
    /// <returns>
    /// <see langword="true" /> si la instancia de <paramref name="obj1" /> no es la
    /// misma que <paramref name="obj2" />, <see langword="false" /> en caso contrario.
    /// </returns>
    [Sugar]
    public static bool IsNot(this object? obj1, object? obj2)
    {
        return !ReferenceEquals(obj1, obj2);
    }

    /// <summary>
    /// Determina si cualquiera de los objetos es la misma instancia que
    /// <paramref name="obj" />.
    /// </summary>
    /// <returns>
    /// Un enumerador con los índices de los objetos que son la misma
    /// instancia que <paramref name="obj" />.
    /// </returns>
    /// <param name="obj">Objeto a comprobar.</param>
    /// <param name="collection">Colección de objetos a comprobar.</param>
    public static IEnumerable<int> WhichAre(this object obj, IEnumerable<object> collection)
    {
        int c = 0;
        foreach (object? j in collection)
        {
            if (j.Is(obj)) yield return c;
            c++;
        }
    }

    /// <summary>
    /// Determina si cualquiera de los objetos es la misma instancia que
    /// <paramref name="obj" />.
    /// </summary>
    /// <returns>
    /// Un enumerador con los índices de los objetos que son la misma
    /// instancia que <paramref name="obj" />.
    /// </returns>
    /// <param name="obj">Objeto a comprobar.</param>
    /// <param name="collection">Colección de objetos a comprobar.</param>
    public static IEnumerable<int> WhichAre(this object obj, params object[] collection)
    {
        return obj.WhichAre(collection.AsEnumerable());
    }

    /// <summary>
    /// Enumera el valor de todas las propiedades que devuelvan valores de
    /// tipo <typeparamref name="T" /> del objeto especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
    /// <param name="instance">
    /// Instancia desde la cual obtener las propiedades.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los valores de tipo
    /// <typeparamref name="T" /> del objeto.
    /// </returns>
    [Sugar]
    public static IEnumerable<T> PropertiesOf<T>(this object instance)
    {
        return instance.GetType().GetProperties().PropertiesOf<T>(instance);
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="obj">
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
    public static bool HasAttr<T>(this object obj, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        return obj switch
        {
            null => throw new ArgumentNullException(nameof(obj)),
            Assembly a => a.HasAttr(out attribute),
            MemberInfo m => m.HasAttr(out attribute),
            Enum e => e.HasAttr(out attribute),
            _ => HasAttrs<T>(obj.GetType(), out IEnumerable<T>? attributes) & (attribute = attributes?.FirstOrDefault()) is not null
        };
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
    /// <param name="obj">
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
    public static bool HasAttrValue<TAttribute, TValue>(this object obj, [MaybeNullWhen(false)] out TValue value)
        where TAttribute : Attribute, IValueAttribute<TValue>
    {
        switch (obj)
        {
            case Assembly a:
                return a.HasAttrValue<TAttribute, TValue>(out value);
            case MemberInfo m:
                return m.HasAttrValue<TAttribute, TValue>(out value);
            case Enum e:
                return e.HasAttrValue<TAttribute, TValue>(out value);
            default:
                bool retVal = HasAttrs<TAttribute>(obj, out IEnumerable<TAttribute>? attributes);
                value = attributes?.FirstOrDefault() is { Value: { } v } ? v : default!;
                return retVal;
        }
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="obj">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttr<T>(this object obj) where T : Attribute
    {
        return HasAttr<T>(obj, out _);
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="obj">
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
    public static bool HasAttrs<T>(this object obj, [NotNullWhen(true)] out IEnumerable<T>? attribute) where T : Attribute
    {
        switch (obj)
        {
            case Assembly a:
                return a.HasAttrs(out attribute);
            case MemberInfo m:
                return m.HasAttrs(out attribute);
            case Enum e:
                return e.HasAttrs(out attribute);
            default:
                attribute = Attribute.GetCustomAttributes(obj.GetType(), typeof(T)).OfType<T>();
                return attribute.Any();
        }
    }

    /// <summary>
    /// Devuelve el atributo asociado al ensamblado especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="member">
    /// <see cref="object" /> del cual se extraerá el
    /// atributo.
    /// </param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del ensamblado; o <see langword="null" /> en caso
    /// de no encontrarse el atributo especificado.
    /// </returns>
    [Sugar]
    public static IEnumerable<T>? GetAttrs<T>(this object member) where T : Attribute
    {
        HasAttrs(member, out IEnumerable<T>? attr);
        return attr;
    }

    /// <summary>
    /// Enumera el valor de todas los campos que devuelvan valores de
    /// tipo <typeparamref name="T" /> del objeto especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
    /// <param name="instance">
    /// Instancia desde la cual obtener los campos.
    /// </param>
    /// <returns>
    /// Una enumeración de todos los valores de tipo
    /// <typeparamref name="T" /> del objeto.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="instance"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FieldsOf<T>(this object instance)
    {
        NullCheck(instance, nameof(instance));
        return ReflectionHelpers.FieldsOf<T>(instance.GetType().GetFields(), instance);
    }

#if DynamicLoading
    /// <summary>
    /// Obtiene el nombre de un objeto.
    /// </summary>
    /// <param name="obj">
    /// Objeto del cual obtener el nombre.
    /// </param>
    /// <returns>
    /// El nombre del objeto.
    /// </returns>
    public static string NameOf(this object obj)
    {
        if (obj is null) throw new ArgumentNullException(nameof(obj));
        return (obj as INameable)?.Name
            ?? (string?)ScanNameMethod(obj.GetType())?.Invoke(null, new[] { obj })
            ?? Types.Extensions.MemberInfoExtensions.NameOf(obj.GetType());
    }

    private static MethodInfo? ScanNameMethod(Type fromType)
    {
        foreach (var j in SafeGetExportedTypes(typeof(Objects).Assembly))
        {
            foreach (var k in j.GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                if (k.Name != "NameOf" || k.ReturnType != typeof(string)) continue;
                var pars = k.GetParameters();
                if (pars.Length != 1 || !pars[0].ParameterType.IsAssignableFrom(fromType)) continue;
                return k;
            }
        }
        return null;
    }
#endif
}
