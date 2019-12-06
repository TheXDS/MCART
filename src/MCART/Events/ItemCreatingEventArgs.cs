/*
ItemCreatingEventArgs.cs

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

using System.ComponentModel;

namespace TheXDS.MCART.Events
{
    /// <inheritdoc />
    /// <summary>
    ///     Contiene información de evento para cualquier clase con eventos donde
    ///     se guardará información.
    /// </summary>
    public class ItemCreatingEventArgs<T> : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de esta clase con la información de
        ///     evento provista.
        /// </summary>
        /// <param name="item">Objeto que ha sido guardado.</param>
        public ItemCreatingEventArgs(T item) : this(item, false)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de esta clase con la información de
        ///     evento provista.
        /// </summary>
        /// <param name="item">Objeto que ha sido guardado.</param>
        /// <param name="cancel">
        ///     Determina si este evento se cancelará de forma predeterminada.
        /// </param>
        public ItemCreatingEventArgs(T item, bool cancel) : base(cancel)
        {
            Item = item;
        }

        /// <summary>
        ///     Obtiene el elemento que ha sido creado/editado.
        /// </summary>
        /// <returns>
        ///     Una referencia de instancia al objeto creado/editado.
        /// </returns>
        public T Item { get; }

        /// <summary>
        ///     Convierte explícitamente un <see cref="ItemCreatingEventArgs{T}" /> en un
        ///     <see cref="ItemCreatedEventArgs{T}" />.
        /// </summary>
        /// <param name="fromValue">Objeto a convertir.</param>
        /// <returns>
        ///     Un <see cref="ItemCreatedEventArgs{T}" /> con la misma información de
        ///     evento que el <see cref="ItemCreatingEventArgs{T}" /> especificado.
        /// </returns>
        public static implicit operator ItemCreatedEventArgs<T>(ItemCreatingEventArgs<T> fromValue)
        {
            return new ItemCreatedEventArgs<T>(fromValue.Item);
        }
    }
}