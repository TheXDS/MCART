/*
TaskExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para la clase <see cref="Task"/>.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///     Espera la finalización de una tarea y devuelve su resultado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
        /// </typeparam>
        /// <param name="task">Tarea a esperar.</param>
        /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
        [DebuggerStepThrough, Thunk]
        public static T Yield<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

        /// <summary>
        ///     Espera la finalización de una tarea y devuelve su resultado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
        /// </typeparam>
        /// <param name="task">Tarea a esperar.</param>
        /// <param name="ct">Token de cancelación de la tarea.</param>
        /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
        [DebuggerStepThrough, Thunk]
        public static T Yield<T>(this Task<T> task, CancellationToken ct)
        {
            task.Wait(ct);
            return task.Result;
        }

        /// <summary>
        ///     Espera la finalización de una tarea y devuelve su resultado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
        /// </typeparam>
        /// <param name="task">Tarea a esperar.</param>
        /// <param name="timeout">
        ///     Cantidad de tiempo a esperar a que la tarea finalice antes de
        ///     abortarla forzosamente.
        /// </param>
        /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
        [DebuggerStepThrough, Thunk]
        public static T Yield<T>(this Task<T> task, TimeSpan timeout)
        {
            task.Wait(timeout);
            return task.Result;
        }

        /// <summary>
        ///     Espera la finalización de una tarea y devuelve su resultado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
        /// </typeparam>
        /// <param name="task">Tarea a esperar.</param>
        /// <param name="msTimeout">
        ///     Cantidad de tiempo en milisegundos a esperar a que la tarea
        ///     finalice antes de abortarla forzosamente.
        /// </param>
        /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
        [DebuggerStepThrough, Thunk]
        public static T Yield<T>(this Task<T> task, int msTimeout)
        {
            task.Wait(msTimeout);
            return task.Result;
        }

        /// <summary>
        ///     Espera la finalización de una tarea y devuelve su resultado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
        /// </typeparam>
        /// <param name="task">Tarea a esperar.</param>
        /// <param name="msTimeout">
        ///     Cantidad de tiempo en milisegundos a esperar a que la tarea
        ///     finalice antes de abortarla forzosamente.
        /// </param>
        /// <param name="ct">Token de cancelación de la tarea.</param>
        /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
        [DebuggerStepThrough, Thunk]
        public static T Yield<T>(this Task<T> task, int msTimeout, CancellationToken ct)
        {
            task.Wait(msTimeout,ct);
            return task.Result;
        }
    }
}