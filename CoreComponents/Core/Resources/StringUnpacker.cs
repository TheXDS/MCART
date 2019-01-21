/*
StringUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Threading.Tasks;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings;
using St2 = TheXDS.MCART.Resources.InternalStrings;

namespace TheXDS.MCART.Resources
{
    /// <inheritdoc cref="AssemblyUnpacker{T}"/>
    /// <summary>
    /// <see cref="T:TheXDS.MCART.Resources.AssemblyUnpacker`1" /> que permite extraer archivos de texto
    /// incrustados en un ensamblado como una cadena.
    /// </summary>
    public class StringUnpacker : AssemblyUnpacker<string>, IAsyncUnpacker<string>
    {
        private StreamReader GetStream(string id, ICompressorGetter compressor)
        {

            try { return new StreamReader(UnpackStream(id, compressor)); }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex) { return new StreamReader(St.Warn(St2.UnkErrLoadingRes(id, ex.Message)).ToStream()); }
#endif
        }

        private StreamReader GetStream(string id, string compressorId)
        {
            return GetStream(id,
                Objects.FindType<ICompressorGetter>(compressorId)?.New<ICompressorGetter>() ?? new NullGetter());
        }

        private static string Read(TextReader r) { using (r) return r.ReadToEnd(); }
        private static Task<string> ReadAsync(TextReader r) { using (r) return r.ReadToEndAsync(); }
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
        /// <inheritdoc />
        /// <summary>
        /// Obtiene un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <returns>
        /// Un <see cref="T:System.String" /> con el contenido del archivo de texto
        /// incrustado en el ensamblado.
        /// </returns>
        public override string Unpack(string id) => Read(new StreamReader(UnpackStream(id)));
        /// <inheritdoc />
        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="T:System.String" /> con el contenido del archivo de texto
        /// incrustado en el ensamblado.
        /// </returns>
        public override string Unpack(string id, string compressorId) => Read(GetStream(id, compressorId));
        /// <inheritdoc />
        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressor">
        /// <see cref="T:TheXDS.MCART.Resources.ICompressorGetter" /> a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="T:System.String" /> con el contenido del archivo de texto
        /// incrustado en el ensamblado.
        /// </returns>
        public override string Unpack(string id, ICompressorGetter compressor) => Read(GetStream(id, compressor));
        /// <inheritdoc />
        /// <summary>
        /// Obtiene un recurso identificable de forma asíncrona.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <returns>
        /// Un <see cref="T:System.String" /> con el contenido del archivo de texto
        /// incrustado en el ensamblado.
        /// </returns>
        public async Task<string> UnpackAsync(string id) => await ReadAsync(new StreamReader(UnpackStream(id)));
        /// <inheritdoc />
        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado de forma asíncrona.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="T:System.String" /> con el contenido del archivo de texto
        /// incrustado en el ensamblado.
        /// </returns>
        public async Task<string> UnpackAsync(string id, string compressorId) => await ReadAsync(GetStream(id, compressorId));
        /// <inheritdoc />
        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado de forma asíncrona.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressor">
        /// <see cref="T:TheXDS.MCART.Resources.ICompressorGetter" /> a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="T:System.String" /> con el contenido del archivo de texto
        /// incrustado en el ensamblado.
        /// </returns>
        public async Task<string> UnpackAsync(string id, ICompressorGetter compressor) => await ReadAsync(GetStream(id, compressor));
    }
}