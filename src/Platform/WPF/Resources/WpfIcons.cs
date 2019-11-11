/*
WpfIcons.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene íconos y otras imágenes para utilizar en aplicaciones de
    /// Windows Presentation Framework.
    /// </summary>
    public sealed class WpfIcons : McartIconLibrary<UIElement>
    {
        private static readonly XamlUnpacker _xaml = new XamlUnpacker(typeof(WpfIcons).Assembly, typeof(Icons).FullName!);
        
        /// <summary>
        ///     Implementa el método de obtención del ícono basado en el nombre
        ///     del ícono solicitado.
        /// </summary>
        /// <param name="id">
        ///     Id del ícono solicitado.
        /// </param>
        /// <returns>
        ///     El ícono solicitado.
        /// </returns>
        protected override sealed UIElement GetIcon([CallerMemberName] string? id = null!)
        {
            return _xaml.Unpack($"{id}_Xml", new DeflateGetter()) as UIElement ?? new TextBlock { Text="?", FontSize = 256, Foreground = Brushes.DarkRed };
        }

    }
}