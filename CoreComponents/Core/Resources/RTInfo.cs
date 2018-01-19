//
//  RTInfo.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Reflection;
using St = TheXDS.MCART.Resources.Strings;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene métodos con funciones de identificación en información del 
    /// ensamblado de MCART.
    /// </summary>
    public static class RTInfo
    {
        /// <summary>
        /// Obtiene la versión del ensamblado de MCART.
        /// </summary>
        /// <returns></returns>
        public static Version RTVersion => RTAssembly.GetName().Version;
        /// <summary>
        /// Obtiene la referencia del ensamblado de MCART
        /// </summary>
        /// <returns>The ssembly.</returns>
        public static Assembly RTAssembly => typeof(RTInfo).Assembly;
        /// <summary>
        /// Comprueba si el ensamblado es compatible con esta versión de MCART
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el ensamblado es compatible con esta versión de
        /// MCART, <see langword="false"/> si no lo es, y <c>null</c> si no se ha podido
        /// determinar la compatibilidad.
        /// </returns>
        /// <param name="asmbly">Ensamblado a comprobar.</param>
        public static bool? RTSupport(Assembly asmbly)
        {
            var minv = asmbly.GetAttr<MinMCARTVersionAttribute>();
            var tgtv = asmbly.GetAttr<TargetMCARTVersionAttribute>();
            if (Objects.IsAnyNull(minv, tgtv)) return null;
            Version vr = RTAssembly.GetName().Version;
            return vr.IsBetween(minv.Value, tgtv.Value);
        }
        /// <summary>
        /// Comprueba si el plugin es compatible con esta versión de MCART
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el plugin es compatible con esta versión de MCART,
        /// <see langword="false"/> si no lo es, y <c>null</c> si no se ha podido
        /// determinar la compatibilidad.
        /// </returns>
        /// <typeparam name="T">
        /// Clase del <see cref="PluginSupport.IPlugin"/> a comprobar.
        /// </typeparam>
        public static bool? RTSupport<T>() where T : PluginSupport.IPlugin
        {
            var minv = typeof(T).GetAttr<MinMCARTVersionAttribute>();
            var tgtv = typeof(T).GetAttr<TargetMCARTVersionAttribute>();
            if (Objects.IsAnyNull(minv, tgtv)) return null;
            Version vr = RTAssembly.GetName().Version;
            return vr.IsBetween(minv.Value, tgtv.Value);
        }
        /// <summary>
        /// Obtiene la ruta de los archivos de ayuda.
        /// </summary>
        /// <value>La ruta de los archivos de ayuda.</value>
        public static string HelpPath => RTAssembly.Location + St.HelpDir;
        /// <summary>
        /// Obtiene la versión de MCART como una cadena.
        /// </summary>
        /// <value>La versión de MCART como una cadena.</value>
        public static string VersionString => string.Format($"MCART {RTAssembly.GetName().Version}");
    }
}