﻿/*
PluginException.cs

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
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce cuando un <see cref="IPlugin" /> encuentra un error.
    /// </summary>
    [Serializable]
    public class PluginException : OffendingException<IPlugin>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException" />.
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
        protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException" />.
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
        /// <param name="plugin"><see cref="IPlugin" /> donde se ha generado la excepción.</param>
        protected PluginException(SerializationInfo info, StreamingContext context, IPlugin plugin) : base(info,
            context, plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException" />.
        /// </summary>
        public PluginException() : base(Msg())
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException" />.
        /// </summary>
        /// <param name="plugin"><see cref="IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(IPlugin plugin) : base(Msg(plugin), plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public PluginException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="plugin"><see cref="IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(string message, IPlugin plugin) : base(message, plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(Exception inner, IPlugin plugin) : base(Msg(plugin), inner, plugin)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(string message, Exception inner, IPlugin plugin) : base(message, inner, plugin)
        {
        }

        private static string Msg()
        {
            return Strings.XFoundError(Strings.ThePlugin);
        }

        private static string Msg(IPlugin plugin)
        {
            return Strings.XFoundError(Strings.XYQuotes(Strings.ThePlugin, plugin.Name));
        }
    }
}