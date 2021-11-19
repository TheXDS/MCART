/*
ValueChangingEventArgs.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

namespace TheXDS.MCART.Events
{
    /// <summary>
    /// Incluye información para cualquier evento que incluya tipos de valor y
    /// puedan ser cancelados.
    /// </summary>
    public class ValueChangingEventArgs : ValueChangingEventArgs<object?>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ValueChangingEventArgs{T}" /> con el valor provisto.
        /// </summary>
        /// <param name="oldValue">
        /// Valor original asociado al evento generado.
        /// </param>
        /// <param name="newValue">
        /// Nuevo valor asociado al evento generado.
        /// </param>
        public ValueChangingEventArgs(object? oldValue, object? newValue) : base(oldValue, newValue)
        {
        }
    }
}