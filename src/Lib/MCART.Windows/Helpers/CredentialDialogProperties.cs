// CredentialDialog.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2026 César Andrés Morgan
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
/// Contains information that allows configuring the creation of a dialog
/// that prompts the user for generic credentials.
/// </summary>
/// <param name="Title">Dialog title.</param>
/// <param name="Message">Dialog message.</param>
/// <param name="DefaultUser">Default user name.</param>
/// <param name="LastWin32Error">
/// Last Win32 error that occurred. Allows viewing generic Windows error
/// messages in the native dialog.
/// </param>
/// <param name="ShowSave">
/// Whether to show the option to save the provided credentials.
/// </param>
/// <seealso href="https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-"/>
[ExcludeFromCodeCoverage]
public readonly record struct CredentialDialogProperties(string? Title, string Message, string? DefaultUser, int LastWin32Error, bool ShowSave)
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CredentialDialogProperties"/> struct.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultUser">Default user name.</param>
    /// <param name="lastWin32Error">
    /// Last Win32 error that occurred. Allows viewing generic Windows error
    /// messages in the native dialog.
    /// </param>
    public CredentialDialogProperties(string? title, string message, string? defaultUser, int lastWin32Error) : this(title, message, defaultUser, lastWin32Error, false) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CredentialDialogProperties"/> struct.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultUser">Default user name.</param>
    /// <param name="showSave">
    /// Whether to show the option to save the provided credentials.
    /// </param>
    public CredentialDialogProperties(string? title, string message, string? defaultUser, bool showSave) : this(title, message, defaultUser, 0, showSave) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CredentialDialogProperties"/> struct.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultUser">Default user name.</param>
    public CredentialDialogProperties(string? title, string message, string? defaultUser) : this(title, message, defaultUser, 0, false) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CredentialDialogProperties"/> struct.
    /// </summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Dialog message.</param>
    public CredentialDialogProperties(string? title, string message) : this(title, message, null, 0, false) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CredentialDialogProperties"/> struct.
    /// </summary>
    /// <param name="message">Dialog message.</param>
    /// <param name="defaultUser">Default user name.</param>
    /// <param name="lastWin32Error">
    /// Last Win32 error that occurred. Allows viewing generic Windows error
    /// messages in the native dialog.
    /// </param>
    public CredentialDialogProperties(string message, string? defaultUser, int lastWin32Error) : this(null, message, defaultUser, lastWin32Error, false) { }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CredentialDialogProperties"/> struct.
    /// </summary>
    /// <param name="message">Dialog message.</param>
    public CredentialDialogProperties(string message) : this(null, message, null, 0, false) { }
}
