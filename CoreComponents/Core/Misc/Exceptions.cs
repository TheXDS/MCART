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
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using TheXDS.MCART.PluginSupport;
using St = TheXDS.MCART.Resources.Strings;

#region Configuración de ReSharper

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#endregion

namespace TheXDS.MCART.Exceptions
{
    /// <summary>
    /// Se produce cuando una función soportada detecta una de las siguientes situaciones:
    /// <list type="bullet">
    /// <item>
    /// <description>Valores de retorno alterados inesperadamente.</description> 
    /// </item>
    /// <item>
    /// <description>Valor de retorno fuera del rango conocido esperado de una función.</description>
    /// </item>
    /// <item>
    /// <description>Corrupción de memoria no capturada por CLR.</description>
    /// </item>
    /// <item>
    /// <description>Modificación externa de valores internos protegidos de la aplicación.</description>
    /// </item>
    /// </list>
    /// </summary>
    [Serializable]
    public class TamperException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TamperException" />.
        /// </summary>
        public TamperException():base(St.TamperDetected)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TamperException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public TamperException(string message) : base(message)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TamperException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public TamperException(Exception inner) : this(St.TamperDetected, inner)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TamperException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public TamperException(string message, Exception inner) : base(message, inner)
        {
        }
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TamperException" /> con datos serializados.
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
        protected TamperException(SerializationInfo info,StreamingContext context) : base(info, context)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción estándar producida al encontrarse un problema con un objeto.
    /// </summary>
    [Serializable]
    public class OffendingException<T> : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        public OffendingException() : base(St.XIsInvalid(St.TheObj))
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        /// <param name="offendingObject">
        ///     Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(T offendingObject) : this()
        {
            OffendingObject = offendingObject;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public OffendingException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingObject">
        ///     Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, T offendingObject) : base(message)
        {
            OffendingObject = offendingObject;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public OffendingException(Exception inner) : base(St.XIsInvalid(St.TheType), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        ///     Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(Exception inner, T offendingObject) : this(inner)
        {
            OffendingObject = offendingObject;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingObject">
        ///     Objeto que es la causa de esta excepción.
        /// </param>
        public OffendingException(string message, Exception inner, T offendingObject) : base(message, inner)
        {
            OffendingObject = offendingObject;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" /> con datos serializados.
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
        protected OffendingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.OffendingException`1" /> con datos serializados.
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
        /// <param name="offendingObject">
        ///     Objeto que es la causa de esta excepción.
        /// </param>
        protected OffendingException(SerializationInfo info, StreamingContext context, T offendingObject) : base(info,
            context)
        {
            OffendingObject = offendingObject;
        }

        /// <summary>
        ///     Objeto que ha causado la excepción.
        /// </summary>
        public T OffendingObject { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se intenta cargar plugins desde un ensamblado que no contiene ninguna clase
    ///     cargable como <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
    /// </summary>
    [Serializable]
    public class NotPluginException : OffendingException<Assembly>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
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
        protected NotPluginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
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
        /// <param name="assembly">
        ///     <see cref="T:System.Reflection.Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
        /// </param>
        protected NotPluginException(SerializationInfo info, StreamingContext context, Assembly assembly) : base(info,
            context, assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        public NotPluginException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        /// <param name="assembly">
        ///     <see cref="T:System.Reflection.Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
        /// </param>
        public NotPluginException(Assembly assembly) : base(Msg(assembly), assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public NotPluginException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="assembly">
        ///     <see cref="T:System.Reflection.Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
        /// </param>
        public NotPluginException(string message, Assembly assembly) : base(message, assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="assembly">
        ///     <see cref="T:System.Reflection.Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
        /// </param>
        public NotPluginException(Exception inner, Assembly assembly) : base(Msg(assembly), inner, assembly)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public NotPluginException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.NotPluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="assembly">
        ///     <see cref="T:System.Reflection.Assembly" /> desde el cual se intentó cargar un
        ///     <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
        /// </param>
        public NotPluginException(string message, Exception inner, Assembly assembly) : base(message, inner, assembly)
        {
        }

        private static string Msg()
        {
            return St.XIsInvalid(St.TheAssembly);
        }

        private static string Msg(Assembly assembly)
        {
            return St.XIsInvalid(St.XYQuotes(St.TheAssembly, assembly.FullName));
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando una colección está vacía.
    /// </summary>
    [Serializable]
    public class EmptyCollectionException : OffendingException<IEnumerable>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
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
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
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
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        protected EmptyCollectionException(SerializationInfo info, StreamingContext context,
            IEnumerable offendingCollection) : base(info, context, offendingCollection)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        public EmptyCollectionException() : base(St.LstEmpty)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(IEnumerable offendingCollection) : base(St.LstEmpty, offendingCollection)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public EmptyCollectionException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(string message, IEnumerable offendingCollection) : base(message,
            offendingCollection)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public EmptyCollectionException(Exception inner) : base(St.LstEmpty, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(Exception inner, IEnumerable offendingCollection) : base(St.LstEmpty, inner,
            offendingCollection)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public EmptyCollectionException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.EmptyCollectionException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingCollection">Colección vacía que ha generado la excepción.</param>
        public EmptyCollectionException(string message, Exception inner, IEnumerable offendingCollection) : base(
            message, inner, offendingCollection)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce si la contraseña es incorrecta.
    /// </summary>
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidPasswordException" />.
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
        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidPasswordException" />.
        /// </summary>
        public InvalidPasswordException() : base(St.InvalidPassword)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidPasswordException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidPasswordException(Exception inner) : base(St.InvalidPassword, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidPasswordException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public InvalidPasswordException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidPasswordException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidPasswordException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce al intentar utilizar una característica no
    ///     disponible.
    /// </summary>
    [Serializable]
    public class FeatureNotAvailableException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.FeatureNotAvailableException" />.
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
        protected FeatureNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.FeatureNotAvailableException" />.
        /// </summary>
        public FeatureNotAvailableException() : base(St.FeatNotAvailable)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.FeatureNotAvailableException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public FeatureNotAvailableException(Exception inner) : base(St.FeatNotAvailable, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.FeatureNotAvailableException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public FeatureNotAvailableException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.FeatureNotAvailableException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public FeatureNotAvailableException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce al enviar demasiados parámetros a un método.
    /// </summary>
    [Serializable]
    public class TooManyArgumentsException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooManyArgumentsException" />.
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
        protected TooManyArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooManyArgumentsException" />.
        /// </summary>
        public TooManyArgumentsException() : base(St.TooManyArguments)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooManyArgumentsException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public TooManyArgumentsException(Exception inner) : base(St.TooManyArguments, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooManyArgumentsException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public TooManyArgumentsException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.TooManyArgumentsException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public TooManyArgumentsException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce al enviar muy pocos parámetros a un método.
    /// </summary>
    [Serializable]
    public class TooFewArgumentsException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooFewArgumentsException" />.
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
        protected TooFewArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooFewArgumentsException" />.
        /// </summary>
        public TooFewArgumentsException() : base(St.TooFewArguments)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooFewArgumentsException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public TooFewArgumentsException(Exception inner) : base(St.TooFewArguments, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooFewArgumentsException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public TooFewArgumentsException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.TooFewArgumentsException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public TooFewArgumentsException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando falta un argumento.
    /// </summary>
    [Serializable]
    public class MissingArgumentException : ArgumentException
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingArgumentException" />.
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
        protected MissingArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingArgumentException" />.
        /// </summary>
        public MissingArgumentException() : base(St.MissingArgument(St.Needed))
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingArgumentException" />.
        /// </summary>
        /// <param name="argumentName">Nombre del argumento faltante.</param>
        public MissingArgumentException(string argumentName) : base(St.MissingArgument(argumentName))
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingArgumentException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="argumentName">Nombre del argumento faltante.</param>
        public MissingArgumentException(string message, string argumentName) : base(message, argumentName)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.MissingArgumentException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public MissingArgumentException(Exception inner) : base(St.MissingArgument(St.Needed), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.MissingArgumentException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public MissingArgumentException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.MissingArgumentException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="argumentName">Nombre del argumento faltante.</param>
        public MissingArgumentException(string message, string argumentName, Exception inner) : base(message,
            argumentName, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se detecta que una función ha devuelto
    ///     un valor inválido sin generar una excepción.
    /// </summary>
    [Serializable]
    public class InvalidReturnValueException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" />.
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
        protected InvalidReturnValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" />.
        /// </summary>
        public InvalidReturnValueException() : base(St.XReturnedInvalid(St.TheFunc))
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" /> especificando el delegado
        ///     que causó esta excepción.
        /// </summary>
        /// <param name="call">
        ///     <see cref="T:System.Delegate" /> cuyo resultado causó la excepción.
        /// </param>
        public InvalidReturnValueException(Delegate call) : base(
            St.XReturnedInvalid(St.XYQuotes(St.TheFunc, call.Method.Name)))
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" /> especificando el delegado
        ///     que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        ///     Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        public InvalidReturnValueException(string methodName) : base(
            St.XReturnedInvalid(St.XYQuotes(St.TheFunc, methodName)))
        {
            OffendingFunctionName = methodName;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" /> especificando el delegado
        ///     que causó esta excepción.
        /// </summary>
        /// <param name="call">
        ///     <see cref="T:System.Delegate" /> cuyo resultado causó la excepción.
        /// </param>
        /// <param name="returnValue">
        ///     Valor inválido devuelto por <paramref name="call" />.
        /// </param>
        public InvalidReturnValueException(Delegate call, object returnValue) : base(
            St.XReturnedInvalid(St.XYQuotes(St.TheFunc, call.Method.Name)))
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = returnValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" /> especificando el delegado
        ///     que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        ///     Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        /// <param name="returnValue">
        ///     Valor inválido devuelto por el método.
        /// </param>
        public InvalidReturnValueException(string methodName, object returnValue) : base(
            St.XReturnedInvalid(St.XYQuotes(St.TheFunc, methodName)))
        {
            OffendingFunctionName = methodName;
            OffendingReturnValue = returnValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" /> especificando el delegado
        ///     que causó esta excepción.
        /// </summary>
        /// <param name="call">
        ///     <see cref="T:System.Delegate" /> cuyo resultado causó la excepción.
        /// </param>
        /// <param name="returnValue">
        ///     Valor inválido devuelto por <paramref name="call" />.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(Delegate call, object returnValue, Exception inner) : base(
            St.XReturnedInvalid(St.XYQuotes(St.TheFunc, call.Method.Name)), inner)
        {
            OffendingFunction = call;
            OffendingFunctionName = OffendingFunction.Method.Name;
            OffendingReturnValue = returnValue;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidReturnValueException" /> especificando el delegado
        ///     que causó esta excepción.
        /// </summary>
        /// <param name="methodName">
        ///     Nombre del método cuyo resultado ha causado la excepción.
        /// </param>
        /// <param name="returnValue">
        ///     Valor inválido devuelto por el método.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidReturnValueException(string methodName, object returnValue, Exception inner) : base(
            St.XReturnedInvalid(St.XYQuotes(St.TheFunc, methodName)), inner)
        {
            OffendingFunctionName = methodName;
            OffendingReturnValue = returnValue;
        }

        /// <summary>
        ///     Obtiene uan referencia al método que ha causado la excepción.
        /// </summary>
        /// <returns>
        ///     Un <see cref="Delegate" /> que representa un método cuyo valor
        ///     devuelto causó la excepción.
        /// </returns>
        public Delegate OffendingFunction { get; }

        /// <summary>
        ///     Obtiene el nombre del método que ha causado la excepción.
        /// </summary>
        /// <returns>
        ///     El nombre de la función cuyo resultado causó la excepción.
        /// </returns>
        public string OffendingFunctionName { get; }

        /// <summary>
        ///     Obtiene el valor devuelto que ha causado la excepción.
        /// </summary>
        /// <returns>
        ///     El valor inválido devuelto por método, causante de la excepción.
        /// </returns>
        public object OffendingReturnValue { get; }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando un <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> encuentra un error.
    /// </summary>
    [Serializable]
    public class PluginException : OffendingException<IPlugin>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
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
        protected PluginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
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
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> donde se ha generado la excepción.</param>
        protected PluginException(SerializationInfo info, StreamingContext context, IPlugin plugin) : base(info,
            context, plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        public PluginException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(IPlugin plugin) : base(Msg(plugin), plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public PluginException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(string message, IPlugin plugin) : base(message, plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(Exception inner, IPlugin plugin) : base(Msg(plugin), inner, plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> donde se ha generado la excepción.</param>
        public PluginException(string message, Exception inner, IPlugin plugin) : base(message, inner, plugin)
        {
        }

        private static string Msg()
        {
            return St.XFoundError(St.ThePlugin);
        }

        private static string Msg(IPlugin plugin)
        {
            return St.XFoundError(St.XYQuotes(St.ThePlugin, plugin.Name));
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se intenta utilizar un objeto marcado con el atributo
    ///     <see cref="T:TheXDS.MCART.Attributes.UnusableAttribute" />.
    /// </summary>
    [Serializable]
    public class UnusableObjectException : OffendingException<object>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
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
        protected UnusableObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
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
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        protected UnusableObjectException(SerializationInfo info, StreamingContext context, object unusableObject) :
            base(info, context, unusableObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        public UnusableObjectException() : base(St.UnusableObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(object unusableObject) : base(St.UnusableObject, unusableObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public UnusableObjectException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(string message, object unusableObject) : base(message, unusableObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public UnusableObjectException(Exception inner) : base(St.UnusableObject, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(Exception inner, object unusableObject) : base(St.UnusableObject, inner,
            unusableObject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public UnusableObjectException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnusableObjectException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="unusableObject">Objeto inutilizable que es la causa de esta excepción.</param>
        public UnusableObjectException(string message, Exception inner, object unusableObject) : base(message, inner,
            unusableObject)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando un plugin no pudo inicializarse.
    /// </summary>
    [Serializable]
    public class PluginInitializationException : OffendingException<IPlugin>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
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
        protected PluginInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
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
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> que no pudo inicializarse.</param>
        protected PluginInitializationException(SerializationInfo info, StreamingContext context, IPlugin plugin) :
            base(info, context, plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        public PluginInitializationException() : base(St.PluginDidntInit)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(IPlugin plugin) : base(St.PluginDidntInit, plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public PluginInitializationException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(string message, IPlugin plugin) : base(message, plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginInitializationException(Exception inner) : base(St.PluginDidntInit, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(Exception inner, IPlugin plugin) : base(St.PluginDidntInit, inner, plugin)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginInitializationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginInitializationException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="plugin"><see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> que no pudo inicializarse.</param>
        public PluginInitializationException(string message, Exception inner, IPlugin plugin) : base(message, inner,
            plugin)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando se intenta utilizar una clase marcada con el atributo
    ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
    /// </summary>
    [Serializable]
    public class DangerousTypeException : OffendingException<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
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
        protected DangerousTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
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
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        protected DangerousTypeException(SerializationInfo info, StreamingContext context, Type dangerousType) : base(
            info, context, dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        public DangerousTypeException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(Type dangerousType) : base(Msg(dangerousType), dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public DangerousTypeException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(string message, Type dangerousType) : base(message, dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousTypeException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(Exception inner, Type dangerousType) : base(Msg(dangerousType), inner,
            dangerousType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousTypeException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="dangerousType">Clase marcada con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.</param>
        public DangerousTypeException(string message, Exception inner, Type dangerousType) : base(message, inner,
            dangerousType)
        {
        }

        private static string Msg()
        {
            return St.ClassIsDangerous(St.Unk);
        }

        private static string Msg(Type dangerousType)
        {
            return St.ClassIsDangerous(dangerousType.Name);
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando un método ha sido marcado con el atributo
    ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
    /// </summary>
    [Serializable]
    public class DangerousMethodException : OffendingException<MethodInfo>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
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
        protected DangerousMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
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
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        protected DangerousMethodException(SerializationInfo info, StreamingContext context, MethodInfo offendingMethod)
            : base(info, context, offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        public DangerousMethodException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(MethodInfo offendingMethod) : base(Msg(offendingMethod), offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public DangerousMethodException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(string message, MethodInfo offendingMethod) : base(message, offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousMethodException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(Exception inner, MethodInfo offendingMethod) : base(Msg(offendingMethod), inner,
            offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public DangerousMethodException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.DangerousMethodException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">
        ///     Método marcado con el atributo
        ///     <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </param>
        public DangerousMethodException(string message, Exception inner, MethodInfo offendingMethod) : base(message,
            inner, offendingMethod)
        {
        }

        private static string Msg()
        {
            return St.MethodIsDangerous(St.Unk);
        }

        private static string Msg(MethodInfo offendingMethod)
        {
            return St.MethodIsDangerous(offendingMethod.Name);
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando un <see cref="T:System.Reflection.Assembly" /> no contiene la clase especificada.
    /// </summary>
    [Serializable]
    public class PluginClassNotFoundException : OffendingException<Type>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
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
        protected PluginClassNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
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
        /// <param name="requiredType">Tipo que fue requerido.</param>
        protected PluginClassNotFoundException(SerializationInfo info, StreamingContext context, Type requiredType) :
            base(info, context, requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        public PluginClassNotFoundException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(Type requiredType) : base(Msg(requiredType), requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public PluginClassNotFoundException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(string message, Type requiredType) : base(message, requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginClassNotFoundException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(Exception inner, Type requiredType) : base(Msg(requiredType), inner,
            requiredType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public PluginClassNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException" />.
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="requiredType">Tipo que fue requerido.</param>
        public PluginClassNotFoundException(string message, Exception inner, Type requiredType) : base(message, inner,
            requiredType)
        {
        }

        private static string Msg()
        {
            return St.XDoesntContainY(St.ThePlugin, St.TheClass.ToLower());
        }

        private static string Msg(Type type)
        {
            return St.XDoesntContainY(St.ThePlugin, St.XYQuotes(St.TheClass.ToLower(), type.Name));
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce al hacer referencia a un tipo desconodico
    /// </summary>
    [Serializable]
    public class UnknownTypeException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.UnknownTypeException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected UnknownTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnknownTypeException" />.
        /// </summary>
        public UnknownTypeException() : base(St.UnknownType)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnknownTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        public UnknownTypeException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnknownTypeException" />.
        /// </summary>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public UnknownTypeException(Exception inner) : base(St.UnknownType, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.UnknownTypeException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public UnknownTypeException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce cuando la firma de un método representado en un
    ///     <see cref="T:System.Reflection.MethodInfo" /> no es válida.
    /// </summary>
    [Serializable]
    public class InvalidMethodSignatureException : OffendingException<MethodInfo>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />.
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
        protected InvalidMethodSignatureException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />.
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
        /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
        protected InvalidMethodSignatureException(SerializationInfo info, StreamingContext context,
            MethodInfo offendingMethod) : base(info, context, offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />.
        /// </summary>
        public InvalidMethodSignatureException() : base(Msg())
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />.
        /// </summary>
        /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(MethodInfo offendingMethod) : base(Msg(offendingMethod), offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />
        ///     .
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        public InvalidMethodSignatureException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />
        ///     .
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(string message, MethodInfo offendingMethod) : base(message,
            offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />
        ///     .
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidMethodSignatureException(Exception inner) : base(Msg(), inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />
        ///     .
        /// </summary>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(Exception inner, MethodInfo offendingMethod) : base(Msg(offendingMethod),
            inner, offendingMethod)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />
        ///     .
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        public InvalidMethodSignatureException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.InvalidMethodSignatureException" />
        ///     .
        /// </summary>
        /// <param name="message">
        ///     Un <see cref="T:System.String" /> que describe a la excepción.
        /// </param>
        /// <param name="inner">
        ///     <see cref="T:System.Exception" /> que es la causa de esta excepción.
        /// </param>
        /// <param name="offendingMethod">Referencia al método que ha causado la excepción.</param>
        public InvalidMethodSignatureException(string message, Exception inner, MethodInfo offendingMethod) : base(
            message, inner, offendingMethod)
        {
        }

        private static string Msg()
        {
            return St.InvalidSignature(St.TheMethod);
        }

        private static string Msg(MethodInfo offendingMethod)
        {
            return St.InvalidSignature(St.XYQuotes(St.TheMethod,
                $"{offendingMethod?.DeclaringType?.FullName}.{offendingMethod?.Name}"));
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Excepción que se produce al intentar remover un objeto de una pila vacía
    /// </summary>
    [Serializable]
    public class StackUnderflowException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Exceptions.StackUnderflowException" />.
        /// </summary>
        /// <param name="info">
        ///     El objeto que contiene la información de serialización.
        /// </param>
        /// <param name="context">
        ///     La información contextual acerca del orígen o el destino.
        /// </param>
        protected StackUnderflowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.StackUnderflowException" />.
        /// </summary>
        public StackUnderflowException() : base(St.StackUnderflow)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.StackUnderflowException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        public StackUnderflowException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.StackUnderflowException" />.
        /// </summary>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public StackUnderflowException(Exception inner) : base(St.StackUnderflow, inner)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Exceptions.StackUnderflowException" />.
        /// </summary>
        /// <param name="message">Un <see cref="T:System.String" /> que describe a la excepción.</param>
        /// <param name="inner"><see cref="T:System.Exception" /> que es la causa de esta excepción.</param>
        public StackUnderflowException(string message, Exception inner) : base(message, inner)
        {
        }
    }

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