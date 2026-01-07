/*
TaskExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Diagnostics;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for the <see cref="Task"/> class.
/// </summary>
[DebuggerStepThrough]
public static class TaskExtensions
{
    /// <summary>
    /// Adds cancellation support to tasks that do not natively support
    /// using a <see cref="CancellationToken"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of result returned by the task.
    /// </typeparam>
    /// <param name="task">
    /// Task to execute.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The result of the asynchronous operation.
    /// </returns>
    /// <exception cref="TaskCanceledException">
    /// Thrown when the task is canceled using
    /// <paramref name="cancellationToken"/> before completing.
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
    /// Adds cancellation support to tasks that do not natively support
    /// using a <see cref="CancellationToken"/>.
    /// </summary>
    /// <param name="task">
    /// Task to execute.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// The result of the asynchronous operation.
    /// </returns>
    /// <exception cref="TaskCanceledException">
    /// Thrown when the task is canceled using
    /// <paramref name="cancellationToken"/> before completing.
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
    /// Waits for a task to complete and returns its result.
    /// </summary>
    /// <typeparam name="T">
    /// Type of result returned by the <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Task to wait for.</param>
    /// <returns>The result of the <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task)
    {
        return task.GetAwaiter().GetResult();
    }

    /// <summary>
    /// Waits for a task to complete and returns its result.
    /// </summary>
    /// <typeparam name="T">
    /// Type of result returned by the <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Task to wait for.</param>
    /// <param name="ct">Cancellation token for the task.</param>
    /// <returns>The result of the <see cref="Task{TResult}"/>.</returns>
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
    /// Waits for a task to complete and returns its result.
    /// </summary>
    /// <typeparam name="T">
    /// Type of result returned by the <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Task to wait for.</param>
    /// <param name="timeout">
    /// Time span to wait for the task to complete before
    /// forcibly aborting it.
    /// </param>
    /// <returns>The result of the <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task, TimeSpan timeout)
    {
        return task.Wait(timeout) ? task.Result : default!;
    }

    /// <summary>
    /// Waits for a task to complete and returns its result.
    /// </summary>
    /// <typeparam name="T">
    /// Type of result returned by the <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Task to wait for.</param>
    /// <param name="msTimeout">
    /// Time in milliseconds to wait for the task
    /// to complete before forcibly aborting it.
    /// </param>
    /// <returns>The result of the <see cref="Task{TResult}"/>.</returns>
    [Sugar]
    public static T Yield<T>(this Task<T> task, int msTimeout)
    {
        return task.Wait(msTimeout) ? task.Result : default!;
    }

    /// <summary>
    /// Waits for a task to complete and returns its result.
    /// </summary>
    /// <typeparam name="T">
    /// Type of result returned by the <see cref="Task{TResult}"/>.
    /// </typeparam>
    /// <param name="task">Task to wait for.</param>
    /// <param name="msTimeout">
    /// Time in milliseconds to wait for the task
    /// to complete before forcibly aborting it.
    /// </param>
    /// <param name="ct">Cancellation token for the task.</param>
    /// <returns>The result of the <see cref="Task{TResult}"/>.</returns>
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
    /// Queues a task.
    /// </summary>
    /// <typeparam name="T">Type of the task.</typeparam>
    /// <param name="task">Task to add to the queue.</param>
    /// <param name="tasks">Queue of tasks.</param>
    /// <returns>
    /// <paramref name="task"/>, which allows for fluent syntax calls.
    /// </returns>
    [Sugar]
    public static T Enqueue<T>(this T task, ICollection<Task> tasks) where T : Task
    {
        tasks.Add(task);
        return task;
    }
}
