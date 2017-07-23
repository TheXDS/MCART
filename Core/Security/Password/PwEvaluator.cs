//
//  PwEvaluator.cs
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

using System;
using System.Collections.Generic;
using St = MCART.Resources.Strings;
using System.Text;
using System.Linq;
namespace MCART.Security.Password
{
    /// <summary>
    /// Define un evaluador de contraseñas. Esta clase no puede heredarse.
    /// </summary>
    public sealed class PwEvaluator : IPwEvaluator
    {
        /// <summary>
        /// Obtiene un <see cref="List{PwEvalRule}"/> con las reglas de
        /// evaluación activas en este objeto.
        /// </summary>
        /// <value>The rules.</value>
        public List<PwEvalRule> Rules { get; }
        /// <summary>
        /// Evalúa la contraseña
        /// </summary>
        /// <returns>La evaluación de la contraseña.</returns>
        /// <param name="Pwd">Contraseña a evaluar.</param>
        public PwEvalResult Evaluate(string Pwd)
        {
            if (Pwd.IsEmpty()) return new PwEvalResult(0, St.Warn(St.PwNeeded), true);
            double c = 0;
            int t = 0;
            PwEvalResult k;
            StringBuilder o = new StringBuilder();
            foreach (PwEvalRule j in Rules.Where(a => a.Enable))
            {
                k = j.Eval(Pwd);
                if (!k.Details.IsEmpty()) o.AppendLine(k.Details);
                if (k.Critical) return new PwEvalResult(0, o.ToString(), true);
                c += k.Result;
                if (!j.IsExtraPoints) t += System.Math.Abs((sbyte)j.Ponderation);
            }
            if (t == 0) return new PwEvalResult(0, St.Warn(St.NoRules), true);
            return new PwEvalResult((float)(c / t).Clamp(0, 1), o.ToString());
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MCART.Security.Password.PwEvaluator"/> class.
        /// </summary>
        /// <param name="evalFuncs">Eval funcs.</param>
        public PwEvaluator(params PwEvalRule[] evalFuncs)
        {
            if (evalFuncs.IsNull()) throw new ArgumentNullException(nameof(evalFuncs));
            Rules = new List<PwEvalRule>();
            if (evalFuncs.Any()) Rules.AddRange(evalFuncs);
        }
    }
}