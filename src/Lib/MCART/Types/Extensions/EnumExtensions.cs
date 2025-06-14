/*
EnumExtensions.cs

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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains extensions for the <see cref="Enum"/> class.
/// </summary>
public static class EnumExtensions
{
    private static byte[] BypassByte(byte b) => [b];

    /// <summary>
    /// Gets a <see cref="MethodInfo"/> for a method that allows
    /// performing the conversion of <typeparamref name="T"/> to a
    /// byte array.
    /// </summary>
    /// <typeparam name="T">Type of the enumeration to convert.</typeparam>
    /// <returns>
    /// A <see cref="MethodInfo"/> for a method that converts from
    /// the underlying type of the enumeration to a byte array.
    /// </returns>
    [DebuggerStepThrough]
    public static MethodInfo ByteConversionMethod<T>() where T : struct, Enum
    {
        return ByteConversionMethodInternal(typeof(T));
    }

    /// <summary>
    /// Gets a <see cref="MethodInfo"/> for a method that allows
    /// performing the conversion of the enumeration type to a byte
    /// array.
    /// </summary>
    /// <param name="enumType">Type of the enumeration to convert.</param>
    /// <returns>
    /// A <see cref="MethodInfo"/> for a method that converts from
    /// the underlying type of the enumeration to a byte array.
    /// </returns>
    [DebuggerStepThrough]
    public static MethodInfo ByteConversionMethod(in Type enumType)
    {
        if (!enumType.IsEnum) throw Errors.EnumExpected(nameof(enumType), enumType);
        return ByteConversionMethodInternal(enumType);
    }

    /// <summary>
    /// Converts an enumeration value to its byte representation.
    /// </summary>
    /// <param name="value">Enumeration value to convert.</param>
    /// <returns>
    /// A byte array with the representation of the enumeration value.
    /// </returns>
    /// <exception cref="PlatformNotSupportedException">
    /// Thrown if the platform is not supported, and the
    /// <see cref="Enum"/> uses an unusual underlying type for which it
    /// is not possible to obtain a byte converter.
    /// </exception>
    [DebuggerStepThrough]
    public static byte[] ToBytes(this Enum value)
    {
        return (byte[])ByteConversionMethodInternal(value.GetType())
            .Invoke(null, [value])!;
    }

    /// <summary>
    /// Creates a delegate that converts an enumeration value of the
    /// specified type into a byte array.
    /// </summary>
    /// <typeparam name="T">Type of the enumeration to convert.</typeparam>
    /// <returns>
    /// A delegate that converts an enumeration value of the
    /// specified type into a byte array.
    /// </returns>
    [DebuggerStepThrough]
    public static Func<T, byte[]> ToBytes<T>() where T : struct, Enum
    {
        return (Func<T, byte[]>)Delegate.CreateDelegate(typeof(Func<T, byte[]>),
            ByteConversionMethodInternal(typeof(T)), true)!;
    }

    /// <summary>
    /// Gets a friendly name for an enumeration value.
    /// </summary>
    /// <param name="value">
    /// <see cref="Enum"/> from which to get the name.
    /// </param>
    /// <returns>
    /// A friendly name for <paramref name="value"/>, or the compiled
    /// name of <paramref name="value"/> if a friendly name has not been
    /// defined using the <see cref="NameAttribute"/> attribute.
    /// </returns>
    public static string NameOf(this Enum value)
    {
        return value.GetAttribute<NameAttribute>()?.Value ??
               value.GetAttribute<DescriptionAttribute>()?.Value ??
               value.GetAttribute<System.ComponentModel.DescriptionAttribute>()?.Description ??
               value.ToString();
    }

    /// <summary>
    /// Exposes the values of an <see cref="Enum"/> as a collection
    /// of <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the enumeration to get.</typeparam>
    /// <returns>
    /// An enumerator that exposes the values of the <see cref="Enum"/>
    /// as a collection of <see cref="NamedObject{T}"/>.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static IEnumerable<NamedObject<T>> NamedEnums<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>().OfType<T>()
            .Select(j => new NamedObject<T>(j.NameOf(), j));
    }

    /// <summary>
    /// Converts an enumeration value to its underlying type.
    /// </summary>
    /// <typeparam name="T">Type of the enumeration.</typeparam>
    /// <param name="value">Enumeration value to convert.</param>
    /// <returns>
    /// A primitive value equal to the enumeration value.
    /// </returns>
    public static object ToUnderlyingType<T>(this T value) where T : struct, Enum
    {
        return Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T)));
    }

    /// <summary>
    /// Converts an enumeration value to its underlying type.
    /// </summary>
    /// <param name="value">Enumeration value to convert.</param>
    /// <returns>
    /// A primitive value equal to the enumeration value.
    /// </returns>
    public static object ToUnderlyingType(this Enum value)
    {
        return Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
    }

    /// <summary>
    /// Determines if an enumeration value has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="enumValue">
    /// Enumeration value from which the attribute will be extracted.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the enumeration value has the attribute,
    /// <see langword="false"/> otherwise.
    /// </returns>
#if !CLSCompliance && PreferExceptions
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the enumeration type does not contain a defined value
    /// for <paramref name="enumValue"/>.
    /// </exception>
    [CLSCompliant(false)]
#endif
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static bool HasAttribute<T>(this Enum enumValue) where T : Attribute => HasAttribute<T>(enumValue, out _);

    /// <summary>
    /// Determines if an enumeration value has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="enumValue">
    /// Enumeration value from which the attribute will be extracted.
    /// </param>
    /// <param name="attribute">
    /// Output parameter. If an attribute of type
    /// <typeparamref name="T"/> has been found, it is returned.
    /// It will return <see langword="null"/> if the member does not have
    /// the specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the enumeration value has the attribute,
    /// <see langword="false"/> otherwise.
    /// </returns>
#if !CLSCompliance && PreferExceptions
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the enumeration type does not contain a defined value
    /// for <paramref name="enumValue"/>.
    /// </exception>
    [CLSCompliant(false)]
#endif
    public static bool HasAttribute<T>(this Enum enumValue, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        Type? type = enumValue.GetType();
        attribute = null;
        if (!type.IsEnumDefined(enumValue))
#if !CLSCompliance && PreferExceptions
            throw Errors.UndefinedEnum(type, nameof(enumValue), enumValue);
#else
            return false;
#endif
        string? n = type.GetEnumName(enumValue)!;
        attribute = type.GetField(n)?.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        return attribute is not null;
    }

    /// <summary>
    /// Determines if a member has a defined attribute.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value to return.
    /// </typeparam>
    /// <typeparam name="TAttribute">
    /// Type of attribute to search for. Must inherit from
    /// <see cref="Attribute"/> and from <see cref="IValueAttribute{T}"/>.
    /// </typeparam>
    /// <param name="enumValue">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="value">
    /// Output parameter. If an attribute of type
    /// <typeparamref name="TAttribute"/> has been found, the value
    /// of the same is returned.
    /// It will return <see langword="default"/> if the member does not have the
    /// specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member has the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static bool HasAttrValue<TAttribute, TValue>(this Enum enumValue, out TValue value)
        where TAttribute : Attribute, IValueAttribute<TValue>
    {
        bool retVal = HasAttribute(enumValue, out TAttribute? attribute);
        value = retVal ? attribute!.Value : default!;
        return retVal;
    }

    /// <summary>
    /// Determines if a member has a defined attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="enumValue">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="attribute">
    /// Output parameter. If an attribute of type
    /// <typeparamref name="T"/> has been found, the same is returned.
    /// It will return <see langword="null"/> if the member does not have the
    /// specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member has the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static bool HasAttributes<T>(this Enum enumValue, out IEnumerable<T> attribute) where T : Attribute
    {
        string? n;
        Type? type = enumValue.GetType();
        if (!type.IsEnumDefined(enumValue) || (n = type.GetEnumName(enumValue)) is null)
        {
            attribute = [];
            return false;
        }
        attribute = type.GetField(n)!.GetCustomAttributes(typeof(T), false).OfType<T>();
        return attribute.Any();
    }

    /// <summary>
    /// Returns the attribute associated with the enumeration value declaration.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit
    /// <see cref="Attribute"/>.
    /// </typeparam>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the
    /// associated data in the enumeration value declaration.
    /// </returns>
    public static T? GetAttribute<T>(this Enum enumValue) where T : Attribute
    {
        HasAttribute(enumValue, out T? attribute);
        return attribute;
    }

    /// <summary>
    /// Returns the attribute associated with the specified assembly.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="enumValue">
    /// <see cref="Enum"/> from which the
    /// attribute will be extracted.
    /// </param>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the
    /// associated data in the declaration of the assembly; or <see langword="null"/> if
    /// the specified attribute is not found.
    /// </returns>
    [Sugar]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static IEnumerable<T>? GetAttributes<T>(this Enum enumValue) where T : Attribute
    {
        HasAttributes(enumValue, out IEnumerable<T>? attributes);
        return attributes;
    }

    [DebuggerStepThrough]
    private static MethodInfo ByteConversionMethodInternal(in Type enumType)
    {
        Type? tRsp = enumType.GetEnumUnderlyingType();
        return tRsp != typeof(byte)
            ? typeof(BitConverter).GetMethods().FirstOrDefault(p =>
            {
                ParameterInfo[]? pars = p.GetParameters();
                return p.Name == nameof(BitConverter.GetBytes)
                       && pars.Length == 1
                       && pars[0].ParameterType == tRsp;
            }) ?? throw new PlatformNotSupportedException()
            : new Func<byte, byte[]>(BypassByte).Method;
    }
}
