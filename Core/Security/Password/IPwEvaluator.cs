//
//  IPwEvaluator.cs
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
namespace MCART.Security.Password
{
    /// <summary>
    /// Describe los métodos a implementar para crear clases que evalúen la
    /// seguridad de una contraseña.
    /// </summary>
    public interface IPwEvaluator
    {
        /// <summary>
        /// Obtiene una lista de las reglas a aplicar a este
        /// <see cref="IPwEvaluator"/> .
        /// </summary>
        /// <value>The rules.</value>
        System.Collections.Generic.List<PwEvalRule> Rules { get; }
        /// <summary>
        /// Evalúa una contraseña.
        /// </summary>
        /// <returns>
        /// La evaluación de la contraseña utilizando todas las reglas activas
        /// en <see cref="Rules"/>.
        /// </returns>
        /// <param name="Pwd">Contraseña a evaluar.</param>
        PwEvalResult Evaluate(string Pwd);
    }
}