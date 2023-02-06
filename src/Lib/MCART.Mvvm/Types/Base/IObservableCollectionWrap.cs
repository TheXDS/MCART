/*
IObservableCollectionWrap.cs

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

using System.Collections.Specialized;
using System.Collections.Generic;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Define una serie de miembros a implementar por una clase que defina
/// un envoltorio observable sobre un <see cref="ICollection{T}"/>.
/// </summary>
public interface IObservableCollectionWrap<T> : INotifyCollectionChanged, ICollection<T>, IRefreshable
{
    /// <summary>
    /// Obtiene una referencia a la colección subyacente de este
    /// envoltorio observable.
    /// </summary>
    ICollection<T> UnderlyingCollection { get; }

    /// <summary>
    /// Sustituye la colección subyacente por una nueva.
    /// </summary>
    /// <param name="newCollection">
    /// Colección a establecer como la colección subyacente.
    /// </param>
    void Substitute(ICollection<T> newCollection);
}
