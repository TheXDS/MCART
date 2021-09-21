/*
NameValueCollectionExtensions.cs

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

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Funciones misceláneas y extensiones para todos los elementos de
    /// tipo <see cref="NameValueCollection"/>.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Convierte un <see cref="NameValueCollection"/> en una colección de
        /// <see cref="IGrouping{TKey, TElement}"/>.
        /// </summary>
        /// <param name="nvc">
        /// <see cref="NameValueCollection"/> a convertir.
        /// </param>
        /// <returns>
        /// Una colección de <see cref="IGrouping{TKey, TElement}"/> con las
        /// llaves y sus respectivos valores.
        /// </returns>
        public static IEnumerable<IGrouping<string?, string>> ToGroup(this NameValueCollection nvc)
        {
            foreach (var j in nvc.AllKeys)
            {
                if (nvc.GetValues(j) is { } v) yield return new Grouping<string?, string>(j, v);
            }
        }

        /// <summary>
        /// Convierte un <see cref="NameValueCollection"/> en una colección de
        /// <see cref="NamedObject{T}"/>.
        /// </summary>
        /// <param name="nvc">
        /// <see cref="NameValueCollection"/> a convertir.
        /// </param>
        /// <returns>
        /// Una colección de <see cref="NamedObject{T}"/> con las llaves y sus
        /// respectivos valores.
        /// </returns>
        /// <remarks>
        /// Este método omitirá todos aquellos valores cuyo nombre sea
        /// <see langword="null"/>.
        /// </remarks>
        public static IEnumerable<NamedObject<string[]>> ToNamedObjectCollection(this NameValueCollection nvc)
        {
            foreach (var j in nvc.AllKeys.NotNull())
            {
                if (nvc.GetValues(j) is { } v) yield return new NamedObject<string[]>(v, j);
            }
        }

        /// <summary>
        /// Convierte un <see cref="NameValueCollection"/> en una colección de
        /// <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <param name="nvc">
        /// <see cref="NameValueCollection"/> a convertir.
        /// </param>
        /// <returns>
        /// Una colección de <see cref="KeyValuePair{TKey, TValue}"/> con las
        /// llaves y sus respectivos valores.
        /// </returns>
        public static IEnumerable<KeyValuePair<string?, string>> ToKeyValuePair(this NameValueCollection nvc)
        {
            foreach (var j in nvc.AllKeys)
            {
                if (nvc.Get(j) is { } v) yield return new KeyValuePair<string?, string>(j, v);
            }
        }

        /// <summary>
        /// Convierte un <see cref="NameValueCollection"/> en una colección de
        /// <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="nvc">
        /// <see cref="NameValueCollection"/> a convertir.
        /// </param>
        /// <returns>
        /// Una colección de <see cref="Dictionary{TKey, TValue}"/> con las
        /// llaves y sus respectivos valores.
        /// </returns>
        /// <remarks>
        /// Este método omitirá todos aquellos valores cuyo nombre sea
        /// <see langword="null"/>.
        /// </remarks>
        public static Dictionary<string, string[]> ToDictionary(this NameValueCollection nvc)
        {
            var d = new Dictionary<string, string[]>();
            foreach (var j in nvc.AllKeys.NotNull())
            {
                if (nvc.GetValues(j) is { } v) d.Add(j, v);
            }
            return d;
        }
    }
}