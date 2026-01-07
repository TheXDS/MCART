/*
Disposable.cs

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

using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class that simplifies the implementation of the 
/// <see cref="IDisposable"/> interface.
/// </summary>
/// <remarks>
/// If the class to implement contains asynchronous disposal 
/// actions, use the <see cref="AsyncDisposable"/> class as 
/// the base class.
/// </remarks>
public abstract class Disposable : IDisposableEx
{
    /// <summary>
    /// Gets a value indicating whether this object has been 
    /// disposed.
    /// </summary>
    public bool IsDisposed { get; private set; } = false;

    /// <summary>
    /// Releases the resources used by this instance.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether managed resources should be released.
    /// </param>
    protected void Dispose(bool disposing)
    {
        if (IsDisposed) return;
        if (disposing)
        {
            OnDispose();
        }
        OnFinalize();
        IsDisposed = true;
    }

    /// <summary>
    /// Performs cleanup operations for unmanaged objects.
    /// </summary>
    protected virtual void OnFinalize() { }

    /// <summary>
    /// Performs cleanup operations for disposable managed objects 
    /// for this instance.
    /// </summary>
    protected abstract void OnDispose();

    /// <summary>
    /// Releases the resources used by this instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        if (ShouldFinalize()) GC.SuppressFinalize(this);
    }

    private bool ShouldFinalize()
    {
        return ReflectionHelpers.GetMethod<Action>(() => OnFinalize)
            .IsOverride();
    }

    /// <summary>
    /// Destroys this instance of the <see cref="Disposable"/> 
    /// class.
    /// </summary>
    ~Disposable()
    {
        if (ShouldFinalize()) Dispose(false);
    }
}
