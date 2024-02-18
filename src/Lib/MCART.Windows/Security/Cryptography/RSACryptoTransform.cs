/*
RSACryptoTransform.cs

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

using System.Security.Cryptography;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security.Cryptography;

/// <summary>
/// Implementa una transformación criptográfica para encriptar datos
/// por medio del algoritmo RSA.
/// </summary>
public class RSACryptoTransform : Disposable, ICryptoTransform
{
    /// <summary>
    /// Instancia un nuevo <see cref="CryptoStream"/> utilizando la
    /// instancia de <see cref="RSACryptoServiceProvider"/> para crear
    /// un nuevo <see cref="RSACryptoTransform"/>.
    /// </summary>
    /// <param name="stream">
    /// Flujo sobre el cual escribir los datos encriptados.
    /// </param>
    /// <param name="rsa">
    /// Instancia de <see cref="RSACryptoServiceProvider"/> a utilizar
    /// para construir el nuevo <see cref="RSACryptoTransform"/>
    /// </param>
    /// <returns>
    /// Un nuevo <see cref="CryptoStream"/> con el destino y el objeto
    /// de transformación especificados.
    /// </returns>
    public static CryptoStream ToStream(Stream stream, RSACryptoServiceProvider rsa)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        return new CryptoStream(stream, new RSACryptoTransform(rsa), CryptoStreamMode.Write);
    }

    private readonly RSACryptoServiceProvider _rsa;

    /// <summary>
    /// Obtiene los parámetros RSA para este
    /// <see cref="RSACryptoTransform"/>.
    /// </summary>
    /// <param name="includePrivateParameters">
    /// Indica si deben incluirse los parámetros de clave privada en el
    /// valor exportado.
    /// </param>
    /// <returns>
    /// Los parámetros RSA para este <see cref="RSACryptoTransform"/>.
    /// </returns>
    public RSAParameters GetRSAParameters(bool includePrivateParameters)
    {
        return _rsa.ExportParameters(includePrivateParameters);
    }

    /// <summary>
    /// Obtiene los parámetros RSA para este
    /// <see cref="RSACryptoTransform"/>.
    /// </summary>
    /// <param name="includePrivateParameters">
    /// Indica si deben incluirse los parámetros de clave privada en el
    /// valor exportado.
    /// </param>
    /// <returns>
    /// Los parámetros RSA para este <see cref="RSACryptoTransform"/>.
    /// </returns>
    public byte[] GetRSACspBlob(bool includePrivateParameters)
    {
        return _rsa.ExportCspBlob(includePrivateParameters);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoTransform"/>, generando una clave RSA de
    /// 4096 bits.
    /// </summary>
    public RSACryptoTransform() : this(4096)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoTransform"/>.
    /// </summary>
    public RSACryptoTransform(RSACryptoServiceProvider rsa)
    {
        _rsa = rsa;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoTransform"/> especificando el tamaño a
    /// utilizar para generar las claves RSA.
    /// </summary>
    /// <param name="keySize">Tamaño de la clave RSA a generar.</param>
    public RSACryptoTransform(int keySize)
    {
        _rsa = new RSACryptoServiceProvider(keySize);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoTransform"/>, especificando un blob de
    /// bytes con el contenido de la configuración a utilizar para
    /// inicializar el RSA.
    /// </summary>
    /// <param name="keyBlob">
    /// Blob binario de configuración de las claves RSA.
    /// </param>
    public RSACryptoTransform(byte[] keyBlob)
    {
        _rsa = new RSACryptoServiceProvider();
        _rsa.ImportCspBlob(keyBlob);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoTransform"/>, especificando los parámetros
    /// de configuración a utilizar para inicializar el RSA.
    /// </summary>
    /// <param name="parameters">
    /// Parámetros de inicialización del RSA.
    /// </param>
    public RSACryptoTransform(RSAParameters parameters)
    {
        _rsa = new RSACryptoServiceProvider();
        _rsa.ImportParameters(parameters);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoTransform"/>, especificando los parámetros
    /// de configuración a utilizar para inicializar el RSA.
    /// </summary>
    /// <param name="parameters">
    /// Parámetros de inicialización del RSA.
    /// </param>
    public RSACryptoTransform(CspParameters parameters)
    {
        _rsa = new RSACryptoServiceProvider(parameters);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RSACryptoTransform"/>, especificando los parámetros
    /// de configuración a utilizar para inicializar el RSA.
    /// </summary>
    /// <param name="dwKeySize">Tamaño de la clave RSA a utilizar.</param>
    /// <param name="parameters">
    /// Parámetros de inicialización del RSA.
    /// </param>
    public RSACryptoTransform(int dwKeySize, CspParameters parameters)
    {
        _rsa = new RSACryptoServiceProvider(dwKeySize, parameters);
    }

    /// <summary>
    /// Indica si este <see cref="ICryptoTransform"/> puede ser
    /// reutilizado para transformar más de un bloque.
    /// </summary>
    public bool CanReuseTransform => true;

    /// <summary>
    /// Indica si este <see cref="ICryptoTransform"/> puede transformar
    /// múltiples bloques.
    /// </summary>
    public bool CanTransformMultipleBlocks => true;

    /// <summary>
    /// Obtiene el tamaño de entrada del bloque a transformar.
    /// </summary>
    public int InputBlockSize => (_rsa.KeySize / 8) - 42;

    /// <summary>
    /// Obtiene el tamaño de salida del bloque.
    /// </summary>
    public int OutputBlockSize => _rsa.KeySize / 8;

    /// <summary>
    /// Libera los recursos utilizados por este objeto.
    /// </summary>
    protected override void OnDispose()
    {
        _rsa.Dispose();
    }

    /// <summary>
    /// Efectúa la transformación de un bloque de datos.
    /// </summary>
    /// <param name="inputBuffer">Búfer de datos a transformar.</param>
    /// <param name="inputOffset">
    /// Offset del búfer de entrada a partir del cual tomar datos.
    /// </param>
    /// <param name="inputCount">
    /// Cantidad de bytes del búfer de entrada a tomar para la
    /// transformación.
    /// </param>
    /// <param name="outputBuffer">
    /// Búfer de salida de la transformación.
    /// </param>
    /// <param name="outputOffset">
    /// Offset del búfer de salida en el cual empezar a escribir datos.
    /// </param>
    /// <returns>
    /// La cantidad de bytes escritos en el búfer de salida.
    /// </returns>
    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
        byte[]? b = _rsa.Encrypt(inputBuffer.Range(inputOffset, inputCount).ToArray(), true);
        b.CopyTo(outputBuffer, outputOffset);
        return b.Length;
    }

    /// <summary>
    /// Efectúa la transformación de un bloque de datos.
    /// </summary>
    /// <param name="inputBuffer">Búfer de datos a transformar.</param>
    /// <param name="inputOffset">
    /// Offset del búfer de entrada a partir del cual tomar datos.
    /// </param>
    /// <param name="inputCount">
    /// Cantidad de bytes del búfer de entrada a tomar para la
    /// transformación.
    /// </param>
    /// <returns>
    /// Un bloque de datos con el resultado de la transformación.
    /// </returns>
    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
        return _rsa.Encrypt(inputBuffer.Range(inputOffset, inputCount).ToArray(), true);
    }
}
