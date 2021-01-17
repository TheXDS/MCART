/*
EmptyCollectionException.cs

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
using System.Collections;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce cuando una colección está vacía.
    /// </summary>
    [Serializable]
    public class EmptyCollectionException : OffendingException<IEnumerable>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext" /> que contiene información
        /// contextual acerca del origen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo" /> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext" /> que contiene información
        /// contextual acerca del origen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo" /> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context,
            IEnumerable offendingCollection) : base(info, context, offendingCollection)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException" />.
        /// </summary>
        public EmptyCollectionException() : base(Strings.LstEmpty)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(IEnumerable offendingCollection) : base(Strings.LstEmpty, offendingCollection)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public EmptyCollectionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(string message, IEnumerable offendingCollection) : base(message,
            offendingCollection)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public EmptyCollectionException(Exception inner) : base(Strings.LstEmpty, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(Exception inner, IEnumerable offendingCollection) : base(Strings.LstEmpty, inner,
            offendingCollection)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public EmptyCollectionException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(string message, Exception inner, IEnumerable offendingCollection) : base(
            message, inner, offendingCollection)
        {
        }
    }
}