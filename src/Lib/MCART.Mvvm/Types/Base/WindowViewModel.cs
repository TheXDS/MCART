/*
WindowViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// ViewModel con propiedades básicas de gestión de la ventana.
/// </summary>
public abstract class WindowViewModel : ViewModelBase
{
    private string _title = string.Empty;
    private Size _size;

    /// <summary>
    /// Obtiene o establece el título de la ventana.
    /// </summary>
    public string Title
    {
        get => _title;
        set => Change(ref _title, value);
    }

    /// <summary>
    /// Obtiene o establece la altura deseada de la ventana.
    /// </summary>
    public double WindowHeight
    {
        get => _size.Height;
        set
        {
            if (_size.Height == value) return;
            _size.Height = value;
            Notify(nameof(WindowHeight), nameof(WindowSize));
        }
    }

    /// <summary>
    /// Obtiene o establece el ancho deseado de la ventana.
    /// </summary>
    public double WindowWidth
    {
        get => _size.Width;
        set
        {
            if (_size.Width == value) return;
            _size.Width = value;
            Notify(nameof(WindowWidth), nameof(WindowSize));
        }
    }

    /// <summary>
    /// Obtiene o establece el tamaño deseado de la ventana.
    /// </summary>
    public Size WindowSize
    {
        get => _size;
        set
        {
            if (Change(ref _size, value))
            {
                Notify(nameof(WindowHeight), nameof(WindowWidth));
            }
        }
    }
}
