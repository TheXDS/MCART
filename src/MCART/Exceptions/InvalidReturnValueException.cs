/*
InvalidReturnValueException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce cuando se detecta que una función ha devuelto
    /// un valor inválido sin generar una excepción.
    /// </summary>
    [Serializable]
    public class InvalidReturnValueException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" />.
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
        protected InvalidReturnValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" />.
        /// </summary>
        public InvalidReturnValueException() : base(Strings.XReturnedInvalid(Strings.TheFunc))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" /> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="call">
        /// <see cref="Delegate" /> cuyo resultado causó la excepción.
        /// </param>
        public InvalidReturnValueException(Delegate call) : base(
            Strings.XReturnedInvalid(Strings.XYQuotes(Strings.TheFunc, call.Method.Name)))
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" /> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        /// Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        public InvalidReturnValueException(string methodName) : base(
            Strings.XReturnedInvalid(Strings.XYQuotes(Strings.TheFunc, methodName)))
        {
            OffendingFunctionName = methodName;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" /> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="call">
        /// <see cref="Delegate" /> cuyo resultado causó la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por <paramref name="call" />.
        /// </param>
        public InvalidReturnValueException(Delegate call, object returnValue) : base(
            Strings.XReturnedInvalid(Strings.XYQuotes(Strings.TheFunc, call.Method.Name)))
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = returnValue;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" /> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        /// Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por el método.
        /// </param>
        public InvalidReturnValueException(string methodName, object returnValue) : base(
            Strings.XReturnedInvalid(Strings.XYQuotes(Strings.TheFunc, methodName)))
        {
            OffendingFunctionName = methodName;
            OffendingReturnValue = returnValue;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" /> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="call">
        /// <see cref="Delegate" /> cuyo resultado causó la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por <paramref name="call" />.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(Delegate call, object returnValue, Exception inner) : base(
            Strings.XReturnedInvalid(Strings.XYQuotes(Strings.TheFunc, call.Method.Name)), inner)
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = returnValue;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException" /> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        /// Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por el método.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(string methodName, object returnValue, Exception inner) : base(
            Strings.XReturnedInvalid(Strings.XYQuotes(Strings.TheFunc, methodName)), inner)
        {
            OffendingFunctionName = methodName;
            OffendingReturnValue = returnValue;
        }

        /// <summary>
        /// Obtiene uan referencia al método que ha causado la excepción.
        /// </summary>
        /// <returns>
        /// Un <see cref="Delegate" /> que representa un método cuyo valor
        /// devuelto causó la excepción.
        /// </returns>
        public Delegate? OffendingFunction { get; }

        /// <summary>
        /// Obtiene el nombre del método que ha causado la excepción.
        /// </summary>
        /// <returns>
        /// El nombre de la función cuyo resultado causó la excepción.
        /// </returns>
        public string? OffendingFunctionName { get; }

        /// <summary>
        /// Obtiene el valor devuelto que ha causado la excepción.
        /// </summary>
        /// <returns>
        /// El valor inválido devuelto por método, causante de la excepción.
        /// </returns>
        public object? OffendingReturnValue { get; }
    }
}