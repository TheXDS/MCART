//
//  ProgressRing.cs
//
//  This file is part of MCART
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Morgan
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
    /*
     * Esta definición de clase permite compartir cierto código que no depende
     * de la plataforma para la cual se compila MCART. Los ensamblados de MCART
     * para cada una de las plataformas de destino definirán la correspondiente
     * herencia de clases e interfaces para crear el respectivo control, por lo
     * que esta clase parcial no incluye ninguna definición de herencia que no
     * pueda ser compartida entre todas las plataformas.
     */
    /// <summary>
    /// Permite representar un valor porcentual en un anillo de progreso.
    /// </summary>
    public partial class ProgressRing
    {
        /// <summary>
        /// Determina la dirección en la que este <see cref="ProgressRing"/>
        /// será rellenado.
        /// </summary>
        public enum SweepDirection : sbyte
        {
            /// <summary>
            /// El <see cref="ProgressRing"/> se rellenará en el sentido de las
            /// agujas del reloj.
            /// </summary>
            Clockwise = 1,
            /// <summary>
            /// El <see cref="ProgressRing"/> se rellenará en el sentido
            /// contrario a las agujas del reloj. 
            /// </summary>
            CounterClockwise = -1
        }
    }
}