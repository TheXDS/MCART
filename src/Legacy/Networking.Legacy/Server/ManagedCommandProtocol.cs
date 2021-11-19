/*
ManagedCommandProtocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Describe un protocolo de comandos administrado por MCART.
    /// </summary>
    /// <typeparam name="TCommand">
    /// <see cref="Enum"/> con los comandos aceptados.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// <see cref="Enum"/> con las respuestas generadas.
    /// </typeparam>
    public abstract class ManagedCommandProtocol<TCommand, TResult> : ManagedCommandProtocol<Client, TCommand, TResult> where TCommand : struct, Enum where TResult : struct, Enum
    {
    }
}