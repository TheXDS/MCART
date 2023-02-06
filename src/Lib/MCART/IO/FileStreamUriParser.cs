/*
FileStreamUriParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO;

/// <summary>
/// Obtiene un <see cref="Stream"/> a partir de la ruta de archivo
/// especificada por un <see cref="Uri"/>.
/// </summary>
#if NET6_0_OR_GREATER
[Obsolete("Esta clase utiliza métodos web deprecados en .Net 6.")]
#endif
public class FileStreamUriParser : SimpleStreamUriParser, IWebUriParser
{
    /// <summary>
    /// Enumera los esquemas soportados por este
    /// <see cref="StreamUriParser"/>.
    /// </summary>
    protected override IEnumerable<string> SchemeList
    {
        get
        {
            yield return "file";
        }
    }

    /// <summary>
    /// Obtiene una respuesta Web a partir del <see cref="Uri"/>
    /// especificado.
    /// </summary>
    /// <param name="uri">Dirección web a resolver.</param>
    /// <returns>
    /// La respuesta enviada por un servidor web.
    /// </returns>
    public WebResponse GetResponse(Uri uri)
    {
        return WebRequest.Create(uri).GetResponse();
    }

    /// <summary>
    /// Obtiene una respuesta Web a partir del <see cref="Uri"/>
    /// especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">Dirección web a resolver.</param>
    /// <returns>
    /// La respuesta enviada por un servidor web.
    /// </returns>
    public Task<WebResponse> GetResponseAsync(Uri uri)
    {
        return WebRequest.Create(uri).GetResponseAsync();
    }

    /// <summary>
    /// Abre un <see cref="Stream"/> desde el <see cref="Uri"/>
    /// especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a abrir para lectura.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
    /// apuntado por el <see cref="Uri"/> especificado.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// Se produce si el recurso apuntado por <paramref name="uri"/> no
    /// existe.
    /// </exception>
    /// <exception cref="System.Security.SecurityException">
    /// Se produce si no se tienen los permisos suficientes para
    /// realizar esta operación.
    /// </exception>
    /// <exception cref="IOException">
    /// Se produce si hay un problema de entrada/salida al abrir el
    /// recurso apuntado por <paramref name="uri"/>.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    /// Se produce si el directorio apuntado en la ruta de
    /// <paramref name="uri"/> no existe.
    /// </exception>
    /// <exception cref="PathTooLongException">
    /// Se produce si la longitud de la ruta de archivo excede los
    /// límites permitidos por el sistema operativo.
    /// </exception>
    public override Stream? Open(Uri uri)
    {
        if (uri.OriginalString.StartsWith("file://"))
        {
            try
            {
                return GetResponse(uri).GetResponseStream();
            }
            catch
            {
                return null;
            }
        }
        else
        {
            if (!File.Exists(uri.OriginalString)) return null;
            return new FileStream(uri.OriginalString, FileMode.Open);
        }
    }
}
