﻿//
//  IGraph.cs
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

namespace MCART.Controls
{
    /// <summary>
    /// Define una serie de métodos y propiedades a implementar por un control
    /// que permita mostrar gráficos de cualquier tipo.
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// Obtiene o establece el título de este <see cref="IGraph"/>.
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Obtiene o establece el tamaño de fuente a aplicar al título.
        /// </summary>
        double TitleFontSize { get; set; }
        /// <summary>
        /// Solicita al control volver a dibujarse en su totalidad.
        /// </summary>
        void Redraw();
    }
}