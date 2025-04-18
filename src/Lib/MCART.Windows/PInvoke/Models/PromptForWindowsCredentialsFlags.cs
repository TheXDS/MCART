﻿/*
CreduiInfo.cs

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

namespace TheXDS.MCART.PInvoke.Models;

[Flags]
internal enum PromptForWindowsCredentialsFlags
{
    /// <summary>
    /// The caller is requesting that the credential provider return the user name and password in plain text.
    /// This value cannot be combined with SECURE_PROMPT.
    /// </summary>
    CREDUIWIN_GENERIC = 0x1,

    /// <summary>
    /// The Save check box is displayed in the dialog box.
    /// </summary>
    CREDUIWIN_CHECKBOX = 0x2,

    /// <summary>
    /// The dialog prompts for administrator rights.
    /// </summary>
    REQUEST_ADMINISTRATOR = 0x4,

    /// <summary>
    /// The dialog excludes certificate checks.
    /// </summary>
    EXCLUDE_CERTIFICATES = 0x8,

    /// <summary>
    /// Only credential providers that support the authentication package specified by the authPackage parameter should be enumerated.
    /// This value cannot be combined with CREDUIWIN_IN_CRED_ONLY.
    /// </summary>
    CREDUIWIN_AUTHPACKAGE_ONLY = 0x10,

    /// <summary>
    /// Only the credentials specified by the InAuthBuffer parameter for the authentication package specified by the authPackage parameter should be enumerated.
    /// If this flag is set, and the InAuthBuffer parameter is NULL, the function fails.
    /// This value cannot be combined with CREDUIWIN_AUTHPACKAGE_ONLY.
    /// </summary>
    CREDUIWIN_IN_CRED_ONLY = 0x20,

    /// <summary>
    /// Shows the "Save Credentials" check box.
    /// </summary>
    SHOW_SAVE_CHECK_BOX = 0x40,

    ALWAYS_SHOW_UI = 0x80,

    /// <summary>
    /// Credential providers should enumerate only administrators. This value is intended for User Account Control (UAC) purposes only. We recommend that external callers not set this flag.
    /// </summary>
    CREDUIWIN_ENUMERATE_ADMINS = 0x100,

    /// <summary>
    /// Only the incoming credentials for the authentication package specified by the authPackage parameter should be enumerated.
    /// </summary>
    CREDUIWIN_ENUMERATE_CURRENT_USER = 0x200,

    /// <summary>
    /// Validates the username agains the available local/domain credentials.
    /// </summary>
    VALIDATE_USERNAME = 0x400,

    /// <summary>
    /// Allows for the completion of the user name.
    /// </summary>
    COMPLETE_USERNAME = 0x800,

    /// <summary>
    /// The credential dialog box should be displayed on the secure desktop. This value cannot be combined with CREDUIWIN_GENERIC.
    /// Windows Vista: This value is not supported until Windows Vista with SP1.
    /// </summary>
    CREDUIWIN_SECURE_PROMPT = 0x1000,

    /// <summary>
    /// The credential dialog box prompts for a server credential.
    /// </summary>
    SERVER_CREDENTIAL = 0x4000,

    /// <summary>
    /// The credential dialog box should allow for password confirmation.
    /// </summary>
    EXPECT_CONFIRMATION = 0x20000,

    /// <summary>
    /// The Credential dialog box prompts for generic credentials.
    /// </summary>
    GENERIC_CREDENTIALS = 0x40000,

    USERNAME_TARGET_CREDENTIALS = 0x80000,

    /// <summary>
    /// The credential provider should align the credential BLOB pointed to by the refOutAuthBuffer parameter to a 32-bit boundary, even if the provider is running on a 64-bit system.
    /// </summary>
    CREDUIWIN_PACK_32_WOW = 0x10000000,
}