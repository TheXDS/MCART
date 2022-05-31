/*
AboutBox.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
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
        public static void Show(IExposeExtendedGuiInfo<UIElement> appInfo)
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
        public static void ShowDialog(IExposeExtendedGuiInfo<UIElement?> appInfo)
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
