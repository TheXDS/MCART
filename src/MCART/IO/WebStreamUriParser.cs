/*
WebStreamUriParser.cs

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
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Networking;
using TheXDS.MCART.Types.Base;

/// <summary>
/// Obtiene un <see cref="Stream"/> a partir de un <see cref="Uri"/>
/// que apunta a un recurso web.
/// </summary>
#if NET6_0_OR_GREATER
[Obsolete("Esta clase utiliza métodos web deprecados en .Net 6.")]
#endif
public abstract class WebStreamUriParser<T> : SimpleStreamUriParser, IWebUriParser where T : WebResponse
{
    /// <summary>
    /// Abre un <see cref="Stream"/> desde el <see cref="Uri"/>
    /// especificado.
    /// </summary>
    /// <param name="uri">Dirección web a resolver.</param>
    /// <returns>
    /// Un <see cref="Stream"/> que permite obtener el recurso apuntado
    /// por el <see cref="Uri"/> especificado.
    /// </returns>
    public sealed override Stream Open(Uri uri)
    {
        return GetResponse(uri).GetResponseStream();
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
        WebRequest? req = WebRequest.Create(uri);
        req.Timeout = 10000;
        return req.GetResponse() as T ?? throw new InvalidUriException(uri);
    }

    /// <summary>
    /// Obtiene una respuesta Web a partir del <see cref="Uri"/>
    /// especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">Dirección web a resolver.</param>
    /// <returns>
    /// La respuesta enviada por un servidor web.
    /// </returns>
    public async Task<WebResponse> GetResponseAsync(Uri uri)
    {
        WebRequest? req = WebRequest.Create(uri);
        req.Timeout = 10000;
        return await req.GetResponseAsync() as T ?? throw new InvalidUriException(uri);
    }

    /// <summary>
    /// Obtiene un valor que indica si este objeto prefiere
    /// transferencias completas a la hora de exponer un 
    /// <see cref="Stream"/>.
    /// </summary>
    public override bool PreferFullTransfer => true;

    /// <summary>
    /// Abre un <see cref="Stream"/> desde el <see cref="Uri"/>
    /// especificado, haciendo una transferencia completa a la memoria
    /// del equipo.
    /// </summary>
    /// <param name="uri">Dirección web a resolver.</param>
    /// <returns>
    /// Un <see cref="Stream"/> que permite obtener el recurso apuntado
    /// por el <see cref="Uri"/> especificado.
    /// </returns>
    public override async Task<Stream?> OpenFullTransferAsync(Uri uri)
    {
        try
        {
            MemoryStream? ms = new();
            await DownloadHelper.DownloadAsync(uri, ms);
            return ms;
        }
        catch
        {
            return null;
        }
    }
}
