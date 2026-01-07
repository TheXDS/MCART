/*
Kernel32.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Runtime.InteropServices;
using System.Text;

namespace TheXDS.MCART.PInvoke;

internal partial class Kernel32
{
    private const string Kernel32Dll = "kernel32.dll";

#if NET7_0_OR_GREATER

    [LibraryImport(Kernel32Dll)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool AllocConsole();

    [LibraryImport(Kernel32Dll)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool FreeConsole();

    [LibraryImport(Kernel32Dll)]
    internal static partial IntPtr GetConsoleWindow();

    [LibraryImport(Kernel32Dll)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetFirmwareType(ref uint FirmwareType);

#else

    [DllImport(Kernel32Dll)]
    internal static extern bool AllocConsole();

    [DllImport(Kernel32Dll)]
    internal static extern bool FreeConsole();

    [DllImport(Kernel32Dll)]
    internal static extern IntPtr GetConsoleWindow();

    [DllImport(Kernel32Dll)]
    internal static extern bool GetFirmwareType(ref uint FirmwareType);

#endif

    [DllImport(Kernel32Dll, CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder? packageFullName);
}
