/*
AutoDictionary.cs

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

using System.Collections.Generic;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Diccionario con soporte para instanciación automática de claves no
    /// existentes.
    /// </summary>
    /// <typeparam name="TKey">Tipo de llave a utilizar.</typeparam>
    /// <typeparam name="TValue">
    /// Tipo del valor contenido en este diccionario.
    /// </typeparam>
    public class AutoDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull where TValue : new()
    {
        /// <summary>
        /// Obtiene o establece el valor asociado con la llave
        /// especificada, instanciando un nuevo valor si la misma no
        /// existe.
        /// </summary>
        /// <param name="key">
        /// Llave del valor a obtener o establecer.
        /// Se creará un nuevo valor si la llave no existe.
        /// </param>
        /// <returns>
        /// Valor asociado a la clave especificada. Si no se encuentra la
        /// clave especificada, se creará un nuevo elemento con dicha
        /// clave.
        /// </returns>
        public new TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key)) Add(key, new TValue());
                return base[key];
            }
            set => base[key] = value;
        }
    }
}
