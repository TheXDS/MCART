/*
TcpClientEx.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Net.Sockets;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types;

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
