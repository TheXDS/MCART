﻿/*
BinaryReaderExtensions.cs

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

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la clase
/// <see cref="BinaryReader"/>.
/// </summary>
public static partial class BinaryReaderExtensions
{
    private delegate object BrDelegate(BinaryReader reader, Type type);
    private record struct BrDynCheck(Func<Type, bool> Predicate, BrDelegate ReadDelegate);
    private static readonly BrDynCheck[] DynamicReadSets =
    [
        new(t => t.IsArray, ReadArray),
        new(t => t.IsEnum, DynamicReadEnum),
        new(TypeExtensions.Implements<ISerializable>, DynamicReadISerializable)
    ];

    /// <summary>
    /// Obtiene un método de lectura definido en la clase
    /// <see cref="BinaryReader"/> que pueda ser utilizado para leer un
    /// objeto del tipo especificado.
    /// </summary>
    /// <param name="t">
    /// Tipo para el cual obtener un método de lectura definido en la clase
    /// <see cref="BinaryReader"/>.
    /// </param>
    /// <returns>
    /// Un método de lectura definido en la clase
    /// <see cref="BinaryReader"/> que pueda ser utilizado para leer un
    /// objeto del tipo especificado, o <see langword="null"/> si no existe
    /// un método de lectura para el tipo especificado.
    /// </returns>
    public static MethodInfo? GetBinaryReadMethod(Type t)
    {
        return typeof(BinaryReader).GetMethods().FirstOrDefault(p =>
           p.Name.StartsWith("Read")
           && !p.Name.StartsWith("Read7BitEncodedInt") // Se debe ignorar este método especial.
           && p.Name.Length > 4
           && p.GetParameters().Length == 0
           && p.ReturnType == t);
    }

    /// <summary>
    /// Lee un valor de enumeración.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valor de enumeración a leer.
    /// </typeparam>
    /// <param name="br">
    /// <see cref="BinaryReader"/> desde el cual obtener.
    /// </param>
    /// <returns>
    /// Valor de enumeración obtenido desde el
    /// <see cref="BinaryReader"/>.
    /// </returns>
    public static T ReadEnum<T>(this BinaryReader br) where T : struct, Enum
    {
        return (T)ReadEnum(br, typeof(T));
    }

    /// <summary>
    /// Lee un valor de enumeración.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> desde el cual obtener.
    /// </param>
    /// <param name="enumType">
    /// Tipo de valor de enumeración a leer.
    /// </param>
    /// <returns>
    /// Valor de enumeración obtenido desde el
    /// <see cref="BinaryReader"/>.
    /// </returns>
    [DebuggerStepThrough]
    public static Enum ReadEnum(this BinaryReader br, Type enumType)
    {
        Type? t = enumType.GetEnumUnderlyingType();
        return (Enum)Enum.ToObject(enumType, GetBinaryReadMethod(t)!.Invoke(br, [])!);
    }

    /// <summary>
    /// Lee un <see cref="Guid"/>.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> desde el cual obtener.
    /// </param>
    /// <returns>
    /// <see cref="Guid"/> obtenido desde el
    /// <see cref="BinaryReader"/>.
    /// </returns>
    public static Guid ReadGuid(this BinaryReader br)
    {
        return new(br.ReadBytes(16));
    }

    /// <summary>
    /// Lee un <see cref="DateTime"/>.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> desde el cual obtener.
    /// </param>
    /// <returns>
    /// <see cref="DateTime"/> obtenido desde el
    /// <see cref="BinaryReader"/>.
    /// </returns>
    public static DateTime ReadDateTime(this BinaryReader br)
    {
        return DateTime.FromBinary(br.ReadInt64());
    }

    /// <summary>
    /// Lee un <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> desde el cual obtener.
    /// </param>
    /// <returns>
    /// <see cref="TimeSpan"/> obtenido desde el
    /// <see cref="BinaryReader"/>.
    /// </returns>
    public static TimeSpan ReadTimeSpan(this BinaryReader br)
    {
        return TimeSpan.FromTicks(br.ReadInt64());
    }

    /// <summary>
    /// Lee una cadena terminada en caracter nulo (<c>'\0'</c>), utilizando
    /// codificación <see cref="Encoding.UTF8"/> de manera predeterminada.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> desde el cual leer la cadena.
    /// </param>
    /// <returns>
    /// Una cadena terminada en caracter nulo (<c>'\0'</c>) leída desde el
    /// <see cref="BinaryReader"/>.
    /// </returns>
    public static string ReadNullTerminatedString(this BinaryReader br)
    {
        return ReadNullTerminatedString(br, Encoding.UTF8);
    }

    /// <summary>
    /// Lee una cadena terminada en caracter nulo (<c>'\0'</c>)
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> desde el cual leer la cadena.
    /// </param>
    /// <param name="encoding">
    /// Codificación a utilizar para leer la cadena.
    /// </param>
    /// <returns>
    /// Una cadena terminada en caracter nulo (<c>'\0'</c>) leída desde el
    /// <see cref="BinaryReader"/>.
    /// </returns>
    public static string ReadNullTerminatedString(this BinaryReader br, Encoding encoding)
    {
        IEnumerable<byte> GetBytes()
        {
            do
            {
                var b = br.ReadByte();
                if (b == 0) yield break;
                yield return b;
            } while (true);
        }
        return encoding.GetString(GetBytes().ToArray());
    }

    /// <summary>
    /// Lee un valor del tipo especificado desde el
    /// <paramref name="reader"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de valor a leer.</typeparam>
    /// <param name="reader">
    /// Instancia de <see cref="BinaryReader"/> desde la cual realizar
    /// la lectura.
    /// </param>
    /// <returns>
    /// El valor leído desde <paramref name="reader"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Se produce si no existe un método de lectura que pueda ser
    /// utilizado para leer el valor del tipo especificado.
    /// </exception>
    /// <remarks>
    /// Si conoce con precisión el tipo del valor a leer y existe un método
    /// implementado en la clase <see cref="BinaryReader"/> o existe una
    /// extensión definida para leer dicho valor, prefiera utilizar dicho
    /// método o extensión, ya que proveerán de mejor rendimiento. Además,
    /// si el valor a leer es una estructura simple definida por el
    /// usuario, puede utilizar el método
    /// <see cref="MarshalReadStruct{T}(BinaryReader)"/> para leer una estructura
    /// utilizando Marshaling, lo cual podría tener un mejor rendimiento
    /// que utilizar este método.
    /// </remarks>
    /// <seealso cref="MarshalReadStruct{T}(BinaryReader)"/>
    public static T Read<T>(this BinaryReader reader)
    {
        return (T)Read(reader, typeof(T));
    }

    /// <summary>
    /// Lee un valor del tipo especificado desde el
    /// <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">
    /// Instancia de <see cref="BinaryReader"/> desde la cual realizar
    /// la lectura.
    /// </param>
    /// <param name="type">Tipo de valor a leer.</param>
    /// <returns>
    /// El valor leído desde <paramref name="reader"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Se produce si no existe un método de lectura que pueda ser
    /// utilizado para leer el valor del tipo especificado.
    /// </exception>
    /// <remarks>
    /// Si conoce con precisión el tipo del valor a leer y existe un método
    /// implementado en la clase <see cref="BinaryReader"/> o existe una
    /// extensión definida para leer dicho valor, prefiera utilizar dicho
    /// método o extensión, ya que proveerán de mejor rendimiento. Además,
    /// si el valor a leer es una estructura simple definida por el
    /// usuario, puede utilizar el método
    /// <see cref="MarshalReadStruct{T}(BinaryReader)"/> para leer una
    /// estructura utilizando Marshaling, lo cual podría tener un mejor
    /// rendimiento que utilizar este método.
    /// 
    /// Además, considere que este método realizará las lecturas de
    /// estructuras leyendo los valores de los campos de la misma, tanto
    /// campos privados como públicos (por ende, incluyendo propiedades).
    /// </remarks>
    /// <seealso cref="MarshalReadStruct{T}(BinaryReader)"/>
    /// <seealso cref="ReadStruct{T}(BinaryReader)"/>
    public static object Read(this BinaryReader reader, Type type)
    {
        foreach (var j in DynamicReadSets)
        {
            if (j.Predicate(type))
            {
                return j.ReadDelegate(reader, type);
            }
        }

        return GetBinaryReadMethod(type)?.Invoke(reader, [])
            ?? LookupExMethod(type)?.Invoke(null, [reader])
            ?? (type.IsStruct() ? ByFieldReadStructInternal(reader, type) : default)
            ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Lee un tipo de arreglo desde el <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">
    /// Instancia de <see cref="BinaryReader"/> desde la cual realizar
    /// la lectura.
    /// </param>
    /// <param name="arrayType">Tipo de arreglo a leer.</param>
    /// <returns>
    /// Un <see cref="Array"/> del tipo especificado que ha sido leído desde el
    /// <see cref="BinaryReader"/> especificado.
    /// </returns>
    public static Array ReadArray(this BinaryReader reader, Type arrayType)
    {
        ReadArray_Contract(reader, arrayType);
        return ReadArray(reader, arrayType.GetElementType()!, arrayType.GetArrayRank());
    }

    /// <summary>
    /// Lee un arreglo unidimensional desde el <paramref name="reader"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos del arreglo.</typeparam>
    /// <param name="reader">
    /// Instancia de <see cref="BinaryReader"/> desde la cual realizar
    /// la lectura.
    /// </param>
    /// <returns>
    /// Un arreglo unidimensional cuyos elementos son del tipo especificado que
    /// ha sido leído desde el <see cref="BinaryReader"/> especificado.
    /// </returns>
    public static T[] ReadArray<T>(this BinaryReader reader)
    {
        return (T[])ReadArray(reader, typeof(T), 1);
    }

    /// <summary>
    /// Lee un tipo de arreglo desde el <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">
    /// Instancia de <see cref="BinaryReader"/> desde la cual realizar
    /// la lectura.
    /// </param>
    /// <param name="elementType">Tipo de elementos del arreglo.</param>
    /// <param name="dimensions">Cantidad de dimensiones del arreglo.</param>
    /// <returns>
    /// Un <see cref="Array"/> con las dimensiones especificadas y cuyos
    /// elementos son del tipo especificado que ha sido leído desde el
    /// <see cref="BinaryReader"/> especificado.
    /// </returns>
    public static Array ReadArray(this BinaryReader reader, Type elementType, int dimensions)
    {
        static bool Inc(int i, int[] counter, int[] indices)
        {
            if (++counter[i] == indices[i])
            {
                counter[i] = 0;
                return i <= 0 || Inc(i - 1, counter, indices);
            }
            return false;
        }
        int[] i = new int[dimensions];
        for (int j = 0; j < i.Length; j++)
        {
            i[j] = reader.ReadInt32();
        }
        Array a = Array.CreateInstance(elementType, i);
        int[] c = new int[i.Length];
        while (true)
        {
            a.SetValue(Read(reader, elementType), c);
            if (Inc(i.Length - 1, c, i)) break;
        }
        return a;
    }

    /// <summary>
    /// Lee una estructura simple desde el <paramref name="reader"/> por
    /// medio de Marshaling.
    /// </summary>
    /// <typeparam name="T">Tipo de valor a leer.</typeparam>
    /// <param name="reader">
    /// Instancia de <see cref="BinaryReader"/> desde la cual realizar
    /// la lectura.
    /// </param>
    /// <returns>
    /// El valor leído desde <paramref name="reader"/>.
    /// </returns>
    /// <remarks>
    /// Esta función se diferencia de <see cref="Read{T}(BinaryReader)"/>
    /// en que la lectura se hará exclusivamente utilizando técnicas de
    /// Marshaling, sin incluir las funciones predeterminadas y/o
    /// implementadas en la clase <see cref="BinaryReader"/> o un método de
    /// extensión conocido de la misma.
    /// 
    /// Este método también requiere que las cadenas sean anotadas con el
    /// atributo <see cref="MarshalAsAttribute"/>, estableciendo una
    /// constante de tamaño máximo esperado de la cadena. Para leer
    /// estructuras con cadenas de tamaño arbitrario, considere utilizar
    /// <see cref="ReadStruct{T}(BinaryReader)"/>.
    /// </remarks>
    /// <seealso cref="Read{T}(BinaryReader)"/>
    /// <seealso cref="ReadStruct{T}(BinaryReader)"/>
    public static T MarshalReadStruct<T>(this BinaryReader reader) where T : struct
    {
        ReadStruct_Contract(reader);
        return ByMarshalReadStructInternal<T>(reader);
    }

    /// <summary>
    /// Lee una estructura simple desde el <paramref name="reader"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de valor a leer.</typeparam>
    /// <param name="reader">
    /// Instancia de <see cref="BinaryReader"/> desde la cual realizar
    /// la lectura.
    /// </param>
    /// <returns>
    /// El valor leído desde <paramref name="reader"/>.
    /// </returns>
    /// <remarks>
    /// Este método permite leer estructuras basado en sus campos, tanto
    /// privados como públicos (por ende, incluyendo propiedades con campos
    /// de almacenamiento), siempre y cuando los mismos sean escribibles,
    /// es decir, no hayan sido declarados como <see langword="readonly"/>.
    /// 
    /// Si la estructura no contiene cadenas, o las mismas han sido
    /// anotadas con el atributo <see cref="MarshalAsAttribute"/>
    /// estableciendo la propiedad
    /// <see cref="MarshalAsAttribute.SizeConst"/>, considere utilizar el
    /// método <see cref="MarshalReadStruct{T}(BinaryReader)"/>, ya que
    /// éste puede proveer de mejor rendimiento.
    /// </remarks>
    /// <seealso cref="Read{T}(BinaryReader)"/>
    /// <seealso cref="MarshalReadStruct{T}(BinaryReader)"/>
    public static T ReadStruct<T>(this BinaryReader reader) where T : struct
    {
        ReadStruct_Contract(reader);
        return (T)ByFieldReadStructInternal(reader, typeof(T));
    }

    private static MethodInfo? LookupExMethod(Type t)
    {
        return typeof(BinaryReaderExtensions).GetMethods().FirstOrDefault(p =>
           p.Name.StartsWith("Read")
           && p.GetParameters().Length == 1
           && p.GetParameters().Single().ParameterType == typeof(BinaryReader)
           && p.ReturnType == t);
    }

    private static T ByMarshalReadStructInternal<T>(BinaryReader reader)
    {
        return (T)ByMarshalReadStructInternal(reader, typeof(T));
    }

    private static object ByMarshalReadStructInternal(BinaryReader reader, Type t)
    {
        object? obj = Activator.CreateInstance(t) ?? throw Errors.Tamper();
        int sze = Marshal.SizeOf(obj);
        IntPtr ptr = Marshal.AllocHGlobal(sze);
        Marshal.Copy(reader.ReadBytes(sze), 0, ptr, sze);
        obj = Marshal.PtrToStructure(ptr, t) ?? throw new InvalidDataException();
        Marshal.FreeHGlobal(ptr);
        return obj;
    }

    private static object ByFieldReadStructInternal(BinaryReader reader, Type t)
    {
        var c = SearchConstructor(t);
        object obj = c?.Invoke(ReadCtorParameters(c, reader).ToArray()) ?? Activator.CreateInstance(t) ?? throw Errors.Tamper();
        foreach (FieldInfo? j in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (!j.IsInitOnly) j.SetValue(obj, reader.Read(j.FieldType));
        }
        return obj;
    }

    private static ConstructorInfo? SearchConstructor(Type t)
    {
        static bool IsFieldMapped((FieldInfo f, ParameterInfo p) x) =>
            x.f.FieldType == x.p.ParameterType &&
            x.f.Name.StartsWith($"<{x.p.Name!.ToLowerInvariant()}>", StringComparison.InvariantCultureIgnoreCase);

        var props = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(p => p.IsInitOnly).ToArray();
        foreach (var c in t.GetConstructors())
        {
            if (!c.IsPublic) continue;
            var @params = c.GetParameters();
            if (props.Length != @params.Length) continue;
            if (props.Zip(@params).All(IsFieldMapped)) return c;
        }
        return null;
    }

    private static IEnumerable<object> ReadCtorParameters(ConstructorInfo c, BinaryReader reader)
    {
        foreach (var j in c.GetParameters()) 
        {
            yield return reader.Read(j.ParameterType);
        }
    }

    private static object DynamicReadISerializable(BinaryReader reader, Type type)
    {
        DataContractSerializer? d = new(type);
        return d.ReadObject(reader.ReadString().ToStream())!;
    }

    private static object DynamicReadEnum(BinaryReader r, Type t)
    {
        return Enum.ToObject(t, ReadEnum(r, t));
    }
}
