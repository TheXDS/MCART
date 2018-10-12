/*
TypeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

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
using TheXDS.MCART.Attributes;

#region Configuración de ReSharper

// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Comprueba si todos los tipos son asignables a partir del tipo
        ///     <paramref name="source" />.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los tipos son asignables a partir de
        ///     <paramref name="source" />, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreAllAssignable(this Type source, IEnumerable<Type> types)
        {
            return types.All(p => p.IsAssignableFrom(source));
        }

        /// <summary>
        ///     Comprueba si todos los tipos son asignables a partir del tipo
        ///     <paramref name="source" />.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        ///     <see langword="true" /> si todos los tipos son asignables a partir de
        ///     <paramref name="source" />, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool AreAllAssignable(this Type source, params Type[] types)
        {
            return source.AreAllAssignable(types.AsEnumerable());
        }

        /// <summary>
        ///     Enumera los tipos asignables a partir de <paramref name="source" />.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        ///     Un enumerador con los tipos que pueden ser asignados a partir de
        ///     <paramref name="source" />.
        /// </returns>
        public static IEnumerable<Type> Assignables(this Type source, IEnumerable<Type> types)
        {
            return types.Where(p => p.IsAssignableFrom(source));
        }

        /// <summary>
        ///     Enumera los tipos asignables a partir de <paramref name="source" />.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        ///     Un enumerador con los tipos que pueden ser asignados a partir de
        ///     <paramref name="source" />.
        /// </returns>
        public static IEnumerable<Type> Assignables(this Type source, params Type[] types)
        {
            return source.Assignables(types.AsEnumerable());
        }

        /// <summary>
        ///     Equivalente programático de <see langword="default" />, obtiene
        ///     el valor predeterminado del tipo.
        /// </summary>
        /// <param name="t">
        ///     <see cref="Type" /> del cual obtener el valor predeterminado.
        /// </param>
        /// <returns>
        ///     Una nueva instancia del tipo si el mismo es un
        ///     <see langword="struct" />, o <see langword="null" /> si es una
        ///     <see langword="class" />.
        /// </returns>
        public static object Default(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        /// <summary>
        ///     Determina si el tipo implementa a <paramref name="baseType" />.
        /// </summary>
        /// <param name="type">Tipo a comprobar</param>
        /// <param name="baseType">Herencia de tipo a verificar.</param>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="type" /> implementa a <paramref name="baseType" />,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool Implements(this Type type, Type baseType)
        {
            if (!baseType.ContainsGenericParameters) return baseType.IsAssignableFrom(type);
            var gt = baseType.MakeGenericType(type);
            return !gt.ContainsGenericParameters && gt.IsAssignableFrom(type);
        }

        /// <summary>
        ///     Determina si el tipo implementa a <paramref name="baseType" /> con los argumentos de tipo genérico especificados.
        /// </summary>
        /// <param name="type">Tipo a comprobar</param>
        /// <param name="baseType">Herencia de tipo a verificar.</param>
        /// <param name="typeArgs">Tipos de argumentos genéricos a utilizar para crear el tipo genérico a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="type"/> implementa a <paramref name="baseType" />,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool Implements(this Type type, Type baseType, params Type[] typeArgs)
        {
            if (!baseType.ContainsGenericParameters) return baseType.IsAssignableFrom(type);
            var gt = baseType.MakeGenericType(typeArgs);
            return !gt.ContainsGenericParameters && gt.IsAssignableFrom(type);
        }

        /// <summary>
        ///     Determina si el tipo implementa a <typeparamref name="T" />.
        /// </summary>
        /// <param name="type">Tipo a comprobar</param>
        /// <typeparam name="T">Herencia de tipo a verificar.</typeparam>
        /// <returns>
        ///     <see langword="true" /> si <paramref name="type" /> implementa a <typeparamref name="T" />,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool Implements<T>(this Type type)
        {
            return Implements(type, typeof(T));
        }

        /// <summary>
        ///     Comprueba si alguno de los tipos especificados es asignable a partir
        ///     del tipo <paramref name="source" />.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        ///     <see langword="true" /> si el tipo <paramref name="source" /> puede ser asignado
        ///     a uno de los tipos especificados, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsAnyAssignable(this Type source, IEnumerable<Type> types)
        {
            return types.Any(p => p.IsAssignableFrom(source));
        }

        /// <summary>
        ///     Comprueba si alguno de los tipos especificados es asignable a partir
        ///     del tipo <paramref name="source" />.
        /// </summary>
        /// <param name="types">Lista de tipos a comprobar.</param>
        /// <param name="source">Tipo que desea asignarse.</param>
        /// <returns>
        ///     <see langword="true" /> si el tipo <paramref name="source" /> puede ser asignado
        ///     a uno de los tipos especificados, <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsAnyAssignable(this Type source, params Type[] types)
        {
            return source.IsAnyAssignable(types.AsEnumerable());
        }

        /// <summary>
        ///     Obtiene un valor que determina si el tipo es instanciable.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <returns>
        ///     <see langword="true" /> si el tipo es instanciable por medio de
        ///     un constructor sin parámetros, <see langword="false" /> en caso
        ///     contrario.
        /// </returns>
        public static bool IsInstantiable(this Type type)
        {
            return IsInstantiable(type, new Type[0]);
        }

        /// <summary>
        ///     Obtiene un valor que determina si el tipo es instanciable.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <param name="constructorArgs">
        ///     Colección con los tipos de argumentos que el constructor a
        ///     buscar debe contener.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el tipo es instanciable por medio de
        ///     un constructor con los parámetros del tipo especificado,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool IsInstantiable(this Type type, IEnumerable<Type> constructorArgs)
        {
            if (constructorArgs is null) return !(type.IsAbstract || type.IsInterface) && type.GetConstructors().Any();
            return !(type.IsAbstract || type.IsInterface) && !(type.GetConstructor(constructorArgs.ToArray()) is null);
        }

        /// <summary>
        ///     Obtiene un valor que determina si el tipo es instanciable
        ///     utilizando un contrustor que acepte los parámetros
        ///     especificados.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <param name="constructorArgs">
        ///     Colección con los tipos de argumentos que el constructor a
        ///     buscar debe contener.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si el tipo es instanciable por medio de
        ///     un constructor con los parámetros del tipo especificado,
        ///     <see langword="false" /> en caso contrario.
        /// </returns>
        [DebuggerStepThrough]
        [Thunk] 
        public static bool IsInstantiable(this Type type, params Type[] constructorArgs)
        {
            return IsInstantiable(type, constructorArgs.AsEnumerable());
        }

        /// <summary>
        ///     Inicializa una nueva instancia del tipo en runtime especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="type">Tipo a instanciar.</param>
        [DebuggerStepThrough]
        [Thunk]
        public static object New(this Type type)
        {
            return type.New<object>(new object[] { });
        }

        /// <summary>
        ///     Inicializa una nueva instancia del tipo dinámico especificado,
        ///     devolviéndola como un <typeparamref name="T" />.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="type">
        ///     Tipo a instanciar. Debe ser, heredar o implementar
        ///     el tipo especificado en <typeparamref name="T" />
        /// </param>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        [DebuggerStepThrough]
        [Thunk]
        public static T New<T>(this Type type)
        {
            return type.New<T>(new object[] { });
        }

        /// <summary>
        ///     Inicializa una nueva instancia de un objeto con un constructor que
        ///     acepte los argumentos provistos.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        /// <param name="type">Tipo a instanciar.</param>
        /// <param name="parameters">
        ///     Parámetros a pasar al constructor. Se buscará
        ///     un constructor compatible para poder crear la instancia.
        /// </param>
        [DebuggerStepThrough]
        [Thunk]
        public static object New(this Type type, params object[] parameters)
        {
            return type.New<object>(parameters);
        }

        /// <summary>
        ///     Inicializa una nueva instancia de un objeto con un constructor que
        ///     acepte los argumentos provistos.
        /// </summary>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        /// <param name="type">
        ///     Tipo a instanciar. Debe ser, heredar o implementar
        ///     el tipo especificado en <typeparamref name="T" />.
        /// </param>
        /// <param name="parameters">
        ///     Parámetros a pasar al constructor. Se buscará
        ///     un constructor compatible para poder crear la instancia.
        /// </param>
        /// <returns>Una nueva instancia del tipo especificado.</returns>
        /// <exception cref="TypeLoadException">
        ///     Se produce si no es posible instanciar una clase del tipo
        ///     solicitado.
        /// </exception>
        [DebuggerStepThrough]
        public static T New<T>(this Type type, params object[] parameters)
        {
            return New<T>(type, true, parameters);
        }

        /// <summary>
        ///     Inicializa una nueva instancia de un objeto con un constructor que
        ///     acepte los argumentos provistos.
        /// </summary>
        /// <typeparam name="T">Tipo de instancia a devolver.</typeparam>
        /// <param name="type">
        ///     Tipo a instanciar. Debe ser, heredar o implementar
        ///     el tipo especificado en <typeparamref name="T" />.
        /// </param>
        /// <param name="throwOnFail">
        ///     Si se establece en <see langword="true"/>, se producirá una
        ///     excepción en caso que el tipo no pueda instanciarse con la
        ///     información provista, o se devolverá <see langword="null"/> si
        ///     se establece en <see langword="false"/>
        /// </param>
        /// <param name="parameters">
        ///     Parámetros a pasar al constructor. Se buscará
        ///     un constructor compatible para poder crear la instancia.
        /// </param>
        /// <returns>Una nueva instancia del tipo especificado.</returns>
        /// <exception cref="TypeLoadException">
        ///     Se produce si no es posible instanciar una clase del tipo
        ///     solicitado.
        /// </exception>
        [DebuggerStepThrough]
        public static T New<T>(this Type type, bool throwOnFail, params object[] parameters)
        {
            if (!type.IsInstantiable(parameters.ToTypes()))
            {
                return throwOnFail ? throw new TypeLoadException() : (T) default;
            }

            try
            {
                return (T) type.GetConstructor(parameters.ToTypes().ToArray())?.Invoke(parameters);

            }
            catch (Exception e)
            {
                return throwOnFail ? throw new TypeLoadException(string.Empty, e) : (T)default;
            }
        }

        /// <summary>
        ///     Obtiene un nombre personalizado para un tipo.
        /// </summary>
        /// <param name="type">
        ///     <see cref="Type" /> del cual obtener el nombre.
        /// </param>
        /// <returns>
        ///     Un nombre amigable para <paramref name="type" />, o el nombre de
        ///     tipo de <paramref name="type" /> si no se ha definido un nombre
        ///     amigable por medio del atributo <see cref="NameAttribute" />.
        /// </returns>
        public static string TypeName(this Type type)
        {
            return type.GetAttr<NameAttribute>()?.Value ?? type.Name;
        }
    }
}