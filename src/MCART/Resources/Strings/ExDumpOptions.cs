/*
ExDumpOptions.cs

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

namespace TheXDS.MCART.Resources.Strings;
using System;

/// <summary>
/// Especifica distintas opciones de formato de texto a generar para
/// describir una excepción.
/// </summary>
[Flags]
public enum ExDumpOptions : byte
{
    /// <summary>
    /// Incluye el nombre de la excepción.
    /// </summary>
    Name = 1,

    /// <summary>
    /// Incluye la descripción de la ubicación donde se ha producido la
    /// excepción.
    /// </summary>
    Source = 2,

    /// <summary>
    /// Incluye el mensaje de error de la excepción.
    /// </summary>
    Message = 4,

    /// <summary>
    /// Incluye el HRESULT de la excepción.
    /// </summary>
    HResult = 8,

    /// <summary>
    /// Incluye un volcado de pila de la excepción.
    /// </summary>
    StackTrace = 16,

    /// <summary>
    /// Incluye cualquier propiedad que contenga excepciones internas.
    /// </summary>
    Inner = 32,

    /// <summary>
    /// Incluye un listado de los ensamblados cargados en el AppDomain
    /// donde se produjo la excepción.
    /// </summary>
    LoadedAssemblies = 64,

    /// <summary>
    /// Formatea el texto para presentarlo en un ancho predefinido.
    /// </summary>
    TextWidthFormatted = 128,

    /// <summary>
    /// Muestra toda la información de la excepción y cualquier excepción
    /// interna producida.
    /// </summary>
    All = 127,

    /// <summary>
    /// Muestra toda la información de la excepción y cualquier excepción
    /// interna producida, formateando el texto para presentarlo en un
    /// ancho predefinido.
    /// </summary>
    AllFormatted = 255,
}
