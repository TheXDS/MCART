/*
Argument.cs

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

using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Component
{
    /// <summary>
    /// Clase base que permite describir argumentos aceptados por la aplicación.
    /// </summary>
    public abstract class Argument : INameable, IDescriptible
    {
        /// <summary>
        /// Indica el tipo de argumento.
        /// </summary>
        public enum ValueKind : byte
        {
            /// <summary>
            /// El argumento es una bandera.
            /// </summary>
            Flag,
            /// <summary>
            /// Cuando se especifica el argumento, el valor es opcional.
            /// </summary>
            Optional,
            /// <summary>
            /// Cuando se especifica el argumento, el valor es requerido.
            /// </summary>
            ValueRequired,
            /// <summary>
            /// El argumento y el valor son requeridos.
            /// </summary>
            Required
        }

        /// <summary>
        /// Obtiene el tipo de este argumento.
        /// </summary>
        public virtual ValueKind Kind => ValueKind.Flag;

        /// <summary>
        /// Obtiene el nombre largo de este argumento.
        /// </summary>
        public virtual string LongName => GetType().Name.ChopEndAny(nameof(Argument), "Arg");

        /// <summary>
        /// Obtiene el nombre corto de este argumento.
        /// </summary>
        public virtual char? ShortName { get; }

        /// <summary>
        /// Obtiene una descripción de ayuda sobre este argumento.
        /// </summary>
        public virtual string? Summary { get; }

        /// <summary>
        /// Para un argumento de tipo <see cref="ValueKind.Optional"/>,
        /// obtiene el valor predeterminado de este argumento.
        /// </summary>
        public virtual string? Default { get; }

        /// <summary>
        /// Ejecuta una acción relacionada a este argumento cuando el mismo
        /// esté presente en la línea de comandos.
        /// </summary>
        public virtual void Run(CmdLineParser args) { }

        /// <summary>
        /// Obtiene el valor asociado a este argumento que ha sido
        /// especificado en la línea de comandos.
        /// </summary>
        public string? Value { get; internal set; }

        string INameable.Name => LongName;

        string IDescriptible.Description => Summary ?? LongName;
    }
}
