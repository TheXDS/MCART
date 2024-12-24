/*
StreamUriParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Clase base para un objeto que permite crear un <see cref="Stream"/>
/// a partir de una <see cref="Uri"/>.
/// </summary>
public abstract class StreamUriParser : IStreamUriParser
{
    /// <summary>
    /// Obtiene el <see cref="StreamUriParser"/> apropiado para manejar
    /// al <see cref="Uri"/> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a partir del cual crear un
    /// <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="StreamUriParser"/> que puede crear un
    /// <see cref="Stream"/> a partir del <see cref="Uri"/>
    /// especificado, o <see langword="null"/> si no existe un
    /// <see cref="StreamUriParser"/> capaz de manejar el 
    /// <see cref="Uri"/>.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static StreamUriParser? Infer(Uri uri)
    {
        return Infer<StreamUriParser>(uri);
    }

    /// <summary>
    /// Obtiene el <see cref="StreamUriParser"/> apropiado para manejar
    /// al <see cref="Uri"/> especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a partir del cual crear un
    /// <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="StreamUriParser"/> que puede crear un
    /// <see cref="Stream"/> a partir del <see cref="Uri"/>
    /// especificado, o <see langword="null"/> si no existe un
    /// <see cref="StreamUriParser"/> capaz de manejar el 
    /// <see cref="Uri"/>.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static StreamUriParser? Infer(string uri)
    {
        return Infer(new Uri(uri));
    }

    /// <summary>
    /// Abre directamente un <see cref="Stream"/> desde el cual leer el
    /// contenido del <paramref name="uri"/>.
    /// </summary>
    /// <param name="uri">Identificador del recurso a localizar.</param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual leer el contenido del
    /// <paramref name="uri"/>.
    /// </returns>
    [Sugar]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static Stream? Get(Uri uri)
    {
        return Infer(uri)?.GetStream(uri);
    }

    /// <summary>
    /// Abre directamente de forma asíncrona un <see cref="Stream"/>
    /// desde el cual leer el contenido del <paramref name="uri"/>.
    /// </summary>
    /// <param name="uri">Identificador del recurso a localizar.</param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual leer el contenido del
    /// <paramref name="uri"/>.
    /// </returns>
    [Sugar]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static Task<Stream?> GetAsync(Uri uri)
    {
        return Infer(uri)?.GetStreamAsync(uri) ?? Task.FromResult<Stream?>(null);
    }

    /// <summary>
    /// Obtiene el <see cref="StreamUriParser"/> apropiado para manejar
    /// al <see cref="Uri"/> especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de <see cref="StreamUriParser"/> especializado a obtener.
    /// </typeparam>
    /// <param name="uri">
    /// <see cref="Uri"/> a partir del cual crear un
    /// <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="StreamUriParser"/> que puede crear un
    /// <see cref="Stream"/> a partir del <see cref="Uri"/>
    /// especificado, o <see langword="null"/> si no existe un
    /// <see cref="StreamUriParser"/> capaz de manejar el 
    /// <see cref="Uri"/>.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static T? Infer<T>(Uri uri) where T : class, IStreamUriParser
    {
        return ReflectionHelpers.FindAllObjects<T>().FirstOrDefault(p => p.Handles(uri));
    }

    /// <summary>
    /// Obtiene el <see cref="StreamUriParser"/> apropiado para manejar
    /// al <see cref="Uri"/> especificado.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de <see cref="StreamUriParser"/> especializado a obtener.
    /// </typeparam>
    /// <param name="uri">
    /// <see cref="Uri"/> a partir del cual crear un
    /// <see cref="Stream"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="StreamUriParser"/> que puede crear un
    /// <see cref="Stream"/> a partir del <see cref="Uri"/>
    /// especificado, o <see langword="null"/> si no existe un
    /// <see cref="StreamUriParser"/> capaz de manejar el 
    /// <see cref="Uri"/>.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static T? Infer<T>(string uri) where T : class, IStreamUriParser
    {
        return Infer<T>(new Uri(uri));
    }

    /// <summary>
    /// Obtiene un valor que determina si el <see cref="Stream"/>
    /// producido por este objeto requiere ser cargado por completo en
    /// un búfer de lectura en memoria.
    /// </summary>
    public virtual bool PreferFullTransfer { get; } = false;

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
    public Stream? GetStream(Uri uri)
    {
        return PreferFullTransfer ? OpenFullTransfer(uri) : Open(uri);
    }

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
    public async Task<Stream?> GetStreamAsync(Uri uri)
    {
        return PreferFullTransfer ? (await OpenFullTransferAsync(uri)) : Open(uri);
    }

    /// <summary>
    /// Determina si este <see cref="StreamUriParser"/> puede crear un
    /// <see cref="Stream"/> a partir del <see cref="Uri"/>
    /// especificado.
    /// </summary>
    /// <param name="uri">
    /// <see cref="Uri"/> a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si este <see cref="StreamUriParser"/>
    /// puede crear un <see cref="Stream"/> a partir del
    /// <see cref="Uri"/> especificado, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public abstract bool Handles(Uri uri);

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
    public abstract Stream? Open(Uri uri);

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
    public virtual Stream? OpenFullTransfer(Uri uri)
    {
        Stream? j = Open(uri);
        if (j is null) return null;
        MemoryStream? ms = new();
        using (j)
        {
            j.CopyTo(ms);
            return ms;
        }
    }

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
    public virtual async Task<Stream?> OpenFullTransferAsync(Uri uri)
    {
        Stream? j = Open(uri);
        if (j is null) return null;
        MemoryStream? ms = new();
        await using (j)
        {
            await j.CopyToAsync(ms);
            return ms;
        }
    }
}
