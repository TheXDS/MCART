/*
TypeFactory.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;
using static System.Reflection.Emit.OpCodes;
using static System.Reflection.MethodAttributes;
using static TheXDS.MCART.Types.Extensions.EnumExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.Types
{
    public abstract class TypeFactory
    {
        protected class PropertyBuilderInfo
        {
            [Flags]
            public enum PropertyFlags
            {
                None,
                Readable,
                Writtable,
                ReadWrite,
                Notifies,


            }
            public readonly PropertyBuilder Builder;
            public readonly FieldBuilder BackingField;
            public readonly ILGenerator Getter;
            public readonly ILGenerator Setter;
            public readonly MethodBuilder GetterBuilder;
            public readonly MethodBuilder SetterBuilder;
            public readonly PropertyFlags Flags;
            private PropertyBuilderInfo(PropertyBuilder builder, FieldBuilder backingField)
            {

            }
        }



        private readonly HashSet<PropertyBuilder> _declaredProperties = new HashSet<PropertyBuilder>();
        private readonly HashSet<FieldBuilder> _declaredFields = new HashSet<FieldBuilder>();


    }

    
}
