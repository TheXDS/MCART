//
//  Plugin.cs
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

using MCART.Exceptions;
using System.Windows.Forms;

namespace MCART.PluginSupport
{
    public abstract partial class Plugin : IPlugin
    {
        /// <summary>
        /// Genera un <see cref="ToolStripMenuItem"/> a partir de las 
        /// interacciones del <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="ToolStripMenuItem"/> que contiene las interacciones
        /// contenidas por el <see cref="IPlugin"/>, en la propiedad
        /// <see cref="IPlugin.PluginInteractions"/>.
        /// </returns>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> a partir del cual se generará el
        /// <see cref="ToolStripMenuItem"/>.
        /// </param>
        public static ToolStripMenuItem GetUIMenu(IPlugin plugin)
        {
            if (!plugin.HasInteractions) throw new FeatureNotAvailableException();
            ToolStripMenuItem mnu = new ToolStripMenuItem(plugin.Name) { ToolTipText = plugin.Description };
            foreach (InteractionItem j in plugin.PluginInteractions)
                mnu.DropDownItems.Add(j.AsToolStripMenuItem());
            return mnu;
        }
        /// <summary>
        /// Obtiene la Interfaz de interacción de un <see cref="IPlugin"/> como
        /// un <see cref="FlowLayoutPanel"/> cuyas acciones son controles de
        /// tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de controles a generar. Se deben utilizar controles que se
        /// deriven de <see cref="ButtonBase"/>.
        /// </typeparam>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> a partir del cual se generará el panel.
        /// </param>
        /// <returns>
        /// Un <see cref="FlowLayoutPanel"/> que contiene las interacciones
        /// contenidas por el <see cref="IPlugin"/>, en la propiedad
        /// <see cref="IPlugin.PluginInteractions"/>.
        /// </returns>
        public static FlowLayoutPanel GetUIPanel<T>(IPlugin plugin) where T : ButtonBase, new()
        {
            FlowLayoutPanel pnl = new FlowLayoutPanel();
            foreach(InteractionItem j in plugin.PluginInteractions)
            {
                T a = j.AsButton<T>();                
                try { a.Click += j.Action; }
                catch { throw; }
                finally { pnl.Controls.Add(a); }                
            }
            return pnl;
        }
        /// <summary>
        /// Convierte el <see cref="IPlugin.PluginInteractions"/> en un
        /// <see cref="ToolStripMenuItem"/>.
        /// </summary>
        /// <value>
        /// Un <see cref="ToolStripMenuItem"/> que puede agregarse a un
        /// <see cref="Menu"/> de Windows Presentation Framework.
        /// </value>
        public ToolStripMenuItem UIMenu => GetUIMenu(this);
        /// <summary>
        /// Convierte el <see cref="IPlugin.PluginInteractions"/> en un
        /// <see cref="Panel"/>, especificando el tipo de controles a
        /// contener para cada <see cref="InteractionItem"/>.
        /// </summary>
        /// <value>
        /// Un <see cref="Panel"/> que puede agregarse a un
        /// <see cref="Menu"/> de Windows Presentation Framework.
        /// </value>
        public FlowLayoutPanel UIPanel<T>() where T : ButtonBase, new()
            => GetUIPanel<T>(this);
    }
}