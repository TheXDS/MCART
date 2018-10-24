/*
MemberInfoExtensions.cs

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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones varias para objetos <see cref="MemberInfo" />.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        ///     Establece el valor de la propiedad de un objeto a su valor predeterminado.
        /// </summary>
        /// <param name="property">Propiedad a restablecer.</param>
        /// <param name="instance">Instancia del objeto que contiene la propiedad.</param>
        public static void Default(this PropertyInfo property, object instance)
        {
            if (instance is null || instance.GetType().GetProperties().Any(p => p.Is(property)))
                property.SetMethod?.Invoke(instance,
                    new[] { property.GetAttr<DefaultValueAttribute>()?.Value ?? property.PropertyType.Default() });
            else
                throw new MissingMemberException(instance.GetType().Name, property.Name);
        }

        /// <summary>
        ///     Establece el valor de una propiedad estática a su valor predeterminado.
        /// </summary>
        /// <param name="property">Propiedad a restablecer.</param>
        public static void Default(this PropertyInfo property)
        {
            Default(property, null);
        }

        /// <summary>
        ///     Obtiene un nombre personalizado para un miembro.
        /// </summary>
        /// <param name="member">
        ///     <see cref="MemberInfo" /> del cual obtener el nombre.
        /// </param>
        /// <returns>
        ///     Un nombre amigable para <paramref name="member" />, o el nombre
        ///     definido para <paramref name="member" /> si no se ha definido
        ///     un nombre amigable por medio del atributo
        ///     <see cref="NameAttribute"/>.
        /// </returns>
        public static string NameOf(this MemberInfo member)
        {
            return member.GetAttr<NameAttribute>()?.Value ?? member.Name;
        }
    }
}