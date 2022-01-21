/*
Margins.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

using System.Runtime.InteropServices;

namespace TheXDS.MCART.PInvoke.Structs
{
    /// <summary>
    /// Define una serie de márgenes aplicables a ventanas de Microsoft Windows.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        /// <summary>
        /// Describe el margen izquierdo.
        /// </summary>
        public int Left;

        /// <summary>
        /// Describe el margen derecho.
        /// </summary>
        public int Right;

        /// <summary>
        /// Describe el margen superior.
        /// </summary>
        public int Top;

        /// <summary>
        /// Describe el margen inferior.
        /// </summary>
        public int Bottom;
    }
}