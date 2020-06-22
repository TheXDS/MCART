/*
RelayCommand.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System;
using System.Windows.Input;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    /// Describe un comando estándar de implementación común bajo el
    /// esquema MVVM.
    /// </summary>
    public class RelayCommand : ICommand
    {
        readonly Action<object> _action;
        readonly Func<object, bool>? _canExecute;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="action">Comando a ejecutar.</param>
        public RelayCommand(Action<object> action) : this(action, null) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="action">Comando a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ser ejecutado.
        /// </param>
        public RelayCommand(Action<object> action, Func<object, bool>? canExecute)
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
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }
        
        /// <summary>
        /// Se produce cuando hay cambios que influyen en si el comando
        /// debería ejecutarse o no.
        /// </summary>
        public event EventHandler CanExecuteChanged
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
        public void Execute(object parameter) { _action(parameter); }

        /// <summary>
        /// Obliga al comando a evaluar <see cref="CanExecute(object)"/>.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}