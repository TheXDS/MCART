/*
WpfSizeExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using W = System.Windows;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de la clase <see cref="Size"/> para WPF.
    /// </summary>
    public static class WpfSizeExtensions
    {
        /// <summary>
        /// Convierte un <see cref="Size"/> en un
        /// <see cref="W.Size"/>.
        /// </summary>
        /// <param name="size">
        /// <see cref="Size"/> a convertir.
        /// </param>
        /// <returns>
        /// Un nuevo <see cref="W.Size"/> creado a partir del
        /// <see cref="Size"/> especificado.
        /// </returns>
        public static W.Size ToWinSize(this Size size)
        {
            return new W.Size(size.Width, size.Height);
        }

        /// <summary>
        /// Convierte un <see cref="W.Size"/> en un
        /// <see cref="Size"/>.
        /// </summary>
        /// <param name="size">
        /// <see cref="W.Size"/> a convertir.
        /// </param>
        /// <returns>
        /// Un nuevo <see cref="Size"/> creado a partir del
        /// <see cref="W.Size"/> especificado.
        /// </returns>
        public static Size FromWinSize(this W.Size size)
        {
            return new Size(size.Width, size.Height);
        }
    }
}