/*
AssemblyUnpacker.cs

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
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Clase base que permite definir un <see cref="IUnpacker{T}"/> que extrae
    /// recursos incrustados desde un <see cref="Assembly"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de recursos a extraer.</typeparam>
    public abstract partial class AssemblyUnpacker<T> : IUnpacker<T>
    {
        readonly string _path;
        readonly Assembly _assembly;

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="AssemblyUnpacker{T}"/>.
        /// </summary>
        /// <param name="assembly">
        /// <see cref="Assembly"/> desde donde se extraerán los recursos
        /// incrustados.
        /// </param>
        /// <param name="path">
        /// Ruta (en formato de espacio de nombre) donde se ubicarán los
        /// recursos incrustados.
        /// </param>
        protected AssemblyUnpacker(Assembly assembly, string path)
        {
            _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

            // TODO: verificar validez de path
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        /// <summary>
        /// Obtiene un <see cref="Stream"/> desde el cual leer un recurso
        /// incrustado.
        /// </summary>
        /// <param name="id">Identificador del recurso incrustado.</param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual leer un recurso incrustado.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="id"/> es una cadena vacía o
        /// <see langword="null"/>.
        /// </exception>
        protected Stream? UnpackStream(string id)
        {
            UnpackStream_Contract(id);
            var p = id.Split('.');
            if (p.Length > 1)
            {
                var c = p[^1];
                return UnpackStream(id.ChopEnd(c), c);
            }
            return _assembly.GetManifestResourceStream($"{_path}.{id}");
        }

        /// <summary>
        /// Obtiene un <see cref="Stream"/> desde el cual extraer un recurso
        /// incrustado comprimido.
        /// </summary>
        /// <param name="id">Identificador del recurso incrustado.</param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual leer un recurso incrustado
        /// sin comprimir.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="id"/> es una cadena vacía o
        /// <see langword="null"/>, o si no se ha encontrado un 
        /// <see cref="ICompressorGetter"/> con el identificador especificado o
        /// si este es una cadena vacía.
        /// </exception>
        /// 
        [Sugar] protected Stream UnpackStream(string id, string compressorId)
        {
            return UnpackStream(id, Objects.FindType<ICompressorGetter>(compressorId)?.New<ICompressorGetter>() ?? new NullGetter());
        }

        /// <summary>
        /// Obtiene un <see cref="Stream"/> desde el cual extraer un recurso
        /// incrustado comprimido.
        /// </summary>
        /// <param name="id">Identificador del recurso incrustado.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer al recurso,
        /// o <see langword="null"/> para no utilizar un extractor de 
        /// descompresión.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual leer un recurso incrustado
        /// sin comprimir.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="id"/> es una cadena vacía o
        /// <see langword="null"/>.
        /// </exception>
        /// <exception cref="MissingResourceException">
        /// Se produce si no se ha encontrado un recurso incrustado con el ID
        /// especificado en la ruta definida para este extractor de recursos.
        /// </exception>
        protected Stream UnpackStream(string id, ICompressorGetter? compressor)
        {
            var c = compressor ?? new NullGetter();
            if (id.IsEmpty()) throw new ArgumentNullException(nameof(id));
            return c.GetCompressor(_assembly?.GetManifestResourceStream($"{_path}.{id}{c.Extension}") ?? throw new MissingResourceException(id));
        }

        /// <summary>
        /// Obtiene un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <returns>Un recurso de tipo <typeparamref name="T" />.</returns>
        public abstract T Unpack(string id);

        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir de tipo <typeparamref name="T" />.
        /// </returns>
        public abstract T Unpack(string id, string compressorId);

        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter" /> a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir de tipo <typeparamref name="T" />.
        /// </returns>
        public abstract T Unpack(string id, ICompressorGetter compressor);

#if !RatherDRY
        private static bool InternalTryUnpack(Func<T> function, out T result)
        {
            try
            {
                result = function();
                return true;
            }
            catch
            {
                result = default!;
                return false;
            }
        }

        /// <summary>
        /// Intenta obtener un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="result">
        /// Parámetro de salida. Un recurso de tipo 
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el recurso se extrajo 
        /// satisfactoriamente, <see langword="false"/> en caso contrario.
        /// </returns>
        public virtual bool TryUnpack(string id, out T result)
        {
            return AssemblyUnpacker<T>.InternalTryUnpack(() => Unpack(id), out result);
        }

        /// <summary>
        /// Intenta obtener un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="result">
        /// Parámetro de salida. Un recurso de tipo 
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el recurso se extrajo 
        /// satisfactoriamente, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer el recurso.
        /// </param>
        public virtual bool TryUnpack(string id, string compressorId, out T result)
        {
            return AssemblyUnpacker<T>.InternalTryUnpack(() => Unpack(id, compressorId), out result);
        }

        /// <summary>
        /// Intenta obtener un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="result">
        /// Parámetro de salida. Un recurso de tipo 
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el recurso se extrajo 
        /// satisfactoriamente, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer el
        /// recurso.
        /// </param>        
        public virtual bool TryUnpack(string id, ICompressorGetter compressor, out T result)
        {
            return AssemblyUnpacker<T>.InternalTryUnpack(() => Unpack(id, compressor), out result);
        }
#else
        public virtual bool TryUnpack(string id, out T result)
        {
            try
            {
                result = Unpack(id);
                return true;
            }
            catch
            {
                result = default!;
                return false;
            }
        }

        public virtual bool TryUnpack(string id, string compressorId, out T result)
        {
            try
            {
                result = Unpack(id, compressorId);
                return true;
            }
            catch
            {
                result = default!;
                return false;
            }
        }

        public virtual bool TryUnpack(string id, ICompressorGetter compressor, out T result)
        {
            try
            {
                result = Unpack(id, compressor);
                return true;
            }
            catch
            {
                result = default!;
                return false;
            }
        }
#endif
    }
}