/*
RuleSets.cs

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

using System.Collections.Generic;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using St2 = TheXDS.MCART.Resources.InternalStrings;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Colección de evaluadores de contraseña definidos.
    /// </summary>
    public static class PasswordEvaluators
    {
        /// <summary>
        /// Lista los evaluadores de contraseña integrados en esta clase.
        /// </summary>
        public static IEnumerable<NamedObject<IPasswordEvaluator>> List
        {
            get
            {
#if DynamicLoading
                return Misc.PrivateInternals.List<IPasswordEvaluator>(typeof(PasswordEvaluators));
#else
                yield return new KeyValuePair<string, IPasswordEvaluator>(St2.SafePw, CommonEvaluator);
                yield return new KeyValuePair<string, IPasswordEvaluator>(St2.SafePw, SaferEvaluator);
#if SaferPasswords
                yield return new KeyValuePair<string, IPasswordEvaluator>(St2.SafePw, ComplexEvaluator);
#endif
#endif
            }
        }

        /// <summary>
        /// Obtiene un evaluador de contraseñas con reglas comunes.
        /// </summary>
        [Name(St2.CommonEvaluator), Description(St2.CommonEvaluatorDesc)]
        public static readonly IPasswordEvaluator CommonEvaluator = new PasswordEvaluator(RuleSets.CommonComplexityRuleSet());

        /// <summary>
        /// Obtiene un evaluador de contraseñas con reglas seguras.
        /// </summary>
        [Name(St2.ExtendedEvaluator)]
        public static readonly IPasswordEvaluator SaferEvaluator = new PasswordEvaluator(RuleSets.ExtendedRuleSet());
#if SaferPasswords
        /// <summary>
        /// Obtiene un evaluador de contraseñas con reglas adicionales de complejidad.
        /// </summary>
        [Name(St2.ComplexEvaluator)]
        public static readonly IPasswordEvaluator ComplexEvaluator = new PasswordEvaluator(RuleSets.ComplexRuleSet());
#endif
    }
}