/*
SimpleCommand_T.cs

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

namespace TheXDS.MCART.Component;

/// <summary>
/// Strongly typed variant of the <see cref="SimpleCommand"/> class, that
/// includes type-safe actions or functions for the command parameter.
/// </summary>
/// <typeparam name="T">
/// Type of command parameter to be used in the command callback.
/// </typeparam>
public class SimpleCommand<T> : SimpleCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand{T}" />
    /// class.
    /// </summary>
    /// <param name="action">Action to be executed.</param>
    public SimpleCommand(Action<T?> action) : this(action, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand{T}" />
    /// class.
    /// </summary>
    /// <param name="action">Action to be executed.</param>
    /// <param name="canExecute">
    /// Indicates if the command will be executable by default.
    /// </param>
    public SimpleCommand(Action<T?> action, bool canExecute) : base(Try(action), canExecute)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand{T}" />
    /// class.
    /// </summary>
    /// <param name="task">Task to be executed.</param>
    public SimpleCommand(Func<T?, Task> task) : this(task, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand{T}" />
    /// class.
    /// </summary>
    /// <param name="task">Task to be executed.</param>
    /// <param name="canExecute">
    /// Indicates if the command will be executable by default.
    /// </param>
    public SimpleCommand(Func<T?, Task> task, bool canExecute) : base(Try(task), canExecute)
    {
    }

    private static Action<object?> Try(Action<T?> action)
    {
        return o => action.Invoke(o is T v ? v : default);
    }

    private static Func<object?, Task> Try(Func<T?, Task> func)
    {
        return o => func.Invoke(o is T v ? v : default);
    }
}
