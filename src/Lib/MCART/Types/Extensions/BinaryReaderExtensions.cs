/*
BinaryReaderExtensions.cs

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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions for the <see cref="BinaryReader"/> class.
/// </summary>
[RequiresDynamicCode(AttributeErrorMessages.ClassCallsDynamicCode)]
[RequiresUnreferencedCode(AttributeErrorMessages.ClassHeavilyUsesReflection)]
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
    /// Gets a read method defined in the <see cref="BinaryReader"/> class that
    /// can be used to read an object of the specified type.
    /// </summary>
    /// <param name="t">
    /// Type for which to get a read method defined in the
    /// <see cref="BinaryReader"/> class.
    /// </param>
    /// <returns>
    /// A read method defined in the <see cref="BinaryReader"/> class that can
    /// be used to read an object of the specified type, or <see langword="null"/>
    /// if no read method exists for the specified type.
    /// </returns>
    public static MethodInfo? GetBinaryReadMethod(Type t)
    {
        return typeof(BinaryReader).GetMethods().FirstOrDefault(p =>
           p.Name.StartsWith("Read")
           && !p.Name.StartsWith("Read7BitEncodedInt") // This special method must be ignored.
           && p.Name.Length > 4
           && p.GetParameters().Length == 0
           && p.ReturnType == t);
    }

    /// <summary>
    /// Reads an enumeration value.
    /// </summary>
    /// <typeparam name="T">
    /// Type of enumeration value to read.
    /// </typeparam>
    /// <param name="br">
    /// <see cref="BinaryReader"/> from which to read.
    /// </param>
    /// <returns>
    /// Enumeration value read from the <see cref="BinaryReader"/>.
    /// </returns>
    public static T ReadEnum<T>(this BinaryReader br) where T : struct, Enum
    {
        return (T)ReadEnum(br, typeof(T));
    }

    /// <summary>
    /// Reads an enumeration value.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> from which to read.
    /// </param>
    /// <param name="enumType">
    /// Type of enumeration value to read.
    /// </param>
    /// <returns>
    /// Enumeration value read from the <see cref="BinaryReader"/>.
    /// </returns>
    [DebuggerStepThrough]
    public static Enum ReadEnum(this BinaryReader br, Type enumType)
    {
        Type? t = enumType.GetEnumUnderlyingType();
        return (Enum)Enum.ToObject(enumType, GetBinaryReadMethod(t)!.Invoke(br, [])!);
    }

    /// <summary>
    /// Reads a <see cref="Guid"/>.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> from which to read.
    /// </param>
    /// <returns>
    /// <see cref="Guid"/> read from the <see cref="BinaryReader"/>.
    /// </returns>
    public static Guid ReadGuid(this BinaryReader br)
    {
        return new(br.ReadBytes(16));
    }

    /// <summary>
    /// Reads a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> from which to read.
    /// </param>
    /// <returns>
    /// <see cref="DateTime"/> read from the <see cref="BinaryReader"/>.
    /// </returns>
    public static DateTime ReadDateTime(this BinaryReader br)
    {
        return DateTime.FromBinary(br.ReadInt64());
    }

    /// <summary>
    /// Reads a <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> from which to read.
    /// </param>
    /// <returns>
    /// <see cref="TimeSpan"/> read from the <see cref="BinaryReader"/>.
    /// </returns>
    public static TimeSpan ReadTimeSpan(this BinaryReader br)
    {
        return TimeSpan.FromTicks(br.ReadInt64());
    }

    /// <summary>
    /// Reads a null-terminated string (<c>'\0'</c>) using
    /// <see cref="Encoding.UTF8"/> by default.
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> from which to read the string.
    /// </param>
    /// <returns>
    /// A null-terminated string (<c>'\0'</c>) read from the
    /// <see cref="BinaryReader"/>.
    /// </returns>
    public static string ReadNullTerminatedString(this BinaryReader br)
    {
        return ReadNullTerminatedString(br, Encoding.UTF8);
    }

    /// <summary>
    /// Reads a null-terminated string (<c>'\0'</c>).
    /// </summary>
    /// <param name="br">
    /// <see cref="BinaryReader"/> from which to read the string.
    /// </param>
    /// <param name="encoding">
    /// Encoding to use for reading the string.
    /// </param>
    /// <returns>
    /// A null-terminated string (<c>'\0'</c>) read from the
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
        return encoding.GetString([.. GetBytes()]);
    }

    /// <summary>
    /// Reads a value of the specified type from the <paramref name="reader"/>.
    /// </summary>
    /// <typeparam name="T">Type of value to read.</typeparam>
    /// <param name="reader">
    /// Instance of <see cref="BinaryReader"/> from which to read.
    /// </param>
    /// <returns>
    /// The value read from <paramref name="reader"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no read method exists for the specified type.
    /// </exception>
    /// <remarks>
    /// If you know the exact type of the value to read and a method exists
    /// in the <see cref="BinaryReader"/> class or an extension is defined
    /// for reading that value, prefer using that method or extension for
    /// better performance. Additionally, if the value to read is a simple
    /// user-defined structure, you can use the
    /// <see cref="MarshalReadStruct{T}(BinaryReader)"/> method to read a
    /// structure using marshaling, which may provide better performance
    /// than using this method.
    /// </remarks>
    /// <seealso cref="MarshalReadStruct{T}(BinaryReader)"/>
    public static T Read<T>(this BinaryReader reader)
    {
        return (T)Read(reader, typeof(T));
    }

    /// <summary>
    /// Reads a value of the specified type from the <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">
    /// Instance of <see cref="BinaryReader"/> from which to read.
    /// </param>
    /// <param name="type">Type of value to read.</param>
    /// <returns>
    /// The value read from <paramref name="reader"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no read method exists for the specified type.
    /// </exception>
    /// <remarks>
    /// If you know the exact type of the value to read and a method exists
    /// in the <see cref="BinaryReader"/> class or an extension is defined
    /// for reading that value, prefer using that method or extension for
    /// better performance. Additionally, if the value to read is a simple
    /// user-defined structure, you can use the
    /// <see cref="MarshalReadStruct{T}(BinaryReader)"/> method to read a
    /// structure using marshaling, which may provide better performance
    /// than using this method.
    /// <br/><br/>
    /// Also, note that this method will read structures by reading the
    /// values of their fields, including both private and public fields
    /// (and therefore, properties).
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
    /// Reads an array of type <paramref name="arrayType"/> from the
    /// <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">
    /// Instance of <see cref="BinaryReader"/> from which to perform the read.
    /// </param>
    /// <param name="arrayType">Type of array to read.</param>
    /// <returns>
    /// An <see cref="Array"/> of the specified type that has been read from the
    /// specified <see cref="BinaryReader"/>.
    /// </returns>
    public static Array ReadArray(this BinaryReader reader, Type arrayType)
    {
        ReadArray_Contract(reader, arrayType);
        return ReadArray(reader, arrayType.GetElementType()!, arrayType.GetArrayRank());
    }

    /// <summary>
    /// Reads a one-dimensional array from the <paramref name="reader"/>.
    /// </summary>
    /// <typeparam name="T">Type of elements in the array.</typeparam>
    /// <param name="reader">
    /// Instance of <see cref="BinaryReader"/> from which to perform the read.
    /// </param>
    /// <returns>
    /// A one-dimensional array that has been read from the specified
    /// <see cref="BinaryReader"/>, whose elements are of the specified type.
    /// </returns>
    public static T[] ReadArray<T>(this BinaryReader reader)
    {
        return (T[])ReadArray(reader, typeof(T), 1);
    }

    /// <summary>
    /// Reads an array of type <paramref name="elementType"/> from the
    /// <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">
    /// Instance of <see cref="BinaryReader"/> from which to perform the read.
    /// </param>
    /// <param name="elementType">Type of elements in the array.</param>
    /// <param name="dimensions">Number of dimensions of the array.</param>
    /// <returns>
    /// An <see cref="Array"/> with the specified dimensions and whose elements are
    /// of the specified type that has been read from the specified
    /// <see cref="BinaryReader"/>.
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
    /// Reads a simple structure from the <paramref name="reader"/> using
    /// marshaling.
    /// </summary>
    /// <typeparam name="T">Type of value to read.</typeparam>
    /// <param name="reader">
    /// Instance of <see cref="BinaryReader"/> from which to perform the read.
    /// </param>
    /// <returns>
    /// The value read from <paramref name="reader"/>.
    /// </returns>
    /// <remarks>
    /// This function differs from <see cref="Read{T}(BinaryReader)"/> in that the
    /// read will be performed exclusively using marshaling techniques, without
    /// including the default functions and/or those implemented in the
    /// <see cref="BinaryReader"/> class or a known extension method of it.
    /// <br/><br/>
    /// This method also requires that strings and arrays be annotated with the
    /// <see cref="MarshalAsAttribute"/>, setting a constant maximum expected size
    /// for the string or array. To read structures with strings or arrays of
    /// arbitrary size, consider using <see cref="ReadStruct{T}(BinaryReader)"/>.
    /// </remarks>
    /// <seealso cref="Read{T}(BinaryReader)"/>
    /// <seealso cref="ReadStruct{T}(BinaryReader)"/>
    public static T MarshalReadStruct<T>(this BinaryReader reader) where T : struct
    {
        ReadStruct_Contract(reader);
        return ByMarshalReadStructInternal<T>(reader);
    }

    /// <summary>
    /// Reads an array of bytes starting on the specified offset.
    /// </summary>
    /// <param name="reader">
    /// <see cref="BinaryReader"/> to be used for reading.
    /// </param>
    /// <param name="offset">
    /// Offset from which to start reading the array.
    /// </param>
    /// <param name="count">Number of bytes to be read.</param>
    /// <returns>
    /// An array with <paramref name="count"/> bytes that has been read
    /// starting from <paramref name="offset"/>.
    /// </returns>
    public static byte[] ReadBytesAt(this BinaryReader reader, in long offset, in int count)
    {
        ReadBytesAt_Contract(reader, offset, count);
        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
        return reader.ReadBytes(count);
    }

    /// <summary>
    /// Reads an array of type <typeparamref name="T"/> by marshaling.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to be read. Must be a <see langword="struct"/>
    /// readable by marshaling.
    /// </typeparam>
    /// <param name="br">
    /// <see cref="BinaryReader"/> to be used for reading.
    /// </param>
    /// <param name="count">Number of elements to be read.</param>
    /// <returns>
    /// An array of <typeparamref name="T"/> containing
    /// <paramref name="count"/> elements.
    /// </returns>
    public static T[] MarshalReadArray<T>(this BinaryReader br, in int count) where T : struct
    {
        MarshalReadArray_Contract(br, count);
        return [.. Enumerable.Range(0, count).Select(_ => br.MarshalReadStruct<T>())];
    }

    /// <summary>
    /// Moves to a specific location in the <see cref="Stream"/> being read by
    /// the <see cref="BinaryReader"/> and reads an array of type
    /// <typeparamref name="T"/> by marshaling.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to be read. Must be a <see langword="struct"/>
    /// readable by marshaling.
    /// </typeparam>
    /// <param name="br">
    /// <see cref="BinaryReader"/> to be used for reading.
    /// </param>
    /// <param name="offset">
    /// Offset from which to start reading the array.</param>
    /// <param name="count">Number of elements to be read.
    /// </param>
    /// <returns>
    /// An array of <typeparamref name="T"/> containing
    /// <paramref name="count"/> elements.
    /// </returns>
    public static T[] MarshalReadArray<T>(this BinaryReader br, in long offset, in int count) where T : struct
    {
        MarshalReadArray_Contract(br, offset, count);
        br.BaseStream.Seek(offset, SeekOrigin.Begin);
        return MarshalReadArray<T>(br, count);
    }

    /// <summary>
    /// Reads a simple structure from the <paramref name="reader"/>.
    /// </summary>
    /// <typeparam name="T">Type of value to read.</typeparam>
    /// <param name="reader">
    /// Instance of <see cref="BinaryReader"/> from which to perform the read.
    /// </param>
    /// <returns>
    /// The value read from <paramref name="reader"/>.
    /// </returns>
    /// <remarks>
    /// This method allows reading structures based on their fields, both private
    /// and public (and therefore, including properties with backing fields), as
    /// long as they are writable, i.e., not declared as <see langword="readonly"/>.
    /// <br/><br/>
    /// If the structure does not contain arbitrarily-sized strings or arrays,
    /// or those have been annotated with the <see cref="MarshalAsAttribute"/>
    /// attribute setting the <see cref="MarshalAsAttribute.SizeConst"/>
    /// property, consider using the
    /// <see cref="MarshalReadStruct{T}(BinaryReader)"/> method, as it may
    /// provide better performance.
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

    private static T ByMarshalReadStructInternal<T>(BinaryReader reader) where T : struct
    {
        int sze = Marshal.SizeOf<T>();
        IntPtr ptr = Marshal.AllocHGlobal(sze);
        var bytes = reader.ReadBytes(sze);
        foreach (var j in typeof(T).GetFields())
        {
            if (j.GetAttribute<EndiannessAttribute>() is { Value: var e })
            {
                switch (e)
                {
                    case Endianness.BigEndian when BitConverter.IsLittleEndian:
                    case Endianness.LittleEndian when !BitConverter.IsLittleEndian:
                        Array.Reverse(bytes, (int)Marshal.OffsetOf<T>(j.Name), Marshal.SizeOf(j.FieldType));
                        break;
                }
            }
        }
        Marshal.Copy(bytes, 0, ptr, sze);
        T obj = Marshal.PtrToStructure<T>(ptr);
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
