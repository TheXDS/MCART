/*
IPasswordStorage.cs

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

using System.Security;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que provea de 
/// métodos para generar Hashes a partir de contraseñas que puedan ser
/// almacenados de forma segura.
/// </summary>
public interface IPasswordStorage
{
    /// <summary>
    /// Obtiene el nombre del algoritmo.
    /// </summary>
    string AlgId => GetType().Name.ChopEndAny("PasswordStorage", "Storage").ToUpperInvariant();

    /// <summary>
    /// Obtiene la configuración a partir del bloque especificado, haciendo
    /// avanzar el lector la cantidad de bytes requeridos por la configuración
    /// de esta instancia.
    /// </summary>
    /// <param name="reader">
    /// Objeto a partir del cual leer los valores de configuración.
    /// </param>
    void ConfigureFrom(BinaryReader reader);

    /// <summary>
    /// Vuelca los valores de configuración en formato binario.
    /// </summary>
    /// <returns>
    /// Un arreglo de bytes a partir del cual se puede volver a construir el
    /// objeto que contiene los valores de configuración de derivación de
    /// claves para esta instancia.
    /// </returns>
    byte[] DumpSettings();

    /// <summary>
    /// Genera un blob binario que puede ser almacenado en una base de datos.
    /// </summary>
    /// <param name="input">
    /// Contraseña a partir de la cual derivar una clave.
    /// </param>
    /// <returns>
    /// Un arreglo de bytes con la clave derivada a partir de la contraseña 
    /// especificada.</returns>
    byte[] Generate(byte[] input);

    /// <summary>
    /// Genera un blob binario que puede ser almacenado en una base de datos.
    /// </summary>
    /// <param name="input">
    /// Contraseña a partir de la cual derivar una clave.
    /// </param>
    /// <returns>
    /// Un arreglo de bytes con la clave derivada a partir de la contraseña 
    /// especificada.</returns>
    byte[] Generate(SecureString input) => Generate(System.Text.Encoding.UTF8.GetBytes(input.Read()));

    /// <summary>
    /// Obtiene un valor que indica la cantidad de bytes de clave que esta
    /// instancia generará.
    /// </summary>
    int KeyLength { get; }

    /// <summary>
    /// Obtiene un objeto que contiene la configuración del algoritmo.
    /// </summary>
    public object? Settings => null;
}
