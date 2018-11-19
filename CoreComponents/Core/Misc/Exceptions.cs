/*
Exceptions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Net;
using System.Runtime.Serialization;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se intenta acceder a una base de datos inválida.
    /// </summary>
    [Serializable]
    public class InvalidDatabaseException : OffendingException<string>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
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
        protected InvalidDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
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
        /// <param name="databasePath">Nombre o ruta de la base de datos que ha causado la excepción.</param>
        protected InvalidDatabaseException(SerializationInfo info, StreamingContext context, string databasePath) :
            base(info, context, databasePath)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
        /// </summary>
        public InvalidDatabaseException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
        /// </summary>
        /// <param name="databasePath">Nombre o ruta de la base de datos que ha causado la excepción.</param>
        public InvalidDatabaseException(string databasePath) : base(Msg(databasePath), databasePath)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="databasePath">Nombre o ruta de la base de datos que ha causado la excepción.</param>
        public InvalidDatabaseException(string message, string databasePath) : base(message, databasePath)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidDatabaseException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="databasePath">Nombre o ruta de la base de datos que ha causado la excepción.</param>
        public InvalidDatabaseException(Exception inner, string databasePath) : base(Msg(databasePath), inner,
            databasePath)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidDatabaseException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidDatabaseException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="databasePath">Nombre o ruta de la base de datos que ha causado la excepción.</param>
        public InvalidDatabaseException(string message, Exception inner, string databasePath) : base(message, inner,
            databasePath)
        {
        }

        private static string Msg()
        {
            return St.InvalidDB;
        }

        private static string Msg(string databasePath)
        {
            return St.XYQuotes(St.InvalidDB, databasePath);
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se reciben datos corruptos
    /// </summary>
    [Serializable]
    public class CorruptDataException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.CorruptDataException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected CorruptDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.CorruptDataException" />.
        /// </summary>
        public CorruptDataException() : base(St.CorruptData)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.CorruptDataException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        public CorruptDataException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.CorruptDataException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public CorruptDataException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.CorruptDataException" />.
        /// </summary>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public CorruptDataException(Exception inner) : base(St.CorruptData, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando una operación falla.
    /// </summary>
    [Serializable]
    public class OperationException : OffendingException<Delegate>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
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
        protected OperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
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
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        protected OperationException(SerializationInfo info, StreamingContext context, Delegate offendingOperation) :
            base(info, context, offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        public OperationException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(Delegate offendingOperation) : base(Msg(offendingOperation), offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public OperationException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(string message, Delegate offendingOperation) : base(message, offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public OperationException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(Exception inner, Delegate offendingOperation) : base(Msg(offendingOperation), inner,
            offendingOperation)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public OperationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.OperationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingOperation">Delegado de operación en donde se ha producido la excepción.</param>
        public OperationException(string message, Exception inner, Delegate offendingOperation) : base(message, inner,
            offendingOperation)
        {
        }

        private static string Msg()
        {
            return St.ExcDoingX(St.TheTask.ToLower());
        }

        private static string Msg(Delegate offendingOperation)
        {
            return St.ExcDoingX(St.XYQuotes(St.TheTask.ToLower(), offendingOperation.Method.Name));
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando la conexión se encontraba cerrada al intentar enviar o recibir datos
    /// </summary>
    [Serializable]
    public class ConnectionClosedException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.ConnectionClosedException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected ConnectionClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.ConnectionClosedException" />.
        /// </summary>
        public ConnectionClosedException() : base(St.ClosdConn)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.ConnectionClosedException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        public ConnectionClosedException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.ConnectionClosedException" />.
        /// </summary>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public ConnectionClosedException(Exception inner) : base(St.ClosdConn, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.ConnectionClosedException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public ConnectionClosedException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Excepción que se produce cuando un tipo requerido no ha sido encontrado.
    /// </summary>
    public class MissingTypeException : OffendingException<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        public MissingTypeException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="offendingObject">
        ///     Tipo que ha producido la excepción.
        /// </param>
        public MissingTypeException(Type offendingObject) : base(offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public MissingTypeException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public MissingTypeException(Exception inner) : base(inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingObject">
        ///     Tipo que ha producido la excepción.
        /// </param>
        public MissingTypeException(string message, Type offendingObject) : base(message, offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        /// <param name="offendingObject">
        ///     Tipo que ha producido la excepción.
        /// </param>
        public MissingTypeException(Exception inner, Type offendingObject) : base(inner, offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public MissingTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        /// <param name="offendingObject">
        ///     Tipo que ha producido la excepción.
        /// </param>
        public MissingTypeException(string message, Exception inner, Type offendingObject) : base(message, inner, offendingObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected MissingTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingTypeException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="offendingObject">
        ///     Tipo que ha producido la excepción.
        /// </param>
        protected MissingTypeException(SerializationInfo info, StreamingContext context, Type offendingObject) : base(info, context, offendingObject)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando una base de datos no corresponde a la
    ///     aplicación.
    /// </summary>
    [Serializable]
    public class NotMyDatabaseException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotMyDatabaseException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected NotMyDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotMyDatabaseException" />.
        /// </summary>
        public NotMyDatabaseException() : base(St.DBDoesntBelong)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotMyDatabaseException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public NotMyDatabaseException(string message) : base(message)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotMyDatabaseException"/>.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta
        ///     excepción.
        /// </param>
        public NotMyDatabaseException(string message, Exception inner) : base(message, inner)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotMyDatabaseException"/>.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception"/> que es la causa de esta
        ///     excepción.
        /// </param>
        public NotMyDatabaseException(Exception inner) : base(St.DBDoesntBelong,inner)
        {
        }
    }

    /// <summary>
    ///     Excepción  que se produce al no encontrar los datos solicitados.
    /// </summary>
    [Serializable]
    public class DataNotFoundException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="DataNotFoundException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected DataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataNotFoundException" />.
        /// </summary>
        public DataNotFoundException() : base(St.DataNotFound)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataNotFoundException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public DataNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataNotFoundException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public DataNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    ///     Excepción que se produce al no haber suficientes datos
    /// </summary>
    [Serializable]
    public class InsufficientDataException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected InsufficientDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        public InsufficientDataException() : base(St.NotEnoughData)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public InsufficientDataException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public InsufficientDataException(Exception inner) : base(St.NotEnoughData, inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InsufficientDataException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public InsufficientDataException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce al hacer referencia a un tipo inválido
    /// </summary>
    [Serializable]
    public class InvalidTypeException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        public InvalidTypeException() : base(St.XIsInvalid(St.TheType))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(Type offendingType) : base(St.XIsInvalid(St.TheType))
        {
            OffendingType = offendingType;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public InvalidTypeException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(string message, Type offendingType) : base(message)
        {
            OffendingType = offendingType;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public InvalidTypeException(Exception inner) : base(St.XIsInvalid(St.TheType), inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public InvalidTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(Exception inner, Type offendingType) : base(St.XIsInvalid(St.TheType), inner)
        {
            OffendingType = offendingType;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(string message, Exception inner, Type offendingType) : base(message, inner)
        {
            OffendingType = offendingType;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="InvalidTypeException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected InvalidTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Tipo que ha causado la excepción.
        /// </summary>
        public Type OffendingType { get; }
    }

    /// <summary>
    ///     Excepción que se produce al intentar crear nueva información dentro de
    ///     una base de datos con un identificador que ya existe.
    /// </summary>
    [Serializable]
    public class DataAlreadyExistsException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException" />.
        /// </summary>
        public DataAlreadyExistsException() : base(St.XAlreadyExists(St.TheUid))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException" />.
        /// </summary>
        /// <param name="uid">Uid.</param>
        public DataAlreadyExistsException(string uid) : base(St.XAlreadyExists(St.XYQuotes(St.TheUid, uid)))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException" />.
        /// </summary>
        /// <param name="uid">Uid.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public DataAlreadyExistsException(string uid, Exception inner) : base(
            St.XAlreadyExists(St.XYQuotes(St.TheUid, uid)), inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="DataAlreadyExistsException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected DataAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    ///     Excepción que se produce cuando se llama a un método de un <see cref="PluginSupport.Plugin" /> sin inicializar
    /// </summary>
    [Serializable]
    public class PluginNotInitializedException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PluginNotInitializedException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected PluginNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginNotInitializedException" />.
        /// </summary>
        public PluginNotInitializedException() : base(St.XNotInit(St.ThePlugin))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginNotInitializedException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public PluginNotInitializedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="PluginNotInitializedException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public PluginNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    ///     Excepción que se produce cuando no se puede resolver un nombre DNS, o no se encuentra el servidor especificado.
    /// </summary>
    [Serializable]
    public class ServerNotFoundException : Exception
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected ServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException" />.
        /// </summary>
        public ServerNotFoundException() : base(St.XNotFound(St.TheSrv))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public ServerNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public ServerNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException" />.
        /// </summary>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public ServerNotFoundException(Exception inner) : base(St.XNotFound(St.TheSrv), inner)
        {
        }
    }

    /// <summary>
    ///     Excepción que se produce cuando no se puede realizar la conexión
    /// </summary>
    [Serializable]
    public class CouldntConnectException : Exception
    {
        private readonly IPEndPoint _offendingEndPoint;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldntConnectException" />.
        /// </summary>
        public CouldntConnectException() : base(St.CldntConnect(St.TheSrv.ToLower()))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldntConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint) : base(
            St.CldntConnect($"{offendingEndPoint.Address}:{offendingEndPoint.Port}"))
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldntConnectException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public CouldntConnectException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldntConnectException" />.
        /// </summary>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public CouldntConnectException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldntConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint, string message) : base(message)
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldntConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint, Exception inner) : base(
            St.CldntConnect($"{offendingEndPoint.Address}:{offendingEndPoint.Port}"), inner)
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="CouldntConnectException" />.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint, string message, Exception inner) : base(message,
            inner)
        {
            _offendingEndPoint = offendingEndPoint;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="CouldntConnectException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected CouldntConnectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     <see cref="IPEndPoint" /> que fue la causa de esta excepción.
        /// </summary>
        public IPEndPoint OffendingEndPoint => _offendingEndPoint;
    }
}