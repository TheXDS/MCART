/*
MockupFactory.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Reflection;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART.Mockups
{
    public class MockupBuilder<T>
    {
        private static readonly TypeFactory _factory = new TypeFactory("TheXDS.MCART.Mockups._Generated", true);
        private readonly TypeBuilder _builder = _factory.NewClass($"{typeof(T).Name}Mockup");
        private readonly ILGenerator _ctor;

        public MockupBuilder()
        {
            _ctor = _builder.AddPublicConstructor().CallBaseCtor<T>();
        }

        public T Build()
        {
            AddMembers();
            _ctor.Return();
            return _builder.CreateType()!.New<T>();
        }

        private void AddMembers()
        {
            foreach (var member in typeof(T).GetMembers())
            {
                switch (member)
                {
                    case PropertyInfo prop:
                        BuildProperty(prop);
                        break;
                    case MethodInfo method:
                        BuildMethod(method);
                        break;
                }
            }
        }

        private void BuildMethod(MethodInfo method)
        {
            MethodBuilder m;
            if (method.IsVoid())
            {
                m = _builder.DefineMethod(method.Name, MethodAttributes.Public,null,method.GetParameters().Select(p=>p.ParameterType).ToArray());



            }
            else
            {
                m = _builder.DefineMethod(method.Name, MethodAttributes.Public,method.ReturnType,);

            }

            if (!method.IsStatic)
            {
                
            }
        }

        private void BuildProperty(PropertyInfo prop) => BuildProperty(prop, prop.PropertyType.Default());

        private void BuildProperty(PropertyInfo prop, object? value)
        {
            switch (prop.CanRead, prop.CanWrite)
            {
                case (true, false):
                    _builder.AddComputedProperty(prop.Name, prop.PropertyType, il => il.LoadConstant(prop.PropertyType, value).Return());
                    break;
                case (false, true):
                    _builder.AddWriteOnlyProperty(prop.Name, prop.PropertyType).Setter!.Return();
                    break;
                case (true, true):
                    var p = _builder.AddAutoProperty(prop.Name, prop.PropertyType);
                    _ctor
                        .LoadConstant(prop.PropertyType, value)
                        .StoreProperty(p);
                    break;
                default:
                    throw new InvalidOperationException();
            };
        }

    }
}
