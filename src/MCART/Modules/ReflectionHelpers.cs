/*
ReflectionHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART
{
    /// <summary>
    ///     Funciones auxiliares de reflexión.
    /// </summary>
    public static class ReflectionHelpers
    {
        /// <summary>
        ///     Obtiene una referencia al método que ha llamado al método
        ///     actualmente en ejecución.
        /// </summary>
        /// <returns>
        ///     El método que ha llamado al método actual en donde se usa esta
        ///     función. Se devolverá <see langword="null"/> si se llama a este
        ///     método desde el punto de entrada de la aplicación (generalmente
        ///     la función <c>Main()</c>).
        /// </returns>
        public static MethodBase GetCallingMethod()
        {
            return GetCallingMethod(2);
        }

        /// <summary>
        ///     Obtiene una referencia al método que ha llamado al método actual.
        /// </summary>
        /// <param name="nCaller">
        ///     Número de iteraciones padre del método a devolver. Debe ser un
        ///     valor mayor o igual a 1.
        /// </param>
        /// <returns>
        ///     El método que ha llamado al método actual en donde se usa esta
        ///     función. Se devolverá <see langword="null"/> si al analizar la
        ///     pila de llamadas se alcanza el punto de entrada de la
        ///     aplicación (generalmente la función <c>Main()</c>).
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="nCaller"/> es menor a 1.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     Se produce si <paramref name="nCaller"/> + 1 produce un error
        ///     de sobreflujo.
        /// </exception>
        public static MethodBase GetCallingMethod(int nCaller)
        {
            if (checked(nCaller++) < 1) throw new ArgumentOutOfRangeException(nameof(nCaller));
            var frames = new StackTrace().GetFrames();
            return frames?.Length >= nCaller ? frames[nCaller].GetMethod() : null;
        }

        /// <summary>
        ///     Determina si el método invalida a una definición base.
        /// </summary>
        /// <param name="method"></param>
        /// <returns>
        ///     <see langword="true"/> si el método invalida a una definición
        ///     base, <see langword="false"/> em caso contrario.
        /// </returns>
        public static bool IsOverride(this MethodInfo method)
        {
            return method.GetBaseDefinition() != method;
        }

        /// <summary>Obtiene un nombre completo para un método.</summary>
        /// <param name="method">Método del cual obtener el nombre.</param>
        /// <returns>El nombre completo del método.</returns>
        public static string FullName(this MethodInfo method)
        {
            var s = new StringBuilder();
            s.Append($"{method.DeclaringType.Namespace}.{method.DeclaringType.Name}.{method.Name}");
            if (method.IsGenericMethod)
            {
                s.Append($"<{string.Join(", ", method.GetGenericArguments().Select(q => q.Name))}>");
            }
            s.Append($"({string.Join(", ", method.GetParameters().Select(q => q.ParameterType.FullName))})");
            return s.ToString();
        }

        /// <summary>
        ///     Obtiene un miembro de una clase a partir de una expresión.
        /// </summary>
        /// <typeparam name="T">
        ///     Clase desde la cual obtener al miembro.
        /// </typeparam>
        /// <param name="memberSelector">
        ///     Expresión que indica qué miembro de la clase debe devolverse.
        /// </param>
        /// <returns>
        ///     Un <see cref="MemberInfo"/> que representa al miembro
        ///     seleccionado en la expresión.
        /// </returns>
        public static MemberInfo GetMember<T>(Expression<Func<T, object>> memberSelector)
        {
            if (memberSelector.Body is UnaryExpression UnExp && UnExp.Operand is MemberExpression)                
                return ((MemberExpression)UnExp.Operand).Member;
            else if (memberSelector.Body is MemberExpression)            
                return ((MemberExpression)memberSelector.Body).Member;
            throw new ArgumentException();
        }

        /// <summary>
        ///     Obtiene un miembro de una clase a partir de una expresión.
        /// </summary>
        /// <typeparam name="T">
        ///     Clase desde la cual obtener al miembro.
        /// </typeparam>
        /// <typeparam name="TProperty">
        ///     Tipo de la propiedad obtenida.
        /// </typeparam>
        /// <param name="memberSelector">
        ///     Expresión que indica qué miembro de la clase debe devolverse.
        /// </param>
        /// <returns>
        ///     Un <see cref="MemberInfo"/> que representa al miembro
        ///     seleccionado en la expresión.
        /// </returns>
        public static MemberInfo GetMember<T, TProperty>(Expression<Func<T, TProperty>> memberSelector)
        {
            if (memberSelector.Body is UnaryExpression UnExp && UnExp.Operand is MemberExpression)
                return ((MemberExpression)UnExp.Operand).Member;
            else if (memberSelector.Body is MemberExpression)
                return ((MemberExpression)memberSelector.Body).Member;
            throw new ArgumentException();
        }
    }

    /// <summary>
    ///     Funciones auxiliares para trabajar con colecciones y enumeraciones.
    /// </summary>
    public static class CollectionHelpers
    {
        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [Thunk]
        public static bool Or(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current | j);
        }
        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [Thunk]
        public static bool And(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current & j);
        }
        /// <summary>
        ///     Aplica un operador OR a una colección de valores
        ///     <see cref="bool"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [Thunk]
        public static bool Xor(this IEnumerable<bool> collection)
        {
            return collection.Aggregate(false, (current, j) => current ^ j);
        }
    }
}