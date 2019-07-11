/*
InvalidUriException.cs

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
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando un <see cref="Uri"/> no hace
    ///     referencia a un recurso válido.
    /// </summary>
    [Serializable]
    public class InvalidUriException : OffendingException<Uri>
    {
        private static string Msg() => Strings.XIsInvalid(Strings.TheUri);
        private static string Msg(Uri uri) => Strings.XIsInvalid(Strings.XYQuotes(Strings.TheUri, uri.ToString()));

        public InvalidUriException() : base(Msg())
        {
        }
        public InvalidUriException(Uri offendingUri) : base(Msg(offendingUri), offendingUri)
        {
        }
        public InvalidUriException(string message) : base(message)
        {
        }
        public InvalidUriException(string message, Uri offendingUri) : base(message, offendingUri)
        {
        }
        public InvalidUriException(Exception inner):base(inner)
        {
        }
        public InvalidUriException(string message, Exception inner):base(message, inner)
        {
        }
        public InvalidUriException(Exception inner, Uri offendingUri):base(inner, offendingUri)
        {
        }
        public InvalidUriException(string message, Exception inner, Uri offendingUri):base(message, inner, offendingUri)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidUriException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected InvalidUriException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}