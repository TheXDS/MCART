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
using MCART.Forms;
using MCART.PluginSupport;

public partial class MainWindow : Window
{
    List<IPlugin> pl = Plugin.LoadEverything<IPlugin>();
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        if (pl.Count > 0)
        {
            Menu mnuplugins = new Menu();
            ((MenuItem)PluginsAction.Proxies[0]).Submenu = mnuplugins;
            foreach (IPlugin j in pl)
            {
                if (j.HasInteractions)
                {
                    MenuItem thisPlg = new MenuItem(j.Name);
                    Menu thsplg = new Menu();
                    thisPlg.Submenu = thsplg;
                    foreach (var k in j.PluginInteractions)
                        thsplg.Append(k.AsMenuItem());
                    mnuplugins.Append(thisPlg);
                }
            }
        }
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnSalirActionActivated(object sender, EventArgs e)
    {
        Destroy();
    }

    protected void OnInfoDePluginsAction1Activated(object sender, EventArgs e)
    {
        (new PluginBrowser()).Show();
    }
}
