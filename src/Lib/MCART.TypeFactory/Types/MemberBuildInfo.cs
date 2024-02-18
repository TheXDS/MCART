/*
MemberBuildInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.Reflection;
using System.Reflection.Emit;

namespace TheXDS.MCART.Types;

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
