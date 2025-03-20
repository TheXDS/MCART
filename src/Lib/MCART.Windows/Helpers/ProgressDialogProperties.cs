// ProgressDialogProperties.cs
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

using System.Diagnostics.CodeAnalysis;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Provee de información sobre las propiedades a establecer al inicializar un
/// nuevo diálogo de progreso nativo de Microsoft Windows.
/// </summary>
/// <param name="CancelButton">
/// Indica si el cuadro de diálogo debe contener un botón para cancelar.
/// </param>
/// <param name="Marquee">
/// Indica si la barra de progreso del diálogo se mostrará en estado de
/// progreso indeterminado.
/// </param>
/// <param name="MinimizeButton">
/// Indica si el diálogo incluirá un botón para minimizar la ventana del
/// diálogo.
/// </param>
/// <param name="Modal">
/// Indica si el diálogo será mostrado en modo modal.
/// </param>
/// <param name="ProgressBar">
/// Indica si el diálogo contendrá una barra de progreso.
/// </param>
/// <param name="ShowTimeRemaining">
/// Indica si se mostrará el tiempo restante de la operación en la tercera
/// línea del diálogo.
/// </param>
[ExcludeFromCodeCoverage]
public readonly record struct ProgressDialogProperties(bool CancelButton, bool Marquee, bool MinimizeButton, bool Modal, bool ProgressBar = true, bool ShowTimeRemaining = true) { }