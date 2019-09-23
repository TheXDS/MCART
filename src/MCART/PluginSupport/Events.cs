/*
Events.cs

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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TheXDS.MCART.PluginSupport.Legacy
{
    /// <inheritdoc />
    /// <summary>
    /// Incluye información adicional del evento 
    /// <see cref="E:TheXDS.MCART.PluginSupport.IPlugin.PluginFinalizing" />.
    /// </summary>
    [Serializable]
    public sealed class PluginFinalizingEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Determina el motivo de finalización del <see cref="IPlugin"/>.
        /// </summary>
        public enum FinalizingReason
        {
            /// <summary>
            /// Cierre normal.
            /// </summary>
            Shutdown,
            /// <summary>
            /// Cierre debido a un error.
            /// </summary>
            Error,
            /// <summary>
            /// Cierre por liberación de recursos.
            /// </summary>
            Disposal,
            /// <summary>
            /// La aplicación se está cerrando.
            /// </summary>
            AppClosing
        }
        /// <summary>
        /// Indica la razón por la cual se está finalizando el 
        /// <see cref="IPlugin"/>.
        /// </summary>
        public FinalizingReason Reason { get; }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginFinalizingEventArgs" />.
        /// </summary>
        internal PluginFinalizingEventArgs() : this(FinalizingReason.Shutdown) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginFinalizingEventArgs" />.
        /// </summary>
        /// <param name="reason">
        /// Establece la cual se está finalizando este plugin.
        /// </param>
        internal PluginFinalizingEventArgs(FinalizingReason reason) { Reason = reason; }
    }
    /// <inheritdoc />
    /// <summary>
    /// Incluye información adicional del evento 
    /// <see cref="E:TheXDS.MCART.PluginSupport.IPlugin.UiChanged" />
    /// </summary>
    public sealed class UiChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Obtiene la nueva interfaz que el <see cref="IPlugin"/> ha 
        /// solicitado.
        /// </summary>
        public ReadOnlyCollection<InteractionItem> NewUi { get; }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UiChangedEventArgs" />.
        /// </summary>
        /// <param name="newUi">Nueva interfaz de usuario solicitada.</param>
        internal UiChangedEventArgs(IList<InteractionItem> newUi)
        {
            NewUi = new ReadOnlyCollection<InteractionItem>(newUi);
        }
    }
}