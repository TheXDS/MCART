/*
ColorParsers.cs

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

#region Configuración de ReSharper

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

#endregion

namespace TheXDS.MCART.Types
{
    public partial struct Color
    {
        /// <summary>
        ///     Define una serie de métodos a implementar por una clase que permita
        ///     convertir un valor en un <see cref="Color" />.
        /// </summary>
        /// <typeparam name="T">Tipo de valor a convertir.</typeparam>
        public interface IColorParser<T> where T : struct
        {
            /// <summary>
            ///     Convierte un <typeparamref name="T" /> en un
            ///     <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor.
            /// </returns>
            Color From(T value);

            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor de tipo
            ///     <typeparamref name="T" />.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor de tipo <typeparamref name="T" /> creado a partir del
            ///     <see cref="Color" /> especificado.
            /// </returns>
            T To(Color color);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 32 bits, 8 bits por canal, 8 bits de alfa ordenados como AABBGGRR.
        /// </summary>
        public class ABGR32 : IColorParser<int>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(int value)
            {
                return new Color(
                    (byte)(value & 0xff),
                    (byte)((value & 0xff00) >> 8),
                    (byte)((value & 0xff0000) >> 16),
                    (byte)((value & 0xff000000) >> 24));
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public int To(Color color)
            {
                return color.R | (color.G << 8) | (color.B << 16) | (color.A << 24);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 24 bits, 8 bits por canal, sin alfa.
        /// </summary>
        public class BGR24 : IColorParser<int>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(int value)
            {
                return new Color(
                    (byte) (value & 0xff),
                    (byte) ((value & 0xff00) >> 8),
                    (byte) ((value & 0xff0000) >> 16));
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public int To(Color color)
            {
                return color.R | (color.G << 8) | (color.B << 16);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 16 bits, 4 bits por canal, 4 bits de alfa.
        /// </summary>
        public class ABGR4444 : IColorParser<short>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value)
            {
                return new Color(
                    (byte) ((value & 0xf) * 256 / 16),
                    (byte) (((value & 0xf0) >> 4) * 256 / 16),
                    (byte) (((value & 0xf00) >> 8) * 256 / 16),
                    (byte) (((value & 0xf000) >> 12) * 256 / 16));
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public short To(Color color)
            {
                return (short) (
                    (color.R * 16 / 256) |
                    ((color.G * 16 / 256) << 4) |
                    ((color.B * 16 / 256) << 8) |
                    ((color.A * 16 / 256) << 12));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 12 bits, 4 bits por canal, sin alfa.
        /// </summary>
        public class BGR12 : IColorParser<short>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value)
            {
                return new Color(
                    (byte) ((value & 0xf) * 256 / 16),
                    (byte) (((value & 0xf0) >> 4) * 256 / 16),
                    (byte) (((value & 0xf00) >> 8) * 256 / 16),
                    255);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public short To(Color color)
            {
                return (short) (
                    (color.R * 16 / 256) |
                    ((color.G * 16 / 256) << 4) |
                    ((color.B * 16 / 256) << 8));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 16 bits, 5 bits por canal, 1 bit de alfa.
        /// </summary>
        public class ABGR16 : IColorParser<short>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value)
            {
                return new Color(
                    (byte) ((value & 0x1f) * 256 / 32),
                    (byte) (((value & 0x3e0) >> 5) * 256 / 32),
                    (byte) (((value & 0x7c00) >> 10) * 256 / 32),
                    (byte) ((value & 0x8000) >> 15) * 256);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public short To(Color color)
            {
                return (short) (
                    (color.R * 32 / 256) |
                    ((color.G * 32 / 256) << 5) |
                    ((color.B * 32 / 256) << 10) |
                    ((color.A * 2 / 256) << 15));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 15 bits, 5 bits por canal, sin alfa.
        /// </summary>
        public class BGR555 : IColorParser<short>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value)
            {
                return new Color(
                    (byte) ((value & 0x1f) * 256 / 32),
                    (byte) (((value & 0x3e0) >> 5) * 256 / 32),
                    (byte) (((value & 0x7c00) >> 10) * 256 / 32),
                    255);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public short To(Color color)
            {
                return (short) (
                    (color.R * 32 / 256) |
                    ((color.G * 32 / 256) << 5) |
                    ((color.B * 32 / 256) << 10));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 16 bits, 5 bits para los canales rojo y azul,
        ///     6 para el canal verde, sin alfa.
        /// </summary>
        public class BGR565 : IColorParser<short>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value)
            {
                return new Color(
                    (byte) ((value & 0x1f) * 256 / 32),
                    (byte) (((value & 0x7e0) >> 5) * 256 / 64),
                    (byte) (((value & 0x7c00) >> 11) * 256 / 32),
                    255);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public short To(Color color)
            {
                return (short) (
                    (color.R * 32 / 256) |
                    ((color.G * 64 / 256) << 5) |
                    ((color.B * 32 / 256) << 11));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 8 bits, 2 bits por canal, 2 bits de alfa.
        /// </summary>
        public class ABGR2222 : IColorParser<byte>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                return new Color(
                    (byte) ((value & 0x3) * 256 / 4),
                    (byte) (((value & 0xc) >> 2) * 256 / 4),
                    (byte) (((value & 0x30) >> 4) * 256 / 4),
                    (byte) (((value & 0xf0) >> 6) * 256 / 4));
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public byte To(Color color)
            {
                return (byte) (
                    (color.R * 4 / 256) |
                    ((color.G * 4 / 256) << 2) |
                    ((color.B * 4 / 256) << 4) |
                    ((color.A * 4 / 256) << 6));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 6 bits, 2 bits por canal, sin alfa.
        /// </summary>
        public class BGR222 : IColorParser<byte>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                return new Color(
                    (byte) ((value & 0x3) * 256 / 4),
                    (byte) (((value & 0xc) >> 2) * 256 / 4),
                    (byte) (((value & 0x30) >> 4) * 256 / 4));
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public byte To(Color color)
            {
                return (byte) (
                    (color.R * 4 / 256) |
                    ((color.G * 4 / 256) << 2) |
                    ((color.B * 4 / 256) << 4));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor de 8 bits, 3 bits para los canales rojo y verde,
        ///     2 para el canal azul, sin alfa.
        /// </summary>
        public class RGB233 : IColorParser<byte>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                return new Color(
                    (byte) (((value & 0xe0) >> 5) * 256 / 8),
                    (byte) (((value & 0x1c) >> 2) * 256 / 8),
                    (byte) ((value & 0x3) * 256 / 4));
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public byte To(Color color)
            {
                return (byte) (
                    ((color.B * 4 / 256) << 5) |
                    ((color.G * 8 / 256) << 2) |
                    (color.R * 8 / 256));
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor monocromático de 1 bit, sin alfa.
        /// </summary>
        public class Monochrome : IColorParser<bool>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(bool value)
            {
                var m = value ? byte.MaxValue : byte.MinValue;
                return new Color(m, m, m);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public bool To(Color color)
            {
                return (color.B | color.G | color.R) * 2 / 256 == 1;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor monocromático de 8 bits (escala de grises) sin
        ///     alfa, en escala lineal.
        /// </summary>
        public class Grayscale : IColorParser<byte>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                return new Color(value, value, value);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public byte To(Color color)
            {
                return (byte) ((color.R + color.G + color.B) / 3);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un valor monocromático de 8 bits sin alfa, en el espacio
        ///     colorimétrico de escala de grises.
        /// </summary>
        public class ColorimetricGrayscale : IColorParser<byte>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                var yLinear = (double) value / 255;
                var ySrgb = (float) (yLinear > 0.0031308
                    ? System.Math.Pow(yLinear, 1 / 2.4) * 1.055 - 0.055
                    : 12.92 * yLinear);
                return new Color(ySrgb, ySrgb, ySrgb);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en un valor, utilizando el
            ///     <see cref="Color.IColorParser{T}" /> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color" /> a convertir.</param>
            /// <returns>
            ///     Un valor creado a partir de este <see cref="Color" />.
            /// </returns>
            public byte To(Color color)
            {
                var lr = color._r > 0.04045f ? System.Math.Pow((color._r + 0.055) / 1.055, 2.4) : color._r / 12.92;
                var lg = color._g > 0.04045f ? System.Math.Pow((color._g + 0.055) / 1.055, 2.4) : color._g / 12.92;
                var lb = color._b > 0.04045f ? System.Math.Pow((color._b + 0.055) / 1.055, 2.4) : color._b / 12.92;
                return (byte) (lr * 0.2126 + lg * 0.7152 + lb * 0.0722);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Implementa un <see cref="Color.IColorParser{T}" /> que tiene como formato
        ///     de color un byte de atributo VGA con información de color e
        ///     intensidad, ignorando el color de fondo y el bit de blink.
        /// </summary>
        public class VGAAttributeByte : IColorParser<byte>
        {
            /// <inheritdoc />
            /// <summary>
            ///     Convierte una estructura compatible en un <see cref="Color" />.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            ///     Un <see cref="Color" /> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                var i = (byte) (((value >> 3) | 1) == 1 ? 255 : 128);
                var r = (byte) (((value >> 2) | 1) * i);
                var g = (byte) (((value >> 1) | 1) * i);
                var b = (byte) ((value | 1) * i);
                return new Color(r, g, b);
            }

            /// <inheritdoc />
            /// <summary>
            ///     Convierte un <see cref="Color" /> en su representación como un
            ///     <see cref="byte" /> de atributo VGA.
            /// </summary>
            /// <returns>
            ///     Un <see cref="byte" /> con la representación binaria de este
            ///     <see cref="Color" />, en formato BGRI de 4 bits.
            ///     El byte de atributo no incluirá color de fondo ni bit de
            ///     Blinker.
            /// </returns>
            public byte To(Color color)
            {
                var b = (byte) (color.B * 2 / 256);
                var g = (byte) ((color.G * 2 / 256) << 1);
                var r = (byte) ((color.R * 2 / 256) << 2);
                var i = (byte) (((color.B | color.G | color.R) * 2 / 256) << 3);
                return (byte) (b | g | r | i);
            }
        }
    }
}