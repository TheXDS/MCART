/*
AboutPage.cs

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
        public AboutPage(ApplicationInfo element) : this()
        {
            Vm.Element = element;
        }
    }
}