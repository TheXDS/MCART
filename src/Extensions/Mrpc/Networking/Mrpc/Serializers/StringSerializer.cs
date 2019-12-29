/*
DateTimeSerializer.cs

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

using System;
using System.IO;

namespace TheXDS.MCART.Networking.Mrpc.Serializers
{
    /// <summary>
    /// Serializador de datos que opera sobre objetos de tipo
    /// <see cref="string"/>.
    /// </summary>
    public sealed class StringSerializer : IDataSerializer
    {
        /// <summary>
        /// Obtiene un valor que indica si el
        /// <see cref="StringSerializer"/> puede manejar objetos del tipo
        /// especificado.
        /// </summary>
        /// <param name="type">
        /// Tipo de objeto a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si <paramref name="type"/> es igual al
        /// tipo <see cref="string"/>, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public bool Handles(Type type) => type == typeof(string);

        /// <summary>
        /// Obtiene un <see cref="string"/> desde el
        /// <see cref="BinaryReader"/> especificado.
        /// </summary>
        /// <param name="reader">
        /// <see cref="BinaryReader"/> desde el cual obtener un
        /// <see cref="string"/>.
        /// </param>
        /// <returns>
        /// El <see cref="string"/> que se ha reconstruido con la
        /// información binaria leída desde el <see cref="BinaryReader"/>
        /// especificado.
        /// </returns>
        public object Read(BinaryReader reader) => reader.ReadString();

        /// <summary>
        /// Serializa un <see cref="string"/> en formato binario y lo
        /// escribe por medio del <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="value">
        /// Valor <see cref="string"/> a serializar.
        /// </param>
        /// <param name="writer">
        /// <see cref="BinaryWriter"/> a utilizar para escribir los datos
        /// binarios serializados.
        /// </param>
        public void Write(object value, BinaryWriter writer) => writer.Write((string)value);
    }
}
