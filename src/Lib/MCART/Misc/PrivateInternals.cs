/*
PrivateInternals.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Misc;

[ExcludeFromCodeCoverage]
internal static class PrivateInternals
{
    public static IEnumerable<NamedObject<TField>> List<TField>(Type source)
    {
        return List<TField>(source, BindingFlags.Static | BindingFlags.Public, null);
    }

    public static IEnumerable<NamedObject<TField>> List<TField>(Type source, object instance)
    {
        return List<TField>(source, BindingFlags.Public, instance);
    }

    public static IEnumerable<Type> SafeGetTypes(this Assembly asm)
    {
        try
        {
            return asm.GetTypes();
        }
        catch
        {
            return Type.EmptyTypes;
        }
    }

    public static bool TryParseValues<TValue, TResult>(string[] separators, string value, in byte items, Func<TValue[], TResult> instantiationCallback, out TResult result)
    {
#if EnforceContracts && DEBUG
        if (separators is null || !separators.Any())
            throw new ArgumentNullException(nameof(separators));
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value));
        if (instantiationCallback is null)
            throw new ArgumentNullException(nameof(instantiationCallback));
#endif
        foreach (TypeConverter t in Common.FindConverters(typeof(string), typeof(TValue)))
        {
            foreach (string? j in separators)
            {
                string[]? l = value.Split(new[] { j }, StringSplitOptions.RemoveEmptyEntries);
                if (l.Length != items) continue;
                try
                {
                    int c = 0;
                    result = instantiationCallback(l.Select(k => (TValue)t.ConvertTo(l[c++].Trim(), typeof(TValue))!).ToArray());
                    return true;
                }
                catch
                {
                    break;
                }
            }
        }
        result = default!;
        return false;
    }

    private static IEnumerable<NamedObject<TField>> List<TField>(IReflect source, BindingFlags flags, object? instance)
    {
        return source.GetFields(flags).Where(f => f.FieldType.Implements<TField>())
            .Select(p => new NamedObject<TField>((TField)p.GetValue(instance)!));
    }
}
