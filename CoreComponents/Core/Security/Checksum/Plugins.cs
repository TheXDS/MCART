//
//  Plugins.cs
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

using MCART.PluginSupport;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace MCART.Security.Checksum
{
    /// <summary>
    /// Define un <see cref="Plugin"/> que implementa la interfaz
    /// <see cref="IEasyChecksum"/>.
    /// </summary>
    public abstract class ChecksumPlugin : Plugin, IEasyChecksum
    {
        /// <summary>
        /// Calcula una suma de verificación sobre un arreglo de bytes.
        /// </summary>
        /// <param name="X">Arreglo de bytes a computar.</param>
        /// <returns>Un arreglo de bytes con la suma de verificación.</returns>
        public abstract byte[] Compute(byte[] X);
        /// <summary>
        /// Calcula una suma de verificación sobre un <see cref="Stream"/>.
        /// </summary>
        /// <param name="X">
        /// <see cref="Stream"/> con la información a computar.
        /// </param>
        /// <returns>Un arreglo de bytes con la suma de verificación.</returns>
        public virtual byte[] Compute(Stream X) => Compute((new StreamReader(X)).ReadToEnd());
        /// <summary>
        /// Calcula una suma de verificación sobre un <see cref="TextReader"/>.
        /// </summary>
        /// <param name="X">
        /// <see cref="TextReader"/> con la información a computar.
        /// </param>
        /// <returns>Un arreglo de bytes con la suma de verificación.</returns>
        public virtual byte[] Compute(TextReader X) => Compute(X.ReadToEnd());
        /// <summary>
        /// Calcula una suma de verificación sobre una cadena
        /// </summary>
        /// <param name="X">Cadena a computar</param>
        /// <returns>Un arreglo de bytes con la suma de verificación</returns>
        public virtual byte[] Compute(string X) => Compute(Encoding.Unicode.GetBytes(X));
        /// <summary>
        /// Calcula una suma de verificación de forma asíncrona sobre un arreglo de bytes
        /// </summary>
        /// <param name="X">Arreglo de bytes a computar</param>
        /// <returns>Un arreglo de bytes con la suma de verificación</returns>
        public virtual async Task<byte[]> ComputeAsync(byte[] X) => await Task.Run(() => Compute(X));
        /// <summary>
        /// Calcula una suma de verificación de forma asíncrona sobre un <see cref="Stream"/>
        /// </summary>
        /// <param name="X"><see cref="Stream"/> con la información a computar</param>
        /// <returns>Un arreglo de bytes con la suma de verificación</returns>
        public virtual async Task<byte[]> ComputeAsync(Stream X) => await Task.Run(() => Compute(X));
        /// <summary>
        /// Calcula una suma de verificación de forma asíncrona sobre un <see cref="TextReader"/>
        /// </summary>
        /// <param name="X"><see cref="TextReader"/> con la información a computar</param>
        /// <returns>Un arreglo de bytes con la suma de verificación</returns>
        public virtual async Task<byte[]> ComputeAsync(TextReader X) => await Task.Run(() => Compute(X));
        /// <summary>
        /// Calcula una suma de verificación de forma asíncrona sobre una cadena
        /// </summary>
        /// <param name="X">Cadena a computar</param>
        /// <returns>Un arreglo de bytes con la suma de verificación</returns>
        public virtual async Task<byte[]> ComputeAsync(string X) => await Task.Run(() => Compute(X));
    }
}