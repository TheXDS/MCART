/*
ImageUnpacker.cs

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

using System.Reflection;
using System.Windows.Media.Imaging;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// <see cref="AssemblyUnpacker{T}" /> que
    /// extrae recursos de imagen como un
    /// <see cref="BitmapImage" />.
    /// </summary>
    public class ImageUnpacker : AssemblyUnpacker<BitmapImage?>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ImageUnpacker" />.
        /// </summary>
        /// <param name="assembly">
        /// <see cref="Assembly" /> de orígen de los recursos incrustados.
        /// </param>
        /// <param name="path">
        /// Ruta (como espacio de nombre) donde se ubican los recursos
        /// incrustados.
        /// </param>
        public ImageUnpacker(Assembly assembly, string path) : base(assembly, path) { }

        /// <summary>
        /// Obtiene un <see cref="BitmapImage" /> desde los recursos incrustados
        /// del ensamblado.
        /// </summary>
        /// <param name="id">Nombre del recurso a extraer.</param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> extraído desde los recursos
        /// incrustados del ensamblado.
        /// </returns>
        public override BitmapImage? Unpack(string id) => WpfUi.GetBitmap(UnpackStream(id));

        /// <summary>
        /// Obtiene un <see cref="BitmapImage" /> desde los recursos incrustados
        /// comprimidos del ensamblado.
        /// </summary>
        /// <param name="id">Nombre del recurso a extraer.</param>
        /// <param name="compressorId">
        /// Nombre del compresor a utilizar para extraer el recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> extraído desde los recursos
        /// incrustados comprimidos del ensamblado.
        /// </returns>
        public override BitmapImage? Unpack(string id, string compressorId) => WpfUi.GetBitmap(UnpackStream(id, compressorId));

        /// <summary>
        /// Obtiene un <see cref="BitmapImage" /> desde los recursos incrustados
        /// comprimidos del ensamblado.
        /// </summary>
        /// <param name="id">Nombre del recurso a extraer.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter" /> desde el cual se obtendrá el
        /// compresor a utilizar para extraer el recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> extraído desde los recursos
        /// incrustados comprimidos del ensamblado.
        /// </returns>
        public override BitmapImage? Unpack(string id, ICompressorGetter compressor) => WpfUi.GetBitmap(UnpackStream(id, compressor));
    }
}