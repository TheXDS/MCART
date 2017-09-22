//
//  MainWindow.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using Gtk;
using MCART;
using MCART.Forms;
using MCART.PluginSupport;

/// <summary>
/// Ventana principal de la aplicación.
/// </summary>
public partial class MainWindow : Window
{
    List<IPlugin> pl = Plugin.LoadEverything<IPlugin>();
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="MainWindow"/>.
    /// </summary>
    public MainWindow() : base(WindowType.Toplevel)
    {
        Build();
        if (pl.Count > 0)
        {
            // HACK: Ubicación dura del submenú de plugins.
            Menu mnuplugins = (Menu)((ImageMenuItem)mnuMain.Children[2]).Submenu;
            mnuplugins.ClearContents();
            mnuplugins.AddPlugins(pl);
        }
        ShowAll();
    }
    /// <summary>
    /// Finaliza la aplicación al cerrar esta ventana.
    /// </summary>
    /// <param name="sender">Objeto que ha generado el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    protected void OnDeleteEvent(object sender, DeleteEventArgs e)
    {
        Application.Quit();
        e.RetVal = true;
    }
    void OnSalirActionActivated(object sender, EventArgs e) => Destroy();
    void OnInfoDePluginsAction1Activated(object sender, EventArgs e)
    {
        (new PluginBrowser()).Show();
    }
}