/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene definiciones misceláneas y compartidas en el espacio de
nombres TheXDS.MCART.Networking.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using TheXDS.MCART.Attributes;

// ReSharper disable ClassNeverInstantiated.Global

namespace TheXDS.MCART.Networking
{
    /// <summary>
    ///     Contiene definiciones y objetos predeterminados a utilizar en el
    ///     espacio de nombres <see cref="Networking"/>.
    /// </summary>
    public static class Common
    {
        /// <summary>
        ///     Puerto predeterminado para todos los objetos de red.
        /// </summary>
        public const int DefaultPort = 51220;

        /// <summary>
        ///     Tiempo de espera en milisegundos antes de realizar una
        ///     desconexión forzada.
        /// </summary>
        public const int DisconnectionTimeout = 15000;
 }

    /// <inheritdoc />
    /// <summary>
    ///     Atributo que establece un número de puerto que un
    ///     <see cref="T:TheXDS.MCART.Networking.Server.Server`1" /> debería utilizar al escuchar
    ///     conexiones entrantes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PortAttribute : IntAttribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="T:TheXDS.MCART.Networking.PortAttribute" />.
        /// </summary>
        /// <param name="portNumber">Número de puerto a utilizar.</param>
        public PortAttribute(int portNumber) : base(portNumber)
        {
            if (!portNumber.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(portNumber));
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Atributo que se establece en el miembro de una enumeración a ser
    ///     utilizado como la respuesta en caso de error que enviará un
    ///     protocolo derivado de la clase
    ///     <see cref="T:TheXDS.MCART.Networking.Server.SelfWiredCommandProtocol`3" />
    /// </summary>
    /// <remarks>
    ///     Si ningún miembro de la enumeración se marca con este atributo, en
    ///     caso de ocurrir un error se lanzará una excepción que el servidor
    ///     deberá manejar.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ErrorResponseAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Atributo que se establece en el miembro de una enumeración a ser
    ///     utilizado como la respuesta en caso de encontrar un comando
    ///     desconocido que enviará un protocolo derivado de la clase
    ///     <see cref="T:TheXDS.MCART.Networking.Server.SelfWiredCommandProtocol`3" />
    /// </summary>
    /// <remarks>
    ///     Si ningún miembro de la enumeración se marca con este atributo, en
    ///     caso de no existir el comando de la solicitud, se devolverá el
    ///     valor que sea marcado con el atributo
    ///     <see cref="T:TheXDS.MCART.Networking.ErrorResponseAttribute" />, o en su defecto se lanzará
    ///     una excepción que el servidor deberá manejar.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class UnknownResponseAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Atributo que se establece en el miembro de una enumeración a ser
    ///     utilizado como la respuesta en caso de encontrar un comando
    ///     que no ha sido mapeado a una función de un protocolo derivado de la
    ///     clase
    ///     <see cref="T:TheXDS.MCART.Networking.Server.SelfWiredCommandProtocol`3" />
    /// </summary>
    /// <remarks>
    ///     Si ningún miembro de la enumeración se marca con este atributo, en
    ///     caso de no existir una función que esté mapeada al comando de la
    ///     solicitud, se devolverá el valor que sea marcado con el atributo
    ///     <see cref="T:TheXDS.MCART.Networking.ErrorResponseAttribute" />, o en su defecto se lanzará
    ///     una excepción que el servidor deberá manejar.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class NotMappedResponseAttribute : Attribute
    {
    }
}