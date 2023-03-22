/*
TimerEx.cs

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

using System.Timers;
using TheXDS.MCART.Types.Base;
using S = System.Timers;

namespace TheXDS.MCART.Types;

/// <summary>
/// Extensión de la clase <see cref="TimerEx" />. provee de toda
/// la funcionalidad previamente disponible, e incluye algunas extensiones
/// útiles.
/// </summary>
public class TimerEx : S.Timer, IDisposableEx
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="TimerEx" />.
    /// </summary>
    public TimerEx()
    {
        Elapsed += Tmr_Elapsed;
        Disposed += TimerEx_Disposed;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="TimerEx" /> y 
    /// establece la propiedad <see cref="S.Timer.Interval" />
    /// en el número de milisegundos especificado.
    /// </summary>
    /// <param name="interval">
    /// Tiempo, en milisegundos, entre eventos. Este valor debe ser mayor
    /// que cero y menor que <see cref="F:System.Int32.MaxValue" />.
    /// </param>
    public TimerEx(double interval) : base(interval) { }

    /// <summary>
    /// Obtiene o establece un valor que indica si este <see cref="TimerEx"/>
    /// debe generar el evento <see cref="S.Timer.Elapsed"/>.
    /// </summary>
    public new bool Enabled
    {
        get => base.Enabled;
        set
        {
            StartTime = value ? DateTime.Now : null;
            base.Enabled = value;
        }
    }

    /// <summary>
    /// Obtiene un valor que indica si este objeto ha sido desechado.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Indica el momento de inicio de este <see cref="TimerEx"/>.
    /// </summary>
    public DateTime? StartTime { get; private set; }

    /// <summary>
    /// Indica la cantidad de tiempo disponible antes de cumplir con el 
    /// intervalo establecido en 
    /// <see cref="S.Timer.Interval"/>.
    /// </summary>
    public TimeSpan? TimeLeft
    {
        get
        {
            if (StartTime is not null) return TimeSpan.FromMilliseconds(Interval) - (DateTime.Now - StartTime);
            return null;
        }
    }

    /// <summary>
    /// Empieza a generar el evento 
    /// <see cref="S.Timer.Elapsed"/> al establecer 
    /// <see cref="Enabled"/> en <see langword="true"/>.
    /// </summary>
    public new void Start()
    {
        StartTime = DateTime.Now;
        base.Start();
    }

    /// <summary>
    /// Deja de generar el evento <see cref="S.Timer.Elapsed"/>
    /// al establecer <see cref="Enabled"/> en <see langword="false"/>.
    /// </summary>
    public new void Stop()
    {
        StartTime = null;
        base.Stop();
    }

    /// <summary>
    /// Reinicia este <see cref="TimerEx"/>.
    /// </summary>
    public void Reset()
    {
        Stop();
        Start();
    }

    private void Tmr_Elapsed(object? sender, ElapsedEventArgs e)
    {
        if (AutoReset) StartTime = DateTime.Now;
    }

    private void TimerEx_Disposed(object? sender, EventArgs e)
    {
        IsDisposed = true;
    }
}
