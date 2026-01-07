/*
PackStreamUriParser.cs

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

using System.IO;
using System.IO.Packaging;
using System.Windows;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO;

/// <summary>
/// Translates a packed resource URI (pack://) into a read Stream for an
/// embedded resource.
/// </summary>
public class PackStreamUriParser : SimpleStreamUriParser
{
    /// <summary>
    /// Initializes the <see cref="PackStreamUriParser"/> class.
    /// </summary>
    static PackStreamUriParser()
    {
        if (!UriParser.IsKnownScheme(PackUriHelper.UriSchemePack))
        {
            UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), PackUriHelper.UriSchemePack, -1);
        }
    }

    /// <summary>
    /// Enumerates the URI schemes supported by this
    /// <see cref="StreamUriParser"/>.
    /// </summary>
    protected override IEnumerable<string> SchemeList
    {
        get
        {
            yield return PackUriHelper.UriSchemePack;
        }
    }

    /// <summary>
    /// Opens a <see cref="Stream"/> that allows reading the embedded
    /// resource referenced by the specified <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">
    /// The <see cref="Uri"/> that indicates the location of the embedded
    /// resource.
    /// </param>
    /// <returns>
    /// A <see cref="Stream"/> from which the embedded resource can be read,
    /// or <see langword="null"/> if the <see cref="Uri"/> does not point to a
    /// valid embedded resource.
    /// </returns>
    public override Stream? Open(Uri uri)
    {
        try
        {
            return Application.GetResourceStream(uri).Stream;
        }
        catch
        {
            return null;
        }
    }
}
