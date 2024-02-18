/*
CommandBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
