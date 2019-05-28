﻿/*
XamlUnpacker.cs

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

using System.Reflection;
using System.Windows.Markup;
using System.Xml;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    ///     Extrae recursos incrustados Xaml desde el ensamblado especificado.
    /// </summary>
    public class XamlUnpacker : AssemblyUnpacker<object>
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase 
        ///     <see cref="XamlUnpacker"/>.
        /// </summary>
        /// <param name="assembly">
        ///     <see cref="Assembly" /> de orígen de los recursos incrustados.
        /// </param>
        /// <param name="path">
        ///     Ruta (como espacio de nombre) donde se ubican los recursos
        ///     incrustados.
        /// </param>
        public XamlUnpacker(Assembly assembly, string path) : base(assembly, path)
        {
        }

        public override object Unpack(string id)
        {
            using (var sr = UnpackStream(id))
            {
                return XamlReader.Load(XmlReader.Create(sr));
            }
        }

        public override object Unpack(string id, string compressorId)
        {
            using (var sr = UnpackStream(id, compressorId))
            {
                return XamlReader.Load(XmlReader.Create(sr));
            }
        }

        public override object Unpack(string id, ICompressorGetter compressor)
        {
            using (var sr = UnpackStream(id, compressor))
            {
                return XamlReader.Load(XmlReader.Create(sr));
            }
        }
    }
}