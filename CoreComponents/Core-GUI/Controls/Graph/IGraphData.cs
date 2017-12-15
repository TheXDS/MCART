//
//  IGraphData.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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

using MCART.Types;

namespace MCART.Controls
{
    /// <summary>
    /// Determina una serie de propiedades a implementar por clases que puedan
    /// ser utilizadas en controles/widgets <see cref="IGraph"/>.
    /// </summary>
    public interface IGraphData
    {
        /// <summary>
        /// Obtiene o establece el nombre de este <see cref="IGraphData"/>.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Obtiene o establece el <see cref="Types.Color"/> a utilizar para
        /// dibujar este <see cref="IGraphData"/>.
        /// </summary>
        Color Color { get; set; }
    }
}