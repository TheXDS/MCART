/*
PluginInitializationException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be
useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Runtime.Serialization;
using TheXDS.MCART.PluginSupport.Legacy;
using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce cuando un plugin no pudo inicializarse.
    /// </summary>
    [Serializable]
    public class PluginInitializationException : OffendingException<IPlugin>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext" /> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo" /> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected PluginInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext" /> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo" /> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        /// <param name="plugin"><see cref="IPlugin" /> que no pudo inicializarse.</param>
        protected PluginInitializationException(SerializationInfo info, StreamingContext context, IPlugin plugin) :
            base(info, context, plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginInitializationException" />.
        /// </summary>
        public PluginInitializationException() : base(Strings.PluginDidntInit)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="plugin"><see cref="IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(IPlugin plugin) : base(Strings.PluginDidntInit, plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public PluginInitializationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="plugin"><see cref="IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(string message, IPlugin plugin) : base(message, plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginInitializationException(Exception inner) : base(Strings.PluginDidntInit, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(Exception inner, IPlugin plugin) : base(Strings.PluginDidntInit, inner, plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginInitializationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(string message, Exception inner, IPlugin plugin) : base(message, inner,
            plugin)
        {
        }
    }
}