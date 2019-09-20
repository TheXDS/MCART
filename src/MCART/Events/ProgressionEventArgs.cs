/*
ProgressionEventArgs.cs

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

#nullable enable

using System;

namespace TheXDS.MCART.Events
{
    /// <inheritdoc />
    /// <summary>
    ///     Incluye información de evento para cualquier clase con eventos que
    ///     reporten el progreso de una operación.
    /// </summary>
    public class ProgressionEventArgs : ValueEventArgs<double>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de este objeto con los datos
        ///     provistos.
        /// </summary>
        /// <param name="progress">
        ///     Valor de progreso. Debe ser un <see cref="double" /> entre
        ///     <c>0.0</c> y <c>1.0</c>, o los valores <see cref="F:System.Double.NaN" />,
        ///     <see cref="F:System.Double.PositiveInfinity" /> o
        ///     <see cref="F:System.Double.NegativeInfinity" />.
        /// </param>
        /// <param name="helpText">
        ///     Parámetro opcional. Descripción del estado de progreso que generó el
        ///     evento.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="progress" /> no en un valor entre <c>0.0</c>
        ///     y <c>1.0</c>.
        /// </exception>
        public ProgressionEventArgs(double progress, string? helpText) : base(progress)
        {
            if (progress > 1 || progress < 0) throw new ArgumentOutOfRangeException();
            HelpText = helpText;
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de este objeto con los datos
        ///     provistos.
        /// </summary>
        /// <param name="progress">
        ///     Valor de progreso. Debe ser un <see cref="double" /> entre
        ///     <c>0.0</c> y <c>1.0</c>, o los valores <see cref="F:System.Double.NaN" />,
        ///     <see cref="F:System.Double.PositiveInfinity" /> o
        ///     <see cref="F:System.Double.NegativeInfinity" />.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="progress" /> no en un valor entre <c>0.0</c>
        ///     y <c>1.0</c>.
        /// </exception>
        public ProgressionEventArgs(double progress) : this(progress, null)
        {

        }


        /// <summary>
        ///     Devuelve una descripción rápida del estado de progreso.
        /// </summary>
        /// <returns>
        ///     Un <see cref="string" /> con un mensaje que describe el estado de
        ///     progreso del evento.
        /// </returns>
        public string? HelpText { get; }
    }
}