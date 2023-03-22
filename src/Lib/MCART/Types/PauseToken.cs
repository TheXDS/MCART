/*
PauseToken.cs

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

using System.Threading.Tasks;

namespace TheXDS.MCART.Types;

/// <summary>
/// Estructura que representa un token utilizado para pausar temporalmente
/// la ejecución de tareas.
/// </summary>
public readonly struct PauseToken
{
    private readonly PauseTokenSource? m_source;

    internal PauseToken(PauseTokenSource source)
    {
        m_source = source;
    }

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="PauseToken"/> se
    /// encuentra en estado de pausa.
    /// </summary>
    public bool IsPaused => m_source?.IsPaused ?? false;

    /// <summary>
    /// Inicia el estado de pausa, esperando a que el
    /// <see cref="PauseTokenSource"/> de origen de esta instancia salga
    /// del estado de pausa.
    /// </summary>
    /// <returns>
    /// Un objeto <see cref="Task"/> que finalizará cuando este token no se
    /// encuentre en pausa.
    /// </returns>
    public Task WaitWhilePausedAsync()
    {
        return IsPaused ?
            m_source!.WaitWhilePausedAsync() :
            Task.CompletedTask;
    }
}
