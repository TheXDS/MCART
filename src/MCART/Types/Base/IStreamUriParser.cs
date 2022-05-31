/*
IStreamUriParser.cs

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

namespace TheXDS.MCART.Types.Base;
using System;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que permita
/// interpretar un <see cref="Uri"/> y obtener un <see cref="Stream"/>
/// a partir del mismo.
/// </summary>
public interface IStreamUriParser
{
    /// <summary>
    /// Obtiene un valor que determina si el <see cref="Stream"/>
    /// producido por este objeto requiere ser cargado por completo en
    /// un búffer de lectura en memoria.
    /// </summary>
    bool PreferFullTransfer { get; }

    /// <summary>
    /// Obtiene un <see cref="Stream"/> que enlaza al recurso
    /// solicitado, seleccionando el método más apropiado para obtener
    /// dicho flujo de datos.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a abrir para lectura.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
    /// apuntado por el <see cref="Uri"/> especificado.
    /// </returns>
    Stream? GetStream(Uri uri);

    /// <summary>
    /// Obtiene un <see cref="Stream"/> que enlaza al recurso
    /// solicitado, seleccionando el método más apropiado para obtener
    /// dicho flujo de datos.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a abrir para lectura.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
    /// apuntado por el <see cref="Uri"/> especificado.
    /// </returns>
    Task<Stream?> GetStreamAsync(Uri uri);

    /// <summary>
    /// Determina si este <see cref="IStreamUriParser"/> puede crear un
    /// <see cref="Stream"/> a partir del <see cref="Uri"/>
    /// especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si este <see cref="IStreamUriParser"/>
    /// puede crear un <see cref="Stream"/> a partir del
    /// <see cref="Uri"/> especificado, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    bool Handles(Uri uri);

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
    Stream? Open(Uri uri);

    /// <summary>
    /// Abre el <see cref="Stream"/> desde el <see cref="Uri"/>
    /// especificado, y lo carga completamente en un nuevo
    /// <see cref="Stream"/> intermedio antes de devolverlo.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a abrir para lectura.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
    /// apuntado por el <see cref="Uri"/> especificado.
    /// </returns>
    Stream? OpenFullTransfer(Uri uri);

    /// <summary>
    /// Abre el <see cref="Stream"/> desde el <see cref="Uri"/>
    /// especificado, y lo carga completamente en un nuevo
    /// <see cref="Stream"/> intermedio de forma asíncrona antes de
    /// devolverlo.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a abrir para lectura.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
    /// apuntado por el <see cref="Uri"/> especificado.
    /// </returns>
    Task<Stream?> OpenFullTransferAsync(Uri uri);
}
