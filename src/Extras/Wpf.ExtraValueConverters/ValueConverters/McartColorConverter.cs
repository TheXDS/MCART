/*
McartColorConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ValueConverters.Base;
using MT = TheXDS.MCART.Types;
using static TheXDS.MCART.Misc.AttributeErrorMessages;

namespace TheXDS.MCART.ValueConverters;

/// <summary>
/// Convierte valores desde y hacia objetos de tipo
/// <see cref="MT.Color"/>.
/// </summary>
[RequiresUnreferencedCode(ClassScansForTypes)]
public sealed class McartColorConverter : IValueConverter
{
    private static readonly Dictionary<Type, IInValueConverter<MT.Color>> _converters = ReflectionHelpers.FindAllObjects<IInValueConverter<MT.Color>>().ToDictionary(p => p.TargetType);

    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is not MT.Color c) return targetType.Default();
        if (targetType.IsAssignableFrom(value?.GetType())) return value;
        return _converters.TryGetValue(targetType, out var converter) ? converter.Convert(c, targetType, parameter, culture) : targetType.Default();
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (targetType != typeof(MT.Color)) return null;
        if (targetType.IsAssignableFrom(value?.GetType())) return value;
        return _converters.TryGetValue(targetType, out var converter) ? converter.ConvertBack(value, targetType, parameter, culture) : targetType.Default();
    }
}
