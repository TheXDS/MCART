//
//  PluginLoader.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MCART.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using St = MCART.Resources.Strings;

namespace MCART.PluginSupport
{
    /// <summary>
    /// Permite cargar <see cref="IPlugin"/>.
    /// </summary>
    public class PluginLoader : IPluginLoader
    {
        readonly string extension;
        readonly IPluginChecker checker;

#if CheckDanger
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
        /// <param name="ignoreDanger"></param>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        [Attributes.Dangerous] public PluginLoader(IPluginChecker pluginChecker, bool ignoreDanger, string pluginExtension = ".dll")
        {
            if (!ignoreDanger && pluginChecker.HasAttr<Attributes.DangerousAttribute>()) throw new DangerousClassException(pluginChecker.GetType());

            extension = pluginExtension;
            checker = pluginChecker ?? throw new ArgumentNullException(nameof(pluginChecker));
        }
#endif
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
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        public PluginLoader(IPluginChecker pluginChecker, string pluginExtension = ".dll")
        {
#if NoDanger
            if (pluginChecker.HasAttr<Attributes.DangerousAttribute>()) throw new DangerousClassException(pluginChecker.GetType());
#endif
            extension = pluginExtension;
            checker = pluginChecker ?? throw new ArgumentNullException(nameof(pluginChecker));
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginLoader"/> utilizando el verificador
        /// predeterminado.
        /// </summary>
        /// <param name="pluginExtension">
        /// Parámetro opcional. Extensión de los archivos que contienen
        /// plugins.
        /// </param>
        public PluginLoader(string pluginExtension = ".dll") : this(new StrictPluginChecker(), pluginExtension) { }
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
        /// Se produce si <paramref name="asmPath"/> es <c>null</c>.
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
        public T Load<T>(string asmPath) where T : class, new() => Load<T>(Assembly.LoadFrom(asmPath));
        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T"/> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a cargar.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        /// <exception cref="NotPluginException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <see cref="IPlugin"/>.
        /// </exception>
        /// <exception cref="PluginClassNotFoundException">
        /// Se produce si el ensamblado no contiene ninguna clase cargable como
        /// <typeparamref name="T"/>.
        /// </exception>
        public T Load<T>(Assembly assembly) where T : class, new()
        {
            if (!checker.IsVaild(assembly)) throw new NotPluginException(assembly);
            return assembly.GetTypes().FirstOrDefault(p =>
                checker.IsVaild(p)
                && (checker.IsCompatible(p) ?? false)
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
        public IEnumerable<T> LoadAll<T>(string asmPath) where T : class, new() => LoadAll(Assembly.LoadFrom(asmPath)).OfType<T>();
        /// <summary>
        /// Carga todos los <see cref="IPlugin"/> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}"/> con los <see cref="IPlugin"/>
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a cargar.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="assembly"/> no contiene clases cargables
        /// como <see cref="IPlugin"/>. 
        /// </exception>
        public IEnumerable<T> LoadAll<T>(Assembly assembly) where T : class, new() => LoadAll(assembly).OfType<T>();
        /// <summary>
        /// Carga todos los <see cref="IPlugin"/> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}"/> con los <see cref="IPlugin"/>
        /// encontrados.
        /// </returns>
        /// <param name="assembly"><see cref="Assembly"/> a cargar.</param>
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
            if (checker.IsVaild(assembly))
#endif
            {
                foreach (var j in assembly.GetTypes().Where(p =>
                checker.IsVaild(p)
                && (checker.IsCompatible(p) ?? false)))
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
        /// <param name="pluginsPath">
        /// Ruta del directorio que contiene los archivos a cargar.
        /// </param>
        /// <param name="search">Modo de búsqueda.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar.
        /// </typeparam>
        public IEnumerable<T> LoadEverything<T>(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly) where T : class, new()
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in (new DirectoryInfo(pluginsPath)).GetFiles($"*{extension}", search))
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
        public IEnumerable<IPlugin> LoadEverything(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in (new DirectoryInfo(pluginsPath)).GetFiles($"*{extension}", search))
            {
                Assembly a = null;
                try { a = Assembly.LoadFrom(f.FullName); }
                catch { System.Diagnostics.Debug.Print(St.Warn(St.XIsInvalid(St.XYQuotes(St.TheAssembly, f.Name)))); }
#if PreferExceptions
                if (checker.IsVaild(a))
#endif
                foreach (var j in LoadAll(a)) yield return j;
            }
        }
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
        public T LoadWhatever<T>(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly) where T : class, new()
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
        public IEnumerable<FileInfo> Dir<T>(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly) where T : class, new()
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in (new DirectoryInfo(pluginsPath)).GetFiles($"*{extension}", search))
            {
                Assembly a = null;
                try { a = Assembly.LoadFrom(f.FullName); }
                catch { }
                if (checker.Has<T>(a)) yield return f;
            }
        }
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
        public IEnumerable<FileInfo> Dir(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            foreach (var f in (new DirectoryInfo(pluginsPath)).GetFiles($"*{extension}", search))
            {
                Assembly a = null;
                try { a = Assembly.LoadFrom(f.FullName); }
                catch { }
                if (checker.IsVaild(a)) yield return f;
            }
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
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<T>> PluginTree<T>(string pluginsPath = ".", string searchPattern = "*", SearchOption search = SearchOption.TopDirectoryOnly) where T : class, new()
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            Dictionary<string, IEnumerable<T>> outp = new Dictionary<string, IEnumerable<T>>();
            foreach (FileInfo f in (new DirectoryInfo(pluginsPath)).GetFiles(searchPattern + extension, search))
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(f.FullName);
                    if (checker.IsVaild(a)) outp.Add(f.Name, LoadAll<T>(a));
                }
                catch (Exception ex) { System.Diagnostics.Debug.Print(ex.Message); }
            }
            return outp;
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
        /// <see cref="SearchOption"/> con las opciones de búsqueda.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public Dictionary<string, IEnumerable<IPlugin>> PluginTree(string pluginsPath = ".", string searchPattern = "*", SearchOption search = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            Dictionary<string, IEnumerable<IPlugin>> outp = new Dictionary<string, IEnumerable<IPlugin>>();
            foreach (FileInfo f in (new DirectoryInfo(pluginsPath)).GetFiles(searchPattern + extension, search))
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(f.FullName);
                    if (checker.IsVaild(a)) outp.Add(f.Name, LoadAll(a));
                }
                catch (Exception ex) { System.Diagnostics.Debug.Print(ex.Message); }
            }
            return outp;
        }
    }
}