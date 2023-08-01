/*
DownloadHelper.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones para la descarga de archivos por medio de
protocolos web que se basen en TCP, como http y ftp.

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

using System.Net;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.IO;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Networking;

/// <summary>
/// Contiene funciones de descarga de archivos por medio de protocolos web.
/// </summary>
[Obsolete("Esta clase utiliza métodos web deprecados en .Net 6.")]
public static class DownloadHelper
{
    private static T GetResponse<T>(Uri uri) where T : WebResponse
    {
        WebRequest? wr = WebRequest.Create(uri);
        wr.Timeout = 10000;
        return wr.GetResponse() as T ?? throw new InvalidUriException(uri);
    }

    private static async Task<T> GetResponseAsync<T>(Uri uri) where T : WebResponse
    {
        WebRequest? wr = WebRequest.Create(uri);
        wr.Timeout = 10000;
        return (await wr.GetResponseAsync()) as T ?? throw new InvalidUriException(uri);
    }

    private static void Copy(WebResponse r, Stream stream)
    {
        r.GetResponseStream()?.CopyTo(stream);
    }
    
    private static async Task Report(Stream targetStream, long totalLength, ReportCallBack cb, int polling, CancellationToken ct)
    {
        long spd = 0L;
        long currentLength = targetStream.Length;
        while (currentLength < totalLength)
        {
            cb.Invoke(currentLength, totalLength, (currentLength - spd) * 1000 / polling);
            spd = currentLength;
            try { await Task.Delay(polling, ct); }
            catch (TaskCanceledException) { return; }
        }
    }

    private static  Task CopyAsync(WebResponse r, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        using Stream rStream = r.GetResponseStream();
        using CancellationTokenSource? ct = new();
        List<Task> tasks = new() { rStream.CopyToAsync(stream) };
        if (r.ContentLength > 0 && reportCallback is { } cb)
        {
            var reportTask = Task.Run(() => Report(stream, r.ContentLength, cb, polling, ct.Token));
            tasks.Add(reportTask);
        }
        return Task.WhenAll(tasks);
    }

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    public static void Download(Uri uri, Stream stream)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        using WebResponse? response = StreamUriParser.Infer<IWebUriParser>(uri)?.GetResponse(uri) ?? throw new UriFormatException();
        Copy(response, stream);
    }

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    public static void Download(string url, Stream stream) => Download(new Uri(url), stream);

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El
    /// primer parámetro devolverá la cantidad de bytes recibidos, o
    /// <see langword="null" /> si <paramref name="stream" /> no es
    /// capaz de reportar su tamaño actual. El segundo parámetro
    /// devolverá la longitud total de la solicitud, o <c>-1</c> si el
    /// servidor no reportó el tamaño de los datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la
    /// descarga.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    public static async Task DownloadAsync(Uri uri, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        Task<WebResponse>? r = StreamUriParser.Infer<IWebUriParser>(uri)?.GetResponseAsync(uri) ?? throw new UriFormatException();
        using WebResponse? response = await r;
        await CopyAsync(response, stream, reportCallback, polling);
    }

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El
    /// primer parámetro devolverá la cantidad de bytes recibidos, o
    /// <see langword="null" /> si <paramref name="stream" /> no es
    /// capaz de reportar su tamaño actual. El segundo parámetro
    /// devolverá la longitud total de la solicitud, o <c>-1</c> si el
    /// servidor no reportó el tamaño de los datos a recibir.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    public static Task DownloadAsync(Uri uri, Stream stream, ReportCallBack reportCallback)
    {
        return DownloadAsync(uri, stream, reportCallback, 100);
    }

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    public static Task DownloadAsync(Uri uri, Stream stream)
    {
        return DownloadAsync(uri, stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El
    /// primer parámetro devolverá la cantidad de bytes recibidos, o
    /// <see langword="null" /> si <paramref name="stream" /> no es
    /// capaz de reportar su tamaño actual. El segundo parámetro
    /// devolverá la longitud total de la solicitud, o <c>-1</c> si el
    /// servidor no reportó el tamaño de los datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la
    /// descarga.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    public static Task DownloadAsync(string url, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        return DownloadAsync(new Uri(url), stream, reportCallback, polling);
    }

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El
    /// primer parámetro devolverá la cantidad de bytes recibidos, o
    /// <see langword="null" /> si <paramref name="stream" /> no es
    /// capaz de reportar su tamaño actual. El segundo parámetro
    /// devolverá la longitud total de la solicitud, o <c>-1</c> si el
    /// servidor no reportó el tamaño de los datos a recibir.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    public static Task DownloadAsync(string url, Stream stream, ReportCallBack reportCallback)
    {
        return DownloadAsync(url, stream, reportCallback, 100);
    }

    /// <summary>
    /// Descarga un archivo desde un servicio web y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta web válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    public static Task DownloadAsync(string url, Stream stream)
    {
        return DownloadAsync(url, stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static HttpStatusCode DownloadHttp(string url, Stream stream)
    {
        return DownloadHttp(new Uri(url), stream);
    }

    /// <summary>
    /// Descarga un archivo por medio de ftp y lo almacena en el
    /// <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static FtpStatusCode DownloadFtp(string url, Stream stream)
    {
        return DownloadFtp(new Uri(url), stream);
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta de protocolo file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool DownloadFile(string url, Stream stream)
    {
        return DownloadFile(new Uri(url), stream);
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="uri">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static HttpStatusCode DownloadHttp(Uri uri, Stream stream)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        using HttpWebResponse? r = GetResponse<HttpWebResponse>(uri);
        if (r.StatusCode == HttpStatusCode.OK) Copy(r, stream);
        return r.StatusCode;
    }

    /// <summary>
    /// Descarga un archivo por medio de ftp y lo almacena en el
    /// <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static FtpStatusCode DownloadFtp(Uri uri, Stream stream)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        using FtpWebResponse? r = GetResponse<FtpWebResponse>(uri);
        if (r.StatusCode == FtpStatusCode.CommandOK) Copy(r, stream);
        return r.StatusCode;
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta de protocolo
    /// file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool DownloadFile(Uri uri, Stream stream)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        try
        {
            using FileWebResponse? r = GetResponse<FileWebResponse>(uri);
            Copy(r, stream);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<HttpStatusCode> DownloadHttpAsync(string url, Stream stream)
    {
        return DownloadHttpAsync(new Uri(url), stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<HttpStatusCode> DownloadHttpAsync(Uri uri, Stream stream)
    {
        return DownloadHttpAsync(uri, stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<HttpStatusCode> DownloadHttpAsync(string url, Stream stream, ReportCallBack? reportCallback)
    {
        return DownloadHttpAsync(new Uri(url), stream, reportCallback);
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<HttpStatusCode> DownloadHttpAsync(Uri uri, Stream stream, ReportCallBack? reportCallback)
    {
        return DownloadHttpAsync(uri, stream, reportCallback, 100);
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la descarga.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<HttpStatusCode> DownloadHttpAsync(string url, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        return DownloadHttpAsync(new Uri(url), stream, reportCallback, polling);
    }

    /// <summary>
    /// Descarga un archivo por medio de http y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// Url del archivo. Debe ser una ruta http válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la descarga.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static async Task<HttpStatusCode> DownloadHttpAsync(Uri uri, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        using HttpWebResponse? r = await GetResponseAsync<HttpWebResponse>(uri);
        if (r.StatusCode == HttpStatusCode.OK)
        {
            await CopyAsync(r, stream, reportCallback, polling);
        }
        return r.StatusCode;
    }

    /// <summary>
    /// Descarga un archivo por medio de Ftp y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta Ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<FtpStatusCode> DownloadFtpAsync(string url, Stream stream)
    {
        return DownloadFtpAsync(new Uri(url), stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo por medio de Ftp y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// Url del archivo. Debe ser una ruta Ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<FtpStatusCode> DownloadFtpAsync(Uri uri, Stream stream)
    {
        return DownloadFtpAsync(uri, stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo por medio de Ftp y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta Ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<FtpStatusCode> DownloadFtpAsync(string url, Stream stream, ReportCallBack? reportCallback)
    {
        return DownloadFtpAsync(new Uri(url), stream, reportCallback);
    }

    /// <summary>
    /// Descarga un archivo por medio de Ftp y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// Url del archivo. Debe ser una ruta Ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<FtpStatusCode> DownloadFtpAsync(Uri uri, Stream stream, ReportCallBack? reportCallback)
    {
        return DownloadFtpAsync(uri, stream, reportCallback, 100);
    }

    /// <summary>
    /// Descarga un archivo por medio de Ftp y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta Ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la descarga.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    /// <returns>
    /// El código de estado que el servidor ha devuelto.
    /// </returns>
    public static Task<FtpStatusCode> DownloadFtpAsync(string url, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        return DownloadFtpAsync(new Uri(url), stream, reportCallback, polling);
    }

    /// <summary>
    /// Descarga un archivo por medio de Ftp y lo almacena en el
    /// <see cref="Stream" /> especificado de forma asíncrona.
    /// </summary>
    /// <param name="uri">
    /// Url del archivo. Debe ser una ruta Ftp válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la descarga.
    /// </param>
    /// <returns>
    /// Un <see cref="Task" /> que representa a la tarea en ejecución.
    /// </returns>
    public static async Task<FtpStatusCode> DownloadFtpAsync(Uri uri, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        using FtpWebResponse? r = await GetResponseAsync<FtpWebResponse>(uri);
        if (r.StatusCode == FtpStatusCode.CommandOK)
        {
            await CopyAsync(r, stream, reportCallback, polling);
        }
        return r.StatusCode;
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta de protocolo file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static Task<bool> DownloadFileAsync(string url, Stream stream)
    {
        return DownloadFileAsync(new Uri(url), stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta de protocolo
    /// file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static Task<bool> DownloadFileAsync(Uri uri, Stream stream)
    {
        return DownloadFileAsync(uri, stream, null, 0);
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta de protocolo file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static Task<bool> DownloadFileAsync(string url, Stream stream, ReportCallBack? reportCallback)
    {
        return DownloadFileAsync(new Uri(url), stream, reportCallback);
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta de protocolo
    /// file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static Task<bool> DownloadFileAsync(Uri uri, Stream stream, ReportCallBack? reportCallback)
    {
        return DownloadFileAsync(uri, stream, reportCallback, 100);
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="url">
    /// Url del archivo. Debe ser una ruta de protocolo file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la descarga.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static Task<bool> DownloadFileAsync(string url, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        return DownloadFileAsync(new Uri(url), stream, reportCallback, polling);
    }

    /// <summary>
    /// Descarga un archivo por medio del protocolo file del sistema
    /// operativo y lo almacena en el <see cref="Stream" /> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> del archivo. Debe ser una ruta de protocolo
    /// file válida.
    /// </param>
    /// <param name="stream">
    /// <see cref="Stream" /> en el cual se almacenará el archivo.
    /// </param>
    /// <param name="reportCallback">
    /// Delegado que permite reportar el estado de esta tarea. El primer
    /// parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
    /// si <paramref name="stream" /> no es capaz de reportar su tamaño
    /// actual. El segundo parámetro devolverá la longitud total de la
    /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
    /// datos a recibir.
    /// </param>
    /// <param name="polling">
    /// Intervalo en milisegundos para reportar el estado de la descarga.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la operación se completa
    /// exitosamente, <see langword="false"/> en caso contrario.
    /// </returns>
    public static async Task<bool> DownloadFileAsync(Uri uri, Stream stream, ReportCallBack? reportCallback, int polling)
    {
        if (!stream.CanWrite) throw new NotSupportedException();
        try
        {
            using FileWebResponse? r = await GetResponseAsync<FileWebResponse>(uri);
            await CopyAsync(r, stream, reportCallback, polling);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Delegado que describe un método para reportar el estado de una
    /// operación de descarga.
    /// </summary>
    /// <param name="current">
    /// Bytes descargados actualmente.
    /// </param>
    /// <param name="total">
    /// Cuenta total de bytes esperados.
    /// </param>
    /// <param name="speed">
    /// Velocidad de descarga actual, en bytes por segundo.
    /// </param>
    public delegate void ReportCallBack(long? current, long? total, long? speed);
}
