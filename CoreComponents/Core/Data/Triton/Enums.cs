//
//  Enums.cs
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

namespace MCART.Data.Triton
{
    /// <summary>
    /// Para los motores de base de datos que lo soporten, brinda información
    /// sobre la acción a tomar al actualizar/eliminar información que ha sido
    /// enlazada por un campo llave.
    /// </summary>
    /// <remarks>
    /// Esto funciona a nivel de los lenguajes SQL, no de MCART Triton.
    /// </remarks>
    public enum FKOption:byte
    {
        /// <summary>
        /// No realizar ninguna acción
        /// </summary>
        None,
        /// <summary>
        /// Restringir la actualización/eliminación.
        /// </summary>
        Restrict,
        /// <summary>
        /// Realizar la actualización/eliminación en cascada.
        /// </summary>
        Cascade,
        /// <summary>
        /// Establecer al campo en <c>null</c>
        /// </summary>
        SetNull
    }
    
    public enum Compare
    {
        Equals,
        Greater,
        Less,
        NotEqual,
        NotNull,
        GreaterOrEqual,
        LessOrEqual,
        Null
    }
}