/*
Size.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.ComponentModel;
using System.Numerics;
using TheXDS.MCART.Math;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using CI = System.Globalization.CultureInfo;

namespace TheXDS.MCART.Types;

/// <summary>
/// Estructura universal que describe el tamaño de un objeto en ancho y
/// alto en un espacio de dos dimensiones.
/// </summary>
/// <param name="width">Valor de ancho.</param>
/// <param name="height">Valor de alto.</param>
public struct Size(double width, double height) : IFormattable, IEquatable<Size>, IEquatable<ISize>, IEquatable<IVector>, ISize, IVector
{
    /// <summary>
    /// Obtiene un valor que no representa ningún tamaño. Este campo es
    /// de solo lectura.
    /// </summary>
    public static readonly Size Nothing = new(double.NaN, double.NaN);

    /// <summary>
    /// Obtiene un valor que representa un tamaño nulo. Este campo es
    /// de solo lectura.
    /// </summary>
    public static readonly Size Zero = new(0, 0);

    /// <summary>
    /// Obtiene un valor que representa un tamaño infinito. Este campo
    /// es de solo lectura.
    /// </summary>
    public static readonly Size Infinity = new(double.PositiveInfinity, double.PositiveInfinity);

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Size operator +(Size l, Size r)
    {
        return new(l.Width + r.Width, l.Height + r.Height);
    }

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Size operator +(Size l, ISize r)
    {
        return new(l.Width + r.Width, l.Height + r.Height);
    }

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Size operator +(Size l, IVector r)
    {
        return new(l.Width + r.X, l.Height + r.Y);
    }

    /// <summary>
    /// Realiza una operación de suma sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de suma.</param>
    /// <returns>
    /// Un nuevo <see cref="Size" /> cuyos vectores son la suma de los
    /// vectores originales + <paramref name="r" />.
    /// </returns>
    public static Size operator +(Size l, double r)
    {
        return new(l.Width + r, l.Height + r);
    }

    /// <summary>
    /// Realiza una operación de resta sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La resta de los vectores de los puntos.</returns>
    public static Size operator -(Size l, Size r)
    {
        return new(l.Width - r.Width, l.Height - r.Height);
    }

    /// <summary>
    /// Realiza una operación de resta sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La resta de los vectores de los puntos.</returns>
    public static Size operator -(Size l, ISize r)
    {
        return new(l.Width - r.Width, l.Height - r.Height);
    }

    /// <summary>
    /// Realiza una operación de resta sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de resta.</param>
    /// <returns>
    /// Un nuevo <see cref="Size" /> cuyos vectores son la resta de los
    /// vectores originales - <paramref name="r" />.
    /// </returns>
    public static Size operator -(Size l, double r)
    {
        return new(l.Width - r, l.Height - r);
    }

    /// <summary>
    /// Realiza una operación de resta sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La resta de los vectores de los puntos.</returns>
    public static Size operator -(Size l, IVector r)
    {
        return new(l.Width - r.X, l.Height - r.Y);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Size operator *(Size l, Size r)
    {
        return new(l.Width * r.Width, l.Height * r.Height);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Size operator *(Size l, ISize r)
    {
        return new(l.Width * r.Width, l.Height * r.Height);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de multiplicación.</param>
    /// <returns>
    /// Un nuevo <see cref="Size" /> cuyos vectores son la multiplicación
    /// de los vectores originales * <paramref name="r" />.
    /// </returns>
    public static Size operator *(Size l, double r)
    {
        return new(l.Width * r, l.Height * r);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Size operator *(Size l, IVector r)
    {
        return new(l.Width * r.X, l.Height * r.Y);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Size operator /(Size l, Size r)
    {
        return new(l.Width / r.Width, l.Height / r.Height);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Size operator /(Size l, ISize r)
    {
        return new(l.Width / r.Width, l.Height / r.Height);
    }

    /// <summary>
    /// Realiza una operación de división sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de división.</param>
    /// <returns>
    /// Un nuevo <see cref="Size" /> cuyos vectores son la división de
    /// los vectores originales / <paramref name="r" />.
    /// </returns>
    public static Size operator /(Size l, double r)
    {
        return new(l.Width / r, l.Height / r);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Size operator /(Size l, IVector r)
    {
        return new(l.Width / r.X, l.Height / r.Y);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>El residuo de los vectores de los puntos.</returns>
    public static Size operator %(Size l, Size r)
    {
        return new(l.Width % r.Width, l.Height % r.Height);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>El residuo de los vectores de los puntos.</returns>
    public static Size operator %(Size l, ISize r)
    {
        return new(l.Width % r.Width, l.Height % r.Height);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de residuo.</param>
    /// <returns>
    /// Un nuevo <see cref="Size" /> cuyos vectores son el residuo de los
    /// vectores originales % <paramref name="r" />.
    /// </returns>
    public static Size operator %(Size l, double r)
    {
        return new(l.Width % r, l.Height % r);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>
    /// Un nuevo <see cref="Size" /> cuyos vectores son el residuo de los
    /// vectores originales % <paramref name="r" />.
    /// </returns>
    public static Size operator %(Size l, IVector r)
    {
        return new(l.Width % r.X, l.Height % r.Y);
    }

    /// <summary>
    /// Incrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a incrementar.</param>
    /// <returns>Un punto con sus vectores incrementados en 1.</returns>
    public static Size operator ++(Size p)
    {
        p.Width++;
        p.Height++;
        return p;
    }

    /// <summary>
    /// Decrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a decrementar.</param>
    /// <returns>Un punto con sus vectores decrementados en 1.</returns>
    public static Size operator --(Size p)
    {
        p.Width--;
        p.Height--;
        return p;
    }

    /// <summary>
    /// Convierte a positivos los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con sus vectores positivos.</returns>
    public static Size operator +(Size p)
    {
        return new(+p.Width, +p.Height);
    }

    /// <summary>
    /// Invierte el signo de los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con el signo de sus vectores invertido.</returns>
    public static Size operator -(Size p)
    {
        return new(-p.Width, -p.Height);
    }

    /// <summary>
    /// Compara la igualdad entre dos instancias de <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// Primer elemento a comparar.
    /// </param>
    /// <param name="size2">
    /// Segundo elemento a comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool operator ==(Size size1, ISize size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compara la igualdad entre dos instancias de <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// Primer elemento a comparar.
    /// </param>
    /// <param name="size2">
    /// Segundo elemento a comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool operator ==(Size size1, Size size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compara la igualdad entre dos instancias de <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// Primer elemento a comparar.
    /// </param>
    /// <param name="size2">
    /// Segundo elemento a comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool operator ==(Size size1, IVector size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compara la desigualdad entre dos instancias de 
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// Primer elemento a comparar.
    /// </param>
    /// <param name="size2">
    /// Segundo elemento a comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son distintos, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public static bool operator !=(Size size1, IVector size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Compara la desigualdad entre dos instancias de 
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// Primer elemento a comparar.
    /// </param>
    /// <param name="size2">
    /// Segundo elemento a comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son distintos, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public static bool operator !=(Size size1, Size size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Compara la desigualdad entre dos instancias de 
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="size1">
    /// Primer elemento a comparar.
    /// </param>
    /// <param name="size2">
    /// Segundo elemento a comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son distintos, <see langword="false"/> en caso
    /// contrario.
    /// </returns>
    public static bool operator !=(Size size1, ISize size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Convierte implícitamente un objeto
    /// <see cref="System.Drawing.Size"/> en un <see cref="Size"/>.
    /// </summary>
    /// <param name="size">
    /// Objeto a convertir.
    /// </param>
    public static implicit operator System.Drawing.Size(Size size)
    {
        return new((int)size.Width, (int)size.Height);
    }

    /// <summary>
    /// Convierte implícitamente un objeto
    /// <see cref="System.Drawing.SizeF"/> en un <see cref="Size"/>.
    /// </summary>
    /// <param name="size">
    /// Objeto a convertir.
    /// </param>
    public static implicit operator System.Drawing.SizeF(Size size)
    {
        return new((float)size.Width, (float)size.Height);
    }

    /// <summary>
    /// Convierte implícitamente un objeto
    /// <see cref="Size"/> en un <see cref="System.Drawing.Size"/>.
    /// </summary>
    /// <param name="size">
    /// Objeto a convertir.
    /// </param>
    public static implicit operator Size(System.Drawing.Size size)
    {
        return new(size.Width, size.Height);
    }

    /// <summary>
    /// Convierte implícitamente un objeto
    /// <see cref="Size"/> en un <see cref="System.Drawing.SizeF"/>.
    /// </summary>
    /// <param name="size">
    /// Objeto a convertir.
    /// </param>
    public static implicit operator Size(System.Drawing.SizeF size)
    {
        return new(size.Width, size.Height);
    }

    /// <summary>
    /// Obtiene el componente de altura del tamaño.
    /// </summary>
    public double Height { get; set; } = height;

    /// <summary>
    /// Obtiene el componente de ancho del tamaño.
    /// </summary>
    public double Width { get; set; } = width;

    /// <summary>
    /// Calcula el área cuadrada representada por este tamaño.
    /// </summary>
    public readonly double SquareArea => Height * Width;

    /// <summary>
    /// Calcula el perímetro cuadrado representado por este tamaño.
    /// </summary>
    public readonly double SquarePerimeter => (Height * 2) + (Width * 2);

    /// <summary>
    /// Determina si esta instancia representa un tamaño nulo.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si el tamaño es nulo,
    /// <see langword="false"/> si el tamaño contiene área, y
    /// <see langword="null"/> si alguna magnitud está indefinida.
    /// </returns>
    public readonly bool? IsZero => Height.IsValid() && Height == 0 && Width.IsValid() && Width == 0;

    /// <summary>
    /// Obtiene un valor que indica si el tamaño es válido en un contexto
    /// físico real.
    /// </summary>
    public readonly bool IsReal => Height.IsValid() && Height > 0 && Width.IsValid() && Width > 0;

    /// <summary>
    /// Obtiene un valor que indica si todas las magnitudes de tamaño de esta
    /// instancia son válidas.
    /// </summary>
    public readonly bool IsValid => Height.IsValid() && Width.IsValid();

    readonly double IVector.X => Width;

    readonly double IVector.Y => Height;

    /// <summary>
    /// Determina si esta instancia de <see cref="Size"/> es igual a
    /// otra.
    /// </summary>
    /// <param name="other">
    /// Instancia de <see cref="Size"/> contra la cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public readonly bool Equals(Size other)
    {
        return (Height == other.Height || (!Height.IsValid() && !other.Height.IsValid()))
            && (Width == other.Width || (!Width.IsValid() && !other.Width.IsValid()));
    }

    /// <summary>
    /// Determina si esta instancia de <see cref="ISize3D"/> es igual a
    /// otra.
    /// </summary>
    /// <param name="other">
    /// Instancia de <see cref="ISize3D"/> contra la cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public readonly bool Equals(ISize? other)
    {
        return other is not null
            && (Height == other.Height || (!Height.IsValid() && !other.Height.IsValid()))
            && (Width == other.Width || (!Width.IsValid() && !other.Width.IsValid()));
    }

    /// <summary>
    /// Determina si esta instancia de <see cref="IVector3D"/> es igual a
    /// otra.
    /// </summary>
    /// <param name="other">
    /// Instancia de <see cref="IVector3D"/> contra la cual comparar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si los tamaños representados en ambos
    /// objetos son iguales, <see langword="false"/> en caso contrario.
    /// </returns>
    public readonly bool Equals(IVector? other)
    {
        return other is not null
            && (Height == other.Y || (!Height.IsValid() && !other.Y.IsValid()))
            && (Width == other.X || (!Width.IsValid() && !other.X.IsValid()));
    }

    /// <summary>
    /// Indica si esta instancia y un objeto especificado son iguales.
    /// </summary>
    /// <param name="obj">
    /// Objeto que se va a compara con la instancia actual.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si esta instancia y <paramref name="obj" /> son iguales;
    /// de lo contrario, <see langword="false" />.
    /// </returns>
    public override readonly bool Equals(object? obj)
    {
        if (obj is not ISize p) return false;
        return Equals(p);
    }

    /// <summary>
    /// Devuelve el código hash generado para esta instancia.
    /// </summary>
    /// <returns>
    /// Un código hash que representa a esta instancia.
    /// </returns>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Height, Width);
    }

    /// <summary>
    /// Crea un <see cref="Size"/> a partir de una cadena.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear un <see cref="Size"/>.
    /// </param>
    /// <exception cref="FormatException">
    /// Se produce si la conversión ha fallado.
    /// </exception>
    /// <returns><see cref="Size"/> que ha sido creado.</returns>
    public static Size Parse(string value)
    {
        if (TryParse(value, out Size returnValue)) return returnValue;
        throw new FormatException();
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public override readonly string ToString()
    {
        return ToString(null);
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <param name="format">Formato a utilizar.</param>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public readonly string ToString(string? format)
    {
        return ToString(format, CI.CurrentCulture);
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <param name="format">Formato a utilizar.</param>
    /// <param name="formatProvider">
    /// Parámetro opcional.
    /// Proveedor de formato de la cultura a utilizar para dar formato a
    /// la representación como una cadena de este objeto. Si se omite,
    /// se utilizará <see cref="CI.CurrentCulture" />.
    /// </param>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format.IsEmpty()) format = "C";
        return format.ToUpperInvariant()[0] switch
        {
            'C' => $"{Width}, {Height}",
            'B' => $"[{Width}, {Height}]",
            'V' => $"Width: {Width}, Height: {Height}",
            'N' => $"Width: {Width}{Environment.NewLine}Height: {Height}",
            _ => throw Errors.FormatNotSupported(format),
        };
    }

    /// <summary>
    /// Intenta crear un <see cref="Size"/> a partir de una cadena.
    /// </summary>
    /// <param name="value">
    /// Valor a partir del cual crear un <see cref="Size"/>.
    /// </param>
    /// <param name="size">
    /// <see cref="Size"/> que ha sido creado.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la conversión ha tenido éxito,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool TryParse(string? value, out Size size)
    {
        switch (value)
        {
            case nameof(Nothing):
            case "":
            case null:
                size = Nothing;
                break;
            case nameof(Zero):
            case "0":
                size = Zero;
                break;
            case nameof(Infinity):
            case "PositiveInfinity":
            case "∞":
                size = Infinity;
                break;
            default:
                string[]? separators =
                [
                    ", ",
                    "; ",
                    " - ",
                    " : ",
                    " | ",
                    " ",
                    ",",
                    ";",
                    ":",
                    "|",
                ];
                return PrivateInternals.TryParseValues<double, Size>(new DoubleConverter(), separators, value.Without("()[]{}".ToCharArray()), 2, l => new(l[0], l[1]), out size);
        }
        return true;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Size"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="p"><see cref="Size"/> value to be converted.</param>
    public static implicit operator Vector2(Size p) => new((float)p.Width, (float)p.Height);

    /// <summary>
    /// Implicitly converts a <see cref="Vector2"/> to a <see cref="Size"/>.
    /// </summary>
    /// <param name="p"><see cref="Vector2"/> value to be converted.</param>
    public static implicit operator Size(Vector2 p) => new(p.X, p.Y);
}
