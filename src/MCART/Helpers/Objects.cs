/*
Objects.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

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

namespace TheXDS.MCART.Helpers
{
    /// <summary>
    /// Funciones de manipulación de objetos.
    /// </summary>
    public static partial class Objects
    {
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
            NullCheck(identifier, nameof(identifier));
            NullCheck(domain, nameof(domain));
            return GetTypes<T>(domain)
                .FirstOrDefault(j => j.GetCustomAttributes(typeof(IdentifierAttribute), false)
                    .Cast<IdentifierAttribute>()
                    .Any(k => k.Value == identifier));
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
}