//
//  IEasyCrypto.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace MCART.Security.Encryption
{
    /// <summary>
    /// Describe lso métodos básicos para implementar un mecanismo simple de
    /// encriptado.
    /// </summary>
    [Attributes.Unsecure]
    public interface IEasyCrypto
    {
        /// <summary>
        /// Encripta el texto especificado.
        /// </summary>
        /// <returns>
        /// Un arreglo de <see cref="byte"/> que corresponde a 
        /// <paramref name="txt"/> luego de ser encriptado.
        /// </returns>
        /// <param name="txt">Texto a encriptar.</param>
        byte[] Encrypt(string txt);
        /// <summary>
        /// Descifra el mensaje encriptado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="string"/> que corresponde al resultado de descifrar
        /// <paramref name="enc"/>.</returns>
        /// <param name="enc">Bytes de información encriptada.</param>
        string Decrypt(byte[] enc);
        /// <summary>
        /// Inicializa la instancia del motor de encriptado utilizando la
        /// contraseña y el vector de inicialización especificados.
        /// </summary>
        /// <param name="pwd">Contraseña a utilizar.</param>
        /// <param name="IV">Vector de inicialización a utilizar.</param>
        void Init(string pwd, byte[] IV);
    }
}