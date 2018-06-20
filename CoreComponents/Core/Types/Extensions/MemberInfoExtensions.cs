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

using System.ComponentModel;
using System.Reflection;
using TheXDS.MCART;
using TheXDS.MCART.Types.Extensions;

namespace MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones varias para objetos <see cref="MemberInfo" />.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Establece el valor de la propiedad de un objeto a su valor predeterminado.
        /// </summary>
        /// <param name="property">Propiedad a restablecer.</param>
        /// <param name="instance">Instancia del objeto que contiene la propiedad.</param>
        public static void Default(this PropertyInfo property, object instance)
        {
            property.SetMethod?.Invoke(instance,
                new[] {property.GetAttr<DefaultValueAttribute>()?.Value ?? property.PropertyType.Default()});
        }

        /// <summary>
        /// Establece el valor de una propiedad estática a su valor predeterminado.
        /// </summary>
        /// <param name="property">Propiedad a restablecer.</param>
        public static void Default(this PropertyInfo property) => Default(property, null);
    }
}