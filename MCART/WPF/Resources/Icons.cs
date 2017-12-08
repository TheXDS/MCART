//
//  Icons.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MCART.Resources
{
    /// <summary>
    /// Contiene íconos y otras imágenes para utilizar en aplicaciones de
    /// Windows Presentation Framework.
    /// </summary>
    public static partial class Icons
    {
        const string u = "pack://application:,,,/MCART;component/Resources/Icons/{0}.png";
        /// <summary>
        /// Obtiene un ícono de los recursos incrustados.
        /// </summary>
        /// <param name="icon">Ícono que se desea obtener.</param>
        /// <returns>El ícono de recurso incrustado solicitado.</returns>
        public static ImageSource GetIcon(IconID icon) => new BitmapImage(new Uri(string.Format(u, icon.ToString())));
    }
}