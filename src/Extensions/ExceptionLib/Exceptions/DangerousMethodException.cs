/*
DangerousMethodException.cs

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
using System.Reflection;
using System.Runtime.Serialization;
using TheXDS.MCART.Attributes;
using static TheXDS.MCART.ExceptionLib.Resources.Strings;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce cuando un método ha sido marcado con el atributo
    /// <see cref="DangerousAttribute" />.
    /// </summary>
    [Serializable]
    public class DangerousMethodException : OffendingException<MethodInfo>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="DangerousMethodException" />.
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
        protected DangerousMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="DangerousMethodException" />.
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
        /// <param name="offendingMethod">
        /// Método marcado con el atributo
        /// <see cref="DangerousAttribute" />.
        /// </param>
        protected DangerousMethodException(SerializationInfo info, StreamingContext context, MethodInfo offendingMethod)
            : base(info, context, offendingMethod)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="DangerousMethodException" />.
        /// </summary>
        public DangerousMethodException() : base(Msg())
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="DangerousMethodException" />.
        /// </summary>
        /// <param name="offendingMethod">
        /// Método marcado con el atributo
        /// <see cref="DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(MethodInfo offendingMethod) : base(Msg(offendingMethod), offendingMethod)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public DangerousMethodException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingMethod">
        /// Método marcado con el atributo
        /// <see cref="DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(string message, MethodInfo offendingMethod) : base(message, offendingMethod)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DangerousMethodException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousMethodException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DangerousMethodException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">
        /// Método marcado con el atributo
        /// <see cref="DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(Exception inner, MethodInfo offendingMethod) : base(Msg(offendingMethod), inner,
            offendingMethod)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousMethodException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">
        /// Método marcado con el atributo
        /// <see cref="DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(string message, Exception inner, MethodInfo offendingMethod) : base(message,
            inner, offendingMethod)
        {
        }

        private static string Msg()
        {
            return MethodIsDangerous;
        }

        private static string Msg(MemberInfo offendingMethod)
        {
            return string.Format(MethodXIsDangerous, offendingMethod.Name);
        }
    }
}