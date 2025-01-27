/*
SimpleCommand.cs

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

using System.Windows.Input;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Component;

/// <summary>
/// Describes a simplified command that can be used in a
/// <see cref="ViewModelBase" />.
/// </summary>
/// <param name="action">Action to execute.</param>
/// <param name="canExecute">
/// Indicates if the command will be executable by default.
/// </param>
public class SimpleCommand(Action<object?> action, bool canExecute) : CommandBase(action), ICommand
{
    private bool _canExecute = canExecute;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
    /// </summary>
    /// <param name="action">Action to be executed.</param>
    public SimpleCommand(Action action) : this(action, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
    /// </summary>
    /// <param name="action">Action to be executed.</param>
    /// <param name="canExecute">
    /// Indicates if the command will be executable by default.
    /// </param>
    public SimpleCommand(Action action, bool canExecute) : this(_ => action())
    {
        _canExecute = canExecute;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
    /// </summary>
    /// <param name="action">Action to be executed.</param>
    public SimpleCommand(Action<object?> action) : this(action, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
    /// </summary>
    /// <param name="task">Task to be executed.</param>
    public SimpleCommand(Func<Task> task) : this((Action)(async () => await task()))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
    /// </summary>
    /// <param name="task">Task to be executed.</param>
    public SimpleCommand(Func<object?, Task> task) : this((Action<object?>)(async o => await task(o)))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
    /// </summary>
    /// <param name="task">Task to be executed.</param>
    /// <param name="canExecute">
    /// Indicates if the command will be executable by default.
    /// </param>
    public SimpleCommand(Func<Task> task, bool canExecute) : this((Action)(async () => await task()))
    {
        _canExecute = canExecute;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleCommand" /> class.
    /// </summary>
    /// <param name="task">Task to be executed.</param>
    /// <param name="canExecute">
    /// Indicates if the command will be executable by default.
    /// </param>
    public SimpleCommand(Func<object?, Task> task, bool canExecute) : this((Action<object?>)(async o => await task(o)))
    {
        _canExecute = canExecute;
    }

    /// <summary>
    /// Checks if the current command can be executed.
    /// </summary>
    /// <param name="parameter">
    /// DCommand parameters. If the command does not require parameters, this
    /// value can be set to <see langword="null" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the command can be executed,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public override bool CanExecute(object? parameter)
    {
        return _canExecute;
    }

    /// <summary>
    /// Manually sets a value that determines if this command can be executed.
    /// </summary>
    /// <param name="canExecute">
    /// Value that indicates if the command can be executed.
    /// </param>
    public void SetCanExecute(bool canExecute)
    {
        _canExecute = canExecute;
        RaiseCanExecuteChanged();
    }
}
