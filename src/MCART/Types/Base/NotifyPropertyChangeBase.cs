/*
NotifyPropertyChanged.cs

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
using System.Linq;
using System.Reflection;

namespace TheXDS.MCART.Types.Base
{
    public abstract class NotifyPropertyChangeBase
    {
        private Action<string> _notifyCall;

        /// <summary>
        ///     Notifica desde un punto externo el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="properties">
        ///     Colección con los nombres de las propiedades a notificar.
        /// </param>
        public void Notify(params string[] properties)
        {
            foreach (var j in properties) _notifyCall(j);
        }

        /// <summary>
        ///     Notifica el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="property">
        ///     Propiedad a notificar.
        /// </param>
        protected abstract void Notify(string property);

        /// <summary>
        ///     Notifica desde un punto externo el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="properties">
        ///     Enumeración con los nombres de las propiedades a notificar.
        /// </param>
        public void Notify(IEnumerable<string> properties) => Notify(properties.ToArray());

        /// <summary>
        ///     Notifica desde un punto externo el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="properties">
        ///     Colección con las propiedades a notificar.
        /// </param>
        public void Notify(IEnumerable<PropertyInfo> properties) => Notify(properties.Select(p => p.Name));

        /// <summary>
        ///     Obliga a notificar que todas las propiedades de este objeto han
        ///     cambiado y necesitan refrescarse.
        /// </summary>
        public virtual void Refresh()
        {
            Notify(GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead));
        }
    }
}