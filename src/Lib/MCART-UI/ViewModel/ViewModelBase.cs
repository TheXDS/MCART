/*
ViewModelBase.cs

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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources.UI;
using TheXDS.MCART.Types.Base;
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

        private void OnInvokeObservedProps(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
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
        /// 
        protected void Observe<T>(Expression<Func<T>> propertySelector, Action handler)
        {
            Observe_Contract(propertySelector, handler);
            try
            {
                Observe(ReflectionHelpers.GetProperty(propertySelector)!.Name, handler);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidArgumentException(St.UnsupportedMember, ex, nameof(propertySelector));
            }
            catch { throw; }
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
        /// <exception cref="MissingMemberException">
        /// Se produce si la instancia a configurar no contiene una propiedad
        /// con el nombre especificado.
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
}
