/*
RelayCommand.cs

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

using System.Windows.Input;

namespace TheXDS.MCART.Component;

/// <summary>
/// Describes a standard RelayCommand implementation commonly used under
/// the MVVM pattern in WPF.
/// </summary>
/// <param name="action">Command to execute.</param>
/// <param name="canExecute">
/// Function that determines whether the command can be executed.
/// </param>
public class RelayCommand(Action<object?> action, Func<object?, bool>? canExecute) : ICommand
{
    private readonly Action<object?> _action = action ?? throw new ArgumentNullException(nameof(action));
    private readonly Func<object?, bool>? _canExecute = canExecute;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="action">Command to execute.</param>
    public RelayCommand(Action<object?> action) : this(action, null) { }

    /// <summary>
    /// Defines the method that determines whether the command can execute
    /// in its current state.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require
    /// data, this object can be set to <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the command can execute; otherwise
    /// <see langword="false"/>.
    /// </returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    /// <summary>
    /// Occurs when changes affect whether the command should execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Defines the method to call when the command is invoked.
    /// </summary>
    /// <param name="parameter">
    /// Data used by the command. If the command does not require data,
    /// this object can be set to <see langword="null"/>.
    /// </param>
    public void Execute(object? parameter) { _action(parameter); }

    /// <summary>
    /// Forces the command to re-evaluate <see cref="CanExecute(object)"/>.
    /// </summary>
    public static void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
