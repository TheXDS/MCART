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

using System.Windows.Forms;

namespace MCART.PluginSupport
{
    public partial interface IPlugin
    {
        /// <summary>
        /// Convierte el <see cref="PluginInteractions"/> en un 
        /// <see cref="ToolStripMenuItem"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="ToolStripMenuItem"/> que se puede agregar a la interfaz
        /// gráfica de la aplicación durante la ejecución.
        /// </returns>
        ToolStripMenuItem UIMenu { get; }
        /// <summary>
        /// Convierte el <see cref="PluginInteractions"/> en un 
        /// <see cref="FlowLayoutPanel"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="FlowLayoutPanel"/> que se puede agregar a la interfaz
        /// gráfica de la aplicación durante la ejecución.
        /// </returns>
        /// <typeparam name="T">
        /// Tipo de controles a incluir dentro del panel. Debe heredar
        /// <see cref="ButtonBase"/>.
        /// </typeparam>
        FlowLayoutPanel UIPanel<T>() where T : ButtonBase, new();
    }
}