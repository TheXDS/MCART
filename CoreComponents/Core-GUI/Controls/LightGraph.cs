//
//  LightGraph.cs
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
    ///<summary>
    /// Control/Widget de gráficos ligero.
    ///</summary>
    public partial class LightGraph
    {
        /// <summary>
        /// Determina el modo de dibujo de las etiquetas puntuales de las 
        /// gráficas.
        /// </summary>
        public enum SpotLabelsDrawMode : byte
        {
            /// <summary>
            /// No dibujar etiquetas.
            /// </summary>
            None = 0,
            /// <summary>
            /// Dibujar el valor del punto.
            /// </summary>
            YValues = 1,
            /// <summary>
            /// Dibujar el valor porcentual del punto.
            /// </summary>
            Percent = 2,
            /// <summary>
            /// Dibujar una etiqueta del eje X.
            /// </summary>
            XValues = 4,
            /// <summary>
            /// Dibujar en el mismo color del gráfico en lugar del color de
            /// texto.
            /// </summary>
            GraphColor = 8,
            /// <summary>
            /// Hace que el fondo de las etiquetas sea obscuro.
            /// </summary>
            DarkBG = 16
        }
        /// <summary>
        /// Determina el modo de dibujo del gráfico.
        /// </summary>
        public enum EnumGraphDrawMode : byte
        {
            /// <summary>
            /// Dibujar histograma
            /// </summary>
            Histogram,
            /// <summary>
            /// Rellenar el área del gráfico
            /// </summary>
            Filled,
            /// <summary>
            /// Dibujar barras
            /// </summary>
            Bars
        }
    }
}