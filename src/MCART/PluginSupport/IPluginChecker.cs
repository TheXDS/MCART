/*
IPluginChecker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Reflection;

namespace TheXDS.MCART.PluginSupport
{
    /// <summary>
    /// Define una serie de métodos y propiedades a implementar por una clase
    /// que pueda utilizarse para verificar la validez de los 
    /// <see cref="IPlugin"/> que se intenten cargar.
    /// </summary>
    public interface IPluginChecker
    {
        /// <summary>
        /// Comprueba si un ensamblado contiene clases cargables como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el ensamblado contiene clases cargables como
        /// <see cref="IPlugin"/>, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a comprobar.</param>
        bool IsVaild(Assembly assembly);
        /// <summary>
        /// Comprueba si un tipo es una clase cargable como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el tipo contiene clases cargables como
        /// <see cref="IPlugin"/>, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="type"><see cref="Type"/> a comprobar.</param>
        bool IsValid(Type type);
        /// <summary>
        /// Comprueba si un ensamblado contiene un <see cref="IPlugin"/> del
        /// tipo especificado.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el ensamblado contiene al menos una clase de tipo
        /// <typeparamref name="T"/> cargable como <see cref="IPlugin"/>, 
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a comprobar.</param>
        /// <typeparam name="T">Tipo a buscar.</typeparam>
        bool Has<T>(Assembly assembly);
        /// <summary>
        /// Comprueba que el tipo cargado sea compatible con esta versión de
        /// MCART.
        /// </summary>
        /// <param name="type">Tipo a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si el tipo es compatible con esta versión de MCART,
        /// <see langword="false"/> en caso de no ser compatible, o <see langword="null"/> si no fue
        /// posible comprobar la compatibilidad.
        /// </returns>
        bool? IsCompatible(Type type);        
    }    
}