//
//  UI.cs
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

using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using MCART.Attributes;
using St = MCART.Resources.Strings;

namespace MCART
{
    /// <summary>
    /// Extensiones de UI para Gtk.
    /// </summary>
    public static class UI
    {
        /// <summary>
        /// Agrega los <see cref="PluginSupport.InteractionItem"/> de cada
        /// <see cref="PluginSupport.IPlugin"/> de una colección a un 
        /// <see cref="Menu"/>.   
        /// </summary>
        /// <param name="pl">
        /// Colección de <see cref="PluginSupport.IPlugin"/> a procesar.
        /// </param>
        /// <param name="menu">Menú de destino para los
        /// <see cref="PluginSupport.InteractionItem"/> de cada
        /// <see cref="PluginSupport.IPlugin"/>.</param>
        public static void AddPlugins(this Menu menu, IEnumerable<PluginSupport.IPlugin> pl)
        {
            foreach (PluginSupport.IPlugin j in pl)
            {
                if (j.HasInteractions)
                {
                    MenuItem thisPlg = new MenuItem(j.Name) { Visible = true };
                    Menu thsplg = new Menu { Visible = true };
                    thisPlg.Submenu = thsplg;
                    foreach (var k in j.PluginInteractions)
                        thsplg.Append(k.AsMenuItem());
                    menu.Append(thisPlg);
                }
                else
                {
                    Console.WriteLine(St.Warn(St.PluginHasNoInters(j.Name)));
                }
            }
        }

        /// <summary>
        /// Elimina a todos los <see cref="Widget"/> hijos de este
        /// <see cref="Container"/>.
        /// </summary>
        /// <param name="c"><see cref="Container"/> a limpiar.</param>
        public static void ClearContents(this Container c)
        {
            Widget[] x = c.Children;
            foreach (Widget j in x)
            {
                c.Remove(j);
                j.Dispose();
            }
            x = null;
        }

        /// <summary>
        /// Oculta los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        public static void HideWidgets(params Widget[] wdgts)
        {
            foreach (Widget j in wdgts) j.Visible = false;
        }

        /// <summary>
        /// Oculta los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        [Thunk]
        public static void HideWidgets(this IEnumerable<Widget> wdgts)
            => HideWidgets(wdgts.ToArray());

        /// <summary>
        /// Muestra los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        public static void ShowWidgets(params Widget[] wdgts)
        {
            foreach (Widget j in wdgts) j.Visible = true;
        }

        /// <summary>
        /// Muestra los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        [Thunk]
        public static void ShowWidgets(this IEnumerable<Widget> wdgts)
            => ShowWidgets(wdgts.ToArray());

        /// <summary>
        /// Habilita  los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        public static void EnableWidgets(params Widget[] wdgts)
        {
            foreach (Widget j in wdgts) j.Sensitive = true;
        }

        /// <summary>
        /// Habilita  los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        [Thunk]
        public static void EnableWidgets(this IEnumerable<Widget> wdgts)
            => EnableWidgets(wdgts.ToArray());

        /// <summary>
        /// Deshabilita  los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        public static void DisableWidgets(params Widget[] wdgts)
        {
            foreach (Widget j in wdgts) j.Sensitive = false;
        }

        /// <summary>
        /// Deshabilita  los <see cref="Widget"/> especificados.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        [Thunk]
        public static void DisableWidgets(this IEnumerable<Widget> wdgts)
            => DisableWidgets(wdgts.ToArray());

        /// <summary>
        /// Habilita o deshabilita los <see cref="Widget"/> según su estado
        /// previo.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        public static void ToggleWidgets(params Widget[] wdgts)
        {
            foreach (Widget j in wdgts) j.Sensitive = !j.Sensitive;
        }

        /// <summary>
        /// Habilita o deshabilita los <see cref="Widget"/> según su estado
        /// previo.
        /// </summary>
        /// <param name="wdgts">
        /// Colección de <see cref="Widget"/> a procesar.
        /// </param>
        [Thunk]
        public static void ToggleWidgets(this IEnumerable<Widget> wdgts)
            => ToggleWidgets(wdgts.ToArray());
    }
}