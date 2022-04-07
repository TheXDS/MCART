/*
ICasteable_T.cs

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
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Define una serie de miembros a implementar por un objeto que
/// permita realizar conversiones desde su tipo hacia otro.
/// </summary>
/// <typeparam name="T">
/// Tipo de destino para la conversión.
/// </typeparam>
public interface ICasteable<T>
{
    /// <summary>
    /// Convierte la instancia actual a un objeto de tipo
    /// <typeparamref name="T"/>.
    /// </summary>
    /// <returns>
    /// Un objeto de tipo <typeparamref name="T"/>.
    /// </returns>
    T Cast();

    /// <summary>
    /// Intenta realizar una operación de conversión de la instancia
    /// actual a un objeto de tipo <typeparamref name="T"/>.
    /// </summary>
    /// <param name="result">
    /// Resultado de la conversión.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la conversión ha sido exitosa,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    bool TryCast([MaybeNullWhen(false)] out T result)
    {
        try
        {
            result = Cast();
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}
