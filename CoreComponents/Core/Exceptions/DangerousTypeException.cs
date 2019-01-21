/*
DangerousTypeException.cs

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
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se intenta utilizar una clase marcada con el atributo
    ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
    /// </summary>
    [Serializable]
    public class DangerousTypeException : OffendingException<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
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
        protected DangerousTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
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
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        protected DangerousTypeException(SerializationInfo info, StreamingContext context, Type dangerousType) : base(
            info, context, dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        public DangerousTypeException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(Type dangerousType) : base(Msg(dangerousType), dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public DangerousTypeException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(string message, Type dangerousType) : base(message, dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousTypeException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(Exception inner, Type dangerousType) : base(Msg(dangerousType), inner,
            dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(string message, Exception inner, Type dangerousType) : base(message, inner,
            dangerousType)
        {
        }

        private static string Msg()
        {
            return Strings.ClassIsDangerous(Strings.Unk);
        }

        private static string Msg(Type dangerousType)
        {
            return Strings.ClassIsDangerous(dangerousType.Name);
        }
    }
}