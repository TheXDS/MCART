/*
Generators.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using St = TheXDS.MCART.Resources.Strings;
using Ist= TheXDS.MCART.Resources.InternalStrings;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene funciones que generan contraseñas.
    /// </summary>
	public static class Generators
    {
        /// <summary>
        ///     Obtiene un listado de todos los generadores de contraseña disponibles.
        /// </summary>
        public static IEnumerable<KeyValuePair<string, IPasswordGenerator>> List
        {
            get
            {
#if DynamicLoading
                return Misc.Internal.List<IPasswordGenerator>(typeof(Generators));
#else
                yield return new KeyValuePair<string, IPasswordGenerator>(Ist.SafePw, Safe);
                yield return new KeyValuePair<string, IPasswordGenerator>(Ist.ComplexPw, VeryComplex);
                yield return new KeyValuePair<string, IPasswordGenerator>(Ist.PinNumber, Pin);
                yield return new KeyValuePair<string, IPasswordGenerator>(Ist.CryptoPw, ExtremelyComplex);
#endif
            }
        }

        /// <summary>
        /// Genera una contraseña segura de 16 caracteres.
        /// </summary>
        [Name(Ist.SafePw)] public static readonly IPasswordGenerator Safe = new PasswordGenerator(St.Chars, 16);

        /// <summary>
        /// Genera una contraseña muy compleja de 128 caracteres.
        /// </summary>
        [Name(Ist.ComplexPw)] public static readonly IPasswordGenerator VeryComplex = new PasswordGenerator(St.MoreChars, 128);

        /// <summary>
        /// Genera un número de pin de 4 dígitos.
        /// </summary>
        [Name(Ist.PinNumber)] public static readonly IPasswordGenerator Pin = new PasswordGenerator(St.Numbers, 4);

        /// <summary>
        /// Genera una contraseña extremadamente segura, utilizando UTF-16.
        /// </summary>
        [Name(Ist.CryptoPw)] public static readonly IPasswordGenerator ExtremelyComplex = new CryptoPasswordGenerator();
    }
}