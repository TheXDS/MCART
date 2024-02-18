/*
ILGeneratorExtensions_Delegates.cs

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

using System.Reflection.Emit;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de código por medio
/// de la clase <see cref="ILGenerator"/>.
/// </summary>
public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Define un delegado que describe un bloque <see langword="for"/>.
    /// </summary>
    /// <param name="il">
    /// Referencia al generador de instrucciones en el cual se inserta el
    /// código del bloque <see langword="for"/>.
    /// </param>
    /// <param name="accumulator">
    /// Referencia al acumulador del ciclo.
    /// </param>
    /// <param name="break">
    /// Etiqueta de salida del bloque <see langword="for"/>.
    /// </param>
    /// <param name="next">
    /// Etiqueta de continuación del bloque <see langword="for"/>.
    /// </param>
    public delegate void ForBlock(ILGenerator il, LocalBuilder accumulator, Label @break, Label next);

    /// <summary>
    /// Define un delegado que describe un bloque <see langword="foreach"/>.
    /// </summary>
    /// <param name="il">
    /// Referencia al generador de instrucciones en el cual se inserta el
    /// código del bloque <see langword="foreach"/>.
    /// </param>
    /// <param name="item">
    /// Referencia al acumulador del ciclo.
    /// </param>
    /// <param name="break">
    /// Etiqueta de salida del bloque <see langword="foreach"/>. Debe ser
    /// invocada por medio de <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    /// <param name="continue">
    /// Etiqueta de continuación del bloque <see langword="foreach"/>.
    /// </param>
    public delegate void ForEachBlock(ILGenerator il, LocalBuilder item, Label @break, Label @continue);

    /// <summary>
    /// Define un delegado que describe un bloque <see langword="try"/>.
    /// </summary>
    /// <param name="il">
    /// Referencia al generador de instrucciones en el cual se inserta el
    /// código del bloque <see langword="try"/>.
    /// </param>
    /// <param name="leaveTry">
    /// Etiqueta de salida del bloque <see langword="try"/>. Debe ser
    /// invocada por medio de <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    public delegate void TryBlock(ILGenerator il, Label leaveTry);

    /// <summary>
    /// Define un delegado que describe un bloque <see langword="using"/>.
    /// </summary>
    /// <param name="il">
    /// Referencia al generador de instrucciones en el cual se inserta el
    /// código del bloque <see langword="using"/>.
    /// </param>
    /// <param name="disposable">
    /// Referencia al elemento desechable dentro del bloque <see langword="using"/>.
    /// </param>
    /// <param name="leaveTry">
    /// Etiqueta de salida del bloque <see langword="using"/>. Debe ser
    /// invocada por medio de <see cref="Leave(ILGenerator, Label)"/>.
    /// </param>
    public delegate void UsingBlock(ILGenerator il, LocalBuilder disposable, Label leaveTry);
}
