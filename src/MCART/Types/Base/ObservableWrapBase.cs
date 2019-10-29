/*
ObservableWrap.cs

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

#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Nccha = System.Collections.Specialized.NotifyCollectionChangedAction;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    ///     Clase base para los envoltorios observables de colecciones.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class ObservableWrapBase : NotifyPropertyChanged, INotifyCollectionChanged, IEnumerable
    {
        private readonly IDictionary<NotifyPropertyChangeBase, HashSet<string>> _notifyRegistroir = new Dictionary<NotifyPropertyChangeBase, HashSet<string>>();

        /// <summary>
        ///     Se produce al ocurrir un cambio en la colección.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        ///     Genera el evento <see cref="CollectionChanged"/>.
        /// </summary>
        /// <param name="eventArgs">Argumentos del evento.</param>
        protected void RaiseCollectionChanged(NcchEa eventArgs)
        {
            CollectionChanged?.Invoke(this, eventArgs);
            foreach (var j in _notifyRegistroir)
            {
                j.Key.Notify(j.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => OnGetEnumerator();

        /// <summary>
        ///     Obtiene un enumerador que itera sobre la colección.
        /// </summary>
        /// <returns>
        ///     Un enumerador que puede ser utilizado para iterar sobre la colección.
        /// </returns>
        protected abstract IEnumerator OnGetEnumerator();

        /// <summary>
        ///     Obtiene el índice del elemento especificado.
        /// </summary>
        /// <param name="item">
        ///     Elemento para el cual buscar el índice.
        /// </param>
        /// <returns>
        ///     El índice del elemento especificado, o <c>-1</c> si el elemento
        ///     no se encuentra en la colección.
        /// </returns>
        public virtual int IndexOf(object item)
        {
            var c = 0;
            foreach (var j in this)
            {
                if (j?.Equals(item) ?? item is null) return c;
                c++;
            }
            return -1;
        }

        /// <summary>
        ///     Obliga a refrescar el estado de un elemento dentro de la
        ///     colección.
        /// </summary>
        /// <param name="item">
        ///     Elemento a refrescar.
        /// </param>
        public void RefreshItem(object? item)
        {
            if (item is null || !Contains(item)) return;
            switch (item)
            {
                case IRefreshable refreshable:
                    refreshable.Refresh();
                    break;
                default:
                    RaiseCollectionChanged(new NcchEa(Nccha.Replace, item, item, IndexOf(item)));
                    break;
            }
        }

        /// <summary>
        ///     Determina si la secuencia subyacente contiene al elemento
        ///     especificado.
        /// </summary>
        /// <param name="item">
        ///     Elemento a buscar dentro de la secuencia.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si la secuencia contiene al elemento
        ///     especificado, <see langword="false"/> en caso contrario.
        /// </returns>
        public virtual bool Contains(object item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        ///     Envía notificaciones adicionales de cambio de propiedad al
        ///     ocurrir un cambio en esta colección.
        /// </summary>
        /// <param name="target">
        ///     Objetivo de notificación.
        /// </param>
        /// <param name="properties">
        ///     Propiedades a notificar.
        /// </param>
        public void ForwardNotify(NotifyPropertyChangeBase target, params string[] properties)
        {
            if (!_notifyRegistroir.ContainsKey(target))
                _notifyRegistroir.Add(target, new HashSet<string>(properties));
            else
            {
                foreach (var j in properties)
                {
                    _notifyRegistroir[target].Add(j);
                }
            }
        }
    }
}