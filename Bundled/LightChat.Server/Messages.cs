//
//  Messages.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.IO;
using MCART;
using static System.Text.Encoding;

namespace LightChat
{
    public partial class LightChat
    {
        /// <summary>
        /// Crea un nuevo mensaje de error.
        /// </summary>
        /// <param name="errNum">Número de error.</param>
        /// <param name="msg">Opcional. Mensaje.</param>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] NewErr(ErrCodes errNum, string msg = null)
        {
            List<byte> outp = new List<byte>
                {
                    (byte)RetVal.Err,
                    (byte)errNum
                };
            if (!msg.IsEmpty()) outp.AddRange(Unicode.GetBytes(msg));
            return outp.ToArray();
        }

        /// <summary>
        /// Crea un nuevo mensaje indicando que el servidor ha atendido la
        /// solicitud exitosamente, devolviendo datos.
        /// </summary>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] OkMsg(byte[] data)
        {
            using (var os = new MemoryStream())
            {
                using (var bw = new BinaryWriter(os))
                {
                    bw.Write((byte)RetVal.Ok);
                    bw.Write(data);
                    return os.ToArray();
                }
            }
        }

        /// <summary>
        /// Crea un nuevo mensaje indicando que el servidor ha atendido la
        /// solicitud exitosamente.
        /// </summary>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] OkMsg() => new byte[] { (byte)RetVal.Ok };

        /// <summary>
        /// Crea un nuevo mensaje para el cliente.
        /// </summary>
        /// <param name="msg">Texto del mensaje.</param>
        /// <returns>
        /// Un mensaje compuesto por un arreglo de <see cref="byte"/>.
        /// </returns>
        byte[] NewMsg(string msg)
        {
            using (var os = new MemoryStream())
            {
                using (var bw = new BinaryWriter(os))
                {
                    bw.Write((byte)RetVal.Msg);
                    bw.Write(msg);
                    return os.ToArray();
                }
            }
        }

    }
}
