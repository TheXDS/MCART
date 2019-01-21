/*
Events.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

#region Configuración de ReSharper

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable IntroduceOptionalParameters.Global

#endregion

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
        /// <param name="x">
        ///     Valor de progreso. Debe ser un <see cref="T:System.Double" /> entre
        ///     <c>0.0</c> y <c>1.0</c>, o los valores <see cref="F:System.Double.NaN" />,
        ///     <see cref="F:System.Double.PositiveInfinity" /> o
        ///     <see cref="F:System.Double.NegativeInfinity" />.
        /// </param>
        /// <param name="y">
        ///     Parámetro opcional. Descripción del estado de progreso que generó el
        ///     evento.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="x" /> no en un valor entre <c>0.0</c>
        ///     y <c>1.0</c>.
        /// </exception>
        public ProgressionEventArgs(double x, string y = null) : base(x)
        {
            if (x > 1 || x < 0) throw new ArgumentOutOfRangeException();
            HelpText = y;
        }

        /// <summary>
        ///     Devuelve una descripción rápida del estado de progreso.
        /// </summary>
        /// <returns>
        ///     Un <see cref="string" /> con un mensaje que describe el estado de
        ///     progreso del evento.
        /// </returns>
        public string HelpText { get; }
    }
}