/*
Objects.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART
{
    /// <summary>
    /// Funciones de manipulación de objetos.
    /// </summary>
    public static class Objects
    {
        /// <summary>
        /// Enumera los tipos asignables a partir de <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        /// Un enumerador con los tipos que pueden ser asignados a partir de
        /// <paramref name="source"/>.
        /// </returns>
        public static IEnumerable<Type> Assignables(this Type source, IEnumerable<Type> types) => types.Where(p => p.IsAssignableFrom(source));
        /// <summary>
        /// Enumera los tipos asignables a partir de <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        /// Un enumerador con los tipos que pueden ser asignados a partir de
        /// <paramref name="source"/>.
        /// </returns>
        public static IEnumerable<Type> Assignables(this Type source, params Type[] types) => source.Assignables(types.AsEnumerable());
        /// <summary>
        /// Comprueba si alguno de los tipos especificados es asignable a partir
        /// del tipo <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        /// <c>true</c> si el tipo <paramref name="source"/> puede ser asignado 
        /// a uno de los tipos especificados, <c>false</c> en caso contrario.
        /// </returns>
        public static bool IsAnyAssignable(this Type source, IEnumerable<Type> types) => types.Any(p => p.IsAssignableFrom(source));
        /// <summary>
        /// Comprueba si alguno de los tipos especificados es asignable a partir
        /// del tipo <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        /// <c>true</c> si el tipo <paramref name="source"/> puede ser asignado 
        /// a uno de los tipos especificados, <c>false</c> en caso contrario.
        /// </returns>
        public static bool IsAnyAssignable(this Type source, params Type[] types) => source.IsAnyAssignable(types.AsEnumerable());
        /// <summary>
        /// Comprueba si todos los tipos son asignables a partir del tipo
        /// <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns><c>true</c> si todos los tipos son asignables a partir de
        /// <paramref name="source"/>, <c>false</c> en caso contrario.</returns>
        public static bool AreAllAssignable(this Type source, IEnumerable<Type> types) => types.All(p => p.IsAssignableFrom(source));
        /// <summary>
        /// Comprueba si todos los tipos son asignables a partir del tipo
        /// <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns><c>true</c> si todos los tipos son asignables a partir de
        /// <paramref name="source"/>, <c>false</c> en caso contrario.</returns>
        public static bool AreAllAssignable(this Type source, params Type[] types) => source.AreAllAssignable(types.AsEnumerable());
        /// <summary>
        /// Determina si cualquiera de los objetos es <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si cualquiera de los objetos es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool IsAnyNull(this IEnumerable<object> x) => x.Any(p => p is null);
        /// <summary>
        /// Determina si cualquiera de los objetos es <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si cualquiera de los objetos es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool IsAnyNull(params object[] x) => x.IsAnyNull();
        /// <summary>
        /// Determina si cualquiera de los objetos es <c>null</c>.
        /// </summary>
        /// <returns>
        /// Un enumerador con los índices de los objetos que son <c>null</c>.
        /// </returns>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAreNull(this IEnumerable<object> collection)
        {
            int c = 0;
            foreach (object j in collection)
            {
                if (j is null) yield return c++;
            }
        }
        /// <summary>
        /// Determina si cualquiera de los objetos es <c>null</c>.
        /// </summary>
        /// <returns>
        /// Un enumerador con los índices de los objetos que son <c>null</c>.
        /// </returns>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAreNull(params object[] collection) => collection.WhichAreNull();
        /// <summary>
        /// Determina si todos los objetos son <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si todos los objetos son <c>null</c>; de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool AreAllNull(this IEnumerable<object> x)=> x.All(p => p is null);
        /// <summary>
        /// Determina si todos los objetos son <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si todos los objetos son <c>null</c>; de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool AreAllNull(params object[] x) => x.AreAllNull();
        /// <summary>
        /// Devuelve una referencia circular a este mismo objeto.
        /// </summary>
        /// <returns>Este objeto.</returns>
        /// <param name="obj">Objeto.</param>
        /// <typeparam name="T">Tipo de este objeto.</typeparam>
        /// <remarks>
        /// Esta función únicamente es útil al utilizar Visual Basic en conjunto
        /// con la estructura <c lang="VB">With</c></remarks>
        [Thunk] public static T Itself<T>(this T obj) => obj;
        /// <summary>
        /// Determina si <paramref name="obj1"/> es la misma instancia en
        /// <paramref name="obj2"/>.
        /// </summary>
        /// <param name="obj1">Objeto a comprobar.</param>
        /// <param name="obj2">Objeto contra el cual comparar.</param>
        /// <returns>
        /// <c>true</c> si la instancia de <paramref name="obj1"/> es la misma
        /// que <paramref name="obj2"/>, <c>false</c> en caso contrario.
        /// </returns>
        [Thunk] public static bool Is(this object obj1, object obj2) => ReferenceEquals(obj1, obj2);
        /// <summary>
        /// Determina si <paramref name="obj1"/> es una instancia diferente a
        /// <paramref name="obj2"/>.
        /// </summary>
        /// <param name="obj1">Objeto a comprobar.</param>
        /// <param name="obj2">Objeto contra el cual comparar.</param>
        /// <returns>
        /// <c>true</c> si la instancia de <paramref name="obj1"/> no es la
        /// misma que <paramref name="obj2"/>, <c>false</c> en caso contrario.
        /// </returns>
        [Thunk] public static bool IsNot(this object obj1, object obj2) => !ReferenceEquals(obj1, obj2);
        /// <summary>
		/// Determina si un objeto es cualquiera de los indicados.
		/// </summary>
		/// <returns><c>true</c>si <paramref name="obj"/> es cualquiera de los
		/// objetos especificados, <c>false</c> en caso contrario.</returns>
		/// <param name="obj">Objeto a comprobar.</param>
		/// <param name="objs">Lista de objetos a comparar.</param>
		public static bool IsEither(this object obj, params object[] objs) => objs.Any(p => p.Is(obj));
        /// <summary>
        /// Determina si un objeto es cualquiera de los indicados.
        /// </summary>
        /// <returns><c>true</c>si <paramref name="obj"/> es cualquiera de los
        /// objetos especificados, <c>false</c> en caso contrario.</returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsEither(this object obj, IEnumerable<object> objs) => objs.Any(p => p.Is(obj));
        /// <summary>
        /// Determina si cualquiera de los objetos es la misma instancia que
        /// <paramref name="obj"/>.
        /// </summary>
        /// <returns>
        /// Un enumerador con los índices de los objetos que son la misma
        /// instancia que <paramref name="obj"/>.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAre(this object obj, IEnumerable<object> collection)
        {
            int c = 0;
            foreach (object j in collection)
            {
                if (j.Is(obj)) yield return c++;
            }
        }
        /// <summary>
        /// Determina si cualquiera de los objetos es la misma instancia que
        /// <paramref name="obj"/>.
        /// </summary>
        /// <returns>
        /// Un enumerador con los índices de los objetos que son la misma
        /// instancia que <paramref name="obj"/>.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="collection">Colección de objetos a comprobar.</param>
        public static IEnumerable<int> WhichAre(this object obj, params object[] collection) => obj.WhichAre(collection.AsEnumerable());
        /// <summary>
        /// Determina si un objeto no es ninguno de los indicados.
        /// </summary>
        /// <returns><c>true</c>si <paramref name="obj"/> no es ninguno de los
        /// objetos especificados, <c>false</c> en caso contrario.</returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsNeither(this object obj, params object[] objs) => obj.IsNeither(objs.AsEnumerable());
        /// <summary>
        /// Determina si un objeto no es ninguno de los indicados.
        /// </summary>
        /// <returns><c>true</c>si <paramref name="obj"/> no es ninguno de los
        /// objetos especificados, <c>false</c> en caso contrario.</returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsNeither(this object obj, IEnumerable<object> objs) => objs.All(p => !p.Is(obj));
        /// <summary>
        /// Obtiene una lista de tipos asignables a partir de la interfaz 
        /// especificada.
        /// </summary>
        /// <typeparam name="T">Interfaz a buscar.</typeparam>
        /// <returns>
        /// Una lista de tipos de las clases que implementan a la interfaz
        /// <typeparamref name="T"/> dentro del 
        /// <see cref="AppDomain.CurrentDomain"/>.
        /// </returns>
        [Thunk] public static IEnumerable<Type> GetTypes<T>() => GetTypes<T>(AppDomain.CurrentDomain);
        /// <summary>
        /// Obtiene una lista de tipos asignables a partir de la interfaz 
        /// especificada dentro del <see cref="AppDomain"/> especificado.
        /// </summary>
        /// <typeparam name="T">Interfaz a buscar.</typeparam>
        /// <param name="Domain">
        /// <see cref="AppDomain"/> en el cual realizar la búsqueda.
        /// </param>
        /// <returns>
        /// Una lista de tipos de las clases que implementan a la interfaz 
        /// <typeparamref name="T"/> dentro del <paramref name="Domain"/>.
        /// </returns>
        public static IEnumerable<Type> GetTypes<T>(this AppDomain Domain)
        {
            return Domain.GetAssemblies().SelectMany(s => s.GetTypes().Where(
                p => typeof(T).IsAssignableFrom(p))).AsEnumerable();
        }
        /// <summary>
        /// Obtiene una lista de los tipos de los objetos especificados.
        /// </summary>
        /// <param name="objects">
        /// Objetos a partir de los cuales generar la colección de tipos.
        /// </param>
        /// <returns>
        /// Una lista compuesta por los tipos de los objetos provistos.
        /// </returns>
        public static IEnumerable<Type> ToTypes(this IEnumerable<object> objects)
        {
            foreach (object j in objects) yield return j.GetType();
        }
        /// <summary>
        /// Obtiene una lista de los tipos de los objetos especificados.
        /// </summary>
        /// <param name="objects">
        /// Objetos a partir de los cuales generar la colección de tipos.
        /// </param>
        /// <returns>
        /// Una lista compuesta por los tipos de los objetos provistos.
        /// </returns>
        public static IEnumerable<Type> ToTypes(params object[] objects) => objects.ToTypes();
        /// <summary>
        /// Inicializa una nueva instancia del tipo en runtime especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar.</param>
        [DebuggerStepThrough] [Thunk] public static object New(this Type j) => j.New<object>(new object[] { });
        /// <summary>
        /// Inicializa una nueva instancia del tipo dinámico especificado,
        /// devolviéndola como un <typeparamref name="T"/>.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar. Debe ser, heredar o implementar 
        /// el tipo especificado en <typeparamref name="T"/></param>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        [DebuggerStepThrough] [Thunk] public static T New<T>(this Type j) => j.New<T>(new object[] { });
        /// <summary>
        /// Inicializa una nueva instancia de un objeto con un constructor que
        /// acepte los argumentos provistos.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar.</param>
        /// <param name="parameters">Parámetros a pasar al constructor. Se buscará 
        /// un constructor compatible para poder crear la instancia.</param>
        [DebuggerStepThrough] [Thunk] public static object New(this Type j, params object[] parameters) => j.New<object>(parameters);
        /// <summary>
        /// Inicializa una nueva instancia de un objeto con un constructor que
        /// acepte los argumentos provistos.
        /// </summary>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        /// <param name="j">Tipo a instanciar. Debe ser, heredar o implementar 
        /// el tipo especificado en <typeparamref name="T"/>.</param>
        /// <param name="parameters">Parámetros a pasar al constructor. Se buscará 
        /// un constructor compatible para poder crear la instancia.</param>
        /// <returns>Una nueva instancia del tipo especificado.</returns>
        /// <exception cref="TypeLoadException">
        /// Se produce si no es posible instanciar una clase del tipo
        /// solicitado.
        /// </exception>
        [DebuggerStepThrough]
        public static T New<T>(this Type j, params object[] parameters)
        {
            if (j.IsAbstract || j.IsInterface) throw new TypeLoadException();
            return (T)j.GetConstructor(parameters.ToTypes().ToArray())?.Invoke(parameters);
        }
        /// <summary>
        /// Libera un objeto COM.
        /// </summary>
        /// <param name="obj">Objeto COM a liberar.</param>
        public static void ReleaseCOMObject(object obj)
        {
            try { Marshal.ReleaseComObject(obj); }
            finally
            {
                obj = null;
                GC.Collect();
            }
        }
        /// <summary>
        /// Devuelve el atributo asociado al ensamblado especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="assembly">
        /// <see cref="Assembly"/> del cual se extraerá el
        /// atributo.
        /// </param>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos
        /// asociados en la declaración del ensamblado; o <c>null</c> en caso
        /// de no encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this Assembly assembly) where T : Attribute
        {
            HasAttr(assembly, out T attr);
            return attr;
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="assembly">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="T"/> ha sido encontrado, el mismo es devuelto.
        /// Se devolverá <c>null</c> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Assembly assembly, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(assembly, typeof(T)) as T;
            return !(attribute is null);
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="assembly">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Assembly assembly) where T : Attribute => HasAttr<T>(assembly, out _);
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del objeto
        /// especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos
        /// asociados en la declaración del miembro; o <c>null</c> en caso de
        /// no encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this MemberInfo member) where T : Attribute
        {
            HasAttr(member, out T attr);
            return attr;
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="T"/> ha sido encontrado, el mismo es devuelto.
        /// Se devolverá <c>null</c> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this MemberInfo member, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(member, typeof(T)) as T;
            return !(attribute is null);
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="member">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this MemberInfo member) where T : Attribute => HasAttr<T>(member, out _);
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del objeto especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="type">Objeto del cual se extraerá el atributo.</param>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos
        /// asociados en la declaración del objeto; o <c>null</c> en caso de no
        /// encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this Type type) where T : Attribute
        {
            HasAttr(type, out T attr);
            return attr;
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="type">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="T"/> ha sido encontrado, el mismo es devuelto.
        /// Se devolverá <c>null</c> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Type type, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(type, typeof(T)) as T;
            return !(attribute is null);
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="type">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Type type) where T : Attribute => HasAttr<T>(type, out _);
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del objeto especificado.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="obj">Objeto del cual se extraerá el atributo.</param>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos
        /// asociados en la declaración del objeto; o <c>null</c> en caso de no
        /// encontrarse el atributo especificado.
        /// </returns>
        [Thunk]
        public static T GetAttr<T>(this object obj) where T : Attribute
        {
            HasAttr(obj, out T attr);
            return attr;
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="obj">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="T"/> ha sido encontrado, el mismo es devuelto.
        /// Se devolverá <c>null</c> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this object obj, out T attribute) where T : Attribute
        {
            attribute = Attribute.GetCustomAttribute(obj.GetType(), typeof(T)) as T;
            return !(attribute is null);
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="obj">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this object obj) where T : Attribute => HasAttr<T>(obj, out _);
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del valor de
        /// enumeración.
        /// </summary>
        /// <typeparam name="T">Tipo de atributo a devolver. Debe heredar 
        /// <see cref="Attribute"/>.</typeparam>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos 
        /// asociados en la declaración del valor de enumeración.
        /// </returns>
        public static T GetAttr<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            if (!type.IsEnumDefined(enumValue))
#if PreferExceptions
                /* -= NOTA =-
                 * si se define el símbolo PreferExceptions, esta función ya no
                 * cumplirá con el estándar CLS, debido a que un valor de tipo
                 * Enum no debería tener un contrato que compruebe el rango de
                 * valores. Al crear aplicaciones que utilicen esta función, se
                 * debe tomar en cuenta este comportamiento.
                 */
                throw new ArgumentOutOfRangeException(nameof(enumValue));
#else
                return null;
#endif
            return enumValue.GetType().GetMember(type.GetEnumName(enumValue))[0].GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }
        /// <summary>
        /// Determina si un valor de enumeración posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="enumValue">
        /// Valor de enumeración del cual se extraerá el atributo.
        /// </param>
        /// <returns><c>true</c> si el valor de enumeración posee el atributo,
        /// <c>false</c> en caso contrario.
        /// </returns>
        public static bool HasAttr<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            if (!type.IsEnumDefined(enumValue))
#if PreferExceptions
                /* -= NOTA =-
                 * si se define el símbolo PreferExceptions, esta función ya no
                 * cumplirá con el estándar CLS, debido a que un valor de tipo
                 * Enum no debería tener un contrato que compruebe el rango de
                 * valores. Al crear aplicaciones que utilicen esta función, se
                 * debe tomar en cuenta este comportamiento.
                 */
                throw new ArgumentOutOfRangeException(nameof(enumValue));
#else
                return false;
#endif
            return type.GetMember(type.GetEnumName(enumValue))[0].GetCustomAttributes(typeof(T), false).Any();
        }
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del tipo
        /// especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de atributo a devolver. Debe heredar 
        /// <see cref="Attribute"/>.</typeparam>
        /// <typeparam name="It">
        /// Tipo del cual se extraerá el atributo.
        /// </typeparam>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos 
        /// asociados en la declaración del tipo.
        /// </returns>
        [Thunk]
        public static T GetAttr<T, It>() where T : Attribute
        {
            HasAttr(typeof(It), out T attr);
            return attr;
        }
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del tipo
        /// especificado, o en su defecto, del ensamblado que lo contiene.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="type">Objeto del cual se extraerá el atributo.</param>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos
        /// asociados en la declaración del tipo; o <c>null</c> en caso de no
        /// encontrarse el atributo especificado.
        /// </returns>
        public static T GetAttrAlt<T>(this Type type) where T : Attribute
        {
            return (Attribute.GetCustomAttribute(type, typeof(T))
                ?? Attribute.GetCustomAttribute(type.Assembly, typeof(T))) as T;
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="type">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <param name="attribute">
        /// Parámetro de salida. Si un atributo de tipo
        /// <typeparamref name="T"/> ha sido encontrado, el mismo es devuelto.
        /// Se devolverá <c>null</c> si el miembro no posee el atributo
        /// especificado.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttrAlt<T>(this Type type, out T attribute) where T : Attribute
        {
            attribute = (Attribute.GetCustomAttribute(type, typeof(T))
                ?? Attribute.GetCustomAttribute(type.Assembly, typeof(T))) as T;
            return !(attribute is null);
        }
        /// <summary>
        /// Determina si un miembro posee un atributo definido.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.
        /// </typeparam>
        /// <param name="type">
        /// Miembro del cual se extraerá el atributo.
        /// </param>
        /// <returns><c>true</c> si el miembro posee el atributo, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public static bool HasAttrAlt<T>(this Type type) where T : Attribute => HasAttrAlt<T>(type, out _);
        /// <summary>
        /// Determina si el tipo <paramref name="T"/> es de un tipo numérico
        /// </summary>
        /// <param name="T">Tipo a comprobar</param>
        /// <returns><c>true</c> si <paramref name="T"/> es un tipo numérico; de
        /// lo contrario, <c>false</c>.</returns>
        [Stub]
        [Obsolete]
        public static bool IsNumericType(Type T)
        {
            return new[]{
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
        /// Determina si el tipo <typeparamref name="T"/> es de un tipo numérico
        /// </summary>
        /// <typeparam name="T">Tipo a comprobar</typeparam>
        /// <returns><c>true</c> si <typeparamref name="T"/> es un tipo numérico, <c>false</c> en caso contrario.</returns>
        [Thunk] [Obsolete] public static bool IsNumericType<T>() => IsNumericType(typeof(T));
        /// <summary>
        /// Comprueba que la firma de un método sea compatible con el delegado
        /// especificado.
        /// </summary>
        /// <param name="methodInfo">
        /// <see cref="MethodInfo"/> a comprobar.
        /// </param>
        /// <param name="delegate">
        /// <see cref="Type"/> del <see cref="Delegate"/> a comprobar.
        /// </param>
        /// <returns></returns>
        public static bool IsSignatureCompatible(this MethodInfo methodInfo, Type @delegate)
        {
            return !(Delegate.CreateDelegate(@delegate, methodInfo, false) is null);
        }
        /// <summary>
        /// Busca en el <see cref="AppDomain"/> actual un tipo que contenga el
        /// <see cref="IdentifierAttribute"/> especificado.
        /// </summary>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <returns>
        /// Un tipo que ha sido etiquetado con el identificador especificado,
        /// o <c>null</c> si ningún tipo contiene el identificador.
        /// </returns>
        [Thunk]public static Type FindType(string identifier) => FindType<object>(identifier);
        /// <summary>
        /// Busca en el <see cref="AppDomain"/> actual un tipo que contenga el
        /// <see cref="IdentifierAttribute"/> especificado.
        /// </summary>
        /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <returns>
        /// Un tipo que ha sido etiquetado con el identificador especificado,
        /// o <c>null</c> si ningún tipo contiene el identificador.
        /// </returns>
        [Thunk] public static Type FindType<T>(string identifier) => FindType<T>(identifier, AppDomain.CurrentDomain);
        /// <summary>
        /// Busca en el <see cref="AppDomain"/> especificado un tipo que
        /// contenga el <see cref="IdentifierAttribute"/> especificado.
        /// </summary>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <param name="domain">Dominio en el cual buscar.</param>
        /// <returns>
        /// Un tipo que ha sido etiquetado con el identificador especificado,
        /// o <c>null</c> si ningún tipo contiene el identificador.
        /// </returns>
        [Thunk] public static Type FindType(string identifier, AppDomain domain) => FindType<object>(identifier, domain);
        /// <summary>
        /// Busca en el <see cref="AppDomain"/> especificado un tipo que
        /// contenga el <see cref="IdentifierAttribute"/> especificado.
        /// </summary>
        /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
        /// <param name="identifier">Identificador a buscar.</param>
        /// <param name="domain">Dominio en el cual buscar.</param>
        /// <returns>
        /// Un tipo que ha sido etiquetado con el identificador especificado,
        /// o <c>null</c> si ningún tipo contiene el identificador.
        /// </returns>
        public static Type FindType<T>(string identifier, AppDomain domain)
        {
            foreach (var j in domain.GetTypes<T>())
            {
                foreach (IdentifierAttribute k in j.GetCustomAttributes(typeof(IdentifierAttribute), false))
                {
                    if (k.Value == identifier) return j;
                }
            }
            return default;
        }
    }
}