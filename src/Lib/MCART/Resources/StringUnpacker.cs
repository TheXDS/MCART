/*
StringUnpacker.cs

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
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Resources;

/// <summary>
/// <see cref="AssemblyUnpacker{T}" /> que permite extraer archivos de
/// texto incrustados en un ensamblado como una cadena.
/// </summary>
public class StringUnpacker : AssemblyUnpacker<string>, IAsyncUnpacker<string>
{
    private static Task<string> ReadAsync(TextReader r)
    {
        using (r) return r.ReadToEndAsync();
    }

    private static string Read(TextReader r)
    {
        using (r) return r.ReadToEnd();
    }

    private static StreamReader GetFailure(string id, Exception ex)
    {
        MemoryStream ms = new();
        using StreamWriter sw = new(ms, Encoding.UTF8, -1, true);
        sw.WriteLine(string.Format(St.Errors.ErrorLoadingRes, id));
        St.Composition.ExDump(sw, ex, St.ExDumpOptions.All);
        ms.Seek(0, SeekOrigin.Begin);
        return new StreamReader(ms, Encoding.UTF8);
    }

    /// <summary>
    /// Obtiene un <see cref="StreamReader"/> a partir del
    /// <paramref name="id"/> y del <paramref name="compressor"/>
    /// especificados.
    /// </summary>
    /// <param name="id">
    /// Id del <see cref="Stream"/> del recurso incrustado a obtener.
    /// </param>
    /// <param name="compressor">
    /// Compresor específico a utilizar para leer el recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="StreamReader"/> que permite leer la secuencia 
    /// subyacente del recurso incrustado.
    /// </returns>
    protected StreamReader GetStream(string id, ICompressorGetter compressor)
    {
        return new(UnpackStream(id, compressor));
    }

    /// <summary>
    /// Obtiene un <see cref="StreamReader"/> a partir del
    /// <paramref name="id"/> especificado.
    /// </summary>
    /// <param name="id">
    /// Id del <see cref="Stream"/> del recurso incrustado a obtener.
    /// </param>
    /// <returns>
    /// Un <see cref="StreamReader"/> que permite leer la secuencia 
    /// subyacente del recurso incrustado.
    /// </returns>
    protected StreamReader GetStream(string id)
    {
        return new(UnpackStream(id) ?? throw new InvalidDataException());
    }

    /// <summary>
    /// Intenta obtener un <see cref="StreamReader"/> a partir del
    /// <paramref name="id"/> y del <paramref name="compressor"/>
    /// especificados.
    /// </summary>
    /// <param name="id">
    /// Id del <see cref="Stream"/> del recurso incrustado a obtener.
    /// </param>
    /// <param name="compressor">
    /// Compresor específico a utilizar para leer el recurso.
    /// </param>
    /// <param name="reader">
    /// Parámetro de salida. Un <see cref="StreamReader"/> que permite
    /// leer la secuencia subyacente del recurso incrustado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si se ha creado un
    /// <see cref="StreamReader"/> de forma satisfactoria para el
    /// recurso, <see langword="false"/> en caso contrario.
    /// </returns>
    protected bool TryGetStream(string id, ICompressorGetter compressor, out StreamReader reader)
    {
        try
        {
            reader = GetStream(id, compressor);
            return true;
        }
        catch (Exception ex)
        {
            reader = GetFailure(id, ex);
            return false;
        }
    }

    /// <summary>
    /// Intenta obtener un <see cref="StreamReader"/> a partir del
    /// <paramref name="id"/> especificado.
    /// </summary>
    /// <param name="id">
    /// Id del <see cref="Stream"/> del recurso incrustado a obtener.
    /// </param>
    /// <param name="reader">
    /// Parámetro de salida. Un <see cref="StreamReader"/> que permite
    /// leer la secuencia subyacente del recurso incrustado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si se ha creado un
    /// <see cref="StreamReader"/> de forma satisfactoria para el
    /// recurso, <see langword="false"/> en caso contrario.
    /// </returns>
    protected bool TryGetStream(string id, out StreamReader reader)
    {
        try
        {
            reader = GetStream(id);
            return true;
        }
        catch (Exception ex)
        {
            reader = GetFailure(id, ex);
            return false;
        }
    }

    /// <summary>
    /// Obtiene un <see cref="StreamReader"/> a partir del
    /// <paramref name="id"/> y del <paramref name="compressorId"/>
    /// especificados.
    /// </summary>
    /// <param name="id">
    /// Id del <see cref="Stream"/> del recurso incrustado a obtener.
    /// </param>
    /// <param name="compressorId">
    /// Id del compresor específico a utilizar para leer el recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="StreamReader"/> que permite leer la secuencia 
    /// subyacente del recurso incrustado.
    /// </returns>
    protected StreamReader GetStream(string id, string compressorId)
    {
        return GetStream(id, ReflectionHelpers.FindType<ICompressorGetter>(compressorId)?.New<ICompressorGetter>() ?? new NullGetter());
    }

    /// <summary>
    /// Intenta obtener un <see cref="StreamReader"/> a partir del
    /// <paramref name="id"/> y del <paramref name="compressorId"/>
    /// especificados.
    /// </summary>
    /// <param name="id">
    /// Id del <see cref="Stream"/> del recurso incrustado a obtener.
    /// </param>
    /// <param name="compressorId">
    /// Id del compresor específico a utilizar para leer el recurso.
    /// </param>
    /// <param name="reader">
    /// Parámetro de salida. Un <see cref="StreamReader"/> que permite
    /// leer la secuencia subyacente del recurso incrustado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si se ha creado un
    /// <see cref="StreamReader"/> de forma satisfactoria para el
    /// recurso, <see langword="false"/> en caso contrario.
    /// </returns>
    protected bool TryGetStream(string id, string compressorId, out StreamReader reader)
    {
        try
        {
            reader = GetStream(id, compressorId);
            return true;
        }
        catch (Exception ex)
        {
            reader = GetFailure(id, ex);
            return false;
        }
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="StringUnpacker"/>.
    /// </summary>
    /// <param name="assembly">
    /// <see cref="Assembly"/> desde donde se extraerán los recursos
    /// incrustados.
    /// </param>
    /// <param name="path">
    /// Ruta (en formato de espacio de nombre) donde se ubicarán los
    /// recursos incrustados.
    /// </param>
    public StringUnpacker(Assembly assembly, string path) : base(assembly, path) { }

    /// <summary>
    /// Obtiene un recurso identificable.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public override string Unpack(string id) => Read(GetStream(id));

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressorId">
    /// Identificador del compresor a utilizar para extraer el recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public override string Unpack(string id, string compressorId) => Read(GetStream(id, compressorId));

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter" /> a utilizar para extraer el
    /// recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public override string Unpack(string id, ICompressorGetter compressor) => Read(GetStream(id, compressor));

    /// <summary>
    /// Intenta obtener un recurso identificable.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="data">
    /// Parámetro de salida. Cadena que ha sido devuelta por la
    /// operación de lectura del recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public override bool TryUnpack(string id, out string data)
    {
        bool r = TryGetStream(id, out StreamReader? reader);
        data = Read(reader);
        return r;
    }

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressorId">
    /// Identificador del compresor a utilizar para extraer el recurso.
    /// </param>
    /// <param name="data">
    /// Parámetro de salida. Cadena que ha sido devuelta por la
    /// operación de lectura del recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public override bool TryUnpack(string id, string compressorId, out string data)
    {
        bool r = TryGetStream(id, compressorId, out StreamReader? reader);
        data = Read(reader);
        return r;
    }

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter" /> a utilizar para extraer el
    /// recurso.
    /// </param>
    /// <param name="data">
    /// Parámetro de salida. Cadena que ha sido devuelta por la
    /// operación de lectura del recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public override bool TryUnpack(string id, ICompressorGetter compressor, out string data)
    {
        bool r = TryGetStream(id, compressor, out StreamReader? reader);
        data = Read(reader);
        return r;
    }

    /// <summary>
    /// Obtiene un recurso identificable de forma asíncrona.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public Task<string> UnpackAsync(string id) => ReadAsync(new StreamReader(UnpackStream(id) ?? throw new InvalidDataException()));

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado de forma asíncrona.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressorId">
    /// Identificador del compresor a utilizar para extraer el recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public Task<string> UnpackAsync(string id, string compressorId) => ReadAsync(GetStream(id, compressorId));

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado de forma asíncrona.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter" /> a utilizar para extraer el
    /// recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="string" /> con el contenido del archivo de texto
    /// incrustado en el ensamblado.
    /// </returns>
    public Task<string> UnpackAsync(string id, ICompressorGetter compressor) => ReadAsync(GetStream(id, compressor));
}
