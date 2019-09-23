/*
ViewModelBase.cs

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
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     Clase base para la creación de ViewModels.
    /// </summary>
    public abstract class ViewModelBase : NotifyPropertyChanged
    {

        /// <summary>
        ///     Inicialica una nueva instancia de la clase 
        ///     <see cref="ViewModelBase"/>.
        /// </summary>
        public ViewModelBase() { }

        /// <summary>
        ///     Inicialica una nueva instancia de la clase 
        ///     <see cref="ViewModelBase"/>.
        /// </summary>
        /// <param name="observeSelf"></param>
        public ViewModelBase(bool observeSelf)
        {
            if (observeSelf)
            PropertyChanged += ViewModelBase_PropertyChanged;
        }

        private void ViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SelfObserve(GetType().GetProperty(e.PropertyName));
        }

        /// <summary>
        ///     Método invalidable que permite observar cambios en los valores de las propiedades de esta instancia.
        /// </summary>
        /// <param name="property"></param>
        protected virtual void SelfObserve(PropertyInfo property) { }

        /// <summary>
        ///     Ejecuta una acción controlando automáticamente el estado de
        ///     'ocupado' de este ViewModel.
        /// </summary>
        /// <param name="action">Acción a ejecutar.</param>
        protected void BusyOp(Action action)
        {
            IsBusy = true;
            action.Invoke();
            IsBusy = false;
        }

        /// <summary>
        ///     Ejecuta una tearea controlando automáticamente el estado de
        ///     'ocupado' de este ViewModel.
        /// </summary>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <returns>
        ///     Un <see cref="Task"/> que puede utilizarse para monitorear la
        ///     operación asíncrona.
        /// </returns>
        protected async Task BusyOp(Task task)
        {
            IsBusy = true;
            await task;
            IsBusy = false;
        }

        /// <summary>
        ///     Ejecuta una función controlando automáticamente el estado de
        ///     'ocupado' de este ViewModel
        /// </summary>
        /// <typeparam name="T">Tipo de resultado de la función.</typeparam>
        /// <param name="func">Función a ejecutar.</param>
        /// <returns>
        ///     El resultado de ejecutar la función especificada.
        /// </returns>
        protected T BusyOp<T>(Func<T> func)
        {
            IsBusy = true;
            var result = func.Invoke();
            IsBusy = false;
            return result;
        }

        /// <summary>
        ///     Ejecuta una tarea que devuelve un resultado controlando
        ///     automáticamente el estado de 'ocupado' de este ViewModel.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de resultado devuelto por la tarea.
        /// </typeparam>
        /// <param name="task">Tarea a ejecutar.</param>
        /// <returns>
        ///     Un <see cref="Task"/> que puede utilizarse para monitorear la
        ///     operación asíncrona.
        /// </returns>
        protected async Task<T> BusyOp<T>(Task<T> task)
        {
            IsBusy = true;
            var result = await task;
            IsBusy = false;
            return result;
        }

        private bool _isBusy;

        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="ViewModelBase"/>
        ///     está ocupado.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            protected set => Change(ref _isBusy, value);
        }

        /// <summary>
        ///     Destruye esta instancia de la clase 
        ///     <see cref="ViewModelBase"/>.
        /// </summary>
        ~ViewModelBase()
        {
            PropertyChanged -= ViewModelBase_PropertyChanged;
        }
    }
}
