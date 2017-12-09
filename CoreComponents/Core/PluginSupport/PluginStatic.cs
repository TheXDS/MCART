//
//  PluginStatic.cs
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

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MCART.Attributes;
using MCART.Exceptions;
using St = MCART.Resources.Strings;
using MCART.Resources;

namespace MCART.PluginSupport
{
    public abstract partial class Plugin : IPlugin
    {
        /// <summary>
        /// Extensión predeterminada de los ensamblados que contienen plugins.
        /// </summary>
        public const string PluginExtension = ".dll";
        /// <summary>
        /// Comprueba si un ensamblado contiene clases cargables como
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns><c>true</c> si el ensamblado contiene clases cargables como
        /// <see cref="IPlugin"/>; de no contrario, <c>false</c>.</returns>
        /// <param name="asmbly"><see cref="Assembly"/> a comprobar.</param>
        [Thunk]
        public static bool IsVaild(Assembly asmbly) => asmbly.IsNot(RTInfo.RTAssembly) && asmbly.GetTypes().Any((arg) => typeof(IPlugin).IsAssignableFrom(arg));
        /// <summary>
        /// Comprueba si un ensamblado contiene un plugin del tipo especificado.
        /// </summary>
        /// <returns>The has.</returns>
        /// <param name="asmbly">Asmbly.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        [Thunk] public static bool Has<T>(Assembly asmbly) => asmbly.GetTypes().Any((arg) => new Type[] { typeof(IPlugin), typeof(T) }.AreAssignableFrom(arg));
        /// <summary>
        /// Inicializa una nueva instancia del plugin.
        /// </summary>
        /// <returns>El plugin cargado.</returns>
        /// <param name="j">
        /// Tipo a instanciar. Debe heredar <see cref="Plugin"/>, o en su
        /// defecto, implementar <see cref="IPlugin"/>.
        /// </param>
        /// <param name="tme">Momento de carga.</param>
        static object NewPlg(Type j, DateTime tme)
        {
            object p = j.New();
            (p as Plugin)?.RaisePlgLoad(tme);
            return p;
        }
        /// <summary>
        /// Determina si un tipo es válido para ser cargado como un
        /// <see cref="Plugin"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c>, si el tipo puede ser cagado como un 
        /// <see cref="Plugin"/>, <c>false</c> en caso contrario.
        /// </returns>
        /// <param name="m">Tipo a comprobar.</param>
        /// <param name="chkVersion">
        /// Parámetro opcional. Si se omite o se establece en <c>true</c>, se
        /// verificará la compatibilidad con esta versión de MCART, si se
        /// establece en <c>false</c>, no se realizará la comprobación de
        /// compatibilidad con esta versión de MCART.
        /// </param>
        static bool IsValidType(Type m, bool chkVersion = true)
        {
#if StrictMCARTVersioning
            if (!chkVersion) return !(m.IsInterface || m.IsAbstract);
            var mt = m.GetAttr<MinMCARTVersionAttribute>()?.Value ?? m.Assembly.GetAttr<MinMCARTVersionAttribute>()?.Value;
            var tt = m.GetAttr<TargetMCARTVersionAttribute>()?.Value ?? m.Assembly.GetAttr<TargetMCARTVersionAttribute>()?.Value;
            if (Objects.IsAnyNull(mt, tt)) return false;
            var mv = RTInfo.RTAssembly.GetName().Version;
            return !m.IsInterface && !m.IsAbstract && mv.IsBetween(mt, tt);
#else
			var mt = chkVersion ? m.GetAttr<MinMCARTVersionAttribute>() : null;
			var mv = RTInfo.RTAssembly.GetName().Version;
			return !m.IsInterface && !m.IsAbstract && ((mt?.Value ?? mv) <= mv) && (m.GetAttr<UnusableAttribute>().IsNull());
#endif
        }
        /// <summary>
        /// Carga todos los <see cref="IPlugin"/> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}"/> con los <see cref="IPlugin"/> 
        /// encontrados.
        /// </returns>
        /// <param name="asmPath">Ruta del ensamblado a cargar.</param>
        /// <param name="ignoreVersion">
        /// Si se establece en <c>true</c>, no se comprobará la versión de
        /// MCART antes de cargar los <see cref="IPlugin"/>, lo cual puede ser
        /// muy peligroso, pero útil para obtener información.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar. Si se utiliza
        /// <see cref="IPlugin"/>, se cargaran absolutamente todos los plugins,
        /// sin importar de qué tipo sean.</typeparam>
        /// <exception cref="FileNotFoundException">
        /// Se produce si el archivo del ensamblado no ha sido encontrado.
        /// </exception>
        public static IEnumerable<T> LoadAll<T>(string asmPath, bool ignoreVersion = false) where T : class
        {
            if (!File.Exists(asmPath))
                throw new FileNotFoundException(
                    St.XNotFound(St.TheAssembly), asmPath);
            return LoadAll<T>(Assembly.LoadFrom(asmPath), ignoreVersion);
        }
        /// <summary>
        /// Carga todos los <see cref="IPlugin"/> contenidos en el ensamblado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IEnumerable{T}"/> con los <see cref="IPlugin"/>
        /// encontrados.
        /// </returns>
        /// <param name="asmbly"><see cref="Assembly"/> a cargar.</param>
        /// <param name="ignoreVersion">
        /// Si se establece en <c>true</c>, no se comprobará la versión de
        /// MCART antes de cargar los <see cref="IPlugin"/>, lo cual puede ser
        /// muy peligroso, pero útil para obtener información.</param>
        /// <typeparam name="T">
        /// Tipo de <see cref="IPlugin"/> a cargar. Si se utiliza
        /// <see cref="IPlugin"/>, se cargaran absolutamente todos los plugins,
        /// sin importar de qué tipo sean.</typeparam>
        /// <exception cref="NotPluginException">
        /// Se produce si <paramref name="asmbly"/> no contiene clases cargables
        /// como <see cref="IPlugin"/>. 
        /// </exception>
        public static IEnumerable<T> LoadAll<T>(Assembly asmbly, bool ignoreVersion = false) where T : class
        {
            if (!IsVaild(asmbly)) throw new NotPluginException(asmbly);
            foreach (Type j in asmbly.GetTypes().Where((arg) =>
                new Type[] { typeof(IPlugin), typeof(T) }.AreAssignableFrom(arg)
                && IsValidType(arg, !ignoreVersion)))
                yield return (T)NewPlg(j, DateTime.Now);
        }
        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T"/> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="asmPath">Ruta del ensamblado a cargar.</param>
        /// <param name="ignoreVersion">
        /// Si se establece en <c>true</c>, se omitirá la comprobación de
        /// versión de MCART.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        /// <exception cref="T:System.IO.FileNotFoundException">
        /// Se produce si el archivo del ensamblado no ha sido encontrado.
        /// </exception>
        public static T Load<T>(string asmPath, bool ignoreVersion = false) where T : class
        {
            if (!File.Exists(asmPath))
                throw new FileNotFoundException(
                    St.XNotFound(St.TheAssembly), asmPath);
            return Load<T>(Assembly.LoadFrom(asmPath), ignoreVersion);
        }
        /// <summary>
        /// Carga una clase de tipo <typeparamref name="T"/> contenida en el
        /// ensamblado especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="T:IPlugin"/> de tipo <typeparamref name="T"/>.
        /// </returns>
        /// <param name="asmbly"><see cref="Assembly"/> a cargar.</param>
        /// <param name="ignoreVersion">
        /// Si se establece en <c>true</c>, se omitirá la comprobación de
        /// versión de MCART.</param>
        /// <typeparam name="T">Clase a cargar.</typeparam>
        public static T Load<T>(Assembly asmbly, bool ignoreVersion = false) where T : class
        {
            if (!IsVaild(asmbly)) throw new NotPluginException(asmbly);
            return NewPlg(asmbly.GetTypes().FirstOrDefault((arg) =>
                new Type[] { typeof(IPlugin), typeof(T) }.AreAssignableFrom(arg)
                && IsValidType(arg, !ignoreVersion)), DateTime.Now) as T ??
            throw new PluginClassNotFoundException(typeof(T));
        }
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio.
        /// </summary>
        /// <returns>The everything.</returns>
        /// <param name="ignoreVersion">If set to <c>true</c> ignore version.</param>
        /// <param name="pluginsPath">Plugins path.</param>
        /// <param name="search">Search.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static List<T> LoadEverything<T>(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly, bool ignoreVersion = false) where T : class
        {
            List<T> outp = new List<T>();
            foreach (FileInfo f in (new DirectoryInfo(pluginsPath)).GetFiles("*" + PluginExtension, search))
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(f.FullName);
                    if (a.IsNot(Resources.RTInfo.RTAssembly) && IsVaild(a)) outp.AddRange(LoadAll<T>(a, ignoreVersion));
                }
                catch { System.Diagnostics.Debug.Print(St.Warn(St.XIsInvalid(St.XYQuotes(St.TheAssembly, f.Name)))); }
            }
            return outp;
        }
        /// <summary>
        /// Carga cualquier plugin disponible.
        /// </summary>
        /// <returns>The whatever.</returns>
        /// <param name="pluginsPath">Plugins path.</param>
        /// <param name="search">Search.</param>
        /// <param name="ignoreVersion">If set to <c>true</c> ignore version.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T LoadWhatever<T>(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly, bool ignoreVersion = false) where T : class
        {
            foreach (FileInfo f in (new DirectoryInfo(pluginsPath)).GetFiles("*" + PluginExtension, search))
            {
                Assembly a = Assembly.LoadFrom(f.FullName);
                if (Has<T>(a)) return Load<T>(a, ignoreVersion);
            }
            throw new PluginClassNotFoundException(typeof(T));
        }
        /// <summary>
        /// Obtiene una lista de los ensamblados que contienen la clase <typeparamref name="T"/>
        /// </summary>
        /// <returns>The dir.</returns>
        /// <param name="pluginsPath">Plugins path.</param>
        /// <param name="search">Search.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static FileInfo[] Dir<T>(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly)
        {
            return (new DirectoryInfo(pluginsPath)).GetFiles("*" + PluginExtension, search).Where((j) =>
            {
                try
                {
                    return Has<T>(Assembly.LoadFrom(j.FullName));
                }
                catch { return false; }
            }).ToArray();
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
        /// <param name="ignoreVersion">
        /// Ignorar la comprobación de versión de MCART.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public static Dictionary<string, List<T>> PluginTree<T>(string pluginsPath = ".", string searchPattern = "*", SearchOption search = SearchOption.TopDirectoryOnly, bool ignoreVersion = false) where T : class
        {
            if (!Directory.Exists(pluginsPath)) throw new DirectoryNotFoundException();
            Dictionary<string, List<T>> outp = new Dictionary<string, List<T>>();
            foreach (FileInfo f in (new DirectoryInfo(pluginsPath)).GetFiles(searchPattern + PluginExtension, search))
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(f.FullName);
                    if (IsVaild(a)) outp.Add(f.Name, LoadAll<T>(a, ignoreVersion).ToList());
                }
                catch (Exception ex) { System.Diagnostics.Debug.Print(ex.Message); }
            }
            return outp;
        }
        /// <summary>
        /// Carga todos los plugins de todos los ensamblados en el directorio
        /// actual como una estructura de árbol.
        /// </summary>
        /// <typeparam name="T">Tipos a cargar.</typeparam>
        /// <param name="ignoreVersion">
        /// Ignorar la comprobación de versión de MCART.
        /// </param>
        /// <returns>
        /// Un <see cref="Dictionary{TKey, TValue}"/> con los ensamblados y 
        /// sus correspondientes plugins.
        /// </returns>
        public static Dictionary<string, List<T>> PluginTree<T>(bool ignoreVersion) where T : class => PluginTree<T>(".", "*", SearchOption.TopDirectoryOnly, ignoreVersion);
        /// <summary>
        /// Obtiene una lista de ensamblados que contienen plugins.
        /// </summary>
        /// <returns>The dir.</returns>
        /// <param name="pluginsPath">Plugins path.</param>
        /// <param name="search">Search.</param>
        [Thunk] public static FileInfo[] Dir(string pluginsPath = ".", SearchOption search = SearchOption.TopDirectoryOnly) => Dir<IPlugin>(pluginsPath, search);
    }
}