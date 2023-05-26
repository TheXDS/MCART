// MvvmCollectionExtensions.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2023 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los elementos de tipo <see cref="ICollection{T}" />
/// que incluyen funcionalidad asociada a la arquitectura MVVM.
/// </summary>
public static partial class MvvmCollectionExtensions
{
    /// <summary>
    /// Obtiene un <see cref="ObservableCollectionWrap{T}"/> que envuelve a la
    /// colección especificada.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección.</typeparam>
    /// <param name="collection">
    /// Colección a envolver dentro del
    /// <see cref="ObservableCollectionWrap{T}"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="ObservableCollectionWrap{T}"/> que envuelve a la colección
    /// para brindar notificaciones de cambio por medio de la interfaz
    /// <see cref="System.Collections.Specialized.INotifyCollectionChanged"/>.
    /// </returns>
    public static ObservableCollectionWrap<T> ToObservable<T>(this ICollection<T> collection)
    {
        ToObservable_Contract(collection);
        return new(collection);
    }
}
