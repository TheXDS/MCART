﻿/*
ByteUnitType.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene operaciones comunes de transformación de datos en los
programas, y de algunas comparaciones especiales.

Algunas de estas funciones también se implementan como extensiones, por lo que
para ser llamadas únicamente es necesario importar el espacio de nombres
"TheXDS.MCART" y utilizar sintaxis de instancia.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

namespace TheXDS.MCART.Helpers;

public static partial class Common
{
    /// <summary>
    /// Enumera los tipos de unidades que se pueden utilizar para
    /// representar grandes cantidades de bytes.
    /// </summary>
    [Flags]
    public enum ByteUnitType : byte
    {
        /// <summary>
        /// Numeración binaria. Cada orden de magnitud equivale a 1024 de su inferior.
        /// </summary>
        Binary,
        /// <summary>
        /// Numeración decimal. Cada orden de magnitud equivale a 1000 de su inferior. 
        /// </summary>
        Decimal,
        /// <summary>
        /// Numeración binaria con nombre largo. Cada orden de magnitud equivale a 1024 de su inferior.
        /// </summary>
        BinaryLong,
        /// <summary>
        /// Numeración decimal con nombre largo. Cada orden de magnitud equivale a 1000 de su inferior. 
        /// </summary>
        DecimalLong
    }
}
