/*
PasswordStorage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     Taylor Hornby (Original implementation)
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene métodos para crear Salty Hashes de contraseñas que son
    /// seguros para ser almacenados, así como también comprobar la validez
    /// de una contraseña.
    /// </summary>
    /// <remarks>
    /// Implementación de referencia de algunos componentes de autoría
    /// original por Taylor Hornby bajo licencia MIT.
    /// </remarks>
    [ThirdParty, SpdxLicense(Resources.SpdxLicenseId.MIT)]
    public static class PasswordStorage
    {
        /* Cambios respecto a la implementación de referencia:
         * - Remoción total de excepciones.
         * - Remoción de campo descriptivo de algoritmo (C# sólo admite SHA1 al
         *   utilizar PBKDF2
         * - Devolución de datos como arreglo de bytes
         * - Uso de SecureString
         * - Simplificación de datos innecesarios
        */

        #region Constantes

        private const short _saltBytes = 24;
        private const short _hashBytes = 18;
        private const int _pbkdf2Iterations = 64000;

        #endregion

        private static byte[] Pbkdf2(SecureString password, byte[] salt, int iterations, int outputBytes)
        {
            using (password)
            using (var pbkdf2 = new Rfc2898DeriveBytes(password.ReadBytes(), salt, iterations))
                return pbkdf2.GetBytes(outputBytes);
        }

        private static bool CheckEquals(IReadOnlyList<byte> a, IReadOnlyList<byte> b)
        {
#if SaferPasswords
            // Este método realiza una comprobación lenta intencionalmente.
            var diff = (uint)a.Count ^ (uint)b.Count;
            for (var i = 0; i < a.Count && i < b.Count; i++)
                diff |= (uint)(a[i] ^ b[i]);
            
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
            var salt = new byte[_saltBytes];
            using (var csprng = new RNGCryptoServiceProvider())
                csprng.GetBytes(salt);

            var hash = Pbkdf2(password, salt, _pbkdf2Iterations, _hashBytes);

            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            bw.Write(_pbkdf2Iterations);
            bw.Write((short)salt.Length);
            bw.Write(salt);
            bw.Write((short)hash.Length);
            bw.Write(hash);
            return ms.ToArray();
        }

        /// <summary>
        /// Verifica una contraseña.
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
                using var ms = new MemoryStream(goodHash);
                using var br = new BinaryReader(ms);
                var iterations = br.ReadInt32();
                var salt = br.ReadBytes(br.ReadInt16());
                var hash = br.ReadBytes(br.ReadInt16());

                var testHash = Pbkdf2(password, salt, iterations, hash.Length);
                return CheckEquals(hash, testHash);
#if !PreferExceptions
            }
            catch { return null; }
#else
            }
            catch { throw; }
#endif
        }

        /// <summary>
        /// Verifica una contraseña de forma asíncrona.
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
        [Sugar] public static Task<bool?> VerifyPasswordAsync(SecureString password, byte[] goodHash) => Task.Run(() => VerifyPassword(password, goodHash));
    }
}