/*
CommandBase.cs

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
using System.Windows.Input;

/// <summary>
/// Clase base para las implementaciones de la interfaz
/// <see cref="ICommand"/> en MCART.
/// </summary>
public abstract class CommandBase : ICommand
{
    private readonly Action<object?> _action;

    /// <summary>
    /// Se produce cuando hay cambios que influyen en si el comando
    /// debería ejecutarse o no.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="CommandBase"/>.
    /// </summary>
    /// <param name="action">
    /// Acción a asociar a este comando.
    /// </param>
    protected CommandBase(Action<object?> action)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
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
    public abstract bool CanExecute(object? parameter);

    /// <summary>
    /// Define el método que determina si el comando puede ejecutarse
    /// en su estado actual.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si se puede ejecutar este comando; de
    /// lo contrario, <see langword="false" />.
    /// </returns>
    public bool CanExecute() => CanExecute(null);

    /// <summary>
    /// Ejecuta el método asociado a la invocación de este comando.
    /// </summary>
    /// <param name="parameter">
    /// Datos que usa el comando. Si el comando no exige pasar los
    /// datos, se puede establecer este objeto en
    /// <see langword="null" />.
    /// </param>
    public virtual void Execute(object? parameter)
    {
        _action(parameter);
    }

    /// <summary>
    /// Ejecuta el método asociado a la invocación de este comando.
    /// </summary>
    public void Execute() => Execute(null);

    /// <summary>
    /// Comprueba si la acción puede ejecutarse, y de ser así, la ejecuta.
    /// </summary>
    /// <param name="arg">
    /// Argumentos a utilizar para comprobar y para ejecutar la acción
    /// asociada a este comando.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si se ha ejecutado la acción luego de
    /// comprobar la posibilidad de ejecutarla, <see langword="false"/> en
    /// caso contrario.
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
    /// Comprueba si la acción puede ejecutarse, y de ser así, la ejecuta.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si se ha ejecutado la acción luego de
    /// comprobar la posibilidad de ejecutarla, <see langword="false"/> en
    /// caso contrario.
    /// </returns>
    public bool TryExecute() => TryExecute(null);

    /// <summary>
    /// Invoca el evento <see cref="CanExecuteChanged"/>.
    /// </summary>
    protected void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
