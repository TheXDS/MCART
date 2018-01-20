﻿/*
AssemblyUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Clase base que permite definir un <see cref="IUnpacker{T}"/> que extrae
    /// recursos incrustados desde un <see cref="Assembly"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de recursos a extraer.</typeparam>
    public abstract class AssemblyUnpacker<T> : IUnpacker<T>
    {
        readonly string path;
        readonly Assembly assembly;
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
            this.assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

            // TODO: verificar validez de path
            this.path = path ?? throw new ArgumentNullException(nameof(path));
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
        protected Stream UnpackStream(string id)
        {
            if (id.IsEmpty()) throw new ArgumentNullException(nameof(id));
            return assembly.GetManifestResourceStream($"{path}.{id}");
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
        [Thunk] protected Stream UnpackStream(string id, string compressorId) => UnpackStream(id, null, compressorId);
        /// <summary>
        /// Obtiene un <see cref="Stream"/> desde el cual extraer un recurso
        /// incrustado comprimido.
        /// </summary>
        /// <param name="id">Identificador del recurso incrustado.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual leer un recurso incrustado
        /// sin comprimir.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="id"/> es una cadena vacía o
        /// <see langword="null"/>, o si <paramref name="compressor"/> es
        /// <see langword="null"/>.
        /// </exception>
        [Thunk] protected Stream UnpackStream(string id, ICompressorGetter compressor) => UnpackStream(id, compressor, null);
        /// <summary>
        /// Obtiene un <see cref="Stream"/> desde el cual extraer un recurso
        /// incrustado comprimido.
        /// </summary>
        /// <param name="id">Identificador del recurso incrustado.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer al recurso.
        /// Si se establece en <see langword="null"/>, se utilizará el
        /// parámetro <paramref name="compressorId"/> para determinar el
        /// compresor a utilizar.
        /// </param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer al recurso.
        /// Si se establece en <see langword="null"/>, se utilizará el
        /// atributo <see cref="IdentifierAttribute"/> del objeto establecido
        /// en el parámetro <paramref name="compressor"/>.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual leer un recurso incrustado
        /// sin comprimir.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="id"/> es una cadena vacía o
        /// <see langword="null"/>, si <paramref name="compressor"/> y
        /// <paramref name="compressorId"/> son <see langword="null"/>,
        /// o si <paramref name="compressor"/> es <see langword="null"/> y no
        /// se ha encontrado un <see cref="ICompressorGetter"/> con el
        /// identificador especificado o si este es una cadena vacía.
        /// </exception>        
        protected Stream UnpackStream(string id, ICompressorGetter compressor, string compressorId)
        {
            if (id.IsEmpty()) throw new ArgumentNullException(nameof(id));
            ICompressorGetter c = compressor ?? Objects.FindType<ICompressorGetter>(compressorId)?.New<ICompressorGetter>() ?? throw new NotSupportedException();
            string cId = compressorId ?? Attribute.GetCustomAttributes(c.GetType()).OfType<IdentifierAttribute>().FirstOrDefault()?.Value ?? "compressed";
            return c.GetCompressor(assembly.GetManifestResourceStream($"{path}.{id}.{cId}"));
        }
        /// <summary>
        /// Obtiene un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <returns>Un recurso de tipo <typeparamref name="T"/>.</returns>
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
        /// Un recurso sin comprimir de tipo <typeparamref name="T"/>.
        /// </returns>
        public abstract T Unpack(string id, string compressorId);
        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir de tipo <typeparamref name="T"/>.
        /// </returns>
        public abstract T Unpack(string id, ICompressorGetter compressor);
    }
}