﻿//
//  DownloadHelper.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TheXDS.MCART.Networking
{
    /// <summary>
    /// Contiene funciones de descarga de archivos por medio de protocolos web.
    /// </summary>
    public static class DownloadHelper
    {
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto.
        /// </summary>
        /// <param name="url">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        public static void DownloadHttp(string url, Stream stream) => DownloadHttp(new Uri(url), stream);
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto.
        /// </summary>
        /// <param name="uri">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        public static void DownloadHttp(Uri uri, Stream stream)
        {
#if RatherDRY
            Task.WaitAll(DownloadHttpAsync(uri, stream));
#else
            var wr = WebRequest.Create(uri);
            wr.Timeout = 10000;
            var r = wr.GetResponse();
            r.GetResponseStream().CopyTo(stream);
#endif
        }
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto de forma asíncrona.
        /// </summary>
        /// <param name="url">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que representa a la tarea en ejecución.
        /// </returns>
        public static async Task DownloadHttpAsync(string url, Stream stream) => await DownloadHttpAsync(new Uri(url), stream, null, 0);
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto de forma asíncrona.
        /// </summary>
        /// <param name="uri">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que representa a la tarea en ejecución.
        /// </returns>
        public static async Task DownloadHttpAsync(Uri uri, Stream stream) => await DownloadHttpAsync(uri, stream, null, 0);
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto de forma asíncrona.
        /// </summary>
        /// <param name="url">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        /// <param name="reportCallback">
        /// Delegado que permite reportar el estado de esta tarea. El primer
        /// parámetro devolverá la cantidad de bytes recibidos, o <c>null</c>
        /// si <paramref name="stream"/> no es capaz de reportar su tamaño
        /// actual. El segundo parámetro devolverá la longitud total de la
        /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
        /// datos a recibir.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que representa a la tarea en ejecución.
        /// </returns>
        public static async Task DownloadHttpAsync(string url, Stream stream, Action<long?, long> reportCallback) => await DownloadHttpAsync(new Uri(url), stream, reportCallback);
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto de forma asíncrona.
        /// </summary>
        /// <param name="uri">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        /// <param name="reportCallback">
        /// Delegado que permite reportar el estado de esta tarea. El primer
        /// parámetro devolverá la cantidad de bytes recibidos, o <c>null</c>
        /// si <paramref name="stream"/> no es capaz de reportar su tamaño
        /// actual. El segundo parámetro devolverá la longitud total de la
        /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
        /// datos a recibir.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que representa a la tarea en ejecución.
        /// </returns>
        public static async Task DownloadHttpAsync(Uri uri, Stream stream, Action<long?, long> reportCallback) => await DownloadHttpAsync(uri, stream, reportCallback, 100);
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto de forma asíncrona.
        /// </summary>
        /// <param name="url">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        /// <param name="reportCallback">
        /// Delegado que permite reportar el estado de esta tarea. El primer
        /// parámetro devolverá la cantidad de bytes recibidos, o <c>null</c>
        /// si <paramref name="stream"/> no es capaz de reportar su tamaño
        /// actual. El segundo parámetro devolverá la longitud total de la
        /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
        /// datos a recibir.
        /// </param>
        /// <param name="polling">
        /// Intervalo en milisegundos para reportar el estado de la descarga.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que representa a la tarea en ejecución.
        /// </returns>
        public static async Task DownloadHttpAsync(string url, Stream stream, Action<long?, long> reportCallback, int polling) => await DownloadHttpAsync(new Uri(url), stream, reportCallback, polling);
        /// <summary>
        /// Descarga un archivo por medio de http y lo almacena en el
        /// <see cref="Stream"/> provisto de forma asíncrona.
        /// </summary>
        /// <param name="uri">
        /// Url del archivo. Debe ser una ruta http válida.
        /// </param>
        /// <param name="stream">
        /// <see cref="Stream"/> en el cual se almacenará el archivo.
        /// </param>
        /// <param name="reportCallback">
        /// Delegado que permite reportar el estado de esta tarea. El primer
        /// parámetro devolverá la cantidad de bytes recibidos, o <c>null</c>
        /// si <paramref name="stream"/> no es capaz de reportar su tamaño
        /// actual. El segundo parámetro devolverá la longitud total de la
        /// solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
        /// datos a recibir.
        /// </param>
        /// <param name="polling">
        /// Intervalo en milisegundos para reportar el estado de la descarga.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que representa a la tarea en ejecución.
        /// </returns>
        public static async Task DownloadHttpAsync(Uri uri, Stream stream, Action<long?, long> reportCallback, int polling)
        {
            var wr = WebRequest.Create(uri);
            wr.Timeout = 10000;
            var r = await wr.GetResponseAsync();
            var rStream = r.GetResponseStream();
            var ct = new CancellationTokenSource();
            var downloadTask = rStream.CopyToAsync(stream);
            var reportTask = new Task(() =>
            {
                if (!(reportCallback is null))
                {
                    while (!ct?.IsCancellationRequested ?? false)
                    {
                        reportCallback.Invoke(
                            stream.CanSeek ? (long?)stream.Length : null,
                            r.ContentLength);
                        Thread.Sleep(polling);
                    }
                    reportCallback.Invoke(stream.CanSeek ? (long?)stream.Length : null, r.ContentLength);
                }
            }, ct.Token);
            reportTask.Start();
            await downloadTask;
            ct.Cancel();
        }
    }
}