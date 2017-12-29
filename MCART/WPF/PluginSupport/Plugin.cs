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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MCART.PluginSupport
{
    public abstract partial class Plugin : IPlugin
    {
        /// <summary>
        /// Genera un <see cref="MenuItem"/> a partir de las interacciones del
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="MenuItem"/> que contiene las interacciones contenidas
        /// por el <see cref="IPlugin"/>, en la propiedad
        /// <see cref="IPlugin.PluginInteractions"/>.
        /// </returns>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> a partir del cual se generará el
        /// <see cref="MenuItem"/>.
        /// </param>
        public static MenuItem GetUIMenu(IPlugin plugin)
        {
            if (!plugin.HasInteractions) throw new FeatureNotAvailableException();
            MenuItem mnu = new MenuItem() { Header = plugin.Name };
            if (!plugin.Description.IsEmpty())
                mnu.ToolTip = new ToolTip() { Content = plugin.Description };
            foreach (InteractionItem j in plugin.PluginInteractions)
                mnu.Items.Add(j.AsMenuItem());
            return mnu;
        }
        /// <summary>
        /// Obtiene la Interfaz de interacción de un <see cref="IPlugin"/> como
        /// un <typeparamref name="PanelT"/> cuyas acciones son controles de
        /// tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de controles a generar. Se deben utilizar controles que se
        /// deriven de <see cref="ButtonBase"/>.
        /// </typeparam>
        /// <typeparam name="PanelT">Tipo de panel a devolver.</typeparam>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> a partir del cual se generará el panel.
        /// </param>
        /// <returns>
        /// Un <typeparamref name="PanelT"/> que contiene las interacciones
        /// contenidas por el <see cref="IPlugin"/>, en la propiedad
        /// <see cref="IPlugin.PluginInteractions"/>.
        /// </returns>
        public static PanelT GetUIPanel<T, PanelT>(IPlugin plugin)
            where T : ButtonBase, new() where PanelT : Panel, new()
        {
            PanelT pnl = new PanelT();
            foreach (InteractionItem j in plugin.PluginInteractions)
                pnl.Children.Add(j.AsButton<T>());
            return pnl;
        }
        /// <summary>
        /// Convierte el <see cref="IPlugin.PluginInteractions"/> en un
        /// <see cref="MenuItem"/>.
        /// </summary>
        /// <value>
        /// Un <see cref="MenuItem"/> que puede agregarse a un
        /// <see cref="Menu"/> de Windows Presentation Framework.
        /// </value>
        public MenuItem UIMenu => GetUIMenu(this);
        /// <summary>
        /// Convierte el <see cref="IPlugin.PluginInteractions"/> en un
        /// <see cref="Panel"/>, especificando el tipo de controles a
        /// contener para cada <see cref="InteractionItem"/>.
        /// </summary>
        /// <value>
        /// Un <see cref="Panel"/> que puede agregarse a un
        /// <see cref="Menu"/> de Windows Presentation Framework.
        /// </value>
        /// <typeparam name="T">
        /// Tipo de controles que serán generados por cada
        /// <see cref="InteractionItem"/> que el <see cref="Plugin"/>
        /// contiene.
        /// </typeparam>
        /// <typeparam name="PanelT">
        /// Tipo de panel que contendrá las interacciones.
        /// </typeparam>
        /// <returns>
        /// Un <typeparamref name="PanelT"/> que contiene las interacciones
        /// contenidas por el <see cref="IPlugin"/>, en la propiedad
        /// <see cref="IPlugin.PluginInteractions"/>.
        /// </returns>
        public PanelT UIPanel<T, PanelT>()
            where T : ButtonBase, new() where PanelT : Panel, new()
            => GetUIPanel<T, PanelT>(this);
    }
}