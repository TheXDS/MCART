//
//  PwEvaluator.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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
using System.Linq;
using System.Security;
using System.Text;
using St = MCART.Resources.Strings;

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
        public PwEvalResult Evaluate(SecureString Pwd)
        {
            if (Pwd.Length == 0) return new PwEvalResult(0, St.Warn(St.PwNeeded), true);
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
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PwEvaluator"/>.
        /// </summary>
        /// <param name="evalRules">Reglas de evaluacióna incluir.</param>
        public PwEvaluator(params PwEvalRule[] evalRules)
        {
            if (evalRules is null) throw new ArgumentNullException(nameof(evalRules));
            Rules = new List<PwEvalRule>(evalRules);
        }
    }
}