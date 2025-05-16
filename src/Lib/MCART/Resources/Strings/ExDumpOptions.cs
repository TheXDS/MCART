/*
ExDumpOptions.cs

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

namespace TheXDS.MCART.Resources.Strings;

/// <summary>
/// Specifies different text formatting options for generating a
/// description of an exception.
/// </summary>
[Flags]
public enum ExDumpOptions : byte
{
    /// <summary>
    /// Includes the exception name.
    /// </summary>
    Name = 1,

    /// <summary>
    /// Includes the description of the location where the exception
    /// occurred.
    /// </summary>
    Source = 2,

    /// <summary>
    /// Includes the exception error message.
    /// </summary>
    Message = 4,

    /// <summary>
    /// Includes the exception HResult.
    /// </summary>
    HResult = 8,

    /// <summary>
    /// Includes an exception stack trace.
    /// </summary>
    StackTrace = 16,

    /// <summary>
    /// Includes any property containing inner exceptions.
    /// </summary>
    Inner = 32,

    /// <summary>
    /// Includes a list of assemblies loaded in the AppDomain where the
    /// exception occurred.
    /// </summary>
    LoadedAssemblies = 64,

    /// <summary>
    /// Formats the text to a predefined width.
    /// </summary>
    TextWidthFormatted = 128,

    /// <summary>
    /// Shows all exception information and any inner exceptions.
    /// </summary>
    All = 127,

    /// <summary>
    /// Shows all exception information and any inner exceptions,
    /// formatting the text to a predefined width.
    /// </summary>
    AllFormatted = 255,
}
