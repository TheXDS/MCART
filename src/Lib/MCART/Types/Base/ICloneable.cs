﻿/*
ICloneable.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Expansión de la interfaz <see cref="ICloneable"/> que provee de un
/// método fuertemente tipeado además de una implementación predeterminada
/// del mismo.
/// </summary>
/// <typeparam name="T">
/// Tipo de objeto resultante de la clonación. Normalmente, pero no
/// necesariamente, es el mismo tipo que implementa esta interfaz.
/// Si el tipo no es el mismo que implementa la interfaz, el tipo debe
/// implementar el tipo especificado.
/// </typeparam>
public interface ICloneable<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)]out T> : ICloneable where T : notnull, new()
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
        return Helpers.Objects.ShallowClone(source);
    }

    object ICloneable.Clone() => Clone();
}
