/*
Size3D.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
public struct Size3D : IFormattable, IEquatable<Size3D>, IEquatable<ISize3D>, IEquatable<IVector3D>, ISize3D, IVector3D
{
    /// <summary>
    /// Obtiene un valor que no representa ningún tamaño. Este campo es
    /// de solo lectura.
    /// </summary>
    public static readonly Size3D Nothing = new(double.NaN, double.NaN, double.NaN);

    /// <summary>
    /// Obtiene un valor que representa un tamaño nulo. Este campo es
    /// de solo lectura.
    /// </summary>
    public static readonly Size3D Zero = new(0, 0, 0);

    /// <summary>
    /// Obtiene un valor que representa un tamaño infinito. Este campo
    /// es de solo lectura.
    /// </summary>
    public static readonly Size3D Infinity = new(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="Size"/>.
    /// </summary>
    /// <param name="width">Valor de ancho.</param>
    /// <param name="height">Valor de alto.</param>
    /// <param name="depth">Valor de profundidad.</param>
    public Size3D(double width, double height, double depth)
    {
        Width = width;
        Height = height;
        Depth = depth;
    }

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Size3D operator +(Size3D l, Size3D r)
    {
        return new(l.Width + r.Width, l.Height + r.Height, l.Depth + r.Depth);
    }

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Size3D operator +(Size3D l, ISize3D r)
    {
        return new(l.Width + r.Width, l.Height + r.Height, l.Depth + r.Depth);
    }

    /// <summary>
    /// Realiza una operación de suma sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La suma de los vectores de los puntos.</returns>
    public static Size3D operator +(Size3D l, IVector3D r)
    {
        return new(l.Width + r.X, l.Height + r.Y, l.Depth + r.Z);
    }

    /// <summary>
    /// Realiza una operación de suma sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de suma.</param>
    /// <returns>
    /// Un nuevo <see cref="Size3D" /> cuyos vectores son la suma de los
    /// vectores originales + <paramref name="r" />.
    /// </returns>
    public static Size3D operator +(Size3D l, double r)
    {
        return new(l.Width + r, l.Height + r, l.Depth + r);
    }

    /// <summary>
    /// Realiza una operación de resta sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La resta de los vectores de los puntos.</returns>
    public static Size3D operator -(Size3D l, Size3D r)
    {
        return new(l.Width - r.Width, l.Height - r.Height, l.Depth - r.Depth);
    }

    /// <summary>
    /// Realiza una operación de resta sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La resta de los vectores de los puntos.</returns>
    public static Size3D operator -(Size3D l, ISize3D r)
    {
        return new(l.Width - r.Width, l.Height - r.Height, l.Depth - r.Depth);
    }

    /// <summary>
    /// Realiza una operación de resta sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de resta.</param>
    /// <returns>
    /// Un nuevo <see cref="Size3D" /> cuyos vectores son la resta de los
    /// vectores originales - <paramref name="r" />.
    /// </returns>
    public static Size3D operator -(Size3D l, double r)
    {
        return new(l.Width - r, l.Height - r, l.Depth - r);
    }

    /// <summary>
    /// Realiza una operación de resta sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La resta de los vectores de los puntos.</returns>
    public static Size3D operator -(Size3D l, IVector3D r)
    {
        return new(l.Width - r.X, l.Height - r.Y, l.Depth - r.Z);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Size3D operator *(Size3D l, Size3D r)
    {
        return new(l.Width * r.Width, l.Height * r.Height, l.Depth * r.Depth);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Size3D operator *(Size3D l, ISize3D r)
    {
        return new(l.Width * r.Width, l.Height * r.Height, l.Depth * r.Depth);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de multiplicación.</param>
    /// <returns>
    /// Un nuevo <see cref="Size3D" /> cuyos vectores son la multiplicación
    /// de los vectores originales * <paramref name="r" />.
    /// </returns>
    public static Size3D operator *(Size3D l, double r)
    {
        return new(l.Width * r, l.Height * r, l.Depth * r);
    }

    /// <summary>
    /// Realiza una operación de multiplicación sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La multiplicación de los vectores de los puntos.</returns>
    public static Size3D operator *(Size3D l, IVector3D r)
    {
        return new(l.Width * r.X, l.Height * r.Y, l.Depth * r.Z);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Size3D operator /(Size3D l, Size3D r)
    {
        return new(l.Width / r.Width, l.Height / r.Height, l.Depth / r.Depth);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Size3D operator /(Size3D l, ISize3D r)
    {
        return new(l.Width / r.Width, l.Height / r.Height, l.Depth / r.Depth);
    }

    /// <summary>
    /// Realiza una operación de división sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de división.</param>
    /// <returns>
    /// Un nuevo <see cref="Size3D" /> cuyos vectores son la división de
    /// los vectores originales / <paramref name="r" />.
    /// </returns>
    public static Size3D operator /(Size3D l, double r)
    {
        return new(l.Width / r, l.Height / r, l.Depth / r);
    }

    /// <summary>
    /// Realiza una operación de división sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>La división de los vectores de los puntos.</returns>
    public static Size3D operator /(Size3D l, IVector3D r)
    {
        return new(l.Width / r.X, l.Height / r.Y, l.Depth / r.Z);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>El residuo de los vectores de los puntos.</returns>
    public static Size3D operator %(Size3D l, Size3D r)
    {
        return new(l.Width % r.Width, l.Height % r.Height, l.Depth % r.Depth);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>El residuo de los vectores de los puntos.</returns>
    public static Size3D operator %(Size3D l, ISize3D r)
    {
        return new(l.Width % r.Width, l.Height % r.Height, l.Depth % r.Depth);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre el punto.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Operando de residuo.</param>
    /// <returns>
    /// Un nuevo <see cref="Size3D" /> cuyos vectores son el residuo de los
    /// vectores originales % <paramref name="r" />.
    /// </returns>
    public static Size3D operator %(Size3D l, double r)
    {
        return new(l.Width % r, l.Height % r, l.Depth % r);
    }

    /// <summary>
    /// Realiza una operación de residuo sobre los puntos.
    /// </summary>
    /// <param name="l">Punto 1.</param>
    /// <param name="r">Punto 2.</param>
    /// <returns>
    /// Un nuevo <see cref="Size3D" /> cuyos vectores son el residuo de los
    /// vectores originales % <paramref name="r" />.
    /// </returns>
    public static Size3D operator %(Size3D l, IVector3D r)
    {
        return new(l.Width % r.X, l.Height % r.Y, l.Depth % r.Z);
    }

    /// <summary>
    /// Incrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a incrementar.</param>
    /// <returns>Un punto con sus vectores incrementados en 1.</returns>
    public static Size3D operator ++(Size3D p)
    {
        p.Width++;
        p.Height++;
        p.Depth++;
        return p;
    }

    /// <summary>
    /// Decrementa en 1 los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a decrementar.</param>
    /// <returns>Un punto con sus vectores decrementados en 1.</returns>
    public static Size3D operator --(Size3D p)
    {
        p.Width--;
        p.Height--;
        p.Depth--;
        return p;
    }

    /// <summary>
    /// Convierte a positivos los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con sus vectores positivos.</returns>
    public static Size3D operator +(Size3D p)
    {
        return new(+p.Width, +p.Height, +p.Depth);
    }

    /// <summary>
    /// Invierte el signo de los vectores del punto.
    /// </summary>
    /// <param name="p">Punto a operar.</param>
    /// <returns>Un punto con el signo de sus vectores invertido.</returns>
    public static Size3D operator -(Size3D p)
    {
        return new(-p.Width, -p.Height, -p.Depth);
    }

    /// <summary>
    /// Compara la igualdad entre dos instancias de <see cref="Size3D"/>.
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
    public static bool operator ==(Size3D size1, ISize3D size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compara la igualdad entre dos instancias de <see cref="Size3D"/>.
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
    public static bool operator ==(Size3D size1, Size3D size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compara la igualdad entre dos instancias de <see cref="Size3D"/>.
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
    public static bool operator ==(Size3D size1, IVector3D size2)
    {
        return size1.Equals(size2);
    }

    /// <summary>
    /// Compara la desigualdad entre dos instancias de 
    /// <see cref="Size3D"/>.
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
    public static bool operator !=(Size3D size1, IVector3D size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Compara la desigualdad entre dos instancias de 
    /// <see cref="Size3D"/>.
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
    public static bool operator !=(Size3D size1, Size3D size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Compara la desigualdad entre dos instancias de 
    /// <see cref="Size3D"/>.
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
    public static bool operator !=(Size3D size1, ISize3D size2)
    {
        return !(size1 == size2);
    }

    /// <summary>
    /// Obtiene el componente de altura del tamaño.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Obtiene el componente de ancho del tamaño.
    /// </summary>
    public double Width { get; set; }
    
    /// <summary>
    /// Obtiene el componente de profundidad del tamaño.
    /// </summary>
    public double Depth { get; set; }

    /// <summary>
    /// Calcula el área cuadrada representada por este tamaño.
    /// </summary>
    public double CubeVolume => Height * Width * Depth;

    /// <summary>
    /// Calcula el perímetro cuadrado representado por este tamaño.
    /// </summary>
    public double CubePerimeter => (Height * 2) + (Width * 2) + (Depth * 2);

    /// <summary>
    /// Determina si esta instancia representa un tamaño nulo.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si el tamaño es nulo o
    /// <see langword="false"/> si el tamaño no contiene volumen.
    /// </returns>
    public bool IsZero
    {
        get
        {
            return Height.IsValid() && Height == 0
                && Width.IsValid() && Width == 0
                && Depth.IsValid() && Depth == 0;
        }
    }

    /// <summary>
    /// Obtiene un valor que indica si el tamaño es válido en un contexto
    /// físico real.
    /// </summary>
    public bool IsReal
    {
        get
        {
            return Height.IsValid() && Height > 0
                && Width.IsValid() && Width > 0
                && Depth.IsValid() && Depth > 0;
        }
    }

    /// <summary>
    /// Obtiene un valor que indica si todas las magnitudes de tamaño de esta
    /// instancia son válidas.
    /// </summary>
    public bool IsValid
    {
        get
        {
            return Height.IsValid() && Width.IsValid() && Depth.IsValid();
        }
    }

    double IVector3D.Z => Depth;

    double IVector.X => Width;

    double IVector.Y => Height;

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
    public bool Equals(Size3D other)
    {
        return (Height == other.Height || (!Height.IsValid() && !other.Height.IsValid()))
            && (Width == other.Width || (!Width.IsValid() && !other.Width.IsValid()))
            && (Depth == other.Depth || (!Depth.IsValid() && !other.Depth.IsValid()));
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
    public bool Equals(ISize3D? other)
    {
        return other is not null
            && (Height == other.Height || (!Height.IsValid() && !other.Height.IsValid()))
            && (Width == other.Width || (!Width.IsValid() && !other.Width.IsValid()))
            && (Depth == other.Depth || (!Depth.IsValid() && !other.Depth.IsValid()));
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
    public bool Equals(IVector3D? other)
    {
        return other is not null
            && (Height == other.Y || (!Height.IsValid() && !other.Y.IsValid()))
            && (Width == other.X || (!Width.IsValid() && !other.X.IsValid()))
            && (Depth == other.Z || (!Depth.IsValid() && !other.Z.IsValid()));
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
    public override bool Equals(object? obj)
    {
        if (obj is not ISize3D p) return false;
        return Equals(p);
    }

    /// <summary>
    /// Devuelve el código hash generado para esta instancia.
    /// </summary>
    /// <returns>
    /// Un código hash que representa a esta instancia.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Height, Width, Depth);
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
    public static Size3D Parse(string value)
    {
        if (TryParse(value, out Size3D returnValue)) return returnValue;
        throw new FormatException();
    }

    /// <summary>
    /// Convierte este objeto en su representación como una cadena.
    /// </summary>
    /// <returns>
    /// Una representación en forma de <see cref="string" /> de este objeto.
    /// </returns>
    public override string ToString()
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
    public string ToString(string? format)
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
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format.IsEmpty()) format = "C";
        return format.ToUpperInvariant()[0] switch
        {
            'C' => $"{Width}, {Height}, {Depth}",
            'B' => $"[{Width}, {Height}, {Depth}]",
            'V' => $"Width: {Width}, Height: {Height}, Depth: {Depth}",
            'N' => $"Width: {Width}{Environment.NewLine}Height: {Height}{Environment.NewLine}Depth: {Depth}",
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
    public static bool TryParse(string? value, out Size3D size)
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
                string[]? separators = new[]
                {
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
                };
                return PrivateInternals.TryParseValues<double, Size3D>(separators, value.Without("()[]{}".ToCharArray()), 3, l => new(l[0], l[1], l[2]), out size);
        }
        return true;
    }

    bool IEquatable<IVector>.Equals(IVector? other) => other is not null && Width == other.X && Height == other.Y;
}
