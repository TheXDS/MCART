/*
ImageUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Resources;
using System.Reflection;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Helpers;

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
    public override BitmapImage? Unpack(string id) => WpfUtils.GetBitmap(UnpackStream(id));

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
    public override BitmapImage? Unpack(string id, string compressorId) => WpfUtils.GetBitmap(UnpackStream(id, compressorId));

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
    public override BitmapImage? Unpack(string id, ICompressorGetter compressor) => WpfUtils.GetBitmap(UnpackStream(id, compressor));
}
