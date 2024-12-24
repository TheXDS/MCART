/*
IPasswordStorage_T.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

namespace TheXDS.MCART.Security;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que implementa
/// <see cref="IPasswordStorage"/> que incluya información de configuración del
/// algoritmo de derivación de clave.
/// </summary>
/// <typeparam name="T">
/// Tipo que contiene los valores de configuración. Se recomienda el uso de
/// tipos <see langword="record"/> con propiedades no mutables.
/// </typeparam>
public interface IPasswordStorage<T> : IPasswordStorage where T : struct
{
    /// <summary>
    /// Obtiene una referencia a la configuración activa de esta instancia.
    /// </summary>
    new T Settings { get; set; }

    object IPasswordStorage.Settings => Settings;

    /// <summary>
    /// Obtiene la configuración a partir del bloque especificado.
    /// </summary>
    /// <param name="data">
    /// Bloque de memoria que contiene los valores de configuración.
    /// </param>
    void ConfigureFrom(ReadOnlySpan<byte> data)
    {
        using var ms = new MemoryStream(data.ToArray());
        using var reader = new BinaryReader(ms);
        ConfigureFrom(reader);
    }
}
