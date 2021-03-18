/*
WpfInternal.cs

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
using System.Reflection;
using System.Windows;
using TheXDS.MCART.Helpers;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

namespace TheXDS.MCART.Misc
{
    internal class WpfInternal
    {
        internal static Uri MakePackUri(string path)
        {
            return MakePackUri(path, ReflectionHelpers.GetCallingMethod()?.DeclaringType?.Assembly ?? throw new InvalidOperationException());
        }

        internal static Uri MkTemplateUri()
        {
            var t = ReflectionHelpers.GetCallingMethod()?.DeclaringType ?? throw new InvalidOperationException();
            return MkTemplateUri(t.Name, t.Assembly);
        }

        internal static Uri MakePackUri(string path, Assembly asm)
        {
            return new Uri($"pack://application:,,,/{asm.GetName().Name};component/{path}");
        }

        internal static Uri MkTemplateUri(string template, Assembly asm)
        {
            return MakePackUri($"Resources/Templates/{template}Template.xaml", asm);
        }

        internal static Uri MkTemplateUri<T>()
        {
            return MkTemplateUri(typeof(T).Name, typeof(T).Assembly);
        }
    }
}
