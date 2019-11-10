/*
EmbeddedLicenseAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Establece un archivo incrustado de licencia a asociar con el elemento.
    /// </summary>
    [AttributeUsage(Class | AttributeTargets.Module | AttributeTargets.Assembly)]
    [Serializable]
    public sealed class EmbeddedLicenseAttribute : LicenseAttributeBase
    {
        /// <summary>
        ///     Ruta del archivo embebido de licencia dentro del ensamblado.
        /// </summary>
        public string Path { get; }
        /// <summary>
        ///     Compressor utilizado para extraer el recurso incrustado.
        /// </summary>
        public Type CompressorType { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="EmbeddedLicenseAttribute" />.
        /// </summary>
        /// <param name="value">
        ///     Archivo incrustado de la licencia.
        /// </param>
        /// <param name="path">
        ///     Ruta del archivo embebido de licencia dentro del ensamblado.
        /// </param>
        public EmbeddedLicenseAttribute(string value, string path) : this(value, path, typeof(NullGetter))
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="EmbeddedLicenseAttribute" />.
        /// </summary>
        /// <param name="value">
        ///     Archivo incrustado de la licencia.
        /// </param>
        /// <param name="path">
        ///     Ruta del archivo embebido de licencia dentro del ensamblado.
        /// </param>
        /// <param name="compressorType">
        ///     Compressor utilizado para extraer el recurso incrustado.
        /// </param>
        public EmbeddedLicenseAttribute(string value, string path, Type compressorType) : base(value)
        {
            if (!compressorType.Implements<ICompressorGetter>()) throw new InvalidTypeException(compressorType);   
            Path = path;
            CompressorType = compressorType;
        }

        /// <summary>
        ///     Lee el contenido de la licencia embebida dentro del ensamblado.
        /// </summary>
        /// <param name="context">
        ///     Objeto a partir del cual se ha obtenido este atributo.
        /// </param>
        /// <returns>
        ///     El contenido de la licencia.
        /// </returns>
        public override License GetLicense(object context)
        {
            var origin = context switch
            {
                Assembly a => a,
                Type t => t.Assembly,
                MemberInfo m => m.DeclaringType?.Assembly!,
                null => throw new ArgumentNullException(nameof(context)),
                _ => context.GetType().Assembly
            };

            if (Value is null) return License.Unspecified;
            var content = new StringUnpacker(origin, Path).Unpack(Value, CompressorType.New<ICompressorGetter>());
            return new TextLicense(content.Split('\n')[0].Trim(' ', '\n', '\r'), content);
        }
    }
}