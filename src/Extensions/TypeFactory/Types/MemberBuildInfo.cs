/*
MemberBuildInfo.cs

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

using System.Reflection;
using System.Reflection.Emit;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Clase base que contiene información compartida de construcción de un
    /// miembro de clase.
    /// </summary>
    public abstract class MemberBuildInfo<T> where T : MemberInfo
    {
        /// <summary>
        /// Referencia al <see cref="System.Reflection.Emit.TypeBuilder"/> en
        /// el cual se ha construido la propiedad.
        /// </summary>
        public TypeBuilder TypeBuilder { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="MemberBuildInfo{T}"/>.
        /// </summary>
        /// <param name="typeBuilder">
        /// <see cref="System.Reflection.Emit.TypeBuilder"/> en donde se ha
        /// definido este miembro.
        /// </param>
        /// <param name="member">
        /// Referencia al miembro que ha sido definido.
        /// </param>
        protected MemberBuildInfo(TypeBuilder typeBuilder, T member)
        {
            TypeBuilder = typeBuilder;
            Member = member;
        }

        /// <summary>
        /// Referencia al <see cref="PropertyBuilder"/> utilizado para
        /// construir a la propiedad.
        /// </summary>
        public T Member { get; }

        /// <summary>
        /// Convierte implícitamente un valor <typeparamref name="T"/>
        /// en un <see cref="MemberBuildInfo{T}"/>.
        /// </summary>
        /// <param name="buildInfo">
        /// <typeparamref name="T"/> desde el cual extraer el
        /// <see cref="MemberBuildInfo{T}"/>.
        /// </param>
        public static implicit operator T(MemberBuildInfo<T> buildInfo) => buildInfo.Member;
    }
}
