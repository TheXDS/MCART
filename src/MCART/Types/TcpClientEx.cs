/*
TcpClientEx.cs

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

namespace TheXDS.MCART.Types;
using System.Net.Sockets;
using TheXDS.MCART.Types.Base;

/// <summary>
/// Extensión de la clase <see cref="TcpClient" />
/// que implementa observación del estado de deshecho del objeto.
/// </summary>
public class TcpClientEx : TcpClient, IDisposableEx
{
    /// <summary>
    /// Obtiene un valor que indica si la instancia actual ha sido
    /// desechada.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Libera los recursos no administrados que usa
    /// <see cref="TcpClient" /> y libera los
    /// recursos administrados de forma opcional.
    /// </summary>
    /// <param name="disposing">
    /// Se establece en <see langword="true" /> para liberar tanto los
    /// recursos administrados como los no administrados; se establece
    /// en <see langword="false" /> para liberar únicamente los
    /// recursos no administrados.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (IsDisposed) return;
        IsDisposed = true;
        base.Dispose(disposing);
    }
}
