/*
StringUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Resources
{
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
        /// Intenta obtiener un <see cref="StreamReader"/> a partir del
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
        /// Intenta obtiener un <see cref="StreamReader"/> a partir del
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
            return GetStream(id, Objects.FindType<ICompressorGetter>(compressorId)?.New<ICompressorGetter>() ?? new NullGetter());
        }

        /// <summary>
        /// Intenta obtiener un <see cref="StreamReader"/> a partir del
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
}