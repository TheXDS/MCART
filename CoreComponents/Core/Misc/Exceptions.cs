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

using System;
using System.Collections;
using TheXDS.MCART.PluginSupport;
using TheXDS.MCART.Attributes;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Excepción estándar producida al encontrarse un problema con un objeto.
    /// </summary>
    [Serializable]
    public class OffendingException<T> : Exception
    {
        /// <summary>
        /// Objeto que ha causado la excepción.
        /// </summary>
        public T OffendingObject { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        public OffendingException() : base(St.XIsInvalid(St.TheObj)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(T offendingObject) : this()
        {
            OffendingObject = offendingObject;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public OffendingException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, T offendingObject) : base(message) { OffendingObject = offendingObject; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public OffendingException(Exception inner) : base(St.XIsInvalid(St.TheType), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(Exception inner, T offendingObject) : this(inner) { OffendingObject = offendingObject; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, Exception inner, T offendingObject) : base(message, inner) { OffendingObject = offendingObject; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/> con datos serializados.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected OffendingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OffendingException{T}"/> con datos serializados.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que es la causa de esta excepción.
        /// </param>
        protected OffendingException(SerializationInfo info, StreamingContext context, T offendingObject) : base(info, context) { OffendingObject = offendingObject; }
    }
    /// <summary>
    /// Excepción que se produce al intentar cargar plugins desde un ensamblado
    /// que no contiene ninguno.
    /// </summary>
    [Serializable]
    public class NotPluginException : OffendingException<Assembly>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        public NotPluginException() : base(St.XIsInvalid(St.TheAssembly)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="offendingAssembly">
        /// <see cref="Assembly"/> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(Assembly offendingAssembly) : base(St.XIsInvalid(St.TheAssembly), offendingAssembly) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public NotPluginException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="offendingAssembly">
        /// <see cref="Assembly"/> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(string message, Assembly offendingAssembly) : base(message, offendingAssembly) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.</param>
        public NotPluginException(Exception inner) : base(St.XIsInvalid(St.TheAssembly), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingAssembly">
        /// <see cref="Assembly"/> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(Exception inner, Assembly offendingAssembly) : base(St.XIsInvalid(St.TheAssembly), inner, offendingAssembly) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingAssembly">
        /// <see cref="Assembly"/> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(string message, Exception inner, Assembly offendingAssembly) : base(message, inner, offendingAssembly) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected NotPluginException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        /// <param name="offendingAssembly">
        /// <see cref="Assembly"/> que es la causa de esta excepción.
        /// </param>
        protected NotPluginException(SerializationInfo info, StreamingContext context, Assembly offendingAssembly) : base(info, context, offendingAssembly) { }
    }
    /// <summary>
    /// Excepción que se produce cuando una colección está vacía.
    /// </summary>
    [Serializable]
    public class EmptyCollectionException : OffendingException<IEnumerable>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        public EmptyCollectionException() : base(St.LstEmpty) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="offendingCollection">
        /// Colección que ha causado la excepción.
        /// </param>
        public EmptyCollectionException(IEnumerable offendingCollection) : base(St.LstEmpty, offendingCollection) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public EmptyCollectionException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="offendingCollection">
        /// Colección que ha causado la excepción.
        /// </param>
        public EmptyCollectionException(string message, IEnumerable offendingCollection) : base(message, offendingCollection) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public EmptyCollectionException(Exception inner) : base(St.LstEmpty, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingCollection">
        /// Colección que ha causado la excepción.
        /// </param>
        public EmptyCollectionException(Exception inner, IEnumerable offendingCollection) : base(St.LstEmpty, inner, offendingCollection) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public EmptyCollectionException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingCollection">
        /// Colección que ha causado la excepción.
        /// </param>
        public EmptyCollectionException(string message, Exception inner, IEnumerable offendingCollection) : base(message, inner, offendingCollection) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="EmptyCollectionException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        /// <param name="offendingCollection">
        /// Colección que ha causado la excepción.
        /// </param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context, IEnumerable offendingCollection) : base(info, context, offendingCollection) { }
    }
    /// <summary>
    /// Excepción que se produce si la contraseña es incorrecta.
    /// </summary>
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidPasswordException"/>.
        /// </summary>
        public InvalidPasswordException() : base(St.InvalidPassword) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public InvalidPasswordException(Exception inner) : base(St.InvalidPassword, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public InvalidPasswordException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al intentar utilizar una característica no
    /// disponible.
    /// </summary>
    [Serializable]
    public class FeatureNotAvailableException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected FeatureNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        public FeatureNotAvailableException() : base(St.FeatNotAvailable) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public FeatureNotAvailableException(Exception inner) : base(St.FeatNotAvailable, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public FeatureNotAvailableException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public FeatureNotAvailableException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al enviar demasiados parámetros a un método.
    /// </summary>
    [Serializable]
    public class TooManyArgumentsException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected TooManyArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooManyArgumentsException"/>.
        /// </summary>
        public TooManyArgumentsException() : base(St.TooManyArguments) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public TooManyArgumentsException(Exception inner) : base(St.TooManyArguments, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public TooManyArgumentsException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public TooManyArgumentsException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al enviar muy pocos parámetros a un método.
    /// </summary>
    [Serializable]
    public class TooFewArgumentsException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected TooFewArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooFewArgumentsException"/>.
        /// </summary>
        public TooFewArgumentsException() : base(St.TooFewArguments) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public TooFewArgumentsException(Exception inner) : base(St.TooFewArguments, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public TooFewArgumentsException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public TooFewArgumentsException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando falta un argumento.
    /// </summary>
    [Serializable]
    public class MissingArgumentException : ArgumentException
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected MissingArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingArgumentException"/>.
        /// </summary>
        public MissingArgumentException() : base(St.MissingArgument(St.Needed)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="argumentName">Nombre del argumento faltante.</param>
        public MissingArgumentException(string argumentName) : base(St.MissingArgument(argumentName)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="argumentName">Nombre del argumento faltante.</param>
        public MissingArgumentException(string message, string argumentName) : base(message, argumentName) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public MissingArgumentException(Exception inner) : base(St.MissingArgument(St.Needed), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public MissingArgumentException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="argumentName">Nombre del argumento faltante.</param>
        public MissingArgumentException(string message, string argumentName, Exception inner) : base(message, argumentName, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando se detecta que una función ha devuelto
    /// un valor inválido sin generar una excepción.
    /// </summary>
    [Serializable]
    public class InvalidReturnValueException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidReturnValueException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected InvalidReturnValueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidReturnValueException"/>.
        /// </summary>
        public InvalidReturnValueException() : base(St.XReturnedInvalid(St.TheFunc)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException"/> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="call">
        /// <see cref="Delegate"/> cuyo resultado causó la excepción.
        /// </param>
        public InvalidReturnValueException(Delegate call) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, call.Method.Name)))
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException"/> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        /// Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        public InvalidReturnValueException(string methodName) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, methodName)))
        {
            OffendingFunctionName = methodName;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException"/> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="call">
        /// <see cref="Delegate"/> cuyo resultado causó la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por <paramref name="call"/>.
        /// </param>
        public InvalidReturnValueException(Delegate call, object returnValue) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, call.Method.Name)))
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = returnValue;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException"/> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        /// Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por el método.
        /// </param>
        public InvalidReturnValueException(string methodName, object returnValue) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, methodName)))
        {
            OffendingFunctionName = methodName;
            OffendingReturnValue = returnValue;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidReturnValueException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException"/> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="call">
        /// <see cref="Delegate"/> cuyo resultado causó la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por <paramref name="call"/>.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(Delegate call, object returnValue, Exception inner) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, call.Method.Name)), inner)
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = returnValue;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidReturnValueException"/> especificando el delegado
        /// que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        /// Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        /// <param name="returnValue">
        /// Valor inválido devuelto por el método.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(string methodName, object returnValue, Exception inner) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, methodName)), inner)
        {
            OffendingFunctionName = methodName;
            OffendingReturnValue = returnValue;
        }
        /// <summary>
        /// Obtiene uan referencia al método que ha causado la excepción.
        /// </summary>
        /// <returns>
        /// Un <see cref="Delegate"/> que representa un método cuyo valor 
        /// devuelto causó la excepción.
        /// </returns>
        public Delegate OffendingFunction { get; }
        /// <summary>
        /// Obtiene el nombre del método que ha causado la excepción.
        /// </summary>
        /// <returns>
        /// El nombre de la función cuyo resultado causó la excepción.
        /// </returns>
        public string OffendingFunctionName { get; }
        /// <summary>
        /// Obtiene el valor devuelto que ha causado la excepción.
        /// </summary>
        /// <returns>
        /// El valor inválido devuelto por método, causante de la excepción.
        /// </returns>
        public object OffendingReturnValue { get; }
    }
    /// <summary>
    /// Excepción que produce un <see cref="IPlugin"/> al encontrar un error.
    /// </summary>
    [Serializable]
    public class PluginException : OffendingException<IPlugin>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        public PluginException() : base(St.XFoundError(St.ThePlugin)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public PluginException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public PluginException(Exception inner) : base(St.XFoundError(St.ThePlugin), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public PluginException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> en donde se produjo la excepción.
        /// </param>
        public PluginException(IPlugin plugin) : base(St.XFoundError(St.XYQuotes(St.ThePlugin, plugin.Name)), plugin) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> en donde se produjo la excepción.
        /// </param>
        public PluginException(string message, IPlugin plugin) : base(message, plugin) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> en donde se produjo la excepción.
        /// </param>
        public PluginException(Exception inner, IPlugin plugin) : base(St.XFoundError(St.XYQuotes(St.ThePlugin, plugin.Name)), inner, plugin) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin">
        /// <see cref="IPlugin"/> en donde se produjo la excepción.
        /// </param>
        public PluginException(string message, Exception inner, IPlugin plugin) : base(message, inner, plugin) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Excepción que se produce cuando se intenta utilizar un objeto marcado
    /// con el atributo <see cref="UnusableAttribute"/>.
    /// </summary>
    [Serializable]
    public class UnusableObjectException : OffendingException<object>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        public UnusableObjectException() : base(St.UnusableObject) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="offendingObject">
        /// Objeto que ha sido marcado con el atributo
        /// <see cref="UnusableAttribute"/>.
        /// </param>
        public UnusableObjectException(object offendingObject) : base(St.UnusableObject, offendingObject) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public UnusableObjectException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que ha sido marcado con el atributo
        /// <see cref="UnusableAttribute"/>.
        /// </param>
        public UnusableObjectException(string message, object offendingObject) : base(message, offendingObject) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public UnusableObjectException(Exception inner) : base(St.UnusableObject, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que ha sido marcado con el atributo
        /// <see cref="UnusableAttribute"/>.
        /// </param>
        public UnusableObjectException(Exception inner, object offendingObject) : base(St.UnusableObject, inner, offendingObject) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public UnusableObjectException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que ha sido marcado con el atributo
        /// <see cref="UnusableAttribute"/>.
        /// </param>
        public UnusableObjectException(string message, Exception inner, object offendingObject) : base(message, inner, offendingObject) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected UnusableObjectException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="UnusableObjectException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        /// <param name="offendingObject">
        /// Objeto que ha sido marcado con el atributo
        /// <see cref="UnusableAttribute"/>.
        /// </param>
        protected UnusableObjectException(SerializationInfo info, StreamingContext context, object offendingObject) : base(info, context, offendingObject) { }
    }
    /// <summary>
    /// Excepción que se produce cuando un <see cref="Plugin"/> encuentra un error al inicializarse.
    /// </summary>
    [Serializable]
    public class PluginInitializationException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginInitializationException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected PluginInitializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginInitializationException"/>.
        /// </summary>
        public PluginInitializationException() : base(St.PluginDidntInit) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginInitializationException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public PluginInitializationException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginInitializationException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public PluginInitializationException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando una clase es peligrosa.
    /// </summary>
    [Serializable]
    public class DangerousClassException : OffendingException<Type>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        public DangerousClassException() : base(St.ClassIsDangerous(St.Unk)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="classType">Clase que ha causado la excepción.</param>
        public DangerousClassException(Type classType) : base(St.ClassIsDangerous(classType.Name), classType) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public DangerousClassException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="classType">Método que ha causado la excepción.</param>
        public DangerousClassException(string message, Type classType) : base(message, classType) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public DangerousClassException(Exception inner) : base(St.ClassIsDangerous(St.Unk), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="classType">Clase que ha causado la excepción.</param>
        public DangerousClassException(Exception inner, Type classType) : base(St.ClassIsDangerous(classType.Name), inner, classType) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public DangerousClassException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="classType">Clase que ha causado la excepción.</param>
        public DangerousClassException(string message, Exception inner, Type classType) : base(message, inner, classType) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected DangerousClassException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousClassException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        /// <param name="classType">Método que ha causado la excepción.</param>
        protected DangerousClassException(SerializationInfo info, StreamingContext context, Type classType) : base(info, context, classType) { }
    }
    /// <summary>
    /// Excepción que se produce cuando la llamada a un método es peligrosa.
    /// </summary>
    [Serializable]
    public class DangerousCallException : OffendingException<MethodInfo>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        public DangerousCallException() : base(St.MethodIsDangerous(St.Unk)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="method">Método que ha causado la excepción.</param>
        public DangerousCallException(MethodInfo method) : base(St.MethodIsDangerous(method.Name), method) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public DangerousCallException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public DangerousCallException(string message, MethodInfo method) : base(message, method) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public DangerousCallException(Exception inner) : base(St.MethodIsDangerous(St.Unk), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public DangerousCallException(Exception inner, MethodInfo method) : base(St.MethodIsDangerous(method.Name), inner, method) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public DangerousCallException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public DangerousCallException(string message, Exception inner, MethodInfo method) : base(message, inner, method) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected DangerousCallException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DangerousCallException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="method">Método que ha causado la excepción.</param>
        protected DangerousCallException(SerializationInfo info, StreamingContext context, MethodInfo method) : base(info, context, method) { }
    }







    /// <summary>
    /// Excepción que se produce cuando un <see cref="Assembly"/> no contiene la clase especificada.
    /// </summary>
    [Serializable]
    public class PluginClassNotFoundException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        /// <param name="context">
        /// El <see cref="StreamingContext"/> que contiene información
        /// contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="info">
        /// El <see cref="SerializationInfo"/> que contiene la información
        /// serializada del objeto acerca de la excepción que está siendo
        /// lanzada.
        /// </param>
        protected PluginClassNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        public PluginClassNotFoundException() : base(St.XDoesntContainY(St.ThePlugin, St.TheClass.ToLower())) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(Type requiredType) : base(St.XDoesntContainY(St.ThePlugin, St.XYQuotes(St.TheClass.ToLower(), requiredType.Name)))
        {
            RequiredType = requiredType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public PluginClassNotFoundException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Tipo que fue requerido.
        /// </summary>
        public Type RequiredType { get; }
    }
    /// <summary>
    /// Excepción que se produce al intentar remover un objeto de una pila vacía
    /// </summary>
    [Serializable]
    public class StackUnderflowException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="StackUnderflowException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected StackUnderflowException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="StackUnderflowException"/>.
        /// </summary>
        public StackUnderflowException() : base(St.StackUnderflow) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="StackUnderflowException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public StackUnderflowException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="StackUnderflowException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public StackUnderflowException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando una base de datos no es válida.
    /// </summary>
    [Serializable]
    public class InvalidDatabaseException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidDatabaseException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected InvalidDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidDatabaseException"/>.
        /// </summary>
        public InvalidDatabaseException() : base(St.InvalidDB) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidDatabaseException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        public InvalidDatabaseException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="InvalidDatabaseException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public InvalidDatabaseException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando una base de datos no corresponde a la
    /// aplicación.
    /// </summary>
    [Serializable]
    public class NotMyDatabaseException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotMyDatabaseException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected NotMyDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NotMyDatabaseException"/>.
        /// </summary>
        public NotMyDatabaseException() : base(St.DBDoesntBelong) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="NotMyDatabaseException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public NotMyDatabaseException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotMyDatabaseException"/>.
        /// </summary>
        /// <param name="message">
        /// Un <see cref="string"/> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.
        /// </param>
        public NotMyDatabaseException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción  que se produce al no encontrar los datos solicitados.
    /// </summary>
    [Serializable]
    public class DataNotFoundException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DataNotFoundException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected DataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataNotFoundException"/>.
        /// </summary>
        public DataNotFoundException() : base(St.DataNotFound) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataNotFoundException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public DataNotFoundException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataNotFoundException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public DataNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al no haber suficientes datos
    /// </summary>
    [Serializable]
    public class InsufficientDataException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InsufficientDataException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected InsufficientDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException"/>.
        /// </summary>
        public InsufficientDataException() : base(St.NotEnoughData) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public InsufficientDataException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InsufficientDataException(Exception inner) : base(St.NotEnoughData, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InsufficientDataException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InsufficientDataException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al hacer referencia a un tipo desconodico
    /// </summary>
    [Serializable]
    public class UnknownTypeException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="UnknownTypeException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected UnknownTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnknownTypeException"/>.
        /// </summary>
        public UnknownTypeException() : base(St.UnknownType) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnknownTypeException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public UnknownTypeException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnknownTypeException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public UnknownTypeException(Exception inner) : base(St.UnknownType, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UnknownTypeException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public UnknownTypeException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al hacer referencia a un tipo inválido
    /// </summary>
    [Serializable]
    public class InvalidTypeException : Exception
    {
        private readonly Type offendingType;
        /// <summary>
        /// Tipo que ha causado la excepción.
        /// </summary>
        public Type OffendingType => offendingType;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        public InvalidTypeException() : base(St.XIsInvalid(St.TheType)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(Type offendingType) : base(St.XIsInvalid(St.TheType))
        {
            this.offendingType = offendingType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public InvalidTypeException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(string message, Type offendingType) : base(message)
        {
            this.offendingType = offendingType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InvalidTypeException(Exception inner) : base(St.XIsInvalid(St.TheType), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InvalidTypeException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(Exception inner, Type offendingType) : base(St.XIsInvalid(St.TheType), inner)
        {
            this.offendingType = offendingType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="offendingType">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(string message, Exception inner, Type offendingType) : base(message, inner)
        {
            this.offendingType = offendingType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected InvalidTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Excepción que se produce al intentar crear nueva información dentro de
    /// una base de datos con un identificador que ya existe.
    /// </summary>
    [Serializable]
    public class DataAlreadyExistsException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException"/>.
        /// </summary>
        public DataAlreadyExistsException() : base(St.XAlreadyExists(St.TheUid)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException"/>.
        /// </summary>
        /// <param name="Uid">Uid.</param>
        public DataAlreadyExistsException(string Uid) : base(St.XAlreadyExists(St.XYQuotes(St.TheUid, Uid))) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DataAlreadyExistsException"/>.
        /// </summary>
        /// <param name="Uid">Uid.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public DataAlreadyExistsException(string Uid, Exception inner) : base(St.XAlreadyExists(St.XYQuotes(St.TheUid, Uid)), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="DataAlreadyExistsException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected DataAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Excepción que se produce cuando se llama a un método de un <see cref="PluginSupport.Plugin"/> sin inicializar
    /// </summary>
    [Serializable]
    public class PluginNotInitializedException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginNotInitializedException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected PluginNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginNotInitializedException"/>.
        /// </summary>
        public PluginNotInitializedException() : base(St.XNotInit(St.ThePlugin)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginNotInitializedException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public PluginNotInitializedException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginNotInitializedException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginNotInitializedException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando no se puede resolver un nombre DNS, o no se encuentra el servidor especificado.
    /// </summary>
    [Serializable]
    public class ServerNotFoundException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ServerNotFoundException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected ServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException"/>.
        /// </summary>
        public ServerNotFoundException() : base(St.XNotFound(St.TheSrv)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public ServerNotFoundException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public ServerNotFoundException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ServerNotFoundException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public ServerNotFoundException(Exception inner) : base(St.XNotFound(St.TheSrv), inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando se reciben datos corruptos
    /// </summary>
    [Serializable]
    public class CorruptDataException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="CorruptDataException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected CorruptDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CorruptDataException"/>.
        /// </summary>
        public CorruptDataException() : base(St.CorruptData) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CorruptDataException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public CorruptDataException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CorruptDataException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public CorruptDataException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CorruptDataException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public CorruptDataException(Exception inner) : base(St.CorruptData, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando no se puede realizar la conexión
    /// </summary>
    [Serializable]
    public class CouldntConnectException : Exception
    {
        private readonly IPEndPoint offendingEndPoint;
        /// <summary>
        /// <see cref="IPEndPoint"/> que fue la causa de esta excepción.
        /// </summary>
        public IPEndPoint OffendingEndPoint => offendingEndPoint;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        public CouldntConnectException() : base(St.CldntConnect(St.TheSrv.ToLower())) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint) : base(St.CldntConnect($"{offendingEndPoint.Address}:{offendingEndPoint.Port}"))
        {
            this.offendingEndPoint = offendingEndPoint;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public CouldntConnectException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public CouldntConnectException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint, string message) : base(message)
        {
            this.offendingEndPoint = offendingEndPoint;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint, Exception inner) : base(St.CldntConnect($"{offendingEndPoint.Address}:{offendingEndPoint.Port}"), inner)
        {
            this.offendingEndPoint = offendingEndPoint;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="offendingEndPoint">End point.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public CouldntConnectException(IPEndPoint offendingEndPoint, string message, Exception inner) : base(message, inner)
        {
            this.offendingEndPoint = offendingEndPoint;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected CouldntConnectException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Excepción que se produce cuando la conexión se encontraba cerrada al intentar enviar o recibir datos
    /// </summary>
    [Serializable]
    public class ConnectionClosedException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ConnectionClosedException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected ConnectionClosedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ConnectionClosedException"/>.
        /// </summary>
        public ConnectionClosedException() : base(St.ClosdConn) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ConnectionClosedException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public ConnectionClosedException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ConnectionClosedException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public ConnectionClosedException(Exception inner) : base(St.ClosdConn, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ConnectionClosedException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public ConnectionClosedException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando una operación falla
    /// </summary>
    [Serializable]
    public class OperationException : Exception
    {
        private readonly Delegate offendingOperation;
        /// <summary>
        /// Operación donde se produjo la excepción.
        /// </summary>
        public Delegate OffendingOperation => offendingOperation;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        public OperationException() : base(St.ExcDoingX(St.TheTask.ToLower())) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public OperationException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public OperationException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public OperationException(Exception inner) : base(St.ExcDoingX(St.TheTask.ToLower()), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="offendingOperation">Operación que causó la excepción.</param>
        public OperationException(Delegate offendingOperation) : base(St.ExcDoingX(St.XYQuotes(St.TheTask.ToLower(), offendingOperation.Method.Name)))
        {
            this.offendingOperation = offendingOperation;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="offendingOperation">Operación que causó la excepción.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public OperationException(Delegate offendingOperation, string message) : base(message)
        {
            this.offendingOperation = offendingOperation;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="offendingOperation">Operación que causó la excepción.</param>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        public OperationException(Delegate offendingOperation, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.offendingOperation = offendingOperation;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="offendingOperation">Operación que causó la excepción.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public OperationException(Delegate offendingOperation, string message, Exception inner) : base(message, inner)
        {
            this.offendingOperation = offendingOperation;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="offendingOperation">Operación que causó la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public OperationException(Delegate offendingOperation, Exception inner) : base(St.ExcDoingX(St.XYQuotes(St.TheTask.ToLower(), offendingOperation.Method.Name)), inner)
        {
            this.offendingOperation = offendingOperation;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="OperationException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected OperationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Excepción que se produce cuando la firma de un método representado en
    /// un <see cref="MethodInfo"/> no es válida.
    /// </summary>
    [Serializable]
    public class InvalidMethodSignatureException : Exception
    {
        private readonly MethodInfo offendingMethod;
        /// <summary>
        /// Referencia al método que ha causado la excepción.
        /// </summary>
        public MethodInfo OffendingMethod => offendingMethod;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        public InvalidMethodSignatureException() : base(St.InvalidSignature(St.TheMethod)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="method">Método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(MethodInfo method) : base(St.InvalidSignature(St.XYQuotes(St.TheMethod, $"{method.DeclaringType.FullName}.{method.Name}"))) { offendingMethod = method; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(string message, MethodInfo method = null) : base(message) { offendingMethod = method; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InvalidMethodSignatureException(Exception inner) : base(St.InvalidSignature(St.TheMethod), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(Exception inner, MethodInfo method) : base(St.InvalidSignature(St.XYQuotes(St.TheMethod, $"{method.DeclaringType.FullName}.{method.Name}")), inner) { offendingMethod = method; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(string message, Exception inner, MethodInfo method = null) : base(message, inner) { offendingMethod = method; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        /// <param name="method">Método que ha causado la excepción.</param>
        protected InvalidMethodSignatureException(SerializationInfo info, StreamingContext context, MethodInfo method = null) : base(info, context) { offendingMethod = method; }
    }
}