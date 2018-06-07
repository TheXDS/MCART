/*
TypeExtensions.cs

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
using TheXDS.MCART;
using TheXDS.MCART.Attributes;

namespace MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo <see cref="Type" />.
    /// </summary>
    public static class TypeExtensions
    {
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
        ///     Obtiene un nombre personalizado para un tipo.
        /// </summary>
        /// <param name="t">
        ///     <see cref="Type" /> del cual obtener el nombre.
        /// </param>
        /// <returns>
        ///     Un nombre amigable para <paramref name="t" />, o el nombre de
        ///     tipo de <paramref name="t" /> si no se ha definido un nombre
        ///     amigable por medio del atributo <see cref="NameAttribute" />.
        /// </returns>
        public static string TypeName(this Type t)
        {
            return t.GetAttr<NameAttribute>()?.Value ?? t.Name;
        }
    }
}