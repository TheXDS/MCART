/*
SimpleCommand.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Windows;
using System.Windows.Input;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.ViewModel
{
    /// <inheritdoc cref="ICommand"/>
    /// <summary>
    ///     Describe un comando simple que puede ser declarado dentro de un
    ///     <see cref="N:TheXDS.MCART.ViewModel" />.
    /// </summary>
    public class SimpleCommand : NotifyPropertyChanged, ICommand
    {
        private readonly Action _action;
        private bool _canExecute;

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.SimpleCommand" />.
        /// </summary>
        /// <param name="action">Acción a ejecutar.</param>
        public SimpleCommand(Action action) : this(action, true)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="SimpleCommand"/>.
        /// </summary>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Valor que indica si el comando puede ser ejecutado
        ///     inmediatamente después de instanciar esta clase.
        /// </param>
        public SimpleCommand(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Define el método que determina si el comando puede ejecutarse
        ///     en su estado actual.
        /// </summary>
        /// <param name="parameter">
        ///     Datos que usa el comando. Si el comando no exige pasar los
        ///     datos, se puede establecer este objeto en
        ///     <see langword="null" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> si se puede ejecutar este comando; de
        ///     lo contrario, <see langword="false" />.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        /// <summary>
        ///     Establece manualmente si este comando puede ser ejecutado.
        /// </summary>
        /// <param name="canExecute"></param>
        public void SetCanExecute(bool canExecute)
        {
            _canExecute = canExecute;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(Visibility));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Se produce cuando hay cambios que influyen en si el comando
        ///     debería ejecutarse o no.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc />
        /// <summary>
        ///     Define el método al que se llamará cuando se invoque el comando.
        /// </summary>
        /// <param name="parameter">
        ///     Datos que usa el comando. Si el comando no exige pasar los
        ///     datos, se puede establecer este objeto en
        ///     <see langword="null" />.
        /// </param>
        public void Execute(object parameter)
        {
            _action();
        }

        /// <summary>
        ///     Atajo especial que permite mostrar u ocultar un control de acuerdo al estado de este <see cref="SimpleCommand"/>.
        /// </summary>
        public Visibility Visibility => _canExecute ? Visibility.Visible : Visibility.Collapsed;
    }
}