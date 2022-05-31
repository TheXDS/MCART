/*
AboutPage.cs

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
using TheXDS.MCART.Dialogs.ViewModel;

namespace TheXDS.MCART.Pages
{
    /// <summary>
    /// Lógica de interacción para AboutPage.xaml
    /// </summary>
    public partial class AboutPage
    {
        private AboutPageViewModel Vm => (AboutPageViewModel)DataContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="AboutPage" />.
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();
            DataContext = new AboutPageViewModel();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="AboutPage" />.
        /// </summary>
        /// <param name="element">Ensamblado del cual se desea mostrar la
        /// información.
        /// </param>
        public AboutPage(Assembly element) : this(new ApplicationInfo(element, true))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:TheXDS.MCART.Pages.AboutPage" />.
        /// </summary>
        /// <param name="element">
        /// <see cref="IExposeInfo" /> a utilizar para exponer la
        /// información a mostrar.
        /// </param>
        public AboutPage(IExposeExtendedGuiInfo<UIElement?> element) : this()
        {
            Vm.Element = element;
        }
    }
}