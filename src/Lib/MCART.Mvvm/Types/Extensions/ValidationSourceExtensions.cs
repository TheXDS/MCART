// ValidationSourceExtensions.cs
// 
// This file is part of Morgan's CLR Advanced Runtime (MCART)
// 
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
// 
// Released under the MIT License (MIT)
// Copyright © 2011 - 2024 César Andrés Morgan
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

using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones para todos los elementos de tipo
/// <see cref="IValidationEntry{T}" /> que proveen de métodos de configuración
/// especiales para reglas de validación.
/// </summary>
public static class ValidationSourceExtensions
{
    /// <summary>
    /// Registra una regla de validación que indica que la cadena no debe ser
    /// <see langword="null"/> ni <see cref="string.Empty"/>.
    /// </summary>
    /// <param name="rule">Instancia de reglas de validación.</param>
    /// <param name="error">
    /// Mensaje de error a mostrar cuando la cadena no pase esta regla de
    /// validación.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="rule"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public static IValidationEntry<string> NotEmpty(this IValidationEntry<string?> rule, string error)
    {
        return rule.AddRule(p => p.IsEmpty() ? null : true, error)!;
    }
}
