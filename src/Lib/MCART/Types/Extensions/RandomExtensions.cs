/*
RandomExtensions.cs

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

using System;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para la clase <see cref="Random" />
/// </summary>
public static class RandomExtensions
{
    private const string Text = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// Necesario para evitar que las funciones que requieren de números
    /// aleatorios generen objetos <see cref="Random" /> con el mismo
    /// número de semilla basada en tiempo.
    /// </summary>
    public static Random Rnd { get; } = new();

    /// <summary>
    /// Obtiene una cadena de texto aleatorio.
    /// </summary>
    /// <param name="r">
    /// Instancia del objeto <see cref="Random" /> a utilizar.
    /// </param>
    /// <param name="length">Longitud de la cadena a generar.</param>
    /// <returns>
    /// Una cadena de texto aleatorio con la longitud especificada.
    /// </returns>
    public static string RndText(this Random r, in int length)
    {
        string? x = string.Empty;
        while (x.Length < length) x += Text[r.Next(0, Text.Length)];
        return x;
    }

    /// <summary>
    /// Devuelve un número entero aleatorio que se encuentra dentro del 
    /// rango especificado.
    /// </summary>
    /// <param name="r">
    /// Instancia del objeto <see cref="Random" /> a utilizar.
    /// </param>
    /// <param name="range">
    /// <see cref="Range{T}" /> de números a seleccionar.
    /// </param>
    /// <returns>
    /// Un número entero aleatorio que se encuentra dentro del rango
    /// especificado.
    /// </returns>
    public static int Next(this Random r, in Range<int> range)
    {
        return r.Next(range.Minimum, range.Maximum);
    }

    /// <summary>
    /// Obtiene aleatoriamente un valor <see langword="true"/> o
    /// <see langword="false"/>.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> o <see langword="false"/>, de manera
    /// aleatoria.
    /// </returns>
    /// <param name="r">
    /// Instancia del objeto <see cref="Random" /> a utilizar.
    /// </param>
    public static bool CoinFlip(this Random r)
    {
        return (r.Next() & 1) == 0;
    }
}
