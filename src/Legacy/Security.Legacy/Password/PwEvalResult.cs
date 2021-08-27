/*
PwEvalResult.cs

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

using TheXDS.MCART.Security.Legacy.Resources;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene información sobre el resultado de la evaluación de una
    /// contraseña.
    /// </summary>
    public struct PwEvalResult
    {
        /// <summary>
        /// Resultado de la evaluación de la contraseña
        /// </summary>
        public float Result;

        /// <summary>
        /// Si es <see langword="true"/>, la contraseña no continuará siendo evaluada, ya
        /// que es inválida.
        /// </summary>
        public bool Critical;

        /// <summary>
        /// Detalles que el <see cref="PasswordEvaluator"/> ha colocado sobre la
        /// evaluación de la contraseña.
        /// </summary>
        public string? Details;

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="PwEvalResult"/>.
        /// </summary>
        /// <param name="r">Resultado de la evaluación.</param>
        public PwEvalResult(float r) : this(r, null, false) { }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="PwEvalResult"/>.
        /// </summary>
        /// <param name="r">Resultado de la evaluación.</param>
        /// <param name="d">Detalles de la evaluación.</param>
        public PwEvalResult(float r, string? d) : this(r, d, false) { }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="PwEvalResult"/>.
        /// </summary>
        /// <param name="r">Resultado de la evaluación.</param>
        /// <param name="d">Detalles de la evaluación.</param>
        /// <param name="c">
        /// Criticalidad de la evaluación. Si se establece en <see langword="true"/>, la
        /// contraseña no seguirá siendo evaluada, ya que es inválida.
        /// </param>
        public PwEvalResult(float r, string? d, bool c)
        {
            Result = r;
            Critical = c;
            Details = d;
        }

        /// <summary>
        /// Obtiene un <see cref="PwEvalResult"/> nulo. Este campo es de solo 
        /// lectura.
        /// </summary>
        public static readonly PwEvalResult Null = new(float.NaN);

        /// <summary>
        /// Obtiene un <see cref="PwEvalResult"/> que representa una
        /// contraseña vacía. Este campo es de solo lectura.
        /// </summary>
        public static readonly PwEvalResult Empty = new(float.NaN, Strings.EmptyPassword);
        
        /// <summary>
        /// Obtiene un <see cref="PwEvalResult"/> fallido. Este campo es de
        /// sólo lectura.
        /// </summary>
        public static readonly PwEvalResult Fail = new(0, null, true);

        /// <summary>
        /// Evalúa si esta instancia y <paramref name="obj"/> son 
        /// iguales.
        /// </summary>
        /// <param name="obj">Valor a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si ambos objetos son iguales, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public override bool Equals(object? obj) => base.Equals(obj);

        /// <summary>
        /// Devuelve el código Hash para esta instancia.
        /// </summary>
        /// <returns>El código Hash que representa a esta instancia.</returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Evalúa si <paramref name="left"/> y <paramref name="right"/> son 
        /// iguales.
        /// </summary>
        /// <param name="left">Valor a comparar.</param>
        /// <param name="right">Valor a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si ambos objetos son iguales, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public static bool operator ==(PwEvalResult left, PwEvalResult right) => (left.Result == right.Result) && left.Critical == right.Critical;

        /// <summary>
        /// Evalúa si <paramref name="left"/> y <paramref name="right"/> son 
        /// diferentes.
        /// </summary>
        /// <param name="left">Valor a comparar.</param>
        /// <param name="right">Valor a comparar.</param>
        /// <returns>
        /// <see langword="true"/> si los objetos son diferentes, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public static bool operator !=(PwEvalResult left, PwEvalResult right) => !(left == right);
    }
}