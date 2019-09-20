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
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Linq;

namespace TheXDS.MCART.PluginSupport
{
    /// <summary>
    ///     Define un contexto que permite alojar plugins cargables desde
    ///     ensamblados.
    /// </summary>
    public class PluginContext : IDisposable
    {
        private class PluginAssemblyLoadContext : AssemblyLoadContext
        {
            private readonly AssemblyDependencyResolver _resolver;

            public PluginAssemblyLoadContext(FileInfo assembly) : base(true)
            {
                _resolver = new AssemblyDependencyResolver(assembly.FullName);
            }

            protected override Assembly? Load(AssemblyName assemblyName)
            {
                return (_resolver.ResolveAssemblyToPath(assemblyName) is string path)
                    ? LoadFromAssemblyPath(path)
                    : null;
            }
        }

        private readonly PluginAssemblyLoadContext _context;

        public PluginContext(FileInfo assembly)
        {
            if (!assembly.Exists) throw new FileNotFoundException(null, assembly.FullName);
            _context = new PluginAssemblyLoadContext(assembly);
        }










        public bool Disposed { get; private set; }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            _context.Unload();
        }
    }
}
