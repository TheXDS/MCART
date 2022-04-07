/*
ClassNotInstantiableException.cs

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
using TheXDS.MCART.Resources.Strings;

/// <summary>
/// Excepción que se produce cuando no es posible instanciar un tipo.
/// </summary>
[Serializable]
public class ClassNotInstantiableException : OffendingException<Type?>
{
    private static string DefaultMessage(Type? offendingType = null)
    {
        if (offendingType is null) return Errors.ClassNotInstantiable;
        return string.Format(Errors.ClassXNotinstantiable, offendingType.Name);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    public ClassNotInstantiableException() : base(DefaultMessage()) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(Type? offendingType) : base(DefaultMessage(offendingType), offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    public ClassNotInstantiableException(string message) : base(message) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(string message, Type? offendingType) : base(message, offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    public ClassNotInstantiableException(Exception inner) : base(DefaultMessage(), inner) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(Exception inner, Type? offendingType) : base(DefaultMessage(offendingType), inner, offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    public ClassNotInstantiableException(string message, Exception inner) : base(message, inner) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" />.
    /// </summary>
    /// <param name="message">
    /// Un <see cref="string" /> que describe a la excepción.
    /// </param>
    /// <param name="inner">
    /// <see cref="Exception" /> secundaria producida por esta excepción.
    /// </param>
    /// <param name="offendingType">
    /// Tipo que es la causa de esta excepción.
    /// </param>
    public ClassNotInstantiableException(string message, Exception inner, Type? offendingType) : base(message, inner, offendingType) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ClassNotInstantiableException" /> con datos serializados.
    /// </summary>
    /// <param name="context">
    /// El <see cref="StreamingContext" /> que contiene información
    /// contextual acerca del orígen o el destino.
    /// </param>
    /// <param name="info">
    /// El <see cref="SerializationInfo" /> que contiene la información
    /// serializada del objeto acerca de la excepción que está siendo
    /// lanzada.
    /// </param>
    protected ClassNotInstantiableException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
