/*
FeatureNotAvailableException.cs

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
using static TheXDS.MCART.PluginSupport.Legacy.Resources.Strings.Errors;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce al intentar utilizar una característica no
    /// disponible.
    /// </summary>
    [Serializable]
    public class FeatureNotAvailableException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException" />.
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
        protected FeatureNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException" />.
        /// </summary>
        public FeatureNotAvailableException() : base(FeatureNotAvailable)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public FeatureNotAvailableException(Exception inner) : base(FeatureNotAvailable, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public FeatureNotAvailableException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FeatureNotAvailableException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public FeatureNotAvailableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}