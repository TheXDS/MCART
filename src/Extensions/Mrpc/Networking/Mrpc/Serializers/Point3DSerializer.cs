/*
Point3DSerializer.cs

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

using System.IO;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Networking.Mrpc.Serializers
{
    /// <summary>
    /// Serializador de datos que opera sobre objetos de tipo
    /// <see cref="Point"/>.
    /// </summary>
    public sealed class Point3DSerializer : DataSerializer<Point3D>
    {
        /// <summary>
        /// Obtiene un <see cref="Point3D"/> desde el
        /// <see cref="BinaryReader"/> especificado.
        /// </summary>
        /// <param name="reader">
        /// <see cref="BinaryReader"/> desde el cual obtener un
        /// <see cref="Point3D"/>.
        /// </param>
        /// <returns>
        /// El <see cref="Point3D"/> que se ha reconstruido con la
        /// información binaria leída desde el <see cref="BinaryReader"/>
        /// especificado.
        /// </returns>
        protected override Point3D Read(BinaryReader reader)
        {
            return new Point3D(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }

        /// <summary>
        /// Serializa un <see cref="Point3D"/> en formato binario y lo
        /// escribe por medio del <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="value">
        /// Valor <see cref="Point3D"/> a serializar.
        /// </param>
        /// <param name="writer">
        /// <see cref="BinaryWriter"/> a utilizar para escribir los datos
        /// binarios serializados.
        /// </param>
        protected override void Write(Point3D value, BinaryWriter writer)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
            writer.Write(value.Z);
        }
    }

}
