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
        public readonly FieldInfo Field;
        public readonly Type LinkStruct;

        public FKeyAttribute(string fieldName, Type linkStruct)
        {
            if (string.IsNullOrWhiteSpace(fieldName)) throw new ArgumentNullException(nameof(fieldName));
            if (ReferenceEquals(linkStruct, null)) throw new ArgumentNullException(nameof(linkStruct));
            // TODO: verificar que linkStruct solo acepte estructuras.
            LinkStruct = linkStruct;
            Field = LinkStruct.GetField(fieldName) ?? throw new MissingMemberException(linkStruct.Name, fieldName);
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

}