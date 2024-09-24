/*
CredUi.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using TheXDS.MCART.PInvoke.Structs;

namespace TheXDS.MCART.PInvoke;

[ExcludeFromCodeCoverage]
internal partial class CredUi
{
    [DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool CredUnPackAuthenticationBuffer(
        int dwFlags,
        IntPtr pAuthBuffer,
        uint cbAuthBuffer,
        StringBuilder pszUserName,
        ref int pcchMaxUserName,
        StringBuilder pszDomainName,
        ref int pcchMaxDomainame,
        StringBuilder pszPassword,
        ref int pcchMaxPassword);

    [DllImport("credui.dll", CharSet = CharSet.Unicode, EntryPoint = "CredUIPromptForWindowsCredentialsW")]
    internal static extern CredUIReturnCodes CredUIPromptForWindowsCredentials(
        ref CreduiInfo credui,
        int authError,
        ref uint authPackage,
        IntPtr InAuthBuffer,
        uint InAuthBufferSize,
        out IntPtr refOutAuthBuffer,
        out uint refOutAuthBufferSize,
        [MarshalAs(UnmanagedType.Bool)] ref bool fSave,
        PromptForWindowsCredentialsFlags flags);

    [LibraryImport("credui.dll", EntryPoint = "CredPackAuthenticationBufferW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool CredPackAuthenticationBuffer(
      uint dwFlags,
      string pszUserName,
      string pszPassword,
      IntPtr pPackedCredentials,
      ref uint pcbPackedCredentials);
}
