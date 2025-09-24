/*
ObservingCommandBuilder.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.ComponentModel;
using TheXDS.MCART.Component;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contiene métodos que permiten crear objetos 
/// <see cref="ObservingCommandBuilder{T}"/>, los cuales permiten configurar e
/// instanciar un <see cref="ObservingCommand"/>.
/// </summary>
public static class ObservingCommandBuilder
{
    private static Action<object?> Try<TParam>(Action<TParam> action) where TParam : notnull
    {
        return p =>
        {
            if (p is TParam x) action.Invoke(x);
        };
    }

    private static Func<object?, Task> Try<TParam>(Func<TParam, Task> action) where TParam : notnull
    {
        return p => (p is TParam x) ? action.Invoke(x) : Task.CompletedTask;
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">Type of observed object.</typeparam>
    /// <param name="observedObject">Observed object instance.</param>
    /// <param name="action">Action to execute upon command invocation.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> instance that can be
    /// used to configure and create an <see cref="ObservingCommand"/>.
    /// </returns>
    public static ObservingCommandBuilder<T> Create<T>(this T observedObject, Action action) where T : INotifyPropertyChanged
    {
        return new ObservingCommandBuilder<T>(observedObject, action);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">Type of observed object.</typeparam>
    /// <param name="observedObject">Observed object instance.</param>
    /// <param name="action">Action to execute upon command invocation.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> instance that can be
    /// used to configure and create an <see cref="ObservingCommand"/>.
    /// </returns>
    public static ObservingCommandBuilder<T> Create<T>(this T observedObject, Action<object?> action) where T : INotifyPropertyChanged
    {
        return new ObservingCommandBuilder<T>(observedObject, action);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">Type of observed object.</typeparam>
    /// <param name="observedObject">Observed object instance.</param>
    /// <param name="task">Task to execute upon command invocation.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> instance that can be
    /// used to configure and create an <see cref="ObservingCommand"/>.
    /// </returns>
    public static ObservingCommandBuilder<T> Create<T>(this T observedObject, Func<Task> task) where T : INotifyPropertyChanged
    {
        return new ObservingCommandBuilder<T>(observedObject, task);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">Type of observed object.</typeparam>
    /// <param name="observedObject">Observed object instance.</param>
    /// <param name="task">Task to execute upon command invocation.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> instance that can be
    /// used to configure and create an <see cref="ObservingCommand"/>.
    /// </returns>
    public static ObservingCommandBuilder<T> Create<T>(this T observedObject, Func<object?, Task> task) where T : INotifyPropertyChanged
    {
        return new ObservingCommandBuilder<T>(observedObject, task);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> instance and
    /// configures it to only be executable when the parameter type is exactly
    /// the expected type for the command callback.
    /// </summary>
    /// <typeparam name="T">Type of observed object.</typeparam>
    /// <typeparam name="TParam">
    /// Type of parameter accepted by the command. Must be non-null.
    /// </typeparam>
    /// <param name="observedObject">Observed object instance.</param>
    /// <param name="action">Action to execute upon command invocation.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> instance that can be
    /// used to configure and create an <see cref="ObservingCommand"/>.
    /// </returns>
    public static ObservingCommandBuilder<T> Create<T, TParam>(this T observedObject, Action<TParam> action)
        where T : INotifyPropertyChanged
        where TParam : notnull
    {
        return new ObservingCommandBuilder<T>(observedObject, Try(action)).CanExecute(p => p is TParam);
    }

    /// <summary>
    /// Creates a new <see cref="ObservingCommandBuilder{T}"/> instance and
    /// configures it to only be executable when the parameter type is exactly
    /// the expected type for the command callback.
    /// </summary>
    /// <typeparam name="T">Type of observed object.</typeparam>
    /// <typeparam name="TParam">
    /// Type of parameter accepted by the command. Must be non-null.
    /// </typeparam>
    /// <param name="observedObject">Observed object instance.</param>
    /// <param name="task">Task to execute upon command invocation.</param>
    /// <returns>
    /// A new <see cref="ObservingCommandBuilder{T}"/> instance that can be
    /// used to configure and create an <see cref="ObservingCommand"/>.
    /// </returns>
    public static ObservingCommandBuilder<T> Create<T, TParam>(this T observedObject, Func<TParam, Task> task)
        where T : INotifyPropertyChanged
        where TParam : notnull
    {
        return new ObservingCommandBuilder<T>(observedObject, Try(task)).CanExecute(p => p is TParam);
    }
}
