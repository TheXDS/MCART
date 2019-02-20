﻿/*
ObservingCommand.cs

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TheXDS.MCART.Annotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace TheXDS.MCART.ViewModel
{
    /// <inheritdoc />
    /// <summary>
    ///     Describe un comando que observa a un objeto que implemente
    ///     <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> y escucha cualquier cambio
    ///     ocurrido en el valor de sus propiedades para habilitar o
    ///     deshabilitar automáticamente la ejecución del comando.
    /// </summary>
    public class ObservingCommand : ICommand
    {
        [NotNull] private readonly Action<object> _action;
        [CanBeNull] private readonly Func<object, bool> _canExecute;
        [CanBeNull] private readonly HashSet<string> _properties;

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action action) : this(observedSource, action, (Func<object, bool>)null,(IEnumerable<string>)null) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action action, [CanBeNull] Func<object, bool> canExecute, params string[] propsToListen) : this(observedSource, p => action(), canExecute,propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action action, [CanBeNull] Func<bool> canExecute, params string[] propsToListen) : this(observedSource, p => action(), canExecute,propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action action, [CanBeNull] Func<bool> canExecute, [CanBeNull, ItemNotNull] IEnumerable<string> propsToListen) : this(observedSource, p => action(), canExecute, propsToListen) { }
       
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action action, [CanBeNull] Func<object, bool> canExecute, [CanBeNull, ItemNotNull] IEnumerable<string> propsToListen) : this(observedSource, p => action(), canExecute,propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action<object> action) : this(observedSource, action, (Func<object, bool>)null, (IEnumerable<string>)null) { }
        
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action<object> action, [CanBeNull] Func<bool> canExecute, params string[] propsToListen) : this(observedSource, action, p => canExecute?.Invoke() ?? true, canExecute is null ? propsToListen?.AsEnumerable() : null) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action<object> action, [CanBeNull] Func<bool> canExecute, [CanBeNull, ItemNotNull] IEnumerable<string> propsToListen) : this(observedSource, action, p => canExecute?.Invoke() ?? true, canExecute is null ? propsToListen : null) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.ViewModel.ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action<object> action, [CanBeNull] Func<object, bool> canExecute, params string[] propsToListen) : this(observedSource, action, canExecute, propsToListen.AsEnumerable()) { }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ObservingCommand"/>.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        ///     Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        ///     Lista de propiedades a escuchar. Si no se establece ningún
        ///     valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand([NotNull] INotifyPropertyChanged observedSource, [NotNull] Action<object> action, [CanBeNull] Func<object, bool> canExecute, [CanBeNull, ItemNotNull] IEnumerable<string> propsToListen)
        {
            ObservedSource = observedSource ?? throw new ArgumentNullException(nameof(observedSource));
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = canExecute;

            var toListen = propsToListen as string[] ?? propsToListen?.ToArray();
#if PreferExceptions
            if (_canExecute is null)
            {
                if (toListen?.Any() ?? false) throw new InvalidArgumentException(nameof(propsToListen));
                return;
            }
            if (toListen.AnyEmpty(out int index)) throw new InvalidEnumArgumentException(nameof(propsToListen),index,typeof(string));
#else
            if (_canExecute is null) return;
#endif
            _properties = toListen?.Any() ?? false ? new HashSet<string>(toListen) : null;
            observedSource.PropertyChanged += RaiseCanExecuteChanged;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Se produce cuando hay cambios que influyen en si el comando
        ///     debería ejecutarse o no.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Referencia al origen de datos observado por este <see cref="ObservingCommand"/>.
        /// </summary>
        [NotNull]
        public INotifyPropertyChanged ObservedSource { get; }

        /// <summary>
        ///     Enumera las propiedades que están siendo observadas por este <see cref="ObservingCommand"/>.
        /// </summary>
        [CanBeNull, ItemNotNull] public IEnumerable<string> ObservedProperties => _properties;

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
        public bool CanExecute([CanBeNull] object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Define el método al que se llamará cuando se invoque el comando.
        /// </summary>
        /// <param name="parameter">
        ///     Datos que usa el comando. Si el comando no exige pasar los
        ///     datos, se puede establecer este objeto en
        ///     <see langword="null" />.
        /// </param>
        public void Execute([CanBeNull] object parameter)
        {
            _action(parameter);
        }

        private void RaiseCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_properties?.Contains(e.PropertyName) ?? true) CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}