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
using MCART.Exceptions;
namespace MCART.PluginSupport
{
    public abstract partial class Plugin : IPlugin
    {
        /// <summary>
        /// Genera un <see cref="MenuItem"/> a partir del <see cref="IPlugin"/>. 
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
            MenuItem mnu = new MenuItem(plugin.Name) { TooltipText = plugin.Description };
            Menu subMnu = new Menu();
            mnu.Submenu = subMnu;
            foreach (InteractionItem j in plugin.PluginInteractions)
            {
                MenuItem k = new MenuItem(j.Text) { TooltipText = j.Description };
                //TODO: cargar ícono
                k.Activated += j.Action;
                subMnu.Append(k);
            }
            return mnu;
        }
        /// <summary>
        /// Convierte el <see cref="IPlugin.PluginInteractions"/> en un
        /// <see cref="MenuItem"/>.
        /// </summary>
        /// <value>
        /// Un <see cref="MenuItem"/> que puede agregarse a un
        /// <see cref="Menu"/> de Gtk.
        /// </value>
        public MenuItem UIMenu => GetUIMenu(this);
    }
}