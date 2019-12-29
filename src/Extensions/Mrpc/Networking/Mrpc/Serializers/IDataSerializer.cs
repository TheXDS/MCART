/*
IDataSerializer.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Mrpc.Serializers
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// serializar datos de un objeto en forma binaria dentro de un
    /// <see cref="Stream"/>.
    /// </summary>
    public interface IDataSerializer
    {
        /// <summary>
        /// Obtiene un valor que indica si el <see cref="IDataSerializer"/>
        /// puede manejar objetos del tipo especificado.
        /// </summary>
        /// <param name="type">
        /// Tipo a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el <see cref="IDataSerializer"/>
        /// puede serializar objetos del tipo especificado,
        /// <see langword="false"/> en caso contrario.
        /// </returns>
        bool Handles(Type type);

        /// <summary>
        /// Obtiene un objeto desde el <see cref="BinaryReader"/>
        /// especificado.
        /// </summary>
        /// <param name="reader">
        /// <see cref="BinaryReader"/> desde el cual obtener un objeto.
        /// </param>
        /// <returns>
        /// El objeto que se ha reconstruido con la información binaria
        /// leída desde el <see cref="BinaryReader"/> especificado.
        /// </returns>
        object Read(BinaryReader reader);

        /// <summary>
        /// Serializa un objeto en formato binario y lo escribe por medio
        /// del <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="value">
        /// Objeto a serializar.
        /// </param>
        /// <param name="writer">
        /// <see cref="BinaryWriter"/> a utilizar para escribir los datos
        /// binarios serializados.
        /// </param>
        void Write(object value, BinaryWriter writer);
    }

    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// serializar datos de un tipo específico en forma binaria dentro de
    /// un <see cref="Stream"/>.
    /// </summary>
    public interface IDataSerializer<T> : IDataSerializer
    {
        /// <summary>
        /// Obtiene un objeto de tipo <typeparamref name="T"/> desde el
        /// <see cref="BinaryReader"/> especificado.
        /// </summary>
        /// <param name="reader">
        /// <see cref="BinaryReader"/> desde el cual obtener un objeto de
        /// tipo <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// El objeto de tipo <typeparamref name="T"/> que se ha
        /// reconstruido con la información binaria leída desde el
        /// <see cref="BinaryReader"/> especificado.
        /// </returns>
        new T Read(BinaryReader reader);
        
        /// <summary>
        /// Serializa un objeto de tipo <typeparamref name="T"/> en formato
        /// binario y lo escribe por medio del <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="value">
        /// Objeto a serializar.
        /// </param>
        /// <param name="writer">
        /// <see cref="BinaryWriter"/> a utilizar para escribir los datos
        /// binarios serializados.
        /// </param>
        void Write(T value, BinaryWriter writer);
        
        bool IDataSerializer.Handles(Type t) => t.Implements<T>();
        object IDataSerializer.Read(BinaryReader reader) => Read(reader);
        void IDataSerializer.Write(object value, BinaryWriter writer) => Write((T) value, writer);
    }
}
