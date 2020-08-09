/*
IArgument.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Collections.Generic;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Cmd.Base
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que represente a
    /// un argumento de consola.
    /// </summary>
    public interface IArgument : INameable, IDescriptible
    {
        /// <summary>
        /// Obtiene el nombre largo de este argumento.
        /// </summary>
        string LongName => GetType().Name.ChopEndAny("Argument", "Arg");

        /// <summary>
        /// Obtiene el nombre corto de este argumento.
        /// </summary>
        char? ShortName => null;

        /// <summary>
        /// Obtiene una descripción de ayuda sobre este argumento.
        /// </summary>
        string? Summary => null;
        
        string INameable.Name => LongName;

        string IDescriptible.Description => Summary ?? LongName;
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un
    /// <see cref="IArgument"/> que acepta y expone valores. 
    /// </summary>
    public interface IValueArgument : IArgument
    {
        /// <summary>
        /// Obtiene el valor proporcionado por el usuario en la línea de
        /// comandos para este argumento.
        /// </summary>
        string? Value { get; }
        
        /// <summary>
        /// Obtiene el valor predeterminado para este argumento.
        /// </summary>
        /// <value>
        /// Un <see cref="string"/> con el valor predeterminado de este
        /// <see cref="IValueArgument"/> cuando el valor no se especifica en la
        /// línea de comandos, o <see langword="null"/> para indicar que el
        /// valor de este argumento es requerido.
        /// </value>
        /// <remarks>
        /// Al omitirse la implementación de esta propiedad en una clase que
        /// implementa directamente <see cref="IValueArgument"/>, se asumirá de
        /// forma automática que el valor del argumento debe ser especificado
        /// en la línea de comandos de la aplicación.
        /// </remarks>
        string? Default => null;
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un
    /// <see cref="IArgument"/> ejecutable.
    /// </summary>
    public interface IRunnableArgument : IArgument
    {
        /// <summary>
        /// Ejecuta una acción relacionada a este argumento cuando el mismo
        /// esté presente en la línea de comandos.
        /// </summary>
        void Run();
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un
    /// <see cref="IArgument"/> que expone información sobre argumentos
    /// requeridos.
    /// </summary>
    public interface IRequiresArgument : IArgument
    {
        /// <summary>
        /// Enumera los tipos de argumentos requeridos por este argumento.
        /// </summary>
        IEnumerable<Type> Requires { get; }
    }
    
    /// <summary>
    /// Define una serie de miembros a implementar por un
    /// <see cref="IArgument"/> que expone información sobre argumentos
    /// reemplazados.
    /// </summary>
    public interface IOverridesArgument : IArgument
    {
        /// <summary>
        /// Enumera una serie de reemplazos a incluirse en la línea de
        /// comandos.
        /// </summary>
        IEnumerable<IArgument> Overrides { get; }
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// interpretar una cadena como un argumento.
    /// </summary>
    public interface IArgumentFinder
    {
        bool? Find(string arg, );
    }
}