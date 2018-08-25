/*
ImageUnpacker.cs

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

using System.Reflection;
using System.Windows.Media.Imaging;

namespace TheXDS.MCART.Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     <see cref="T:TheXDS.MCART.Resources.AssemblyUnpacker`1" /> que
    ///     extrae recursos de imagen como un
    ///     <see cref="T:System.Windows.Media.Imaging.BitmapImage" />.
    /// </summary>
    public class ImageUnpacker : AssemblyUnpacker<BitmapImage>
    {
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="T:TheXDS.MCART.Resources.ImageUnpacker" />.
        /// </summary>
        /// <param name="assembly">
        /// <see cref="T:System.Reflection.Assembly" /> de orígen de los recursos incrustados.
        /// </param>
        /// <param name="path">
        /// Ruta (como espacio de nombre) donde se ubican los recursos
        /// incrustados.
        /// </param>
        public ImageUnpacker(Assembly assembly, string path) : base(assembly, path) { }
        /// <inheritdoc />
        /// <summary>
        /// Obtiene un <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> desde los recursos incrustados
        /// del ensamblado.
        /// </summary>
        /// <param name="id">Nombre del recurso a extraer.</param>
        /// <returns>
        /// Un <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> extraído desde los recursos
        /// incrustados del ensamblado.
        /// </returns>
        public override BitmapImage Unpack(string id) => UI.GetBitmap(UnpackStream(id));
        /// <inheritdoc />
        /// <summary>
        /// Obtiene un <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> desde los recursos incrustados
        /// comprimidos del ensamblado.
        /// </summary>
        /// <param name="id">Nombre del recurso a extraer.</param>
        /// <param name="compressorId">
        /// Nombre del compresor a utilizar para extraer el recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> extraído desde los recursos
        /// incrustados comprimidos del ensamblado.
        /// </returns>
        public override BitmapImage Unpack(string id, string compressorId) => UI.GetBitmap(UnpackStream(id, compressorId));
        /// <inheritdoc />
        /// <summary>
        /// Obtiene un <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> desde los recursos incrustados
        /// comprimidos del ensamblado.
        /// </summary>
        /// <param name="id">Nombre del recurso a extraer.</param>
        /// <param name="compressor">
        /// <see cref="T:TheXDS.MCART.Resources.ICompressorGetter" /> desde el cual se obtendrá el
        /// compresor a utilizar para extraer el recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="T:System.Windows.Media.Imaging.BitmapImage" /> extraído desde los recursos
        /// incrustados comprimidos del ensamblado.
        /// </returns>
        public override BitmapImage Unpack(string id, ICompressorGetter compressor) => UI.GetBitmap(UnpackStream(id, compressor));
    }
}