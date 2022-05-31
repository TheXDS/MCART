/*
BitmapUnpacker.cs

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
using System.Drawing;
using System.IO;
using System.Reflection;
using TheXDS.MCART.Exceptions;

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

    private static Bitmap GetBitmap(Stream? getter)
    {
        MemoryStream? ms = new();
        (getter ?? throw new MissingResourceException()).CopyTo(ms);
        return new Bitmap(ms);
    }
}
