/*
RingGraph.cs

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

using System;

namespace TheXDS.MCART.Controls
{
    public partial class RingGraph
    {
        double ringThickness = 30.0;
        int subLevelsShown = 1;
        double total = 0;
        bool totalVisible = true;
        string title = string.Empty;
        double titleFontSize = 16.0;
        string toolTipFormat = string.Empty;

        /// <summary>
        /// Obtiene o establece el porcentaje de espacio ocupado por los datos
        /// desde el radio hasta el centro del gráfico, o hasta el espacio
        /// reservado para la etiqueta de total.
        /// </summary>
        public double RingThickness
        {
            get => ringThickness;
            set
            {
                if (!value.IsBetween(0.0, 100.0)) throw new ArgumentOutOfRangeException(nameof(value));
                ringThickness = value;
                Redraw();
            }
        }
        /// <summary>
        /// Obtiene o establece la cantidad de sub-niveles a mostrar en este
        /// <see cref="ISliceGraph"/>.
        /// </summary>
        public int SubLevelsShown
        {
            get => subLevelsShown;
            set
            {
                if (!(value > 0)) throw new ArgumentOutOfRangeException(nameof(value));
                subLevelsShown = value;
                Redraw();
            }
        }
        /// <summary>
        /// Obtiene el total general de los datos de este
        /// <see cref="ISliceGraph"/>.
        /// </summary>
        public double Total => total;

        public string Title
        {
            get => title;
            set
            {

            }
        }

        public bool TotalVisible
        {
            get => totalVisible;
            set
            {

            }
        }

        public double TitleFontSize
        {
            get => titleFontSize;
            set
            {

            }
        }
        public string ToolTipFormat
        {
            get => toolTipFormat;
            set
            {

            }
        }
    }
}