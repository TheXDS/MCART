/*
InvalidUriException.cs

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
using TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción que se produce cuando un <see cref="Uri"/> no hace
    /// referencia a un recurso válido.
    /// </summary>
    [Serializable]
    public class InvalidUriException : OffendingException<Uri>
    {
        private static string Msg() => Errors.InvalidUri;
        private static string Msg(Uri uri) => string.Format(Errors.InvalidXUri, uri.ToString());

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        public InvalidUriException() : base(Msg())
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        /// <param name="offendingUri">
        /// <see cref="Uri"/> que apunta a un recurso inválido.
        /// </param>
        public InvalidUriException(Uri offendingUri) : base(Msg(offendingUri), offendingUri)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        public InvalidUriException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingUri">
        /// <see cref="Uri"/> que apunta a un recurso inválido.
        /// </param>
        public InvalidUriException(string message, Uri offendingUri) : base(message, offendingUri)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidUriException(Exception inner) : base(inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidUriException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingUri">
        /// <see cref="Uri"/> que apunta a un recurso inválido.
        /// </param>
        public InvalidUriException(Exception inner, Uri offendingUri) : base(inner, offendingUri)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidUriException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingUri">
        /// <see cref="Uri"/> que apunta a un recurso inválido.
        /// </param>
        public InvalidUriException(string message, Exception inner, Uri offendingUri) : base(message, inner, offendingUri)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidUriException" />.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected InvalidUriException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}