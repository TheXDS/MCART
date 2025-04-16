/*
StreamExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Text;
using TheXDS.MCART.Attributes;
using System.Diagnostics;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensiones de la clase <see cref="Stream"/>.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Destruye el contenido del <see cref="Stream"/>.
    /// </summary>
    /// <param name="fs">
    /// <see cref="Stream"/> a destruir.
    /// </param>
    /// <exception cref="IOException">
    /// Se produce si ocurre un error de I/O durante la operación.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Se produce si el <see cref="Stream"/> no soporta operaciones de
    /// escritura y búsqueda, como en casos en que el mismo se ha
    /// construido a partir de un túnel o del flujo de la consola.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Ocurre cuando se ha intentado ejecutar esta operación sobre un
    /// <see cref="Stream"/> que ya ha sido desechado.
    /// </exception>
    [DebuggerStepThrough]
    [Sugar]
    public static void Destroy(this Stream fs) => fs.SetLength(0);

    /// <summary>
    /// Salta la cantidad especificada de bytes en la secuencia desde
    /// la posición actual.
    /// </summary>
    /// <param name="fs">
    /// <see cref="Stream"/> en el cual se saltará el cursor de
    /// lectura.
    /// </param>
    /// <param name="bytesToSkip">Bytes a saltar.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Se produce si <paramref name="bytesToSkip"/> es menor a cero, o
    /// si al saltar, el cursor se extiende fuera de la secuencia.
    /// </exception>
    [DebuggerStepThrough]
    [Sugar]
    public static void Skip(this Stream fs, int bytesToSkip)
    {
        if (bytesToSkip < 0 || (fs.Position + bytesToSkip) > fs.Length)
            throw new ArgumentOutOfRangeException(nameof(bytesToSkip));
        fs.Seek(bytesToSkip, SeekOrigin.Current);
    }

    /// <summary>
    /// Lee una cadena desde la secuencia y avanza la posición de
    /// lectura hasta después del último carácter Unicode leído.
    /// </summary>
    /// <returns>La cadena que ha sido leída.</returns>
    /// <param name="fs">
    /// <see cref="Stream"/> desde el cual leer la cadena.
    /// </param>
    /// <param name="count">Cantidad de caracteres a leer.</param>
    [DebuggerStepThrough]
    [Sugar]
    public static string ReadString(this Stream fs, int count) => ReadString(fs, count, Encoding.Default);

    /// <summary>
    /// Lee una cadena desde la secuencia y avanza la posición de
    /// lectura hasta después del último carácter leído.
    /// </summary>
    /// <returns>La cadena que ha sido leída.</returns>
    /// <param name="fs">
    /// <see cref="Stream"/> desde el cual leer la cadena.
    /// </param>        
    /// <param name="count">Cantidad de caracteres a leer.</param>
    /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
    [DebuggerStepThrough]
    public static string ReadString(this Stream fs, int count, Encoding encoding)
    {
        List<char>? retVal = [];
        using BinaryReader? br = new(fs, encoding, true);
        while (retVal.Count < count) retVal.Add(br.ReadChar());
        return new string([.. retVal]);
    }

    /// <summary>
    /// Obtiene la cantidad de bytes restantes desde la posición
    /// actual.
    /// </summary>
    /// <param name="fs">
    /// <see cref="Stream"/> del cual se obtendrán los bytes restantes.
    /// </param>
    /// <returns>
    /// La cantidad de bytes restantes desde la posición actual.
    /// </returns>
    [DebuggerStepThrough]
    [Sugar] public static long RemainingBytes(this Stream fs) => fs.Length - fs.Position;

    /// <summary>
    /// Lee asíncronamente una cadena desde la secuencia y avanza la
    /// posición de lectura hasta después del último carácter Unicode
    /// leído.
    /// </summary>
    /// <returns>La cadena que ha sido leída.</returns>
    /// <param name="fs">
    /// <see cref="Stream"/> desde el cual leer la cadena.
    /// </param>        
    /// <param name="count">Cantidad de caracteres a leer.</param>
    [DebuggerStepThrough]
    [Sugar] public static Task<string> ReadStringAsync(this Stream fs, int count) => Task.Run(() => ReadString(fs, count));

    /// <summary>
    /// Lee asíncronamente una cadena desde la secuencia y avanza la
    /// posición de lectura hasta después del último carácter leído.
    /// </summary>
    /// <returns>La cadena que ha sido leída.</returns>
    /// <param name="fs">
    /// <see cref="Stream"/> desde el cual leer la cadena.
    /// </param>        
    /// <param name="count">Cantidad de caracteres a leer.</param>
    /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
    [DebuggerStepThrough]
    public static Task<string> ReadStringAsync(this Stream fs, int count, Encoding encoding) => Task.Run(() => ReadString(fs, count, encoding));

    /// <summary>
    /// Lee asíncronamente una cadena desde la posición actual hasta el
    /// final de la secuencia.
    /// </summary>
    /// <param name="fs">
    /// <see cref="Stream"/> desde el cual leer la cadena.
    /// </param>        
    /// <returns>La cadena que ha sido leída.</returns>
    [DebuggerStepThrough]
    [Sugar] public static Task<string> ReadStringToEndAsync(this Stream fs) => ReadStringToAsync(fs, fs.Length);

    /// <summary>
    /// Lee asíncronamente una cadena desde la posición actual hasta 
    /// alcanzar la posición especificada.
    /// </summary>
    /// <param name="fs">
    /// <see cref="Stream"/> desde el cual leer la cadena.
    /// </param>        
    /// <param name="pos">
    /// Posición hasta la cual se leerá la cadena.
    /// </param>
    /// <returns>La cadena que ha sido leída.</returns>
    [DebuggerStepThrough]
    public static async Task<string> ReadStringToAsync(this Stream fs, long pos)
    {
        if (pos < fs.Position)
        {
            (pos, fs.Position) = (fs.Position, pos);
        }
        byte[]? bf = new byte[pos - fs.Position];
        await fs.ReadAsync(bf);
        return Encoding.Default.GetString(bf);
    }

    /// <summary>
    /// Escribe un conjunto de <see cref="byte"/> en el 
    /// <see cref="Stream"/>.
    /// </summary>
    /// <param name="fs">
    /// <see cref="Stream"/> sobre el cual se escribirán los bytes.
    /// </param>
    /// <param name="bytes">
    /// Colección de <see cref="byte"/> a escribir en el
    /// <see cref="Stream"/>.
    /// </param>
    [DebuggerStepThrough]
    [Sugar] public static void WriteBytes(this Stream fs, params byte[] bytes) => fs.Write(bytes, 0, bytes.Length);

    /// <summary>
    /// Escribe un conjunto de secuencias de <see cref="byte"/> en el 
    /// <see cref="Stream"/>.
    /// </summary>
    /// <param name="fs">
    /// <see cref="Stream"/> sobre el cual se escribirán los bytes.
    /// </param>
    /// <param name="bytes">
    /// Colecciones de <see cref="byte"/> a escribir en el
    /// <see cref="Stream"/>.
    /// </param>
    [DebuggerStepThrough]
    public static void WriteSeveralBytes(this Stream fs, params byte[][] bytes)
    {
        foreach (byte[]? x in bytes) fs.Write(x, 0, x.Length);
    }
}
