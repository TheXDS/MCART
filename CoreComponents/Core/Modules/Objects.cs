/*
Objects.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using TheXDS.MCART.Attributes;

#region Configuración de ReSharper

// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART
{
    /// <summary>
    ///     Funciones de manipulación de objetos.
    /// </summary>
    public static class Objects
    {
        /// <summary>
        ///     Obtiene todos los tipos públicos que implementan al tipo especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipode objetos a obtener.
        /// </typeparam>
        /// <returns>
        ///     Una enumeración con todos los tipos que heredan o implementan el
        ///     tipo especificado.
        /// </returns>
        public static IEnumerable<Type> AllTypes<T>()
        {
            return AllTypes(typeof(T));
        }

        /// <summary>
        ///     Obtiene todos los tipos públicos que implementan al tipo especificado.
        /// </summary>
        /// <param name="t">Tipo a obtener.</param>
        /// <returns>
        ///     Una enumeración con todos los tipos que heredan o implementan el
        ///     tipo especificado.
        /// </returns>
        public static IEnumerable<Type> AllTypes(Type t)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(p => p.GetExportedTypes())
                .Where(t.IsAssignableFrom);
        }

        /// <summary>
        ///     Determina si todos los objetos son <see langword="null" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />, si todos los objetos son <see langword="null" />; de lo contrario,
        ///     <see langword="false" />.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool AreAllNull(this IEnumerable<object> x)
        {
            return x.All(p => p is null);
        }

        /// <summary>
        ///     Determina si todos los objetos son <see langword="null" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />, si todos los objetos son <see langword="null" />; de lo contrario,
        ///     <see langword="false" />.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool AreAllNull(params object[] x)
        {
            return x.AreAllNull();
        }

        /// <summary>
        ///     Enumera el valor de todas los campos que devuelvan valores de tipo
        ///     <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
        /// <param name="fields">
        ///     Colección de campos a analizar.
        /// </param>
        /// <param name="instance">
        ///     Instancia desde la cual obtener los campos.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los valores de tipo
        ///     <typeparamref name="T" /> de la instancia.
        /// </returns>
        public static IEnumerable<T> FieldsOf<T>(this IEnumerable<FieldInfo> fields, object instance)
        {
            return
                from j in fields.Where(p => p.IsPublic)
                where j.FieldType == typeof(T)
                select (T) j.GetValue(instance);
        }

        /// <summary>
        ///     Enumera el valor de todas los campos que devuelvan valores de
        ///     tipo <typeparamref name="T" /> del objeto especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
        /// <param name="instance">
        ///     Instancia desde la cual obtener los campos.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los valores de tipo
        ///     <typeparamref name="T" /> del objeto.
        /// </returns>
        public static IEnumerable<T> FieldsOf<T>(this object instance)
        {
            return FieldsOf<T>(instance.GetType().GetFields(), instance);
        }

        /// <summary>
        ///     Enumera el valor de todas los campos que devuelvan valores de
        ///     tipo <typeparamref name="T" /> del objeto especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
        /// <param name="type">
        ///     Tipo desde el cual obtener los campos.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los valores de tipo
        ///     <typeparamref name="T" /> del objeto.
        /// </returns>
        public static IEnumerable<T> FieldsOf<T>(this Type type)
        {
            return FieldsOf<T>(type.GetFields(), null);
        }

        /// <summary>
        ///     Enumera el valor de todas los campos estáticos que devuelvan
        ///     valores de tipo <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Tipo de campos a obtener.</typeparam>
        /// <param name="fields">
        ///     Colección de campos a analizar.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los valores de tipo
        ///     <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> FieldsOf<T>(this IEnumerable<FieldInfo> fields)
        {
            return FieldsOf<T>(fields, null);
        }

        /// <summary>
        ///     Busca en el <see cref="AppDomain" /> actual un tipo que contenga el
        ///     <see cref="IdentifierAttribute" /> especificado.
        /// </summary>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <returns>
        ///     Un tipo que ha sido etiquetado con el identificador especificado,
        ///     o <see langword="null" /> si ningún tipo contiene el identificador.
        /// </returns>
        [Thunk]
        public static Type FindType(string identifier)
        {
            return FindType<object>(identifier);
        }

        /// <summary>
        ///     Busca en el <see cref="AppDomain" /> actual un tipo que contenga el
        ///     <see cref="IdentifierAttribute" /> especificado.
        /// </summary>
        /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <returns>
        ///     Un tipo que ha sido etiquetado con el identificador especificado,
        ///     o <see langword="null" /> si ningún tipo contiene el identificador.
        /// </returns>
        [Thunk]
        public static Type FindType<T>(string identifier)
        {
            return FindType<T>(identifier, AppDomain.CurrentDomain);
        }

        /// <summary>
        ///     Busca en el <see cref="AppDomain" /> especificado un tipo que
        ///     contenga el <see cref="IdentifierAttribute" /> especificado.
        /// </summary>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <param name="domain">Dominio en el cual buscar.</param>
        /// <returns>
        ///     Un tipo que ha sido etiquetado con el identificador especificado,
        ///     o <see langword="null" /> si ningún tipo contiene el identificador.
        /// </returns>
        [Thunk]
        public static Type FindType(string identifier, AppDomain domain)
        {
            return FindType<object>(identifier, domain);
        }

        /// <summary>
        ///     Busca en el <see cref="AppDomain" /> especificado un tipo que
        ///     contenga el <see cref="IdentifierAttribute" /> especificado.
        /// </summary>
        /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <param name="domain">Dominio en el cual buscar.</param>
        /// <returns>
        ///     Un tipo que ha sido etiquetado con el identificador especificado,
        ///     o <see langword="null" /> si ningún tipo contiene el identificador.
        /// </returns>
        public static Type FindType<T>(string identifier, AppDomain domain)
        {
            return domain.GetTypes<T>()
                .FirstOrDefault(j => j.GetCustomAttributes(typeof(IdentifierAttribute), false)
                    .Cast<IdentifierAttribute>()
                    .Any(k => k.Value == identifier));
        }

        /// <summary>
        ///     Devuelve el atributo asociado al ensamblado especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="assembly">
        ///     <see cref="Assembly" /> del cual se extraerá el
        ///     atributo.
        /// </param>
        /// <returns>
        ///     Un atributo del tipo <typeparamref name="T" /> con los datos
        ///     asociados en la declaración del ensamblado; o <see langword="null" /> en caso
        ///     de no encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this Assembly assembly) where T : Attribute
        {
            HasAttr(assembly, out T attr);
            return attr;
        }

        /// <summary>
        ///     Devuelve el atributo asociado a la declaración del objeto
        ///     especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        ///     Un atributo del tipo <typeparamref name="T" /> con los datos
        ///     asociados en la declaración del miembro; o <see langword="null" /> en caso de
        ///     no encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this MemberInfo member) where T : Attribute
        {
            HasAttr(member, out T attr);
            return attr;
        }

        /// <summary>
        ///     Devuelve el atributo asociado a la declaración del objeto especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="type">Objeto del cual se extraerá el atributo.</param>
        /// <returns>
        ///     Un atributo del tipo <typeparamref name="T" /> con los datos
        ///     asociados en la declaración del objeto; o <see langword="null" /> en caso de no
        ///     encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this Type type) where T : Attribute
        {
            HasAttr(type, out T attr);
            return attr;
        }

        /// <summary>
        ///     Devuelve el atributo asociado a la declaración del objeto especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="obj">Objeto del cual se extraerá el atributo.</param>
        /// <returns>
        ///     Un atributo del tipo <typeparamref name="T" /> con los datos
        ///     asociados en la declaración del objeto; o <see langword="null" /> en caso de no
        ///     encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this object obj) where T : Attribute
        {
            HasAttr(obj, out T attr);
            return attr;
        }

        /// <summary>
        ///     Devuelve el atributo asociado a la declaración del valor de
        ///     enumeración.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar
        ///     <see cref="Attribute" />.
        /// </typeparam>
        /// <returns>
        ///     Un atributo del tipo <typeparamref name="T" /> con los datos
        ///     asociados en la declaración del valor de enumeración.
        /// </returns>
#if !CLSCompliance && PreferExceptions
/// <exception cref="ArgumentOutOfRangeException">
/// Se produce si el tipo de enumeración no contiene un valor definido
/// para <paramref name="enumValue"/>.
/// </exception>
        [CLSCompliant(false)]
#endif
        public static T GetAttr<T>(this Enum enumValue) where T : Attribute
        {
            HasAttr<T>(enumValue, out var retval);
            return retval;
        }

        /// <summary>
        ///     Devuelve el atributo asociado a la declaración del tipo
        ///     especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar
        ///     <see cref="Attribute" />.
        /// </typeparam>
        /// <typeparam name="TIt">
        ///     Tipo del cual se extraerá el atributo.
        /// </typeparam>
        /// <returns>
        ///     Un atributo del tipo <typeparamref name="T" /> con los datos
        ///     asociados en la declaración del tipo.
        /// </returns>
        [Thunk]
        public static T GetAttr<T, TIt>() where T : Attribute
        {
            HasAttr(typeof(TIt), out T attr);
            return attr;
        }

        /// <summary>
        ///     Devuelve el atributo asociado a la declaración del tipo
        ///     especificado, o en su defecto, del ensamblado que lo contiene.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="type">Objeto del cual se extraerá el atributo.</param>
        /// <returns>
        ///     Un atributo del tipo <typeparamref name="T" /> con los datos
        ///     asociados en la declaración del tipo; o <see langword="null" /> en caso de no
        ///     encontrarse el atributo especificado.
        /// </returns>
        public static T GetAttrAlt<T>(this Type type) where T : Attribute
        {
            return (Attribute.GetCustomAttribute(type, typeof(T))
                    ?? Attribute.GetCustomAttribute(type.Assembly, typeof(T))) as T;
        }

        /// <summary>
        ///     Obtiene una lista de tipos asignables a partir de la interfaz o clase base
        ///     especificada.
        /// </summary>
        /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
        /// <returns>
        ///     Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
        ///     <typeparamref name="T" /> dentro de <see cref="AppDomain.CurrentDomain" />.
        /// </returns>
        [Thunk]
        public static IEnumerable<Type> GetTypes<T>()
        {
            return GetTypes<T>(AppDomain.CurrentDomain);
        }

        /// <summary>
        ///     Obtiene una lista de tipos asignables a partir de la interfaz o clase base
        ///     especificada dentro del <see cref="AppDomain" /> especificado.
        /// </summary>
        /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
        /// <param name="domain">
        ///     <see cref="AppDomain" /> en el cual realizar la búsqueda.
        /// </param>
        /// <returns>
        ///     Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
        ///     <typeparamref name="T" /> dentro del <paramref name="domain" />.
        /// </returns>
        public static IEnumerable<Type> GetTypes<T>(this AppDomain domain)
        {
            return domain.GetAssemblies().SelectMany(s => s.GetTypes()
                .Where(p => typeof(T).IsAssignableFrom(p))).AsEnumerable();
        }

        /// <summary>
        ///     Obtiene una lista de tipos asignables a partir de la interfaz o clase base
        ///     especificada.
        /// </summary>
        /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
        /// <param name="instantiablesOnly">
        ///     Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
        ///     <see langword="false" /> hará que se devuelvan todos los tipos coincidientes.
        /// </param>
        /// <returns>
        ///     Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
        ///     <typeparamref name="T" /> dentro de <see cref="AppDomain.CurrentDomain" />.
        /// </returns>
        public static IEnumerable<Type> GetTypes<T>(bool instantiablesOnly)
        {
            return GetTypes<T>(AppDomain.CurrentDomain, instantiablesOnly);
        }

        /// <summary>
        ///     Obtiene una lista de tipos asignables a partir de la interfaz o clase base
        ///     especificada dentro del <see cref="AppDomain" /> especificado.
        /// </summary>
        /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
        /// <param name="domain">
        ///     <see cref="AppDomain" /> en el cual realizar la búsqueda.
        /// </param>
        /// <param name="instantiablesOnly">
        ///     Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
        ///     <see langword="false" /> hará que se devuelvan todos los tipos coincidientes.
        /// </param>
        /// <returns>
        ///     Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
        ///     <typeparamref name="T" /> dentro del <paramref name="domain" />.
        /// </returns>
        public static IEnumerable<Type> GetTypes<T>(this AppDomain domain, bool instantiablesOnly)
        {
            return domain.GetAssemblies().SelectMany(s => s.GetTypes()
                .Where(p => typeof(T).IsAssignableFrom(p))
                .Where(p => !instantiablesOnly
                            || !(p.IsInterface
                                 || p.IsAbstract
                                 || !p.GetConstructors().Any()))).AsEnumerable();
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="assembly">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        ///     Parámetro de salida. Si un atributo de tipo
        ///     <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        ///     Se devolverá <see langword="null" /> si el miembro no posee el atributo
        ///     especificado.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Assembly assembly, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(assembly, typeof(T)) as T;
            return !(attribute is null);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="assembly">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Assembly assembly) where T : Attribute
        {
            return HasAttr<T>(assembly, out _);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        ///     Parámetro de salida. Si un atributo de tipo
        ///     <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        ///     Se devolverá <see langword="null" /> si el miembro no posee el atributo
        ///     especificado.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this MemberInfo member, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(member, typeof(T)) as T;
            return !(attribute is null);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="member">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this MemberInfo member) where T : Attribute
        {
            return HasAttr<T>(member, out _);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="type">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        ///     Parámetro de salida. Si un atributo de tipo
        ///     <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        ///     Se devolverá <see langword="null" /> si el miembro no posee el atributo
        ///     especificado.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Type type, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(type, typeof(T)) as T;
            return !(attribute is null);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="type">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Type type) where T : Attribute
        {
            return HasAttr<T>(type, out _);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="obj">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        ///     Parámetro de salida. Si un atributo de tipo
        ///     <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        ///     Se devolverá <see langword="null" /> si el miembro no posee el atributo
        ///     especificado.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this object obj, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(obj.GetType(), typeof(T)) as T;
            return !(attribute is null);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="obj">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this object obj) where T : Attribute
        {
            return HasAttr<T>(obj, out _);
        }

        /// <summary>
        ///     Determina si un valor de enumeración posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="enumValue">
        ///     Valor de enumeración del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el valor de enumeración posee el atributo,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
#if !CLSCompliance && PreferExceptions
/// <exception cref="ArgumentOutOfRangeException">
/// Se produce si el tipo de enumeración no contiene un valor definido
/// para <paramref name="enumValue"/>.
/// </exception>
        [CLSCompliant(false)]
#endif
        public static bool HasAttr<T>(this Enum enumValue) where T : Attribute => HasAttr<T>(enumValue, out _);

        /// <summary>
        ///     Determina si un valor de enumeración posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="enumValue">
        ///     Valor de enumeración del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        ///     Parámetro de salida. Si un atributo de tipo
        ///     <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        ///     Se devolverá <see langword="null" /> si el miembro no posee el atributo
        ///     especificado.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el valor de enumeración posee el atributo,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
#if !CLSCompliance && PreferExceptions
/// <exception cref="ArgumentOutOfRangeException">
/// Se produce si el tipo de enumeración no contiene un valor definido
/// para <paramref name="enumValue"/>.
/// </exception>
        [CLSCompliant(false)]
#endif
        public static bool HasAttr<T>(this Enum enumValue, out T attribute) where T : Attribute
        {
            var type = enumValue.GetType();
            attribute = null;
            if (!type.IsEnumDefined(enumValue))
#if !CLSCompliance && PreferExceptions
                throw new ArgumentOutOfRangeException(nameof(enumValue));
#else
                return false;
#endif

            attribute = type.GetMember(type.GetEnumName(enumValue))[0].GetCustomAttributes(typeof(T), false)
                .FirstOrDefault() as T;
            return !(attribute is null);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="type">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        ///     Parámetro de salida. Si un atributo de tipo
        ///     <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
        ///     Se devolverá <see langword="null" /> si el miembro no posee el atributo
        ///     especificado.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttrAlt<T>(this Type type, out T attribute) where T : Attribute
        {
            attribute = (Attribute.GetCustomAttribute(type, typeof(T))
                         ?? Attribute.GetCustomAttribute(type.Assembly, typeof(T))) as T;
            return !(attribute is null);
        }

        /// <summary>
        ///     Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
        /// </typeparam>
        /// <param name="type">
        ///     Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
        ///     en caso contrario.
        /// </returns>
        public static bool HasAttrAlt<T>(this Type type) where T : Attribute
        {
            return HasAttrAlt<T>(type, out _);
        }

        /// <summary>
        ///     Determina si <paramref name="obj1" /> es la misma instancia en
        ///     <paramref name="obj2" />.
        /// </summary>
        /// <param name="obj1">Objeto a comprobar.</param>
        /// <param name="obj2">Objeto contra el cual comparar.</param>
        /// <returns>
        ///     <see langword="true" /> si la instancia de <paramref name="obj1" /> es la misma
        ///     que <paramref name="obj2" />, <see langword="false" /> en caso contrario.
        /// </returns>
        [Thunk]
        public static bool Is(this object obj1, object obj2)
        {
            return ReferenceEquals(obj1, obj2);
        }

        /// <summary>
        ///     Determina si cualquiera de los objetos es <see langword="null" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />, si cualquiera de los objetos es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool IsAnyNull(this IEnumerable<object> x)
        {
            return x.Any(p => p is null);
        }

        /// <summary>
        ///     Determina si cualquiera de los objetos es <see langword="null" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />, si cualquiera de los objetos es <see langword="null" />; de lo
        ///     contrario, <see langword="false" />.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool IsAnyNull(params object[] x)
        {
            return x.IsAnyNull();
        }

        /// <summary>
        ///     Determina si un objeto es cualquiera de los indicados.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si <paramref name="obj" /> es cualquiera de los
        ///     objetos especificados, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsEither(this object obj, params object[] objs)
        {
            return objs.Any(p => p.Is(obj));
        }

        /// <summary>
        ///     Determina si un objeto es cualquiera de los indicados.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si <paramref name="obj" /> es cualquiera de los
        ///     objetos especificados, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsEither(this object obj, IEnumerable<object> objs)
        {
            return objs.Any(p => p.Is(obj));
        }

        /// <summary>
        ///     Determina si un objeto no es ninguno de los indicados.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si <paramref name="obj" /> no es ninguno de los
        ///     objetos especificados, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsNeither(this object obj, params object[] objs)
        {
            return obj.IsNeither(objs.AsEnumerable());
        }

        /// <summary>
        ///     Determina si un objeto no es ninguno de los indicados.
        /// </summary>
        /// <returns>
        ///     <see langword="true" />si <paramref name="obj" /> no es ninguno de los
        ///     objetos especificados, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsNeither(this object obj, IEnumerable<object> objs)
        {
            return objs.All(p => !p.Is(obj));
        }

        /// <summary>
        ///     Determina si <paramref name="obj1" /> es una instancia diferente a
        ///     <paramref name="obj2" />.
        /// </summary>
        /// <param name="obj1">Objeto a comprobar.</param>
        /// <param name="obj2">Objeto contra el cual comparar.</param>
        /// <returns>
        ///     <see langword="true" /> si la instancia de <paramref name="obj1" /> no es la
        ///     misma que <paramref name="obj2" />, <see langword="false" /> en caso contrario.
        /// </returns>
        [Thunk]
        public static bool IsNot(this object obj1, object obj2)
        {
            return !ReferenceEquals(obj1, obj2);
        }

        /// <summary>
        ///     Determina si el tipo <paramref name="T" /> es de un tipo numérico
        /// </summary>
        /// <param name="T">Tipo a comprobar</param>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="T" /> es un tipo numérico; de
        ///     lo contrario, <see langword="false" />.
        /// </returns>
        [Stub]
        public static bool IsNumericType(Type T)
        {
            return new[]
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(decimal),
                typeof(float),
                typeof(double)
            }.Contains(T);
        }

        /// <summary>
        ///     Comprueba que la firma de un método sea compatible con el delegado
        ///     especificado.
        /// </summary>
        /// <param name="methodInfo">
        ///     <see cref="MethodInfo" /> a comprobar.
        /// </param>
        /// <param name="delegate">
        ///     <see cref="Type" /> del <see cref="Delegate" /> a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el método es compatible con la firma del
        ///     delegado especificado, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsSignatureCompatible(this MethodInfo methodInfo, Type @delegate)
        {
            return !(Delegate.CreateDelegate(@delegate, methodInfo, false) is null);
        }

        /// <summary>
        ///     Comprueba que la firma de un método sea compatible con el delegado
        ///     especificado.
        /// </summary>
        /// <param name="methodInfo">
        ///     <see cref="MethodInfo" /> a comprobar.
        /// </param>
        /// <typeparam name="T">
        ///     Tipo del <see cref="Delegate" /> a comprobar.
        /// </typeparam>
        /// <returns>
        ///     <see langword="true" /> si el método es compatible con la firma del
        ///     delegado especificado, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsSignatureCompatible<T>(this MethodInfo methodInfo) where T : Delegate
        {
            return IsSignatureCompatible(methodInfo, typeof(T));
        }

        /// <summary>
        ///     Devuelve una referencia circular a este mismo objeto.
        /// </summary>
        /// <returns>Este objeto.</returns>
        /// <param name="obj">Objeto.</param>
        /// <typeparam name="T">Tipo de este objeto.</typeparam>
        /// <remarks>
        ///     Esta función únicamente es únicamente útil al utilizar Visual
        ///     Basic en conjunto con la estructura <c lang="VB">With</c>.
        /// </remarks>
        [Thunk]
        public static T Itself<T>(this T obj)
        {
            return obj;
        }

        /// <summary>
        ///     Enumera el valor de todas las propiedades que devuelvan valores de
        ///     tipo <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
        /// <param name="properties">
        ///     Colección de propiedades a analizar.
        /// </param>
        /// <param name="instance">
        ///     Instancia desde la cual obtener las propiedades.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los valores de tipo
        ///     <typeparamref name="T" /> de la instancia.
        /// </returns>
        public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties, object instance)
        {
            return
                from j in properties.Where(p => p.CanRead)
                where j.PropertyType == typeof(T)
                select (T) j.GetMethod.Invoke(instance, new object[0]);
        }

        /// <summary>
        ///     Enumera el valor de todas las propiedades que devuelvan valores de
        ///     tipo <typeparamref name="T" /> del objeto especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
        /// <param name="instance">
        ///     Instancia desde la cual obtener las propiedades.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los valores de tipo
        ///     <typeparamref name="T" /> del objeto.
        /// </returns>
        public static IEnumerable<T> PropertiesOf<T>(this object instance)
        {
            return PropertiesOf<T>(instance.GetType().GetProperties(), instance);
        }

        /// <summary>
        ///     Enumera el valor de todas las propiedades estáticas que devuelvan
        ///     valores de tipo <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Tipo de propiedades a obtener.</typeparam>
        /// <param name="properties">
        ///     Colección de propiedades a analizar.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los valores de tipo
        ///     <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> PropertiesOf<T>(this IEnumerable<PropertyInfo> properties)
        {
            return PropertiesOf<T>(properties, null);
        }

        /// <summary>
        ///     Libera un objeto COM.
        /// </summary>
        /// <param name="obj">Objeto COM a liberar.</param>
        public static void ReleaseComObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
            }
            finally
            {
                // ReSharper disable once RedundantAssignment
                obj = null;
                GC.Collect();
            }
        }

        /// <summary>
        ///     Obtiene una lista de los tipos de los objetos especificados.
        /// </summary>
        /// <param name="objects">
        ///     Objetos a partir de los cuales generar la colección de tipos.
        /// </param>
        /// <returns>
        ///     Una lista compuesta por los tipos de los objetos provistos.
        /// </returns>
        public static IEnumerable<Type> ToTypes(this IEnumerable<object> objects)
        {
            return objects.Select(j => j.GetType());
        }

        /// <summary>
        ///     Obtiene una lista de los tipos de los objetos especificados.
        /// </summary>
        /// <param name="objects">
        ///     Objetos a partir de los cuales generar la colección de tipos.
        /// </param>
        /// <returns>
        ///     Una lista compuesta por los tipos de los objetos provistos.
        /// </returns>
        public static IEnumerable<Type> ToTypes(params object[] objects)
        {
            return objects.ToTypes();
        }

        /// <summary>
        ///     Determina si cualquiera de los objetos es la misma instancia que
        ///     <paramref name="obj" />.
        /// </summary>
        /// <returns>
        ///     Un enumerador con los índices de los objetos que son la misma
        ///     instancia que <paramref name="obj" />.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAre(this object obj, IEnumerable<object> collection)
        {
            var c = 0;
            foreach (var j in collection)
            {
                if (j.Is(obj)) yield return c;
                c++;
            }
        }

        /// <summary>
        ///     Determina si cualquiera de los objetos es la misma instancia que
        ///     <paramref name="obj" />.
        /// </summary>
        /// <returns>
        ///     Un enumerador con los índices de los objetos que son la misma
        ///     instancia que <paramref name="obj" />.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAre(this object obj, params object[] collection)
        {
            return obj.WhichAre(collection.AsEnumerable());
        }

        /// <summary>
        ///     Determina si cualquiera de los objetos es <see langword="null" />.
        /// </summary>
        /// <returns>
        ///     Un enumerador con los índices de los objetos que son <see langword="null" />.
        /// </returns>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAreNull(this IEnumerable<object> collection)
        {
            var c = 0;
            foreach (var j in collection)
            {
                if (j is null) yield return c;
                c++;
            }
        }

        /// <summary>
        ///     Determina si cualquiera de los objetos es <see langword="null" />.
        /// </summary>
        /// <returns>
        ///     Un enumerador con los índices de los objetos que son <see langword="null" />.
        /// </returns>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAreNull(params object[] collection)
        {
            return collection.WhichAreNull();
        }

        /// <summary>
        ///     Obtiene todos los métodos estáticos con firma compatible con el
        ///     delegado especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Delegado a utilizar como firma a comprobar.
        /// </typeparam>
        /// <param name="methods">
        ///     Colección de métodos en la cual realizar la búsqueda.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los métodos que tienen una firma
        ///     compatible con <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<MethodInfo> methods)
            where T : Delegate
        {
            foreach (var j in methods)
                if (Delegate.CreateDelegate(typeof(T), j, false) is T d)
                    yield return d;
        }

        /// <summary>
        ///     Obtiene todos los métodos de instancia con firma compatible con el
        ///     delegado especificado.
        /// </summary>
        /// <typeparam name="T">
        ///     Delegado a utilizar como firma a comprobar.
        /// </typeparam>
        /// <param name="methods">
        ///     Colección de métodos en la cual realizar la búsqueda.
        /// </param>
        /// <param name="instance">
        ///     Instancia del objeto sobre el cual construir los delegados.
        /// </param>
        /// <returns>
        ///     Una enumeración de todos los métodos que tienen una firma
        ///     compatible con <typeparamref name="T" />.
        /// </returns>
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<MethodInfo> methods, object instance)
            where T : Delegate
        {
            foreach (var j in methods)
                if (Delegate.CreateDelegate(
                    typeof(T),
                    instance,
                    j.Name,
                    false, false) is T d)
                    yield return d;
        }
    }
}