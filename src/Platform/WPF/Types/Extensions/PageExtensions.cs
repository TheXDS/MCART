/*
PageExtensions.cs

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

using System.Windows.Controls;
using System.Windows.Navigation;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de la clase <see cref="Page"/>.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Envuelve un <see cref="Page"/> dentro de un <see cref="Frame"/> que
        /// permite utilizarlo como un <see cref="Control"/>.
        /// </summary>
        /// <param name="page">Página a envolver.</param>
        /// <returns>
        /// Un <see cref="Frame"/> que ha navegado a la página solicitada.
        /// </returns>
        public static Frame InFrame(this Page page)
        {
            var f = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };
            f.Navigate(page);
            return f;
        }
    }
}
