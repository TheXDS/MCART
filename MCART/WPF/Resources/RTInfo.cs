/*
RTInfo.cs

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

using System.Windows;
using TheXDS.MCART.Component;
using TheXDS.MCART.Dialogs;
using TheXDS.MCART.Pages;

namespace TheXDS.MCART.Resources
{
    public static partial class RTInfo
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
        public static bool? RTSupport(Application app) => RTSupport<Application>(app);

        /// <summary>
        ///     Obtiene un <see cref="IExposeInfo"/> que brinda acceso a la
        ///     información básica de identificación de MCART.
        /// </summary>
        /// <returns>
        ///     Un <see cref="IExposeInfo"/> con la información de MCART.
        /// </returns>
        public static IExposeInfo GetInfo()=>new AssemblyDataExposer(RTAssembly, Icons.GetXamlIcon(Icons.IconId.MCART));

        /// <summary>
        ///     Muestra la información de identificación de MCART.
        /// </summary>
        public static void Show()
        {
            AboutBox.ShowDialog(GetInfo());
        }
    }
}