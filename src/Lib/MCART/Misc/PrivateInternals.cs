/*
PrivateInternals.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using static TheXDS.MCART.Misc.AttributeErrorMessages;

namespace TheXDS.MCART.Misc;

internal static class PrivateInternals
{
    public static bool TryParseValues<TValue, TResult>(TypeConverter t, string[] separators, string value, in byte items, Func<TValue[], TResult> instantiationCallback, out TResult result)
    {
#if EnforceContracts && DEBUG
        if (separators is null || separators.Length == 0)
            throw new ArgumentNullException(nameof(separators));
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value));
        ArgumentNullException.ThrowIfNull(t);
        ArgumentNullException.ThrowIfNull(instantiationCallback);
#endif
        foreach (string? j in separators)
        {
            string[]? l = value.Split([j], StringSplitOptions.RemoveEmptyEntries);
            if (l.Length != items) continue;
            try
            {
                int c = 0;
                result = instantiationCallback.Invoke([.. l.Select(k => (TValue)t.ConvertTo(l[c++].Trim(), typeof(TValue))!)]);
                return true;
            }
            catch
            {
                break;
            }
        }
        result = default!;
        return false;
    }

    [RequiresUnreferencedCode(MethodScansForTypes)]
    public static IEnumerable<Type> SafeGetExportedTypes(Assembly arg)
    {
        try
        {
            return arg.GetExportedTypes();
        }
        catch
        {
            return [];
        }
    }
}
