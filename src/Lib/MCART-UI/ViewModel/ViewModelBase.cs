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

using System.Collections.Generic;
using System.Reflection;
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
