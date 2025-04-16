/*
WindowChanges.cs

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
internal enum WindowChanges : uint
{
    IgnoreResize = 0x0001,
    IgnoreMove = 0x0002,
    IgnoreZOrder = 0x0004,
    DoNotRedraw = 0x0008,
    DoNotActivate = 0x0010,
    DrawFrame = 0x0020,
    FrameChanged = DrawFrame,
    ShowWindow = 0x0040,
    HideWindow = 0x0080,
    DoNotCopyBits = 0x0100,
    DoNotChangeOwnerZOrder = 0x0200,
    DoNotReposition = DoNotChangeOwnerZOrder,
    DoNotSendChangingEvent = 0x0400,
    DeferErase = 0x2000,
    AsynchronousWindowPosition = 0x4000,
}
