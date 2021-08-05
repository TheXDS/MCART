/*
ICloneable.cs

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
using System.Linq;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Expansión de la interfaz <see cref="ICloneable"/> que provee de un
    /// método fuertemente tipeado además de una implementación predeterminada
    /// del mismo.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objeto resultante de la clonación. Normalmente, pero no
    /// necesariamente, es el mismo tipo que implementa esta interfaz.
    /// </typeparam>
    public interface ICloneable<T> : ICloneable where T : notnull, new()
    {
        /// <summary>
        /// Crea una copia de esta instancia.
        /// </summary>
        /// <returns>
        /// Una copia de esta instancia de tipo <typeparamref name="T"/>.
        /// </returns>
        new T Clone()
        {
            if (this is not T source) throw new InvalidCastException();
            var copy = new T();
            
            foreach (var j in typeof(T).GetFields().Where(p => p.IsPublic))
            {
                j.SetValue(copy, j.GetValue(source));
            }
            foreach (var j in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                j.SetValue(copy, j.GetValue(source));
            }
            return copy;
        }

        object ICloneable.Clone() => Clone();
    }
}