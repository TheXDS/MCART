//
//  PluginCheckers.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MCART.Attributes;
using MCART.Resources;
using System;

namespace MCART.PluginSupport
{
    /// <summary>
    /// <see cref="PluginChecker"/> con reglas de compatibilidad estrictas.
    /// </summary>
    public class StrictPluginChecker : PluginChecker
    {
        /// <summary>
        /// Comprueba que el tipo cargado sea compatible con esta versión de
        /// MCART.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <returns>
        /// <c>true</c> si el tipo es compatible con esta versión de MCART,
        /// <c>false</c> en caso de no ser compatible, o <c>null</c> si no fue
        /// posible comprobar la compatibilidad.
        /// </returns>
        public override bool? IsCompatible(Type type)
        {
            if (!(type.HasAttr(out TargetMCARTVersionAttribute tt) || type.Assembly.HasAttr(out tt))) return null;
            if (!(type.HasAttr(out MinMCARTVersionAttribute mt) || type.Assembly.HasAttr(out mt)))
#if StrictMCARTVersioning
                return null;
#else
                mt=tt;
#endif
            return RTInfo.RTAssembly.GetName().Version.IsBetween(mt?.Value, tt?.Value);
        }
        /// <summary>
        /// Determina si un tipo es válido para ser cargado como un
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si el tipo puede ser cagado como un 
        /// <see cref="Plugin"/>, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="type">Tipo a comprobar.</param>
        public override bool IsVaild(Type type)
        {
            return typeof(IPlugin).IsAssignableFrom(type) && !(
                type.IsInterface
                || type.IsAbstract
                || type.HasAttr<UnusableAttribute>(out _)
                || type.HasAttr<NotPluginAttribute>(out _));
        }
    }
    /// <summary>
    /// <see cref="PluginChecker"/> con reglas de compatibilidad relajadas.
    /// </summary>
    [Dangerous] public class RelaxedPluginChecker : PluginChecker
    {
        /// <summary>
        /// Siempre devuelve <c>true</c> al comprobar la compatibilidad de un
        /// tipo con esta versión de MCART.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <returns>Esta función siempre devuelve <c>true</c>.</returns>
        [Dangerous] public override bool? IsCompatible(Type type) => true;
        /// <summary>
        /// Determina si un tipo es válido para ser cargado como un
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si el tipo puede ser cagado como un 
        /// <see cref="Plugin"/>, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="type">Tipo a comprobar.</param>
        public override bool IsVaild(Type type) => !(type.IsInterface || type.IsAbstract) && typeof(IPlugin).IsAssignableFrom(type);
    }
}