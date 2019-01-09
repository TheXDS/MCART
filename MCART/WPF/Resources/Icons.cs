/*
Icons.cs

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

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene íconos y otras imágenes para utilizar en aplicaciones de
    /// Windows Presentation Framework.
    /// </summary>
    public static partial class Icons
    {
        private const string DefaultExt = "png";

        private static readonly ImageUnpacker imgs = new ImageUnpacker(RTInfo.RTAssembly, typeof(Icons).FullName);
        private static readonly StringUnpacker strs = new StringUnpacker(RTInfo.RTAssembly, typeof(Icons).FullName);

        /// <summary>
        ///     Obtiene un ícono desde los recursos incrustados del ensamblado
        ///     de MCART.
        /// </summary>
        /// <param name="icon">Ícono que se desea obtener.</param>
        /// <returns>El ícono de recurso incrustado solicitado.</returns>
        public static ImageSource GetIcon(IconId icon) => imgs.Unpack(
            $"{icon.ToString()}.{(icon.HasAttr<IdentifierAttribute>(out var attr) ? attr.Value : DefaultExt)}");

        /// <summary>
        ///     Obtiene un ícono desde los recursos incrustados del ensamblado
        ///     de MCART.
        /// </summary>
        /// <param name="icon">Ícono que se desea obtener.</param>
        /// <returns>El ícono de recurso incrustado solicitado.</returns>
        public static UIElement GetXamlIcon(IconId icon)
        {
            using (var sr = new StringReader(strs.Unpack($"{icon.ToString()}_Xml", new DeflateGetter())))
            {
                var xx = XmlReader.Create(sr);
                return XamlReader.Load(xx) as UIElement;
            }
        }
    }
}