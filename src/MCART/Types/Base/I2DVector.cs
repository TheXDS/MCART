/*
I2DVector.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Interfaz que define propiedades comunes para estructuras de datos
    /// que describen coordenadas, vectores, magnitudes y tamaños en un
    /// espacio de dos dimensiones.
    /// </summary>
    public interface I2DVector : IEquatable<I2DVector>
    {
        /// <summary>
        /// Obtiene el componente horizontal (eje X) representado por este
        /// <see cref="I2DVector"/>.
        /// </summary>
        double X { get; }

        /// <summary>
        /// Obtiene el componente vertical (eje Y) representado por este
        /// <see cref="I2DVector"/>.
        /// </summary>
        double Y { get; }
    }
}