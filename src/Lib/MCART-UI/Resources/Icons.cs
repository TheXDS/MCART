/*
Icons.cs

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

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene íconos y otras imágenes para utilizar en cualquier aplicación.
    /// </summary>
    public static partial class Icons
    {
        /// <summary>
        /// Determina el ícono a obtener.
        /// </summary>
        public enum IconId
        {
            /// <summary>
            /// Ícono principal de MCART.
            /// </summary>
            MCART,
            /// <summary>
            /// Etiqueta de MCART.
            /// </summary>
            MCARTBadge,
            /// <summary>
            /// Ícono de plugin de MCART.
            /// </summary>
            MCARTPlugin
        }
    }
}