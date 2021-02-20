/*
ViewModelBase.cs

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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources.UI;
using TheXDS.MCART.Types.Base;
using System.Linq;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;
using St = TheXDS.MCART.Resources.UI.ErrorStrings;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    /// Clase base para la creación de ViewModels.
    /// </summary>
    public abstract partial class ViewModelBase : NotifyPropertyChanged, IViewModel
    {
        private bool _isBusy;
        private readonly Dictionary<string, ICollection<Action>> _observeRegistry = new Dictionary<string, ICollection<Action>>();

        private void OnInvokeObservedProps(object? sender, PropertyChangedEventArgs e)
        {
            if (_observeRegistry.TryGetValue(e.PropertyName ?? throw Errors.NullValue("e.PropertyName"), out var c))
            {
                foreach (var j in c)
                {
                    j.Invoke();
                }
            }
        }

        /// <summary>
        /// Inicialica una nueva instancia de la clase 
        /// <see cref="ViewModelBase"/>.
        /// </summary>
        protected ViewModelBase()
        {
            PropertyChanged += OnInvokeObservedProps;
        }

        /// <summary>
        /// Registra una propiedad con notificación de cambio de valor para ser
        /// observada y manejada por el delegado especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de la propiedad.</typeparam>
        /// <param name="propertySelector">
        /// Función selectora de la propiedad a observar.
        /// </param>
        /// <param name="handler">
        /// Delegado a invocar cuando la propiedad haya cambiado.
        /// </param>
        /// <exception cref="InvalidArgumentException">
        /// Se produce si la función de selección de propiedad no ha
        /// seleccionado un miembro válido de la instancia a configurar.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="propertySelector"/> o
        /// <paramref name="handler"/> son <see langword="null"/>.
        /// </exception>
        protected void Observe<T>(Expression<Func<T>> propertySelector, Action handler)
        {
            Observe_Contract(propertySelector);
            ObserveFrom(this, ReflectionHelpers.GetProperty(propertySelector), handler);
        }

        /// <summary>
        /// Registra una propiedad con notificación de cambio de valor para ser
        /// observada y manejada por el delegado especificado.
        /// </summary>
        /// <param name="propertyName">
        /// Nombre de la propiedad a observar.
        /// </param>
        /// <param name="handler">
        /// Delegado a invocar cuando la propiedad haya cambiado.
        /// </param>
        /// <exception cref="InvalidArgumentException">
        /// Se produce si <paramref name="propertyName"/> es una cadena vacía o
        /// una cadena de espacios.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="propertyName"/> o 
        /// <paramref name="handler"/> son  <see langword="null"/>.
        /// </exception>
        protected void Observe(string propertyName, Action handler)
        {
            Observe_Contract(propertyName, handler);
            if (!_observeRegistry.ContainsKey(propertyName))
            {
                _observeRegistry.Add(propertyName, new HashSet<Action>());
            }
            _observeRegistry[propertyName].Add(handler);
        }

        /// <summary>
        /// Registra una propiedad con notificación de cambio de valor para ser
        /// observada y manejada por el delegado especificado.
        /// </summary>
        /// <typeparam name="T">Tipo de la propiedad.</typeparam>
        /// <param name="source">Origen observado.</param>
        /// <param name="propertySelector">
        /// Función selectora de la propiedad a observar.
        /// </param>
        /// <param name="handler">
        /// Delegado a invocar cuando la propiedad haya cambiado.
        /// </param>
        /// <exception cref="InvalidArgumentException">
        /// Se produce si la función de selección de propiedad no ha
        /// seleccionado un miembro válido de la instancia a configurar.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="source"/>,
        /// <paramref name="propertySelector"/> o <paramref name="handler"/>
        /// son <see langword="null"/>.
        /// </exception>
        protected void ObserveFrom<T>(T source, Expression<Func<T, object?>> propertySelector, Action handler) where T : notnull, INotifyPropertyChanged
        {
            Observe_Contract(propertySelector);
            ObserveFrom(source, ReflectionHelpers.GetProperty(propertySelector), handler);
        }

        /// <summary>
        /// Registra una propiedad con notificación de cambio de valor para ser
        /// observada y manejada por el delegado especificado.
        /// </summary>
        /// <param name="source">Origen observado.</param>
        /// <param name="property">Propiedad a observar.</param>
        /// <param name="handler">
        /// Delegado a invocar cuando la propiedad haya cambiado.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="source"/>,
        /// <paramref name="property"/> o <paramref name="handler"/> son 
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="MissingMemberException">
        /// Se produce si la propiedad no ha sido encontrada en la instancia a
        /// configurar.
        /// </exception>
        protected void ObserveFrom(INotifyPropertyChanged source, PropertyInfo property, Action handler)
        {
            Observe_Contract(source, property);
            Observe(property.Name, handler);
        }

        /// <summary>
        /// Ejecuta una acción controlando automáticamente el estado de
        /// 'ocupado' de este ViewModel.
        /// </summary>
        /// <param name="action">Acción a ejecutar.</param>
        protected void BusyOp(Action action)
        {
            BusyOp_Contract(action);
            IsBusy = true;
            action.Invoke();
            IsBusy = false;
        }

        /// <summary>
        /// Ejecuta una tearea controlando automáticamente el estado de
        /// 'ocupado' de este ViewModel.
        /// </summary>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear la
        /// operación asíncrona.
        /// </returns>
        protected async Task BusyOp(Task task)
        {
            BusyOp_Contract(task);
            IsBusy = true;
            await task;
            IsBusy = false;
        }

        /// <summary>
        /// Ejecuta una función controlando automáticamente el estado de
        /// 'ocupado' de este ViewModel
        /// </summary>
        /// <typeparam name="T">Tipo de resultado de la función.</typeparam>
        /// <param name="func">Función a ejecutar.</param>
        /// <returns>
        /// El resultado de ejecutar la función especificada.
        /// </returns>
        protected T BusyOp<T>(Func<T> func)
        {
            BusyOp_Contract(func);
            IsBusy = true;
            var result = func.Invoke();
            IsBusy = false;
            return result;
        }

        /// <summary>
        /// Ejecuta una tarea que devuelve un resultado controlando
        /// automáticamente el estado de 'ocupado' de este ViewModel.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de resultado devuelto por la tarea.
        /// </typeparam>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear la
        /// operación asíncrona.
        /// </returns>
        protected async Task<T> BusyOp<T>(Task<T> task)
        {
            BusyOp_Contract(task);
            IsBusy = true;
            var result = await task;
            IsBusy = false;
            return result;
        }

        /// <summary>
        /// Obtiene un valor que indica si este <see cref="ViewModelBase"/>
        /// está ocupado.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            protected set => Change(ref _isBusy, value);
        }

        /// <summary>
        /// Destruye esta instancia de la clase <see cref="ViewModelBase"/>.
        /// </summary>
        ~ViewModelBase()
        {
            PropertyChanged -= OnInvokeObservedProps;

        }
    }

    /// <summary>
    /// Clase base que permite definir un ViewModel que provee de servicios de
    /// formulario y validación de datos.
    /// </summary>
    public abstract class FormViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private interface IValidationEntry
        {
            IEnumerable<string> Check(object? value);
            PropertyInfo Property { get; }
        }

        /// <summary>
        /// Define una serie de miembros disponibles para configurar una regla
        /// de validación.
        /// </summary>
        /// <typeparam name="T">Tipo de propiedad.</typeparam>
        protected interface IValidationEntry<T>
        {
            /// <summary>
            /// Agrega una regla de validación al registro.
            /// </summary>
            /// <param name="rule">
            /// FUnción que realiza la validación. Debe devolver 
            /// <see langword="true"/> si una propiedad supera la prueba,
            /// <see langword="false"/> en caso contrario.
            /// </param>
            /// <param name="error">
            /// Mensaje de error a presentar si la validación falla.
            /// </param>
            /// <returns>
            /// Esta misma regla, permitiendo el uso de sintaxis Fluent.
            /// </returns>
            IValidationEntry<T> AddRule(Func<T, bool> rule, string error);

            /// <summary>
            /// Agrega una regla de validación al registro.
            /// </summary>
            /// <param name="rule">
            /// FUnción que realiza la validación. Debe devolver 
            /// <see langword="true"/> si una propiedad supera la prueba,
            /// <see langword="false"/> en caso contrario.
            /// </param>
            /// <returns>
            /// Esta misma regla, permitiendo el uso de sintaxis Fluent.
            /// </returns>
            IValidationEntry<T> AddRule(Func<T, bool> rule);
        }

        private class ValidationEntry<T> : IValidationEntry, IValidationEntry<T>
        {
            private record ValidationRule(Func<T, bool> Rule, string Error);

            private readonly List<ValidationRule> _rules = new List<ValidationRule>();

            public PropertyInfo Property { get; }

            internal ValidationEntry(PropertyInfo property)
            {
                Property = property;
            }

            IValidationEntry<T> IValidationEntry<T>.AddRule(Func<T, bool> rule, string error)
            {
                _rules.Add(new ValidationRule(rule,error));
                return this;
            }

            IValidationEntry<T> IValidationEntry<T>.AddRule(Func<T, bool> rule)
            {
                return ((IValidationEntry<T>)this).AddRule(rule, St.FieldRequired);
            }

            IEnumerable<string> IValidationEntry.Check(object? value)
            {
                return _rules.Where(p => !p.Rule(value is T v ? v : default!)).Select(p => p.Error);
            }
        }

        private readonly IDictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly List<IValidationEntry> _validationRules = new List<IValidationEntry>();
        private SimpleCommand[]? _validationAffectedCommands;

        /// <summary>
        /// Triggered whenever validations are run.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        /// <summary>
        /// Returns this instance, required for proper
        /// <see cref="INotifyPropertyChanged"/> implementation.
        /// </summary>
        public INotifyDataErrorInfo ErrorSource => this;

        /// <summary>
        /// Gets a value indicating whether this instance has any validation errors.
        /// </summary>
        public bool HasErrors => _errors.Any();

        /// <summary>
        /// Checks for validation errors.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all validations passed,
        /// <see langword="false"/> otherwise.
        /// </returns>
        public bool CheckErrors()
        {
            return _validationRules.Locked(p => {
                foreach (var j in p)
                {
                    AppendErrors(j, j.Property.GetValue(this));
                }
                return !HasErrors && p.Any();
            });
        }

        /// <summary>
        /// Enumerates the errors for the provided property name, or for all
        /// properties if <paramref name="propertyName"/> is
        /// <see langword="null"/>.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property for which to get the errors. If it is an empty
        /// string or <see langword="null"/>, this method will return all
        /// validation errors.
        /// </param>
        /// <returns>
        /// An enumeration of all validation errors for the specified property
        /// or for all properties.
        /// </returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName.IsEmpty())
            {
                return _errors.SelectMany(p => p.Value);
            }
            if (_errors.ContainsKey(propertyName) && _errors[propertyName].Any())
            {
                return _errors[propertyName];
            }
            return Array.Empty<string>();
        }

        /// <summary>
        /// Reemplaza el método 
        /// <see cref="NotifyPropertyChanged.Change{T}(ref T, T, string)"/>,
        /// permitiendo la ejecución de validaciones sobre una propiedad.
        /// </summary>
        /// <typeparam name="T">Type of backing field.</typeparam>
        /// <param name="backingStore">
        /// Field that holds the property value.
        /// </param>
        /// <param name="value">Value to be set.</param>
        /// <param name="propertyName">
        /// Name of the property. This parameter should be omitted always,
        /// unless you need to specify a different property to be notified.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the property did change its value,
        /// <see langword="false"/> otherwise.
        /// </returns>
        protected override bool Change<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (!base.Change(ref backingStore, value, propertyName)) return false;
            var prop = GetType().GetProperty(propertyName);
            var vr = _validationRules.FirstOrDefault(p => p.Property == prop);
            if (!(vr is null))
            {
                AppendErrors(vr, value);
                var act = (GetErrors(propertyName).ToGeneric().Any());
                foreach (var j in _validationAffectedCommands ?? Array.Empty<SimpleCommand>()) j.SetCanExecute(act);
            }
            return true;
        }

        /// <summary>
        /// Registers a property validation ruleset.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="propertySelector">
        /// Expression that selects the property to be configured.
        /// </param>
        /// <returns>
        /// An object which allows the configuration of the validation rules to
        /// be applied to the selected property.
        /// </returns>
        protected IValidationEntry<T> RegisterValidation<T>(Expression<Func<T>> propertySelector)
        {
            var r = new ValidationEntry<T>(ReflectionHelpers.GetProperty(propertySelector));
            _validationRules.Add(r);
            return r;
        }

        /// <summary>
        /// Tells to the validation engine that validation will affect the
        /// specified commands.
        /// </summary>
        /// <param name="commands">
        /// Collection of commands to be affected by validation failures.
        /// </param>
        /// <remarks>
        /// Please call this method after instancing the commands.
        /// </remarks>
        protected void ValidationAffects(params SimpleCommand[] commands)
        {
            _validationAffectedCommands = commands;
        }

        private void AppendErrors(IValidationEntry entry, object? value)
        {
            _errors.Remove(entry.Property.Name);
            foreach (var j in entry?.Check(value) ?? Array.Empty<string>())
            {
                if (_errors.ContainsKey(entry!.Property.Name))
                {
                    _errors[entry.Property.Name].Add(j);
                }
                else
                {
                    _errors.Add(entry.Property.Name, new List<string> { j });
                }
            }
            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(ErrorSource));
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(entry?.Property.Name));
        }
    }
}
