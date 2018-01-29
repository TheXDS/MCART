/*
InternalStrings.cs

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

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene cadenas específicas para uso interno de MCART.
    /// </summary>
    internal static partial class InternalStrings
    {
        internal const string GpVNoData = " (Sin datos)";
        internal const string NullPwEvalRule = "Regla nula";
        internal const string NullPwEvalRule2 = "Esta regla no evaluará la contraseña, sino que devolverá un valor constante que ayuda a balancear el puntaje de otras reglas en el total.";
        internal const string ObsoleteCheckDoc = "Este método no está soportado. Por favor, lea la documentación de MCART acerca de esta clase.";
        internal const string PwLatinEvalRule = "Caracteres latinos";
        internal const string PwLcaseEvalRule = "Minúsculas";
        internal const string PwLenghtEvalRule = "Longitud de contraseña";
        internal const string PwLenghtEvalRule2 = "Esta regla evalúa la longitud de la contraseña, para asegurarse que contenga de {0} a {1} caracteres.";
        internal const string PwNumbersEvalRule = "Números";
        internal const string PwOtherSymbsEvalRule = "Otros símbolos";
        internal const string PwOtherUTFEvalRule = "Otros caracteres Unicode";
        internal const string PwSymbolsEvalRule = "Símbolos";
        internal const string PwUcaseEvalRule = "Mayúsculas";
        internal const string xBuilder = "Esta regla evalúa que la contraseña contenga {0}";
        internal static string UnkErrLoadingRes(string res, string ex) => $"Error desconocido al cargar recurso {res}\n{ex}";
    }
}