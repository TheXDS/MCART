/*
BinaryWriterExtensions.cs

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

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la clase
/// <see cref="BinaryWriter"/>.
/// </summary>
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
    {
        new(t => t.IsArray, DynamicWriteArray),
        new(CanUseBinaryWriter, DynamicWriteBinaryWriter),
        new(CanUseBinaryWriterEx, DynamicWriteBinaryWriterEx),
        new(TypeExtensions.Implements<ISerializable>, DynamicWriteISerializable),
        new(TypeExtensions.IsStruct, ByFieldWriteStructInternal),
    };

    /// <summary>
    /// Escribe un <see cref="Guid"/> en el <see cref="BinaryWriter"/>
    /// especificado.
    /// </summary>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Valor a escribir.
    /// </param>
    public static void Write(this BinaryWriter bw, Guid value)
    {
        bw.Write(value.ToByteArray());
    }

    /// <summary>
    /// Escribe un <see cref="DateTime"/> en el
    /// <see cref="BinaryWriter"/> especificado.
    /// </summary>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Valor a escribir.
    /// </param>
    public static void Write(this BinaryWriter bw, DateTime value)
    {
        bw.Write(value.ToBinary());
    }

    /// <summary>
    /// Escribe un <see cref="TimeSpan"/> en el
    /// <see cref="BinaryWriter"/> especificado.
    /// </summary>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Valor a escribir.
    /// </param>
    public static void Write(this BinaryWriter bw, TimeSpan value)
    {
        bw.Write(value.Ticks);
    }

    /// <summary>
    /// Escribe un valor <see cref="Enum"/> en el
    /// <see cref="BinaryWriter"/> especificado.
    /// </summary>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Valor a escribir.
    /// </param>
    public static void Write(this BinaryWriter bw, Enum value)
    {
        bw.Write(value.ToBytes());
    }

    /// <summary>
    /// Escribe un objeto serializable en el
    /// <see cref="BinaryWriter"/> especificado.
    /// </summary>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Objeto serializable a escribir.
    /// </param>
    public static void Write(this BinaryWriter bw, ISerializable value)
    {
        DataContractSerializer? d = new(value.GetType());
        using MemoryStream? ms = new();
        d.WriteObject(ms, value);
        bw.Write(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
    }

    /// <summary>
    /// Realiza una escritura del objeto especificado, determinando
    /// dinámicamente el método apropiado de escritura a utilizar.
    /// </summary>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Objeto a escribir.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Se produce si no ha sido posible encontrar un método que pueda
    /// escribir el valor especificado. 
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="bw"/> o <paramref name="value"/> son
    /// <see langword="null"/>.
    /// </exception>
    public static void DynamicWrite(this BinaryWriter bw, object value)
    {
        DynamicWrite_Contract(bw, value);
        DynamicInternalWrite(bw, value, value.GetType());
    }

    /// <summary>
    /// Escribe una estructura simple en el <see cref="Stream"/>
    /// subyacente por medio de Marshaling.
    /// </summary>
    /// <typeparam name="T">Tipo de la estructura.</typeparam>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Objeto a escribir.
    /// </param>
    public static void MarshalWriteStruct<T>(this BinaryWriter bw, T value) where T : struct
    {
        WriteStruct_Contract(bw);
        int sze = Marshal.SizeOf(value);
        byte[]? arr = new byte[sze];
        IntPtr ptr = Marshal.AllocHGlobal(sze);
        Marshal.StructureToPtr(value, ptr, true);
        Marshal.Copy(ptr, arr, 0, sze);
        Marshal.FreeHGlobal(ptr);
        bw.Write(arr);
    }

    /// <summary>
    /// Escribe una estructura simple en el <see cref="Stream"/>
    /// subyacente.
    /// </summary>
    /// <typeparam name="T">Tipo de la estructura.</typeparam>
    /// <param name="bw">
    /// Instancia sobre la cual realizar la escritura.
    /// </param>
    /// <param name="value">
    /// Objeto a escribir.
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
        ((MethodInfo)m!).Invoke(bw, new[] { value });
    }

    private static void DynamicWriteBinaryWriterEx(object? m, BinaryWriter bw, object value)
    {
        ((MethodInfo)m!).Invoke(null, new[] { bw, value });
    }

    private static void DynamicWriteISerializable(BinaryWriter bw, object value)
    {
        Write(bw, (ISerializable)value);
    }
}
