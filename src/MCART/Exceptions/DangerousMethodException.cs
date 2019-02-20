﻿/*
DangerousMethodException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Reflection;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando un método ha sido marcado con el atributo
    ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
    /// </summary>
#if NETFX_CORE
    [DataContract]
#else
    [Serializable]
#endif
    public class DangerousMethodException : OffendingException<MethodInfo>
    {
#if !NETFX_CORE
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="context">
        ///     El <see cref="T:System.Runtime.Serialization.StreamingContext" /> que contiene información
        ///     contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        ///     El <see cref="T:System.Runtime.Serialization.SerializationInfo" /> que contiene la información
        ///     serializada del objeto acerca de la excepción que está siendo
        ///     lanzada.
        /// </param>
        protected DangerousMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="context">
        ///     El <see cref="T:System.Runtime.Serialization.StreamingContext" /> que contiene información
        ///     contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        ///     El <see cref="T:System.Runtime.Serialization.SerializationInfo" /> que contiene la información
        ///     serializada del objeto acerca de la excepción que está siendo
        ///     lanzada.
        /// </param>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        protected DangerousMethodException(SerializationInfo info, StreamingContext context, MethodInfo offendingMethod)
            : base(info, context, offendingMethod)
        {
        }
#endif

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        public DangerousMethodException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(MethodInfo offendingMethod) : base(Msg(offendingMethod), offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public DangerousMethodException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(string message, MethodInfo offendingMethod) : base(message, offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousMethodException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(Exception inner, MethodInfo offendingMethod) : base(Msg(offendingMethod), inner,
            offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousMethodException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(string message, Exception inner, MethodInfo offendingMethod) : base(message,
            inner, offendingMethod)
        {
        }

        private static string Msg()
        {
            return Strings.MethodIsDangerous(Strings.Unk);
        }

        private static string Msg(MemberInfo offendingMethod)
        {
            return Strings.MethodIsDangerous(offendingMethod.Name);
        }
    }
}