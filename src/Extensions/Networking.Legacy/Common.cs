/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene definiciones misceláneas y compartidas en el espacio de
nombres TheXDS.MCART.Networking.

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

namespace TheXDS.MCART.Networking.Legacy
{
    /// <summary>
    /// Contiene definiciones y objetos predeterminados a utilizar en el
    /// espacio de nombres <see cref="Networking"/>.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Puerto predeterminado para todos los objetos de red.
        /// </summary>
        public const int DefaultPort = 51200;

        /// <summary>
        /// Tiempo de espera en milisegundos antes de realizar una
        /// desconexión forzada.
        /// </summary>
        public const int DisconnectionTimeout = 15000;
    }
}