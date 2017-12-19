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

namespace TestAppGtk
{

    /// <summary>
    /// Ventana principal de la aplicación.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Construcción de ventana
        /// <summary>
        /// Crea una nueva instancia de la clase <see cref="MainWindow"/>.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="MainWindow"/>.
        /// </returns>
        public static MainWindow Create()
        {
            var t = typeof(MainWindow);
            Builder builder = new Builder(null, $"{t.FullName}.glade", null);
            return new MainWindow(builder, builder.GetObject(t.Name).Handle);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="MainWindow"/>.
        /// </summary>
        /// <param name="builder">Builder.</param>
        /// <param name="handle">Handle.</param>
        public MainWindow(Builder builder, IntPtr handle) : base(handle)
        {
            builder.Autoconnect(this);

            // Cablear eventos...
            DeleteEvent += OnDeleteEvent;
            mnuScreenshot.Activated += MnuScreenshot_Activated;
            mnuExit.Activated += MnuExit_Activated;
            mnuProgressRing.Activated += MnuProgressRing_Activated;

            mnuAboutMCART.Activated += MnuAboutMCART_Activated;

            // Cargar plugins...
            if (pl.Count > 0)
            {
                Menu mnuPlg = new Menu();
                mnuPlugins.Submenu = mnuPlg;
                //mnuPlg.ClearContents();
                mnuPlg.AddPlugins(pl);
            }
            ShowAll();
        }
        #endregion

        #region Widgets
#pragma warning disable CS0649
        [Builder.Object] ImageMenuItem mnuScreenshot;
        [Builder.Object] ImageMenuItem mnuExit;
        [Builder.Object] MenuItem mnuProgressRing;
        [Builder.Object] MenuItem mnuPwd;
        [Builder.Object] MenuItem mnuSetPwd;
        [Builder.Object] MenuItem mnuPwGenPin;
        [Builder.Object] MenuItem mnuPwGenSafe;
        [Builder.Object] MenuItem mnuPwGenComplex;
        [Builder.Object] MenuItem mnuPwGenExtreme;
        [Builder.Object] MenuItem mnuGrphRing;
        [Builder.Object] MenuItem mnuPlugins;
        [Builder.Object] ImageMenuItem mnuAboutMCART;
#pragma warning restore CS0649
        #endregion




        List<IPlugin> pl = Plugin.LoadEverything<IPlugin>();

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

        void MnuScreenshot_Activated(object sender, EventArgs e)
        {

        }

        void MnuExit_Activated(object sender, EventArgs e) => Destroy();

        void MnuProgressRing_Activated(object sender, EventArgs e)
        {

        }

        void MnuAboutMCART_Activated(object sender, EventArgs e)
        {
            PluginBrowser.Create().Show();
        }
    }
}