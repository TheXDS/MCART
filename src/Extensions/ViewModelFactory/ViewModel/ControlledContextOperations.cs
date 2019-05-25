/*
ViewModelFactory.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

//#define IncludeLockBlock
#define AltClearMethod

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using static TheXDS.MCART.Types.Extensions.CollectionExtensions;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Repositorio de funciones comunes ejecutadas desde un
    ///     <see cref="IDynamicViewModel"/> compilado en runtime en un contexto
    ///     controlado.
    /// </summary>
    public static class ControlledContextOperations
    {
        /// <summary>
        ///     Limpia una colección en un contexto controlado.
        /// </summary>
        /// <param name="collection">
        ///     Colección a limpiar.
        /// </param>
        public static void Clear<T>(ICollection<T> collection)
        {
            collection.Locked(c => c.Clear());
        }

        /// <summary>
        ///     Limpia una colección en un contexto controlado.
        /// </summary>
        /// <param name="collection">
        ///     Colección a limpiar.
        /// </param>
        /// <param name="item">
        ///     Elemento a agregar a la colección.
        /// </param>
        public static void Add<T>(ICollection<T> collection, T item)
        {
            collection.Locked(c => c.Add(item));
        }

        internal static MethodInfo Call(string method, params Type[] genericArgs)
        {
            var m = typeof(ControlledContextOperations).GetMethod(method, BindingFlags.Public | BindingFlags.Static);
            return genericArgs.Any() ? m.MakeGenericMethod(genericArgs) : m;
        }
    }
}