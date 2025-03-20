/*
RSACryptoStream.cs

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

using System.Security.Cryptography;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security.Cryptography;

/// <summary>
/// Implementa un flujo que lee y escribe información encriptada en RSA
/// sobre un <see cref="Stream"/> especificado.
/// </summary>
public partial class RSACryptoStream : Stream, IDisposable
{
    private readonly Stream _stream;
    private readonly RSACryptoServiceProvider _rsa;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoStream"/>.
    /// </summary>
    /// <param name="underlyingStream">
    /// <see cref="Stream"/> subyacente sobre el cual realizar las
    /// operaciones de lectura/escritura.
    /// </param>
    public RSACryptoStream(Stream underlyingStream) : this(underlyingStream, new RSACryptoServiceProvider())
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoStream"/>.
    /// </summary>
    /// <param name="underlyingStream">
    /// <see cref="Stream"/> subyacente sobre el cual realizar las
    /// operaciones de lectura/escritura.
    /// </param>
    /// <param name="rsa">
    /// Instancia de <see cref="RSACryptoServiceProvider"/> a utilizar
    /// para realizar las operaciones de encriptado/desencriptado.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, RSACryptoServiceProvider rsa)
    {
        _stream = underlyingStream;
        _rsa = rsa;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoStream"/>.
    /// </summary>
    /// <param name="underlyingStream">
    /// <see cref="Stream"/> subyacente sobre el cual realizar las
    /// operaciones de lectura/escritura.
    /// </param>
    /// <param name="keySize">
    /// Tamaño de la clave a generar para el RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, int keySize) : this(underlyingStream, new RSACryptoServiceProvider(keySize))
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoStream"/>.
    /// </summary>
    /// <param name="underlyingStream">
    /// <see cref="Stream"/> subyacente sobre el cual realizar las
    /// operaciones de lectura/escritura.
    /// </param>
    /// <param name="keyBlob">
    /// Blob binario con las claves a utilizar en el RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, byte[] keyBlob) : this(underlyingStream, new RSACryptoServiceProvider())
    {
        _rsa.ImportCspBlob(keyBlob);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoStream"/>.
    /// </summary>
    /// <param name="underlyingStream">
    /// <see cref="Stream"/> subyacente sobre el cual realizar las
    /// operaciones de lectura/escritura.
    /// </param>
    /// <param name="parameters">
    /// Parámetros de configuración a utilizar para el RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, RSAParameters parameters) : this(underlyingStream, new RSACryptoServiceProvider())
    {
        _rsa.ImportParameters(parameters);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoStream"/>.
    /// </summary>
    /// <param name="underlyingStream">
    /// <see cref="Stream"/> subyacente sobre el cual realizar las
    /// operaciones de lectura/escritura.
    /// </param>
    /// <param name="parameters">
    /// Parámetros de configuración a utilizar para el RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, CspParameters parameters) : this(underlyingStream, new RSACryptoServiceProvider(parameters))
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoStream"/>.
    /// </summary>
    /// <param name="underlyingStream">
    /// <see cref="Stream"/> subyacente sobre el cual realizar las
    /// operaciones de lectura/escritura.
    /// </param>
    /// <param name="dwKeySize">
    /// Longitud de las llaves a generar para el RSA.
    /// </param>
    /// <param name="parameters">
    /// Parámetros de configuración a utilizar para el RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, int dwKeySize, CspParameters parameters) : this(underlyingStream, new RSACryptoServiceProvider(dwKeySize, parameters))
    {
    }

    /// <summary>
    /// Obtiene una referencia al <see cref="Stream"/> en el cual esta 
    /// instancia lee y escribe datos.
    /// </summary>
    public Stream BaseStream => _stream;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="Stream"/> puede 
    /// ser leído.
    /// </summary>
    public override bool CanRead => _stream.CanRead && !_rsa.PublicOnly;

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="Stream"/>
    /// permite desplazarse por el contenido del mismo.
    /// </summary>
    public override bool CanSeek => false;

    /// <summary>
    /// Obtiene un valor que indica si se puede escribir sobre este
    /// <see cref="Stream"/>.
    /// </summary>
    public override bool CanWrite => _stream.CanWrite;

    /// <summary>
    /// Obtiene la longitud de este <see cref="Stream"/>.
    /// </summary>
    public override long Length => throw new NotSupportedException();

    /// <summary>
    /// Obtiene o establece la posición del cursor dentro de este
    /// <see cref="Stream"/>.
    /// </summary>
    public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

    /// <summary>
    /// Vacía los búferes de escritura de este <see cref="Stream"/>
    /// causando que la información del mismo sea escrita en el
    /// dispositivo subyacente.
    /// </summary>
    public override void Flush()
    {
        _stream.Flush();
    }

    /// <summary>
    /// Lee la totalidad de los datos de este <see cref="Stream"/>.
    /// </summary>
    /// <param name="buffer">Búfer de salida de datos.</param>
    /// <param name="offset">Offset sobre el cual empezar a escribir datos.</param>
    /// <param name="count">Cantidad máxima de bytes a escribir en el búfer de salida.</param>
    /// <returns>
    /// La cantidad de bytes escritos en el búfer de salida.
    /// </returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
        byte[]? a = ReadToEnd().Take(count).ToArray();
        a.CopyTo(buffer, offset);
        return a.Length;
    }

    /// <summary>
    /// Lee todo el contenido de este <see cref="Stream"/>.
    /// </summary>
    /// <returns>
    /// El contenido leído de este <see cref="Stream"/>.
    /// </returns>
    public byte[] ReadToEnd()
    {
        ReadToEnd_Contract();
        byte[] a;
        if (_stream.CanSeek)
        {
            int sze = (int)_stream.RemainingBytes();
            a = new byte[sze];
            _stream.Read(a, 0, sze);
        }
        else
        {
            List<byte>? l = new();
            int r;
            while (true)
            {
                r = _stream.ReadByte();
                if (r == -1) break;
                l.Add((byte)r);
            }
            a = l.ToArray();
        }
        return _rsa.Decrypt(a, true);
    }

    /// <summary>
    /// Desplaza el cursor del lectura dentro de este <see cref="Stream"/>.
    /// </summary>
    /// <param name="offset">
    /// Número de posiciones a desplazar el cursor.
    /// </param>
    /// <param name="origin">
    /// Punto de origen del desplazamiento.
    /// </param>
    /// <returns>
    /// La cantidad de bytes que el cursor se ha desplazado.
    /// </returns>
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Establece la longitud de este <see cref="Stream"/>.
    /// </summary>
    /// <param name="value"></param>
    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Escribe una secuencia de datos en este <see cref="Stream"/>.
    /// </summary>
    /// <param name="buffer">
    /// Búfer de datos a escribir.
    /// </param>
    /// <param name="offset">
    /// Offset desde el cual leer los datos a escribir.
    /// </param>
    /// <param name="count">
    /// Cantidad de bytes a escribir.
    /// </param>
    public override void Write(byte[] buffer, int offset, int count)
    {
        Write_Contract(buffer, offset, count);
        byte[]? b = buffer.Range(offset, count).ToArray();
        if (b.Length == 0) return;
        byte[]? eb = _rsa.Encrypt(b, true);
        _stream.Write(eb, 0, eb.Length);
    }
}
