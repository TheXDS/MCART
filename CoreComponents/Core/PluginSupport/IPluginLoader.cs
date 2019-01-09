/*
IPluginLoader.cs

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

using TheXDS.MCART.Exceptions;
using System.Collections.Generic;
using System.Reflection;

namespace TheXDS.MCART.PluginSupport
{
    /// <summary>
    /// Define una serie de métodos y propiedades a implementar por una clase
    /// que pueda utilizarse para cargar clases que implementen la interfaz 
    /// <see cref="IPlugin"/>.
    /// </summary>
    public interface IPluginLoader
    {
        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T"/> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a cargar.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        T Load<T>(Assembly assembly) where T : class;
        /// <summary>
        /// Carga todos los <see cref="IPlugin"/> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}"/> con los <see cref="IPlugin"/>
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a cargar.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly"/> no contiene clases cargables
        /// como <see cref="IPlugin"/>. 
        /// </exception>
        IEnumerable<T> LoadAll<T>(Assembly assembly) where T : class;
        /// <summary>
        /// Carga todos los <see cref="IPlugin"/> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}"/> con los <see cref="IPlugin"/>
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a cargar.</param>
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly"/> no contiene clases cargables
        /// como <see cref="IPlugin"/>. 
        /// </exception>
        IEnumerable<IPlugin> LoadAll(Assembly assembly);
    }
}