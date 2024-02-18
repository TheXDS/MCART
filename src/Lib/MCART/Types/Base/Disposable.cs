/*
Disposable.cs

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

using System.Runtime.CompilerServices;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Clase base que simplifica la implementación de la interfaz
/// <see cref="IDisposable"/>.
/// </summary>
/// <remarks>
/// Si la clase a implementar contendrá acciones asíncronas de limpieza,
/// utilice la clase <see cref="AsyncDisposable"/> como clase base.
/// </remarks>
public abstract class Disposable : IDisposableEx
{
    /// <summary>
    /// Destruye esta instancia de la clase <see cref="Disposable"/>.
    /// </summary>
    ~Disposable()
    {
        if (ShouldFinalize()) Dispose(false);
    }

    /// <summary>
    /// Obtiene un valor que indica si este objeto ha sido desechado.
    /// </summary>
    public bool IsDisposed { get; private set; } = false;

    private bool ShouldFinalize() => GetType().GetMethod(nameof(OnFinalize), Instance | NonPublic)!.IsOverride();

    /// <summary>
    /// Libera los recursos utilizados por esta instancia.
    /// </summary>
    /// <param name="disposing">
    /// Indica si deben liberarse los recursos administrados.
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
    /// Realiza operaciones de limpieza para objetos no administrados.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void OnFinalize() { }

    /// <summary>
    /// Realiza las operaciones de limpieza de objetos administrados
    /// desechables de esta instancia.
    /// </summary>
    protected abstract void OnDispose();

    /// <summary>
    /// Libera los recursos utilizados por esta instancia.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        if (ShouldFinalize()) GC.SuppressFinalize(this);
    }
}
