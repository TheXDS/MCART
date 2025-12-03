/*
RSACryptoTransform.cs

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
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security.Cryptography;

/// <summary>
/// Implements a cryptographic transformation that encrypts data using
/// the RSA algorithm.
/// </summary>
public class RSACryptoTransform : Disposable, ICryptoTransform
{
    /// <summary>
    /// Instantiates a new <see cref="CryptoStream"/> using the
    /// <see cref="RSACryptoServiceProvider"/> instance to create a new
    /// <see cref="RSACryptoTransform"/>.
    /// </summary>
    /// <param name="stream">
    /// Stream on which to write the encrypted data.
    /// </param>
    /// <param name="rsa">
    /// <see cref="RSACryptoServiceProvider"/> instance to use for
    /// constructing the new <see cref="RSACryptoTransform"/>.
    /// </param>
    /// <returns>
    /// A new <see cref="CryptoStream"/> with the specified destination
    /// and transformation object.
    /// </returns>
    public static CryptoStream ToStream(Stream stream, RSACryptoServiceProvider rsa)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        return new CryptoStream(stream, new RSACryptoTransform(rsa), CryptoStreamMode.Write);
    }

    private readonly RSACryptoServiceProvider _rsa;

    /// <summary>
    /// Gets the RSA parameters for this <see cref="RSACryptoTransform"/>.
    /// </summary>
    /// <param name="includePrivateParameters">
    /// Indicates whether to include private key parameters in the exported
    /// value.
    /// </param>
    /// <returns>
    /// The RSA parameters for this <see cref="RSACryptoTransform"/>.
    /// </returns>
    public RSAParameters GetRSAParameters(bool includePrivateParameters)
    {
        return _rsa.ExportParameters(includePrivateParameters);
    }

    /// <summary>
    /// Exports the RSA CSP blob for this <see cref="RSACryptoTransform"/>.
    /// </summary>
    /// <param name="includePrivateParameters">
    /// Indicates whether to include private key parameters in the exported
    /// value.
    /// </param>
    /// <returns>
    /// The RSA CSP blob for this <see cref="RSACryptoTransform"/>.
    /// </returns>
    public byte[] GetRSACspBlob(bool includePrivateParameters)
    {
        return _rsa.ExportCspBlob(includePrivateParameters);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoTransform"/> class,
    /// generating a 4096‑bit RSA key.
    /// </summary>
    public RSACryptoTransform() : this(4096)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoTransform"/> class
    /// with the supplied <see cref="RSACryptoServiceProvider"/>.
    /// </summary>
    public RSACryptoTransform(RSACryptoServiceProvider rsa)
    {
        _rsa = rsa;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoTransform"/> class
    /// with the specified RSA key size.
    /// </summary>
    /// <param name="keySize">Size of the RSA key to generate.</param>
    public RSACryptoTransform(int keySize)
    {
        _rsa = new RSACryptoServiceProvider(keySize);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoTransform"/> class
    /// using the specified byte blob containing RSA configuration
    /// information.
    /// </summary>
    /// <param name="keyBlob">
    /// Binary CSP blob that contains the RSA key configuration.
    /// </param>
    public RSACryptoTransform(byte[] keyBlob)
    {
        _rsa = new RSACryptoServiceProvider();
        _rsa.ImportCspBlob(keyBlob);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoTransform"/> class,
    /// specifying the RSA parameters to use for initialization.
    /// </summary>
    /// <param name="parameters">RSA initialization parameters.</param>
    public RSACryptoTransform(RSAParameters parameters)
    {
        _rsa = new RSACryptoServiceProvider();
        _rsa.ImportParameters(parameters);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoTransform"/> class,
    /// specifying the CspParameters to use for initialization.
    /// </summary>
    /// <param name="parameters">CspParameters for the RSA provider.</param>
    public RSACryptoTransform(CspParameters parameters)
    {
        _rsa = new RSACryptoServiceProvider(parameters);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoTransform"/> class,
    /// specifying the key size and CspParameters for initialization.
    /// </summary>
    /// <param name="dwKeySize">RSA key size to use.</param>
    /// <param name="parameters">CspParameters for the RSA provider.</param>
    public RSACryptoTransform(int dwKeySize, CspParameters parameters)
    {
        _rsa = new RSACryptoServiceProvider(dwKeySize, parameters);
    }

    /// <summary>
    /// Indicates whether this <see cref="ICryptoTransform"/> can be reused to
    /// transform more than one block.
    /// </summary>
    public bool CanReuseTransform => true;

    /// <summary>
    /// Indicates whether this <see cref="ICryptoTransform"/> can transform
    /// multiple blocks.
    /// </summary>
    public bool CanTransformMultipleBlocks => true;

    /// <summary>
    /// Gets the input block size for the transformation.
    /// </summary>
    public int InputBlockSize => (_rsa.KeySize / 8) - 42;

    /// <summary>
    /// Gets the output block size for the transformation.
    /// </summary>
    public int OutputBlockSize => _rsa.KeySize / 8;

    /// <summary>
    /// Releases all resources used by this object.
    /// </summary>
    protected override void OnDispose()
    {
        _rsa.Dispose();
    }

    /// <summary>
    /// Performs the transformation of a block of data.
    /// </summary>
    /// <param name="inputBuffer">Input data buffer.</param>
    /// <param name="inputOffset">
    /// Offset within the input buffer from which to begin reading data.
    /// </param>
    /// <param name="inputCount">
    /// Number of bytes from the input buffer to transform.
    /// </param>
    /// <param name="outputBuffer">
    /// Output buffer for the transformed data.
    /// </param>
    /// <param name="outputOffset">
    /// Offset within the output buffer at which to start writing.
    /// </param>
    /// <returns>
    /// The number of bytes written to the output buffer.
    /// </returns>
    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
        byte[]? b = _rsa.Encrypt([.. inputBuffer.Range(inputOffset, inputCount)], true);
        b.CopyTo(outputBuffer, outputOffset);
        return b.Length;
    }

    /// <summary>
    /// Performs the transformation of the final block of data.
    /// </summary>
    /// <param name="inputBuffer">Input data buffer.</param>
    /// <param name="inputOffset">
    /// Offset within the input buffer from which to begin reading.
    /// </param>
    /// <param name="inputCount">
    /// Number of bytes from the input buffer to transform.
    /// </param>
    /// <returns>
    /// The transformed data block.
    /// </returns>
    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
        return _rsa.Encrypt([.. inputBuffer.Range(inputOffset, inputCount)], true);
    }
}
