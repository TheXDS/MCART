/*
PluginClassNotFoundException.cs

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

#region Configuración de ReSharper

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

using System;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando un <see cref="Reflection.Assembly" /> no contiene la clase especificada.
    /// </summary>
    [Serializable]
    public class PluginClassNotFoundException : OffendingException<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PluginClassNotFoundException" />.
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
        protected PluginClassNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PluginClassNotFoundException" />.
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
        /// <param name="requiredType">Tipo que fue requerido.</param>
        protected PluginClassNotFoundException(SerializationInfo info, StreamingContext context, Type requiredType) :
            base(info, context, requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PluginClassNotFoundException" />.
        /// </summary>
        public PluginClassNotFoundException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(Type requiredType) : base(Msg(requiredType), requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public PluginClassNotFoundException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(string message, Type requiredType) : base(message, requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginClassNotFoundException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(Exception inner, Type requiredType) : base(Msg(requiredType), inner,
            requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginClassNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(string message, Exception inner, Type requiredType) : base(message, inner,
            requiredType)
        {
        }

        private static string Msg()
        {
            return Strings.XDoesntContainY(Strings.ThePlugin, Strings.TheClass.ToLower());
        }

        private static string Msg(Type type)
        {
            return Strings.XDoesntContainY(Strings.ThePlugin, Strings.XYQuotes(Strings.TheClass.ToLower(), type.Name));
        }
    }
}