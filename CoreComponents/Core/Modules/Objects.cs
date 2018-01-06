//
//  Objects.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MCART.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace MCART
{
    /// <summary>
    /// Funciones de manipulación de objetos.
    /// </summary>
    public static class Objects
    {
        /// <summary>
        /// Comprueba si alguno de los tipos especificados es asignable a partir
        /// del tipo <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <param name="index">
        /// Argumento de salida. Indica el índice del primer tipo que puede ser
        /// asignado a partir de <paramref name="source"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> si el tipo <paramref name="source"/> puede ser asignado 
        /// a uno de los tipos especificados, <c>false</c> en caso contrario.
        /// </returns>
        public static bool AnyAssignableFrom(this IEnumerable<Type> types, Type source, out int? index)
        {
            index = 0;
            foreach (Type j in types)
            {
                if (j.IsAssignableFrom(source)) return true;
                index++;
            }
            index = null;
            return false;
        }
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
        [Thunk] public static bool AnyAssignableFrom(this IEnumerable<Type> types, Type source) => AnyAssignableFrom(types, source, out _);
        /// <summary>
        /// Comprueba si todos los tipos son asignables a partir del tipo
        /// <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns><c>true</c> si todos los tipos son asignables a partir de
        /// <paramref name="source"/>, <c>false</c> en caso contrario.</returns>
        [Thunk] public static bool AreAssignableFrom(this Type[] types, Type source) => types.All(p => p.IsAssignableFrom(source));
        /// <summary>
        /// Determina si cualquiera de los objetos es <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si cualquiera de los objetos es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        [Thunk] public static bool IsAnyNull(params object[] x) => x.Any(p => p is null);
        /// <summary>
        /// Determina si cualquiera de los objetos es <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si cualquiera de los objetos es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="index">
        /// Parámetro de salida. Si se encuentra un objeto que es <c>null</c>,
        /// este valor será igual al índice de dicho objeto, en caso contrario, 
        /// se devolverá <c>-1</c>.
        /// </param>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool IsAnyNull(out int index, params object[] x)
        {
            index = 0;
            foreach (object j in x)
            {
                if (j is null) return true;
                index++;
            }
            index = -1;
            return false;
        }
        /// <summary>
        /// Determina si todos los objetos son <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si todos los objetos son <c>null</c>; de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        [Thunk] public static bool AreAllNull(params object[] x) => x.All(p => p is null);
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
		[Thunk] public static bool IsEither(this object obj, params object[] objs) => objs.Any(p => p.Is(obj));
        /// <summary>
        /// Determina si un objeto no es ninguno de los indicados.
        /// </summary>
        /// <returns><c>true</c>si <paramref name="obj"/> no es ninguno de los
        /// objetos especificados, <c>false</c> en caso contrario.</returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        [Thunk] public static bool IsNeither(this object obj, params object[] objs) => objs.All(p => !p.Is(obj));
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
            return Domain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(
                p => typeof(T).IsAssignableFrom(p)).ToList();
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
        /// Inicializa una nueva instancia del tipo en runtime especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <typeparam name="T">Tipo de instancia a crear.</typeparam>
        [DebuggerStepThrough] [Thunk] public static T New<T>() => typeof(T).New<T>(new object[] { });
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
        /// Inicializa una nueva instancia del tipo en runtime especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar.</param>
        [DebuggerStepThrough] [Thunk] public static object New(this Type j) => j.New<object>(new object[] { });
        /// <summary>
		/// Crea uns instancia de un objeto con un constructor que acepte los 
		/// argumentos provistos.
		/// </summary>
		/// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
		/// <param name="Params">Parámetros a pasar al constructor. Se buscará 
		/// un constructor compatible para poder crear la instancia.</param>
		/// <returns>Una nueva instancia del tipo especificado.</returns>
        [DebuggerStepThrough] [Thunk] public static T New<T>(object[] Params) => New<T>(typeof(T), Params);
        /// <summary>
        /// Inicializa una nueva instancia de un objeto con un constructor que
        /// acepte los argumentos provistos.
        /// </summary>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        /// <param name="j">Tipo a instanciar. Debe ser, heredar o implementar 
        /// el tipo especificado en <typeparamref name="T"/>.</param>
        /// <param name="Params">Parámetros a pasar al constructor. Se buscará 
        /// un constructor compatible para poder crear la instancia.</param>
        /// <returns>Una nueva instancia del tipo especificado.</returns>
        /// <exception cref="TypeLoadException">
        /// Se produce si no es posible instanciar una clase del tipo
        /// solicitado.
        /// </exception>
        [DebuggerStepThrough]
        public static T New<T>(this Type j, params object[] Params)
        {
#if NoDanger
            if (j.HasAttr<DangerousAttribute>()) throw new Exceptions.DangerousClassException(j);
#endif
            if (j.IsAbstract || j.IsInterface) throw new TypeLoadException();
            return (T)j.GetConstructor(Params.ToTypes().ToArray()).Invoke(Params);
        }
        /// <summary>
        /// Inicializa una nueva instancia del tipo en runtime especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar.</param>
        /// <param name="Params">Parámetros a pasar al constructor. Se buscará 
        /// un constructor compatible para poder crear la instancia.</param>
        [DebuggerStepThrough] [Thunk] public static object New(this Type j, params object[] Params) => j.New<object>(Params);
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
        /// Devuelve <c>true</c> si el tipo <typeparamref name="T"/> es alguno de los tipos especificados
        /// </summary>
        /// <typeparam name="T">Tipo a comprobar</typeparam>
        /// <param name="Types">Lista de tipos aceptados</param>
        /// <returns><c>true</c> si <typeparamref name="T"/> es alguno de los tipos especificados en <paramref name="Types"/>, <c>false</c> en caso contrario.</returns>
        [Thunk] public static bool IsTypeAnyOf<T>(params Type[] Types) => Types.Contains(typeof(T));
        /// <summary>
        /// Devuelve <c>true</c> si el tipo <paramref name="T"/> es alguno de los tipos especificados
        /// </summary>
        /// <param name="T">Tipo a comprobar</param>
        /// <param name="Types">Lista de tipos aceptados</param>
        /// <returns><c>true</c> si <paramref name="T"/> es alguno de los tipos especificados en <paramref name="Types"/>, <c>false</c> en caso contrario.</returns>
        [Thunk] public static bool IsTypeAnyOf(Type T, params Type[] Types) => Types.Contains(T);
        /// <summary>
        /// Determina si el tipo <paramref name="T"/> es de un tipo numérico
        /// </summary>
        /// <param name="T">Tipo a comprobar</param>
        /// <returns><c>true</c> si <paramref name="T"/> es un tipo numérico; de
        /// lo contrario, <c>false</c>.</returns>
        [Stub]
        [Thunk]
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
        [Thunk] public static bool IsNumericType<T>() => IsNumericType(typeof(T));
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
    }
}