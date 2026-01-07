/*
ImageUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Reflection;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Extracts <see cref="BitmapImage"/> resources from an assembly.
/// </summary>
/// <param name="assembly">
/// Source <see cref="Assembly" />.
/// </param>
/// <param name="path">
/// Path (as a namespace) where the embedded resources will be located at.
/// </param>
public class ImageUnpacker(Assembly assembly, string path) : AssemblyUnpacker<BitmapImage?>(assembly, path)
{
    /// <summary>
    /// Extracts a <see cref="BitmapImage"/> resource with the specified id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="BitmapImage"/> to be extracted.
    /// </param>
    /// <returns>
    /// A <see cref="BitmapImage"/> that has been extracted from the specified
    /// embedded resource id.
    /// </returns>
    public override BitmapImage? Unpack(string id) => WpfUtils.GetBitmap(UnpackStream(id));

    /// <summary>
    /// Extracts a <see cref="BitmapImage"/> resource with the specified id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="BitmapImage"/> to be extracted.
    /// </param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> to use when extracting the resource.
    /// </param>
    /// <returns>
    /// A <see cref="BitmapImage"/> that has been extracted from the specified
    /// embedded resource id.
    /// </returns>
    public override BitmapImage? Unpack(string id, ICompressorGetter compressor) => WpfUtils.GetBitmap(UnpackStream(id, compressor));
}
