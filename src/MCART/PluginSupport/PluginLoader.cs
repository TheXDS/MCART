/*
PluginLoader.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.PluginSupport.Legacy
{
    /// <inheritdoc />
    /// <summary>
    /// Permite cargar clases que implementen la interfaz
    /// <see cref="IPlugin" />.
    /// </summary>
    public class PluginLoader : IPluginLoader
    {
        private const string _defaultPluginExtension = ".dll";
        private readonly IPluginChecker _checker;
        private readonly string _extension;

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginLoader" /> utilizando el verificador
        /// predeterminado.
        /// </summary>
        public PluginLoader() : this(new DefaultPluginChecker(), _defaultPluginExtension)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginLoader" /> utilizando el verificador
        /// predeterminado.
        /// </summary>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        public PluginLoader(string pluginExtension) : this(new StrictPluginChecker(), pluginExtension)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginLoader" /> utilizando el
        /// <see cref="IPluginChecker" /> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="IPluginChecker" /> a utilizar para comprobar la
        /// compatibilidad de los plugins.
        /// </param>
        public PluginLoader(IPluginChecker pluginChecker) : this(pluginChecker, SanityChecks.Default,
            _defaultPluginExtension)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginLoader" /> utilizando el
        /// <see cref="IPluginChecker" /> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="IPluginChecker" /> a utilizar para comprobar la
        /// compatibilidad de los plugins.
        /// </param>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        public PluginLoader(IPluginChecker pluginChecker, string pluginExtension) : this(pluginChecker,
            SanityChecks.Default, pluginExtension)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginLoader" /> utilizando el
        /// <see cref="IPluginChecker" /> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="IPluginChecker" /> a utilizar para comprobar la
        /// compatibilidad de los plugins.
        /// </param>
        /// <param name="sanityChecks">
        /// Omite las comprobaciones de peligrosidad de los
        /// <see cref="Plugin" /> y sus miembros.
        /// </param>
        /// <exception cref="DangerousMethodException">
        /// Se produce si <paramref name="sanityChecks" /> contiene un valor que
        /// ha sido marcado con el atributo <see cref="DangerousAttribute" />.
        /// </exception>
        /// <exception cref="DangerousTypeException">
        /// Se produce si <paramref name="sanityChecks" /> no contiene la
        /// bandera <see cref="F:TheXDS.MCART.SanityChecks.IgnoreDanger" /> y el
        /// <paramref name="pluginChecker" /> a utilizar fue marcado en su
        /// declaración con el atributo <see cref="DangerousAttribute" />.
        /// </exception>
        /// <exception cref="UnusableObjectException">
        /// Se produce si <paramref name="sanityChecks" /> no contiene la
        /// bandera <see cref="F:TheXDS.MCART.SanityChecks.IgnoreUnusable" /> y el
        /// <paramref name="pluginChecker" /> a utilizar fue marcado en su
        /// declaración con el atributo <see cref="UnusableAttribute" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="pluginChecker" /> es <see langword="null" />.
        /// </exception>
        public PluginLoader(IPluginChecker pluginChecker, SanityChecks sanityChecks) : this(pluginChecker, sanityChecks,
            _defaultPluginExtension)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginLoader" /> utilizando el
        /// <see cref="IPluginChecker" /> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="IPluginChecker" /> a utilizar para comprobar la
        /// compatibilidad de los plugins.
        /// </param>
        /// <param name="sanityChecks">
        /// Omite las comprobaciones de peligrosidad de los
        /// <see cref="Plugin" /> y sus miembros.
        /// </param>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        /// <exception cref="DangerousMethodException">
        /// Se produce si <paramref name="sanityChecks" /> contiene un valor que
        /// ha sido marcado con el atributo <see cref="DangerousAttribute" />.
        /// </exception>
        /// <exception cref="DangerousTypeException">
        /// Se produce si <paramref name="sanityChecks" /> no contiene la
        /// bandera <see cref="SanityChecks.IgnoreDanger" /> y el
        /// <paramref name="pluginChecker" /> a utilizar fue marcado en su
        /// declaración con el atributo <see cref="DangerousAttribute" />.
        /// </exception>
        /// <exception cref="UnusableObjectException">
        /// Se produce si <paramref name="sanityChecks" /> no contiene la
        /// bandera <see cref="SanityChecks.IgnoreUnusable" /> y el
        /// <paramref name="pluginChecker" /> a utilizar fue marcado en su
        /// declaración con el atributo <see cref="UnusableAttribute" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="pluginChecker" /> es <see langword="null" />.
        /// </exception>
        public PluginLoader(IPluginChecker pluginChecker, SanityChecks sanityChecks, string pluginExtension)
        {
#if CheckDanger
            if (sanityChecks.HasAttr<DangerousAttribute>()) throw new DangerousMethodException();
#endif
            _checker = pluginChecker ?? throw new ArgumentNullException(nameof(pluginChecker));
            if (!sanityChecks.HasFlag(SanityChecks.IgnoreDanger) && _checker.HasAttr<DangerousAttribute>())
                throw new DangerousTypeException(pluginChecker.GetType());
            if (!sanityChecks.HasFlag(SanityChecks.IgnoreUnusable) && _checker.HasAttr<UnusableAttribute>())
                throw new UnusableObjectException(pluginChecker);
            _extension = pluginExtension;
        }

        /// <inheritdoc />
        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T" /> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin" /> de tipo <typeparamref name="T" />.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly" /> a cargar.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        /// <exception cref="NotPluginException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <see cref="IPlugin" />.
        /// </exception>
        /// <exception cref="PluginClassNotFoundException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <typeparamref name="T" />.
        /// </exception>
        public T Load<T>(Assembly assembly) where T : class
        {
            if (!_checker.IsVaild(assembly)) throw new NotPluginException(assembly);
            return assembly.GetTypes().FirstOrDefault(p =>
                       _checker.IsValid(p)
                       && (_checker.IsCompatible(p) ?? false)
                       && typeof(T).IsAssignableFrom(p)
                   )?.New<T>() ?? throw new PluginClassNotFoundException(typeof(T));
        }

        /// <inheritdoc />
        /// <summary>
        /// Carga todos los <see cref="IPlugin" /> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> con los
        /// <see cref="IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly" /> a cargar.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly" /> no contiene clases cargables
        /// como <see cref="IPlugin" />.
        /// </exception>
        public IEnumerable<T> LoadAll<T>(Assembly assembly) where T : class
        {
            return LoadAll<T>(assembly, null);
        }

        /// <inheritdoc />
        /// <summary>
        /// Carga todos los <see cref="IPlugin" /> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> con los
        /// <see cref="IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly" /> a cargar.</param>
#if PreferExceptions
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly"/> no contiene clases cargables
        /// como <see cref="IPlugin"/>. 
        /// </exception>
#endif
        public IEnumerable<IPlugin> LoadAll(Assembly assembly)
        {
            return LoadAll(assembly, null);
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <typeparamref name="T" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>() where T : class
        {
            return Dir<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T" />.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <typeparamref name="T" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>(string pluginsPath) where T : class
        {
            return Dir<T>(pluginsPath, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T" />.
        /// </summary>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <typeparamref name="T" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>(SearchOption search) where T : class
        {
            return Dir<T>(Environment.CurrentDirectory, search);
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T" />.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <typeparamref name="T" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>(string pluginsPath, SearchOption search) where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in new DirectoryInfo(pluginsPath).GetFiles($"*{_extension}", search))
            {
                Assembly? a;
                try
                {
                    a = Assembly.LoadFrom(f.FullName);
                }
                catch { continue; }

                if (_checker.Has<T>(a)) yield return f;
            }
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin" />.
        /// </summary>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir()
        {
            return Dir<IPlugin>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin" />.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir(string pluginsPath)
        {
            return Dir<IPlugin>(pluginsPath, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin" />.
        /// </summary>
        /// <param name="search">Modo de búsqueda.</param>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir(SearchOption search)
        {
            return Dir<IPlugin>(Environment.CurrentDirectory, search);
        }

        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin" />.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo" /> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin" />.
        /// </returns>
        public IEnumerable<FileInfo> Dir(string pluginsPath, SearchOption search)
        {
            return Dir<IPlugin>(pluginsPath, search);
        }

        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T" /> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin" /> de tipo <typeparamref name="T" />.
        /// </returns>
        /// <param name="asmPath">Ruta del ensamblado a cargar.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="asmPath" /> es <see langword="null" />.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// Se produce si el archivo del ensamblado no ha sido encontrado.
        /// </exception>
        /// <exception cref="FileLoadException">
        /// Se produce si el ensamblado no se pudo cargar desde el archivo.
        /// </exception>
        /// <exception cref="BadImageFormatException">
        /// Se produce si <paramref name="asmPath" /> no contiene una imagen de
        /// biblioteca de vínculos dinámicos o ejecutable válida.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Se produce si no es posible cargar el ensamblado desde
        /// <paramref name="asmPath" /> debido a un problema de seguridad.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Se produce si <paramref name="asmPath" /> no es un argumento válido.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// Se produce si <paramref name="asmPath" /> excede la longitud de ruta
        /// de archivo máxima admitida por el sistema de archivos y/o el
        /// sistema operativo.
        /// </exception>
        /// <exception cref="NotPluginException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <see cref="IPlugin" />.
        /// </exception>
        /// <exception cref="PluginClassNotFoundException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <typeparamref name="T" />.
        /// </exception>
        public T Load<T>(string asmPath) where T : class
        {
            return Load<T>(Assembly.LoadFrom(asmPath));
        }

        /// <summary>
        /// Carga todos los <see cref="IPlugin" /> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> con los <see cref="IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="asmPath">Ruta del ensamblado a cargar.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        /// <exception cref="FileNotFoundException">
        /// Se produce si el archivo del ensamblado no ha sido encontrado.
        /// </exception>
        public IEnumerable<T> LoadAll<T>(string asmPath) where T : class
        {
            return LoadAll(Assembly.LoadFrom(asmPath)).OfType<T>();
        }

        /// <summary>
        /// Carga todos los <see cref="IPlugin" /> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> con los
        /// <see cref="IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly" /> a cargar.</param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly" /> no contiene clases cargables
        /// como <see cref="IPlugin" />.
        /// </exception>
        public IEnumerable<T> LoadAll<T>(Assembly assembly, Func<Type, bool>? predicate) where T : class
        {
            return LoadAll(assembly, predicate).OfType<T>();
        }

        /// <summary>
        /// Carga todos los <see cref="IPlugin" /> contenidos en el
        /// ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> con los <see cref="IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly" /> a cargar.</param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa
        /// <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
#if PreferExceptions
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly"/> no contiene clases cargables
        /// como <see cref="IPlugin"/>. 
        /// </exception>
#endif
        public IEnumerable<IPlugin> LoadAll(Assembly assembly, Func<Type, bool>? predicate)
        {
            bool IsTypeCompatible(Type p) => _checker.IsValid(p) && (_checker.IsCompatible(p) ?? false) && (predicate?.Invoke(p) ?? true);
#if PreferExceptions
            if (!checker.IsVaild(assembly)) throw new NotPluginException(assembly);
#else
            if (_checker.IsVaild(assembly))
#endif
                foreach (var j in assembly.GetTypes().Where(IsTypeCompatible))
                    yield return j.New<IPlugin>();
        }

        /// <summary>
        /// Carga todos los <see cref="IPlugin" /> contenidos en el
        /// ensamblado de forma asíncrona.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> con los <see cref="IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly" /> a cargar.</param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa
        /// <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        public async Task<IEnumerable<IPlugin>> LoadAllAsync(Assembly assembly, Func<Type, bool>? predicate)
        {
            bool IsTypeCompatible(Type p) => _checker.IsValid(p) && (_checker.IsCompatible(p) ?? false) && (predicate?.Invoke(p) ?? true);
            if (await Task.Run(() => _checker.IsVaild(assembly)))
            {
                return assembly.GetTypes().Where(IsTypeCompatible).Select(p => p.New<IPlugin>());
            }
#if PreferExceptions
            else { throw new NotPluginException(assembly); }
#endif
            return global::System.Array.Empty<global::TheXDS.MCART.PluginSupport.Legacy.IPlugin>();
        }

        /// <summary>
        /// Carga todos los <see cref="IPlugin" /> contenidos en el
        /// ensamblado de forma asíncrona.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}" /> con los <see cref="IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly" /> a cargar.</param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa
        /// <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        public async Task<IEnumerable<T>> LoadAllAsync<T>(Assembly assembly, Func<Type, bool>? predicate) where T : class
        {
            return (await LoadAllAsync(assembly, predicate)).OfType<T>();
        }


        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        public IEnumerable<IPlugin> LoadEverything()
        {
            return LoadEverything<IPlugin>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        public IEnumerable<IPlugin> LoadEverything(string pluginsPath)
        {
            return LoadEverything<IPlugin>(pluginsPath, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        public IEnumerable<IPlugin> LoadEverything(SearchOption search)
        {
            return LoadEverything<IPlugin>(Environment.CurrentDirectory, search);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        public IEnumerable<IPlugin> LoadEverything(string pluginsPath, SearchOption search)
        {
            return LoadEverything<IPlugin>(pluginsPath, search);
        }


        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        public IEnumerable<IPlugin> LoadEverything(Func<Type, bool> predicate)
        {
            return LoadEverything<IPlugin>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        public IEnumerable<IPlugin> LoadEverything(string pluginsPath, Func<Type, bool> predicate)
        {
            return LoadEverything<IPlugin>(pluginsPath, SearchOption.TopDirectoryOnly, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        public IEnumerable<IPlugin> LoadEverything(SearchOption search, Func<Type, bool> predicate)
        {
            return LoadEverything<IPlugin>(Environment.CurrentDirectory, search, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        public IEnumerable<IPlugin> LoadEverything(string pluginsPath, SearchOption search, Func<Type, bool> predicate)
        {
            return LoadEverything<IPlugin>(pluginsPath, search, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>() where T : class
        {
            return LoadEverything<T>((Func<Type, bool>?)null);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(Func<Type, bool>? predicate) where T : class
        {
            return LoadEverything<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(string pluginsPath) where T : class
        {
            return LoadEverything<T>(pluginsPath, null);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(string pluginsPath, Func<Type, bool>? predicate) where T : class
        {
            return LoadEverything<T>(pluginsPath, SearchOption.TopDirectoryOnly, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(SearchOption search) where T : class
        {
            return LoadEverything<T>(search, null);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(SearchOption search, Func<Type, bool>? predicate) where T : class
        {
            return LoadEverything<T>(Environment.CurrentDirectory, search, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(string pluginsPath, SearchOption search) where T : class
        {
            return LoadEverything<T>(pluginsPath, search, null);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(string pluginsPath, SearchOption search, Func<Type, bool>? predicate)
            where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in new DirectoryInfo(pluginsPath).GetFiles($"*{_extension}", search))
            {
                Assembly? a = null;
                try
                {
                    a = Assembly.LoadFrom(f.FullName);
                }
                catch
                {
                    Debug.Print(St.Warn(St.XIsInvalid(St.XYQuotes(St.TheAssembly, f.Name))));
                    continue;
                }
#if PreferExceptions
                if (checker.IsVaild(a))
#endif
                foreach (var j in LoadAll<T>(a, predicate)) yield return j;
            }
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public async Task<IEnumerable<T>> LoadEverythingAsync<T>(string pluginsPath, SearchOption search, Func<Type, bool>? predicate)
            where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            var r = new List<T>();
            foreach (var f in await Task.Run(() => new DirectoryInfo(pluginsPath).GetFiles($"*{_extension}", search)))
            {
                Assembly? a = null;
                try
                {
                    a = await Task.Run(() => Assembly.LoadFrom(f.FullName));
                }
                catch
                {
                    Debug.Print(St.Warn(St.XIsInvalid(St.XYQuotes(St.TheAssembly, f.Name))));
                    continue;
                }
#if PreferExceptions
                if (checker.IsVaild(a))
#endif
                r.AddRange(await LoadAllAsync<T>(a, predicate));
            }
            return r;
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public Task<IEnumerable<T>> LoadEverythingAsync<T>(string pluginsPath, SearchOption search) where T : class
        {
            return LoadEverythingAsync<T>(pluginsPath, search, null);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public Task<IEnumerable<T>> LoadEverythingAsync<T>(string pluginsPath) where T : class
        {
            return LoadEverythingAsync<T>(pluginsPath, SearchOption.TopDirectoryOnly, null);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public Task<IEnumerable<T>> LoadEverythingAsync<T>() where T : class
        {
            return LoadEverythingAsync<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly, null);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public Task<IEnumerable<T>> LoadEverythingAsync<T>(Func<Type, bool> predicate)
            where T : class
        {
            return LoadEverythingAsync<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly, predicate);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin" /> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="predicate">
        /// Función que evalúa si un tipo que implementa <see cref="IPlugin" /> debería ser cargado o no.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public Task<IEnumerable<T>> LoadEverythingAsync<T>(string pluginsPath, Func<Type, bool> predicate)
            where T : class
        {
            return LoadEverythingAsync<T>(pluginsPath, SearchOption.TopDirectoryOnly, predicate);
        }

        /// <summary>
        /// Carga cualquier <see cref="IPlugin" /> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin" /> de tipo <typeparamref name="T" />.
        /// </returns>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public T LoadWhatever<T>() where T : class
        {
            return LoadWhatever<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga cualquier <see cref="IPlugin" /> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin" /> de tipo <typeparamref name="T" />.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public T LoadWhatever<T>(string pluginsPath) where T : class
        {
            return LoadWhatever<T>(pluginsPath, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga cualquier <see cref="IPlugin" /> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin" /> de tipo <typeparamref name="T" />.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public T LoadWhatever<T>(SearchOption search) where T : class
        {
            return LoadWhatever<T>(Environment.CurrentDirectory, search);
        }

        /// <summary>
        /// Carga cualquier <see cref="IPlugin" /> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin" /> de tipo <typeparamref name="T" />.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin" /> a cargar.
        /// </typeparam>
        public T LoadWhatever<T>(string pluginsPath, SearchOption search) where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            return LoadEverything<T>(pluginsPath, search).FirstOrDefault()
#if !PreferExceptions
                   ?? throw new PluginClassNotFoundException(typeof(T))
#endif
                ;
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados como una
        /// estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>() where T : class
        {
            return PluginTree<T>(Environment.CurrentDirectory, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath) where T : class
        {
            return PluginTree<T>(pluginsPath, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="searchPattern">
        /// Patrón de búsqueda de ensamblados.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath, string searchPattern)
            where T : class
        {
            return PluginTree<T>(pluginsPath, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="search">
        /// <see cref="SearchOption" /> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(SearchOption search) where T : class
        {
            return PluginTree<T>(Environment.CurrentDirectory, "*", search);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="search">
        /// <see cref="SearchOption" /> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath, SearchOption search) where T : class
        {
            return PluginTree<T>(pluginsPath, "*", search);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="searchPattern">
        /// Patrón de búsqueda de ensamblados.
        /// </param>
        /// <param name="search">
        /// <see cref="SearchOption" /> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath, string searchPattern,
            SearchOption search) where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            var outp = new Dictionary<string, IEnumerable<T>>();
            foreach (var f in new DirectoryInfo(pluginsPath).GetFiles(searchPattern + _extension, search))
                try
                {
                    var a = Assembly.LoadFrom(f.FullName);
                    if (_checker.IsVaild(a)) outp.Add(f.Name, LoadAll<T>(a));
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }

            return outp;
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados como una
        /// estructura de árbol.
        /// </summary>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree()
        {
            return PluginTree<IPlugin>(Environment.CurrentDirectory, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath)
        {
            return PluginTree<IPlugin>(pluginsPath, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="searchPattern">
        /// Patrón de búsqueda de ensamblados.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath, string searchPattern)
        {
            return PluginTree<IPlugin>(pluginsPath, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="search">
        /// <see cref="SearchOption" /> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(SearchOption search)
        {
            return PluginTree<IPlugin>(Environment.CurrentDirectory, "*", search);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="search">
        /// <see cref="SearchOption" /> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath, SearchOption search)
        {
            return PluginTree<IPlugin>(pluginsPath, "*", search);
        }

        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="searchPattern">
        /// Patrón de búsqueda de ensamblados.
        /// </param>
        /// <param name="search">
        /// <see cref="SearchOption" /> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}" /> con los ensamblados y
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath, string searchPattern,
            SearchOption search)
        {
            return PluginTree<IPlugin>(pluginsPath, searchPattern, search);
        }
    }
}