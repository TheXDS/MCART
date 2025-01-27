// CredentialDialog.cs
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
/// Contiene información que permite configurar la creación de un diálogo que
/// solicita credenciales genéricas al usuario.
/// </summary>
/// <param name="Title">Título del diálogo.</param>
/// <param name="Message">Mensaje del diálogo.</param>
/// <param name="DefaultUser">Usuario predeterminado.</param>
/// <param name="LastWin32Error">
/// Último error de Win32 que ha ocurrido. Permite visualizar mensajes de error
/// genéricos de Windows en el diálogo nativo.
/// </param>
/// <param name="ShowSave">
/// Indica si se debe mostrar al usuario la opción de guardar las credenciales
/// provistas.
/// </param>
/// <seealso href="https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-"/>
[ExcludeFromCodeCoverage]
public readonly record struct CredentialDialogProperties(string? Title, string Message, string? DefaultUser, int LastWin32Error, bool ShowSave)
{
    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="CredentialDialogProperties"/>.
    /// </summary>
    /// <param name="title">Título del diálogo.</param>
    /// <param name="message">Mensaje del diálogo.</param>
    /// <param name="defaultUser">Usuario predeterminado.</param>
    /// <param name="lastWin32Error">
    /// Último error de Win32 que ha ocurrido. Permite visualizar mensajes de error
    /// genéricos de Windows en el diálogo nativo.
    /// </param>
    public CredentialDialogProperties(string? title, string message, string? defaultUser, int lastWin32Error) : this(title, message, defaultUser, lastWin32Error, false) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// </summary>
    /// <param name="title">Título del diálogo.</param>
    /// <param name="message">Mensaje del diálogo.</param>
    /// <param name="defaultUser">Usuario predeterminado.</param>
    /// <param name="showSave">
    /// Indica si se debe mostrar al usuario la opción de guardar las credenciales
    /// provistas.
    /// </param>
    public CredentialDialogProperties(string? title, string message, string? defaultUser, bool showSave) : this(title, message, defaultUser, 0, showSave) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="CredentialDialogProperties"/>.
    /// </summary>
    /// <param name="title">Título del diálogo.</param>
    /// <param name="message">Mensaje del diálogo.</param>
    /// <param name="defaultUser">Usuario predeterminado.</param>
    public CredentialDialogProperties(string? title, string message, string? defaultUser) : this(title, message, defaultUser, 0, false) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="CredentialDialogProperties"/>.
    /// </summary>
    /// <param name="title">Título del diálogo.</param>
    /// <param name="message">Mensaje del diálogo.</param>
    public CredentialDialogProperties(string? title, string message) : this(title, message, null, 0, false) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="CredentialDialogProperties"/>.
    /// </summary>
    /// <param name="message">Mensaje del diálogo.</param>
    /// <param name="defaultUser">Usuario predeterminado.</param>
    /// <param name="lastWin32Error">
    /// Último error de Win32 que ha ocurrido. Permite visualizar mensajes de error
    /// genéricos de Windows en el diálogo nativo.
    /// </param>
    public CredentialDialogProperties(string message, string? defaultUser, int lastWin32Error) : this(null, message, defaultUser, lastWin32Error, false) { }

    /// <summary>
    /// Inicializa una nueva instancia de la estructura
    /// <see cref="CredentialDialogProperties"/>.
    /// </summary>
    /// <param name="message">Mensaje del diálogo.</param>
    public CredentialDialogProperties(string message) : this(null, message, null, 0, false) { }
}
