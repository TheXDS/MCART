﻿/*
TamperException.cs

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
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Se produce cuando una función soportada detecta una de las 
    /// siguientes situaciones:
    /// <list type="bullet">
    /// <item>
    ///     <description>
    ///         Valores de retorno alterados inesperadamente.
    ///     </description> 
    /// </item>
    /// <item>
    ///     <description>
    ///         Valor de retorno fuera del rango conocido esperado de
    ///         una función.
    ///     </description>
    /// </item>
    /// <item>
    ///     <description>
    ///         Corrupción de memoria no capturada por CLR.
    ///     </description>
    /// </item>
    /// <item>
    ///     <description>
    ///         Modificación externa de valores internos protegidos de
    ///         la aplicación.
    ///     </description>
    /// </item>
    /// </list>
    /// </summary>
    [Serializable]
    public class TamperException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TamperException" />.
        /// </summary>
        public TamperException():base(Strings.TamperDetected)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TamperException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public TamperException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TamperException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public TamperException(Exception inner) : this(Strings.TamperDetected, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TamperException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public TamperException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TamperException" /> con datos serializados.
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
        protected TamperException(SerializationInfo info,StreamingContext context) : base(info, context)
        {
        }
    }
}