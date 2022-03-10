/*
SimpleCommand.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
