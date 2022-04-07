/*
MissingTypeException.cs

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
using System.Runtime.Serialization;

/// <summary>
/// Excepción que se produce cuando un tipo requerido no ha sido encontrado.
/// </summary>
[Serializable]
public class MissingTypeException : OffendingException<Type>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="MissingTypeException" />.
    /// </summary>
    public MissingTypeException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="offendingObject">
    /// Tipo que ha producido la excepción.
    /// </param>
    public MissingTypeException(Type offendingObject) : base(offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    public MissingTypeException(string message) : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
    public MissingTypeException(Exception inner) : base(inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="offendingObject">
    /// Tipo que ha producido la excepción.
    /// </param>
    public MissingTypeException(string message, Type offendingObject) : base(message, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
    /// <param name="offendingObject">
    /// Tipo que ha producido la excepción.
    /// </param>
    public MissingTypeException(Exception inner, Type offendingObject) : base(inner, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
    /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
    public MissingTypeException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="message">Un <see cref="string" /> que describe a la excepción.</param>
    /// <param name="inner"><see cref="Exception" /> que es la causa de esta excepción.</param>
    /// <param name="offendingObject">
    /// Tipo que ha producido la excepción.
    /// </param>
    public MissingTypeException(string message, Exception inner, Type offendingObject) : base(message, inner, offendingObject)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="info">
    /// El objeto que contiene la información de serialización.
    /// </param>
    /// <param name="context">
    /// La información contextual acerca del orígen o el destino.
    /// </param>
    protected MissingTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="MissingTypeException" />.
    /// </summary>
    /// <param name="info">
    /// El objeto que contiene la información de serialización.
    /// </param>
    /// <param name="context">
    /// La información contextual acerca del orígen o el destino.
    /// </param>
    /// <param name="offendingObject">
    /// Tipo que ha producido la excepción.
    /// </param>
    protected MissingTypeException(SerializationInfo info, StreamingContext context, Type offendingObject) : base(info, context, offendingObject)
    {
    }
}
