/*
Color_Contracts.cs

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

using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static TheXDS.MCART.Misc.Internals;
using CI = System.Globalization.CultureInfo;
using DR = System.Drawing;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Estructura universal que describe un color en sus componentes alfa,
    /// rojo, verde y azul.
    /// </summary>
    public partial struct Color
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void Blend_Contract(in IEnumerable<Color> colors)
        {
            if (!colors.Any()) throw Errors.EmptyCollection(colors);
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void AreClose_Contract(in float delta)
        {
            if (!delta.IsBetween(0f, 1f)) throw Errors.ValueOutOfRange(nameof(delta), 0f, 1f);
        }

        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void TryParse_Contract(string from)
        {
            NullCheck(from, nameof(from));
        }
    }
}