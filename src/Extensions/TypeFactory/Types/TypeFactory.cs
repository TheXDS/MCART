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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;
using static System.Reflection.Emit.OpCodes;
using static System.Reflection.MethodAttributes;
using static TheXDS.MCART.Types.Extensions.EnumExtensions;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using TheXDS.MCART.Component;

namespace TheXDS.MCART.Types
{







    /// <summary>
    /// Fábrica de tipos. Permite compilar nuevos tipos en Runtime.
    /// </summary>
    public class TypeFactory : IExposeAssembly
    {
        private readonly string _namespace;
        private readonly bool _useGuid;
        private readonly ModuleBuilder _mBuilder;
        private readonly AssemblyBuilder _assembly;
        private readonly IDictionary<string, Type> _builtTypes = new Dictionary<string, Type>();
        
        public Assembly Assembly => _assembly;


        public TypeFactory() : this("TheXDS.MCART.Types._Generated") { }

        public TypeFactory(string @namespace) : this(@namespace, true) { }

        public TypeFactory(string @namespace, bool useGuid)
        {
            _namespace = @namespace;
            _useGuid = useGuid;
            _assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_namespace), AssemblyBuilderAccess.Run);
            _mBuilder = _assembly.DefineDynamicModule(_namespace);
        }






        public TypeBuilder NewClass(string name)
        {
            return NewClass(name, typeof(object), Type.EmptyTypes);
        }

        public TypeBuilder NewClass(string name, IEnumerable<Type> interfaces)
        {
            return NewClass(name, typeof(object), interfaces);
        }

        public TypeBuilder NewClass(string name, Type baseType, IEnumerable<Type> interfaces)
        {
            var nme = new StringBuilder();
            nme.Append($"{_namespace}.{name}");
            if (_useGuid) nme.Append($"_{Guid.NewGuid().ToString().Replace("-", string.Empty)}");
            return _mBuilder.DefineType(nme.ToString(), TypeAttributes.Public | TypeAttributes.Class, baseType, interfaces?.ToArray());
        }







    }
}
