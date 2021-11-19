/*
BitmapUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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

using System.Drawing;
using System.IO;
using System.Reflection;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Extrae recursos de mapa de bits desde el ensamblado especificado.
    /// </summary>
    public class BitmapUnpacker : AssemblyUnpacker<Bitmap>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="BitmapUnpacker"/>.
        /// </summary>
        /// <param name="assembly">
        /// <see cref="Assembly" /> de orígen de los recursos incrustados.
        /// </param>
        /// <param name="path">
        /// Ruta (como espacio de nombre) donde se ubican los recursos
        /// incrustados.
        /// </param>
        public BitmapUnpacker(Assembly assembly, string path) : base(assembly, path) { }

        /// <summary>
        /// Extrae un mapa de bits con el id especificado.
        /// </summary>
        /// <param name="id">
        /// Id del mapa de bits a extraer.
        /// </param>
        /// <returns>
        /// Un mapa de bits extraído del recurso con el id especificado.
        /// </returns>
        public override Bitmap Unpack(string id)
        {
            return GetBitmap(UnpackStream(id));
        }

        /// <summary>
        /// Extrae un mapa de bits con el id especificado.
        /// </summary>
        /// <param name="id">
        /// Id del mapa de bits a extraer.
        /// </param>
        /// <param name="compressorId">
        /// Id del compresor a utilizar para extraer el recurso.
        /// </param>
        /// <returns>
        /// Un mapa de bits extraído del recurso con el id especificado.
        /// </returns>
        public override Bitmap Unpack(string id, string compressorId)
        {
            return GetBitmap(UnpackStream(id, compressorId));
        }

        /// <summary>
        /// Extrae un mapa de bits con el id especificado.
        /// </summary>
        /// <param name="id">
        /// Id del mapa de bits a extraer.
        /// </param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer el
        /// recurso.
        /// </param>
        /// <returns>
        /// Un mapa de bits extraído del recurso con el id especificado.
        /// </returns>
        public override Bitmap Unpack(string id, ICompressorGetter compressor)
        {
            return GetBitmap(UnpackStream(id, compressor));
        }

        private Bitmap GetBitmap(Stream? getter)
        {
            var ms = new MemoryStream();
            (getter ?? throw new MissingResourceException()).CopyTo(ms);
            return new Bitmap(ms);
        }
    }
}