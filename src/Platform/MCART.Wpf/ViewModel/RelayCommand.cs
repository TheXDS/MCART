/*
RelayCommand.cs

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

namespace TheXDS.MCART.ViewModel;
using System;
using System.Windows.Input;

/// <summary>
/// Describe un comando estándar de implementación común bajo el
/// paradigma MVVM en Wpf.
/// </summary>
public class RelayCommand : ICommand
{
    readonly Action<object?> _action;
    readonly Func<object?, bool>? _canExecute;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RelayCommand"/>.
    /// </summary>
    /// <param name="action">Comando a ejecutar.</param>
    public RelayCommand(Action<object?> action) : this(action, null) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RelayCommand"/>.
    /// </summary>
    /// <param name="action">Comando a ejecutar.</param>
    /// <param name="canExecute">
    /// Función que determina si el comando puede ser ejecutado.
    /// </param>
    public RelayCommand(Action<object?> action, Func<object?, bool>? canExecute)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Define el método que determina si el comando puede ejecutarse
    /// en su estado actual.
    /// </summary>
    /// <param name="parameter">
    /// Datos que usa el comando. Si el comando no exige pasar los
    /// datos, se puede establecer este objeto en
    /// <see langword="null" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si se puede ejecutar este comando; de
    /// lo contrario, <see langword="false" />.
    /// </returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    /// <summary>
    /// Se produce cuando hay cambios que influyen en si el comando
    /// debería ejecutarse o no.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Define el método al que se llamará cuando se invoque el comando.
    /// </summary>
    /// <param name="parameter">
    /// Datos que usa el comando. Si el comando no exige pasar los
    /// datos, se puede establecer este objeto en
    /// <see langword="null" />.
    /// </param>
    public void Execute(object? parameter) { _action(parameter); }

    /// <summary>
    /// Obliga al comando a evaluar <see cref="CanExecute(object)"/>.
    /// </summary>
    public static void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
