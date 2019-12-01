/*
PluginBrowser.cs

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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.PluginSupport
{

    public sealed class PluginDomain : IDisposable
    {
        private readonly HashSet<PluginAssembly> _loadedAssemblies = new HashSet<PluginAssembly>();

        public void Dispose()
        {
            if (!_loadedAssemblies.Any()) return;
            Parallel.ForEach(_loadedAssemblies, p => p.Dispose());
            _loadedAssemblies.Clear();
            GC.WaitForPendingFinalizers();
        }

        public PluginDomain(DirectoryInfo directory)
        {
            foreach (var j in directory.GetFiles(InferPluginExtension(), GetEnumerationOptions()))
            {
                if (!TryLoadAssembly(j, out var asm)) continue;
                _loadedAssemblies.Add(asm!);
            }
        }






        
        private static bool TryLoadAssembly(FileInfo file, out PluginAssembly? asm)
        {
            try
            {
                asm = new PluginAssembly(file);
                return true;
            }
            catch
            {
                asm = null;
                return false;
            }
        }

        private static EnumerationOptions GetEnumerationOptions()
        {
            return new EnumerationOptions()
            {
                IgnoreInaccessible = true,
                MatchCasing= MatchCasing.PlatformDefault,
                MatchType = MatchType.Simple,
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            };
        }

        private static string InferPluginExtension()
        {
            return (Environment.OSVersion.Platform) switch
            {
                PlatformID.Win32NT => "*.dll",
                PlatformID.Unix => "*.so",
                _ => string.Empty
            };
        }
    }

    /// <summary>
    ///     Define un contexto que permite alojar plugins cargables desde
    ///     ensamblados. Esta clase no puede heredarse.
    /// </summary>
    public sealed class PluginAssembly : Disposable
    {
        private class PluginAssemblyLoadContext : AssemblyLoadContext
        {
            public Assembly LoadedAssembly { get; }
            private readonly AssemblyDependencyResolver _resolver;

            public PluginAssemblyLoadContext(FileInfo assembly) : base(true)
            {
                _resolver = new AssemblyDependencyResolver(assembly.FullName);
                LoadedAssembly = LoadFromAssemblyPath(assembly.FullName);
            }

            protected override Assembly? Load(AssemblyName assemblyName)
            {
                return (_resolver.ResolveAssemblyToPath(assemblyName) is string path)
                    ? LoadFromAssemblyPath(path)
                    : null;
            }
        }

        private readonly PluginAssemblyLoadContext _context;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PluginAssembly"/>, cargando el archivo especificado.
        /// </summary>
        /// <param name="assembly">
        ///     Ruta del archivo a cargar. La ruta debe apuntar a un ensamblado
        ///     cargable por el CLR.
        /// </param>
        public PluginAssembly(FileInfo assembly)
        {
            if (!assembly.Exists) throw new FileNotFoundException(null, assembly.FullName);
            _context = new PluginAssemblyLoadContext(assembly);
        }










        protected override void OnDispose()
        {
            _context.Unload();            
        }
    }    
}
