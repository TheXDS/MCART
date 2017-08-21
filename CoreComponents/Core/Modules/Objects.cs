﻿//
//  Objects.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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

        [Thunk]
        public static bool AnyAssignableFrom(this IEnumerable<Type> types, Type source)
        {
            return AnyAssignableFrom(types, source, out int? ignore);
        }
        /// <summary>
        /// Comprueba si todos los tipos son asignables a partir del tipo
        /// <paramref name="source"/>.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns><c>true</c> si todos los tipos son asignables a partir de
        /// <paramref name="source"/>; de lo contrario, <c>false</c>.</returns>
        public static bool AreAssignableFrom(this Type[] types, Type source)
        {
            foreach (Type j in types)
                if (!j.IsAssignableFrom(source)) return false;
            return true;
        }
        /// <summary>
        /// Determina si cualquiera de los objetos es <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si cualquiera de los objetos es <c>null</c>; de lo
        /// contrario, <c>false</c>.
        /// </returns>
        /// <param name="x">Objetos a comprobar.</param>
        public static bool AreAnyNull(params object[] x)
        {
            foreach (object j in x) if (j.IsNull()) return true;
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
        public static bool AreAllNull(params object[] x)
        {
            foreach (object j in x) if (!j.IsNull()) return false;
            return true;
        }
        /// <summary>
        /// Obtiene un valor que determina si el objeto es <c>null</c>.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el objeto es <c>null</c>; de lo contrario,
        /// <c>false</c>.
        /// </returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <typeparam name="T">Tipo del objeto.</typeparam>
        [Thunk] public static bool IsNull<T>(this T obj) => ReferenceEquals(obj, null);
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
        /// <returns></returns>
        [Thunk] public static bool Is(this object obj1, object obj2) => ReferenceEquals(obj1, obj2);
        /// <summary>
        /// Determina si <paramref name="obj1"/> es una instancia diferente a
        /// <paramref name="obj2"/>.
        /// </summary>
        /// <param name="obj1">Objeto a comprobar.</param>
        /// <param name="obj2">Objeto contra el cual comparar.</param>
        /// <returns>
        /// <c>true</c> si la instancia de <paramref name="obj1"/> es
        /// <paramref name="obj2"/>; de lo contrario, <c>false</c>.</returns>
        [Thunk] public static bool IsNot(this object obj1, object obj2) => !ReferenceEquals(obj1, obj2);
        /// <summary>
		/// Determina si un objeto es cualquiera de los indicados.
		/// </summary>
		/// <returns><c>true</c>si <paramref name="obj"/> es cualquiera de los
		/// objetos especificados; de lo contrario, <c>false</c>.</returns>
		/// <param name="obj">Objeto a comprobar.</param>
		/// <param name="objs">Lista de objetos a comparar.</param>
		public static bool IsEither(this object obj, params object[] objs)
        {
            foreach (object o in objs) if (ReferenceEquals(obj, o)) return true;
            return false;
        }
        /// <summary>
        /// Determina si un objeto no es ninguno de los indicados.
        /// </summary>
        /// <returns><c>true</c>si <paramref name="obj"/> no es ninguno de los
        /// objetos especificados; de lo contrario, <c>false</c>.</returns>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <param name="objs">Lista de objetos a comparar.</param>
        public static bool IsNeither(this object obj, params object[] objs)
        {
            foreach (object o in objs) if (ReferenceEquals(obj, o)) return false;
            return true;
        }
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
        public static IEnumerable<Type> ToTypes(this object[] objects)
        {
            foreach (object j in objects) yield return j.GetType();
        }
        /// <summary>
        /// Inicializa una nueva instancia del tipo dinámico especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <typeparam name="T">Tipo de instancia a crear.</typeparam>
        public static T New<T>()
        {
            try { return typeof(T).New<T>(new object[] { }); }
            catch { throw; }
        }
        /// <summary>
        /// Inicializa una nueva instancia del tipo dinámico especificado,
        /// devolviéndola como un <typeparamref name="T"/>.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar. Debe ser, heredar o implementar 
        /// el tipo especificado en <typeparamref name="T"/></param>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        public static T New<T>(this Type j)
        {
            if (j.IsAbstract || j.IsInterface) throw new TypeLoadException();
            try { return j.New<T>(new object[] { }); }
            catch { throw; }
        }
        /// <summary>
        /// Inicializa una nueva instancia del tipo dinámico especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar.</param>
        public static object New(this Type j)
        {
            if (j.IsAbstract || j.IsInterface) throw new TypeLoadException();
            try { return j.New<object>(new object[] { }); }
            catch { throw; }
        }
        /// <summary>
		/// Crea uns instancia de un objeto con un constructor que acepte los 
		/// argumentos provistos.
		/// </summary>
		/// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
		/// <param name="Params">Parámetros a pasar al constructor. Se buscará 
		/// un constructor compatible para poder crear la instancia.</param>
		/// <returns>Una nueva instancia del tipo especificado.</returns>
        public static T New<T>(object[] Params)
        {
            try { return New<T>(typeof(T), Params); }
            catch { throw; }
        }
        /// <summary>
        /// Crea uns instancia de un objeto con un constructor que acepte los 
        /// argumentos provistos.
        /// </summary>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        /// <param name="j">Tipo a instanciar. Debe ser, heredar o implementar 
        /// el tipo especificado en <typeparamref name="T"/>.</param>
        /// <param name="Params">Parámetros a pasar al constructor. Se buscará 
        /// un constructor compatible para poder crear la instancia.</param>
        /// <returns>Una nueva instancia del tipo especificado.</returns>
        public static T New<T>(this Type j, params object[] Params)
        {
            if (j.IsAbstract || j.IsInterface) throw new TypeLoadException();
            try { return (T)j.GetConstructor(Params.ToTypes().ToArray()).Invoke(Params); }
            catch { throw; }
        }
        /// <summary>
        /// Inicializa una nueva instancia del tipo dinámico especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="j">Tipo a instanciar.</param>
        /// <param name="Params">Parámetros a pasar al constructor. Se buscará 
        /// un constructor compatible para poder crear la instancia.</param>
        [Thunk]
        public static object New(this Type j, params object[] Params)
        {
            if (j.IsAbstract || j.IsInterface) throw new TypeLoadException();
            try { return j.New<object>(Params); }
            catch { throw; }
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
        /// Devuelve el atributo asociado a la declaración del objeto especificado
        /// </summary>
        /// <typeparam name="T">Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.</typeparam>
        /// <param name="it"><see cref="System.Reflection.Assembly"/> del cual se extraerá el atributo.</param>
        /// <returns>Un atributo del tipo <typeparamref name="T"/> con los datos asociados en la declaración del objeto.</returns>
        [Thunk] public static T GetAttr<T>(this System.Reflection.Assembly it) where T : Attribute => (T)Attribute.GetCustomAttribute(it, typeof(T));
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del objeto especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de atributo a devolver. Debe heredar <see cref="Attribute"/>.</typeparam>
        /// <param name="it">Objeto del cual se extraerá el atributo.</param>
        /// <returns>Un atributo del tipo <typeparamref name="T"/> con los datos asociados en la declaración del objeto.</returns>
        [Thunk] public static T GetAttr<T>(this object it) where T : Attribute => (T)Attribute.GetCustomAttribute(it.GetType(), typeof(T));
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del tipo
        /// especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de atributo a devolver. Debe heredar 
        /// <see cref="Attribute"/>.</typeparam>
        /// <typeparam name="it">
        /// Tipo del cual se extraerá el atributo.
        /// </typeparam>
        /// <returns>
        /// Un atributo del tipo <typeparamref name="T"/> con los datos 
        /// asociados en la declaración del tipo.
        /// </returns>
        [Thunk] public static T GetAttr<T, it>() where T : Attribute => (T)Attribute.GetCustomAttribute(typeof(it), typeof(T));
        /// <summary>
        /// Devuelve el atributo asociado a la declaración del tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo de atributo a devolver. Debe heredar 
        /// <see cref="Attribute"/>.</typeparam>
        /// <param name="it">Tipo del cual se extraerá el atributo</param>
        /// <returns>Un atributo del tipo <typeparamref name="T"/> con los datos
        ///  asociados en la declaración del tipo.</returns>
        [Thunk] public static T GetAttr<T>(this Type it) where T : Attribute => (T)Attribute.GetCustomAttribute(it, typeof(T));
        /// <summary>
        /// Devuelve <c>True</c> si el tipo <typeparamref name="T"/> es alguno de los tipos especificados
        /// </summary>
        /// <typeparam name="T">Tipo a comprobar</typeparam>
        /// <param name="Types">Lista de tipos aceptados</param>
        /// <returns><c>True</c> si <typeparamref name="T"/> es alguno de los tipos especificados en <paramref name="Types"/>; de lo contrario, <c>False</c>.</returns>
        [Thunk] public static bool IsTypeAnyOf<T>(params Type[] Types) => Types.Contains(typeof(T));
        /// <summary>
        /// Devuelve <c>True</c> si el tipo <paramref name="T"/> es alguno de los tipos especificados
        /// </summary>
        /// <param name="T">Tipo a comprobar</param>
        /// <param name="Types">Lista de tipos aceptados</param>
        /// <returns><c>True</c> si <paramref name="T"/> es alguno de los tipos especificados en <paramref name="Types"/>; de lo contrario, <c>False</c>.</returns>
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
            return new Type[]{
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
        /// <returns><c>True</c> si <typeparamref name="T"/> es un tipo numérico; de lo contrario, <c>False</c>.</returns>
        [Thunk] public static bool IsNumericType<T>() => IsNumericType(typeof(T));

        /*
		 * Oops! Alguas API de Mono parecen no estar completas, esta directiva
		 * deshabilita la advertencia al compilar desde MonoDevelop.
		 * 
		 * SecureString no provee de funcionalidad de encriptado en Mono, lo que
		 * podría ser inseguro.
         */
#pragma warning disable XS0001

        /// <summary>
        /// Convierte un <see cref="System.Security.SecureString"/> en un
        /// <see cref="string"/>.
        /// </summary>
        /// <param name="value">
        /// <see cref="System.Security.SecureString"/> a convertir.
        /// </param>
        /// <returns>Un <see cref="string"/> de código administrado.</returns>
        /// <remarks>
        /// El uso de este método NO ESTÁ RECOMENDADO, ya que la conversión al
        /// tipo <see cref="string"/> vence el propósito original de
        /// <see cref="System.Security.SecureString"/>, y se provee como una
        /// alternativa sencilla, en casos en los que el programa no dependa de
        /// que la confidencialidad de una cadena en particular se deba
        /// mantener durante la ejecución.
        /// </remarks>
        [Unsecure]
        public static string ReadString(this System.Security.SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally { Marshal.ZeroFreeGlobalAllocUnicode(valuePtr); }
        }
        /// <summary>
        /// Convierte un <see cref="System.Security.SecureString"/> en un
        /// arreglo de <see cref="short"/>.
        /// </summary>
        /// <param name="value">
        /// <see cref="System.Security.SecureString"/> a convertir.
        /// </param>
        /// <returns>Un <see cref="string"/> de código administrado.</returns>
        public static short[] Read(this System.Security.SecureString value)
        {
            List<short> outp = new List<short>();
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (int i = 0; i < value.Length; i++) outp.Add(Marshal.ReadInt16(valuePtr, i * 2));
                return outp.ToArray();
            }
            finally { Marshal.ZeroFreeGlobalAllocUnicode(valuePtr); }
        }

#pragma warning restore XS0001

    }
}