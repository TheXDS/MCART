/*
Objects.cs

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
using System.Reflection;
using System.Runtime.InteropServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Extensions;
using static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Object manipulation functions.
/// </summary>
public static partial class Objects
{
    /// <summary>
    /// Determines if all objects are <see langword="null" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if all objects are <see langword="null" />;
    /// otherwise, <see langword="false" />.
    /// </returns>
    /// <param name="collection">Objects to check.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="collection"/> is <see langword="null"/>.
    /// </exception>
    public static bool AreAllNull(params object?[] collection)
    {
        return collection.AreAllNull();
    }

    /// <summary>
    /// Gets a value of type <typeparamref name="T"/> from a byte array
    /// using marshaling.
    /// </summary>
    /// <typeparam name="T">
    /// Type of value to retrieve. Must be a structure.
    /// </typeparam>
    /// <param name="rawBytes">Bytes to convert into the value.</param>
    /// <returns>
    /// A value of type <typeparamref name="T"/> created from the byte array.
    /// </returns>
    /// <remarks>
    /// Fields of type <see cref="string"/> in the structure must be
    /// decorated with:
    /// <code>
    /// [MarshalAs(UnmanagedType.ByValTStr, SizeConst = &lt;max size&gt;)]
    /// </code>
    /// </remarks>
    /// <seealso cref="GetBytes{T}(T)"/>
    public static T FromBytes<[DynamicallyAccessedMembers(PublicConstructors | NonPublicConstructors)] T>(byte[] rawBytes) where T : struct
    {
        ArgumentNullException.ThrowIfNull(rawBytes);
        int sze = rawBytes.Length;
        IntPtr ptr = IntPtr.Zero;
        try
        {
            ptr = Marshal.AllocHGlobal(sze);
            Marshal.Copy(rawBytes, 0, ptr, sze);
            return Marshal.PtrToStructure<T>(ptr);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Marshal.FreeHGlobal(ptr);
        }
    }

    /// <summary>
    /// Returns the attribute associated with the specified type declaration.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit from <see cref="Attribute" />.
    /// </typeparam>
    /// <typeparam name="TIt">Type from which to extract the attribute.</typeparam>
    /// <returns>
    /// An attribute of type <typeparamref name="T" /> with the data
    /// associated with the type declaration.
    /// </returns>
    [Sugar]
    public static T? GetAttribute<T, TIt>() where T : Attribute
    {
        typeof(TIt).HasAttribute(out T? attribute);
        return attribute;
    }

    /// <summary>
    /// Gets a byte array from a value of type <typeparamref name="T"/>
    /// using marshaling.
    /// </summary>
    /// <typeparam name="T">Type of value to convert to bytes.</typeparam>
    /// <param name="value">Value to convert to bytes.</param>
    /// <returns>
    /// A byte array that can be used to reconstruct the value.
    /// </returns>
    /// <remarks>
    /// Fields of type <see cref="string"/> in the structure must be
    /// decorated with:
    /// <code>
    /// [MarshalAs(UnmanagedType.ByValTStr, SizeConst = &lt;max size&gt;)]
    /// </code>
    /// </remarks>
    /// <seealso cref="FromBytes{T}(byte[])"/>
    public static byte[] GetBytes<T>(T value) where T : struct
    {
        int sze = Marshal.SizeOf(value);
        IntPtr ptr = IntPtr.Zero;
        try
        {
            byte[]? arr = new byte[sze];
            ptr = Marshal.AllocHGlobal(sze);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, arr, 0, sze);
            return arr;
        }
        finally
        {
            if (ptr != IntPtr.Zero) Marshal.FreeHGlobal(ptr);
        }
    }

    /// <summary>
    /// Determines if any of the objects is <see langword="null" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if any object is <see langword="null" />;
    /// otherwise, <see langword="false" />.
    /// </returns>
    /// <param name="x">Objects to check.</param>
    public static bool IsAnyNull(params object?[] x)
    {
        return x.IsAnyNull();
    }

    /// <summary>
    /// Returns a circular reference to this object.
    /// </summary>
    /// <returns>This object.</returns>
    /// <param name="obj">Object.</param>
    /// <typeparam name="T">Type of this object.</typeparam>
    /// <remarks>
    /// This function is only useful when using Visual Basic with the
    /// <c lang="VB">With</c> structure.
    /// </remarks>
    [Sugar]
    public static T Itself<T>(this T obj)
    {
        return obj;
    }

    /// <summary>
    /// Creates a new copy of the object.
    /// </summary>
    /// <typeparam name="T">Type of object to clone.</typeparam>
    /// <param name="source">Source object.</param>
    /// <returns>
    /// A new instance of type <typeparamref name="T"/> with the same
    /// values as <paramref name="source"/> in its public fields and
    /// read/write properties.
    /// </returns>
    public static T ShallowClone<[DynamicallyAccessedMembers(PublicFields | PublicProperties)] T>(this T source) where T : notnull, new()
    {
        T copy = new();
        ShallowCopyTo(source, copy);
        return copy;
    }

    /// <summary>
    /// Copies the property values from one object to another.
    /// </summary>
    /// <typeparam name="T">Type of objects to operate on.</typeparam>
    /// <param name="source">Source object.</param>
    /// <param name="destination">Destination object.</param>
    public static void ShallowCopyTo<[DynamicallyAccessedMembers(PublicFields | PublicProperties)] T>(this T source, T destination) where T : notnull
    {
        ShallowCopyTo_Contract(source, destination);
        foreach (FieldInfo j in typeof(T).GetFields().Where(p => p.IsPublic && !p.IsInitOnly && !p.IsLiteral))
        {
            j.SetValue(destination, j.GetValue(source));
        }
        foreach (PropertyInfo j in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            j.SetValue(destination, j.GetValue(source));
        }
    }

    /// <summary>
    /// Copies the property values from one object to another.
    /// </summary>
    /// <param name="source">Source object.</param>
    /// <param name="destination">Destination object.</param>
    /// <param name="objectType">
    /// Type of objects to copy. Limits the copy to properties and fields
    /// accessible within the specified type.
    /// </param>
    public static void ShallowCopyTo(this object source, object destination, [DynamicallyAccessedMembers(PublicFields | PublicProperties)] Type objectType)
    {
        ShallowCopyTo_Contract(source, destination, objectType);
        foreach (FieldInfo j in objectType.GetFields().Where(p => p.IsPublic && !p.IsInitOnly && !p.IsLiteral))
        {
            j.SetValue(destination, j.GetValue(source));
        }
        foreach (PropertyInfo j in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            j.SetValue(destination, j.GetValue(source));
        }
    }

    /// <summary>
    /// Gets a list of the types of the specified objects.
    /// </summary>
    /// <param name="objects">Objects to generate the type collection from.</param>
    /// <returns>
    /// A list of the types of the provided objects.
    /// </returns>
    public static IEnumerable<Type> ToTypes(params object[] objects)
    {
        return objects.ToTypes();
    }

    /// <summary>
    /// Encapsulates <see cref="Delegate.CreateDelegate(Type, object, string, bool, bool)"/>
    /// to ensure all possible exceptions are caught.
    /// </summary>
    /// <typeparam name="T">Type of delegate to create.</typeparam>
    /// <param name="method">Method to create the delegate from.</param>
    /// <param name="instance">Instance to bind the delegate to.</param>
    /// <param name="delegate">
    /// Created delegate. <see langword="null"/> if the delegate could not
    /// be created.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the delegate was successfully created;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method was created due to a quirk in
    /// <see cref="Delegate.CreateDelegate(Type, object, string, bool, bool)"/>,
    /// where it may still throw an exception if the method contains
    /// generic parameters.
    /// </remarks>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodCreatesDelegates)]
    public static bool TryCreateDelegate<T>(MethodInfo method, object instance, [NotNullWhen(true)] out T? @delegate) where T : notnull, Delegate
    {
        try
        {
            @delegate = Delegate.CreateDelegate(typeof(T), instance, method.Name, false, false) as T;
            return @delegate is not null;
        }
        catch
        {
            @delegate = null;
            return false;
        }
    }

    /// <summary>
    /// Safe version of <see cref="Delegate.CreateDelegate(Type, MethodInfo, bool)"/>.
    /// </summary>
    /// <typeparam name="T">Type of delegate to create.</typeparam>
    /// <param name="method">Method to create the delegate from.</param>
    /// <param name="delegate">
    /// Created delegate. <see langword="null"/> if the delegate could not
    /// be created.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the delegate was successfully created;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method was created due to a quirk in
    /// <see cref="Delegate.CreateDelegate(Type, MethodInfo, bool)"/>,
    /// where it may still throw an exception if the method contains
    /// generic parameters.
    /// </remarks>
    public static bool TryCreateDelegate<T>(MethodInfo method, [NotNullWhen(true)] out T? @delegate) where T : notnull, Delegate
    {
        try
        {
            @delegate = Delegate.CreateDelegate(typeof(T), method, false) as T;
            return @delegate is not null;
        }
        catch
        {
            @delegate = null;
            return false;
        }
    }

    /// <summary>
    /// Determines which objects are <see langword="null" />.
    /// </summary>
    /// <returns>
    /// An enumerator with the indices of objects that are <see langword="null" />.
    /// </returns>
    /// <param name="collection">Collection of objects to check.</param>
    public static IEnumerable<int> WhichAreNull(params object?[] collection)
    {
        return collection.WhichAreNull();
    }
}
