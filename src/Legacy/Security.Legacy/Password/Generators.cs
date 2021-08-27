/*
Generators.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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
using TheXDS.MCART.Types;
using static TheXDS.MCART.Security.Legacy.Resources.Strings;
using static TheXDS.MCART.Resources.Strings.Constants;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene funciones que generan contraseñas.
    /// </summary>
	public static class Generators
    {
        /// <summary>
        /// Obtiene un listado de todos los generadores de contraseña disponibles.
        /// </summary>
        public static IEnumerable<NamedObject<IPasswordGenerator>> List
        {
            get
            {
#if DynamicLoading
                return Misc.PrivateInternals.List<IPasswordGenerator>(typeof(Generators));
#else
                yield return new KeyValuePair<string, IPasswordGenerator>(PwGenSafe, Safe);
                yield return new KeyValuePair<string, IPasswordGenerator>(PwGenComplex, VeryComplex);
                yield return new KeyValuePair<string, IPasswordGenerator>(PwGenPin, Pin);
                yield return new KeyValuePair<string, IPasswordGenerator>(PwGenCrypto, ExtremelyComplex);
#endif
            }
        }

        /// <summary>
        /// Genera una contraseña segura de 16 caracteres.
        /// </summary>
        public static readonly IPasswordGenerator Safe = new PasswordGenerator(Chars, 16);

        /// <summary>
        /// Genera una contraseña muy compleja de 128 caracteres.
        /// </summary>
        public static readonly IPasswordGenerator VeryComplex = new PasswordGenerator(MoreChars, 128);

        /// <summary>
        /// Genera un número de pin de 4 dígitos.
        /// </summary>
        public static readonly IPasswordGenerator Pin = new PasswordGenerator(Numbers, 4);

        /// <summary>
        /// Genera una contraseña extremadamente segura, utilizando UTF-16.
        /// </summary>
        public static readonly IPasswordGenerator ExtremelyComplex = new CryptoPasswordGenerator();
    }
}