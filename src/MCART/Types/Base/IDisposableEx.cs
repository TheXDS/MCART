/*
IDisposableEx.cs

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

namespace TheXDS.MCART.Types.Base;
using System;

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
