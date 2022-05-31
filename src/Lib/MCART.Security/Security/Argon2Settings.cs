/*
Argon2Settings.cs

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

namespace TheXDS.MCART.Security;

/// <summary>
/// Contiene valores de configuración a utilizar para derivar contraseñas
/// utilizando el algoritmo Argon2.
/// </summary>
public record struct Argon2Settings
{
    /// <summary>
    /// Obtiene o inicializa el bloque de sal a utilizar para derivar la clave.
    /// </summary>
    public byte[] Salt { get; init; }

    /// <summary>
    /// Obtiene o inicializa un valor que determina la cantidad de iteraciones
    /// a ejecutar de PBKDF2 para derivar una clave.
    /// </summary>
    public int Iterations { get; init; }

    /// <summary>
    /// Obtiene o inicializa un valor que indica la cantidad de memoria (en KB)
    /// a utilizar para derivar la clave.
    /// </summary>
    public int KbMemSize { get;init; }

    /// <summary>
    /// Obtiene o inicializa un valor que indica la cantidad de hilos a 
    /// utilizar al derivar una clave.
    /// </summary>
    public short Parallelism { get; init; }

    /// <summary>
    /// Obtiene o inicializa un valor que determina la cantidad de bytes a
    /// derivar.
    /// </summary>
    public int KeyLength { get; init; }
}