/*
Objects.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

namespace TheXDS.MCART.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Misc.Internals;
using static TheXDS.MCART.Types.Extensions.EnumerableExtensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;

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
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    public static IEnumerable<T> FindAllObjects<T>() where T : notnull
    {
        return FindAllObjects<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidientes.
    /// </param>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FindAllObjects<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindAllObjects<T>(null, typeFilter);
    }

    /// <summary>
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    public static IEnumerable<T> FindAllObjects<T>(IEnumerable? ctorArgs) where T : notnull
    {
        return GetTypes<T>(true).NotNull().Select(p => p.New<T>(false, ctorArgs)).Where(p => p is not null)!;
    }

    /// <summary>
    /// Instancia todos los objetos del tipo especificado,
    /// devolviéndolos en una enumeración.
    /// </summary>
    /// <typeparam name="T">Tipo de objetos a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidentes.
    /// </param>
    /// <returns>
    /// Una enumeración de todas las instancias de objeto de tipo
    /// <typeparamref name="T"/> encontradas.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<T> FindAllObjects<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        NullCheck(typeFilter, nameof(typeFilter));
        return GetTypes<T>(true).NotNull().Where(typeFilter).Select(p => p.New<T>(false, ctorArgs)).Where(p => p is not null)!;
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindFirstObject<T>() where T : notnull
    {
        return FindFirstObject<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidientes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindFirstObject<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindFirstObject<T>(null, typeFilter);
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindFirstObject<T>(IEnumerable? ctorArgs) where T : notnull
    {
        Type? t = GetTypes<T>().FirstOrDefault(p => p.IsInstantiable(ctorArgs?.ToTypes().ToArray() ?? Type.EmptyTypes));
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Obtiene al primer objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidientes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindFirstObject<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        NullCheck(typeFilter, nameof(typeFilter));
        Type? t = GetTypes<T>(true).NotNull().FirstOrDefault(typeFilter);
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindSingleObject<T>() where T : notnull
    {
        return FindSingleObject<T>((IEnumerable?)null);
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidientes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindSingleObject<T>(Func<Type, bool> typeFilter) where T : notnull
    {
        return FindSingleObject<T>(null, typeFilter);
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    public static T? FindSingleObject<T>(IEnumerable? ctorArgs) where T : notnull
    {
        Type? t = GetTypes<T>(true).NotNull().SingleOrDefault();
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Obtiene un único objeto que coincida con el tipo base
    /// especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a buscar.</typeparam>
    /// <param name="ctorArgs">
    /// Argumentos a pasar al constructor de instancia de la clase.
    /// </param>
    /// <param name="typeFilter">
    /// Función de filtro a aplicar a los tipos coincidientes.
    /// </param>
    /// <returns>
    /// Una nueva instancia del objeto solicitado, o
    /// <see langword="null"/> si no se encuentra ningún tipo
    /// coincidente.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="typeFilter"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static T? FindSingleObject<T>(IEnumerable? ctorArgs, Func<Type, bool> typeFilter) where T : notnull
    {
        NullCheck(typeFilter, nameof(typeFilter));
        Type? t = GetTypes<T>(true).NotNull().SingleOrDefault(typeFilter);
        return t is not null ? t.New<T>(false, ctorArgs) : default;
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> actual un tipo que contenga el
    /// <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static Type? FindType(string identifier)
    {
        return FindType<object>(identifier);
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> especificado un tipo que
    /// contenga el <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <param name="domain">Dominio en el cual buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> o
    /// <paramref name="domain"/> son <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static Type? FindType(string identifier, AppDomain domain)
    {
        return FindType<object>(identifier, domain);
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> actual un tipo que contenga el
    /// <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> es
    /// <see langword="null"/>.
    /// </exception>
    [Sugar]
    public static Type? FindType<T>(string identifier)
    {
        return FindType<T>(identifier, AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Busca en el <see cref="AppDomain" /> especificado un tipo que
    /// contenga el <see cref="IdentifierAttribute" /> especificado.
    /// </summary>
    /// <typeparam name="T">Restringir búsqueda a estos tipos.</typeparam>
    /// <param name="identifier">Identificador a buscar.</param>
    /// <param name="domain">Dominio en el cual buscar.</param>
    /// <returns>
    /// Un tipo que ha sido etiquetado con el identificador especificado,
    /// o <see langword="null" /> si ningún tipo contiene el identificador.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="identifier"/> o
    /// <paramref name="domain"/> son <see langword="null"/>.
    /// </exception>
    public static Type? FindType<T>(string identifier, AppDomain domain)
    {
        EmptyCheck(identifier, nameof(identifier));
        NullCheck(domain, nameof(domain));
        return GetTypes<T>(domain)
            .FirstOrDefault(j => j.GetCustomAttributes(typeof(IdentifierAttribute), false)
                .Cast<IdentifierAttribute>()
                .Any(k => k.Value == identifier));
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
    public static T? GetAttr<T, TIt>() where T : Attribute
    {
        typeof(TIt).HasAttr<T>(out T? attr);
        return attr;
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
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro de <see cref="AppDomain.CurrentDomain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos ls ensamblados dentro del dominio
    /// actual. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    public static IEnumerable<Type> GetTypes<T>()
    {
        return typeof(T).Derivates();
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada dentro del <see cref="AppDomain" /> especificado.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="domain">
    /// <see cref="AppDomain" /> en el cual realizar la búsqueda.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro del <paramref name="domain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos ls ensamblados dentro del dominio
    /// especificado. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    [Sugar]
    public static IEnumerable<Type> GetTypes<T>(AppDomain domain)
    {
        return typeof(T).Derivates(domain);
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada dentro del <see cref="AppDomain" /> especificado.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="domain">
    /// <see cref="AppDomain" /> en el cual realizar la búsqueda.
    /// </param>
    /// <param name="instantiablesOnly">
    /// Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
    /// <see langword="false" /> hará que se devuelvan todos los tipos coincidientes.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro del <paramref name="domain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos los ensamblados dentro del dominio
    /// especificado. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    public static IEnumerable<Type> GetTypes<T>(AppDomain domain, in bool instantiablesOnly)
    {
        return domain.GetAssemblies().GetTypes<T>(instantiablesOnly);
    }

    /// <summary>
    /// Obtiene una lista de tipos asignables a partir de la interfaz o clase base
    /// especificada.
    /// </summary>
    /// <typeparam name="T">Interfaz o clase base a buscar.</typeparam>
    /// <param name="instantiablesOnly">
    /// Si se establece en <see langword="true" />, únicamente se incluirán aquellos tipos instanciables.
    /// <see langword="false" /> hará que se devuelvan todos los tipos coincidientes.
    /// </param>
    /// <returns>
    /// Una lista de tipos de las clases que implementan a la interfaz o que heredan a la clase base
    /// <typeparamref name="T" /> dentro de <see cref="AppDomain.CurrentDomain" />.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos (privados y públicos)
    /// definidos dentro de todos ls ensamblados dentro del dominio
    /// actual. Para obtener únicamente aquellos tipos exportados
    /// públicamente, utilice <see cref="PublicTypes(Type)"/>,
    /// <see cref="PublicTypes(Type, AppDomain)"/>,
    /// <see cref="PublicTypes{T}()"/> o
    /// <see cref="PublicTypes{T}(AppDomain)"/>.
    /// </remarks>
    public static IEnumerable<Type> GetTypes<T>(bool instantiablesOnly)
    {
        return GetTypes<T>(AppDomain.CurrentDomain, instantiablesOnly);
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
    /// Esta función únicamente es únicamente útil al utilizar Visual
    /// Basic en conjunto con la estructura <c lang="VB">With</c>.
    /// </remarks>
    [Sugar]
    public static T Itself<T>(this T obj)
    {
        return obj;
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que estén dentro del
    /// <see cref="AppDomain"/> actual.
    /// </summary>
    /// <returns>
    /// Una enumeración con todos los tipos públicos encontrados en el
    /// dominio actual.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio actual, obviando los ensamblados dinámicos (generados
    /// por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    public static IEnumerable<Type> PublicTypes()
    {
        return PublicTypes(AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que estén dentro del
    /// <see cref="AppDomain"/> especificado.
    /// </summary>
    /// <param name="domain">
    /// Dominio de aplicación dentro del cual buscar tipos.
    /// </param>
    /// <returns>
    /// Una enumeración con todos los tipos públicos encontrados en el
    /// dominio especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="domain"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<Type> PublicTypes(AppDomain domain)
    {
        NullCheck(domain, nameof(domain));
        return domain.GetAssemblies()
            .Where(p => !p.IsDynamic)
            .SelectMany(SafeGetExportedTypes);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <param name="t">Tipo a obtener.</param>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio actual, obviando los ensamblados dinámicos (generados
    /// por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    public static IEnumerable<Type> PublicTypes(Type t)
    {
        return PublicTypes(t, AppDomain.CurrentDomain);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <param name="t">Tipo a obtener.</param>
    /// <param name="domain">Dominio de aplicación dentro del cual buscar tipos.</param>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="domain"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<Type> PublicTypes(Type t, AppDomain domain)
    {
        NullCheck(domain, nameof(domain));
        return PublicTypes(domain).Where(t.IsAssignableFrom);
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objetos a obtener.
    /// </typeparam>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    public static IEnumerable<Type> PublicTypes<T>()
    {
        return PublicTypes(typeof(T));
    }

    /// <summary>
    /// Obtiene todos los tipos públicos que implementan al tipo especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de objetos a obtener.
    /// </typeparam>
    /// <param name="domain">Dominio de aplicación dentro del cual buscar tipos.</param>
    /// <returns>
    /// Una enumeración con todos los tipos que heredan o implementan el
    /// tipo especificado.
    /// </returns>
    /// <remarks>
    /// Esta función obtiene todos los tipos públicos exportados del
    /// dominio especificado, obviando los ensamblados dinámicos
    /// (generados por medio del espacio de nombres
    /// <see cref="System.Reflection.Emit"/>). Para obtener una lista
    /// indiscriminada de tipos, utilice <see cref="GetTypes{T}()"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="domain"/> es
    /// <see langword="null"/>.
    /// </exception>
    public static IEnumerable<Type> PublicTypes<T>(AppDomain domain)
    {
        return PublicTypes(typeof(T), domain);
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
    public static T ShallowClone<T>(this T source) where T : new()
    {
        T copy = new();

        foreach (FieldInfo? j in typeof(T).GetFields().Where(p => p.IsPublic))
        {
            j.SetValue(copy, j.GetValue(source));
        }
        foreach (PropertyInfo? j in typeof(T).GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            j.SetValue(copy, j.GetValue(source));
        }
        return copy;
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

    private static IEnumerable<Type> SafeGetExportedTypes(Assembly arg)
    {
        Type[] types;
        try
        {
            types = arg.GetExportedTypes();
        }
        catch
        {
            types = Array.Empty<Type>();
        }
        return types;
    }
}
