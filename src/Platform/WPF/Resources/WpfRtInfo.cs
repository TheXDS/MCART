/*
WpfRtInfo.cs

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

using System.Windows;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Pages;

[assembly:McartComponent(TheXDS.MCART.Resources.RtInfo.ComponentKind.PlatformTarget)]

namespace TheXDS.MCART.Resources
{
    /// <summary>
    ///     Expone los detalles de MCART para WPF.
    /// </summary>
    public class WpfRtInfo : RtInfo
    {
        /// <summary>
        /// Comprueba si la aplicación es compatible con esta versión de
        /// <see cref="MCART"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si la aplicación es compatible con esta
        /// versión de <see cref="MCART"/>, <see langword="false"/> si no lo
        /// es, y <see langword="null"/> si no se ha podido determinar la
        /// compatibilidad.
        /// </returns>
        /// <param name="app"><see cref="Application"/> a comprobar.</param>
        public static bool? RtSupport(Application app) => RtSupport(app);

        /// <summary>
        ///     Muestra la información de identificación de MCART.
        /// </summary>
        public static void Show()
        {
            var w = new Window {
                Content = new AboutPage(typeof(WpfRtInfo).Assembly)
            };

            //AboutBox.ShowDialog(GetInfo());
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Resources.WpfRtInfo" />.
        /// </summary>
        public WpfRtInfo() : base(typeof(WpfRtInfo).Assembly) { }
    }
}