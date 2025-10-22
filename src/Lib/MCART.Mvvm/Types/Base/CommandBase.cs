/*
CommandBase.cs

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

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for MCART implementations of the
/// <see cref="ICommand"/> interface.
/// </summary>
/// <param name="action">Action to associate with this command.</param>
public abstract class CommandBase(Action<object?> action) : ICommand
{
    private readonly Action<object?> _action = action ?? throw new ArgumentNullException(nameof(action));

    /// <summary>
    /// Raised when changes occur that affect whether the command
    /// should execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Determines whether the command can execute in its current
    /// state.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require
    /// data, this object can be set to <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this command can execute;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public abstract bool CanExecute(object? parameter);

    /// <summary>
    /// Determines whether the command can execute in its current
    /// state.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this command can execute;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool CanExecute() => CanExecute(null);

    /// <summary>
    /// Executes the action associated with this command.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require
    /// data, this object can be set to <see langword="null"/>.
    /// </param>
    public virtual void Execute(object? parameter)
    {
        _action(parameter);
    }

    /// <summary>
    /// Executes the action associated with this command.
    /// </summary>
    public void Execute() => Execute(null);

    /// <summary>
    /// Checks whether the action can execute and, if so, executes it.
    /// </summary>
    /// <param name="arg">
    /// Argument to use both for the check and for executing the action.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the action was executed after
    /// checking; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryExecute(object? arg)
    {
        if (CanExecute(arg))
        {
            Execute(arg);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks whether the action can execute and, if so, executes it.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the action was executed after
    /// checking; otherwise, <see langword="false"/>.
    /// </returns>
    public bool TryExecute() => TryExecute(null);

    /// <summary>
    /// Raises the <see cref="CanExecuteChanged"/> event.
    /// </summary>
    protected void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
