/*
RuleSets.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using TheXDS.MCART.Attributes;
using System.Linq;
using TheXDS.MCART.Math;
using St = TheXDS.MCART.Resources.Strings;
using St2 = TheXDS.MCART.Resources.InternalStrings;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene un conjunto de reglas de evaluación de contraseñas.
    /// </summary>
    public static class RuleSets
    {

        #region Fábrica de reglas de contención

        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName) => ContentionRuleFactory(charset, ruleName, null, PonderationLevel.Normal, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription) => ContentionRuleFactory(charset, ruleName, ruleDescription, PonderationLevel.Normal, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, PonderationLevel ponderation) => ContentionRuleFactory(charset, ruleName, null, ponderation, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, null, PonderationLevel.Normal, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
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
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, bool defaultEnable, bool isExtra) => ContentionRuleFactory(charset, ruleName, null, PonderationLevel.Normal, defaultEnable, isExtra);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, PonderationLevel ponderation) => ContentionRuleFactory(charset, ruleName, ruleDescription, ponderation, true, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, PonderationLevel ponderation, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, ruleDescription, ponderation, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ruleDescription">Descripción de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, ruleDescription, PonderationLevel.Normal, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
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
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, bool defaultEnable, bool isExtra) => ContentionRuleFactory(charset, ruleName, ruleDescription, PonderationLevel.Normal, defaultEnable, isExtra);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
        /// </returns>
        /// <param name="charset">Charset contenido.</param>
        /// <param name="ruleName">Nombre de regla.</param>
        /// <param name="ponderation">Ponderación de la regla.</param>
        /// <param name="defaultEnable">
        /// Si se establece en <see langword="true"/>, la regla se activa de
        /// forma predeterminada.
        /// </param>
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, PonderationLevel ponderation, bool defaultEnable) => ContentionRuleFactory(charset, ruleName, null, ponderation, defaultEnable, false);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
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
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, PonderationLevel ponderation, bool defaultEnable, bool isExtra) => ContentionRuleFactory(charset, ruleName, null, ponderation, defaultEnable, isExtra);
        /// <summary>
        /// Fábrica de reglas de contención de caracteres.
        /// </summary>
        /// <returns>
        /// Un <see cref="PasswordEvaluationRule"/> fabricado con los parámetros deseados.
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
        public static PasswordEvaluationRule ContentionRuleFactory(string charset, string ruleName, string ruleDescription, PonderationLevel ponderation, bool defaultEnable, bool isExtra)
        {
            // Factor base de contención de caracteres.
            // En teoría:
            // Al aplicar esta regla, ya se gana un 30% de puntaje.
            const float CFactoryFactor = 0.3f;

            return new PasswordEvaluationRule(ps =>
            {
                var p = ps.ReadInt16();
                var d = 0;
                foreach (var j in charset) if (p.Contains((short)j)) d++;
                if (d == 0) return new PwEvalResult(0, isExtra ?
                    St.OkX(St.Include(ruleName.ToLower())) :
                    St.Warn(St.Include(ruleName.ToLower())));
                return new PwEvalResult(((CFactoryFactor + (float)d / p.Length).Clamp(0, 1)));
            }, ruleName, ruleDescription ?? string.Format(St2.xBuilder, ruleName.ToLower()), ponderation, defaultEnable, isExtra);
        }

        #endregion

        /// <summary>
        /// Establece un conjunto de reglas comunes de complejidad.
        /// </summary>
        /// <returns>
        /// Un arreglo de <see cref="PasswordEvaluationRule"/> con métodos comunes de
        /// evaluación de complejidad.
        /// </returns>
        public static IEnumerable<PasswordEvaluationRule> CommonComplexityRuleSet()
        {
            return new[] {
                PwLengthEvalRule(),
                PwUcaseEvalRule(),
                PwLcaseEvalRule(),
                PwNumbersEvalRule(),
                PwSymbolsEvalRule(),
            };
        }

        /// <summary>
        /// Establece un conjunto de reglas extendidas de complejidad.
        /// </summary>
        /// <returns>
        /// Un arreglo de <see cref="PasswordEvaluationRule"/> con métodos extendidos de
        /// evaluación de complejidad.
        /// </returns>
        public static IEnumerable<PasswordEvaluationRule> ExtendedRuleSet()
        {
            return CommonComplexityRuleSet().Concat(new[] {
                AvoidCommonPasswords(),
                AvoidYears()
            });
        }

#if SaferPasswords
        /// <summary>
        /// Establece un conjunto de reglas extendidas de complejidad.
        /// </summary>
        /// <returns>
        /// Un arreglo de <see cref="PasswordEvaluationRule"/> con métodos extendidos de
        /// evaluación de complejidad.
        /// </returns>
        public static IEnumerable<PasswordEvaluationRule> ComplexRuleSet()
        {
            return ExtendedRuleSet().Concat(new[] {
                PwLatinEvalRule(),
                PwOtherSymbsEvalRule(),
                PwOtherUtfEvalRule()
            });
        }
#endif
        /// <summary>
        /// Crea una regla nula especial para ayudar a balancear los puntajes de evaluación.
        /// </summary>
        /// <returns>Regla de evaluación que devuelve valores constantes.</returns>
        public static PasswordEvaluationRule NullEvalRule() => NullEvalRule(PonderationLevel.AdverseNormal);

        /// <summary>
        /// Crea una regla nula especial para ayudar a balancear los puntajes de evaluación.
        /// </summary>
        /// <returns>Regla de evaluación que devuelve valores constantes.</returns>
        /// <param name="ponderation">Ponderation.</param>
        public static PasswordEvaluationRule NullEvalRule(PonderationLevel ponderation) => new PasswordEvaluationRule(p => new PwEvalResult(1), St2.NullPwEvalRule, St2.NullPwEvalRule2, ponderation);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que verifica la longitud de una contraseña.</returns>
        public static PasswordEvaluationRule PwLengthEvalRule() => PwLengthEvalRule(8, 16, int.MaxValue);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que verifica la longitud de una contraseña.</returns>
        /// <param name="minLength">Longitud mínima requerida.</param>
        public static PasswordEvaluationRule PwLengthEvalRule(int minLength) => PwLengthEvalRule(minLength, 16, int.MaxValue);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que verifica la longitud de una contraseña.</returns>
        /// <param name="minLength">Longitud mínima requerida.</param>
        /// <param name="idealLength">Longitud ideal de la contraseña.</param>
        public static PasswordEvaluationRule PwLengthEvalRule(int minLength, int idealLength) => PwLengthEvalRule(minLength, idealLength, int.MaxValue);
        /// <summary>
        /// Crea una regla de longitud de contraseña.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que verifica la longitud de una contraseña.</returns>
        /// <param name="minLength">Longitud mínima requerida.</param>
        /// <param name="idealLength">Longitud ideal de la contraseña.</param>
        /// <param name="excessiveLength">
        /// Longitud excesiva. a partir de este punto, se muestra una
        /// advertencia.
        /// </param>
        public static PasswordEvaluationRule PwLengthEvalRule(int minLength, int idealLength, int excessiveLength)
        {
            const float baseScore = 0.6f;
            const float scoringTo1 = 1.0f - baseScore;
            return new PasswordEvaluationRule(p =>
            {
                if (p.Length < minLength) return new PwEvalResult(0, St.Warn(St.PwTooShort), true);
                if (p.Length > idealLength) return new PwEvalResult(1, p.Length > excessiveLength ? St.Warn(St.PwTooLong) : null);
                return new PwEvalResult(baseScore + ((p.Length - minLength) / (idealLength - minLength) * scoringTo1));
            }, St2.PwLenghtEvalRule, string.Format(St2.PwLenghtEvalRule2, minLength, idealLength), PonderationLevel.Normal);
        }
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene mayúsculas.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que comprueba si la contraseña contiene mayúsculas.</returns>
        [Thunk] public static PasswordEvaluationRule PwUcaseEvalRule() => ContentionRuleFactory(St.Alpha.ToUpper(), St2.PwUcaseEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene minúsculas.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que comprueba si la contraseña contiene minúsculas.</returns>
        [Thunk] public static PasswordEvaluationRule PwLcaseEvalRule() => ContentionRuleFactory(St.Alpha.ToLower(), St2.PwLcaseEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene números.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que comprueba si la contraseña contiene números.</returns>
        [Thunk]
        public static PasswordEvaluationRule PwNumbersEvalRule()
        {
            return ContentionRuleFactory(
#if NativeNumbers
            string.Join(null, System.Globalization.NumberFormatInfo.CurrentInfo.NativeDigits)
#else
            St.Numbers
#endif
            , St2.PwNumbersEvalRule);
        }
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene símbolos.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que comprueba si la contraseña contiene símbolos.</returns>
        [Thunk] public static PasswordEvaluationRule PwSymbolsEvalRule() => ContentionRuleFactory(St.Symbols, St2.PwSymbolsEvalRule);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene caracteres latinos.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que comprueba si la contraseña contiene caracteres latinos.</returns>
        [Thunk] public static PasswordEvaluationRule PwLatinEvalRule() => ContentionRuleFactory(St.LatinChars, St2.PwLatinEvalRule, null, PonderationLevel.Low, true, true);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene otros símbolos.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que comprueba si la contraseña contiene otros símbolos.</returns>
        [Thunk] public static PasswordEvaluationRule PwOtherSymbsEvalRule() => ContentionRuleFactory(St.MoreSymbs, St2.PwOtherSymbsEvalRule, null, PonderationLevel.High, true, true);
        /// <summary>
        /// Crea una nueva regla que comprueba si la contraseña contiene otros
        /// caracteres Unicode que no están disponibles en el teclado Inglés
        /// Internacional, y en teoría en ningún otro teclado estándar.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que comprueba si la contraseña contiene otros caracteres Unicode.</returns>
        public static PasswordEvaluationRule PwOtherUtfEvalRule()
        {
            return new PasswordEvaluationRule(p =>
            {
                var charArr = St.MoreChars.ToCharArray();
                return p.ReadInt16().Any(j => !charArr.Contains((char)j)) 
                    ? new PwEvalResult(1, St.OkX(St.Includes(St2.PwOtherUTFEvalRule.ToLower()))) 
                    : new PwEvalResult(0);
            }, St2.PwOtherUTFEvalRule, null, PonderationLevel.Highest, true, true);
        }

        /// <summary>
        ///     Crea una nueva regla que evita el uso de contraseñas comunes conocidas.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que evita el uso de contraseñas comunes conocidas.</returns>
        public static PasswordEvaluationRule AvoidCommonPasswords()
        {
            return new PasswordEvaluationRule(p =>
            {
                return new []
                {
                    "password","passw0rd","123456","1234","admin","12345","12345678","123456789","1234567890","987654321","9876543210",
                    "0987654321","qwerty","qwertyuiop","1q2w3e4r","dragon","pussy","baseball","abc123","football","letmein","none",
                    "master","1111","111111","fuckyou","fuckme","trustn01","trustno1","batman","google","test","pass","6969","access",
                    "666666","0000","00000000","welcome"
                }.Contains(p.Read()) 
                    ? new PwEvalResult(0, St.Warn(St2.AvoidCommonPasswords), true) 
                    : new PwEvalResult(1);
            }, St2.AvoidCommonPasswords, null, PonderationLevel.Normal);
        }

        /// <summary>
        ///     Crea una nueva regla que evita el uso del año actual como parte de la contraseña.
        /// </summary>
        /// <returns>Un <see cref="PasswordEvaluationRule"/> que evita el uso del año actual como parte de la contraseña.</returns>
        public static PasswordEvaluationRule AvoidYears()
        {
            return new PasswordEvaluationRule(p =>
            {
                var t = p.Read();
                return t.EndsWith(DateTime.Today.Year.ToString()) 
                    ? new PwEvalResult(0, St.Warn(St2.AvoidYears), true) 
                    : new PwEvalResult(1);
            }, St2.AvoidYears, null, PonderationLevel.Normal);
        }
    }
}