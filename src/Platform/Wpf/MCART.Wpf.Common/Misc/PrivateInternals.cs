/*
PrivateInternals.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;
using TheXDS.MCART.Helpers;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: XmlnsDefinition("http://schemas.thexds.local/mcart", "TheXDS.MCART.Types", AssemblyName = "MCART.Wpf.Common")]
[assembly: XmlnsDefinition("http://schemas.thexds.local/mcart", "TheXDS.MCART.ValueConverters", AssemblyName = "MCART.Wpf.Common")]
[assembly: XmlnsPrefix("http://schemas.thexds.local/mcart", "mcart")]

namespace TheXDS.MCART.Misc;

internal class PrivateInternals
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static Uri MakePackUri(string path)
    {
        return MakePackUri(path, ReflectionHelpers.GetCallingMethod()?.DeclaringType?.Assembly ?? throw new InvalidOperationException());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static Uri MkTemplateUri()
    {
        Type? t = ReflectionHelpers.GetCallingMethod()?.DeclaringType ?? throw new InvalidOperationException();
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
