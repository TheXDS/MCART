//
//  IPluginChecker.cs
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

using System;
using System.Reflection;

namespace MCART.PluginSupport
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
        /// <c>true</c> si el ensamblado contiene clases cargables como
        /// <see cref="IPlugin"/>, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a comprobar.</param>
        bool IsVaild(Assembly assembly);
        /// <summary>
        /// Comprueba si un tipo es una clase cargable como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el tipo contiene clases cargables como
        /// <see cref="IPlugin"/>, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="type"><see cref="Type"/> a comprobar.</param>
        bool IsVaild(Type type);
        /// <summary>
        /// Comprueba si un ensamblado contiene un <see cref="IPlugin"/> del
        /// tipo especificado.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el ensamblado contiene al menos una clase de tipo
        /// <typeparamref name="T"/> cargable como <see cref="IPlugin"/>, 
        /// <c>false</c> en caso contrario.
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
        /// <c>true</c> si el tipo es compatible con esta versión de MCART,
        /// <c>false</c> en caso de no ser compatible, o <c>null</c> si no fue
        /// posible comprobar la compatibilidad.
        /// </returns>
        bool? IsCompatible(Type type);        
    }    
}