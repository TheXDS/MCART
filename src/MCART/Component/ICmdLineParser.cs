/*
CmdLineParser.cs

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

using System.Collections.Generic;

namespace TheXDS.MCART.Component
{
    /// <summary>
    ///     Define una serie de miembros a implementar por un tipo que permita
    ///     exponer la línea de comandos de la aplicación como abstracciones de
    ///     objetos.
    /// </summary>
    public interface ICmdLineParser
    {
        /// <summary>
        ///     Obtiene una colección de comandos o archivos incluidos en la
        ///     línea de comandos.
        /// </summary>
        IEnumerable<string> Commands { get; }

        /// <summary>
        ///     Obtiene una colección con todos los argumentos disponibles para
        ///     utilizar en la línea de comandos.
        /// </summary>
        IEnumerable<Argument> AvailableArguments { get; }

        /// <summary>
        ///     Obtiene una colección con todos los argumentos especificados en
        ///     la línea de comandos.
        /// </summary>
        IEnumerable<Argument> Present { get; }

        /// <summary>
        ///     Obtiene una colección con todos los argumentos requeridos que
        ///     no han sido incluidos en la línea de comandos.
        /// </summary>
        IEnumerable<Argument> Missing { get; }

        /// <summary>
        ///     Obtiene una colección de argumentos que no tienen una
        ///     definición establecida, o que se encuentran malformados.
        /// </summary>
        IEnumerable<string> Invalid { get; }

        /// <summary>
        ///     Obtiene una referencia al argumento especificado en la línea de
        ///     comandos.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de argumento a obtener.
        /// </typeparam>
        /// <returns>
        ///     El argumento especificado en la ínea de comandos, o 
        ///     <see langword="null"/> si el mismo no ha sido especificado.
        /// </returns>
        T? Arg<T>() where T : Argument, new();

        /// <summary>
        ///     Obtiene un valor que indica si el argumento del tipo 
        ///     especificado se encuentra presente en la línea de comandos.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de argumento a comprobar.
        /// </typeparam>
        /// <returns>
        ///     <see langword="true"/> si el argumento se encuentra presente en
        ///     la línea de comandos, <see langword="false"/> en caso
        ///     contrario.
        /// </returns>
        bool IsPresent<T>() where T : Argument, new();

        /// <summary>
        ///     Obtiene el valor de un argumento especificado en la línea de
        ///     comandos.
        /// </summary>
        /// <typeparam name="T">
        ///     Tipo de argumento del cual obtener el valor.
        /// </typeparam>
        /// <returns>
        ///     El valor establecido del argumento, o <see langword="null"/> si
        ///     el mismo no ha sido especificado.
        /// </returns>
        string? Value<T>() where T : Argument, new();
    }
}
