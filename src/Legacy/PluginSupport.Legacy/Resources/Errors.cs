/*
Errors.cs

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
using System.Reflection;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.PluginSupport.Legacy.Resources
{
    /// <summary>
    /// Contiene recursos que generan nuevas instancias de excepción a ser lanzadas.
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Crea una nueva instancia de un <see cref="PluginException"/> que
        /// indica que el método especificado no tiene una firma válida en este
        /// contexto.
        /// </summary>
        /// <param name="plugin">
        /// Instancia de <see cref="IPlugin"/> donde se ha producido la
        /// excepción.
        /// </param>
        /// <param name="method">
        /// Método con firma inválida que ha causado la excepción.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="PluginException"/>.
        /// </returns>
        /// <remarks>
        /// La excepción interna determina el tipo de excepción específica
        /// ocurrida dentro del <see cref="IPlugin"/>. En este caso, la
        /// excepción interna a retornar será de tipo
        /// <see cref="InvalidMethodSignatureException"/>.
        /// </remarks>
        public static Exception InvalidMethodSignature(IPlugin plugin, MethodInfo method)
        {
            return new PluginException(new InvalidMethodSignatureException(method), plugin);
        }
    }
}
