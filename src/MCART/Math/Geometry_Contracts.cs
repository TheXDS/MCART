﻿/*
Geometry_Contracts.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Math
{
     public static partial class Geometry
     {
          [Conditional("EnforceContracts")]
          [MethodImpl(MethodImplOptions.AggressiveInlining)]
          [DebuggerNonUserCode]
          private static void GetQuadBezierPoint_Contract(in double position, in Point startPoint, in Point controlPoint, in Point endPoint)
          {
               if (!position.IsBetween(0, 1)) throw new ArgumentOutOfRangeException(nameof(position));
               if (!new[] {startPoint.X, startPoint.Y}.AreValid()) throw new ArgumentException("", nameof(startPoint));
               if (!new[] {controlPoint.X, controlPoint.Y}.AreValid()) throw new ArgumentException("", nameof(controlPoint));
               if (!new[] {endPoint.X, endPoint.Y}.AreValid()) throw new ArgumentException("", nameof(endPoint));
          }
     }
}