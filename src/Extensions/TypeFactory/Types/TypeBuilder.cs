/*
TypeBuilder.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// <see cref="TypeBuilder"/> que incluye información fuertemente tipeada
    /// sobre su clase base.
    /// </summary>
    /// <typeparam name="T">
    /// Clase base del tipo a construir.
    /// </typeparam>
    public class TypeBuilder<T> : ITypeBuilder<T>
    {
        /// <summary>
        /// <see cref="TypeBuilder"/> subyacente de esta instancia.
        /// </summary>
        public TypeBuilder Builder { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        ///  <see cref="TypeBuilder{T}"/> especificando al
        ///  <see cref="TypeBuilder"/> subyacente a asociar.
        /// </summary>
        /// <param name="builder">
        /// <see cref="TypeBuilder"/> subyacente a asociar.
        /// </param>
        public TypeBuilder(TypeBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="TypeBuilder{T}"/> en un
        /// <see cref="TypeBuilder"/>.
        /// </summary>
        /// <param name="builder">
        /// <see cref="TypeBuilder"/> a convertir.
        /// </param>
        public static implicit operator TypeBuilder(TypeBuilder<T> builder) => builder.Builder;

        /// <summary>
        /// Convierte implícitamente un <see cref="TypeBuilder"/> en un
        /// <see cref="TypeBuilder{T}"/>.
        /// </summary>
        /// <param name="builder">
        /// <see cref="TypeBuilder"/> a convertir.
        /// </param>
        public static implicit operator TypeBuilder<T>(TypeBuilder builder) => new TypeBuilder<T>(builder);

        /// <summary>
        /// Inicializa una nueva instancia del tipo en runtime especificado.
        /// </summary>
        /// <returns>La nueva instancia del tipo especificado.</returns>
        [DebuggerStepThrough]
        [Sugar]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object New() => Builder.New();
    }
}
