//
//  Exceptions.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
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
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        public InterfaceExpectedException() : base(St.InterfaceExpected) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(Type T) : base(St.InterfaceExpected)
        {
            OffendingType = T;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        public InterfaceExpectedException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(string message, Type T)
            : base(message) { OffendingType = T; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="inner">The exception that is the cause of the current
        /// exception.</param>
        public InterfaceExpectedException(Exception inner)
            : base(St.InterfaceExpected, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="inner">The exception that is the cause of the current
        /// exception.</param>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(Exception inner, Type T)
            : base(St.InterfaceExpected, inner) { OffendingType = T; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        /// <param name="inner">The exception that is the cause of the current
        /// exception. </param>
        public InterfaceExpectedException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes
        /// the exception.</param>
        /// <param name="inner">The exception that is the cause of the current
        /// exception.</param>
        /// <param name="T">Tipo que generó la excepción.</param>
        public InterfaceExpectedException(string message, Exception inner, Type T)
            : base(message, inner) { OffendingType = T; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class
        /// </summary>
        /// <param name="context">The contextual information about the source or
        /// destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        protected InterfaceExpectedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="context">The contextual information about the source or
        /// destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
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
        /// Initializes a new instance of the <see cref="T:MCART.Exceptions.NotPluginException"/> class.
        /// </summary>
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(System.Reflection.Assembly offendingAssembly = null)
            : base(string.Format(St.XInvalid, St.TheAssembly)) { OffendingAssembly = offendingAssembly; }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MCART.Exceptions.NotPluginException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception.</param>
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(string message, System.Reflection.Assembly offendingAssembly = null)
            : base(message) { OffendingAssembly = offendingAssembly; }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MCART.Exceptions.NotPluginException"/> class.
        /// </summary>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(Exception inner, System.Reflection.Assembly offendingAssembly = null)
            : base(string.Format(St.XInvalid, St.TheAssembly), inner) { OffendingAssembly = offendingAssembly; }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MCART.Exceptions.NotPluginException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        /// <param name="offendingAssembly">Offending assembly.</param>
        public NotPluginException(string message, Exception inner, System.Reflection.Assembly offendingAssembly = null)
            : base(message, inner) { OffendingAssembly = offendingAssembly; }
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceExpectedException"/> class.
        /// </summary>
        /// <param name="context">The contextual information about the source or
        /// destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="offendingAssembly">Offending assembly.</param>
        protected NotPluginException(SerializationInfo info, StreamingContext context, System.Reflection.Assembly offendingAssembly = null)
            : base(info, context) { OffendingAssembly = offendingAssembly; }
    }

    /// <summary>
    /// Excepción que se produce cuando un <see cref="IEnumerable{T}"/> está vacío.
    /// </summary>
    [Serializable]
    public class EmptyCollectionException<T> : Exception
    {
        public readonly IEnumerable<T> OffendingCollection;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        public EmptyCollectionException(IEnumerable<T> offendingList) : base(St.LstEmpty) { OffendingCollection = offendingList; }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class.
        /// </summary>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        /// <param name="offendingList">Offending list.</param>
        public EmptyCollectionException(Exception inner, IEnumerable<T> offendingList) : base(St.LstEmpty, inner) { OffendingCollection = offendingList; }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception.</param>
        public EmptyCollectionException(string message, IEnumerable<T> offendingList) : base(message) { OffendingCollection = offendingList; }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public EmptyCollectionException(string message, Exception inner, IEnumerable<T> offendingList) : base(message, inner) { OffendingCollection = offendingList; }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context, IEnumerable<T> offendingList) : base(info, context) { OffendingCollection = offendingList; }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        public EmptyCollectionException() : base(St.LstEmpty) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception. </param>
        public EmptyCollectionException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class.
        /// </summary>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public EmptyCollectionException(Exception inner) : base(St.LstEmpty, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public EmptyCollectionException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCollectionException{T}"/> class
        /// </summary>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Excepcion que se produce cuando se solicita cargar un <see cref="PluginSupport.Plugin"/> con la interfaz especificada, pero no la implementa.
    /// </summary>
    [Serializable]
    public class InterfaceNotImplementedException : Exception
    {
        protected InterfaceNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        /// <summary>
        /// Obtiene el tipo de la interfaz que se ha solicitado cargar
        /// </summary>
        /// <returns></returns>
        public readonly Type MissingInterface;
        /// <summary>
        /// Crea una nueva instancia de este objeto con los valores predeterminados, sin establecer la instancia solicitada que generó el error
        /// </summary>
        public InterfaceNotImplementedException() : base(string.Format(St.XDoesntContainY, St.ThePlugin, St.TheInterface)) { }
        /// <summary>
        /// Crea una nueva instancia de este objeto con la excepción interna que causó esta excepción
        /// </summary>
        /// <param name="inner"><see cref="Exception"/>que produjo esta excepción</param>
        public InterfaceNotImplementedException(Exception inner) : base(string.Format(St.XDoesntContainY, St.ThePlugin, St.TheInterface, inner)) { }
        /// <summary>
        /// Crea una nueva instancia de este objeto especificando el tipo que causó la excepción
        /// </summary>
        /// <param name="T">Tipo que generó la excepción</param>
        public InterfaceNotImplementedException(Type T) : base(string.Format(St.XDoesntContainY, St.ThePlugin, string.Format(St.XYQuotes, St.TheInterface, T.Name)))
        {
            MissingInterface = T;
        }
        /// <summary>
        /// Crea una nueva instancia de este objeto especificando el tipo que causó la excepción, además de la <see cref="Exception"/> que generó esta.
        /// </summary>
        /// <param name="T">Tipo que generó la excepción</param>
        /// <param name="inner"><see cref="Exception"/>que produjo esta excepción</param>
        public InterfaceNotImplementedException(Type T, Exception inner) : base(string.Format(St.XDoesntContainY, St.ThePlugin, string.Format(St.XYQuotes, St.TheInterface, T.Name)), inner)
        {
            MissingInterface = T;
        }
        /// <summary>
        /// Crea una nueva instancia de este objeto con el mensaje especificado
        /// </summary>
        /// <param name="message">Mensaje sobre la excepción</param>
        public InterfaceNotImplementedException(string message) : base(message) { }
        /// <summary>
        /// Crea una nueva instancia de este objeto especificando el mensaje, la <see cref="Exception"/> y el tipo que generó esta excepción.
        /// </summary>
        /// <param name="message">Mensaje sobre la excepción</param>
        /// <param name="inner"><see cref="Exception"/>que produjo esta excepción</param>
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
        public readonly DirectoryInfo OffendingDirectory;

        protected DirectoryIsFullException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public DirectoryIsFullException(DirectoryInfo OffendingDirectory = null) : base(St.DirectoryIsFull)
        {
            this.OffendingDirectory = OffendingDirectory;
        }

        public DirectoryIsFullException(Exception inner, DirectoryInfo OffendingDirectory = null) : base(St.DirectoryIsFull, inner)
        {
            this.OffendingDirectory = OffendingDirectory;
        }

        public DirectoryIsFullException(string message, DirectoryInfo OffendingDirectory = null) : base(message)
        {
            this.OffendingDirectory = OffendingDirectory;
        }

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
        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public InvalidPasswordException() : base(St.IncorrectPWD) { }
        public InvalidPasswordException(Exception inner) : base(St.IncorrectPWD, inner) { }
        public InvalidPasswordException(string message) : base(message) { }
        public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Excepción que se produce al intentar utilizar una característica no disponible
    /// </summary>
    [Serializable]
    public class FeatureNotAvailableException : Exception
    {
        protected FeatureNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public FeatureNotAvailableException() : base(St.FeatNotAvailable) { }
        public FeatureNotAvailableException(Exception inner) : base(St.FeatNotAvailable, inner) { }
        public FeatureNotAvailableException(string message) : base(message) { }
        public FeatureNotAvailableException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Excepción que se produce al enviar demasiados parámetros a un método
    /// </summary>
    [Serializable]
    public class TooManyArgumentsException : Exception
    {
        protected TooManyArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public TooManyArgumentsException() : base(St.TooManyArguments) { }
        public TooManyArgumentsException(Exception inner) : base(St.TooManyArguments, inner) { }
        public TooManyArgumentsException(string message) : base(message) { }
        public TooManyArgumentsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Excepción que se produce al enviar muy pocos parámetros a un método
    /// </summary>
    [Serializable]
    public class TooFewArgumentsException : Exception
    {
        protected TooFewArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public TooFewArgumentsException() : base(St.TooFewArguments) { }
        public TooFewArgumentsException(Exception inner) : base(St.TooFewArguments, inner) { }
        public TooFewArgumentsException(string message) : base(message) { }
        public TooFewArgumentsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Excepción que se produce cuando falta un argumento
    /// </summary>
    [Serializable]
    public class MissingArgumentException : Exception
    {
        protected MissingArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public MissingArgumentException() : base(string.Format(St.MissingArgument, St.Needed)) { }
        public MissingArgumentException(Exception inner) : base(string.Format(St.MissingArgument, St.Needed), inner) { }
        public MissingArgumentException(string ArgumentName) : base(string.Format(St.MissingArgument, ArgumentName)) { }
        public MissingArgumentException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Excepción que se produce cuando se detecta que una función ha devuelto
    /// un valor inválido sin generar una excepción.
    /// </summary>
    public class InvalidReturnValueException : Exception
    {
        /// <summary>
        /// Crea una nueva instancia de este objeto.
        /// </summary>
        public InvalidReturnValueException() : base(string.Format(St.XReturnedInvalid, St.TheFunc)) { }
        /// <summary>
        /// Crea una nueva instancia de la excepción con el delegado
        /// especificado.
        /// </summary>
        /// <param name="Function"><see cref="Delegate"/> cuyo resultado causó
        /// la excepción.</param>
        public InvalidReturnValueException(Delegate Function) : base(string.Format(St.XReturnedInvalid, string.Format(St.XYQuotes, St.TheFunc, Function.Method.Name)))
        {
            OffendingFunction = Function;
            OffendingFunctionName = OffendingFunction.Method.Name;
        }
        /// <summary>
        /// Crea una nueva instancia de la excepción especificando el nombre de
        /// la función.
        /// </summary>
        /// <param name="FunctionName">Nombre de la función cuyo resultado ha causado la excepción</param>
        public InvalidReturnValueException(string FunctionName) : base(string.Format(St.XReturnedInvalid, string.Format(St.XYQuotes, St.TheFunc, FunctionName)))
        {
            OffendingFunctionName = FunctionName;
        }
        /// <summary>
        /// Crea una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="Function"><see cref="Delegate"/> cuyo resultado causó
        /// la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        public InvalidReturnValueException(Delegate Function, object ReturnValue) : base(string.Format(St.XReturnedInvalid, string.Format(St.XYQuotes, St.TheFunc, Function.Method.Name)))
        {
            OffendingFunction = Function;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Crea una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="FunctionName">Nombre de la función cuyo resultado ha
        /// causado la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        public InvalidReturnValueException(string FunctionName, object ReturnValue) : base(string.Format(St.XReturnedInvalid, string.Format(St.XYQuotes, St.TheFunc, FunctionName)))
        {
            OffendingFunctionName = FunctionName;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Crea una nueva instancia de esta excepción con el mensaje y la
        /// excepción interna especificadas.
        /// </summary>
        /// <param name="message">Mensaje informativo acerca de esta excepción.</param>
        /// <param name="inner">Excepción interna que causó esta excepción.</param>
        public InvalidReturnValueException(string message, Exception inner) : base(message, inner)
        {
        }
        /// <summary>
        /// Crea una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="Function"><see cref="[Delegate]"/> cuyo resultado causó
        /// la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        /// <param name="inner">Excepción interna que causó esta excepción.</param>
        public InvalidReturnValueException(Delegate Function, object ReturnValue, Exception inner) : base(string.Format(St.XReturnedInvalid, string.Format(St.XYQuotes, St.TheFunc, Function.Method.Name)), inner)
        {
            OffendingFunction = Function;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Crea una nueva instancia de este objeto con la función y el valor
        /// devuelto especificados.
        /// </summary>
        /// <param name="FunctionName">Nombre de la función cuyo resultado ha
        /// causado la excepción.</param>
        /// <param name="ReturnValue">Valor inválido que ha causado esta
        /// excepción.</param>
        /// <param name="inner">Excepción interna que causó esta excepción.</param>
        public InvalidReturnValueException(string FunctionName, object ReturnValue, Exception inner) : base(string.Format(St.XReturnedInvalid, string.Format(St.XYQuotes, St.TheFunc, FunctionName)), inner)
        {
            OffendingFunctionName = FunctionName;
            OffendingReturnValue = ReturnValue;
        }
        /// <summary>
        /// Obtiene el delegado del método que ha causado la excepción
        /// </summary>
        /// <returns>Un objeto <see cref="[Delegate]"/> cuyo resultado causó la excepción</returns>
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
    public class PluginException : Exception
    {
        public readonly PluginSupport.IPlugin OffendingPlugin;
        public PluginException() : base(string.Format(St.XFoundError, St.ThePlugin)) { }
        public PluginException(string message) : base(message) { }
        public PluginException(Exception inner) : base(string.Format(St.XFoundError, St.ThePlugin, inner)) { }
        public PluginException(string message, Exception inner) : base(message, inner) { }
        public PluginException(PluginSupport.IPlugin Plugin) : base(string.Format(St.XFoundError, string.Format(St.XYQuotes, St.ThePlugin, Plugin.Name)))
        {
            OffendingPlugin = Plugin;
        }
        public PluginException(PluginSupport.IPlugin Plugin, string message) : base(message)
        {
            OffendingPlugin = Plugin;
        }
        public PluginException(PluginSupport.IPlugin Plugin, Exception inner) : base(string.Format(St.XFoundError, string.Format(St.XYQuotes, St.ThePlugin, Plugin.Name)), inner)
        {
            OffendingPlugin = Plugin;
        }
        public PluginException(PluginSupport.IPlugin Plugin, string message, Exception inner) : base(message, inner)
        {
            OffendingPlugin = Plugin;
        }
    }

    /// <summary>
    /// Excepción que se produce cuando se requere un plugin para continuar
    /// </summary>
    public class PluginNeededException : Exception
    {
        public PluginNeededException() : base(St.PluginNeeded) { }
        public PluginNeededException(Type ClassType) : base(St.PluginNeeded + string.Format(St.OfX, ClassType.Name))
        {
            RequiredClassType = ClassType;
        }
        public PluginNeededException(string ClassName) : base(St.PluginNeeded + string.Format(St.OfX, ClassName)) { }
        public PluginNeededException(string message, Exception inner) : base(message, inner) { }
        public readonly Type RequiredClassType;
    }
    /// <summary>
    /// Excepción que se produce cuando un <see cref="PluginSupport.Plugin"/> encuentra un error al inicializarse
    /// </summary>
    public class PluginInitializationException : Exception
    {
        public PluginInitializationException() : base(St.PluginDidntInit) { }
        public PluginInitializationException(string message) : base(message) { }
        public PluginInitializationException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando la clase no puede ser cargada como <see cref="PluginSupport.Plugin"/>
    /// </summary>
    public class InvalidPluginClassException : Exception
    {
        public InvalidPluginClassException() : base(string.Format(St.InvalidPluginClass, St.TheClass)) { }
        public InvalidPluginClassException(Type ClassType) : base(string.Format(St.InvalidPluginClass, string.Format(St.XYQuotes, St.TheClass, ClassType.Name)))
        {
            RequiredClassType = ClassType;
        }
        public InvalidPluginClassException(string ClassName) : base(string.Format(St.InvalidPluginClass, string.Format(St.XYQuotes, St.TheClass, ClassName))) { }
        public InvalidPluginClassException(string message, Exception inner) : base(message, inner) { }
        public readonly Type RequiredClassType;
    }
    /// <summary>
    /// Excepción que se produce cuando un <see cref="System.Reflection.Assembly"/> no contiene la clase especificada
    /// </summary>
    public class PluginClassNotFoundException : Exception
    {
        public PluginClassNotFoundException() : base(string.Format(St.XDoesntContainY, St.ThePlugin, St.TheClass.ToLower())) { }
        public PluginClassNotFoundException(Type ClassType) : base(string.Format(St.XDoesntContainY, St.ThePlugin, string.Format(St.XYQuotes, St.TheClass.ToLower(), ClassType.Name)))
        {
            RequiredClassType = ClassType;
        }
        public PluginClassNotFoundException(string ClassName) : base(string.Format(St.XDoesntContainY, St.ThePlugin, string.Format(St.XYQuotes, St.TheClass.ToLower(), ClassName))) { }
        public PluginClassNotFoundException(string message, Exception inner) : base(message, inner) { }
        public readonly Type RequiredClassType;
    }
    /// <summary>
    /// Excepción que se produce al intentar remover un objeto de una pila vacía
    /// </summary>
    public class StackUnderflowException : Exception
    {
        public StackUnderflowException() : base(St.StackUnderflow) { }
        public StackUnderflowException(string message) : base(message) { }
        public StackUnderflowException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando se intenta cargar un archivo que no es comprensible por la clase Database
    /// </summary>
    public class InvalidDatabaseException : Exception
    {
        public InvalidDatabaseException() : base(St.InvalidDB) { }
        public InvalidDatabaseException(string message) : base(message) { }
        public InvalidDatabaseException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando un objeto Database no está etiquetado para su uso en este ensamblado
    /// </summary>
    public class NotMyDatabaseException : Exception
    {
        public NotMyDatabaseException() : base(St.DBDoesntBelong) { }
        public NotMyDatabaseException(string message) : base(message) { }
        public NotMyDatabaseException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción  que se produce al no encontrar los datos solicitados
    /// </summary>
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException() : base(St.DataNotFound)
        {
            throw new DataNotFoundException();
        }
        public DataNotFoundException(string message) : base(message) { }
        public DataNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al no haber suficientes datos
    /// </summary>
    public class InsufficientDataException : Exception
    {
        public InsufficientDataException() : base(St.NotEnoughData) { }
        public InsufficientDataException(string message) : base(message) { }
        public InsufficientDataException(Exception inner) : base(St.NotEnoughData, inner) { }
        public InsufficientDataException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al hacer referencia a un tipo desconodico
    /// </summary>
    public class UnknownTypeException : Exception
    {
        public UnknownTypeException() : base(St.UnknownType) { }
        public UnknownTypeException(string message) : base(message) { }
        public UnknownTypeException(Exception inner) : base(St.UnknownType, inner) { }
        public UnknownTypeException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce al hacer referencia a un tipo inválido
    /// </summary>
    public class InvalidTypeException : Exception
    {
        public readonly Type OffendingType;
        public InvalidTypeException() : base(string.Format(St.XInvalid, St.TheType)) { }
        public InvalidTypeException(Type Type) : base(string.Format(St.XInvalid, St.TheType))
        {
            OffendingType = Type;
        }
        public InvalidTypeException(string message) : base(message) { }
        public InvalidTypeException(string message, Type Type) : base(message)
        {
            OffendingType = Type;
        }
        public InvalidTypeException(Exception inner) : base(string.Format(St.XInvalid, St.TheType), inner) { }
        public InvalidTypeException(string message, Exception inner) : base(message, inner) { }
        public InvalidTypeException(Exception inner, Type Type) : base(string.Format(St.XInvalid, St.TheType), inner)
        {
            OffendingType = Type;
        }
        public InvalidTypeException(string message, Exception inner, Type Type) : base(message, inner)
        {
            OffendingType = Type;
        }
    }
    /// <summary>
    /// Excepción que se produce al intentar crear nueva información dentro de una Database con un Uid que ya existe
    /// </summary>
    public class DataAlreadyExistsException : Exception
    {
        public DataAlreadyExistsException() : base(string.Format(St.XAlreadyExists, St.TheUid)) { }
        public DataAlreadyExistsException(string Uid) : base(string.Format(St.XAlreadyExists, string.Format(St.XYQuotes, St.TheUid, Uid))) { }
        public DataAlreadyExistsException(string Uid, Exception inner) : base(string.Format(St.XAlreadyExists, string.Format(St.XYQuotes, St.TheUid, Uid)), inner) { }
    }

    ///// <summary>
    ///// Excepción que se produce cuando no se ha encontrado el <see cref="MCART.XmlDB.MCADBElement"/> especificado
    ///// </summary>
    //public class DBElementNotFoundException : Exception
    //{
    //    public DBElementNotFoundException() : base(St.DataNotFound)
    //    {
    //    }
    //    public DBElementNotFoundException(MCADatabase.MCADBElement DBElement) : base(string.Format(St.MCADBObjNotFound, DBElement.GetType.Name, DBElement.Uid))
    //    {
    //    }
    //    public DBElementNotFoundException(string message) : base(message)
    //    {
    //    }
    //    public DBElementNotFoundException(string message, Exception inner) : base(message, inner)
    //    {
    //    }
    //}

    /// <summary>
    /// Excepción que se produce cuando se llama a un método de un <see cref="PluginSupport.Plugin"/> sin inicializar
    /// </summary>
    public class PluginNotInitializedException : Exception
    {
        public PluginNotInitializedException() : base(string.Format(St.XNotInit, St.ThePlugin)) { }
        public PluginNotInitializedException(string message) : base(message) { }
        public PluginNotInitializedException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando no se puede resolver un nombre DNS, o no se encuentra el servidor especificado.
    /// </summary>
    public class ServerNotFoundException : Exception
    {
        public ServerNotFoundException() : base(string.Format(St.XNotFound, St.TheSrv)) { }
        public ServerNotFoundException(string message) : base(message) { }
        public ServerNotFoundException(string message, Exception inner) : base(message, inner) { }
        public ServerNotFoundException(Exception inner) : base(string.Format(St.XNotFound, St.TheSrv), inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando se reciben datos corruptos
    /// </summary>
    public class CorruptDataException : Exception
    {
        public CorruptDataException() : base(St.CorruptData) { }
        public CorruptDataException(string message) : base(message) { }
        public CorruptDataException(string message, Exception inner) : base(message, inner) { }
        public CorruptDataException(Exception inner) : base(St.CorruptData, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando no se puede realizar la conexión
    /// </summary>
    public class CouldntConnectException : Exception
    {
        public readonly System.Net.IPEndPoint OffendingIP;
        public CouldntConnectException() : base(string.Format(St.CldntConnect, St.TheSrv.ToLower())) { }
        public CouldntConnectException(System.Net.IPEndPoint EndPoint) : base(string.Format(St.CldntConnect, EndPoint.Address + ":" + EndPoint.Port))
        {
            OffendingIP = EndPoint;
        }
        public CouldntConnectException(string message) : base(message) { }
        public CouldntConnectException(string message, Exception inner) : base(message, inner) { }
        public CouldntConnectException(System.Net.IPEndPoint EndPoint, string message) : base(message)
        {
            OffendingIP = EndPoint;
        }
        public CouldntConnectException(System.Net.IPEndPoint EndPoint, Exception inner) : base(string.Format(St.CldntConnect, EndPoint.Address + ":" + EndPoint.Port), inner)
        {
            OffendingIP = EndPoint;
        }
        public CouldntConnectException(System.Net.IPEndPoint EndPoint, string message, Exception inner) : base(message, inner)
        {
            OffendingIP = EndPoint;
        }
    }
    /// <summary>
    /// Excepción que se produce cuando la conexión se encontraba cerrada al intentar enviar o recibir datos
    /// </summary>
    public class ConnectionClosedException : Exception
    {
        public ConnectionClosedException() : base(St.ClosdConn) { }
        public ConnectionClosedException(string message) : base(message) { }
        public ConnectionClosedException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    /// Excepción que se produce cuando una operación falla
    /// </summary>
    public class OperationException : Exception
    {
        public readonly Delegate OffendingTask;
        public OperationException() : base(string.Format(St.ExcDoingX, St.TheTask.ToLower())) { }
        public OperationException(string message) : base(message) { }
        public OperationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public OperationException(string message, Exception inner) : base(message, inner) { }
        public OperationException(Exception inner) : base(string.Format(St.ExcDoingX, St.TheTask.ToLower()), inner) { }
        public OperationException(Delegate Task) : base(string.Format(St.ExcDoingX, string.Format(St.XYQuotes, St.TheTask.ToLower(), Task.Method.Name)))
        {
            OffendingTask = Task;
        }
        public OperationException(Delegate Task, string message) : base(message)
        {
            OffendingTask = Task;
        }
        public OperationException(Delegate Task, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            OffendingTask = Task;
        }
        public OperationException(Delegate Task, string message, Exception inner) : base(message, inner)
        {
            OffendingTask = Task;
        }
        public OperationException(Delegate Task, Exception inner) : base(string.Format(St.ExcDoingX, string.Format(St.XYQuotes, St.TheTask.ToLower(), Task.Method.Name)), inner)
        {
            OffendingTask = Task;
        }
    }
}