//
//  FieldAttributes.cs
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
using System.Reflection;

namespace MCART.Data.Triton
{
    /// <summary>
    /// Atributo de campo llave principal.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class PKeyAttribute : Attribute
    {
        public readonly bool Automatic;

        public PKeyAttribute(bool automatic = false)
        {
            Automatic = automatic;
        }
    }

    /// <summary>
    /// Atributo de campo llave secundario.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class FKeyAttribute : Attribute
    {
        /// <summary>
        /// Campo al cual se hace referencia.
        /// </summary>
        public readonly FieldInfo Field;
        /// <summary>
        /// "Tabla" a la cual se hace referencia. En realidad, es un tipo
        /// <c>struct</c> de .Net Framework que tiene al campo descrito por
        /// <see cref="Field"/> como miembro público.
        /// </summary>
        public readonly Type LinkStruct;
        /// <summary>
        /// Opciones de actualización/eliminación para los motores de base de
        /// datos que lo soporten.
        /// </summary>
        public readonly FKOption FKOption;
        /// <summary>
        /// Marca un campo público como un campo llave secundario,
        /// estableciendo la referencia a otro campo llave.
        /// </summary>
        /// <param name="fieldName">Nombre del campo.</param>
        /// <param name="linkStruct">
        /// <c>struct</c> que contiene al campo.
        /// </param>
        public FKeyAttribute(string fieldName, Type linkStruct)
        {
            if (fieldName.IsEmpty()) throw new ArgumentNullException(nameof(fieldName));
            if (linkStruct.IsNull()) throw new ArgumentNullException(nameof(linkStruct));
            // TODO: verificar que linkStruct solo acepte estructuras.
            LinkStruct = linkStruct;
            Field = linkStruct.GetField(fieldName) ?? throw new MissingMemberException(linkStruct.Name, fieldName);
        }
    }

    /// <summary>
    /// Atributo de campo que no puede ser nulo.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class NotNullAttribute : Attribute { }

    /// <summary>
    /// Atributo que define un valor predeterminado para el campo en caso de 
    /// omitirse su valor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class DefaultValueAttribute : Attribute
    {
        public readonly object Value;
        public DefaultValueAttribute(object value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Atributo que indica que un campo no debe repetirse en una tabla.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class UniqueAttribute : Attribute { }

    /// <summary>
    /// Atributo que indica un campo que, si el motor de base de datos lo
    /// permite, será rellenado con ceros para ocupar todo su espacio asignado.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class ZeroFillAttribute : Attribute { }
    
    /// <summary>
    /// Atributo que indica un campo que, si el motor de base de datos lo
    /// permite, será almacenado en un formato binario alternativo.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class BinaryAttribute : Attribute { }

    /// <summary>
    /// Atributo que indica un campo numérico que, si el motor de base de datos
    /// lo permite, será almacenado sin signo en la base de datos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class UnsignedAttribute : Attribute { }

}