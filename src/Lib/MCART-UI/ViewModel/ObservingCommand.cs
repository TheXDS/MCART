/*
ObservingCommand.cs

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources.UI;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    /// Describe un comando que observa a un objeto que implemente
    /// <see cref="INotifyPropertyChanged" /> y
    /// escucha cualquier cambio ocurrido en el valor de sus propiedades
    /// para habilitar o deshabilitar automáticamente la ejecución del
    /// comando.
    /// </summary>
    public class ObservingCommand : CommandBase
    {
        private Func<INotifyPropertyChanged, object?, bool>? _canExecute;
        private readonly HashSet<string> _properties = new();

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action) : this(observedSource, (_) => action()) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Func<Task> task) : this(observedSource, (Action<object?>)(async (_) => await task())) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Func<object?, Task> task) : this(observedSource, (Action<object?>)(async (object? o) => await task(o))) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action) : base(action)
        {
            ObservedSource = NullChecked(observedSource, nameof(observedSource));
        }

        /// <summary>
        /// Referencia al origen de datos observado por este <see cref="ObservingCommand"/>.
        /// </summary>
        public INotifyPropertyChanged ObservedSource { get; }

        /// <summary>
        /// Enumera las propiedades que están siendo observadas por este <see cref="ObservingCommand"/>.
        /// </summary>
        public IEnumerable<string> ObservedProperties => _properties.ToArray();

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
            return _canExecute?.Invoke(ObservedSource, parameter) ?? true;
        }

        /// <summary>
        /// Registra una nueva propiedad a observar en este comando.
        /// </summary>
        /// <param name="properties">
        /// Nombre(s) de la(s) propiedad(es) a observar.
        /// </param>
        /// <returns>
        /// Esta misma instancia, lo que permite usar esta función con
        /// sintaxis fluent.
        /// </returns>
        public ObservingCommand RegisterObservedProperty(params string[] properties)
        {
            foreach (var j in properties.NotEmpty())
            {
                _properties.Add(j);
            }
            return this;
        }

        /// <summary>
        /// Registra una nueva propiedad a observar en este comando.
        /// </summary>
        /// <param name="property">Nombre de la propiedad a observar.</param>
        /// <returns>
        /// Esta misma instancia, lo que permite usar esta función con
        /// sintaxis fluent.
        /// </returns>
        public ObservingCommand RegisterObservedProperty(Expression<Func<object?>> property)
        {
            var prop = (ReflectionHelpers.GetMember(property) as PropertyInfo) ?? throw new ArgumentException(null, nameof(property));
            RegisterObservedProperty(prop.Name);
            return this;
        }

        /// <summary>
        /// Establece la función de comprobación a ejecutar cuando se desee
        /// saber si es posible ejecutar el comando.
        /// </summary>
        /// <param name="canExecute">
        /// Función a ejecutar para determinar la posibilidad de ejecutar
        /// el comando.
        /// </param>
        /// <returns>
        /// Esta misma instancia, lo que permite usar esta función con
        /// sintaxis fluent.
        /// </returns>
        public ObservingCommand SetCanExecute(Func<bool> canExecute)
        {
            NullCheck(canExecute, nameof(canExecute));
            return SetCanExecute(_ => canExecute());
        }

        /// <summary>
        /// Establece la función de comprobación a ejecutar cuando se desee
        /// saber si es posible ejecutar el comando.
        /// </summary>
        /// <param name="canExecute">
        /// Función a ejecutar para determinar la posibilidad de ejecutar
        /// el comando.
        /// </param>
        /// <returns>
        /// Esta misma instancia, lo que permite usar esta función con
        /// sintaxis fluent.
        /// </returns>
        public ObservingCommand SetCanExecute(Func<object?, bool> canExecute)
        {
            NullCheck(canExecute, nameof(canExecute));
            return SetCanExecute((_, o) => canExecute(o));
        }

        /// <summary>
        /// Establece la función de comprobación a ejecutar cuando se desee
        /// saber si es posible ejecutar el comando.
        /// </summary>
        /// <param name="canExecute">
        /// Función a ejecutar para determinar la posibilidad de ejecutar
        /// el comando.
        /// </param>
        /// <returns>
        /// Esta misma instancia, lo que permite usar esta función con
        /// sintaxis fluent.
        /// </returns>
        public ObservingCommand SetCanExecute(Func<INotifyPropertyChanged, object?, bool>? canExecute)
        {
            if (canExecute is null)
            {
                ObservedSource.PropertyChanged -= RaiseCanExecuteChanged;
            }
            else if (_canExecute is null)
            {
                ObservedSource.PropertyChanged += RaiseCanExecuteChanged;
            }
            _canExecute = canExecute;
            return this;
        }

        /// <summary>
        /// Desconecta la función establecida para comprobar la posibilidad
        /// de ejecutar este comando.
        /// </summary>
        public void UnsetCanExecute() => SetCanExecute((Func<INotifyPropertyChanged, object?, bool>?)null);

        private void RaiseCanExecuteChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_properties.Contains(e.PropertyName ?? throw Errors.NullValue("e.PropertyName"))) RaiseCanExecuteChanged();
        }
    }
}