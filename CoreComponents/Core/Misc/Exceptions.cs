//
//  Exceptions.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using St = MCART.Resources.Strings;

namespace MCART.Exceptions
{
    /// <summary>
    /// Excepcion que se produce cuando un método o función esperaba una
    /// interfaz como argumento.
    /// </summary>
    [Serializable]
    public class InterfaceExpectedException : Exception
    {
        /// <summary>
        /// Tipo que ha causado la excepción.
        /// </summary>
        public readonly Type OffendingType;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        public InterfaceExpectedException() : base(St.InterfaceExpected) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(Type T) : base(St.InterfaceExpected)
        {
            OffendingType = T;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        public InterfaceExpectedException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(string message, Type T)
            : base(message) { OffendingType = T; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="inner">The exception that is the cause of the current
        /// exception.</param>
        public InterfaceExpectedException(Exception inner)
            : base(St.InterfaceExpected, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="inner">The exception that is the cause of the current
        /// exception.</param>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(Exception inner, Type T)
            : base(St.InterfaceExpected, inner) { OffendingType = T; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        /// <param name="inner">The exception that is the cause of the current
        /// exception. </param>
        public InterfaceExpectedException(string message, Exception inner)
            : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        /// <param name="inner">The exception that is the cause of the current
        /// exception.</param>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(string message, Exception inner, Type T)
            : base(message, inner) { OffendingType = T; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="context">The contextual information about the source or
        /// destination.</param>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        protected InterfaceExpectedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="context">The contextual information about the source or
        /// destination.</param>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="T">Tipo que generó la excepción.</param>
        protected InterfaceExpectedException(SerializationInfo info, StreamingContext context, Type T)
            : base(info, context) { OffendingType = T; }
    }
    /// <summary>
    /// Excepción que se produce al intentar cargar plugins desde un ensamblado
    /// que no contiene ninguno.
    /// </summary>
    [Serializable]
    public class NotPluginException : Exception
    {
        /// <summary>
        /// The offending assembly.
        /// </summary>
        public readonly System.Reflection.Assembly OffendingAssembly;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(System.Reflection.Assembly offendingAssembly = null)
            : base(St.XIsInvalid(St.TheAssembly)) { OffendingAssembly = offendingAssembly; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="message">
        /// A <see cref="string"/> that describes the exception.</param>
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(string message, System.Reflection.Assembly offendingAssembly = null)
            : base(message) { OffendingAssembly = offendingAssembly; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="NotPluginException"/>.
        /// </summary>
        /// <param name="inner">
        /// <see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(Exception inner, System.Reflection.Assembly offendingAssembly = null)
            : base(St.XIsInvalid(St.TheAssembly), inner) { OffendingAssembly = offendingAssembly; }
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
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(string message, Exception inner, System.Reflection.Assembly offendingAssembly = null)
            : base(message, inner) { OffendingAssembly = offendingAssembly; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InterfaceExpectedException"/>.
        /// </summary>
        /// <param name="context">
        /// The contextual information about the source or destination.
        /// </param>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="offendingAssembly">Offending assembly.</param>
        protected NotPluginException(SerializationInfo info, StreamingContext context, System.Reflection.Assembly offendingAssembly = null)
            : base(info, context) { OffendingAssembly = offendingAssembly; }
    }
    /// <summary>
    /// Excepción que se produce cuando un <see cref="IEnumerable{T}"/> está 
    /// vacío.
    /// </summary>
    [Serializable]
    public class EmptyCollectionException<T> : Exception
    {
        /// <summary>
        /// Colección a la que se intentó acceder.
        /// </summary>
        public readonly IEnumerable<T> OffendingCollection;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        public EmptyCollectionException(IEnumerable<T> offendingList) : base(St.LstEmpty) { OffendingCollection = offendingList; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="offendingList">Offending list.</param>
        public EmptyCollectionException(Exception inner, IEnumerable<T> offendingList) : base(St.LstEmpty, inner) { OffendingCollection = offendingList; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="offendingCollection">Colección a la que se intentó acceder.</param>
        public EmptyCollectionException(string message, IEnumerable<T> offendingCollection) : base(message) { OffendingCollection = offendingCollection; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="offendingCollection">Colección a la que se intentó acceder.</param>
        public EmptyCollectionException(string message, Exception inner, IEnumerable<T> offendingCollection) : base(message, inner) { OffendingCollection = offendingCollection; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="offendingCollection">Colección a la que se intentó acceder.</param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context, IEnumerable<T> offendingCollection) : base(info, context) { OffendingCollection = offendingCollection; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        public EmptyCollectionException() : base(St.LstEmpty) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception. </param>
        public EmptyCollectionException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public EmptyCollectionException(Exception inner) : base(St.LstEmpty, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public EmptyCollectionException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Excepcion que se produce cuando se solicita cargar un <see cref="PluginSupport.Plugin"/> con la interfaz especificada, pero no la implementa.
    /// </summary>
    [Serializable]
    public class InterfaceNotImplementedException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InterfaceNotImplementedException"/>.
        /// </summary>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        protected InterfaceNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Obtiene el tipo de la interfaz que se ha solicitado cargar
        /// </summary>
        /// <returns></returns>
        public readonly Type MissingInterface;
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con los valores predeterminados, sin establecer la instancia solicitada que generó el error
        /// </summary>
        public InterfaceNotImplementedException() : base(St.XDoesntContainY(St.ThePlugin, St.TheInterface)) { }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con la excepción interna que causó esta excepción
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InterfaceNotImplementedException(Exception inner) : base(St.XDoesntContainY(St.ThePlugin, St.TheInterface), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto especificando el tipo que causó la excepción
        /// </summary>
        /// <param name="T">Tipo que generó la excepción</param>
        public InterfaceNotImplementedException(Type T) : base(St.XDoesntContainY(St.ThePlugin, St.XYQuotes(St.TheInterface, T.Name)))
        {
            MissingInterface = T;
        }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto especificando el tipo que causó la excepción, además de la <see cref="Exception"/> que generó esta.
        /// </summary>
        /// <param name="T">Tipo que generó la excepción</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InterfaceNotImplementedException(Type T, Exception inner) : base(St.XDoesntContainY(St.ThePlugin, St.XYQuotes(St.TheInterface, T.Name)), inner)
        {
            MissingInterface = T;
        }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con el mensaje especificado
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public InterfaceNotImplementedException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto especificando el mensaje, la <see cref="Exception"/> y el tipo que generó esta excepción.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="T">Tipo que generó la excepción</param>
        public InterfaceNotImplementedException(string message, Exception inner, Type T = null) : base(message, inner)
        {
            MissingInterface = T;
        }
    }
    /// <summary>
    /// Excepción que se produce cuando se determina que el directorio está lleno
    /// </summary>
    [Serializable]
    public class DirectoryIsFullException : Exception
    {
        /// <summary>
        /// Directorio al que se intentó escribir.
        /// </summary>
        public readonly DirectoryInfo OffendingDirectory;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DirectoryIsFullException"/>.
        /// </summary>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        protected DirectoryIsFullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DirectoryIsFullException"/>.
        /// </summary>
        /// <param name="OffendingDirectory">Offending directory.</param>
        public DirectoryIsFullException(DirectoryInfo OffendingDirectory = null) : base(St.DirectoryIsFull)
        {
            this.OffendingDirectory = OffendingDirectory;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DirectoryIsFullException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="OffendingDirectory">Offending directory.</param>
        public DirectoryIsFullException(Exception inner, DirectoryInfo OffendingDirectory = null) : base(St.DirectoryIsFull, inner)
        {
            this.OffendingDirectory = OffendingDirectory;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DirectoryIsFullException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="OffendingDirectory">Offending directory.</param>
        public DirectoryIsFullException(string message, DirectoryInfo OffendingDirectory = null) : base(message)
        {
            this.OffendingDirectory = OffendingDirectory;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DirectoryIsFullException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="OffendingDirectory">Offending directory.</param>
        public DirectoryIsFullException(string message, Exception inner, DirectoryInfo OffendingDirectory = null) : base(message, inner)
        {
            this.OffendingDirectory = OffendingDirectory;
        }
    }
    /// <summary>
    /// Excepción que se produce si la contraseña es incorrecta
    /// </summary>
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPasswordException"/>.
        /// </summary>
        public InvalidPasswordException() : base(St.IncorrectPWD) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InvalidPasswordException(Exception inner) : base(St.IncorrectPWD, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public InvalidPasswordException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPasswordException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al intentar utilizar una característica no disponible
    /// </summary>
    [Serializable]
    public class FeatureNotAvailableException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        protected FeatureNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        public FeatureNotAvailableException() : base(St.FeatNotAvailable) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public FeatureNotAvailableException(Exception inner) : base(St.FeatNotAvailable, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public FeatureNotAvailableException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FeatureNotAvailableException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public FeatureNotAvailableException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al enviar demasiados parámetros a un método
    /// </summary>
    [Serializable]
    public class TooManyArgumentsException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        protected TooManyArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooManyArgumentsException"/>.
        /// </summary>
        public TooManyArgumentsException() : base(St.TooManyArguments) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public TooManyArgumentsException(Exception inner) : base(St.TooManyArguments, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public TooManyArgumentsException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooManyArgumentsException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public TooManyArgumentsException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al enviar muy pocos parámetros a un método
    /// </summary>
    [Serializable]
    public class TooFewArgumentsException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        protected TooFewArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooFewArgumentsException"/>.
        /// </summary>
        public TooFewArgumentsException() : base(St.TooFewArguments) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public TooFewArgumentsException(Exception inner) : base(St.TooFewArguments, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public TooFewArgumentsException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TooFewArgumentsException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public TooFewArgumentsException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando falta un argumento
    /// </summary>
    [Serializable]
    public class MissingArgumentException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        protected MissingArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingArgumentException"/>.
        /// </summary>
        public MissingArgumentException() : base(St.MissingArgument(St.Needed)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public MissingArgumentException(Exception inner) : base(St.MissingArgument(St.Needed), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="ArgumentName">Argument name.</param>
        public MissingArgumentException(string ArgumentName) : base(St.MissingArgument(ArgumentName)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MissingArgumentException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public MissingArgumentException(string message, Exception inner) : base(message, inner) { }
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
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected InvalidReturnValueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidReturnValueException"/> .
        /// </summary>
        public InvalidReturnValueException() : base(St.XReturnedInvalid(St.TheFunc)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la excepción con el delegado
        /// especificado.
        /// </summary>
        /// <param name="Function"><see cref="Delegate"/> cuyo resultado causó
        /// la excepción.</param>
        public InvalidReturnValueException(Delegate Function) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, Function.Method.Name)))
        {
            OffendingFunction = Function;
            OffendingFunctionName = OffendingFunction.Method.Name;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la excepción especificando el nombre de
        /// la función.
        /// </summary>
        /// <param name="FunctionName">Nombre de la función cuyo resultado ha causado la excepción</param>
        public InvalidReturnValueException(string FunctionName) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, FunctionName)))
        {
            OffendingFunctionName = FunctionName;
        }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="Function"><see cref="Delegate"/> cuyo resultado causó
        /// la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        public InvalidReturnValueException(Delegate Function, object ReturnValue) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, Function.Method.Name)))
        {
            OffendingFunction = Function;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="FunctionName">Nombre de la función cuyo resultado ha
        /// causado la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        public InvalidReturnValueException(string FunctionName, object ReturnValue) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, FunctionName)))
        {
            OffendingFunctionName = FunctionName;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Inicializa una nueva instancia de esta excepción con el mensaje y la
        /// excepción interna especificadas.
        /// </summary>
        /// <param name="message">Mensaje informativo acerca de esta excepción.</param>
        /// <param name="inner">Excepción interna que causó esta excepción.</param>
        public InvalidReturnValueException(string message, Exception inner) : base(message, inner)
        {
        }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="Function"><see cref="Delegate"/> cuyo resultado causó
        /// la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        /// <param name="inner">Excepción interna que causó esta excepción.</param>
        public InvalidReturnValueException(Delegate Function, object ReturnValue, Exception inner) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, Function.Method.Name)), inner)
        {
            OffendingFunction = Function;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="FunctionName">Nombre de la función cuyo resultado ha
        /// causado la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        /// <param name="inner">Excepción interna que causó esta excepción.</param>
        public InvalidReturnValueException(string FunctionName, object ReturnValue, Exception inner) : base(St.XReturnedInvalid(St.XYQuotes(St.TheFunc, FunctionName)), inner)
        {
            OffendingFunctionName = FunctionName;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Obtiene el delegado del método que ha causado la excepción
        /// </summary>
        /// <returns>Un objeto <see cref="Delegate"/> cuyo resultado causó la excepción</returns>
        public readonly Delegate OffendingFunction;
        /// <summary>
        /// Obtiene el nombre de la función que ha causado la excepción
        /// </summary>
        /// <returns>El nombre de la función cuyo resultado causó la excepción</returns>
        public readonly string OffendingFunctionName;
        /// <summary>
        /// Obtiene el valor que ha causado la excepción
        /// </summary>
        /// <returns>El valor inválido devuelto por la función, causante de la excepción</returns>
        public readonly object OffendingReturnValue;
    }
    /// <summary>
    /// Excepción que produce un <see cref="PluginSupport.Plugin"/> al encontrar un error
    /// </summary>
    [Serializable]
    public class PluginException : Exception
    {
        /// <summary>
        /// The offending plugin.
        /// </summary>
        public readonly PluginSupport.IPlugin OffendingPlugin;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        public PluginException() : base(St.XFoundError(St.ThePlugin)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public PluginException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginException(Exception inner) : base(St.XFoundError(St.ThePlugin), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        /// <param name="Plugin">Plugin.</param>
        public PluginException(PluginSupport.IPlugin Plugin) : base(St.XFoundError(St.XYQuotes(St.ThePlugin, Plugin.Name)))
        {
            OffendingPlugin = Plugin;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        /// <param name="Plugin">Plugin.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public PluginException(PluginSupport.IPlugin Plugin, string message) : base(message)
        {
            OffendingPlugin = Plugin;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        /// <param name="Plugin">Plugin.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginException(PluginSupport.IPlugin Plugin, Exception inner) : base(St.XFoundError(St.XYQuotes(St.ThePlugin, Plugin.Name)), inner)
        {
            OffendingPlugin = Plugin;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginException"/>.
        /// </summary>
        /// <param name="Plugin">Plugin.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginException(PluginSupport.IPlugin Plugin, string message, Exception inner) : base(message, inner)
        {
            OffendingPlugin = Plugin;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Excepción que se produce cuando se requere un plugin para continuar
    /// </summary>
    [Serializable]
    public class PluginNeededException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginNeededException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected PluginNeededException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginNeededException"/>.
        /// </summary>
        public PluginNeededException() : base(St.PluginNeeded) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginNeededException"/>.
        /// </summary>
        /// <param name="ClassType">Class type.</param>
        public PluginNeededException(Type ClassType) : base(St.PluginXNeeded(ClassType.Name))
        {
            RequiredClassType = ClassType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginNeededException"/>.
        /// </summary>
        /// <param name="ClassName">Class name.</param>
        public PluginNeededException(string ClassName) : base(St.PluginXNeeded(ClassName)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginNeededException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginNeededException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Tipo de clase requerida.
        /// </summary>
        public readonly Type RequiredClassType;
    }
    /// <summary>
    /// Excepción que se produce cuando un <see cref="PluginSupport.Plugin"/> encuentra un error al inicializarse.
    /// </summary>
    [Serializable]
    public class PluginInitializationException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginInitializationException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected PluginInitializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException"/>.
        /// </summary>
        public PluginInitializationException() : base(St.PluginDidntInit) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public PluginInitializationException(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginInitializationException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginInitializationException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando la clase no puede ser cargada como <see cref="PluginSupport.Plugin"/>
    /// </summary>
    [Serializable]
    public class InvalidPluginClassException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidPluginClassException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected InvalidPluginClassException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPluginClassException"/>.
        /// </summary>
        public InvalidPluginClassException() : base(St.invalidPluginClass) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPluginClassException"/>.
        /// </summary>
        /// <param name="ClassType">Class type.</param>
        public InvalidPluginClassException(Type ClassType) : base(St.InvalidPluginClass(St.XYQuotes(St.TheClass, ClassType.Name)))
        {
            RequiredClassType = ClassType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidPluginClassException"/>.
        /// </summary>
        /// <param name="ClassName">Nombre de la clase que es la causa de este 
        /// <see cref="InvalidPluginClassException"/>.</param>
        public InvalidPluginClassException(string ClassName) : base(St.InvalidPluginClass(St.XYQuotes(St.TheClass, ClassName))) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidPluginClassException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public InvalidPluginClassException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Tipo de clase requerida.
        /// </summary>
        public readonly Type RequiredClassType;
    }
    /// <summary>
    /// Excepción que se produce cuando un <see cref="System.Reflection.Assembly"/> no contiene la clase especificada.
    /// </summary>
    [Serializable]
    public class PluginClassNotFoundException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        /// <param name="info">
        /// El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        /// La información contextual acerca del orígen o el destino.
        /// </param>
        protected PluginClassNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        public PluginClassNotFoundException() : base(St.XDoesntContainY(St.ThePlugin, St.TheClass.ToLower())) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        /// <param name="ClassType">Class type.</param>
        public PluginClassNotFoundException(Type ClassType) : base(St.XDoesntContainY(St.ThePlugin, St.XYQuotes(St.TheClass.ToLower(), ClassType.Name)))
        {
            RequiredClassType = ClassType;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        /// <param name="ClassName">Class name.</param>
        public PluginClassNotFoundException(string ClassName) : base(St.XDoesntContainY(St.ThePlugin, St.XYQuotes(St.TheClass.ToLower(), ClassName))) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginClassNotFoundException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public PluginClassNotFoundException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Tipo de clase requerida.
        /// </summary>
        public readonly Type RequiredClassType;
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
        /// <summary>
        /// Tipo que ha causado la excepción.
        /// </summary>
        public readonly Type OffendingType;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        public InvalidTypeException() : base(St.XIsInvalid(St.TheType)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="Type">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(Type Type) : base(St.XIsInvalid(St.TheType))
        {
            OffendingType = Type;
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
        /// <param name="Type">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(string message, Type Type) : base(message)
        {
            OffendingType = Type;
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
        /// <param name="Type">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(Exception inner, Type Type) : base(St.XIsInvalid(St.TheType), inner)
        {
            OffendingType = Type;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InvalidTypeException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="Type">Tipo que ha causado la excepción.</param>
        public InvalidTypeException(string message, Exception inner, Type Type) : base(message, inner)
        {
            OffendingType = Type;
        }
    }
    /// <summary>
    /// Excepción que se produce al intentar crear nueva información dentro de una Database con un Uid que ya existe
    /// </summary>
    [Serializable]
    public class DataAlreadyExistsException : Exception
    {
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
        /// <summary>
        /// The offending ip.
        /// </summary>
        public readonly System.Net.IPEndPoint OffendingIP;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        public CouldntConnectException() : base(St.CldntConnect(St.TheSrv.ToLower())) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="EndPoint">End point.</param>
        public CouldntConnectException(System.Net.IPEndPoint EndPoint) : base(St.CldntConnect($"{EndPoint.Address}:{EndPoint.Port}"))
        {
            OffendingIP = EndPoint;
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
        /// <param name="EndPoint">End point.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public CouldntConnectException(System.Net.IPEndPoint EndPoint, string message) : base(message)
        {
            OffendingIP = EndPoint;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="EndPoint">End point.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public CouldntConnectException(System.Net.IPEndPoint EndPoint, Exception inner) : base(St.CldntConnect($"{EndPoint.Address}:{EndPoint.Port}"), inner)
        {
            OffendingIP = EndPoint;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CouldntConnectException"/>.
        /// </summary>
        /// <param name="EndPoint">End point.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public CouldntConnectException(System.Net.IPEndPoint EndPoint, string message, Exception inner) : base(message, inner)
        {
            OffendingIP = EndPoint;
        }
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
        /// <summary>
        /// The offending task.
        /// </summary>
        public readonly Delegate OffendingTask;
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
        /// <param name="Task">Task.</param>
        public OperationException(Delegate Task) : base(St.ExcDoingX(St.XYQuotes(St.TheTask.ToLower(), Task.Method.Name)))
        {
            OffendingTask = Task;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="Task">Task.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        public OperationException(Delegate Task, string message) : base(message)
        {
            OffendingTask = Task;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="Task">Task.</param>
        /// <param name="info">El objeto que contiene la información de serialización.</param>
        /// <param name="context">La información contextual acerca del orígen o el destino.</param>
        public OperationException(Delegate Task, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            OffendingTask = Task;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="Task">Task.</param>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public OperationException(Delegate Task, string message, Exception inner) : base(message, inner)
        {
            OffendingTask = Task;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OperationException"/>.
        /// </summary>
        /// <param name="Task">Task.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        public OperationException(Delegate Task, Exception inner) : base(St.ExcDoingX(St.XYQuotes(St.TheTask.ToLower(), Task.Method.Name)), inner)
        {
            OffendingTask = Task;
        }
    }
    /// <summary>
    /// Excepción que se produce cuando la firma de un método representado en
    /// un <see cref="MethodInfo"/> no es válida.
    /// </summary>
    [Serializable]
    public class InvalidMethodSignatureException : Exception
    {
        /// <summary>
        /// Referencia al método que ha causado la excepción.
        /// </summary>
        public readonly MethodInfo OffendingMethod;
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
        public InvalidMethodSignatureException(MethodInfo method) : base(St.InvalidSignature(St.XYQuotes(St.TheMethod, method.Name))) { OffendingMethod = method; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(string message, MethodInfo method = null) : base(message) { OffendingMethod = method; }
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
        public InvalidMethodSignatureException(Exception inner, MethodInfo method) : base(St.InvalidSignature(St.XYQuotes(St.TheMethod, method.Name)), inner) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </summary>
        /// <param name="message">Un <see cref="string"/> que describe a la excepción.</param>
        /// <param name="inner"><see cref="Exception"/> que es la causa de esta excepción.</param>
        /// <param name="method">Método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(string message, Exception inner, MethodInfo method = null) : base(message, inner) { OffendingMethod = method; }
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
        protected InvalidMethodSignatureException(SerializationInfo info, StreamingContext context, MethodInfo method = null) : base(info, context) { OffendingMethod = method; }
    }
}