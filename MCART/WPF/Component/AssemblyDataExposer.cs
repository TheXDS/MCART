/*
AssemblyDataExposer.cs

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

namespace TheXDS.MCART.Component
{
    public partial class AssemblyDataExposer
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Component.AssemblyDataExposer" />
        /// </summary>
        /// <param name="assembly">
        ///     Ensamblado del cual se mostrará la información.
        /// </param>
        /// <param name="inferIcon"></param>
        public AssemblyDataExposer(Assembly assembly, bool inferIcon):this(assembly)
        {
            if (!inferIcon)
            {
                Icon = null;
                return;
            }
            try
            {
                var uri = new UriBuilder(Assembly.CodeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                using (var sysicon = System.Drawing.Icon.ExtractAssociatedIcon(path) ?? throw new Exception())
                {
                    Icon = new Image
                    {
                        Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
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

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un ícono opcional a mostrar que describe al elemento.
        /// </summary>
        public UIElement Icon { get; }
    }
}