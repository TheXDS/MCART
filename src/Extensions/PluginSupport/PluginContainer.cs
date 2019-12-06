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

using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.PluginSupport
{
    public sealed class PluginContainer : Disposable
    {
        private class PluginAssemblyLoadContext : AssemblyLoadContext
        {
            public readonly Assembly _loadedAssembly;
            private readonly AssemblyDependencyResolver _resolver;

            public PluginAssemblyLoadContext(FileInfo assembly) : base(true)
            {
                _resolver = new AssemblyDependencyResolver(assembly.FullName);
                _loadedAssembly = LoadFromAssemblyPath(assembly.FullName);
            }

            protected override Assembly? Load(AssemblyName assemblyName)
            {
                return (_resolver.ResolveAssemblyToPath(assemblyName) is string path)
                    ? LoadFromAssemblyPath(path)
                    : null;
            }
        }

        private readonly PluginAssemblyLoadContext _context;

        internal PluginContainer(FileInfo assembly)
        {
            if (!assembly.Exists) throw new FileNotFoundException(null, assembly.FullName);
            _context = new PluginAssemblyLoadContext(assembly);
        }

        internal Assembly Assembly => _context._loadedAssembly;

        protected override void OnDispose()
        {
            _context.Unload();            
        }
    }    
}
