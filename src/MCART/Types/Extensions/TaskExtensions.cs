/*
TaskExtensions.cs

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
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones para la clase <see cref="Task"/>.
    /// </summary>
    [DebuggerStepThrough]
    public static class TaskExtensions
    {
        /// <summary>
        ///     Agrega soporte de cancelación a las tareas que no admiten el
        ///     uso de un <see cref="CancellationToken"/> de forma nativa.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por la tarea.
        /// </typeparam>
        /// <param name="task">
        ///     Tarea a ejecutar.
        /// </param>
        /// <param name="cancellationToken">
        ///     Token de cancelación.
        /// </param>
        /// <returns>
        ///     El resutlado de la operación asíncrona.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     Se produce cuando la tarea es cancelada por medio de
        ///     <paramref name="cancellationToken"/> antes de finalizar.
        /// </exception>
        public static Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            return task.IsCompleted
                ? task
                : task.ContinueWith(
                    completedTask => completedTask.GetAwaiter().GetResult(),
                    cancellationToken,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
        }

        /// <summary>
        ///     Agrega soporte de cancelación a las tareas que no admiten el
        ///     uso de un <see cref="CancellationToken"/> de forma nativa.
        /// </summary>
        /// <param name="task">
        ///     Tarea a ejecutar.
        /// </param>
        /// <param name="cancellationToken">
        ///     Token de cancelación.
        /// </param>
        /// <returns>
        ///     El resutlado de la operación asíncrona.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     Se produce cuando la tarea es cancelada por medio de
        ///     <paramref name="cancellationToken"/> antes de finalizar.
        /// </exception>
        public static Task WithCancellation(this Task task, CancellationToken cancellationToken)
        {
            return task.IsCompleted
                ? task
                : task.ContinueWith(
                    completedTask => completedTask.GetAwaiter().GetResult(),
                    cancellationToken,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
        }

        /// <summary>
        ///     Espera la finalización de una tarea y devuelve su resultado.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
        /// </typeparam>
        /// <param name="task">Tarea a esperar.</param>
        /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
        [Sugar]
        public static T Yield<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
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
        [Sugar]
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
        [Sugar]
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
        [Sugar]
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
        [Sugar]
        public static T Yield<T>(this Task<T> task, int msTimeout, CancellationToken ct)
        {
            task.Wait(msTimeout, ct);
            return task.Result;
        }

        /// <summary>
        ///     Ejecuta una tarea en un contexto que arrojará cualquier
        ///     excepción producida durante su ejecución.
        /// </summary>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <returns>Una tarea que permite observar la operación actual.</returns>
        public static async Task Throwable(this Task task)
        {
            await task;
            if (task.IsFaulted) throw task.Exception ?? new AggregateException();
        }

        /// <summary>
        ///     Ejecuta una tarea en un contexto que arrojará cualquier
        ///     excepción producida durante su ejecución.
        /// </summary>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <returns>Una tarea que permite observar la operación actual.</returns>
        public static async Task<T> Throwable<T>(this Task<T> task)
        {
            var r = await task;
            if (task.IsFaulted) throw task.Exception ?? new AggregateException();
            return r;
        }

        /// <summary>
        ///     Pone en cola una tarea.
        /// </summary>
        /// <typeparam name="T">Tipo de la tarea.</typeparam>
        /// <param name="task">Tarea a agregar a la cola.</param>
        /// <param name="tasks">Cola de tareas.</param>
        /// <returns>
        ///     <paramref name="task"/>, lo cual permite realizar llamadas con
        ///     sintaxis Fluent.
        /// </returns>
        [Sugar]
        public static T Enqueue<T>(this T task, ICollection<Task> tasks) where T : Task
        {
            tasks.Add(task);
            return task;
        }
    }
}