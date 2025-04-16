// IProgressDialog.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que provea de
/// funcionalidad de reporte de progreso de una operación en un cuadro de
/// diálogo nativo de Microsoft Windows.
/// </summary>
public interface IProgressDialog
{
    /// <summary>
    /// Obtiene o establece el título del diálogo.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Obtiene o establece el valor que representa el máximo en la barra de
    /// progreso del diálogo.
    /// </summary>
    int Maximum { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que indica si se deben compactar las
    /// líneas de texto en caso que superen el ancho disponible en el diálogo.
    /// </summary>
    bool CompactPaths { get; set; }

    /// <summary>
    /// Obtiene o establece el mensaje a mostrar cuando se solicita la
    /// cancelación de la operación en progreso.
    /// </summary>
    string CancelMessage { get; set; }

    /// <summary>
    /// Obtiene o establece el valor actual de progreso del diálobo.
    /// </summary>
    int Value { get; set; }

    /// <summary>
    /// Obtiene o establece un valor que indica si el diálogo debe cerrarse
    /// automáticamente al alcanzar un progreso del 100%.
    /// </summary>
    bool AutoClose { get; set; }

    /// <summary>
    /// Obtiene o establece el valor de la primera línea de texto del cuadro de
    /// diálogo.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Se produce si se intenta establecer el valor de esta línea cuando el
    /// diálogo ha sido configurado para mostrar el tiempo restante en la
    /// tercera línea.
    /// </exception>
    string Line3 { get; set; }

    /// <summary>
    /// Obtiene o establece el valor de la segunda línea de texto del cuadro de
    /// diálogo.
    /// </summary>
    string Line2 { get; set; }

    /// <summary>
    /// Obtiene o establece el valor de la tercera línea de texto del cuadro de
    /// diálogo.
    /// </summary>
    string Line1 { get; set; }

    /// <summary>
    /// Obtiene un valor que indica si se ha solicitado la cancelación de la
    /// operación actualmente en curso.
    /// </summary>
    bool HasUserCancelled { get; }

    /// <summary>
    /// Cierra el diálogo de progreso de operación.
    /// </summary>
    void Close();

    /// <summary>
    /// Indica que la operación ha sido pausada.
    /// </summary>
    void Pause();

    /// <summary>
    /// Indica que una operación previamente pausada continuará.
    /// </summary>
    void Resume();
}