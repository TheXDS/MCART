/*
RSACryptoStream.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
/// Implements a stream that reads and writes RSA‑encrypted
/// information on a specified <see cref="Stream"/>.
/// </summary>
/// <param name="underlyingStream">
/// The underlying <see cref="Stream"/> on which to perform
/// read/write operations.
/// </param>
/// <param name="rsa">
/// The <see cref="RSACryptoServiceProvider"/> instance used to
/// perform encryption/decryption.
/// </param>
public partial class RSACryptoStream(Stream underlyingStream, RSACryptoServiceProvider rsa) : Stream, IDisposable
{
    private readonly Stream _stream = underlyingStream;
    private readonly RSACryptoServiceProvider _rsa = rsa;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="RSACryptoStream"/> class.
    /// </summary>
    /// <param name="underlyingStream">
    /// The underlying <see cref="Stream"/> on which to perform
    /// read/write operations.
    /// </param>
    public RSACryptoStream(Stream underlyingStream) : this(underlyingStream, new RSACryptoServiceProvider())
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="RSACryptoStream"/> class.
    /// </summary>
    /// <param name="underlyingStream">
    /// The underlying <see cref="Stream"/> on which to perform
    /// read/write operations.
    /// </param>
    /// <param name="keySize">
    /// The key size to generate for the RSA algorithm.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, int keySize) : this(underlyingStream, new RSACryptoServiceProvider(keySize))
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="RSACryptoStream"/> class.
    /// </summary>
    /// <param name="underlyingStream">
    /// The underlying <see cref="Stream"/> on which to perform
    /// read/write operations.
    /// </param>
    /// <param name="keyBlob">
    /// A binary blob containing the RSA keys to use.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, byte[] keyBlob) : this(underlyingStream, new RSACryptoServiceProvider())
    {
        _rsa.ImportCspBlob(keyBlob);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="RSACryptoStream"/> class.
    /// </summary>
    /// <param name="underlyingStream">
    /// The underlying <see cref="Stream"/> on which to perform
    /// read/write operations.
    /// </param>
    /// <param name="parameters">
    /// Configuration parameters to use for RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, RSAParameters parameters) : this(underlyingStream, new RSACryptoServiceProvider())
    {
        _rsa.ImportParameters(parameters);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="RSACryptoStream"/> class.
    /// </summary>
    /// <param name="underlyingStream">
    /// The underlying <see cref="Stream"/> on which to perform
    /// read/write operations.
    /// </param>
    /// <param name="parameters">
    /// Configuration parameters to use for RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, CspParameters parameters) : this(underlyingStream, new RSACryptoServiceProvider(parameters))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RSACryptoStream"/> class.
    /// </summary>
    /// <param name="underlyingStream">
    /// The underlying <see cref="Stream"/> on which read/write operations are performed.
    /// </param>
    /// <param name="dwKeySize">
    /// Length of the keys to generate for RSA.
    /// </param>
    /// <param name="parameters">
    /// Configuration parameters to use for RSA.
    /// </param>
    public RSACryptoStream(Stream underlyingStream, int dwKeySize, CspParameters parameters) : this(underlyingStream, new RSACryptoServiceProvider(dwKeySize, parameters))
    {
    }

    /// <summary>
    /// Gets a reference to the <see cref="Stream"/> in which this
    /// instance reads and writes data.
    /// </summary>
    public Stream BaseStream => _stream;

    /// <summary>
    /// Gets a value that indicates whether this <see cref="Stream"/> can be read.
    /// </summary>
    public override bool CanRead => _stream.CanRead && !_rsa.PublicOnly;

    /// <summary>
    /// Gets a value that indicates whether this <see cref="Stream"/> allows
    /// seeking through its content.
    /// </summary>
    public override bool CanSeek => false;

    /// <summary>
    /// Gets a value that indicates whether data can be written to this
    /// <see cref="Stream"/>.
    /// </summary>
    public override bool CanWrite => _stream.CanWrite;

    /// <summary>
    /// Gets the length of this <see cref="Stream"/>.
    /// </summary>
    public override long Length => throw new NotSupportedException();

    /// <summary>
    /// Gets or sets the current position within this <see cref="Stream"/>.
    /// </summary>
    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    /// <summary>
    /// Flushes the write buffers of this <see cref="Stream"/>, causing
    /// any buffered data to be written to the underlying device.
    /// </summary>
    public override void Flush()
    {
        _stream.Flush();
    }

    /// <summary>
    /// Reads the entire content of this <see cref="Stream"/>.
    /// </summary>
    /// <param name="buffer">Output data buffer.</param>
    /// <param name="offset">Offset at which to begin writing data.</param>
    /// <param name="count">Maximum number of bytes to write to the output buffer.</param>
    /// <returns>The number of bytes read into the output buffer.</returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
        byte[]? a = ReadToEnd().Take(count).ToArray();
        a.CopyTo(buffer, offset);
        return a.Length;
    }

    /// <summary>
    /// Reads the entire content of this <see cref="Stream"/>.
    /// </summary>
    /// <returns>
    /// The data read from this <see cref="Stream"/>.
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
            List<byte>? l = [];
            int r;
            while (true)
            {
                r = _stream.ReadByte();
                if (r == -1) break;
                l.Add((byte)r);
            }
            a = [.. l];
        }
        return _rsa.Decrypt(a, true);
    }

    /// <summary>
    /// Seeks within the readable cursor of this <see cref="Stream"/>.
    /// </summary>
    /// <param name="offset">
    /// The number of positions to move the cursor.
    /// </param>
    /// <param name="origin">
    /// The origin point for the offset.
    /// </param>
    /// <returns>
    /// The number of bytes the cursor has moved.
    /// </returns>
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Sets the length of this <see cref="Stream"/>.
    /// </summary>
    /// <param name="value"></param>
    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Writes a sequence of data to this <see cref="Stream"/>.
    /// </summary>
    /// <param name="buffer">
    /// The data buffer to write.
    /// </param>
    /// <param name="offset">
    /// The offset from which to read the data to write.
    /// </param>
    /// <param name="count">
    /// The number of bytes to write.
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
