//
//  RingGraph.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;

namespace MCART.Controls
{
    public partial class RingGraph : ISliceGraph, IGraph
    {
        private List<Slice> slices = new List<Slice>();
        /// <summary>
        /// Obtiene un listado de los <see cref="Slice"/> que conforman el
        /// set de datos de este <see cref="ISliceGraph"/>.
        /// </summary>
        /// <remarks>
        /// Esta no puede ser una propiedad de dependencia debido a que la
        /// observación de la lista de <see cref="Slice"/> se implementa
        /// mediante eventos.
        /// </remarks>
        public IList<Slice> Slices => slices;
        /// <summary>
        /// Vuelve a dibujar todo el control.
        /// </summary>
        /// <param name="r">
        /// <see cref="Slice"/> que ha realizado la solicitud.
        /// </param>
        public void DrawMe(Slice r)
        {
            // Si un Slice cambia, cambia todo el gráfico.
            Redraw();
        }
    }
}