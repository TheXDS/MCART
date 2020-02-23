/*
ObservingCommand.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Windows.Input;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.ViewModel
{
    /// <inheritdoc />
    /// <summary>
    /// Describe un comando que observa a un objeto que implemente
    /// <see cref="INotifyPropertyChanged" /> y
    /// escucha cualquier cambio ocurrido en el valor de sus propiedades
    /// para habilitar o deshabilitar automáticamente la ejecución del
    /// comando.
    /// </summary>
    public class ObservingCommand : CommandBase
    {
        private Func<object?, bool>? _canExecute;
        private readonly HashSet<string> _properties;

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action) : this(observedSource, action, (Func<object?, bool>?)null, (IEnumerable<string>?)null) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<object?, bool>? canExecute, params string[] propsToListen)
            : this(observedSource, _ => action(), canExecute, propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<bool>? canExecute, params string[] propsToListen)
            : this(observedSource, _ => action(), canExecute, propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<bool>? canExecute, IEnumerable<string>? propsToListen)
            : this(observedSource, _ => action(), canExecute, propsToListen) { }
       
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<object?, bool>? canExecute, IEnumerable<string>? propsToListen)
            : this(observedSource, _ => action(), canExecute, propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<object?, bool>? canExecute, params Expression<Func<object?>>[] propsToListen)
            : this(observedSource, _ => action(), canExecute, propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<bool> canExecute, params Expression<Func<object?>>[] propsToListen) 
            : this(observedSource, _ => action(), _=> canExecute(), propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<bool> canExecute, IEnumerable<Expression<Func<object?>>>? propsToListen)
            : this(observedSource, _ => action(), _ => canExecute(), propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action action, Func<object?, bool>? canExecute, IEnumerable<Expression<Func<object?>>>? propsToListen)
            : this(observedSource, _ => action(), canExecute, propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task) : this(observedSource, _ => task.GetAwaiter().GetResult()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<object?, bool>? canExecute, params string[] propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), canExecute, propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<bool>? canExecute, params string[] propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), canExecute, propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<bool>? canExecute, IEnumerable<string>? propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), canExecute, propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<object?, bool>? canExecute, IEnumerable<string>? propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), canExecute, propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<object?, bool>? canExecute, params Expression<Func<object?>>[] propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), canExecute, propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<bool> canExecute, params Expression<Func<object?>>[] propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), _ => canExecute(), propsToListen.AsEnumerable()) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<bool> canExecute, IEnumerable<Expression<Func<object?>>>? propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), _ => canExecute(), propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Task task, Func<object?, bool>? canExecute, IEnumerable<Expression<Func<object?>>>? propsToListen)
            : this(observedSource, _ => task.GetAwaiter().GetResult(), canExecute, propsToListen) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action) : this(observedSource, action, (Func<object?, bool>?)null, (IEnumerable<string>?)null) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<bool>? canExecute, params string[] propsToListen)
            : this(observedSource, action, _ => canExecute?.Invoke() ?? true, canExecute is null ? propsToListen?.AsEnumerable() : null) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<bool>? canExecute, IEnumerable<string>? propsToListen)
            : this(observedSource, action, _ => canExecute?.Invoke() ?? true, canExecute is null ? propsToListen : null) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<bool>? canExecute, params Expression<Func<object?>>[] propsToListen)
            : this(observedSource, action, _ => canExecute?.Invoke() ?? true, canExecute is null ? propsToListen?.AsEnumerable() : null) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<bool>? canExecute, IEnumerable<Expression<Func<object?>>>? propsToListen)
            : this(observedSource, action, _ => canExecute?.Invoke() ?? true, canExecute is null ? propsToListen : null) { }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand" />.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<object?, bool>? canExecute, params string[] propsToListen)
            : this(observedSource, action, canExecute, propsToListen.AsEnumerable()) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand"/>.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<object?, bool>? canExecute, params Expression<Func<object?>>[] propsToListen)
            : this(observedSource, action, canExecute, propsToListen.AsEnumerable())
        { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand"/>.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<object?, bool>? canExecute, IEnumerable<Expression<Func<object?>>>? propsToListen)
            : this(observedSource, action, canExecute, propsToListen.Select(p => ExpressionToString(observedSource, p)))
        { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ObservingCommand"/>.
        /// </summary>
        /// <param name="observedSource">Origen de datos observado.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="canExecute">
        /// Función que determina si el comando puede ejecutarse o no.
        /// </param>
        /// <param name="propsToListen">
        /// Lista de propiedades a escuchar. Si no se establece ningún
        /// valor, se escuchará el cambio de todas las propiedades.
        /// </param>
        public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action, Func<object?, bool>? canExecute, IEnumerable<string>? propsToListen) : base(action)
        {
            ObservedSource = observedSource ?? throw new ArgumentNullException(nameof(observedSource));
            _canExecute = canExecute;

            var toListen = propsToListen as string[] ?? propsToListen?.ToArray();
#if PreferExceptions
            if (_canExecute is null)
            {
                if (toListen?.Any() ?? false) throw new InvalidArgumentException(nameof(propsToListen));
                return;
            }
            if (toListen.AnyEmpty(out int index)) throw new InvalidEnumArgumentException(nameof(propsToListen),index,typeof(string));
#endif
            _properties = toListen?.Any() ?? false ? new HashSet<string>(toListen!) : new HashSet<string>();
            if (_canExecute is null) return;
            observedSource.PropertyChanged += RaiseCanExecuteChanged;
        }

        /// <summary>
        /// Referencia al origen de datos observado por este <see cref="ObservingCommand"/>.
        /// </summary>
        public INotifyPropertyChanged ObservedSource { get; }

        /// <summary>
        /// Enumera las propiedades que están siendo observadas por este <see cref="ObservingCommand"/>.
        /// </summary>
        public IEnumerable<string> ObservedProperties => _properties;

        /// <inheritdoc />
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
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// Registra una nueva propiedad a observar en este comando.
        /// </summary>
        /// <param name="property">Nombre de la propiedad a observar.</param>
        /// <returns>
        /// Esta misma instancia, lo que permite usar esta función con
        /// sintaxis fluent.
        /// </returns>
        public ObservingCommand RegisterObservedProperty(string property)
        {
            if (property is null) throw new ArgumentNullException(nameof(property));
            if (property.IsEmpty()) throw new ArgumentException();
            _properties.Add(property);
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
            var prop = (ReflectionHelpers.GetMember(property) as PropertyInfo) ?? throw new ArgumentException();
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
        public ObservingCommand SetCanExecute(Func<object?, bool>? canExecute)
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
            if (canExecute is null) throw new ArgumentNullException();
            SetCanExecute(_ => canExecute());
            return this;
        }

        /// <summary>
        /// Desconecta la función establecida para comprobar la posibilidad
        /// de ejecutar este comando.
        /// </summary>
        public void UnsetCanExecute() => SetCanExecute((Func<object?, bool>?)null);

        private void RaiseCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_properties.Contains(e.PropertyName)) RaiseCanExecuteChanged();
        }

        private static string ExpressionToString(object instance, Expression<Func<object?>> exp)
        {
            var prop = (ReflectionHelpers.GetMember(exp) as PropertyInfo) ?? throw new ArgumentException();
            if (!instance.GetType().GetProperties().Contains(prop)) throw new MissingMemberException();
            return prop.Name;
        }
    }
}