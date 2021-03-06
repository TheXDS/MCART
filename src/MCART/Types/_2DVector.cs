﻿/*
_2DVector.cs

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types
{
#pragma warning disable IDE1006
    internal struct _2DVector : I2DVector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool Equals([AllowNull] I2DVector other)
        {
            return X == other?.X && Y == other.Y;
        }
    }
}