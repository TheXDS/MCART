/*
PluginLoader.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using TheXDS.MCART.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheXDS.MCART.Attributes;
using System.Reflection;
using System.Threading.Tasks;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using St = TheXDS.MCART.Resources.Strings;

#region Configuración de ReSharper

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

#endregion

namespace TheXDS.MCART.PluginSupport
{
    /// <inheritdoc />
    /// <summary>
    /// Permite cargar clases que implementen la interfaz
    /// <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
    /// </summary>
    public class PluginLoader : IPluginLoader
    {
        private const string DefaultPluginExtension = ".dll";
        private readonly string _extension;
        private readonly IPluginChecker _checker;
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="T:TheXDS.MCART.PluginSupport.PluginLoader" /> utilizando el verificador
        /// predeterminado.
        /// </summary>
        public PluginLoader() : this(new DefaultPluginChecker(), DefaultPluginExtension) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="T:TheXDS.MCART.PluginSupport.PluginLoader" /> utilizando el verificador
        /// predeterminado.
        /// </summary>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        public PluginLoader(string pluginExtension) : this(new StrictPluginChecker(), pluginExtension) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="T:TheXDS.MCART.PluginSupport.PluginLoader" /> utilizando el
        /// <see cref="T:TheXDS.MCART.PluginSupport.IPluginChecker" /> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="T:TheXDS.MCART.PluginSupport.IPluginChecker" /> a utilizar para compropbar la
        /// compatilibilidad de los plugins.
        /// </param>
        public PluginLoader(IPluginChecker pluginChecker) : this(pluginChecker, SanityChecks.Default, DefaultPluginExtension) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="T:TheXDS.MCART.PluginSupport.PluginLoader" /> utilizando el
        /// <see cref="T:TheXDS.MCART.PluginSupport.IPluginChecker" /> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="T:TheXDS.MCART.PluginSupport.IPluginChecker" /> a utilizar para compropbar la
        /// compatilibilidad de los plugins.
        /// </param>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        public PluginLoader(IPluginChecker pluginChecker, string pluginExtension) : this(pluginChecker, SanityChecks.Default, pluginExtension) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="T:TheXDS.MCART.PluginSupport.PluginLoader" /> utilizando el
        /// <see cref="T:TheXDS.MCART.PluginSupport.IPluginChecker" /> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="T:TheXDS.MCART.PluginSupport.IPluginChecker" /> a utilizar para compropbar la
        /// compatilibilidad de los plugins.
        /// </param>
        /// <param name="sanityChecks">
        /// Omite las comprobaciones de peligrosidad de los
        /// <see cref="T:TheXDS.MCART.PluginSupport.Plugin" /> y sus miembros.
        /// </param>
        /// <exception cref="T:TheXDS.MCART.Exceptions.DangerousMethodException">
        /// Se produce si <paramref name="sanityChecks" /> contiene un valor que
        /// ha sido marcado con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </exception>
        /// <exception cref="T:TheXDS.MCART.Exceptions.DangerousTypeException">
        /// Se produce si <paramref name="sanityChecks" /> no contiene la
        /// bandera <see cref="F:TheXDS.MCART.SanityChecks.IgnoreDanger" /> y el
        /// <paramref name="pluginChecker" /> a utilizar fue marcado en su 
        /// declaración con el atributo <see cref="T:TheXDS.MCART.Attributes.DangerousAttribute" />.
        /// </exception>
        /// <exception cref="T:TheXDS.MCART.Exceptions.UnusableObjectException">
        /// Se produce si <paramref name="sanityChecks" /> no contiene la
        /// bandera <see cref="F:TheXDS.MCART.SanityChecks.IgnoreUnusable" /> y el
        /// <paramref name="pluginChecker" /> a utilizar fue marcado en su 
        /// declaración con el atributo <see cref="T:TheXDS.MCART.Attributes.UnusableAttribute" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="pluginChecker" /> es <see langword="null" />.
        /// </exception>
        public PluginLoader(IPluginChecker pluginChecker, SanityChecks sanityChecks) : this(pluginChecker, sanityChecks, DefaultPluginExtension) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginLoader"/> utilizando el
        /// <see cref="IPluginChecker"/> y la extensión de plugins
        /// especificada.
        /// </summary>
        /// <param name="pluginChecker">
        /// <see cref="IPluginChecker"/> a utilizar para compropbar la
        /// compatilibilidad de los plugins.
        /// </param>
        /// <param name="sanityChecks">
        /// Omite las comprobaciones de peligrosidad de los
        /// <see cref="Plugin"/> y sus miembros.
        /// </param>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        /// <exception cref="DangerousMethodException">
        /// Se produce si <paramref name="sanityChecks"/> contiene un valor que
        /// ha sido marcado con el atributo <see cref="DangerousAttribute"/>.
        /// </exception>
        /// <exception cref="DangerousTypeException">
        /// Se produce si <paramref name="sanityChecks"/> no contiene la
        /// bandera <see cref="SanityChecks.IgnoreDanger"/> y el
        /// <paramref name="pluginChecker"/> a utilizar fue marcado en su 
        /// declaración con el atributo <see cref="DangerousAttribute"/>.
        /// </exception>
        /// <exception cref="UnusableObjectException">
        /// Se produce si <paramref name="sanityChecks"/> no contiene la
        /// bandera <see cref="SanityChecks.IgnoreUnusable"/> y el
        /// <paramref name="pluginChecker"/> a utilizar fue marcado en su 
        /// declaración con el atributo <see cref="UnusableAttribute"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="pluginChecker"/> es <see langword="null"/>.
        /// </exception>
        public PluginLoader(IPluginChecker pluginChecker, SanityChecks sanityChecks, string pluginExtension)
        {
#if CheckDanger
            if (sanityChecks.HasAttr<DangerousAttribute>()) throw new DangerousMethodException();
#endif
            _checker = pluginChecker ?? throw new ArgumentNullException(nameof(pluginChecker));
            if (!sanityChecks.HasFlag(SanityChecks.IgnoreDanger) && _checker.HasAttr<DangerousAttribute>()) throw new DangerousTypeException(pluginChecker.GetType());
            if (!sanityChecks.HasFlag(SanityChecks.IgnoreUnusable) && _checker.HasAttr<UnusableAttribute>()) throw new UnusableObjectException(pluginChecker);
            _extension = pluginExtension;
        }
        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T"/> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="asmPath">Ruta del ensamblado a cargar.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="asmPath"/> es <see langword="null"/>.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// Se produce si el archivo del ensamblado no ha sido encontrado.
        /// </exception>
        /// <exception cref="FileLoadException">
        /// Se produce si el ensamblado no se pudo cargar desde el archivo.
        /// </exception>
        /// <exception cref="BadImageFormatException">
        /// Se produce si <paramref name="asmPath"/> no contiene una imagen de
        /// biblioteca de vínculos dinámicos o ejecutable válida.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Se produce si no es posible cargar el ensamblado desde
        /// <paramref name="asmPath"/> debido a un problema de seguridad.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Se produce si <paramref name="asmPath"/> no es un argumento válido.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// Se produce si <paramref name="asmPath"/> excede la longitud de ruta
        /// de archivo máxima admitida por el sistema de archivos y/o el
        /// sistema operativo.
        /// </exception>
        /// <exception cref="NotPluginException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <see cref="IPlugin"/>.
        /// </exception>
        /// <exception cref="PluginClassNotFoundException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <typeparamref name="T"/>.
        /// </exception>
        public T Load<T>(string asmPath) where T : class => Load<T>(Assembly.LoadFrom(asmPath));
        /// <inheritdoc />
        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T" /> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> de tipo <typeparamref name="T" />.
        /// </returns>
        /// <param name="assembly"><see cref="T:System.Reflection.Assembly" /> a cargar.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        /// <exception cref="T:TheXDS.MCART.Exceptions.NotPluginException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />.
        /// </exception>
        /// <exception cref="T:TheXDS.MCART.Exceptions.PluginClassNotFoundException">
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
        /// <summary>
        /// Carga todos los <see cref="IPlugin"/> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}"/> con los <see cref="IPlugin"/> 
        /// encontrados.
        /// </returns>
        /// <param name="asmPath">Ruta del ensamblado a cargar.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        /// <exception cref="FileNotFoundException">
        /// Se produce si el archivo del ensamblado no ha sido encontrado.
        /// </exception>
        public IEnumerable<T> LoadAll<T>(string asmPath) where T : class => LoadAll(Assembly.LoadFrom(asmPath)).OfType<T>();
        /// <inheritdoc />
        /// <summary>
        /// Carga todos los <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="T:System.Collections.Generic.IEnumerable`1" /> con los <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="T:System.Reflection.Assembly" /> a cargar.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> a cargar.
        /// </typeparam>
        /// <exception cref="T:TheXDS.MCART.Exceptions.NotPluginException">
        /// Se produce si <paramref name="assembly" /> no contiene clases cargables
        /// como <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />. 
        /// </exception>
        public IEnumerable<T> LoadAll<T>(Assembly assembly) where T : class => LoadAll(assembly).OfType<T>();
        /// <inheritdoc />
        /// <summary>
        /// Carga todos los <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" /> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="T:System.Collections.Generic.IEnumerable`1" /> con los <see cref="T:TheXDS.MCART.PluginSupport.IPlugin" />
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="T:System.Reflection.Assembly" /> a cargar.</param>
#if PreferExceptions
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly"/> no contiene clases cargables
        /// como <see cref="IPlugin"/>. 
        /// </exception>
#endif
        public IEnumerable<IPlugin> LoadAll(Assembly assembly)
        {
#if PreferExceptions
            if (!checker.IsVaild(assembly)) throw new NotPluginException(assembly);
#else
            if (_checker.IsVaild(assembly))
#endif
            {
                foreach (var j in assembly.GetTypes().Where(p =>
                _checker.IsValid(p)
                && (_checker.IsCompatible(p) ?? false)))
                    yield return j.New() as IPlugin;
            }
        }
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        public IEnumerable<IPlugin> LoadEverything() => LoadEverything<IPlugin>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        public IEnumerable<IPlugin> LoadEverything(string pluginsPath) => LoadEverything<IPlugin>(pluginsPath, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        public IEnumerable<IPlugin> LoadEverything(SearchOption search) => LoadEverything<IPlugin>(Environment.CurrentDirectory, search);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        public IEnumerable<IPlugin> LoadEverything(string pluginsPath, SearchOption search) => LoadEverything<IPlugin>(pluginsPath, search);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>() where T : class => LoadEverything<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(string pluginsPath) where T : class => LoadEverything<T>(pluginsPath, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(SearchOption search) where T : class => LoadEverything<T>(Environment.CurrentDirectory, search);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>
        /// Un enumerador que itera sobre todos los <see cref="IPlugin"/> que
        /// pueden ser cargados.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(string pluginsPath, SearchOption search) where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in new DirectoryInfo(pluginsPath).GetFiles($"*{_extension}", search))
            {
                Assembly a = null;
                try { a = Assembly.LoadFrom(f.FullName); }
                catch { System.Diagnostics.Debug.Print(St.Warn(St.XIsInvalid(St.XYQuotes(St.TheAssembly, f.Name)))); }
#if PreferExceptions
                if (checker.IsVaild(a))
#endif
                foreach (var j in LoadAll<T>(a)) yield return j;
            }
        }
        /// <summary>
        /// Carga cualquier <see cref="IPlugin"/> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public T LoadWhatever<T>() where T : class => LoadWhatever<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga cualquier <see cref="IPlugin"/> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public T LoadWhatever<T>(string pluginsPath) where T : class => LoadWhatever<T>(pluginsPath, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga cualquier <see cref="IPlugin"/> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public T LoadWhatever<T>(SearchOption search) where T : class => LoadWhatever<T>(Environment.CurrentDirectory, search);
        /// <summary>
        /// Carga cualquier <see cref="IPlugin"/> disponible.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
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
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <typeparamref name="T"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>() where T : class => Dir<T>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <typeparamref name="T"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>(string pluginsPath) where T : class => Dir<T>(pluginsPath, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <typeparamref name="T"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>(SearchOption search) where T : class => Dir<T>(Environment.CurrentDirectory, search);
        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <typeparamref name="T"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir<T>(string pluginsPath, SearchOption search) where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in (new DirectoryInfo(pluginsPath)).GetFiles($"*{_extension}", search))
            {
                Assembly a = null;
                try { a = Assembly.LoadFrom(f.FullName); }
                catch { }
                if (_checker.Has<T>(a)) yield return f;
            }
        }
        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir() => Dir<IPlugin>(Environment.CurrentDirectory, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir(string pluginsPath) => Dir<IPlugin>(pluginsPath, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="search">Modo de búsqueda.</param>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir(SearchOption search) => Dir<IPlugin>(Environment.CurrentDirectory, search);
        /// <summary>
        /// Enumera todos los archivos que contienen clases cargables como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <returns>
        /// Un enumerador de <see cref="FileInfo"/> de los archivos que
        /// contienen clases cargables como <see cref="IPlugin"/>.
        /// </returns>
        public IEnumerable<FileInfo> Dir(string pluginsPath, SearchOption search) => Dir<IPlugin>(pluginsPath, search);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados como una
        /// estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>() where T : class => PluginTree<T>(Environment.CurrentDirectory, "*", SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con 
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath) where T : class => PluginTree<T>(pluginsPath, "*", SearchOption.TopDirectoryOnly);
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
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath, string searchPattern) where T : class => PluginTree<T>(pluginsPath, searchPattern, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con 
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="search">
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(SearchOption search) where T : class => PluginTree<T>(Environment.CurrentDirectory, "*", search);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con 
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="search">
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath, SearchOption search) where T : class => PluginTree<T>(pluginsPath, "*", search);
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
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath, string searchPattern, SearchOption search) where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            Dictionary<string, IEnumerable<T>> outp = new Dictionary<string, IEnumerable<T>>();
            foreach (FileInfo f in (new DirectoryInfo(pluginsPath)).GetFiles(searchPattern + _extension, search))
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(f.FullName);
                    if (_checker.IsVaild(a)) outp.Add(f.Name, LoadAll<T>(a));
                }
                catch (Exception ex) { System.Diagnostics.Debug.Print(ex.Message); }
            }
            return outp;
        }
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados como una
        /// estructura de árbol.
        /// </summary>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree() => PluginTree<IPlugin>(Environment.CurrentDirectory, "*", SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con 
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath) => PluginTree<IPlugin>(pluginsPath, "*", SearchOption.TopDirectoryOnly);
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
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath, string searchPattern)=> PluginTree<IPlugin>(pluginsPath, searchPattern, SearchOption.TopDirectoryOnly);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con 
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="search">
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(SearchOption search) => PluginTree<IPlugin>(Environment.CurrentDirectory, "*", search);
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados que coincidan con 
        /// el patrón como una estructura de árbol.
        /// </summary>
        /// <param name="pluginsPath">
        /// Ruta de búsqueda. Debe ser un directorio.
        /// </param>
        /// <param name="search">
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath, SearchOption search) => PluginTree<IPlugin>(pluginsPath, "*", search);
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
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath, string searchPattern, SearchOption search) => PluginTree<IPlugin>(pluginsPath, searchPattern, search);
    }
}