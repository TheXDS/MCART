// ProgressDialogProperties.cs
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
/// Provides information about the properties used when initializing a new
/// native Microsoft Windows progress dialog.
/// </summary>
/// <param name="CancelButton">
/// Indicates whether the dialog should contain a cancel button.
/// </param>
/// <param name="Marquee">
/// Indicates whether the dialog's progress bar should display in an
/// indeterminate marquee state.
/// </param>
/// <param name="MinimizeButton">
/// Indicates whether the dialog should include a minimize button.
/// </param>
/// <param name="Modal">
/// Indicates whether the dialog should be displayed modally.
/// </param>
/// <param name="ProgressBar">
/// Indicates whether the dialog should contain a progress bar.
/// </param>
/// <param name="ShowTimeRemaining">
/// Indicates whether the remaining time of the operation should be shown
/// in the dialog's third line.
/// </param>
[ExcludeFromCodeCoverage]
public readonly record struct ProgressDialogProperties(
    bool CancelButton,
    bool Marquee,
    bool MinimizeButton,
    bool Modal,
    bool ProgressBar = true,
    bool ShowTimeRemaining = true);