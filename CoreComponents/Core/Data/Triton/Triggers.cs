//
//  Triggers.cs
//
//  This file is part of MCART
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Morgan
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

namespace MCART.Data.Triton
{
    /// <summary>
    /// Delegado para una función que MCART Tritón ejecuta antes/después de una
    /// inserción/actualización/eliminación de datos.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de los datos que este delegado procesa. Generalmente, es el mismo
    /// tipo del <c>struct</c> del cual el delegado es miembro.
    /// </typeparam>
    /// <param name="affectedRow">Fila afectada por la transacción.</param>
    /// <param name="table">
    /// Datos de la tabla antes/después de la transacción.
    /// </param>
    public delegate void TriggerFunction<T>(T affectedRow, IEnumerable<T> table) where T : struct;

    /// <summary>
    /// Marca al método para ser ejecutado antes de una inserción de datos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class BeforeInsertAttribute : Attribute { }

    /// <summary>
    /// Marca al método para ser ejecutado después de una inserción de datos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class AfterInsertAttribute : Attribute { }

    /// <summary>
    /// Marca al método para ser ejecutado antes de una actualización de datos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class BeforeUpdateAttribute : Attribute { }

    /// <summary>
    /// Marca al método para ser ejecutado después de una actualización de
    /// datos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class AfterUpdateAttribute : Attribute { }
    
    /// <summary>
    /// Marca al método para ser ejecutado antes de una eliminación de datos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class BeforeDeleteAttribute : Attribute { }

    /// <summary>
    /// Marca al método para ser ejecutado después de una eliminación de datos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class AfterDeleteAttribute : Attribute { }
}