﻿/*
EnumDescriptionConverter.cs

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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types.Converters;

/// <summary>
/// Converts an enumeration value to its friendly presentation as a string.
/// </summary>
/// <param name="type">Type of enumeration to describe</param>
[RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
public class EnumDescriptionConverter([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type type) : EnumConverter(type)
{
    private static readonly Func<Enum, string?>[] _converters;

    static EnumDescriptionConverter()
    {
        _converters =
        [
            e => e.GetAttribute<LocalizedDescriptionAttribute>()?.Description,
            e => e.GetAttribute<System.ComponentModel.DescriptionAttribute>()?.Description,
            e => e.GetAttribute<Attributes.DescriptionAttribute>()?.Value,
            e => e.NameOf()
        ];
    }

    /// <inheritdoc/>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        return destinationType == typeof(string) && value is Enum e 
            ? _converters.Select(p => p.Invoke(e)).NotNull().First()
            : base.ConvertTo(context, culture, value, destinationType);
    }
}
