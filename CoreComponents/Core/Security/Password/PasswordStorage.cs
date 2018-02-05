/*
PasswordStorage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     Taylor Hornby (Original implementation)
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene métodos para crear Salty Hashes de contraseñas que son seguros
    /// para ser almacenados, así com otambién comprobar la validez de una
    /// contraseña.
    /// </summary>
    /// <remarks>
    /// Implementación de referencia original por Taylor Hornby.
    /// </remarks>
    public static class PasswordStorage
    {
        /* Cambios:
         * - Remoción total de excepciones.
         * - Remoción de campo descriptivo de algoritmo (C# sólo admite SHA1 al
         *   utilizar PBKDF2
         * - Devolución de datos como arreglo de bytes
         * - Uso de SecureString
         * - Simplificación de datos innecesarios
        */

        #region Constantes
        const short SALT_BYTES = 24;
        const short HASH_BYTES = 18;
        const int PBKDF2_ITERATIONS = 64000;
        #endregion

        static byte[] PBKDF2(SecureString password, byte[] salt, int iterations, int outputBytes)
        {
            using (password)
            using (var pbkdf2 = new Rfc2898DeriveBytes(password.ReadBytes(), salt, iterations))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }
        static bool CheckEquals(byte[] a, byte[] b)
        {
#if SaferPasswords
            // Este método realiza una comprobación lenta intencionalmente.
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
#else
            return System.Linq.Enumerable.SequenceEqual(a, b);
#endif
        }

        /// <summary>
        /// Crea un Salty Hash seguro para almacenar la contraseña.
        /// </summary>
        /// <param name="password">Contraseña a almacenar.</param>
        /// <returns></returns>
        public static byte[] CreateHash(SecureString password)
        {
            byte[] salt = new byte[SALT_BYTES];
            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(salt);
            }
            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);

            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(PBKDF2_ITERATIONS);
                bw.Write(salt.Length);
                bw.Write(salt);
                bw.Write(hash.Length);
                bw.Write(hash);
                return ms.ToArray();
            }
        }

#if PreferExceptions
        /// <summary>
        /// Verifica la contraseña.
        /// </summary>
        /// <param name="password">
        /// Contraseña a verificar.
        /// </param>
        /// <param name="goodHash">
        /// Hash+Salt contra el cual comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la contraseña es válida,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        public static bool? VerifyPassword(SecureString password, byte[] goodHash)
        {
#else
        /// <summary>
        /// Verifica la contraseña.
        /// </summary>
        /// <param name="password">
        /// Contraseña a verificar.
        /// </param>
        /// <param name="goodHash">
        /// Hash+Salt contra el cual comparar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la contraseña es válida,
        /// <see langword="false"/> en caso contrario, o
        /// <see langword="null"/> si hay un problema al verificar la
        /// contraseña, como ser, debido a tampering.
        /// </returns>
        public static bool? VerifyPassword(SecureString password, byte[] goodHash)
        {
            try
            {
#endif
                using (var ms = new MemoryStream(goodHash))
                using (var br = new BinaryReader(ms))
                {
                    int iterations = br.ReadInt32();
                    byte[] salt = br.ReadBytes(br.ReadInt16());
                    byte[] hash = br.ReadBytes(br.ReadInt16());

                    byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
                    return CheckEquals(hash, testHash);
                }
#if !PreferExceptions
            }
            catch { return null; }
#endif
        }
    }
}