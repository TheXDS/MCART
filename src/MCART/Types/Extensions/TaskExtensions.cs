/*
TaskExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;

/// <summary>
/// Extensiones para la clase <see cref="Task"/>.
/// </summary>
[DebuggerStepThrough]
public static class TaskExtensions
{
    /// <summary>
    /// Agrega soporte de cancelación a las tareas que no admiten el
    /// uso de un <see cref="CancellationToken"/> de forma nativa.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de resultado devuelto por la tarea.
    /// </typeparam>
    /// <param name="task">
    /// Tarea a ejecutar.
    /// </param>
    /// <param name="cancellationToken">
    /// Token de cancelación.
    /// </param>
    /// <returns>
    /// El resutlado de la operación asíncrona.
    /// </returns>
    /// <exception cref="TaskCanceledException">
    /// Se produce cuando la tarea es cancelada por medio de
    /// <paramref name="cancellationToken"/> antes de finalizar.
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
    /// Agrega soporte de cancelación a las tareas que no admiten el
    /// uso de un <see cref="CancellationToken"/> de forma nativa.
    /// </summary>
    /// <param name="task">
    /// Tarea a ejecutar.
    /// </param>
    /// <param name="cancellationToken">
    /// Token de cancelación.
    /// </param>
    /// <returns>
    /// El resutlado de la operación asíncrona.
    /// </returns>
    /// <exception cref="TaskCanceledException">
    /// Se produce cuando la tarea es cancelada por medio de
    /// <paramref name="cancellationToken"/> antes de finalizar.
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
    /// Espera la finalización de una tarea y devuelve su resultado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Tarea a esperar.</param>
    /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task)
    {
        return task.GetAwaiter().GetResult();
    }

    /// <summary>
    /// Espera la finalización de una tarea y devuelve su resultado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Tarea a esperar.</param>
    /// <param name="ct">Token de cancelación de la tarea.</param>
    /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task, CancellationToken ct)
    {
        try
        {
            task.Wait(ct);
            return task.IsCompletedSuccessfully ? task.Result : default!;
        }
        catch (OperationCanceledException)
        {
            return default!;
        }
    }

    /// <summary>
    /// Espera la finalización de una tarea y devuelve su resultado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Tarea a esperar.</param>
    /// <param name="timeout">
    /// Cantidad de tiempo a esperar a que la tarea finalice antes de
    /// abortarla forzosamente.
    /// </param>
    /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task, TimeSpan timeout)
    {
        return task.Wait(timeout) ? task.Result : default!;
    }

    /// <summary>
    /// Espera la finalización de una tarea y devuelve su resultado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Tarea a esperar.</param>
    /// <param name="msTimeout">
    /// Cantidad de tiempo en milisegundos a esperar a que la tarea
    /// finalice antes de abortarla forzosamente.
    /// </param>
    /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task, int msTimeout)
    {
        return task.Wait(msTimeout) ? task.Result : default!;
    }

    /// <summary>
    /// Espera la finalización de una tarea y devuelve su resultado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de resultado devuelto por el <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Tarea a esperar.</param>
    /// <param name="msTimeout">
    /// Cantidad de tiempo en milisegundos a esperar a que la tarea
    /// finalice antes de abortarla forzosamente.
    /// </param>
    /// <param name="ct">Token de cancelación de la tarea.</param>
    /// <returns>El resultado del <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task, int msTimeout, CancellationToken ct)
    {
        try
        {
            task.Wait(msTimeout, ct);
            return task.IsCompletedSuccessfully ? task.Result : default!;
        }
        catch (OperationCanceledException)
        {
            return default!;
        }
    }

    /// <summary>
    /// Pone en cola una tarea.
    /// </summary>
    /// <typeparam name="T">Tipo de la tarea.</typeparam>
    /// <param name="task">Tarea a agregar a la cola.</param>
    /// <param name="tasks">Cola de tareas.</param>
    /// <returns>
    /// <paramref name="task"/>, lo cual permite realizar llamadas con
    /// sintaxis Fluent.
    /// </returns>
    [Sugar]
    public static T Enqueue<T>(this T task, ICollection<Task> tasks) where T : Task
    {
        tasks.Add(task);
        return task;
    }
}
