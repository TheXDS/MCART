/*
DownloadHelper.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones para la descarga de archivos por medio de
protocolos web que se bansen en TCP, como http y ftp.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TheXDS.MCART.Networking
{
	/// <summary>
	///     Contiene funciones de descarga de archivos por medio de protocolos web.
	/// </summary>
	public static class DownloadHelper
	{
		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto.
		/// </summary>
		/// <param name="url">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		public static void DownloadHttp(string url, Stream stream)
		{
			DownloadHttp(new Uri(url), stream);
		}

		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto.
		/// </summary>
		/// <param name="uri">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		public static void DownloadHttp(Uri uri, Stream stream)
		{
#if RatherDRY
            DownloadHttpAsync(uri, stream).GetAwaiter().GetResult();
#else
			var wr = WebRequest.Create(uri);
			wr.Timeout = 10000;
			using (var r = wr.GetResponse())
			{
				r.GetResponseStream()?.CopyTo(stream);
			}
#endif
		}

		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto de forma asíncrona.
		/// </summary>
		/// <param name="url">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		/// <returns>
		///     Un <see cref="Task" /> que representa a la tarea en ejecución.
		/// </returns>
		public static async Task DownloadHttpAsync(string url, Stream stream)
		{
			await DownloadHttpAsync(new Uri(url), stream, null, 0);
		}

		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto de forma asíncrona.
		/// </summary>
		/// <param name="uri">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		/// <returns>
		///     Un <see cref="Task" /> que representa a la tarea en ejecución.
		/// </returns>
		public static async Task DownloadHttpAsync(Uri uri, Stream stream)
		{
			await DownloadHttpAsync(uri, stream, null, 0);
		}

		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto de forma asíncrona.
		/// </summary>
		/// <param name="url">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		/// <param name="reportCallback">
		///     Delegado que permite reportar el estado de esta tarea. El primer
		///     parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
		///     si <paramref name="stream" /> no es capaz de reportar su tamaño
		///     actual. El segundo parámetro devolverá la longitud total de la
		///     solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
		///     datos a recibir.
		/// </param>
		/// <returns>
		///     Un <see cref="Task" /> que representa a la tarea en ejecución.
		/// </returns>
		public static async Task DownloadHttpAsync(string url, Stream stream, ReportCallBack reportCallback)
		{
			await DownloadHttpAsync(new Uri(url), stream, reportCallback);
		}

		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto de forma asíncrona.
		/// </summary>
		/// <param name="uri">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		/// <param name="reportCallback">
		///     Delegado que permite reportar el estado de esta tarea. El primer
		///     parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
		///     si <paramref name="stream" /> no es capaz de reportar su tamaño
		///     actual. El segundo parámetro devolverá la longitud total de la
		///     solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
		///     datos a recibir.
		/// </param>
		/// <returns>
		///     Un <see cref="Task" /> que representa a la tarea en ejecución.
		/// </returns>
		public static async Task DownloadHttpAsync(Uri uri, Stream stream, ReportCallBack reportCallback)
		{
			await DownloadHttpAsync(uri, stream, reportCallback, 100);
		}

		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto de forma asíncrona.
		/// </summary>
		/// <param name="url">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		/// <param name="reportCallback">
		///     Delegado que permite reportar el estado de esta tarea. El primer
		///     parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
		///     si <paramref name="stream" /> no es capaz de reportar su tamaño
		///     actual. El segundo parámetro devolverá la longitud total de la
		///     solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
		///     datos a recibir.
		/// </param>
		/// <param name="polling">
		///     Intervalo en milisegundos para reportar el estado de la descarga.
		/// </param>
		/// <returns>
		///     Un <see cref="Task" /> que representa a la tarea en ejecución.
		/// </returns>
		public static async Task DownloadHttpAsync(string url, Stream stream, ReportCallBack reportCallback,
			int polling)
		{
			await DownloadHttpAsync(new Uri(url), stream, reportCallback, polling);
		}

		/// <summary>
		///     Descarga un archivo por medio de http y lo almacena en el
		///     <see cref="Stream" /> provisto de forma asíncrona.
		/// </summary>
		/// <param name="uri">
		///     Url del archivo. Debe ser una ruta http válida.
		/// </param>
		/// <param name="stream">
		///     <see cref="Stream" /> en el cual se almacenará el archivo.
		/// </param>
		/// <param name="reportCallback">
		///     Delegado que permite reportar el estado de esta tarea. El primer
		///     parámetro devolverá la cantidad de bytes recibidos, o <see langword="null" />
		///     si <paramref name="stream" /> no es capaz de reportar su tamaño
		///     actual. El segundo parámetro devolverá la longitud total de la
		///     solicitud, o <c>-1</c> si el servidor no reportó el tamaño de los
		///     datos a recibir.
		/// </param>
		/// <param name="polling">
		///     Intervalo en milisegundos para reportar el estado de la descarga.
		/// </param>
		/// <returns>
		///     Un <see cref="Task" /> que representa a la tarea en ejecución.
		/// </returns>
		public static async Task DownloadHttpAsync(Uri uri, Stream stream, ReportCallBack reportCallback,
			int polling)
		{
			var wr = WebRequest.Create(uri);
			wr.Timeout = 10000;
		    WebResponse r;
		    CancellationTokenSource ct;

			using (r = await wr.GetResponseAsync())
			using (var rStream = r.GetResponseStream())
			using (ct = new CancellationTokenSource())
			using (var downloadTask = rStream?.CopyToAsync(stream))
			using (var reportTask = new Task(() =>
			{
				if (reportCallback is null) return;
				var t = r.ContentLength > 0 ? r.ContentLength : (long?)null;
				var spd = 0L;
				while (!ct?.IsCancellationRequested ?? false)
				{
					if (stream.CanSeek)
					{
						var l = stream.Length;
						reportCallback.Invoke(l, t, (l - spd) * 1000 / polling);
						spd = l;
					}
					else reportCallback.Invoke(null, t, null);
					Thread.Sleep(polling);
				}

				if (stream.CanSeek)
				{
					spd = stream.Length - spd;
					reportCallback.Invoke(stream.Length, t, spd * 1000 / polling);
				}
				else reportCallback.Invoke(null, t, null);
			}, ct.Token))
			{
				reportTask.Start();
				if (downloadTask != null) await downloadTask;
				ct.Cancel();
				await reportTask;
			}
		}

		/// <summary>
		///     Delegado que describe un método para reportar el estado de una
		///     operación de descarga.
		/// </summary>
		/// <param name="current">
		///     Bytes descargados actualmente.
		/// </param>
		/// <param name="total">
		///     Cuenta total de bytes esperados.
		/// </param>
		/// <param name="speed">
		///     Velocidad de descarga actual, en bytes por segundo.
		/// </param>
		public delegate void ReportCallBack(long? current, long? total, long? speed);
	}
}