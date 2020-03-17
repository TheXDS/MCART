/*
Common.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene definiciones misceláneas y compartidas en el espacio de
nombres TheXDS.MCART.Networking.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using TheXDS.MCART.Networking.Server;

namespace TheXDS.MCART.Networking
{
    /// <inheritdoc />
    /// <summary>
    /// Atributo que se establece en el miembro de una enumeración a ser
    /// utilizado como la respuesta en caso de encontrar un comando
    /// desconocido que enviará un protocolo derivado de la clase
    /// <see cref="SelfWiredCommandProtocol{TClient, TCommand, TResponse}" />
    /// </summary>
    /// <remarks>
    /// Si ningún miembro de la enumeración se marca con este atributo, en
    /// caso de no existir el comando de la solicitud, se devolverá el
    /// valor que sea marcado con el atributo
    /// <see cref="ErrorResponseAttribute" />, o en su defecto se lanzará
    /// una excepción que el servidor deberá manejar.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class UnknownResponseAttribute : Attribute
    {
    }
}