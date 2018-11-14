/*
AboutPage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MCART.Types.Base;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using TheXDS.MCART.Dialogs;
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
        public AboutPage(Assembly element):this(new AssemblyDataExposer(element))
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
            throw new NotImplementedException();
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

    internal class AboutPageViewModel : NotifyPropertyChanged
    {
        private IExposeInfo _element;

        public IExposeInfo Element
        {
            get => _element;
            set
            {
                if (Equals(value, _element)) return;
                _element = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Copyright));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Author));
                OnPropertyChanged(nameof(License));
                OnPropertyChanged(nameof(Version));
                OnPropertyChanged(nameof(HasLicense));
                OnPropertyChanged(nameof(IsMcart));
            }
        }

        public string Name => Element.Name;
        public string Copyright => Element.Copyright;
        public string Description => Element.Description;
        public string Author => Element.Author;
        public string License => Element.License;
        public Version Version => Element.Version;
        public bool HasLicense => Element.HasLicense;
        public UIElement Icon => Element.Icon;

        public bool IsMcart => (Element as AssemblyDataExposer)?.Assembly == RTInfo.RTAssembly;
    }

    /// <inheritdoc />
    /// <summary>
    ///     Expone la información de identificación de un ensamblado.
    /// </summary>
    public class AssemblyDataExposer : IExposeInfo
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="AssemblyDataExposer"/>
        /// </summary>
        /// <param name="assembly">
        ///     Ensamblado del cual se mostrará la información.
        /// </param>
        public AssemblyDataExposer(Assembly assembly)
        {
            Assembly = assembly;
            try
            {
                var uri = new UriBuilder(Assembly.CodeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                using (var sysicon = System.Drawing.Icon.ExtractAssociatedIcon(path) ?? throw new Exception())
                {
                    Icon = new Image
                    {
                        Source =  System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                            sysicon.Handle,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions())
                    };
                }
            }
            catch
            {
                Icon = null;
            }
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="AssemblyDataExposer"/>
        /// </summary>
        /// <param name="assembly">
        ///     Ensamblado del cual se mostrará la información.
        /// </param>
        /// <param name="icon">ícono a mostrar.</param>
        public AssemblyDataExposer(Assembly assembly, UIElement icon)
        {
            Assembly = assembly;
            Icon = icon;
        }

        /// <summary>
        ///     Referencia al ensamblado del cual se expone la información.
        /// </summary>
        public readonly Assembly Assembly;

        /// <inheritdoc />
        /// <summary>
        /// Devuelve el nombre del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string Name => Assembly.GetAttr<AssemblyTitleAttribute>()?.Title;

        /// <inheritdoc />
        /// <summary>
        /// Devuelve el Copyright del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string Copyright => Assembly.GetAttr<AssemblyCopyrightAttribute>()?.Copyright;

        /// <inheritdoc />
        /// <summary>
        /// Devuelve una descripción del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string Description => Assembly.GetAttr<AssemblyDescriptionAttribute>()?.Description;

        /// <inheritdoc />
        /// <summary>
        /// Devuelve el autor del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string Author => Assembly.GetAttr<AssemblyCompanyAttribute>()?.Company;

        /// <inheritdoc />
        /// <summary>
        /// Devuelve la licencia del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string License => Assembly.GetAttr<LicenseTextAttribute>()?.Value;

        /// <inheritdoc />
        /// <summary>
        /// Devuelve la versión del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public Version Version => Assembly.GetName().Version;

        /// <inheritdoc />
        /// <summary>
        /// Obtiene un valor que determina si este <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// contiene información de licencia.
        /// </summary>
        public bool HasLicense => Assembly.HasAttr<LicenseTextAttribute>();

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un ícono opcional a mostrar que describe al elemento.
        /// </summary>
        public UIElement Icon { get; }
    }
}