/*
IPasswordEvaluator.cs

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

using System.Security;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Describe los métodos a implementar para crear clases que evalúen la
    /// seguridad de una contraseña.
    /// </summary>
    public interface IPasswordEvaluator
    {
        /// <summary>
        /// Obtiene una lista de las reglas a aplicar a este
        /// <see cref="IPasswordEvaluator"/> .
        /// </summary>
        /// <value>The rules.</value>
        System.Collections.Generic.HashSet<PasswordEvaluationRule> Rules { get; }
        /// <summary>
        /// Evalúa una contraseña.
        /// </summary>
        /// <returns>
        /// La evaluación de la contraseña utilizando todas las reglas activas
        /// en <see cref="Rules"/>.
        /// </returns>
        /// <param name="password">Contraseña a evaluar.</param>
        PwEvalResult Evaluate(SecureString password);
    }
}