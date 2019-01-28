/*
NotifyPropertyChange.cs

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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Annotations;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    ///     Clase base para los objetos que puedan notificar sobre el cambio
    ///     del valor de una de sus propiedades, tanto antes como después de
    ///     haber ocurrido dicho cambio.
    /// </summary>
    public abstract class NotifyPropertyChange : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <inheritdoc />
        /// <summary>
        ///     Se produce cuando se cambiará el valor de una propiedad.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <inheritdoc />
        /// <summary>
        ///     Ocurre cuando el valor de una propiedad ha cambiado.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Notifica a los clientes que el valor de una propiedad cambiará.
        /// </summary>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        ///     Notifica a los clientes que el valor de una propiedad ha
        ///     cambiado.
        /// </summary>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Cambia el valor de un campo, y genera los eventos de
        ///     notificación correspondientes.
        /// </summary>
        /// <typeparam name="T">Tipo de valores a procesar.</typeparam>
        /// <param name="field">Campo a actualizar.</param>
        /// <param name="value">Nuevo valor del campo.</param>
        /// <param name="propertyName">
        ///     Nombre de la propiedad. Por lo general, este valor debe
        ///     omitirse.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si el valor de la propiedad ha
        ///     cambiado, <see langword="false"/> en caso contrario.
        /// </returns>
        protected bool Change<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value)) return false;
            OnPropertyChanging(propertyName);
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}