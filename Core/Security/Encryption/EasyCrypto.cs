//
//  EasyCrypto.cs
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

using System;
using MCART.Attributes;
using System.Security.Cryptography;
using System.IO;

namespace MCART.Security.Encryption
{
    /// <summary>
    /// Permite una implementación más sencilla de un
    /// <see cref="SymmetricAlgorithm"/> evitando algunas capas de abstracción.
    /// </summary>
    [Beta]
    [Unsecure]
    public class EasyCrypto : IEasyCrypto
    {
        bool init;
        readonly SymmetricAlgorithm man;
        byte[] p;
        byte[] i;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EasyCrypto"/>.
        /// </summary>
        /// <param name="algorithm">
        /// <see cref="SymmetricAlgorithm"/> a utilizar.
        /// </param>
        public EasyCrypto(SymmetricAlgorithm algorithm) { man = algorithm; }
        /// <summary>
        /// Inicializa esta instancia de la clase <see cref="EasyCrypto"/>.
        /// </summary>
        /// <param name="Pwd">Contraseña a utilizar.</param>
        /// <param name="IV">Vector de inicialización a utilizar.</param>
        public void Init(string Pwd, byte[] IV)
        {
            p = System.Text.Encoding.Unicode.GetBytes(Pwd);
            i = IV;
            init = true;
        }
        /// <summary>
        /// Descrifra un arreglo de <see cref="byte"/>.
        /// </summary>
        /// <param name="CryptBytes">Bytes a descrifrar.</param>
        /// <returns>Una cadena con el texto descifrado.</returns>
        public string Decrypt(byte[] CryptBytes)
        {
            if (!init) throw new InvalidOperationException();
            if (CryptBytes.Length == 0) return null;
            using (ICryptoTransform ct = man.CreateDecryptor(p, i))
            {
                using (MemoryStream msDecrypt = new MemoryStream(CryptBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, ct, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            try { return srDecrypt.ReadToEnd(); }
                            catch { throw new Exceptions.InvalidPasswordException(); }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Encripta una cadena.
        /// </summary>
        /// <param name="TextToEncrypt">Cadena a encriptar.</param>
        /// <returns>Un arreglo de <see cref="byte"/> con el mensaje cifrado.</returns>
        public byte[] Encrypt(string TextToEncrypt)
        {
            if (!init) throw new InvalidOperationException();
            using (ICryptoTransform encryptor = man.CreateEncryptor(p, i))
            {
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(TextToEncrypt);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }
    }
}