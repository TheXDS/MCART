//
//  RuleSets.cs
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

#region Opciones de compilación
// determina si se utilizarán los números nativos de la región configurada en 
// el sistema.
#define NativeDigits
#endregion

using MCART.Attributes;
using St = MCART.Resources.Strings;
using St2 = MCART.Resources.SpecificStrings;

namespace MCART.Security.Password
{
    /// <summary>
    /// Contiene un conjunto de reglas de evaluación de contraseñas.
    /// </summary>
    public static class RuleSets
    {
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="a">Charset contenido.</param>
        /// <param name="b">Nombre de regla.</param>
        /// <param name="c">
        /// Opcional. Descripción. Si se omite, la regla tendrá una descripción
        /// predeterminada.
        /// </param>
        /// <param name="pn">
        /// Opcional. Ponderación. Si se omite, se asume 
        /// <see cref="PonderationLevel.Normal"/>.
        /// </param>
        /// <param name="de">
        /// Opcional. Si se establece en <c>true</c>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        /// <param name="ie">
        /// Opcional. Si se establece en <c>true</c>, la regla se marcará como
        /// puntos adicionales.
        /// </param>
        static PwEvalRule ContentionRuleFactory(string a, string b, string c = null, PonderationLevel pn = PonderationLevel.Normal, bool de = true, bool ie = false)
        {
            // Factor base de contención de caracteres.
            // En teoría:
            // Al aplicar esta regla, ya se gana un 30% de puntaje.
            const float CFactoryFactor = 0.3f;

            return new PwEvalRule(p =>
            {
                int d = 0;
                foreach (char j in a) if (p.Contains(j.ToString())) d++;
                if (d == 0) return new PwEvalResult(0, ie ?
                    St.Ok(St.Include(b.ToLower())) :
                    St.Warn(St.Include(b.ToLower())));
                return new PwEvalResult((CFactoryFactor + ((float)d / p.Length).Clamp(0, 1)));
            }, b, pn, c ?? string.Format(St2.xBuilder, b.ToLower()), de, ie);
        }
        /// <summary>
        /// Establece un conjunto de reglas comunes de complejidad.
        /// </summary>
        /// <returns>
        /// Un arreglo de <see cref="PwEvalRule"/> con métodos comunes de
        /// evaluación de complejidad.
        /// </returns>
        public static PwEvalRule[] CommonComplexityRuleSet()
        {
            return new PwEvalRule[] {
                PwLengthEvalRule(),
                PwUcaseEvalRule(),
                PwLcaseEvalRule(),
                PwNumbersEvalRule(),
                PwSymbolsEvalRule()
            };
        }
        /// <summary>
        /// Crea una regla nula especial para ayudar a balancear los puntajes de evaluación.
        /// </summary>
        /// <returns>Regla de evaluación que devuelve valores constantes.</returns>
        /// <param name="ponderation">Ponderation.</param>
        public static PwEvalRule NullEvalRule(PonderationLevel ponderation = PonderationLevel.AdverseNormal) => new PwEvalRule((p) => new PwEvalResult(1), St2.NullPwEvalRule, ponderation, St2.NullPwEvalRule2);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Regla de evaluación que verifica la longitud de una contraseña.</returns>
        /// <param name="minLength">Minimum length.</param>
        /// <param name="idealLength">Ideal length.</param>
        /// <param name="excessiveLength">Excessive length.</param>
        public static PwEvalRule PwLengthEvalRule(ushort minLength = 8, ushort idealLength = 16, ushort excessiveLength = ushort.MaxValue)
        {
            const float baseScore = 0.6f;
            const float scoringTo1 = 1.0f - baseScore;
            return new PwEvalRule((p) =>
            {
                if (p.Length < minLength) return new PwEvalResult(
                    0, St.Warn(St.PwTooShort), true);
                if (p.Length > idealLength) return new PwEvalResult(
                    1, p.Length > excessiveLength ? St.Warn(St.PwTooLong) : null);
                return new PwEvalResult(
                        baseScore + ((p.Length - minLength) / (idealLength - minLength) * scoringTo1));
            }, St2.PwLenghtEvalRule, PonderationLevel.Normal, string.Format(St2.PwLenghtEvalRule2, minLength, idealLength));
        }
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene mayúsculas.
        /// </summary>
        /// <returns>The ucase eval rule.</returns>
        [Thunk] public static PwEvalRule PwUcaseEvalRule() => ContentionRuleFactory(St.Alpha.ToUpper(), St2.PwUcaseEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene minúsculas.
        /// </summary>
        /// <returns>The ucase eval rule.</returns>
        [Thunk] public static PwEvalRule PwLcaseEvalRule() => ContentionRuleFactory(St.Alpha.ToLower(), St2.PwLcaseEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene números.
        /// </summary>
        /// <returns>The ucase eval rule.</returns>
        [Thunk]
        public static PwEvalRule PwNumbersEvalRule()
        {
            return ContentionRuleFactory(
#if NativeDigits
            System.Globalization.NumberFormatInfo.CurrentInfo.NativeDigits.Condense()
#else
            St.Numbers
#endif
            , St2.PwLcaseEvalRule);
        }
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene símbolos.
        /// </summary>
        /// <returns>The ucase eval rule.</returns>
        [Thunk] public static PwEvalRule PwSymbolsEvalRule() => ContentionRuleFactory(St.Symbols, St2.PwSymbolsEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene caracteres latinos.
        /// </summary>
        /// <returns>The ucase eval rule.</returns>
        [Thunk] public static PwEvalRule PwLatinEvalRule() => ContentionRuleFactory(St.LatinChars, St2.PwLatinEvalRule, null, PonderationLevel.Low, true, true);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene caracteres latinos.
        /// </summary>
        /// <returns>The ucase eval rule.</returns>
        [Thunk] public static PwEvalRule PwOtherSymbsEvalRule() => ContentionRuleFactory(St.MoreSymbs, St2.PwLatinEvalRule, null, PonderationLevel.Normal, true, true);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene otros
        /// caracteres Unicode que no están disponibles en el teclado Inglés
        /// Internacional, y en teoría en ningún otro teclado estándar.
        /// </summary>
        /// <returns>The other UTFE value rule.</returns>
        public static PwEvalRule PwOtherUTFEvalRule()
        {
            return new PwEvalRule((p) =>
            {
                if (p.CountChars(St.MoreChars.ToCharArray()) == 0)
                    return new PwEvalResult(1, St.Ok(St.Includes(St2.PwOtherUTFEvalRule.ToLower())));
                return new PwEvalResult(0);
            }, St2.PwOtherUTFEvalRule, PonderationLevel.High, null, true, true);
        }
    }
}