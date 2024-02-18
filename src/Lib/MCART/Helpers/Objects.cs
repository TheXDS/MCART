/*
Objects.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Funciones de manipulación de objetos.
/// </summary>
public static partial class Objects
{
    /// <summary>
    /// Determina si todos los objetos son <see langword="null" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" />, si todos los objetos son <see langword="null" />; de lo contrario,
    /// <see langword="false" />.
    /// </returns>
    /// <param name="collection">Objetos a comprobar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="collection"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static bool AreAllNull(params object?[] collection)
    {
        return collection.AreAllNull();
    }

    /// <summary>
    /// Obtiene un valor de tipo <typeparamref name="T"/> a partir de un
    /// arreglo de bytes utilizando Marshaling.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valor a obtener. Debe ser una estructura.
    /// </typeparam>
    /// <param name="rawBytes">
    /// Bytes desde los cuales obtener el valor.
    /// </param>
    /// <returns>
    /// Un valor de tipo <typeparamref name="T"/> creado a partir del
    /// arreglo de bytes.
    /// </returns>
    /// <remarks>
    /// Será necesario decorar los campos de tipo <see cref="string"/> de
    /// la estructura con el siguiente snippet de código:
    /// <code>
    /// [MarshalAs(UnmanagedType.ByValTStr, SizeConst = &lt;tamaño máximo&gt;)]
    /// </code>
    /// </remarks>
    /// <seealso cref="GetBytes{T}(T)"/>
    public static T FromBytes<T>(byte[] rawBytes) where T : struct
    {
        FromBytes_Contract(rawBytes);
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
    /// Devuelve el atributo asociado a la declaración del tipo
    /// especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de atributo a devolver. Debe heredar
    /// <see cref="Attribute" />.
    /// </typeparam>
    /// <typeparam name="TIt">
    /// Tipo del cual se extraerá el atributo.
    /// </typeparam>
    /// <returns>
    /// Un atributo del tipo <typeparamref name="T" /> con los datos
    /// asociados en la declaración del tipo.
    /// </returns>
    [Sugar]
    public static T? GetAttribute<T, TIt>() where T : Attribute
    {
        typeof(TIt).HasAttribute(out T? attribute);
        return attribute;
    }

    /// <summary>
    /// Obtiene un arreglo de bytes a partir de un valor de tipo
    /// <typeparamref name="T"/> utilizando Marshaling.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de valor desde el cual obtener el arreglo de bytes.
    /// </typeparam>
    /// <param name="value">Valor desde el cual obtener los bytes.</param>
    /// <returns>
    /// Un arreglo de bytes con el cual es posible reconstruir el valor.
    /// </returns>
    /// <remarks>
    /// Será necesario decorar los campos de tipo <see cref="string"/> de
    /// la estructura con el siguiente snippet de código:
    /// <code>
    /// [MarshalAs(UnmanagedType.ByValTStr, SizeConst = &lt;tamaño máximo&gt;)]
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
    /// Determina si cualquiera de los objetos es <see langword="null" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" />, si cualquiera de los objetos es <see langword="null" />; de lo
    /// contrario, <see langword="false" />.
    /// </returns>
    /// <param name="x">Objetos a comprobar.</param>
    public static bool IsAnyNull(params object?[] x)
    {
        return x.IsAnyNull();
    }

    /// <summary>
    /// Devuelve una referencia circular a este mismo objeto.
    /// </summary>
    /// <returns>Este objeto.</returns>
    /// <param name="obj">Objeto.</param>
    /// <typeparam name="T">Tipo de este objeto.</typeparam>
    /// <remarks>
    /// Esta función es únicamente útil al utilizar Visual
    /// Basic en conjunto con la estructura <c lang="VB">With</c>.
    /// </remarks>
    [Sugar]
    public static T Itself<T>(this T obj)
    {
        return obj;
    }

    /// <summary>
    /// Crea una nueva copia del objeto.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a clonar.</typeparam>
    /// <param name="source">Objeto de origen.</param>
    /// <returns>
    /// Una nueva instancia de tipo <typeparamref name="T"/>, que contiene
    /// los mismos valores que <paramref name="source"/> en sus campos
    /// públicos y propiedades de lectura/escritura.
    /// </returns>
    public static T ShallowClone<T>(this T source) where T : notnull, new()
    {
        T copy = new();
        ShallowCopyTo(source, copy);
        return copy;
    }

    /// <summary>
    /// Copia el valor de las propiedades de un objeto a otro.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objetos sobre los cuales ejecutar la operación.
    /// </typeparam>
    /// <param name="source">Objeto de origen.</param>
    /// <param name="destination">Objeto de destino.</param>
    public static void ShallowCopyTo<T>(this T source, T destination) where T : notnull
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
    /// Obtiene una lista de los tipos de los objetos especificados.
    /// </summary>
    /// <param name="objects">
    /// Objetos a partir de los cuales generar la colección de tipos.
    /// </param>
    /// <returns>
    /// Una lista compuesta por los tipos de los objetos provistos.
    /// </returns>
    public static IEnumerable<Type> ToTypes(params object[] objects)
    {
        return objects.ToTypes();
    }

    /// <summary>
    /// Encapsula <see cref="Delegate.CreateDelegate(Type, object, string, bool, bool)"/>
    /// para garantizar la captura de todas las excepciones posibles.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo del delegado a crear.
    /// </typeparam>
    /// <param name="method">
    /// Método desde el cual se creará el delegado.
    /// </param>
    /// <param name="instance">Instancia hacia la cual enlazar el delegado a crear.</param>
    /// <param name="delegate">Delegado que ha sido creado. <see langword="null"/> si no fue posible crear el delegado especificado.</param>
    /// <returns>
    /// <see langword="true"/> si se ha creado el delegado de forma
    /// satisfactoria, <see langword="false"/> en caso contrario.
    /// </returns>
    /// <remarks>
    /// Este método se creó debido a un Quirk de funcionamiento del
    /// método
    /// <see cref="Delegate.CreateDelegate(Type, object, string, bool, bool)"/>,
    /// en el cual el mismo aún podría arrojar una excepción cuando no
    /// es posible enlazar un método a un delegado si el método
    /// contiene parámetros genéricos.
    /// </remarks>
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
    /// Versión segura de <see cref="Delegate.CreateDelegate(Type, MethodInfo, bool)"/>
    /// </summary>
    /// <typeparam name="T">
    /// Tipo del delegado a crear.
    /// </typeparam>
    /// <param name="method">
    /// Método desde el cual se creará el delegado.
    /// </param>
    /// <param name="delegate">
    /// Delegado que ha sido creado. <see langword="null"/> si no fue
    /// posible crear el delegado especificado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si se ha creado el delegado de forma
    /// satisfactoria, <see langword="false"/> en caso contrario.
    /// </returns>
    /// <remarks>
    /// Este método se creó debido a un Quirk de funcionamiento del
    /// método
    /// <see cref="Delegate.CreateDelegate(Type, MethodInfo, bool)"/>,
    /// en el cual el mismo aún podría arrojar una excepción cuando no
    /// es posible enlazar un método a un delegado si el método
    /// contiene parámetros genéricos.
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
    /// Determina si cualquiera de los objetos es <see langword="null" />.
    /// </summary>
    /// <returns>
    /// Un enumerador con los índices de los objetos que son <see langword="null" />.
    /// </returns>
    /// <param name="collection">Colección de objetos a comprobar.</param>
    public static IEnumerable<int> WhichAreNull(params object?[] collection)
    {
        return collection.WhichAreNull();
    }
}
