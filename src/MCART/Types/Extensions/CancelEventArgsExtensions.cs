/*
CancelEventArgsExtensions.cs

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
using System.ComponentModel;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para todos los elementos de tipo
    ///     <see cref="CancelEventArgs" />.
    /// </summary>
    public static class CancelEventArgsExtensions
    {
        /// <summary>
        ///     Ejecuta un controlador de eventos con estos argumentos, y 
        ///     continúa con otra llamada si el evento no ha sido cancelado.
        /// </summary>
        /// <typeparam name="TCancelEventArgs">
        ///     Tipo que deriva de <see cref="CancelEventArgs"/> para el cual
        ///     este método es una extensión.
        /// </typeparam>
        /// <param name="evtArgs">
        ///     Argumentos a utilizar en la llamada del evento cancelable.
        /// </param>
        /// <param name="handler">
        ///     Evento cancelable a ejecutar.
        /// </param>
        /// <param name="sender">
        ///     Objeto que es el origen del evento a generar.
        /// </param>
        /// <param name="continuation">
        ///     Llamada de continuación en caso que el evento no haya sido 
        ///     cancelado.
        /// </param>
        public static void Attempt<TCancelEventArgs>(this TCancelEventArgs evtArgs, EventHandler<TCancelEventArgs>? handler, object? sender, Action<TCancelEventArgs> continuation) where TCancelEventArgs : CancelEventArgs
        {
            handler?.Invoke(sender, evtArgs);
            if (!evtArgs.Cancel)
            {
                continuation(evtArgs);
            }
        }
    }
}