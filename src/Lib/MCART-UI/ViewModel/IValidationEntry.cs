/*
IValidationEntry.cs

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

namespace TheXDS.MCART.ViewModel;
using System;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que permita
/// configurar reglas de validación para una propiedad.
/// </summary>
/// <typeparam name="T">Tipo de la propiedad seleccionada.</typeparam>
public interface IValidationEntry<T>
{
    /// <summary>
    /// Agrega una regla de validación para la propiedad seleccionada.
    /// </summary>
    /// <param name="rule">
    /// FUnción que ejecutala validación. La función debe devolder 
    /// <see langword="true"/> si la propiedad pasa satisfactoriamente
    /// la prueba, <see langword="false"/> en caso contrario.
    /// </param>
    /// <param name="error">
    /// Mensaje de error a mostrarse si la regla falla.
    /// </param>
    /// <returns>
    /// La misma instancia de regla de validación, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    IValidationEntry<T> AddRule(Func<T, bool> rule, string error);
}
