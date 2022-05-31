/*
ValueChangingEventArgs_T.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Events;
using System.ComponentModel;

/// <summary>
/// Incluye información para cualquier evento que incluya tipos de valor y
/// puedan ser cancelados.
/// </summary>
/// <typeparam name="T">
/// Tipo del valor almacenado por esta instancia.
/// </typeparam>
public class ValueChangingEventArgs<T> : CancelEventArgs
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ValueChangingEventArgs{T}" /> con el valor provisto.
    /// </summary>
    /// <param name="oldValue">
    /// Valor original asociado al evento generado.
    /// </param>
    /// <param name="newValue">
    /// Nuevo valor asociado al evento generado.
    /// </param>
    public ValueChangingEventArgs(T oldValue, T newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// Convierte explícitamente este
    /// <see cref="ValueChangingEventArgs{T}" /> en un
    /// <see cref="ValueEventArgs{T}" />.
    /// </summary>
    /// <param name="fromValue">Objeto a convertir.</param>
    /// <returns>
    /// Un <see cref="ValueEventArgs{T}" /> con la información pertinente
    /// del <see cref="ValueChangingEventArgs{T}" /> especificado.
    /// </returns>
    public static explicit operator ValueEventArgs<T>(ValueChangingEventArgs<T> fromValue)
    {
        return new(fromValue.NewValue);
    }

    /// <summary>
    /// Devuelve el valor original asociado a este evento.
    /// </summary>
    /// <returns>
    /// Un valor de tipo <typeparamref name="T" /> con el valor asociado al
    /// evento.
    /// </returns>
    public T OldValue { get; }

    /// <summary>
    /// Devuelve el nuevo valor asociado a este evento.
    /// </summary>
    /// <returns>
    /// Un valor de tipo <typeparamref name="T" /> con el valor asociado al
    /// evento.
    /// </returns>
    public T NewValue { get; }
}
