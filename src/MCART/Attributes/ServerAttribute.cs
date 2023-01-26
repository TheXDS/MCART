/*
ServerAttribute.cs

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
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.Misc.Internals;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Atributo que define la ruta de un servidor.
/// </summary>
/// <remarks>
/// Es posible establecer este atributo más de una vez en un mismo elemento.
/// </remarks>
[AttributeUsage(All, AllowMultiple = true)]
[Serializable]
public sealed class ServerAttribute : Attribute, IValueAttribute<string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ServerAttribute" /> estableciendo el servidor y el puerto
    /// al cual este atributo hará referencia.
    /// </summary>
    /// <param name="server">Nombre del servidor / Dirección IP.</param>
    /// <param name="port">Número de puerto del servidor.</param>
    /// <remarks>
    /// Si se define un número de puerto en <paramref name="server" />, el
    /// valor del parámetro <paramref name="port" /> tomará precedencia.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Se produce si el servidor es una ruta malformada.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="port" /> es inferior a 1, o superior
    /// a 65535.
    /// </exception>
    public ServerAttribute(string server, int port)
    {
        if (!port.IsBetween(1, 65535)) throw Errors.ValueOutOfRange(nameof(port), 1, 65535);
        Server = EmptyChecked(server, nameof(server));
        Port = port;
    }

    /// <summary>
    /// Obtiene el servidor.
    /// </summary>
    /// <value>
    /// La ruta del servidor a la cual este atributo apunta.
    /// </value>
    public string Server { get; }

    /// <summary>
    /// Obtiene o establece el puerto de conexión del servidor.
    /// </summary>
    /// <value>
    /// Un valor entre 1 y 65535 que establece el número de puerto a
    /// apuntar.
    /// </value>
    public int Port { get; }

    /// <summary>
    /// Devuelve una cadena que representa al objeto actual.
    /// </summary>
    /// <returns>Una cadena que representa al objeto actual.</returns>
    public override string ToString()
    {
        return $"{Server}:{Port}";
    }

    /// <summary>
    /// Obtiene el valor de este atributo.
    /// </summary>
    public string Value => ToString();
}
