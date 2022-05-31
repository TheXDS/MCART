/*
EnumExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;

/// <summary>
/// Contiene extensiones útiles para la clase <see cref="Enum" />.
/// </summary>
public static class EnumExtensions
{
    private static byte[] BypassByte(byte b) => new[] { b };

    /// <summary>
    /// Obtiene un <see cref="MethodInfo" /> para un método que permita
    /// realizar la conversión de <typeparamref name="T" /> a un arreglo
    /// de bytes.
    /// </summary>
    /// <typeparam name="T">Tipo de la enumeración a convertir.</typeparam>
    /// <returns>
    /// Un <see cref="MethodInfo" /> para un método que convierte desde
    /// el tipo base de la enumeración a un arreglo de bytes.
    /// </returns>
    [DebuggerStepThrough]
    public static MethodInfo ByteConversionMethod<T>() where T : struct, Enum
    {
        return ByteConversionMethodInternal(typeof(T));
    }

    /// <summary>
    /// Obtiene un <see cref="MethodInfo" /> para un método que permita
    /// realizar la conversión del tipo de la enumeración a un arreglo
    /// de bytes.
    /// </summary>
    /// <param name="enumType">Tipo de la enumeración a convertir.</param>
    /// <returns>
    /// Un <see cref="MethodInfo" /> para un método que convierte desde
    /// el tipo base de la enumeración a un arreglo de bytes.
    /// </returns>
    [DebuggerStepThrough]
    public static MethodInfo ByteConversionMethod(in Type enumType)
    {
        if (!enumType.IsEnum) throw Errors.EnumExpected(nameof(enumType), enumType);
        return ByteConversionMethodInternal(enumType);
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

    /// <summary>
    /// Convierte un valor de enumeración a su representación en bytes.
    /// </summary>
    /// <param name="value">Valor de enumeración a convertir.</param>
    /// <returns>
    /// Un arreglo de bytes con la representación del valor de
    /// enumeración.
    /// </returns>
    /// <exception cref="PlatformNotSupportedException">
    /// Se produce en caso que la plataforma no sea soportada, y que el
    /// <see cref="Enum" /> utilice un tipo base inusual para el cual no
    /// sea posible obtener un convertidor a bytes.
    /// </exception>
    [DebuggerStepThrough]
    public static byte[] ToBytes(this Enum value)
    {
        return (byte[])ByteConversionMethodInternal(value.GetType())
            .Invoke(null, new object[] { value })!;
    }

    /// <summary>
    /// Crea un delegado que convierte un valor de enumeración del tipo
    /// especificado en un arreglo de bytes.
    /// </summary>
    /// <typeparam name="T">Tipo de la enumeración a convertir.</typeparam>
    /// <returns>
    /// Un delegado que convierte un valor de enumeración del tipo
    /// especificado en un arreglo de bytes.
    /// </returns>
    [DebuggerStepThrough]
    public static Func<T, byte[]> ToBytes<T>() where T : struct, Enum
    {
        return (Func<T, byte[]>)Delegate.CreateDelegate(typeof(Func<T, byte[]>),
            ByteConversionMethodInternal(typeof(T)), true)!;
    }

    /// <summary>
    /// Obtiene un nombre personalizado para un valor de enumeración.
    /// </summary>
    /// <param name="value">
    /// <see cref="Enum" /> del cual obtener el nombre.
    /// </param>
    /// <returns>
    /// Un nombre amigable para <paramref name="value" />, o el nombre
    /// compilado de <paramref name="value" /> si no se ha definido un
    /// nombre amigable por medio del atributo
    /// <see cref="NameAttribute"/>.
    /// </returns>
    public static string NameOf(this Enum value)
    {
        return value.GetAttr<NameAttribute>()?.Value ??
               value.GetAttr<DescriptionAttribute>()?.Value ??
               value.GetAttr<System.ComponentModel.DescriptionAttribute>()?.Description ??
               value.ToString();
    }

    /// <summary>
    /// Expone los valores de un <see cref="Enum"/> como una colección
    /// de <see cref="NamedObject{T}"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de la enumeración a obtener.</typeparam>
    /// <returns>
    /// Un enumerador que expone los valores del <see cref="Enum"/>
    /// como una colección de <see cref="NamedObject{T}"/>.
    /// </returns>
    public static IEnumerable<NamedObject<T>> NamedEnums<T>() where T : Enum
    {
        return typeof(T).GetEnumValues().OfType<T>()
            .Select(j => new NamedObject<T>(j, j.NameOf()));
    }

    /// <summary>
    /// Convierte un valor de enumeración a su tipo base.
    /// </summary>
    /// <typeparam name="T">Tipo de la enumeración.</typeparam>
    /// <param name="value">Valor de enumeración a convertir.</param>
    /// <returns>
    /// Un valor primitivo igual al valor de enumeración.
    /// </returns>
    public static object ToUnderlyingType<T>(this T value) where T : struct, Enum
    {
        return Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T)));
    }

    /// <summary>
    /// Convierte un valor de enumeración a su tipo base.
    /// </summary>
    /// <param name="value">Valor de enumeración a convertir.</param>
    /// <returns>
    /// Un valor primitivo igual al valor de enumeración.
    /// </returns>
    public static object ToUnderlyingType(this Enum value)
    {
        return Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
    }

    /// <summary>
    /// Determina si un valor de enumeración posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="enumValue">
    /// Valor de enumeración del cual se extraerá el atributo.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el valor de enumeración posee el atributo,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
#if !CLSCompliance && PreferExceptions
/// <exception cref="ArgumentOutOfRangeException">
/// Se produce si el tipo de enumeración no contiene un valor definido
/// para <paramref name="enumValue"/>.
/// </exception>
    [CLSCompliant(false)]
#endif
    public static bool HasAttr<T>(this Enum enumValue) where T : Attribute => HasAttr<T>(enumValue, out _);

    /// <summary>
    /// Determina si un valor de enumeración posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="enumValue">
    /// Valor de enumeración del cual se extraerá el atributo.
    /// </param>
    /// <param name="attribute">
    /// Parámetro de salida. Si un atributo de tipo
    /// <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
    /// Se devolverá <see langword="null" /> si el miembro no posee el atributo
    /// especificado.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el valor de enumeración posee el atributo,
    /// <see langword="false" /> en caso contrario.
    /// </returns>
#if !CLSCompliance && PreferExceptions
/// <exception cref="ArgumentOutOfRangeException">
/// Se produce si el tipo de enumeración no contiene un valor definido
/// para <paramref name="enumValue"/>.
/// </exception>
    [CLSCompliant(false)]
#endif
    public static bool HasAttr<T>(this Enum enumValue, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        Type? type = enumValue.GetType();
        attribute = null;
        if (!type.IsEnumDefined(enumValue))
#if !CLSCompliance && PreferExceptions
            throw new ArgumentOutOfRangeException(nameof(enumValue));
#else
            return false;
#endif
        string? n = type.GetEnumName(enumValue)!;
        attribute = type.GetMember(n)[0].GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        return attribute is not null;
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="TValue">
    /// Tipo de valor a devolver.
    /// </typeparam>
    /// <typeparam name="TAttribute">
    /// Tipo de atributo a buscar. Debe heredar de
    /// <see cref="Attribute"/> y de <see cref="IValueAttribute{T}"/>.
    /// </typeparam>
    /// <param name="enumValue">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <param name="value">
    /// Parámetro de salida. Si un atributo de tipo
    /// <typeparamref name="TAttribute" /> ha sido encontrado, el valor
    /// del mismo es devuelto.
    /// Se devolverá <see langword="default" /> si el miembro no posee el atributo
    /// especificado.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttrValue<TAttribute, TValue>(this Enum enumValue, out TValue value)
        where TAttribute : Attribute, IValueAttribute<TValue>
    {
        bool retVal = HasAttr<TAttribute>(enumValue, out TAttribute? attr);
        value = retVal ? attr!.Value : default!;
        return retVal;
    }

    /// <summary>
    /// Determina si un miembro posee un atributo definido.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="enumValue">
    /// Miembro del cual se extraerá el atributo.
    /// </param>
    /// <param name="attribute">
    /// Parámetro de salida. Si un atributo de tipo
    /// <typeparamref name="T" /> ha sido encontrado, el mismo es devuelto.
    /// Se devolverá <see langword="null" /> si el miembro no posee el atributo
    /// especificado.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el miembro posee el atributo, <see langword="false" />
    /// en caso contrario.
    /// </returns>
    public static bool HasAttrs<T>(this Enum enumValue, out IEnumerable<T> attribute) where T : Attribute
    {
        string? n;
        Type? type = enumValue.GetType();
        if (!type.IsEnumDefined(enumValue) || (n = type.GetEnumName(enumValue)) is null)
        {
            attribute = Array.Empty<T>();
            return false;
        }

        attribute = type.GetMember(n)[0].GetCustomAttributes(typeof(T), false).OfType<T>();
        return attribute.Any();
    }

    /// <summary>
    /// Devuelve el atributo asociado a la declaración del valor de
    /// enumeración.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar
    /// <see cref="Attribute" />.
    /// </typeparam>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del valor de enumeración.
    /// </returns>
    public static T? GetAttr<T>(this Enum enumValue) where T : Attribute
    {
        HasAttr<T>(enumValue, out T? retval);
        return retval;
    }

    /// <summary>
    /// Devuelve el atributo asociado al ensamblado especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar <see cref="Attribute" />.
    /// </typeparam>
    /// <param name="enumValue">
    /// <see cref="Enum" /> del cual se extraerá el
    /// atributo.
    /// </param>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del ensamblado; o <see langword="null" /> en caso
    /// de no encontrarse el atributo especificado.
    /// </returns>
    [Sugar]
    public static IEnumerable<T>? GetAttrs<T>(this Enum enumValue) where T : Attribute
    {
        HasAttrs(enumValue, out IEnumerable<T>? attr);
        return attr;
    }
}
