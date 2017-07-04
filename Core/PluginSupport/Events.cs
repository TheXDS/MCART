//
//  EventArgs.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace MCART.PluginSupport
{
    #region Delegados EventHandler
    /// <summary>
    /// Maneja el evento <see cref="IPlugin.PluginLoadFailed"/>.
    /// </summary>
    /// <param name="sender">Objeto que generó el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void PluginLoadFailedEventHandler(IPlugin sender, PluginFinalizedEventArgs e);
    /// <summary>
    /// Maneja el evento <see cref="IPlugin.PluginLoaded"/>.
    /// </summary>
    /// <param name="sender">Objeto que generó el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void PluginLoadedEventHandler(IPlugin sender, PluginLoadedEventArgs e);
    /// <summary>
    /// Maneja el evento <see cref="IPlugin.PluginFinalized"/>.
    /// </summary>
    /// <param name="sender">
    /// Este argumento siempre devolverá <c>null</c>.
    /// </param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void PluginFinalizedEventHandler(object sender, PluginFinalizedEventArgs e);
    /// <summary>
    /// Maneja el evento <see cref="IPlugin.PluginFinalizing"/>.
    /// </summary>
    /// <param name="sender">Objeto que generó el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void PluginFinalizingEventHandler(IPlugin sender, PluginFinalizingEventArgs e);
    /// <summary>
    /// Maneja el evento <see cref="IPlugin.UIChangeRequested"/>.
    /// </summary>
    /// <param name="sender">Objeto que generó el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public delegate void UIChangeRequestedEventHandler(IPlugin sender, UIChangeEventArgs e);
    #endregion
    /// <summary>
    /// Incluye información adicional del evento <see cref="IPlugin.PluginFinalizing"/>.
    /// </summary>
    [Serializable]
    public sealed class PluginFinalizingEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Determina el motivo de finalización del <see cref="Plugin"/>.
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
        /// Indica la razón por la cual se está finalizando el <see cref="Plugin"/>
        /// </summary>
        public readonly FinalizingReason Reason;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:MCART.PluginSupport.PluginFinalizingEventArgs"/>.
        /// </summary>
        /// <param name="reason">Parámetro opcional. Permite establecer una
        /// razón por la cual se está finalizando este plugin.</param>
        internal PluginFinalizingEventArgs(FinalizingReason reason = FinalizingReason.Shutdown)
        {
            Reason = reason;
        }
    }
    /// <summary>
    /// Incluye información adicional del evento
    /// <see cref="IPlugin.PluginFinalized"/>.
    /// </summary>
    [Serializable]
    public sealed class PluginFinalizedEventArgs : EventArgs
    {
        /// <summary>
        /// Obtiene la excepción que causó la finalización del Plugin
        /// </summary>
        /// <remarks>Se devolverá <c>null</c> si el <see cref="Plugin"/> ha
        /// finalizado correctamente, en caso contrario se devuelve la excepción
        /// que causó la finalización del <see cref="Plugin"/>.</remarks>
        public readonly Exception Exception;
        /// <summary>
        /// Obtiene un valor que indica si el <see cref="Plugin"/> finalizó
        /// correctamente.
        /// </summary>
        /// <value><c>true</c> si el <see cref="Plugin"/> finalizó
        /// correctamente; de lo contrario, <c>false</c>.</value>
        public bool OK => ReferenceEquals(Exception, null);
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginFinalizedEventArgs"/>.
        /// </summary>
        /// <param name="exception">Opcional. Excepción de finalización del
        /// <see cref="Plugin"/>. Si se omite o se establece en <c>null</c>,
        /// significa que el <see cref="Plugin"/> ha finalizado correctamente.
        /// </param>
        internal PluginFinalizedEventArgs(Exception exception = null)
        {
            Exception = exception;
        }
    }
    /// <summary>
    /// Incluye información adicional del evento 
    /// <see cref="IPlugin.PluginLoaded"/>.
    /// </summary>
    [Serializable]
    public sealed class PluginLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// Obtiene el tiempo que le ha tomado al <see cref="Plugin"/> ser
        /// cargado.
        /// </summary>
        public readonly TimeSpan LoadTime;
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:MCART.PluginSupport.PluginLoadedEventArgs"/>.
        /// </summary>
        /// <param name="t"><see cref="TimeSpan"/> que le ha tomado al
        /// <see cref="Plugin"/> ser cargado.</param>
        internal PluginLoadedEventArgs(TimeSpan t)
        {
            LoadTime = t;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:MCART.PluginSupport.PluginLoadedEventArgs"/>.
        /// </summary>
        /// <param name="ticks">Tiempo en ticks que le ha tomado al
        /// <see cref="Plugin"/> ser cargado.</param>
        internal PluginLoadedEventArgs(long ticks)
        {
            LoadTime = new TimeSpan(ticks);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:MCART.PluginSupport.PluginLoadedEventArgs"/>.
        /// </summary>
        /// <param name="milliseconds">Milisegundos que le ha tomado al
        /// <see cref="Plugin"/> ser cargado.</param>
        /// <param name="seconds">Opcional. Segundos que le ha tomado al
        /// <see cref="Plugin"/> ser cargado.</param>
        internal PluginLoadedEventArgs(int milliseconds, int seconds = 0)
        {
            LoadTime = new TimeSpan(0, 0, 0, seconds, milliseconds);
        }
    }
    /// <summary>
    /// Incluye información adicional del evento 
    /// <see cref="IPlugin.UIChangeRequested"/>
    /// </summary>
    public sealed class UIChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Obtiene la nueva interfaz que el <see cref="IPlugin"/> ha 
        /// solicitado.
        /// </summary>
        public readonly ReadOnlyCollection<InteractionItem> NewUI;
        internal UIChangeEventArgs(IList<InteractionItem> NewUI)
        {
            this.NewUI = new ReadOnlyCollection<InteractionItem>(NewUI);
        }
    }
}