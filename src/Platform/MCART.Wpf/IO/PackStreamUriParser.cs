/*
PackStreamUriParser.cs

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

namespace TheXDS.MCART.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using TheXDS.MCART.Types.Base;

/// <summary>
/// Traduce un URI de recursos incrustados (pack://) a un
/// <see cref="Stream"/> de lectura para un recurso incrustado.
/// </summary>
public class PackStreamUriParser : SimpleStreamUriParser
{
    /// <summary>
    /// Inicializa la clase <see cref="PackStreamUriParser"/>
    /// </summary>
    static PackStreamUriParser()
    {
        if (!UriParser.IsKnownScheme(PackUriHelper.UriSchemePack))
        {
            UriParser.Register(new GenericUriParser(GenericUriParserOptions.GenericAuthority), PackUriHelper.UriSchemePack, -1);
        }
    }

    /// <summary>
    /// Enumera los esquemas de URI soportados por este 
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
    /// Abre un <see cref="Stream"/> que permita leer el recurso incrustado
    /// referenciado por el <see cref="Uri"/> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> que indica la ubicación del recurso incrustado.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual es posible leer el recurso
    /// incrustado, o <see langword="null"/> si el <see cref="Uri"/> no
    /// apunta a un recurso incrustado válido.
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
