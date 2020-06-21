/*
PluginHost.cs

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Base;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Collections;

namespace TheXDS.MCART.PluginSupport
{
    public sealed class PluginHost : Disposable, IEnumerable<Assembly>, IEnumerable<PluginContainer>
    {
        private readonly HashSet<PluginContainer> _loadedContainers = new HashSet<PluginContainer>();

        /// <summary>
        /// Carga todos los ensamblados de un directorio en este
        /// <see cref="PluginHost"/>.
        /// </summary>
        /// <param name="directory"></param>
        public void LoadFrom(DirectoryInfo directory)
        {
            foreach (var j in directory.GetFiles(InferPluginExtension(), GetEnumerationOptions()))
            {
                if (TryLoadAssembly(j, out var asm)) _loadedContainers.Add(asm!);
            }
        }




        /// <summary>
        /// Finaliza los plugins cargados en este host, y los descarga.
        /// </summary>
        /// <remarks>
        /// Luego de llamar a este método, se esperará a que los
        /// finalizadores pendientes en el <see cref="GC"/> se ejecuten.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unload()
        {
            if (!_loadedContainers.Any()) return;
            Parallel.ForEach(_loadedContainers, p => p.Dispose());
            _loadedContainers.Clear();
            GC.WaitForPendingFinalizers();
        }

        public void Load()
        {
            
        }


        protected override void OnDispose()
        {
            Unload();
        }

        private static bool TryLoadAssembly(FileInfo file, out PluginContainer? asm)
        {
            try
            {
                asm = new PluginContainer(file);
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
                MatchCasing = MatchCasing.PlatformDefault,
                MatchType = MatchType.Simple,
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            };
        }

        private static string InferPluginExtension()
        {
            return Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => "*.dll",
                PlatformID.Unix => "*.so",
                _ => string.Empty
            };
        }

        IEnumerator<Assembly> IEnumerable<Assembly>.GetEnumerator()
        {
            return _loadedContainers.Select(p => p.Assembly).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        public IEnumerator<PluginContainer> GetEnumerator()
        {
            return _loadedContainers.GetEnumerator();
        }
    }
}