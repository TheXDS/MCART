// ProgressDialog.cs
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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.ComInterop;
using TheXDS.MCART.ComInterop.Models;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Windows.Resources.Strings;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Clase que permite interactuar con un cuadro de diálogo nativo de Microsoft
/// Windows que muestra el progreso de una operación.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class ProgressDialog : IProgressDialog, IDisposable
{
    private static readonly Type progressDialogType = Type.GetTypeFromCLSID(new Guid("{F8383852-FCD3-11d1-A6B9-006097DF5BD4}")) ?? throw new NotSupportedException();

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">
    /// Referencia al propietario del nuevo diálogo.
    /// </param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    public static void Run(nint owner, ProgressDialogProperties properties, Action<IProgressDialog> operationCallback)
    {
        using var dialog = Create(owner, properties);
        operationCallback.Invoke(dialog);        
    }

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">
    /// Referencia al propietario del nuevo diálogo.
    /// </param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// El resultado de la operación.
    /// </returns>
    public static T Run<T>(nint owner, ProgressDialogProperties properties, Func<IProgressDialog, T> operationCallback)
    {
        using var dialog = Create(owner, properties);
        return operationCallback.Invoke(dialog);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">
    /// Referencia al propietario del nuevo diálogo.
    /// </param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task"/> que puede utilizarse para esperar la
    /// completación de la operación asíncrona.
    /// </returns>
    public static Task Run(nint owner, ProgressDialogProperties properties, Func<IProgressDialog, Task> operationCallback)
    {
        return Run<Task>(owner, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">
    /// Referencia al propietario del nuevo diálogo.
    /// </param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task{TResult}"/> que puede utilizarse para esperar
    /// la completación de la operación asíncrona.
    /// </returns>
    public static Task<T> Run<T>(nint owner, ProgressDialogProperties properties, Func<IProgressDialog, Task<T>> operationCallback)
    {
        return Run<Task<T>>(owner, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">Referencia al propietario del nuevo diálogo.</param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método será un objeto que permitirá interactuar con el diálogo nativo de Windows.
    /// </param>
    public static void Run(IMsWindow? owner, ProgressDialogProperties properties, Action<IProgressDialog> operationCallback)
    {
        Run(owner?.Handle ?? Process.GetCurrentProcess().MainWindowHandle, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método será un objeto que permitirá interactuar con el diálogo nativo de Windows.
    /// </param>
    public static void Run(ProgressDialogProperties properties, Action<IProgressDialog> operationCallback)
    {
        Run(null, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="progressDialog">
    /// Referencia al método de operación a ejecutar. El parámetro del método será un objeto que permitirá interactuar con el diálogo nativo de Windows.
    /// </param>
    public static void Run(Action<IProgressDialog> progressDialog)
    {
        Run(null, new ProgressDialogProperties(), progressDialog);
    }

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">Referencia al propietario del nuevo diálogo.</param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método será un objeto que permitirá interactuar con el diálogo nativo de Windows.
    /// </param>
    /// <returns>
    /// El resultado de la operación.
    /// </returns>
    public static T Run<T>(IMsWindow? owner, ProgressDialogProperties properties, Func<IProgressDialog, T> operationCallback)
    {
        return Run(owner?.Handle ?? Process.GetCurrentProcess().MainWindowHandle, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método será un objeto que permitirá interactuar con el diálogo nativo de Windows.
    /// </param>
    /// <returns>
    /// El resultado de la operación.
    /// </returns>
    public static T Run<T>(ProgressDialogProperties properties, Func<IProgressDialog, T> operationCallback)
    {
        return Run(null, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación que reporta su estado utilizando un diálogo de
    /// progreso nativo de Windows.
    /// </summary>
    /// <param name="progressDialog">
    /// Referencia al método de operación a ejecutar. El parámetro del método será un objeto que permitirá interactuar con el diálogo nativo de Windows.
    /// </param>
    /// <returns>
    /// El resultado de la operación.
    /// </returns>
    public static T Run<T>(Func<IProgressDialog, T> progressDialog)
    {
        return Run(null, new ProgressDialogProperties(), progressDialog);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">
    /// Referencia al propietario del nuevo diálogo.
    /// </param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task"/> que puede utilizarse para esperar la
    /// completación de la operación asíncrona.
    /// </returns>
    public static Task Run(IMsWindow? owner, ProgressDialogProperties properties, Func<IProgressDialog, Task> operationCallback)
    {
        return Run<Task>(owner, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task"/> que puede utilizarse para esperar la
    /// completación de la operación asíncrona.
    /// </returns>
    public static Task Run(ProgressDialogProperties properties, Func<IProgressDialog, Task> operationCallback)
    {
        return Run<Task>(properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task"/> que puede utilizarse para esperar la
    /// completación de la operación asíncrona.
    /// </returns>
    public static Task Run(Func<IProgressDialog, Task> operationCallback)
    {
        return Run<Task>(operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="owner">
    /// Referencia al propietario del nuevo diálogo.
    /// </param>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task{TResult}"/> que puede utilizarse para esperar
    /// la completación de la operación asíncrona.
    /// </returns>
    public static Task<T> Run<T>(IMsWindow? owner, ProgressDialogProperties properties, Func<IProgressDialog, Task<T>> operationCallback)
    {
        return Run<Task<T>>(owner, properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="properties">
    /// Referencia al método a utilizar para inicializar el nuevo diálogo.
    /// </param>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task{TResult}"/> que puede utilizarse para esperar
    /// la completación de la operación asíncrona.
    /// </returns>
    public static Task<T> Run<T>(ProgressDialogProperties properties, Func<IProgressDialog, Task<T>> operationCallback)
    {
        return Run<Task<T>>(properties, operationCallback);
    }

    /// <summary>
    /// Ejecuta una operación asíncrona que reporta su estado utilizando un
    /// diálogo de progreso nativo de Windows.
    /// </summary>
    /// <param name="operationCallback">
    /// Referencia al método de operación a ejecutar. El parámetro del método 
    /// será un objeto que permitirá interactuar con el diálogo nativo de
    /// Windows.
    /// </param>
    /// <returns>
    /// Un objeto <see cref="Task{TResult}"/> que puede utilizarse para esperar
    /// la completación de la operación asíncrona.
    /// </returns>
    public static Task<T> Run<T>(Func<IProgressDialog, Task<T>> operationCallback)
    {
        return Run<Task<T>>(operationCallback);
    }

    private static ProgressDialog Create(nint owner, ProgressDialogProperties properties)
    {
        var dialog = new ProgressDialog(properties)
            .SetFlag(!properties.CancelButton, ProgressDialogFlags.NoCancel)
            .SetFlag(properties.Marquee, ProgressDialogFlags.MarqueeProgress)
            .SetFlag(!properties.MinimizeButton, ProgressDialogFlags.NoMinimize)
            .SetFlag(properties.Modal, ProgressDialogFlags.Modal)
            .SetFlag(!properties.ProgressBar, ProgressDialogFlags.NoProgressBar)
            .SetFlag(properties.ShowTimeRemaining, ProgressDialogFlags.AutoTime)
            .SetFlag(!properties.ShowTimeRemaining, ProgressDialogFlags.NoTime);

        dialog.Show(owner);
        return dialog;
    }

    private IProgressDialogComInterop? _nativeProgressDialog = Activator.CreateInstance(progressDialogType) as IProgressDialogComInterop ?? throw new NotSupportedException();
    private string _cancelMessage = string.Empty;
    private bool _compactPaths = false;
    private string _title = string.Empty;
    private string _line1 = string.Empty;
    private string _line2 = string.Empty;
    private string _line3 = string.Empty;
    private int _maximum = 100;
    private int _value = 0;
    private ProgressDialogFlags _flags = ProgressDialogFlags.Normal | ProgressDialogFlags.AutoTime;
    private ProgressDialogState _state = ProgressDialogState.Stopped;
    private readonly ProgressDialogProperties properties;

    private ProgressDialog(ProgressDialogProperties properties)
    {
        this.properties = properties;
    }

    bool IProgressDialog.HasUserCancelled => _nativeProgressDialog?.HasUserCancelled() ?? false;

    bool IProgressDialog.AutoClose { get; set; } = true;

    string IProgressDialog.CancelMessage
    {
        get => _cancelMessage;
        set => _nativeProgressDialog?.SetCancelMsg(_cancelMessage = value, nint.Zero);
    }

    bool IProgressDialog.CompactPaths
    {
        get => _compactPaths;
        set
        {
            bool diff = _compactPaths != value;
            _compactPaths = value;
            if (!diff || _nativeProgressDialog is null) return;
            _nativeProgressDialog.SetLine(1, _line1, _compactPaths, nint.Zero);
            _nativeProgressDialog.SetLine(2, _line1, _compactPaths, nint.Zero);
            _nativeProgressDialog.SetLine(3, _line1, _compactPaths, nint.Zero);
        }
    }

    string IProgressDialog.Line1
    {
        get => _line1;
        set
        {
            ActiveDialogContract();
            _nativeProgressDialog?.SetLine(1, _line1 = value, _compactPaths, nint.Zero);
        }
    }

    string IProgressDialog.Line2
    {
        get => _line2;
        set
        {
            ActiveDialogContract();
            _nativeProgressDialog?.SetLine(2, _line2 = value, _compactPaths, nint.Zero);
        }
    }

    string IProgressDialog.Line3
    {
        get => _line3;
        set
        {
            ActiveDialogContract();
            if (_nativeProgressDialog is null) return;
            if (properties.ShowTimeRemaining)
            {
                throw new InvalidOperationException(WinErrors.CannotChangeLine3);
            }
            _nativeProgressDialog.SetLine(3, _line3 = value, _compactPaths, nint.Zero);
        }
    }

    int IProgressDialog.Maximum
    {
        get => _maximum;
        set
        {
            _maximum = value;
            if (_state != ProgressDialogState.Stopped) UpdateProgress();
        }
    }

    string IProgressDialog.Title
    {
        get => _title;
        set => _nativeProgressDialog?.SetTitle(_title = value);
    }

    int IProgressDialog.Value
    {
        get => _value;
        set
        {
            _value = value;
            if (_state != ProgressDialogState.Stopped)
            {
                UpdateProgress();
                if (((IProgressDialog)this).AutoClose && _value >= _maximum) ((IProgressDialog)this).Close();
            }
        }
    }

    void IProgressDialog.Pause()
    {
        switch (_state)
        {
            case ProgressDialogState.Paused: break;
            case ProgressDialogState.Running:
                _nativeProgressDialog?.Timer(ProgressDialogTimer.Pause, nint.Zero);
                _state = ProgressDialogState.Paused;
                break;
            default: throw Errors.Tamper();
        }
    }

    void IProgressDialog.Resume()
    {
        switch (_state)
        {
            case ProgressDialogState.Paused:
                _nativeProgressDialog?.Timer(ProgressDialogTimer.Resume, nint.Zero);
                _state = ProgressDialogState.Running;
                break;
            case ProgressDialogState.Running: break;
            default: throw Errors.Tamper();
        }
    }

    void IProgressDialog.Close()
    {
        if (_state != ProgressDialogState.Stopped)
        {
            _nativeProgressDialog?.StopProgressDialog();
            _state = ProgressDialogState.Stopped;
        }
        Cleanup();
    }

    void IDisposable.Dispose()
    {
        Cleanup();
    }

    private void UpdateProgress()
    {
        _nativeProgressDialog?.SetProgress((uint)_value, (uint)_maximum);
    }

    private void Show(nint owner)
    {
        _nativeProgressDialog!.StartProgressDialog(owner, nint.Zero, _flags, nint.Zero);
        _state = ProgressDialogState.Running;
    }

    private void Cleanup()
    {
        if (_nativeProgressDialog != null)
        {
            if (_state != ProgressDialogState.Stopped)
            {
                try { _nativeProgressDialog.StopProgressDialog(); } catch { }
            }

            _nativeProgressDialog = null;
        }

        _state = ProgressDialogState.Stopped;
    }

    private void ActiveDialogContract()
    {
        //if (_state == ProgressDialogState.Stopped) throw Errors.Tamper();
    }

    private ProgressDialog SetFlag(bool value, ProgressDialogFlags flags)
    {
        if (value) _flags |= flags;
        else _flags &= ~flags;
        return this;
    }
}
