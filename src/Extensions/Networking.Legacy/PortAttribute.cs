/*
PortAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene definiciones misceláneas y compartidas en el espacio de
nombres TheXDS.MCART.Networking.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using TheXDS.MCART.Networking.Legacy.Server;

namespace TheXDS.MCART.Networking.Legacy
{
    /// <summary>
    /// Atributo que establece un número de puerto que un
    /// <see cref="Server{TClient}" /> debería utilizar al escuchar
    /// conexiones entrantes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PortAttribute : IntAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PortAttribute" />.
        /// </summary>
        /// <param name="portNumber">Número de puerto a utilizar.</param>
        public PortAttribute(int portNumber) : base(portNumber)
        {
            if (!portNumber.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(portNumber));
        }
    }
}