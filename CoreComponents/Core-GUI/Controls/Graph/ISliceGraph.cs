/*
ISliceGraph.cs

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

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Expone una serie de métodos de redibujado disponibles para un
    /// control que acepte un <see cref="Slice"/>.
    /// </summary>
    public partial interface ISliceGraph : IGraph
    {
        /// <summary>
        /// Dibuja al <see cref="Slice"/> y a sus hijos.
        /// </summary>
        /// <param name="r">
        /// <see cref="Slice"/> que ha realizado la solicitud de redibujo.
        /// </param>
        void DrawMe(Slice r);
        /// <summary>
        /// Dibuja únicamente a los hijos del <see cref="Slice"/>.
        /// </summary>
        /// <param name="r">
        /// <see cref="Slice"/> que ha realizado la solicitud de redibujo.
        /// </param>
        void DrawMyChildren(Slice r);
        /// <summary>
        /// Obtiene o establece un valor que determina si se mostrarán los
        /// totales de los puntos y el total general de los datos.
        /// </summary>
        bool TotalVisible { get; set; }
        /// <summary>
        /// Obtiene un listado de los <see cref="Slice"/> que conforman el
        /// set de datos de este <see cref="ISliceGraph"/>.
        /// </summary>
        System.Collections.Generic.IList<Slice> Slices { get; set; }
        /// <summary>
        /// Obtiene el total general de los datos de este 
        /// <see cref="ISliceGraph"/>.
        /// </summary>
        double Total { get; }
        /// <summary>
        /// Obtiene o establece la cantidad de sub-niveles a mostrar en este
        /// <see cref="ISliceGraph"/>.
        /// </summary>
        int SubLevelsShown { get; set; }
    }
}