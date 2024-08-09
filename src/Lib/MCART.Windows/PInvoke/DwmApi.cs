/*
DwmApi.cs

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
using TheXDS.MCART.PInvoke.Structs;

namespace TheXDS.MCART.PInvoke;

[ExcludeFromCodeCoverage]
internal partial class DwmApi
{
#pragma warning disable SYSLIB1054
    [DllImport("dwmapi.dll", SetLastError = false, ExactSpelling = true)]
    internal static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, in DWM_BLURBEHIND pDwmBlurbehind);
#pragma warning restore SYSLIB1054

#if NET7_0_OR_GREATER

    [LibraryImport("dwmapi.dll")]
    internal static partial int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);

    [LibraryImport("dwmapi.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool DwmIsCompositionEnabled();

    [LibraryImport("dwmapi.dll")]
    internal static partial int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, [MarshalAs(UnmanagedType.Bool)] out bool pvAttribute, int cbAttribute);

    [LibraryImport("dwmapi.dll")] 
    internal static partial int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref IntPtr pvAttribute, uint cbAttribute);

    [LibraryImport("dwmapi.dll")]
    internal static partial int DwmGetColorizationColor(out uint pcrColorization, [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

#else

    [DllImport("dwmapi.dll")]
    internal static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);

    [DllImport("dwmapi.dll", PreserveSig = false)]
    internal static extern bool DwmIsCompositionEnabled();

    [DllImport("dwmapi.dll")]
    internal static extern int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, out bool pvAttribute, int cbAttribute);

    [DllImport("dwmapi.dll")]
    internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref uint pvAttribute, int cbAttribute);

    [DllImport("dwmapi.dll")]
    internal static extern int DwmGetColorizationColor(out uint pcrColorization, [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

#endif
}
