/*
NotifyPropertyChangeBase.cs

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
using System.Linq;
using System.Reflection;
using St = TheXDS.MCART.Resources.Strings;
using Ist = TheXDS.MCART.Resources.InternalStrings;
using static TheXDS.MCART.Types.Extensions.DictionaryExtensions;
using System.Collections.ObjectModel;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    ///     Clase base abstracta para todas las clases que implementen alguna
    ///     de las interfaces de notificación de propiedades disponibles en
    ///     .Net Framework / .Net Core.
    /// </summary>
    public abstract class NotifyPropertyChangeBase
    {
        private readonly IDictionary<string, IEnumerable<string>> _observeRegistry
            = new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NotifyPropertyChangeBase"/>.
        /// </summary>
        protected NotifyPropertyChangeBase()
        {
             ObserveRegistry= new ReadOnlyDictionary<string, IEnumerable<string>>(_observeRegistry);
        }
        /// <summary>
        ///     Registra un Broadcast de notificación de cambio de propiedad.
        /// </summary>
        /// <param name="property">
        ///     Propiedad a registrar.
        /// </param>
        /// <param name="affectedProperties">
        ///     Colección de propiedades a notificar cuando se cambie el valor
        ///     de esta propiedad.
        /// </param>
        protected void RegisterPropertyChangeBroadcast(string property, params string[] affectedProperties)
        {
            if (_observeRegistry.ContainsKey(property))
                throw new InvalidOperationException(Ist.ErrorXAlreadyRegistered(St.XYQuotes(St.TheProperty,property)));
            
            if (_observeRegistry.CheckCircularRef(property))
                throw new InvalidOperationException(Ist.ErrorCircularOperationDetected);

            _observeRegistry.Add(property, affectedProperties);
        }

        /// <summary>
        ///     Registra un Broadcast de notificación de cambio de propiedad.
        /// </summary>
        /// <param name="property">
        ///     Propiedad a registrar.
        /// </param>
        /// <param name="affectedProperties">
        ///     Colección de propiedades a notificar cuando se cambie el valor
        ///     de esta propiedad.
        /// </param>
        protected void RegisterPropertyChangeBroadcast(string property, IEnumerable<string> affectedProperties)
        {
            RegisterPropertyChangeBroadcast(property, affectedProperties.ToArray());
        }

        /// <summary>
        ///     Quita una entrada del registro de Broadcast de notificación de
        ///     cambio de propiedad.
        /// </summary>
        /// <param name="property">
        ///     Entrada a quitar del registro de Broadcast.
        /// </param>
        protected void UnregisterPropertyChangeBroadcast(string property)
        {
            if (_observeRegistry.ContainsKey(property))
                _observeRegistry.Remove(property);
        }

        /// <summary>
        ///     Obtiene el registro de broadcast de notificaciones de cambio de
        ///     propiedad.
        /// </summary>
        protected IReadOnlyDictionary<string, IEnumerable<string>> ObserveRegistry { get; }

        /// <summary>
        ///     Notifica desde un punto externo el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="properties">
        ///     Colección con los nombres de las propiedades a notificar.
        /// </param>
        public void Notify(params string[] properties)
        {
            foreach (var j in properties) Notify(j);
        }

        /// <summary>
        ///     Notifica el cambio en el valor de una propiedad.
        /// </summary>
        /// <param name="property">
        ///     Propiedad a notificar.
        /// </param>
        protected abstract void Notify(string property);

        /// <summary>
        ///     Ejecuta una propagación de notificación según el registro
        ///     integrado de notificaciones suscritas.
        /// </summary>
        /// <param name="property"></param>
        protected void NotifyRegistroir(string property)
        {
            if (_observeRegistry.ContainsKey(property))
            {
                foreach (var j in _observeRegistry[property])
                {
                    Notify(j);
                }
            }
        }

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