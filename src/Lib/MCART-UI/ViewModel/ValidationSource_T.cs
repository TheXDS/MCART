/*
ValidationSource_T.cs

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

namespace TheXDS.MCART.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheXDS.MCART.Helpers;

/// <summary>
/// Ejecuta validaciones de datos dentro de un 
/// <see cref="IValidatingViewModel"/>.
/// </summary>
/// <typeparam name="T">
/// Tipo de ViewModel cuyos datos serán validados con esta instancia.
/// </typeparam>
public sealed class ValidationSource<T> : ValidationSource where T : IValidatingViewModel
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ValidationSource{TViewModel}"/>.
    /// </summary>
    /// <param name="npcSource">
    /// Instancia que es el origen de los datos de validación.
    /// </param>
    public ValidationSource(T npcSource) : base(npcSource)
    {
    }

    /// <summary>
    /// Registra un conjunto de reglas en la instancia de validación.
    /// </summary>
    /// <typeparam name="TValue">Tipo de la propiedad.</typeparam>
    /// <param name="propertySelector">
    /// Espresión que selecciona la propiedad a configurar.
    /// </param>
    /// <returns>
    /// La misma instancia de validación, lo cual permite utilizar sintaxis
    /// Fluent.
    /// </returns>
    public IValidationEntry<TValue> RegisterValidation<TValue>(Expression<Func<T, TValue>> propertySelector)
    {
        ValidationEntry<TValue>? r = new(ReflectionHelpers.GetProperty(propertySelector));
        _validationRules.Add(r);
        return r;
    }

    /// <summary>
    /// Enumera los errores de validación para la propiedad
    /// <paramref name="propertySelector"/>.
    /// </summary>
    /// <param name="propertySelector">
    /// Propiedad para la cual obtener los errores de validación.
    /// </param>
    /// <returns>
    /// Una enumeración con todos los errores de validación de la propiedad
    /// seleccionada.
    /// </returns>
    public IEnumerable<string> GetErrors(Expression<Func<T>> propertySelector)
    {
        return base[ReflectionHelpers.GetProperty(propertySelector).Name];
    }
}
