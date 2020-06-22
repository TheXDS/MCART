/*
NotifyPropertyChange.cs

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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Annotations;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Clase base para los objetos que puedan notificar sobre el cambio
    /// del valor de una de sus propiedades, tanto antes como después de
    /// haber ocurrido dicho cambio.
    /// </summary>
    public abstract class NotifyPropertyChange : NotifyPropertyChangeBase, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private readonly HashSet<WeakReference<PropertyChangeObserver>> _observeSubscriptions = new HashSet<WeakReference<PropertyChangeObserver>>();

        /// <summary>
        /// Se produce cuando se cambiará el valor de una propiedad.
        /// </summary>
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// Ocurre cuando el valor de una propiedad ha cambiado.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifica a los clientes que el valor de una propiedad cambiará.
        /// </summary>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null!)
        {
            if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Notifica a los clientes que el valor de una propiedad ha
        /// cambiado.
        /// </summary>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            if (propertyName is null) throw new ArgumentNullException(nameof(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            NotifyRegistroir(propertyName);
            foreach (var j in _forwardings) j.Notify(propertyName);
        }

        /// <summary>
        /// Cambia el valor de un campo, y genera los eventos de
        /// notificación correspondientes.
        /// </summary>
        /// <typeparam name="T">Tipo de valores a procesar.</typeparam>
        /// <param name="field">Campo a actualizar.</param>
        /// <param name="value">Nuevo valor del campo.</param>
        /// <param name="propertyName">
        /// Nombre de la propiedad. Por lo general, este valor debe
        /// omitirse.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el valor de la propiedad ha
        /// cambiado, <see langword="false"/> en caso contrario.
        /// </returns>
        protected override sealed bool Change<T>(ref T field, T value, [CallerMemberName] string propertyName = null!)
        {
            if (field?.Equals(value) ?? Objects.AreAllNull(field, value)) return false;

            var m = ReflectionHelpers.GetCallingMethod() ?? throw new TamperException();
            var p = GetType().GetProperties().SingleOrDefault(q => q.SetMethod == m)
                ?? throw new InvalidOperationException();
            if (p.Name != propertyName) throw new TamperException();

            OnPropertyChanging(propertyName);
            field = value;

            var rm = new HashSet<WeakReference<PropertyChangeObserver>>();
            foreach (var j in _observeSubscriptions)
            {
                if (j.TryGetTarget(out var t)) t.Invoke(this, p);
                else rm.Add(j);
            }
            foreach (var j in rm)
            {
                _observeSubscriptions.Remove(j);
            }
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Suscribe a un delegado para observar el cambio de una propiedad.
        /// </summary>
        /// <param name="callback">Delegado a suscribir.</param>
        public void Subscribe(PropertyChangeObserver callback)
        {
            _observeSubscriptions.Add(new WeakReference<PropertyChangeObserver>(callback));
        }

        /// <summary>
        /// Quita al delegado previamente suscrito de la lista de
        /// suscripciones de notificación de cambios de propiedad.
        /// </summary>
        /// <param name="callback">Delegado a quitar.</param>
        public void Unsubscribe(PropertyChangeObserver callback)
        {
            var rm = _observeSubscriptions.FirstOrDefault(p => p.TryGetTarget(out var t) && t == callback);
            if (!(rm is null)) _observeSubscriptions.Remove(rm);
        }

        /// <summary>
        /// Notifica el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="property">
        /// Propiedad a notificar.
        /// </param>
        protected override void Notify(string property)
        {
            OnPropertyChanged(property);
        }
    }

    /// <summary>
    /// Delegado que define un método para observar y procesar cambios en
    /// el valor de una propiedad asociada a un objeto.
    /// </summary>
    /// <param name="instance">
    /// Instancia del objeto a observar.
    /// </param>
    /// <param name="property">
    /// Propiedad observada.
    /// </param>
    public delegate void PropertyChangeObserver(object instance, PropertyInfo property);
}