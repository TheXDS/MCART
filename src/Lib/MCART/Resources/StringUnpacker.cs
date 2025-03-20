/*
StringUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Reflection;
using System.Text;
using TheXDS.MCART.Exceptions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Resources;

/// <summary>
/// <see cref="AssemblyUnpacker{T}" /> que permite extraer archivos de
/// texto incrustados en un ensamblado como una cadena.
/// </summary>
/// <param name="assembly">
/// <see cref="Assembly"/> desde donde se extraerán los recursos
/// incrustados.
/// </param>
/// <param name="path">
/// Ruta (en formato de espacio de nombre) donde se ubicarán los
/// recursos incrustados.
/// </param>
public class StringUnpacker(Assembly assembly, string path) : AssemblyUnpacker<string>(assembly, path), IAsyncUnpacker<string>
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
        return new(UnpackStream(id) ?? throw new MissingResourceException());
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

    /// <inheritdoc/>
    public override string Unpack(string id) => Read(GetStream(id));

    /// <inheritdoc/>
    public override string Unpack(string id, ICompressorGetter compressor) => Read(GetStream(id, compressor));

    /// <inheritdoc/>
    public override bool TryUnpack(string id, out string data)
    {
        bool r = TryGetStream(id, out StreamReader? reader);
        data = Read(reader);
        return r;
    }

    /// <inheritdoc/>
    public override bool TryUnpack(string id, ICompressorGetter compressor, out string data)
    {
        bool r = TryGetStream(id, compressor, out StreamReader? reader);
        data = Read(reader);
        return r;
    }

    /// <inheritdoc/>
    public Task<string> UnpackAsync(string id) => ReadAsync(new StreamReader(UnpackStream(id) ?? throw new InvalidDataException()));

    /// <inheritdoc/>
    public Task<string> UnpackAsync(string id, ICompressorGetter compressor) => ReadAsync(GetStream(id, compressor));
    
    /// <inheritdoc/>
    public async Task<UnpackResult<string>> TryUnpackAsync(string id)
    {
        return TryGetStream(id, out StreamReader? reader) ? new(true, await ReadAsync(reader)) : new(false, null);
    }

    /// <inheritdoc/>
    public async Task<UnpackResult<string>> TryUnpackAsync(string id, ICompressorGetter compressor)
    {
        return TryGetStream(id, compressor, out StreamReader? reader) ? new(true, await ReadAsync(reader)) : new(false, null);
    }
}
