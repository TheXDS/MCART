/*
RSACryptoStream.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using TheXDS.MCART.Types.Extensions;
using System.Linq;

namespace TheXDS.MCART.Security.Cryptography
{
    /// <summary>
    /// Implementa un flujo que lee y escribe información encriptada en RSA
    /// sobre un <see cref="Stream"/> especificado.
    /// </summary>
    public class RSACryptoStream : Stream, IDisposable
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
            var a = ReadToEnd().Take(count).ToArray();
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
            if (!_stream.CanRead || _rsa.PublicOnly) throw new NotSupportedException();

            byte[] a;
            if (_stream.CanSeek)
            {
                var sze = (int) _stream.RemainingBytes();
                a = new byte[sze];
                _stream.Read(a, 0, sze);
            }
            else
            {
                var l = new List<byte>();
                int r;
                while(true)
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
            if (offset + count > buffer.Length) throw new ArgumentException();
            if (buffer is null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (!CanWrite) throw new NotSupportedException();
            var b = buffer.Range(offset, count).ToArray();
            if (!b.Any()) return;
            var eb = _rsa.Encrypt(b, true);
            _stream.Write(eb, 0, eb.Length);
        }
    }
}