/*
PwEvaluator.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Linq;
using System.Security;
using System.Text;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Security.Password
{
    /// <inheritdoc />
    /// <summary>
    ///     Define un evaluador de contraseñas. Esta clase no puede heredarse.
    /// </summary>
    public sealed class PasswordEvaluator : IPasswordEvaluator
    {
        /// <inheritdoc />
        /// <summary>
        /// Obtiene un <see cref="List{T}" /> con las reglas de
        /// evaluación activas en este objeto.
        /// </summary>
        /// <value>Reglas de evaluación a incluir.</value>
        public HashSet<PasswordEvaluationRule> Rules { get; }

        /// <inheritdoc />
        /// <summary>
        /// Evalúa la contraseña
        /// </summary>
        /// <returns>La evaluación de la contraseña.</returns>
        /// <param name="password">Contraseña a evaluar.</param>
        public PwEvalResult Evaluate(SecureString password)
        {
            if (password.Length == 0) return new PwEvalResult(0, St.Warn(St.PwNeeded), true);
            double c = 0;
            var t = 0;
            var o = new StringBuilder();
            foreach (var j in Rules.Where(a => a.Enable))
            {
                var k = j.Eval(password);
                if (!k.Details.IsEmpty()) o.AppendLine(k.Details);
                if (k.Critical) return new PwEvalResult(0, o.ToString(), true);
                c += k.Result;
                if (!j.IsExtraPoints) t += System.Math.Abs((sbyte)j.Ponderation);
            }
            return t == 0 
                ? new PwEvalResult(0, St.Warn(St.NoRules), true) 
                : new PwEvalResult((float)(c / t).Clamp(0, 1), o.ToString());
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:TheXDS.MCART.Security.Password.PasswordEvaluator" />.
        /// </summary>
        /// <param name="evalRules">Reglas de evaluación a incluir.</param>
        public PasswordEvaluator(params PasswordEvaluationRule[] evalRules)
        {
            if (!evalRules?.Any() ?? true) throw new ArgumentNullException(nameof(evalRules));
            Rules = new HashSet<PasswordEvaluationRule>(evalRules!.NotNull());
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:TheXDS.MCART.Security.Password.PasswordEvaluator" />.
        /// </summary>
        /// <param name="evalRules">Reglas de evaluación a incluir.</param>
        public PasswordEvaluator(IEnumerable<PasswordEvaluationRule> evalRules):this(evalRules.ToArray())
        {
        }
    }
}