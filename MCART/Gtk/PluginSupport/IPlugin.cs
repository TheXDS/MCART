//
//  IPlugin.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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

using Gtk;

namespace MCART.PluginSupport
{
    public partial interface IPlugin
    {
        /// <summary>
        /// Convierte el <see cref="PluginInteractions"/> en un 
        /// <see cref="Menu"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="Menu"/> que se puede agregar a la interfaz gráfica de
        /// la aplicación durante la ejecución.
        /// </returns>
        MenuItem UIMenu { get; }
    }
}