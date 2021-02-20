/*
IValidationEntry.cs

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

using System;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// configurar reglas de validación para una propiedad.
    /// </summary>
    /// <typeparam name="T">Tipo de la propiedad seleccionada.</typeparam>
    public interface IValidationEntry<T>
    {
        /// <summary>
        /// Agrega una regla de validación para la propiedad seleccionada.
        /// </summary>
        /// <param name="rule">
        /// FUnción que ejecutala validación. La función debe devolder 
        /// <see langword="true"/> si la propiedad pasa satisfactoriamente
        /// la prueba, <see langword="false"/> en caso contrario.
        /// </param>
        /// <param name="error">
        /// Mensaje de error a mostrarse si la regla falla.
        /// </param>
        /// <returns>
        /// La misma instancia de regla de validación, permitiendo el uso
        /// de sintaxis Fluent.
        /// </returns>
        IValidationEntry<T> AddRule(Func<T, bool> rule, string error);
    }
}