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

namespace TheXDS.MCART.Exceptions;
using System;
using System.Collections;
using System.Runtime.Serialization;

/// <summary>
/// Se produce cuando un elemento de una colección es
/// <see langword="null"/> de manera inesperada.
/// </summary>
[Serializable]
public class NullItemException : OffendingException<IList>
{
    /// <summary>
    /// Obtiene el índice del elemento que ha producido esta excepción.
    /// </summary>
    /// <value>
    /// El índice del elemento que es <see langword="null"/>.
    /// </value>
    public int NullIndex { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    public NullItemException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(IList offendingObject) : base(offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Mensaje descriptivo de la excepción.</param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(string message, IList offendingObject) : base(message, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    public NullItemException(Exception inner) : base(inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(Exception inner, IList offendingObject) : base(inner, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Mensaje descriptivo de la excepción.</param>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    public NullItemException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="message">Mensaje descriptivo de la excepción.</param>
    /// <param name="inner">
    /// Excepción que es la causa de esta excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    public NullItemException(string message, Exception inner, IList offendingObject) : base(message, inner, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="info">
    /// Objeto que contiene la información de serialización de esta
    /// instancia.
    /// </param>
    /// <param name="context">
    /// Información de contexto de serialización.
    /// </param>
    protected NullItemException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NullItemException"/>.
    /// </summary>
    /// <param name="info">
    /// Objeto que contiene la información de serialización de esta
    /// instancia.
    /// </param>
    /// <param name="context">
    /// Información de contexto de serialización.
    /// </param>
    /// <param name="offendingObject">
    /// Lista en la cual se ha encontrado un elemento nulo.
    /// </param>
    protected NullItemException(SerializationInfo info, StreamingContext context, IList offendingObject) : base(info, context, offendingObject)
    {
    }
}
