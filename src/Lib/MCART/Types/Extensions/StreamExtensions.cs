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
/// Extensions for the <see cref="Stream"/> class.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Destroys the content of the <see cref="Stream"/>.
    /// </summary>
    /// <param name="fs">
    /// The <see cref="Stream"/> to destroy.
    /// </param>
    /// <exception cref="IOException">
    /// Thrown if an I/O error occurs during the operation.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Thrown if the <see cref="Stream"/> does not support writing and seeking operations,
    /// such as when it's constructed from a pipe or console stream.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Occurs when this operation is attempted on an already disposed <see cref="Stream"/>.
    /// </exception>
    [DebuggerStepThrough]
    [Sugar]
    public static void Destroy(this Stream fs) => fs.SetLength(0);

    /// <summary>
    /// Skips the specified number of bytes in the sequence from the current position.
    /// </summary>
    /// <param name="fs">
    /// The <see cref="Stream"/> where the read cursor will be skipped.
    /// </param>
    /// <param name="bytesToSkip">Bytes to skip.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="bytesToSkip"/> is less than zero, or
    /// if skipping extends the cursor beyond the sequence.
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
    /// Reads a string from the sequence and advances the read position past the last character read.
    /// </summary>
    /// <returns>The string that has been read.</returns>
    /// <param name="fs">
    /// The <see cref="Stream"/> to read the string from.
    /// </param>
    /// <param name="count">Number of characters to read.</param>
    [DebuggerStepThrough]
    [Sugar]
    public static string ReadString(this Stream fs, int count) => ReadString(fs, count, Encoding.Default);

    /// <summary>
    /// Reads a string from the sequence and advances the read position past the last character read.
    /// </summary>
    /// <returns>The string that has been read.</returns>
    /// <param name="fs">
    /// The <see cref="Stream"/> to read the string from.
    /// </param>
    /// <param name="count">Number of characters to read.</param>
    /// <param name="encoding"><see cref="Encoding"/> to use.</param>
    [DebuggerStepThrough]
    public static string ReadString(this Stream fs, int count, Encoding encoding)
    {
        List<char>? retVal = [];
        using BinaryReader? br = new(fs, encoding, true);
        while (retVal.Count < count) retVal.Add(br.ReadChar());
        return new string([.. retVal]);
    }

    /// <summary>
    /// Gets the number of remaining bytes from the current position.
    /// </summary>
    /// <param name="fs">
    /// The <see cref="Stream"/> to get the remaining bytes from.
    /// </param>
    /// <returns>
    /// The number of remaining bytes from the current position.
    /// </returns>
    [DebuggerStepThrough]
    [Sugar] public static long RemainingBytes(this Stream fs) => fs.Length - fs.Position;

    /// <summary>
    /// Asynchronously reads a string from the sequence and advances the read position past the last Unicode character read.
    /// </summary>
    /// <returns>The string that has been read.</returns>
    /// <param name="fs">
    /// The <see cref="Stream"/> to read the string from.
    /// </param>
    /// <param name="count">Number of characters to read.</param>
    [DebuggerStepThrough]
    [Sugar] public static Task<string> ReadStringAsync(this Stream fs, int count) => Task.Run(() => ReadString(fs, count));

    /// <summary>
    /// Asynchronously reads a string from the sequence and advances the read position past the last character read.
    /// </summary>
    /// <returns>The string that has been read.</returns>
    /// <param name="fs">
    /// The <see cref="Stream"/> to read the string from.
    /// </param>
    /// <param name="count">Number of characters to read.</param>
    /// <param name="encoding"><see cref="Encoding"/> to use.</param>
    [DebuggerStepThrough]
    public static Task<string> ReadStringAsync(this Stream fs, int count, Encoding encoding) => Task.Run(() => ReadString(fs, count, encoding));

    /// <summary>
    /// Asynchronously reads a string from the current position until the end of the sequence.
    /// </summary>
    /// <param name="fs">
    /// The <see cref="Stream"/> to read the string from.
    /// </param>
    /// <returns>The string that has been read.</returns>
    [DebuggerStepThrough]
    [Sugar] public static Task<string> ReadStringToEndAsync(this Stream fs) => ReadStringToAsync(fs, fs.Length);

    /// <summary>
    /// Asynchronously reads a string from the current position until reaching the specified position.
    /// </summary>
    /// <param name="fs">
    /// The <see cref="Stream"/> to read the string from.
    /// </param>
    /// <param name="pos">
    /// Position up to which the string will be read.
    /// </param>
    /// <returns>The string that has been read.</returns>
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
    /// Writes a set of <see cref="byte"/> to the <see cref="Stream"/>.
    /// </summary>
    /// <param name="fs">
    /// The <see cref="Stream"/> to write bytes on.
    /// </param>
    /// <param name="bytes">
    /// Collection of <see cref="byte"/> to write in the <see cref="Stream"/>.
    /// </param>
    [DebuggerStepThrough]
    [Sugar] public static void WriteBytes(this Stream fs, params byte[] bytes) => fs.Write(bytes, 0, bytes.Length);

    /// <summary>
    /// Writes a set of sequences of <see cref="byte"/> to the <see cref="Stream"/>.
    /// </summary>
    /// <param name="fs">
    /// The <see cref="Stream"/> to write bytes on.
    /// </param>
    /// <param name="bytes">
    /// Collections of <see cref="byte"/> to write in the <see cref="Stream"/>.
    /// </param>
    [DebuggerStepThrough]
    public static void WriteSeveralBytes(this Stream fs, params byte[][] bytes)
    {
        foreach (byte[]? x in bytes) fs.Write(x, 0, x.Length);
    }
}
