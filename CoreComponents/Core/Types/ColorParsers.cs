/*
ColorParsers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

namespace TheXDS.MCART.Types
{
    public partial struct Color
    {
        /// <summary>
        /// Define una serie de métodos a implementar por una clase que permita
        /// convertir un valor en un <see cref="Color"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de valor a convertir.</typeparam>
        public interface IColorParser<T> where T : struct
        {
            /// <summary>
            /// Convierte un <typeparamref name="T"/> en un
            /// <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor.
            /// </returns>
            Color From(T value);
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor de tipo
            /// <typeparamref name="T"/>.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor de tipo <typeparamref name="T"/> creado a partir del 
            /// <see cref="Color"/> especificado.
            /// </returns>
            T To(Color color);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 32 bits, 8 bits por canal, 8 bits de alfa.
        /// </summary>
        public class RGBA8888 : IColorParser<int>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(int value) => new Color(
                (byte)(value & 0xff),
                (byte)((value & 0xff00) >> 8),
                (byte)((value & 0xff0000) >> 16),
                (byte)((value & 0xff000000) >> 24));
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public int To(Color color) => (color.R | color.G << 4 | color.B << 8 | color.A << 12);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 24 bits, 8 bits por canal, sin alfa.
        /// </summary>
        public class RGB888 : IColorParser<int>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(int value) => new Color(
                (byte)(value & 0xff),
                (byte)((value & 0xff00) >> 8),
                (byte)((value & 0xff0000) >> 16));
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public int To(Color color) => (color.R | color.G << 4 | color.B << 8);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 16 bits, 4 bits por canal, 4 bits de alfa.
        /// </summary>
        public class RGBA4444 : IColorParser<short>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value) => new Color(
                (byte)((value & 0xf) * 256 / 16),
                (byte)(((value & 0xf0) >> 4) * 256 / 16),
                (byte)(((value & 0xf00) >> 8) * 256 / 16),
                (byte)(((value & 0xf000) >> 12) * 256 / 16));
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public short To(Color color) => (short)(
                (color.R * 16 / 256) |
                (color.G * 16 / 256) << 4 |
                (color.B * 16 / 256) << 8 |
                (color.A * 16 / 256) << 12);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 12 bits, 4 bits por canal, sin alfa.
        /// </summary>
        public class RGB444 : IColorParser<short>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value) => new Color(
                (byte)((value & 0xf) * 256 / 16),
                (byte)(((value & 0xf0) >> 4) * 256 / 16),
                (byte)(((value & 0xf00) >> 8) * 256 / 16),
                255);
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public short To(Color color) => (short)(
                (color.R * 16 / 256) |
                (color.G * 16 / 256) << 4 |
                (color.B * 16 / 256) << 8);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 16 bits, 5 bits por canal, 1 bit de alfa.
        /// </summary>
        public class RGBA5551 : IColorParser<short>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value) => new Color(
                (byte)((value & 0x1f) * 256 / 32),
                (byte)(((value & 0x3e0) >> 5) * 256 / 32),
                (byte)(((value & 0x7c00) >> 10) * 256 / 32),
                (byte)((value & 0x8000) >> 15) * 256);
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public short To(Color color) => (short)(
                (color.R * 32 / 256) |
                (color.G * 32 / 256) << 5 |
                (color.B * 32 / 256) << 10 |
                (color.A * 2 / 256) << 15);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 15 bits, 5 bits por canal, sin alfa.
        /// </summary>
        public class RGB555 : IColorParser<short>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value) => new Color(
                (byte)((value & 0x1f) * 256 / 32),
                (byte)(((value & 0x3e0) >> 5) * 256 / 32),
                (byte)(((value & 0x7c00) >> 10) * 256 / 32),
                255);
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public short To(Color color) => (short)(
                (color.R * 32 / 256) |
                (color.G * 32 / 256) << 5 |
                (color.B * 32 / 256) << 10);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 16 bits, 5 bits para los canales rojo y azul,
        /// 6 para el canal verde, sin alfa.
        /// </summary>
        public class RGB565 : IColorParser<short>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(short value) => new Color(
                (byte)((value & 0x1f) * 256 / 32),
                (byte)(((value & 0x7e0) >> 5) * 256 / 64),
                (byte)(((value & 0x7c00) >> 11) * 256 / 32),
                255);
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public short To(Color color) => (short)(
                (color.R * 32 / 256) |
                (color.G * 64 / 256) << 5 |
                (color.B * 32 / 256) << 11);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 8 bits, 2 bits por canal, 2 bits de alfa.
        /// </summary>
        public class RGBA2222 : IColorParser<byte>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value) => new Color(
                (byte)((value & 0x3) * 256 / 4),
                (byte)(((value & 0xc) >> 2) * 256 / 4),
                (byte)(((value & 0x30) >> 4) * 256 / 4),
                (byte)(((value & 0xf0) >> 6) * 256 / 4));
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public byte To(Color color) => (byte)(
                (color.R * 4 / 256) |
                (color.G * 4 / 256) << 2 |
                (color.B * 4 / 256) << 4 |
                (color.A * 4 / 256) << 6);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 6 bits, 2 bits por canal, sin alfa.
        /// </summary>
        public class RGB222 : IColorParser<byte>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value) => new Color(
                (byte)((value & 0x3) * 256 / 4),
                (byte)(((value & 0xc) >> 2) * 256 / 4),
                (byte)(((value & 0x30) >> 4) * 256 / 4));
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public byte To(Color color) => (byte)(
                (color.R * 4 / 256) |
                (color.G * 4 / 256) << 2 |
                (color.B * 4 / 256) << 4);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor de 8 bits, 3 bits para los canales rojo y verde,
        /// 2 para el canal azul, sin alfa.
        /// </summary>
        public class BGR332 : IColorParser<byte>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value) => new Color(
                (byte)(((value & 0xe0) >> 5) * 256 / 8),
                (byte)(((value & 0x1c) >> 2) * 256 / 8),
                (byte)((value & 0x3) * 256 / 4));
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public byte To(Color color) => (byte)(
                (color.B * 4 / 256) << 5 |
                (color.G * 8 / 256) << 2 |
                (color.R * 8 / 256));
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor monocromático de 1 bit, sin alfa.
        /// </summary>
        public class Monochrome : IColorParser<bool>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(bool value)
            {
                byte m = value ? byte.MaxValue : byte.MinValue;
                return new Color(m, m, m);
            }
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public bool To(Color color) => ((color.B | color.G | color.R) * 2 / 256) == 1;
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor monocromático de 8 bits (escala de grises) sin
        /// alfa, en escala lineal.
        /// </summary>
        public class Grayscale : IColorParser<byte>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value) => new Color(value, value, value);
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public byte To(Color color) => (byte)((color.R + color.G + color.B) / 3);
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un valor monocromático de 8 bits sin alfa, en el espacio
        /// colorimétrico de escala de grises.
        /// </summary>
        public class ColorimetricGrayscale : IColorParser<byte>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                double yLinear = value / 255;
                float ySrgb = (float)(yLinear > 0.0031308 ? (System.Math.Pow(yLinear, 1 / 2.4) * 1.055) - 0.055 : 12.92 * yLinear);
                return new Color(ySrgb, ySrgb, ySrgb);
            }
            /// <summary>
            /// Convierte un <see cref="Color"/> en un valor, utilizando el
            /// <see cref="IColorParser{T}"/> especificado.
            /// </summary>
            /// <param name="color"><see cref="Color"/> a convertir.</param>
            /// <returns>
            /// Un valor creado a partir de este <see cref="Color"/>.
            /// </returns>
            public byte To(Color color)
            {
                double lr = color.r > 0.04045f ? System.Math.Pow((color.r + 0.055) / 1.055, 2.4) : color.r / 12.92;
                double lg = color.g > 0.04045f ? System.Math.Pow((color.g + 0.055) / 1.055, 2.4) : color.g / 12.92;
                double lb = color.b > 0.04045f ? System.Math.Pow((color.b + 0.055) / 1.055, 2.4) : color.b / 12.92;
                return (byte)(lr * 0.2126 + lg * 0.7152 + lb * 0.0722);
            }
        }
        /// <summary>
        /// Implementa un <see cref="IColorParser{T}"/> que tiene como formato
        /// de color un byte de atributo VGA con información de color e
        /// intensidad, ignorando el color de fondo y el bit de blink.
        /// </summary>
        public class VGAAttributeByte : IColorParser<byte>
        {
            /// <summary>
            /// Convierte una estructura compatible en un <see cref="Color"/>.
            /// </summary>
            /// <param name="value">Valor a convertir.</param>
            /// <returns>
            /// Un <see cref="Color"/> creado a partir del valor especificado.
            /// </returns>
            public Color From(byte value)
            {
                byte i = (byte)(((value >> 3) | 1) == 1 ? 255 : 128);
                byte r = (byte)(((value >> 2) | 1) * i);
                byte g = (byte)(((value >> 1) | 1) * i);
                byte b = (byte)((value | 1) * i);
                return new Color(r, g, b);
            }
            /// <summary>
            /// Convierte un <see cref="Color"/> en su representación como un
            /// <see cref="byte"/> de atributo VGA.
            /// </summary>
            /// <returns>
            /// Un <see cref="byte"/> con la representación binaria de este
            /// <see cref="Color"/>, en formato BGRI de 4 bits.
            /// El byte de atributo no incluirá color de fondo ni bit de
            /// Blinker.
            /// </returns>
            public byte To(Color color)
            {
                byte b = (byte)(color.B * 2 / 256);
                byte g = (byte)((color.G * 2 / 256) << 1);
                byte r = (byte)((color.R * 2 / 256) << 2);
                byte i = (byte)(((color.B | color.G | color.R) * 2 / 256) << 3);
                return (byte)(b | g | r | i);
            }
        }
    }
}