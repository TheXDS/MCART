/*
ConstantLoaderComparer.cs

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

using TheXDS.MCART.Types.Extensions.ConstantLoaders;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Permite comparar la igualdad entre dos 
/// <see cref="IConstantLoader"/> basado en el tipo de constante que
/// ambos son capaces de cargar.
/// </summary>
public class ConstantLoaderComparer : IEqualityComparer<IConstantLoader>
{
    /// <summary>
    /// Compara dos instancias de <see cref="IConstantLoader"/>.
    /// </summary>
    /// <param name="x">
    /// Primer objeto a comparar.
    /// </param>
    /// <param name="y">
    /// Segundo objeto a comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si ambos objetos cargan constantes del
    /// mismo tipo, <see langword="false"/> en caso contrario.
    /// </returns>
    public bool Equals(IConstantLoader? x, IConstantLoader? y)
    {
        return x?.ConstantType.Equals(y?.ConstantType) ?? y is null;
    }

    /// <summary>
    /// Obtiene el código hash de una instancia de 
    /// <see cref="IConstantLoader"/> que puede ser utilizado para
    /// comparar el tipo de constante que el objeto es capaz de cargar.
    /// </summary>
    /// <param name="obj">
    /// Objeto desde el cual obtener el código hash.
    /// </param>
    /// <returns>
    /// El código hash del tipo de constante que el objeto es capaz de
    /// cargar.
    /// </returns>
    public int GetHashCode(IConstantLoader obj)
    {
        return obj.ConstantType.GetHashCode();
    }
}
