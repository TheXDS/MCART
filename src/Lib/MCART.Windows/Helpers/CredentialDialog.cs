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
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TheXDS.MCART.PInvoke;
using TheXDS.MCART.PInvoke.Models;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.PInvoke.Models.PromptForWindowsCredentialsFlags;
using St = TheXDS.MCART.Resources.Strings.Common;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Includes methods that provide access to a native Windows dialog
/// that requests generic credentials from the user.
/// </summary>
[ExcludeFromCodeCoverage]
public static class CredentialDialog
{
    private static CreduiInfo GetCredUiInfo(string? title, string message)
    {
        CreduiInfo credui = new()
        {
            pszCaptionText = title ?? St.Login,
            pszMessageText = message
        };
        credui.cbSize = Marshal.SizeOf(credui);
        return credui;
    }

    private static (nint, uint) GetDefaultCred(string? username)
    {
        if (username.IsEmpty()) return (nint.Zero, 0);
        uint inCredSize = 0;
        try { _ = CredUi.CredPackAuthenticationBuffer(0, username, string.Empty, nint.Zero, ref inCredSize); } catch { }
        nint inCredBuffer = Marshal.AllocCoTaskMem((int)inCredSize);
        CredUi.CredPackAuthenticationBuffer(0, username, string.Empty, inCredBuffer, ref inCredSize);
        return (inCredBuffer, inCredSize);
    }

    private static (string?, SecureString?) ReadResult(nint outCredBuffer, uint outCredSize)
    {
        const int MaxLength = 100;
        var usernameBuf = new StringBuilder(MaxLength);
        var passwordBuf = new StringBuilder(MaxLength);
        var domainBuf = new StringBuilder(MaxLength);
        int maxUserName = MaxLength;
        int maxDomain = MaxLength;
        int maxPassword = MaxLength;
        bool result = CredUi.CredUnPackAuthenticationBuffer(0, outCredBuffer, outCredSize, usernameBuf, ref maxUserName, domainBuf, ref maxDomain, passwordBuf, ref maxPassword);
        Ole32.CoTaskMemFree(outCredBuffer);
        return result ? (usernameBuf.ToString(), passwordBuf.ToString().ToSecureString()) : (null, null);
    }

    /// <summary>
    /// Requests credentials from the user.
    /// </summary>
    /// <param name="props">
    /// Properties that indicate the presentation and behavior of the dialog.
    /// </param>
    /// <returns>
    /// A <see cref="CredentialBoxResult"/> with the credentials entered by the user,
    /// or <see langword="null"/> if the user cancels or does not enter any credentials.
    /// </returns>
    public static CredentialBoxResult? GetCredentials(CredentialDialogProperties props)
    {
        CreduiInfo credui = GetCredUiInfo(props.Title, props.Message);
        (nint defaultCred, uint defaultCredSz) = GetDefaultCred(props.DefaultUser);
        uint authPackage = 0;
        bool save = false;
        PromptForWindowsCredentialsFlags dialogFlags = CREDUIWIN_GENERIC | GENERIC_CREDENTIALS;
        if (props.ShowSave) dialogFlags |= SHOW_SAVE_CHECK_BOX | CREDUIWIN_CHECKBOX;
        switch (CredUi.CredUIPromptForWindowsCredentials(ref credui, props.LastWin32Error, ref authPackage, defaultCred, defaultCredSz, out nint outCredBuffer, out uint outCredSize, ref save, dialogFlags))
        {
            case CredUIReturnCodes.NO_ERROR when ReadResult(outCredBuffer, outCredSize) is (string username, SecureString password):
                return new CredentialBoxResult(username, password, save);
            case CredUIReturnCodes.NO_ERROR:
            case CredUIReturnCodes.ERROR_CANCELLED:
                break;
            default:
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                break;
        }
        return null;
    }
}
