/*
BinaryWriterExtensions.cs

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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions for the
/// <see cref="BinaryWriter"/> class.
/// </summary>
[RequiresUnreferencedCode(AttributeErrorMessages.ClassScansForTypes)]
[RequiresDynamicCode(AttributeErrorMessages.ClassCallsDynamicCode)]
public static partial class BinaryWriterExtensions
{
    private readonly record struct BwDynCheck(bool CanWrite, object? PredicateContext)
    {
        public BwDynCheck(bool CanWrite)
            : this(CanWrite, null)
        {
        }

        public static implicit operator BwDynCheck(bool value) => new(value);
    }

    private readonly record struct DynWriteSet(DynCheck Predicate, DynWrite WriteAction)
    {
        public DynWriteSet(Func<Type, bool> function, DynWrite action)
            : this(t => new(function(t), null), action)
        {
        }

        public DynWriteSet(Func<Type, bool> function, Action<BinaryWriter, object> action)
            : this(function, (_, bw, v) => action(bw, v))
        {
        }
    }

    private delegate BwDynCheck DynCheck(Type type);
    private delegate void DynWrite(object? predicateContext, BinaryWriter writer, object value);

    private static readonly DynWriteSet[] DynamicWriteSets =
    [
        new(t => t.IsArray, DynamicWriteArray),
        new(CanUseBinaryWriter, DynamicWriteBinaryWriter),
        new(CanUseBinaryWriterEx, DynamicWriteBinaryWriterEx),
        new(TypeExtensions.Implements<ISerializable>, DynamicWriteISerializable),
        new(TypeExtensions.IsStruct, ByFieldWriteStructInternal),
    ];

    /// <summary>
    /// Writes a <see cref="Guid"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The value to write.
    /// </param>
    public static void Write(this BinaryWriter bw, Guid value)
    {
        bw.Write(value.ToByteArray());
    }

    /// <summary>
    /// Writes a <see cref="DateTime"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The value to write.
    /// </param>
    public static void Write(this BinaryWriter bw, DateTime value)
    {
        bw.Write(value.ToBinary());
    }

    /// <summary>
    /// Writes a <see cref="TimeSpan"/> to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The value to write.
    /// </param>
    public static void Write(this BinaryWriter bw, TimeSpan value)
    {
        bw.Write(value.Ticks);
    }

    /// <summary>
    /// Writes an <see cref="Enum"/> value to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The value to write.
    /// </param>
    public static void Write(this BinaryWriter bw, Enum value)
    {
        bw.Write(value.ToBytes());
    }

    /// <summary>
    /// Writes a serializable object to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The serializable object to write.
    /// </param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static void Write(this BinaryWriter bw, ISerializable value)
    {
        DataContractSerializer? d = new(value.GetType());
        using MemoryStream? ms = new();
        d.WriteObject(ms, value);
        bw.Write(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
    }

    /// <summary>
    /// Performs a write of the specified object, dynamically determining
    /// the appropriate write method to use.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The object to write.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if it has not been possible to find a method that can
    /// write the specified value.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="bw"/> or <paramref name="value"/> are
    /// <see langword="null"/>.
    /// </exception>
    public static void DynamicWrite(this BinaryWriter bw, object value)
    {
        DynamicWrite_Contract(bw, value);
        DynamicInternalWrite(bw, value, value.GetType());
    }

    /// <summary>
    /// Writes a null-terminated string to the specified <see cref="BinaryWriter"/>,
    /// using UTF-8 encoding.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">The string to write.</param>
    public static void WriteNullTerminatedString(this BinaryWriter bw, string value)
    {
        WriteNullTerminatedString(bw, value, Encoding.UTF8);
    }

    /// <summary>
    /// Writes a null-terminated string to the specified <see cref="BinaryWriter"/>.
    /// </summary>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">The string to write.</param>
    /// <param name="encoding">
    /// The encoding to use for writing the string.
    /// </param>
    public static void WriteNullTerminatedString(this BinaryWriter bw, string value, Encoding encoding)
    {
        bw.Write([.. encoding.GetBytes(value), 0]);
    }

    /// <summary>
    /// Writes a simple structure to the underlying <see cref="Stream"/>
    /// via Marshaling.
    /// </summary>
    /// <typeparam name="T">Type of the structure.</typeparam>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The object to write.
    /// </param>
    public static int MarshalWriteStruct<T>(this BinaryWriter bw, T value) where T : struct
    {
        WriteStruct_Contract(bw);
        int sze = Marshal.SizeOf(value);
        byte[]? data = new byte[sze];
        IntPtr ptr = Marshal.AllocHGlobal(sze);
        Marshal.StructureToPtr(value, ptr, false);
        Marshal.Copy(ptr, data, 0, sze);
        Marshal.FreeHGlobal(ptr);
        foreach (var j in typeof(T).GetFields())
        {
            if (j.GetAttribute<EndiannessAttribute>() is { Value: var e })
            {
                switch (e)
                {
                    case Endianness.BigEndian when BitConverter.IsLittleEndian:
                    case Endianness.LittleEndian when !BitConverter.IsLittleEndian:
                        Array.Reverse(data, (int)Marshal.OffsetOf<T>(j.Name), Marshal.SizeOf(j.FieldType));
                        break;
                }
            }
        }
        bw.Write(data);
        return sze;
    }

    /// <summary>
    /// Writes an array of structures to the underlying stream using
    /// Marshaling.
    /// </summary>
    /// <typeparam name="T">Type of structure array to write.</typeparam>
    /// <param name="bw">Binary writer to use when writing the array.</param>
    /// <param name="array">Array of values to write.</param>
    /// <returns>
    /// The number of bytes written to the stream.
    /// </returns>
    public static int MarshalWriteStructArray<T>(this BinaryWriter bw, T[] array) where T : struct
    {
        WriteStruct_Contract(bw);
        int sizeOf = Marshal.SizeOf<T>();
        int dataSize = sizeOf * array.Length;
        nint ptr = Marshal.AllocHGlobal(dataSize);
        for (int i = 0; i < array.Length; i++)
        {
            Marshal.StructureToPtr(array[i], ptr + (i * sizeOf), false);
        }
        byte[] data = new byte[dataSize];
        Marshal.Copy(ptr, data, 0, dataSize);
        Marshal.FreeHGlobal(ptr);
        foreach (var j in typeof(T).GetFields())
        {
            MarshalWriteStructArray_CheckFieldEndianness(j, data, array, sizeOf);
        }
        bw.Write(data);
        return data.Length;
    }

    /// <summary>
    /// Writes a simple structure to the underlying <see cref="Stream"/>
    /// </summary>
    /// <typeparam name="T">Type of the structure.</typeparam>
    /// <param name="bw">
    /// The instance to perform the writing on.
    /// </param>
    /// <param name="value">
    /// The object to write.
    /// </param>
    public static void WriteStruct<T>(this BinaryWriter bw, T value) where T : struct
    {
        WriteStruct_Contract(bw, typeof(T));
        ByFieldWriteStructInternal(bw, value);
    }

    private static void ByFieldWriteStructInternal(BinaryWriter writer, object obj)
    {
        foreach (FieldInfo? j in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            writer.DynamicWrite(j.GetValue(obj) ?? throw Errors.FieldIsNull(j));
        }
    }

    private static bool CanWrite(MethodInfo p, Type t)
    {
        ParameterInfo[]? l = p.GetParameters();
        return p.IsVoid()
            && p.Name == "Write"
            && l.Length == 1
            && l.Single().ParameterType == t;
    }

    private static bool CanExWrite(MethodInfo p, Type t)
    {
        ParameterInfo[]? l = p.GetParameters();
        return p.IsVoid()
            && p.Name == "Write"
            && l.Length == 2
            && l.First().ParameterType == typeof(BinaryWriter)
            && l.Last().ParameterType == t;
    }

    private static void DynamicInternalWrite(BinaryWriter bw, object value, Type t)
    {
        foreach (var j in DynamicWriteSets)
        {
            var p = j.Predicate(t);
            if (p.CanWrite)
            {
                j.WriteAction(p.PredicateContext, bw, value);
                return;
            }
        }
        throw Errors.CantWriteObj(value.GetType());
    }

    private static BwDynCheck CanUseBinaryWriter(Type t)
    {
        return typeof(BinaryWriter).GetMethods().FirstOrDefault(p => CanWrite(p, t)) is { } m 
            ? new (true, m) 
            : new (false);
    }

    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    private static BwDynCheck CanUseBinaryWriterEx(Type t)
    {
        return typeof(BinaryWriterExtensions).GetMethods().FirstOrDefault(p => CanExWrite(p, t)) is { } m
            ? new (true, m)
            : new(false);
    }

    private static void DynamicWriteArray(BinaryWriter bw, object value)
    {
        var a = (Array)value;
        for (int j = 0; j < a.Rank; j++)
        {
            bw.Write(a.GetLength(j));
        }
        foreach (var j in a)
        {
            DynamicInternalWrite(bw, j, a.GetType().GetElementType()!);
        }
    }

    private static void DynamicWriteBinaryWriter(object? m, BinaryWriter bw, object value)
    {
        ((MethodInfo)m!).Invoke(bw, [value]);
    }

    private static void DynamicWriteBinaryWriterEx(object? m, BinaryWriter bw, object value)
    {
        ((MethodInfo)m!).Invoke(null, [bw, value]);
    }

    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    private static void DynamicWriteISerializable(BinaryWriter bw, object value)
    {
        Write(bw, (ISerializable)value);
    }

    private static void MarshalWriteStructArray_CheckFieldEndianness<T>(FieldInfo j, byte[] data, T[] array, int sizeOf) where T : struct
    {
        if (j.GetAttribute<EndiannessAttribute>() is { Value: var e })
        {
            switch (e)
            {
                case Endianness.BigEndian when BitConverter.IsLittleEndian:
                case Endianness.LittleEndian when !BitConverter.IsLittleEndian:
                    MarshalWriteStructArray_SwitchFieldEndianness(j, data, array, sizeOf);
                    break;
            }
        }
    }
    private static void MarshalWriteStructArray_SwitchFieldEndianness<T>(FieldInfo j, byte[] data, T[] array, int sizeOf) where T : struct
    {
        var fieldOffset = (int)Marshal.OffsetOf<T>(j.Name);
        var sizeOfField = Marshal.SizeOf(j.FieldType);
        foreach (var (index, _) in array.WithIndex())
        {
            Array.Reverse(data, fieldOffset + (sizeOf * index), sizeOfField);
        }
    }
}
