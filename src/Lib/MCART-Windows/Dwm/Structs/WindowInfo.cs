/*
WindowInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>
     PInvoke.net Community <http://www.pinvoke.net>

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

namespace TheXDS.MCART.Windows.Dwm.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowInfo
    {
        public uint CbSize;
        public Rect RcWindow;
        public Rect RcClient;
        public WindowStyles DwStyle;
        public WindowStyles DwExtendedStyle;
        public uint DwWindowStatus;
        public uint CxWindowBorders;
        public uint CyWindowBorders;
        public ushort AtomWindowType;
        public ushort WCreatorVersion;

        public WindowInfo(bool setCbSize = true) : this()
        {
            if (setCbSize) CbSize = (uint)Marshal.SizeOf(typeof(WindowInfo));
        }
    }
}