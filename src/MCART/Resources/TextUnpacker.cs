/*
TextUnpacker.cs

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

using System.IO;
using System.Reflection;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// <see cref="AssemblyUnpacker{T}" /> que
    /// extrae recursos de texto.
    /// </summary>
    public class TextUnpacker : AssemblyUnpacker<string>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="AssemblyUnpacker{T}" />.
        /// </summary>
        /// <param name="assembly">
        /// <see cref="Assembly" /> desde donde se extraerán los recursos
        /// incrustados.
        /// </param>
        /// <param name="path">
        /// Ruta (en formato de espacio de nombre) donde se ubicarán los
        /// recursos incrustados.
        /// </param>
        public TextUnpacker(Assembly assembly, string path) : base(assembly, path)
        {
        }

        /// <summary>
        /// Obtiene un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <returns>
        /// Una cadena con el contenido total del recurso.
        /// </returns>
        public override string Unpack(string id)
        {
            using StreamReader? sr = new(UnpackStream(id) ?? throw new MissingResourceException(id));
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir como una cadena.
        /// </returns>
        public override string Unpack(string id, string compressorId)
        {
            using StreamReader? sr = new(UnpackStream(id, compressorId));
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter" /> a utilizar para extraer al
        /// recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir como una cadena.
        /// </returns>
        public override string Unpack(string id, ICompressorGetter compressor)
        {
            using StreamReader? sr = new(UnpackStream(id, compressor));
            return sr.ReadToEnd();
        }
    }
}