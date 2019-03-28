/*
ValueChangingEventArgs.cs

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

#nullable enable

using System.ComponentModel;

namespace TheXDS.MCART.Events
{
    /// <inheritdoc />
    /// <summary>
    ///     Incluye información para cualquier evento que incluya tipos de valor y
    ///     puedan ser cancelados.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo del valor almacenado por esta instancia.
    /// </typeparam>
    public class ValueChangingEventArgs<T> : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Events.ValueChangingEventArgs`1" /> con el valor provisto.
        /// </summary>
        /// <param name="oldValue">
        ///     Valor original asociado al evento generado.
        /// </param>
        /// <param name="newValue">
        ///     Nuevo valor asociado al evento generado.
        /// </param>
        public ValueChangingEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        ///     Devuelve el valor original asociado a este evento.
        /// </summary>
        /// <returns>
        ///     Un valor de tipo <typeparamref name="T" /> con el valor asociado al
        ///     evento.
        /// </returns>
        public T OldValue { get; }

        /// <summary>
        ///     Devuelve el nuevo valor asociado a este evento.
        /// </summary>
        /// <returns>
        ///     Un valor de tipo <typeparamref name="T" /> con el valor asociado al
        ///     evento.
        /// </returns>
        public T NewValue { get; }

        /// <summary>
        ///     Convierte explícitamente este
        ///     <see cref="ValueChangingEventArgs{T}" /> en un
        ///     <see cref="ValueEventArgs{T}" />.
        /// </summary>
        /// <param name="fromValue">Objeto a convertir.</param>
        /// <returns>
        ///     Un <see cref="ValueEventArgs{T}" /> con la información pertinente
        ///     del <see cref="ValueChangingEventArgs{T}" /> especificado.
        /// </returns>
        public static explicit operator ValueEventArgs<T>(ValueChangingEventArgs<T> fromValue)
        {
            return new ValueEventArgs<T>(fromValue.NewValue);
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Incluye información para cualquier evento que incluya tipos de valor y
    ///     puedan ser cancelados.
    /// </summary>
    public class ValueChangingEventArgs : ValueChangingEventArgs<object?>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Events.ValueChangingEventArgs`1" /> con el valor provisto.
        /// </summary>
        /// <param name="oldValue">
        ///     Valor original asociado al evento generado.
        /// </param>
        /// <param name="newValue">
        ///     Nuevo valor asociado al evento generado.
        /// </param>
        public ValueChangingEventArgs(object? oldValue, object? newValue) : base(oldValue, newValue)
        {
        }
    }
}