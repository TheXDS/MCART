﻿/*
IDisposableEx.cs

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

using System;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Extensión de la interfaz <see cref="IDisposable"/>. Provee de toda
/// la funcionalidad previamente disponible, e incluye algunas
/// extensiones útiles.
/// </summary>
public interface IDisposableEx : IDisposable
{
    /// <summary>
    /// Obtiene un valor que indica si este objeto ha sido desechado.
    /// </summary>
    bool IsDisposed { get; }

    /// <summary>
    /// Intenta liberar los recursos de esta instancia.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si la instancia se ha desechado
    /// correctamente, <see langword="false"/> si esta instancia ya ha sido
    /// desechada o si ha ocurrido un error al desecharla.
    /// </returns>
    bool TryDispose()
    {
        if (!IsDisposed)
        {
            try
            {
                Dispose();
                return true;
            }
            catch { }
        }
        return false;
    }
}