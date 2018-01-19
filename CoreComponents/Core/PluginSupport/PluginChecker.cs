//
//  PluginChecker.cs
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

using TheXDS.MCART.Resources;
using System;
using System.Linq;
using System.Reflection;

namespace TheXDS.MCART.PluginSupport
{
    /// <summary>
    /// Esta clase realiza diferentes verificaciones de compatibilidad de plugins.
    /// </summary>
    public abstract class PluginChecker : IPluginChecker
    {
        /// <summary>
        /// Comprueba si un ensamblado contiene un plugin del tipo especificado.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el ensamblado contiene al menos una clase de tipo
        /// <typeparamref name="T"/> cargable como <see cref="IPlugin"/>, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a comprobar.</param>
        /// <typeparam name="T">Tipo a buscar.</typeparam>
        public bool Has<T>(Assembly assembly) => assembly.IsNeither(RTInfo.RTAssembly, null) && assembly.GetTypes().Any((arg) => IsVaild(arg) && typeof(T).IsAssignableFrom(arg));
        /// <summary>
        /// Comprueba que el tipo cargado sea compatible con esta versión de
        /// MCART.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el tipo es compatible con esta versión de MCART,
        /// <see langword="false"/> en caso de no ser compatible, o <c>null</c> si no fue
        /// posible comprobar la compatibilidad.
        /// </returns>
        public abstract bool? IsCompatible(Type type);
        /// <summary>
        /// Comprueba si un <see cref="Assembly"/> contiene clases cargables 
        /// como <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el ensamblado contiene clases cargables como
        /// <see cref="IPlugin"/>, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a comprobar.</param>
        public bool IsVaild(Assembly assembly) => assembly.IsNeither(RTInfo.RTAssembly, null) && assembly.GetTypes().Any((arg) => IsVaild(arg));
        /// <summary>
        /// Determina si un tipo es válido para ser cargado como un
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, si el tipo puede ser cagado como un 
        /// <see cref="Plugin"/>, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="type">Tipo a comprobar.</param>
        public abstract bool IsVaild(Type type);
    }
}