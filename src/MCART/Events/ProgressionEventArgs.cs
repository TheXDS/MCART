/*
ProgressionEventArgs.cs

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
using System;
using TheXDS.MCART.Resources;

/// <summary>
/// Incluye información de evento para cualquier clase con eventos que
/// reporten el progreso de una operación.
/// </summary>
public class ProgressionEventArgs : ValueEventArgs<double>
{
    /// <summary>
    /// Inicializa una nueva instancia de este objeto con los datos
    /// provistos.
    /// </summary>
    /// <param name="progress">
    /// Valor de progreso. Debe ser un <see cref="double" /> entre
    /// <c>0.0</c> y <c>1.0</c>, o los valores <see cref="double.NaN" />,
    /// <see cref="double.PositiveInfinity" /> o
    /// <see cref="double.NegativeInfinity" />.
    /// </param>
    /// <param name="helpText">
    /// Parámetro opcional. Descripción del estado de progreso que generó el
    /// evento.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="progress" /> no en un valor entre <c>0.0</c>
    /// y <c>1.0</c>.
    /// </exception>
    public ProgressionEventArgs(double progress, string? helpText) : base(progress)
    {
        if (progress > 1 || progress < 0) throw Errors.ValueOutOfRange(nameof(progress), 0, 1);
        HelpText = helpText;
    }

    /// <summary>
    /// Inicializa una nueva instancia de este objeto con los datos
    /// provistos.
    /// </summary>
    /// <param name="progress">
    /// Valor de progreso. Debe ser un <see cref="double" /> entre
    /// <c>0.0</c> y <c>1.0</c>, o los valores <see cref="double.NaN" />,
    /// <see cref="double.PositiveInfinity" /> o
    /// <see cref="double.NegativeInfinity" />.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="progress" /> no en un valor entre <c>0.0</c>
    /// y <c>1.0</c>.
    /// </exception>
    public ProgressionEventArgs(double progress) : this(progress, null)
    {
    }

    /// <summary>
    /// Devuelve una descripción rápida del estado de progreso.
    /// </summary>
    /// <returns>
    /// Un <see cref="string" /> con un mensaje que describe el estado de
    /// progreso del evento.
    /// </returns>
    public string? HelpText { get; }
}
