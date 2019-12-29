/*
Icons.cs

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

using System.Drawing;
using System.Runtime.CompilerServices;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene íconos y otras imágenes para utilizar en cualquier aplicación.
    /// </summary>
    public sealed class Icons : McartIconLibrary<Bitmap>
    {
        private static readonly BitmapUnpacker _imgs = new BitmapUnpacker(typeof(Icons).Assembly, typeof(Icons).FullName!);

        /// <summary>
        /// Implementa el método de obtención del ícono basado en el nombre
        /// del ícono solicitado.
        /// </summary>
        /// <param name="id">
        /// Id del ícono solicitado.
        /// </param>
        /// <returns>
        /// El ícono solicitado.
        /// </returns>
        protected override sealed Bitmap GetIcon([CallerMemberName] string? id = null)
        {
            return _imgs.Unpack($"{id}.png", new NullGetter());
        }
    }
}