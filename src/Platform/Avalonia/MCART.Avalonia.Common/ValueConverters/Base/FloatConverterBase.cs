/*
FloatConverterBase.cs

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

using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.ValueConverters.Base;

/// <summary>
/// Clase base que incluye un método para obtener un <see cref="float"/>.
/// </summary>
public abstract class FloatConverterBase
{
    /// <summary>
    /// Convierte un valor a un <see cref="float"/>.
    /// </summary>
    /// <param name="value">
    /// Valor desde el cual obtener un <see cref="float"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="float"/> obtenido a partir del valor brindado.
    /// </returns>
    protected static float GetFloat(object? value)
    {
        return value is not null ? GetFloatFromNotNull(value) : 0f;
    }

    private static float GetFloatFromNotNull(object value)
    {
        return GetFloatFromParser(value) ?? GetFloatFromConverter(value) ?? 0f;
    }

    private static float? GetFloatFromParser(object value)
    {
        var parsers = new IFloatParser[]
        {
            new FloatParser<float>(o => o),
            new FloatParser<double>(o => (float)o),
            new FloatParser<byte>(o => o / 255f),
            new FloatParser<short>(o => o / 100f),
            new FloatParser<int>(o => o),
            new FloatParser<long>(o => o),
            new FloatParser<sbyte>(o => o),
            new FloatParser<ushort>(o => o / 100f),
            new FloatParser<uint>(o => o),
            new FloatParser<ulong>(o => o),
            new FloatParser<string>(o => float.TryParse(o, out float v) ? v : 0f),
        };
        return parsers.FirstOrDefault(p => p.Type == typeof(float))?.Parse(value);
    }

    private static float? GetFloatFromConverter(object value)
    {
        return (float?)Common.FindConverter(value.GetType(), typeof(float))?.ConvertTo(value, typeof(float));
    }

    private interface IFloatParser
    {
        Type Type { get; }
        float Parse(object value);
    }

    private class FloatParser<T> : IFloatParser where T : notnull
    {
        private readonly Func<T,float> _parser;

        public FloatParser(Func<T, float> parser)
        {
            _parser = parser;
        }

        Type IFloatParser.Type => typeof(T);

        float IFloatParser.Parse(object value) => _parser((T)value);
    }
}
