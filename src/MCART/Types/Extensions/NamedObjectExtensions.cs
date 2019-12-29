/*
NamedObjectExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.Type del CLR,
supliéndolo de nueva funcionalidad previamente no existente, o de invocación
compleja.

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

using System;
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Funciones misceláneas y extensiones para todos los elementos de
    /// tipo <see cref="NamedObject{T}"/>.
    /// </summary>
    public static class NamedObjectExtensions
    {
        /// <summary>
        /// Enumera todos los valores de enumeración del tipo especificado
        /// como <see cref="NamedObject{T}"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de enumeración a convertir.</typeparam>
        /// <returns>
        /// Una enumeración de <see cref="NamedObject{T}"/> a partir de los
        /// valores de enumeración del tipo especificado.
        /// </returns>
        [Sugar]
        public static IEnumerable<NamedObject<T>> AsNamedObject<T>() where T : Enum
        {
            return NamedObject<T>.FromEnum();
        }
        /// <summary>
        /// Enumera todos los valores de enumeración del tipo especificado
        /// como <see cref="NamedObject{T}"/>.
        /// </summary>
        /// <param name="t">Tipo de enumeración a convertir.</param>
        /// <returns>
        /// Una enumeración de <see cref="NamedObject{T}"/> a partir de los
        /// valores de enumeración del tipo especificado.
        /// </returns>
        public static IEnumerable<NamedObject<Enum>> AsNamedEnum(this Type t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            var q = t.IsEnum ? t : Nullable.GetUnderlyingType(t)!;
            if (!q.IsEnum) throw new InvalidTypeException(t);
            return q.GetEnumValues().OfType<Enum>().Select(p => new NamedObject<Enum>(p, p.NameOf()));
        }
    }
}