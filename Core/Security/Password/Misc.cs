//
//  Misc.cs
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

namespace MCART.Security.Password
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
        /// Si es <c>true</c>, la contraseña no continuará siendo evaluada, ya
        /// que es inválida.
        /// </summary>
        public bool Critical;
        /// <summary>
        /// Detalles que el <see cref="PwEvaluator"/> ha colocado sobre la
        /// evaluación de la contraseña.
        /// </summary>
        public string Details;
        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="PwEvalResult"/>.
        /// </summary>
        /// <param name="r">Resultado de la evaluación.</param>
        /// <param name="d">Detalles de la evaluación.</param>
        /// <param name="c">
        /// Criticalidad de la evaluación. Si se establece en <c>true</c>, la
        /// contraseña no seguirá siendo evaluada, ya que es inválida.
        /// </param>
        public PwEvalResult(float r, string d = null, bool c = false)
        {
            Result = r;
            Critical = c;
            Details = d;
        }
        /// <summary>
        /// Obtiene un <see cref="PwEvalResult"/> nulo. Este campo es de sólo 
        /// lectura.
        /// </summary>
        public static readonly PwEvalResult Null = new PwEvalResult();
    }
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
	/// Delegado que define una función que genera contraseñas.
	/// </summary>
	public delegate string PwGenerator(int length);
    /// <summary>
    /// Delegado que define una función que evalúa contraseñas.
    /// </summary>
    public delegate PwEvalResult PwEvalFunc(string pwToEval);
}