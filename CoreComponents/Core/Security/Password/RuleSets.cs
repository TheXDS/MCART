/*
RuleSets.cs

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

using TheXDS.MCART.Attributes;
using System.Linq;
using St = TheXDS.MCART.Resources.Strings;
using St2 = TheXDS.MCART.Resources.InternalStrings;

namespace TheXDS.MCART.Security.Password
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
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName) => ContentionRuleFactory(charset, ruleName, null, PonderationLevel.Normal, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription) => ContentionRuleFactory(charset, ruleName, ruleDescription, PonderationLevel.Normal, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, PonderationLevel ponderation) => ContentionRuleFactory(charset, ruleName, null, ponderation, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, null, PonderationLevel.Normal, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, la regla se marcará como
        /// puntos adicionales.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, bool defaultEnable, bool isExtra) => ContentionRuleFactory(charset, ruleName, null, PonderationLevel.Normal, defaultEnable, isExtra);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, PonderationLevel ponderation) => ContentionRuleFactory(charset, ruleName, ruleDescription, ponderation, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, PonderationLevel ponderation, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, ruleDescription, ponderation, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, ruleDescription, PonderationLevel.Normal, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, la regla se marcará como
        /// puntos adicionales.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, bool defaultEnable, bool isExtra) => ContentionRuleFactory(charset, ruleName, ruleDescription, PonderationLevel.Normal, defaultEnable, isExtra);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, PonderationLevel ponderation, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, null, ponderation, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, la regla se marcará como
        /// puntos adicionales.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, PonderationLevel ponderation, bool defaultEnable, bool isExtra) => ContentionRuleFactory(charset, ruleName, null, ponderation, defaultEnable, isExtra);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PwEvalRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        /// <param name="isExtra">
        /// Si se establece en <see langword="true"/>, la regla se marcará como
        /// puntos adicionales.
        /// </param>
        public static PwEvalRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, PonderationLevel ponderation, bool defaultEnable, bool isExtra)
        {
            // Factor base de contención de caracteres.
            // En teoría:
            // Al aplicar esta regla, ya se gana un 30% de puntaje.
            const float CFactoryFactor = 0.3f;

            return new PwEvalRule(ps =>
            {
                short[] p = ps.ReadInt16();
                int d = 0;
                foreach (char j in charset) if (p.Contains((short)j)) d++;
                if (d == 0) return new PwEvalResult(0, isExtra ?
                    St.OkX(St.Include(ruleName.ToLower())) :
                    St.Warn(St.Include(ruleName.ToLower())));
                return new PwEvalResult(((CFactoryFactor + (float)d / p.Length).Clamp(0, 1)));
            }, ruleName, ruleDescription ?? string.Format(St2.xBuilder, ruleName.ToLower()), ponderation, defaultEnable, isExtra);
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
        public static PwEvalRule NullEvalRule() => NullEvalRule(PonderationLevel.AdverseNormal);
        /// <summary>
        /// Crea una regla nula especial para ayudar a balancear los puntajes de evaluación.
        /// </summary>
        /// <returns>Regla de evaluación que devuelve valores constantes.</returns>
        /// <param name="ponderation">Ponderation.</param>
        public static PwEvalRule NullEvalRule(PonderationLevel ponderation) => new PwEvalRule((p) => new PwEvalResult(1), St2.NullPwEvalRule, St2.NullPwEvalRule2, ponderation);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que verifica la longitud de una contraseña.</returns>
        public static PwEvalRule PwLengthEvalRule() => PwLengthEvalRule(8, 16, int.MaxValue);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que verifica la longitud de una contraseña.</returns>
        /// <param name="minLength">Longitud mínima requerida.</param>
        public static PwEvalRule PwLengthEvalRule(int minLength) => PwLengthEvalRule(minLength, 16, int.MaxValue);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que verifica la longitud de una contraseña.</returns>
        /// <param name="minLength">Longitud mínima requerida.</param>
        /// <param name="idealLength">Longitud ideal de la contraseña.</param>
        public static PwEvalRule PwLengthEvalRule(int minLength, int idealLength) => PwLengthEvalRule(minLength, idealLength, int.MaxValue);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que verifica la longitud de una contraseña.</returns>
        /// <param name="minLength">Longitud mínima requerida.</param>
        /// <param name="idealLength">Longitud ideal de la contraseña.</param>
        /// <param name="excessiveLength">
        /// Longitud excesiva. a partir de este punto, se muestra una
        /// advertencia.
        /// </param>
        public static PwEvalRule PwLengthEvalRule(int minLength, int idealLength, int excessiveLength)
        {
            const float baseScore = 0.6f;
            const float scoringTo1 = 1.0f - baseScore;
            return new PwEvalRule((p) =>
            {
                if (p.Length < minLength) return new PwEvalResult(0, St.Warn(St.PwTooShort), true);
                if (p.Length > idealLength) return new PwEvalResult(1, p.Length > excessiveLength ? St.Warn(St.PwTooLong) : null);
                return new PwEvalResult(baseScore + ((p.Length - minLength) / (idealLength - minLength) * scoringTo1));
            }, St2.PwLenghtEvalRule, string.Format(St2.PwLenghtEvalRule2, minLength, idealLength), PonderationLevel.Normal);
        }
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene mayúsculas.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que comprueba si la contraseña contiene mayúsculas.</returns>
        [Thunk] public static PwEvalRule PwUcaseEvalRule() => ContentionRuleFactory(St.Alpha.ToUpper(), St2.PwUcaseEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene minúsculas.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que comprueba si la contraseña contiene minúsculas.</returns>
        [Thunk] public static PwEvalRule PwLcaseEvalRule() => ContentionRuleFactory(St.Alpha.ToLower(), St2.PwLcaseEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene números.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que comprueba si la contraseña contiene números.</returns>
        [Thunk]
        public static PwEvalRule PwNumbersEvalRule()
        {
            return ContentionRuleFactory(
#if NativeNumbers
            System.Globalization.NumberFormatInfo.CurrentInfo.NativeDigits.Condense()
#else
            St.Numbers
#endif
            , St2.PwNumbersEvalRule);
        }
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene símbolos.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que comprueba si la contraseña contiene símbolos.</returns>
        [Thunk] public static PwEvalRule PwSymbolsEvalRule() => ContentionRuleFactory(St.Symbols, St2.PwSymbolsEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene caracteres latinos.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que comprueba si la contraseña contiene caracteres latinos.</returns>
        [Thunk] public static PwEvalRule PwLatinEvalRule() => ContentionRuleFactory(St.LatinChars, St2.PwLatinEvalRule, null, PonderationLevel.Low, true, true);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene otros símbolos.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que comprueba si la contraseña contiene otros símbolos.</returns>
        [Thunk] public static PwEvalRule PwOtherSymbsEvalRule() => ContentionRuleFactory(St.MoreSymbs, St2.PwOtherSymbsEvalRule, null, PonderationLevel.High, true, true);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene otros
        /// caracteres Unicode que no están disponibles en el teclado Inglés
        /// Internacional, y en teoría en ningún otro teclado estándar.
        /// </summary>
        /// <returns>Un <see cref="PwEvalRule"/> que comprueba si la contraseña contiene otros caracteres Unicode.</returns>
        public static PwEvalRule PwOtherUTFEvalRule()
        {
            return new PwEvalRule((p) =>
            {
                var charArr = St.MoreChars.ToCharArray();
                foreach (var j in p.ReadInt16())
                    if (!charArr.Contains((char)j))
                        return new PwEvalResult(1, St.OkX(St.Includes(St2.PwOtherUTFEvalRule.ToLower())));
                return new PwEvalResult(0);
            }, St2.PwOtherUTFEvalRule, null, PonderationLevel.Highest, true, true);
        }
    }
}