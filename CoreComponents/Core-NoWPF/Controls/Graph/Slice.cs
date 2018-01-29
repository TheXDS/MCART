/*
Slice.cs

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

using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using System;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Representa una sección de un <see cref="ISliceGraph"/>.
    /// </summary>
    public partial class Slice
    {
        private string name = "Series";
        private Color sliceColor = Colors.Pick();
        private double val = 1.0;

        /// <summary>
        /// Obtiene o establece el nombre de este <see cref="Slice"/>.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                drawingParent?.DrawMyLabel(this);
            }
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para
        /// dibujar este <see cref="Slice"/>.
        /// </summary>
        public Color Color
        {
            get => sliceColor;
            set
            {
                sliceColor = value;
                drawingParent?.DrawOnlyMe(this);
                drawingParent?.DrawMyLabel(this);
            }
        }
        /// <summary>
        /// Obtiene o establece el valor de este <see cref="Slice"/>.
        /// </summary>
        public double Value
        {
            get => val;
            set
            {
                val = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(value));
                drawingParent?.DrawMe(this);
            }
        }
    }
}