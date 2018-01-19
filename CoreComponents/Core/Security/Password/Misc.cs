/*
Misc.cs

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

using System.Security;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Determina el nivel de ponderación a aplicar a un objeto <see cref="PwEvalRule"/>
    /// </summary>
    public enum PonderationLevel : sbyte
	{
		/// <summary>
		/// Puntuación adversa más baja.
		/// </summary>
		AdverseLowest = -1,
		/// <summary>
		/// Puntuación adversa muy baja.
		/// </summary>
		AdverseLower = -2,
		/// <summary>
		/// Puntuación adversa baja
		/// </summary>
		AdverseLow = -3,
		/// <summary>
		/// Puntuación adversa normal.
		/// </summary>
		AdverseNormal = -4,
		/// <summary>
		/// Puntuación adversa alta.
		/// </summary>
		AdverseHigh = -5,
		/// <summary>
		/// Puntuación adversa muy alta.
		/// </summary>
		AdverseHigher = -6,
		/// <summary>
		/// Puntuación adversa más alta.
		/// </summary>
		AdverseHighest = -7,
		/// <summary>
		/// puntuación sin valor.
		/// </summary>
		None = 0,
		/// <summary>
		/// Puntuación más baja.
		/// </summary>
		Lowest = 1,
		/// <summary>
		/// Puntuación muy baja.
		/// </summary>
		Lower = 2,
		/// <summary>
		/// Puntuación baja
		/// </summary>
		Low = 3,
		/// <summary>
		/// Puntuación normal.
		/// </summary>
		Normal = 4,
		/// <summary>
		/// Puntuación alta.
		/// </summary>
		High = 5,
		/// <summary>
		/// Puntuación muy alta.
		/// </summary>
		Higher = 6,
		/// <summary>
		/// Puntuación más alta.
		/// </summary>
		Highest = 7
	}
    /// <summary>
    /// Delegado que permite verificar una contraseña.
    /// </summary>
    /// <param name="user">Usuario a verificar.</param>
    /// <param name="password">Contraseña a verificar.</param>
    /// <returns>
    /// <see langword="true"/> si la información de inicio de sesión es válida, 
    /// <see langword="false"/> en caso contrario.</returns>
    public delegate System.Threading.Tasks.Task<bool> LoginValidator(string user, SecureString password);
	/// <summary>
	/// Delegado que define una función que genera contraseñas.
	/// </summary>
	public delegate SecureString PwGenerator(int length);
	/// <summary>
	/// Delegado que define una función que evalúa contraseñas.
	/// </summary>
	public delegate PwEvalResult PwEvalFunc(SecureString pwToEval);
}