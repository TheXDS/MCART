/*
DataSerializer.cs

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
using TheXDS.MCART.Types.Extensions;
using System.IO;

namespace TheXDS.MCART.Networking.Mrpc.Serializers
{
    /// <summary>
    ///     Clase base que describe un serializador de tipo específico.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de la estructura a serializar.
    /// </typeparam>
    public abstract class DataSerializer<T> : IDataSerializer where T : struct
    {
        bool IDataSerializer.Handles(Type type) => type.Implements<T>();

        object IDataSerializer.Read(BinaryReader reader)
        {
            return Read(reader);
        }

        /// <summary>
        ///     Obtiene un objeto de tipo <typeparamref name="T"/> desde el
        ///     <see cref="BinaryReader"/> especificado.
        /// </summary>
        /// <param name="reader">
        ///     <see cref="BinaryReader"/> desde el cual obtener un objeto.
        /// </param>
        /// <returns>
        ///     El objeto de tipo <typeparamref name="T"/> que se ha
        ///     reconstruido con la información binaria leída desde el
        ///     <see cref="BinaryReader"/> especificado.
        /// </returns>
        protected abstract T Read(BinaryReader reader);

        void IDataSerializer.Write(object value, BinaryWriter writer)
        {
            Write((T)value, writer);
        }

        /// <summary>
        ///     Serializa un objeto de tipo <typeparamref name="T"/> en formato
        ///     binario y lo escribe por medio del <see cref="BinaryWriter"/>
        ///     especificado.
        /// </summary>
        /// <param name="value">
        ///     Objeto de tipo <typeparamref name="T"/> a serializar.
        /// </param>
        /// <param name="writer">
        ///     <see cref="BinaryWriter"/> a utilizar para escribir los datos
        ///     binarios serializados.
        /// </param>
        protected abstract void Write(T value, BinaryWriter writer);
    }
}
