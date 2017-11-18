//
//  SpecificStrings.cs
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

namespace MCART.Resources
{
    /// <summary>
    /// Contiene cadenas específicas para uso interno de MCART.
    /// </summary>
    internal static class SpecificStrings
    {

#pragma warning disable CS1591 // Las cadenas generalmente no requieren de descripción.

        public static readonly string GpVNoData =
            " (Sin datos)";
        public static readonly string NullPwEvalRule =
            "Regla nula";
        public static readonly string NullPwEvalRule2 =
            "Esta regla no evaluará la contraseña, sino que devolverá un " +
            "valor constante que ayuda a balancear el puntaje de otras " +
            "reglas en el total.";
        public static readonly string PwLatinEvalRule =
            "Caracteres latinos";
        public static readonly string PwLcaseEvalRule =
            "Minúsculas";
        public static readonly string PwLenghtEvalRule =
            "Longitud de contraseña";
        public static readonly string PwLenghtEvalRule2 =
            "Esta regla evalúa la longitud de la contraseña, para asegurarse " +
            "que contenga de {0} a {1} caracteres.";
        public static readonly string PwNumbersEvalRule =
            "Números";
        public static readonly string PwOtherSymbsEvalRule =
            "Otros símbolos";
        public static readonly string PwOtherUTFEvalRule =
            "Otros caracteres Unicode";
        public static readonly string PwSymbolsEvalRule =
            "Símbolos";
        public static readonly string PwUcaseEvalRule =
            "Mayúsculas";
        public static readonly string xBuilder =
            "Esta regla evalúa que la contraseña contenga {0}";

#pragma warning restore CS1591

    }
}