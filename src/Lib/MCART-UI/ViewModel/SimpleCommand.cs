/*
SimpleCommand.cs

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
using System.Threading.Tasks;
using System.Windows.Input;

/// <summary>
/// Describe un comando simple que puede ser declarado dentro de un
/// <see cref="ViewModelBase" />.
/// </summary>
public class SimpleCommand : CommandBase, ICommand
{
    private bool _canExecute;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand" />.
    /// </summary>
    /// <param name="action">Acción a ejecutar.</param>
    public SimpleCommand(Action action) : this(action, true)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand"/>.
    /// </summary>
    /// <param name="action">Acción a ejecutar.</param>
    /// <param name="canExecute">
    /// Valor que indica si el comando puede ser ejecutado
    /// inmediatamente después de instanciar esta clase.
    /// </param>
    public SimpleCommand(Action action, bool canExecute) : this(_ => action())
    {
        _canExecute = canExecute;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand" />.
    /// </summary>
    /// <param name="action">Acción a ejecutar.</param>
    public SimpleCommand(Action<object?> action) : this(action, true)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand"/>.
    /// </summary>
    /// <param name="action">Acción a ejecutar.</param>
    /// <param name="canExecute">
    /// Valor que indica si el comando puede ser ejecutado
    /// inmediatamente después de instanciar esta clase.
    /// </param>
    public SimpleCommand(Action<object?> action, bool canExecute) : base(action)
    {
        _canExecute = canExecute;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand" />.
    /// </summary>
    /// <param name="task">Tarea a ejecutar.</param>
    public SimpleCommand(Func<Task> task) : this((Action)(async () => await task()))
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand" />.
    /// </summary>
    /// <param name="task">Tarea a ejecutar.</param>
    public SimpleCommand(Func<object?, Task> task) : this((Action<object?>)(async o => await task(o)))
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand"/>.
    /// </summary>
    /// <param name="task">Tarea a ejecutar.</param>
    /// <param name="canExecute">
    /// Valor que indica si el comando puede ser ejecutado
    /// inmediatamente después de instanciar esta clase.
    /// </param>
    public SimpleCommand(Func<Task> task, bool canExecute) : this((Action)(async () => await task()))
    {
        _canExecute = canExecute;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="SimpleCommand"/>.
    /// </summary>
    /// <param name="task">Tarea a ejecutar.</param>
    /// <param name="canExecute">
    /// Valor que indica si el comando puede ser ejecutado
    /// inmediatamente después de instanciar esta clase.
    /// </param>
    public SimpleCommand(Func<object?, Task> task, bool canExecute) : this((Action<object?>)(async o => await task(o)))
    {
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
    public override bool CanExecute(object? parameter)
    {
        return _canExecute;
    }

    /// <summary>
    /// Establece manualmente si este comando puede ser ejecutado.
    /// </summary>
    /// <param name="canExecute"></param>
    public void SetCanExecute(bool canExecute)
    {
        _canExecute = canExecute;
        RaiseCanExecuteChanged();
    }
}
