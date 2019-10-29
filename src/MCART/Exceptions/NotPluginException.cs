/*
NotPluginException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Reflection;
using System.Runtime.Serialization;
using TheXDS.MCART.PluginSupport.Legacy;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se intenta cargar plugins desde un ensamblado que no contiene ninguna clase
    ///     cargable como <see cref="IPlugin" />.
    /// </summary>
    [Serializable]
    public class NotPluginException : OffendingException<Assembly>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="context">
        ///     El <see cref="StreamingContext" /> que contiene información
        ///     contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        ///     El <see cref="SerializationInfo" /> que contiene la información
        ///     serializada del objeto acerca de la excepción que está siendo
        ///     lanzada.
        /// </param>
        protected NotPluginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="context">
        ///     El <see cref="StreamingContext" /> que contiene información
        ///     contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        ///     El <see cref="SerializationInfo" /> que contiene la información
        ///     serializada del objeto acerca de la excepción que está siendo
        ///     lanzada.
        /// </param>
        /// <param name="assembly">
        ///     <see cref="Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="IPlugin" />.
        /// </param>
        protected NotPluginException(SerializationInfo info, StreamingContext context, Assembly assembly) : base(info,
            context, assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NotPluginException" />.
        /// </summary>
        public NotPluginException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="assembly">
        ///     <see cref="Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="IPlugin" />.
        /// </param>
        public NotPluginException(Assembly assembly) : base(Msg(assembly), assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public NotPluginException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="assembly">
        ///     <see cref="Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="IPlugin" />.
        /// </param>
        public NotPluginException(string message, Assembly assembly) : base(message, assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="assembly">
        ///     <see cref="Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="IPlugin" />.
        /// </param>
        public NotPluginException(Exception inner, Assembly assembly) : base(Msg(assembly), inner, assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="assembly">
        ///     <see cref="Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="IPlugin" />.
        /// </param>
        public NotPluginException(string message, Exception inner, Assembly assembly) : base(message, inner, assembly)
        {
        }

        private static string Msg()
        {
            return Strings.XIsInvalid(Strings.TheAssembly);
        }

        private static string Msg(Assembly assembly)
        {
            return Strings.XIsInvalid(Strings.XYQuotes(Strings.TheAssembly, assembly.FullName));
        }
    }
}