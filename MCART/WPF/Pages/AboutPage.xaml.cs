/*
AboutPage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.MCART.Component;
using TheXDS.MCART.Dialogs;
using TheXDS.MCART.Dialogs.ViewModel;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Pages
{
    /// <inheritdoc cref="Page"/>
    /// <summary>
    /// Lógica de interacción para AboutPage.xaml
    /// </summary>
    public partial class AboutPage
    {
        private AboutPageViewModel Vm => DataContext as AboutPageViewModel;
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Pages.AboutPage" />.
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();
            DataContext = new AboutPageViewModel();
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Pages.AboutPage" />.
        /// </summary>
        ///     <param name="element">Ensamblado del cual se desea mostrar la
        ///     información.
        /// </param>
        public AboutPage(Assembly element):this(new AssemblyDataExposer(element, true))
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Pages.AboutPage" />.
        /// </summary>
        /// <param name="element">
        ///     <see cref="T:TheXDS.MCART.Component.IExposeInfo" /> a utilizar para exponer la
        ///     información a mostrar.
        /// </param>
        public AboutPage(IExposeInfo element):this()
        {
            Vm.Element = element;
        }

        private void BtnLicense_OnClick(object sender, RoutedEventArgs e)
        {
            var w = new Window
            {
                SizeToContent = SizeToContent.Width,
                MaxWidth = 640,
                MaxHeight = 480,
                Content = new ScrollViewer
                {
                    Content = new TextBox
                    {
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        FontFamily = new FontFamily("Consolas"),
                        Text=Vm.License,
                        AcceptsReturn=true,
                        IsReadOnly = true,
                        TextWrapping = TextWrapping.WrapWithOverflow
                    }
                }
            };
            w.ShowDialog();
        }

        private void BtnAboutMCART_OnClick(object sender, RoutedEventArgs e)
        {
            RTInfo.Show();
        }

        private void BtnPluginInfo_OnClick(object sender, RoutedEventArgs e)
        {
            new PluginBrowser().ShowDialog();
        }
    }
}