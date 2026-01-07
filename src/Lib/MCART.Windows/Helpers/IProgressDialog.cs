// IProgressDialog.cs
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

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Define a series of members to be implemented by a type that provides
/// functionality for reporting progress of an operation in a native
/// Microsoft Windows dialog.
/// </summary>
public interface IProgressDialog
{
    /// <summary>
    /// Gets or sets the dialog's title.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the maximum value displayed in the progress bar.
    /// </summary>
    int Maximum { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether text lines should be compressed
    /// if they exceed the available width.
    /// </summary>
    bool CompactPaths { get; set; }

    /// <summary>
    /// Gets or sets the message displayed when the operation in progress is
    /// canceled.
    /// </summary>
    string CancelMessage { get; set; }

    /// <summary>
    /// Gets or sets the current progress value of the dialog.
    /// </summary>
    int Value { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog should automatically
    /// close upon reaching 100% progress.
    /// </summary>
    bool AutoClose { get; set; }

    /// <summary>
    /// Gets or sets the value of the third line of text in the dialog.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to set this line's value while the dialog
    /// displays remaining time on the third line.
    /// </exception>
    string Line3 { get; set; }

    /// <summary>
    /// Gets or sets the value of the second line of text in the dialog.
    /// </summary>
    string Line2 { get; set; }

    /// <summary>
    /// Gets or sets the value of the first line of text in the dialog.
    /// </summary>
    string Line1 { get; set; }

    /// <summary>
    /// Gets a value indicating whether the user has requested cancellation of
    /// the current operation.
    /// </summary>
    bool HasUserCancelled { get; }

    /// <summary>
    /// Closes the progress dialog.
    /// </summary>
    void Close();

    /// <summary>
    /// Indicates that the operation has been paused.
    /// </summary>
    void Pause();

    /// <summary>
    /// Indicates that a previously paused operation will continue.
    /// </summary>
    void Resume();
}