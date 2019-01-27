/*
RTInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Reflection;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene métodos con funciones de identificación en información del 
    /// ensamblado de MCART.
    /// </summary>
    public static partial class RTInfo
    {
        internal static bool? RTSupport<T>(T obj)
        {
            if (!obj.HasAttr(out TargetMCARTVersionAttribute tt)) return null;
            if (!obj.HasAttr(out MinMCARTVersionAttribute mt))
#if StrictMCARTVersioning
                return null;
#else
                return RTVersion == tt?.Value;
#endif
            return RTVersion.IsBetween(mt?.Value, tt?.Value);
        }
        /// <summary>
        /// Obtiene la versión del ensamblado de <see cref="MCART"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="Version"/> con la información de versión de 
        /// <see cref="MCART"/>.
        /// </returns>
        public static Version RTVersion => RTAssembly.GetName().Version;
        /// <summary>
        /// Obtiene la referencia del ensamblado de MCART
        /// </summary>
        /// <returns>The ssembly.</returns>
        public static Assembly RTAssembly => typeof(RTInfo).Assembly;
        /// <summary>
        /// Comprueba si el ensamblado es compatible con esta versión de <see cref="MCART"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el ensamblado es compatible con esta
        /// versión de <see cref="MCART"/>, <see langword="false"/> si no lo
        /// es, y <see langword="null"/> si no se ha podido determinar la
        /// compatibilidad.
        /// </returns>
        /// <param name="asmbly">Ensamblado a comprobar.</param>
        public static bool? RTSupport(Assembly asmbly) => RTSupport<Assembly>(asmbly);
        /// <summary>
        /// Comprueba si el <see cref="Type"/> es compatible con esta versión de <see cref="MCART"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el <see cref="Type"/> es compatible con esta
        /// versión de <see cref="MCART"/>, <see langword="false"/> si no lo
        /// es, y <see langword="null"/> si no se ha podido determinar la
        /// compatibilidad.
        /// </returns>
        /// <param name="type"><see cref="Type"/> a comprobar.</param>
        public static bool? RTSupport(Type type)
        {
            /* HACK: Problema al implementar RTSupport(Type)
             * Esta función debe reimplementarse completa debido a un
             * problema de boxing al intentar llamar a RTSupport<T>(T), ya que
             * .Net Framework podría pasar un objeto de tipo interno, 
             * System.Reflection.RuntimeType, el cual se encaja como Object al
             * intentar llamar a la función mencionada, causando que se llame a
             * la función HasAttr<T>(object, T) en lugar de HasAttr(Type, T),
             * lo cual no es la implementación intencionada.
             */
            if (!type.HasAttr(out TargetMCARTVersionAttribute tt)) return null;
            if (!type.HasAttr(out MinMCARTVersionAttribute mt))
#if StrictMCARTVersioning
                return null;
#else
                return RTVersion == tt?.Value;
#endif
            return RTVersion.IsBetween(mt?.Value, tt?.Value);
        }
        /// <summary>
        /// Obtiene la versión de MCART como una cadena.
        /// </summary>
        /// <value>La versión de MCART como una cadena.</value>
        public static string VersionString => $"{RTAssembly.FullName} {RTAssembly.GetName().Version}";
    }
}