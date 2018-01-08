//
//  Misc.cs
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
using System.Threading.Tasks;

namespace TheXDS.MCART.Networking
{
    /// <summary>
    /// Funciones varias para trabajar con objetos de red.
    /// </summary>
    public static class Misc
    {
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
            WriteToStream(r.GetResponseStream(), stream);
#endif
        }
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
        public static async Task DownloadHttpAsync(Uri uri, Stream stream)
        {
            var wr = WebRequest.Create(uri);
            wr.Timeout = 10000;
            var r = await wr.GetResponseAsync();
            WriteToStream(r.GetResponseStream(), stream);
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
        public static async Task DownloadHttpAsync(string url, Stream stream) => await DownloadHttpAsync(new Uri(url), stream);

        private static void WriteToStream(Stream @in, Stream @out)
        {
            if (!(@in.CanRead && @out.CanWrite)) throw new InvalidOperationException();            
#if BufferedIO
            // 64 KiB de búffer parece razonable...
            var b = new byte[65536];
            var rd = @in.Read(b, 0, b.Length);
            while (rd > 0)
            {
                @out.Write(b, 0, rd);
                rd = @in.Read(b, 0, b.Length);
            }
#else
            var b = new byte[@in.Length];
            @in.Read(b, 0, b.Length);
            @out.Write(b, 0, b.Length);
#endif
        }
    }
}