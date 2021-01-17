/*
AboutBox.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System.Reflection;
using System.Windows;
using TheXDS.MCART.Component;
using TheXDS.MCART.Pages;

namespace TheXDS.MCART.Dialogs
{
    /// <summary>
    /// Diálogo que muestra información sobre un elemento.
    /// </summary>
    public sealed class AboutBox : Window
    {
        private AboutBox(AboutPage page)
        {
            MaxWidth = 500;
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Content = page;
        }

        /// <summary>
        /// Muestra un cuadro de diálogo con información sobre la
        /// aplicación de WPF especificada.
        /// </summary>
        /// <param name="app">
        /// Aplicación sobre la cual mostrar información.
        /// </param>
        public static void Show(Application app)
        {
            Show(app.GetType().Assembly);
        }

        /// <summary>
        /// Muestra un cuadro de diálogo modal con información sobre la
        /// aplicación de WPF especificada.
        /// </summary>
        /// <param name="app">
        /// Aplicación sobre la cual mostrar información.
        /// </param>
        public static void ShowDialog(Application app)
        {
            ShowDialog(app.GetType().Assembly);
        }

        /// <summary>
        /// Muestra un cuadro de diálogo con información sobre el
        /// ensamblado especificado.
        /// </summary>
        /// <param name="assembly">
        /// Ensamblado sobre el cual mostrar información.
        /// </param>
        public static void Show(Assembly assembly)
        {
            new AboutBox(new AboutPage(assembly)).Show();
        }

        /// <summary>
        /// Muestra un cuadro de diálogo modal con información sobre el
        /// ensamblado especificado.
        /// </summary>
        /// <param name="assembly">
        /// Ensamblado sobre el cual mostrar información.
        /// </param>
        public static void ShowDialog(Assembly assembly)
        {
            new AboutBox(new AboutPage(assembly)).ShowDialog();
        }

        /// <summary>
        /// Muestra un cuadro de diálogo con información sobre el
        /// <see cref="ApplicationInfo"/> especificado.
        /// </summary>
        /// <param name="appInfo">
        /// <see cref="ApplicationInfo"/> sobre el cual mostrar
        /// información.
        /// </param>
        public static void Show(ApplicationInfo appInfo)
        {
            new AboutBox(new AboutPage(appInfo)).Show();
        }

        /// <summary>
        /// Muestra un cuadro de diálogo modal con información sobre el
        /// <see cref="ApplicationInfo"/> especificado.
        /// </summary>
        /// <param name="appInfo">
        /// <see cref="ApplicationInfo"/> sobre el cual mostrar
        /// información.
        /// </param>
        public static void ShowDialog(ApplicationInfo appInfo)
        {
            new AboutBox(new AboutPage(appInfo)).ShowDialog();
        }

        /// <summary>
        /// Muestra un cuadro de diálogo con información sobre el
        /// <see cref="IExposeInfo"/> especificado.
        /// </summary>
        /// <param name="info">
        /// <see cref="IExposeInfo"/> sobre el cual mostrar
        /// información.
        /// </param>
        public static void Show(IExposeAssembly info)
        {
            new AboutBox(new AboutPage(info.Assembly)).Show();
        }

        /// <summary>
        /// Muestra un cuadro de diálogo modal con información sobre el
        /// <see cref="IExposeInfo"/> especificado.
        /// </summary>
        /// <param name="info">
        /// <see cref="IExposeInfo"/> sobre el cual mostrar
        /// información.
        /// </param>
        public static void ShowDialog(IExposeAssembly info)
        {
            new AboutBox(new AboutPage(info.Assembly)).ShowDialog();
        }
    }
}
