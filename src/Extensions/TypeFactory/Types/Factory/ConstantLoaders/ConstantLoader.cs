/*
ConstantLoader.cs

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
using System.Reflection.Emit;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Clase abstracta que define un objeto que permite cargar un valor 
    /// constante enla secuencia de instrucciones MSIL.
    /// </summary>
    /// <typeparam name="T">Tipo de constante a cargar.</typeparam>
    public abstract class ConstantLoader<T> : IConstantLoader, IEquatable<IConstantLoader>
    {
        /// <summary>
        /// Obtiene una referencia al tipo de constante que esta instancia
        /// puede cargar en la secuencia de instruciones MSIL.
        /// </summary>
        public Type ConstantType => typeof(T);

        /// <summary>
        /// Carga un valor constante en la secuencia de instrucciones MSIL.
        /// </summary>
        /// <param name="il">
        /// Generador de código a utilizar.
        /// </param>
        /// <param name="value">
        /// Valor constante a cargar. Debe ser de tipo 
        /// <typeparamref name="T"/>.
        /// </param>
        public void Emit(ILGenerator il, object? value)
        {
            Emit(il, (T)value!);
        }

        /// <summary>
        /// Carga un valor constante en la secuencia de instrucciones MSIL.
        /// </summary>
        /// <param name="il">
        /// Generador de código a utilizar.
        /// </param>
        /// <param name="value">
        /// Valor constante a cargar.
        /// </param>
        public abstract void Emit(ILGenerator il, T value);

        /// <summary>
        /// Comprueba la igualdad entre esta instancia y otro 
        /// <see cref="IConstantLoader"/> basado en el tipo de constante
        /// que ambos son capaces de cargar.
        /// </summary>
        /// <param name="other">
        /// Una instancia de <see cref="IConstantLoader"/> contra la cual
        /// comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si ambos <see cref="IConstantLoader"/>
        /// permiten cargar el mismo tipo de valores,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Equals(IConstantLoader? other)
        {
            return other is {ConstantType: Type o} && ConstantType.Equals(o);
        }
    }
}