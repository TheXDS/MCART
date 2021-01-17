/*
McartIconLibrary.cs

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

using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Clase base interna para exponer íconos incrustados en los 
    /// ensamblados de MCART.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de ícono a obtener.
    /// </typeparam>
    public abstract class McartIconLibrary<T>
    {
        private static readonly McartIconLibrary<T> _staticInstance = Objects.FindFirstObject<McartIconLibrary<T>>() ?? throw new MissingTypeException(typeof(McartIconLibrary<T>));

        /// <summary>
        /// Inicializa la clase <see cref="McartIconLibrary{T}"/>
        /// </summary>
        static McartIconLibrary()
        {
            if (!_staticInstance.GetType().Assembly.HasAttr<McartComponentAttribute>()) throw new TamperException();
        }

        /// <summary>
        /// Implementa el método de obtención del ícono basado en el nombre
        /// del ícono solicitado.
        /// </summary>
        /// <param name="id">
        /// Id del ícono solicitado.
        /// </param>
        /// <returns>
        /// El ícono solicitado.
        /// </returns>
        protected abstract T GetIcon([CallerMemberName] string? id = null!);

        /// <summary>
        /// Ícono principal de MCART.
        /// </summary>
        public static T MCART => _staticInstance.GetIcon();

        /// <summary>
        /// Ícono de plugin de MCART.
        /// </summary>
        public static T Plugin => _staticInstance.GetIcon();

        /// <summary>
        /// Ícono de archivo incorrecto.
        /// </summary>
        public static T BadFile => _staticInstance.GetIcon();

        /// <summary>
        /// Ícono de archivo no encontrado.
        /// </summary>
        public static T FileMissing => _staticInstance.GetIcon();

        /// <summary>
        /// Ícono de problema con archivo.
        /// </summary>
        public static T FileWarning => _staticInstance.GetIcon();
    }
}