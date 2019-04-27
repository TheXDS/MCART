/*
WpfSizeExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones de la clase <see cref="Size"/> para WPF.
    /// </summary>
    public static class WpfSizeExtensions
    {
        /// <summary>
        ///     Convierte un <see cref="Size"/> en un
        ///     <see cref="System.Windows.Size"/>.
        /// </summary>
        /// <param name="size">
        ///     <see cref="Size"/> a convertir.
        /// </param>
        /// <returns>
        ///     Un nuevo <see cref="System.Windows.Size"/> creado a partir del
        ///     <see cref="Size"/> especificado.
        /// </returns>
        public static System.Windows.Size ToWinSize(this Size size)
        {
            return new System.Windows.Size(size.Width, size.Height);
        }

        /// <summary>
        ///     Convierte un <see cref="System.Windows.Size"/> en un
        ///     <see cref="Size"/>.
        /// </summary>
        /// <param name="size">
        ///     <see cref="System.Windows.Size"/> a convertir.
        /// </param>
        /// <returns>
        ///     Un nuevo <see cref="Size"/> creado a partir del
        ///     <see cref="System.Windows.Size"/> especificado.
        /// </returns>

        public static Size FromWinSize(this System.Windows.Size size)
        {
            return new Size(size.Width, size.Height);
        }
    }
}