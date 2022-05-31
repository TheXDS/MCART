/*
UiErrors.cs

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

namespace TheXDS.MCART.Resources;
using System;

/// <summary>
/// Contiene una serie de funciones que generan excepciones para errores
/// ocurridos dentro de las funciones de UI de MCART.
/// </summary>
public static class UiErrors
{
    /// <summary>
    /// Genera una excepción cuando un valor es <see langword="null"/>.
    /// </summary>
    /// <param name="v">
    /// Nombre del valor que es <see langword="null"/>.
    /// </param>
    /// <returns>
    /// Una nueva instancia de la clase <see cref="ArgumentException"/> que
    /// describe el error.
    /// </returns>
    public static Exception NullValue(string v)
    {
        return new ArgumentException(string.Format(Strings.UiErrors.NullValueException, v));
    }
}
