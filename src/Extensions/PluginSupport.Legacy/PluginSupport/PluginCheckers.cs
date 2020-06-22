/*
PluginCheckers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;
using System;
using System.Collections.Generic;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.PluginSupport.Legacy
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
        /// <see langword="true" /> si el tipo es compatible con esta versión de MCART,
        /// <see langword="false" /> en caso de no ser compatible, o <see langword="null" /> si no fue
        /// posible comprobar la compatibilidad.
        /// </returns>
        public override bool? IsCompatible(Type type) => RtInfo.RtSupport(type);

        /// <summary>
        /// Determina si un tipo es válido para ser cargado como un
        /// <see cref="IPlugin" />.
        /// </summary>
        /// <returns>
        /// <see langword="true" />, si el tipo puede ser cagado como un 
        /// <see cref="Plugin" />, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="type">Tipo a comprobar.</param>
        public override bool IsValid(Type type)
        {
            return typeof(IPlugin).IsAssignableFrom(type) && !(
                type.IsInterface
                || type.IsAbstract
                || type.HasAttr<UnusableAttribute>(out _)
                || type.HasAttr<NotPluginAttribute>(out _));
        }
    }

    /// <summary>
    /// <see cref="PluginChecker" /> con reglas de compatibilidad estándard.
    /// </summary>
    public class DefaultPluginChecker : StrictPluginChecker
    {
        /// <summary>
        /// Comprueba que el tipo cargado sea compatible con esta versión de
        /// MCART.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si el tipo es compatible con esta versión de
        /// MCART o si el plugin no incluye información de compatibilidad,
        /// <see langword="false" /> en caso de no ser compatible.
        /// </returns>
        public override bool? IsCompatible(Type type) => base.IsCompatible(type) ?? true;
    }

    /// <summary>
    /// <see cref="PluginChecker" /> con reglas de compatibilidad relajadas.
    /// </summary>
    public class RelaxedPluginChecker : PluginChecker
    {
        /// <summary>
        /// Siempre devuelve <see langword="true" /> al comprobar la compatibilidad de un
        /// tipo con esta versión de MCART.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <returns>Esta función siempre devuelve <see langword="true" />.</returns>
        [Dangerous] public override bool? IsCompatible(Type type) => true;

        /// <summary>
        /// Determina si un tipo es válido para ser cargado como un
        /// <see cref="IPlugin" />.
        /// </summary>
        /// <returns>
        /// <see langword="true" />, si el tipo puede ser cagado como un 
        /// <see cref="Plugin" />, <see langword="false" /> en caso contrario.
        /// </returns>
        /// <param name="type">Tipo a comprobar.</param>
        public override bool IsValid(Type type) => type.Implements<IPlugin>() && type.IsInstantiable((IEnumerable<Type>?)null);
    }
}