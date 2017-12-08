//
//  IEasyChecksum.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Threading.Tasks;
using System.IO;

namespace MCART.Security.Checksum
{
    /// <summary>
    /// Describe los métodos básicos a implementar por un mecanismo de cálculo
    /// de checksum.
    /// </summary>
    public interface IEasyChecksum
    {
        /// <summary>
        /// Ejecuta la transformación del algoritmo y devuelve el Checksum/Hash
        /// de los datos como una colección de <see cref="byte"/>.
        /// </summary>
        /// <param name="X">Cadena de entrada.</param>
        /// <returns>
        /// Una colección de <see cref="byte"/> correspondiente al resultado de
        /// ejecutar el algoritmo con la información provista.
        /// </returns>
        byte[] Compute(string X);
        /// <summary>
        /// Ejecuta la transformación del algoritmo de forma asíncrona y
        /// devuelve el Checksum/Hash de los datos como una colección de
        /// <see cref="byte"/>.
        /// </summary>
        /// <param name="X">Cadena de entrada.</param>
        /// <returns>Un <see cref="Task{TResult}"/> correspondiente al resultado
        /// de ejecutar el algoritmo con la información provista.</returns>
        Task<byte[]> ComputeAsync(string X);
        /// <summary>
        /// Ejecuta la transformación del algoritmo y devuelve el Checksum/Hash
        /// de los datos como una colección de <see cref="byte"/>.
        /// </summary>
        /// <param name="X"><see cref="TextReader"/> de entrada.</param>
        /// <returns>
        /// Una colección de <see cref="byte"/> correspondiente al resultado de
        /// ejecutar el algoritmo con la información provista.
        /// </returns>
        byte[] Compute(TextReader X);
        /// <summary>
        /// Ejecuta la transformación del algoritmo de forma asíncrona y
        /// devuelve el Checksum/Hash de los datos como una colección de
        /// <see cref="byte"/>.
        /// </summary>
        /// <param name="X"><see cref="TextReader"/> de entrada.</param>
        /// <returns>
        /// Un <see cref="Task{T}"/> correspondiente al resultado de
        /// ejecutar el algoritmo con la información provista.
        /// </returns>
        Task<byte[]> ComputeAsync(TextReader X);
        /// <summary>
        /// Ejecuta la transformación del algoritmo y devuelve el Checksum/Hash
        /// de los datos como una colección de <see cref="byte"/>.
        /// </summary>
        /// <param name="X">Arreglo de <see cref="byte"/> de entrada.</param>
        /// <returns>
        /// Una colección de <see cref="byte"/> correspondiente al resultado de
        /// ejecutar el algoritmo con la información provista.
        /// </returns>
        byte[] Compute(byte[] X);
        /// <summary>
        /// Ejecuta la transformación del algoritmo de forma asíncrona y
        /// devuelve el Checksum / Hash de los datos como una colección de
        /// <see cref="byte"/>.
        /// </summary>
        /// <param name="X">Arreglo de <see cref="byte"/> de entrada.</param>
        /// <returns>
        /// Un <see cref="Task{T}"/> correspondiente al resultado de
        /// ejecutar el algoritmo con la información provista.
        /// </returns>
        Task<byte[]> ComputeAsync(byte[] X);
        /// <summary>
        /// Ejecuta la transformación del algoritmo y devuelve el Checksum/Hash
        /// de los datos como una colección de <see cref="byte"/>.
        /// </summary>
        /// <param name="X"><see cref="Stream"/> de entrada.</param>
        /// <returns>
        /// Un <see cref="Task{T}"/> correspondiente al resultado de
        /// ejecutar el algoritmo con la información provista.
        /// </returns>
        byte[] Compute(Stream X);
        /// <summary>
        /// Ejecuta la transformación del algoritmo de forma asíncrona y
        /// devuelve el Checksum / Hash de los datos como una colección de
        /// <see cref="byte"/>.
        /// </summary>
        /// <param name="X"><see cref="Stream"/> de entrada.</param>
        /// <returns>
        /// Un <see cref="Task{T}"/> correspondiente al resultado de
        /// ejecutar el algoritmo con la información provista.
        /// </returns>
        Task<byte[]> ComputeAsync(Stream X);
    }
}