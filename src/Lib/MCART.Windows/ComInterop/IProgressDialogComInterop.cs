// IProgressDialogComInterop.cs
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

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using TheXDS.MCART.ComInterop.Models;

namespace TheXDS.MCART.ComInterop;

[GeneratedComInterface]
[Guid("EBBC7C04-315E-11d2-B62F-006097DF5BD4")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IProgressDialogComInterop
{
    void StartProgressDialog(nint hwndParent, nint punkEnableModless, ProgressDialogFlags dwFlags, nint resevered);
    void StopProgressDialog();
    void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
    void SetLine(uint dwLineNum, [MarshalAs(UnmanagedType.LPWStr)] string pszLine, [MarshalAs(UnmanagedType.VariantBool)] bool fCompactMode, nint resevered);
    void SetProgress(uint nCompleted, uint nTotal);
    void SetProgress64(ulong ullCompleted, ulong ullTotal);
    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool HasUserCancelled();
    void SetCancelMsg([MarshalAs(UnmanagedType.LPWStr)] string pszCancelMsg, nint reserved);
    void Timer(ProgressDialogTimer uTimerAction, nint reserved);
    void SetAnimation(nint hInstAnimation, ushort idAnimation);
    void SetTitleEx([MarshalAs(UnmanagedType.LPWStr)] string pszTitle, ProgressDialogFlags dwFlags);
}
