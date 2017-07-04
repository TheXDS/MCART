//
//  UITools.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using MCART.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace MCART.UI
{
    /// <summary>
    /// Contiene varias herramientas de UI para utilizar en proyectos de
    /// Windows Presentation Framework.
    /// </summary>
    public static class UITools
    {
        /// <summary>
        /// Contiene información de los controles modificados por la función
        /// <see cref="Warn(Control, string)"/>.
        /// </summary>
        private struct OrigControlColor
        {
            /// <summary>
            /// Color primario original.
            /// </summary>
            internal System.Windows.Media.Brush fore;
            /// <summary>
            /// Color de fondo original.
            /// </summary>
            internal System.Windows.Media.Brush bacg;
            /// <summary>
            /// Referencia del control al cual se aplica.
            /// </summary>
            internal Control rf;
            /// <summary>
            /// <see cref="ToolTip"/> original del control.
            /// </summary>
            internal ToolTip ttip;
        }
        /// <summary>
        /// Lista privada de estados de los controles modificados por la función
        /// 
        /// </summary>
        static List<OrigControlColor> origctrls = new List<OrigControlColor>();
        /// <summary>
        /// Lista privada de <see cref="BitmapEncoder"/> cargados en el
        /// <see cref="AppDomain"/> actual.
        /// </summary>
        static List<BitmapEncoder> bEncLst;
        /// <summary>
        /// Devuelve una colección de los códecs de mapas de bits disponibles.
        /// Soporta cargar códecs desde cualquier ensamblado cargado.
        /// </summary>
        /// <returns>
        /// Una lista con una nueva instancia de todos los códecs de mapa de
        /// bits disponibles.
        /// </returns>
        public static IReadOnlyCollection<BitmapEncoder> GetBitmapEncoders()
        {
            if (!(bool)bEncLst?.Any())
            {
                bEncLst = new List<BitmapEncoder>();
                Type t = typeof(BitmapEncoder);
                foreach (Assembly j in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type k in j.GetTypes().Where(
                        (x) => t.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface))
                        bEncLst.Add(k.New<BitmapEncoder>());
                }
            }
            return bEncLst.AsReadOnly();
        }
        /// <summary>
        /// Limpia la lista en caché de los <see cref="BitmapEncoder"/>
        /// cargados desde el <see cref="AppDomain"/> actual.
        /// </summary>
        public static void FlushBitmapEncoders()
        {
            bEncLst?.Clear();
            bEncLst = null;
            GC.Collect();
        }
        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement"/>.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement"/> a renderizar.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap"/> que contiene una imagen
        /// renderizada de <paramref name="f"/>.
        /// </returns>
        public static RenderTargetBitmap Render(this FrameworkElement f)
        {
            System.Drawing.Point dpi = GetDpi();
            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)f.ActualWidth,
                (int)f.ActualHeight,
                dpi.X, dpi.Y,
                PixelFormats.Pbgra32);
            bmp.Render(f);
            return bmp;
        }
        /// <summary>
        /// Genera un arco de círculo que puede usarse en Windows Presentation
        /// Framework.
        /// </summary>
        /// <param name="radius">Radio del arco a generar.</param>
        /// <param name="angle">Ángulo, o tamaño del arco.</param>
        /// <param name="thickness">
        /// Grosor del trazo del arco. Ayuda a balancear el grosor del trazo y
        /// el radio para lograr un tamaño más uniforme.
        /// </param>
        /// <returns>
        /// Un <see cref="PathGeometry"/> que contiene el arco generado por
        /// esta función.
        /// </returns>
        public static PathGeometry GetCircleArc(double radius, double angle, double thickness = 0)
        {
            System.Windows.Point cp = new System.Windows.Point(radius + (thickness / 2), radius + (thickness / 2));
            ArcSegment arc = new ArcSegment()
            {
                IsLargeArc = angle > 180.0,
                Point = new System.Windows.Point(
                    cp.X + System.Math.Sin(Math.Deg_Rad * angle) * radius,
                    cp.Y - System.Math.Cos(Math.Deg_Rad * angle) * radius),
                Size = new System.Windows.Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise
            };
            PathFigure pth = new PathFigure()
            {
                StartPoint = new System.Windows.Point(radius + (thickness / 2), thickness / 2),
                IsClosed = false
            };
            pth.Segments.Add(arc);
            PathGeometry outp = new PathGeometry();
            outp.Figures.Add(pth);
            return outp;
        }
        /// <summary>
        /// Establece un estado de error para un control.
        /// </summary>
        /// <param name="c">Control a advertir.</param>
        /// <param name="ttip">
        /// <see cref="ToolTip"/> con un mensaje de error para mostrar.
        /// </param>
        public static void Warn(this Control c, string ttip = null)
        {
            origctrls.Add(new OrigControlColor
            {
                rf = c,
                fore = c.Foreground,
                bacg = c.Background,
                ttip = (ToolTip)c.ToolTip
            });
            c.Background = new SolidColorBrush(Colors.Pink);
            c.Foreground = new SolidColorBrush(Colors.DarkRed);
            if (!string.IsNullOrEmpty(ttip)) c.ToolTip = new ToolTip() { Content = ttip };            
        }
        /// <summary>
        /// Quita el estado de error de un control.
        /// </summary>
        /// <param name="c">Control a limpiar.</param>
        public static void ClearWarn(this Control c)
        {
            OrigControlColor j;
            for (int k = 0; k < origctrls.Count; k++)
            {
                j = origctrls[k];
                if (j.rf.Is(c))
                {
                    c.Foreground = j.fore;
                    c.Background = j.bacg;
                    c.ToolTip = j.ttip;
                    origctrls.Remove(j);
                    return;
                }
            }
        }
        /// <summary>
        /// Obtiene un valor que determina si el control está advertido.
        /// </summary>
        /// <param name="c">Control a comprobar.</param>
        /// <returns><c>true</c> si el control está mostrando una advertencia;
        /// de lo contrario, <c>false</c>.</returns>
        public static bool IsWarned(this Control c)
        {
            if (c.IsNull()) throw new ArgumentNullException(nameof(c));
            foreach (OrigControlColor j in origctrls) if (j.rf.Is(c)) return true;
            return false;
        }
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Collapsed"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">Arreglo de controles a deshabilitar.</param>
        public static void CollapseControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Hidden"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">Arreglo de controles a deshabilitar.</param>
        public static void HideControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Visible"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">Arreglo de controles a deshabilitar.</param>
        public static void ShowControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Deshabilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">Arreglo de controles a deshabilitar.</param>
        public static void DisableControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.IsEnabled = false;
        }
        /// <summary>
        /// Habilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">Arreglo de controles a habilitar.</param>
        public static void EnableControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.IsEnabled = true;
        }
        /// <summary>
        /// Habilita o deshabilita una lista de controles según su estado previo.
        /// </summary>
        /// <param name="ctrls">Arreglo de controles a habilitar/deshabilitar.</param>
        public static void ToggleControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.IsEnabled = !j.IsEnabled;
        }
        /// <summary>
        /// Inicia una consola para la aplicación.
        /// </summary>
        /// <returns><c>true</c> si la llamada obtuvo correctamente una consola; de lo contrario, <c>false</c>.</returns>
        /// <remarks>
        /// Esta función es exclusiva para sistemas operativos Microsoft
        /// Windows®.
        /// </remarks>
        [DllImport("kernel32.dll")] public static extern bool AllocConsole();
        /// <summary>
        /// Libera la consola de la aplicación.
        /// </summary>
        /// <returns><c>true</c> si la llamada liberó correctamente la consola; de lo contrario, <c>false</c>.</returns>
        /// <remarks>
        /// Esta función es exclusiva para sistemas operativos Microsoft
        /// Windows®.
        /// </remarks>
        [DllImport("kernel32.dll")] public static extern bool FreeConsole();
        /// <summary>
        /// Obtiene la información física del contexto del dispositivo gráfico
        /// especificado.
        /// </summary>
        /// <param name="hdc">Identificador de contexto a verificar.</param>
        /// <param name="nIndex">Propiedad a obtener.</param>
        /// <returns>
        /// Un <see cref="int"/> que representa el valor obtenido.
        /// </returns>
        /// <remarks>
        /// Esta función es exclusiva para sistemas operativos Microsoft
        /// Windows®.
        /// </remarks>
        [DllImport("gdi32.dll")] public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        /// <summary>
        /// Obtiene el factor de escala de la interfaz gráfica.
        /// </summary>
        /// <returns>
        /// Un valor <see cref="float"/> que representa el factor de escala
        /// utilizado para dibujar la interfaz gráfica del sistema.
        /// </returns>
        [Thunk] public static float GetScalingFactor() => GetScalingFactor(IntPtr.Zero);
        /// <summary>
        /// Obtiene el factor de escala de la ventana especificada por
        /// <paramref name="Hwnd"/>.
        /// </summary>
        /// <param name="Hwnd">Identificador de ventana a verificar.</param>
        /// <returns>
        /// Un valor <see cref="float"/> que representa el factor de escala
        /// utilizado para dibujar la ventana especificada por 
        /// <paramref name="Hwnd"/>.
        /// </returns>
        public static float GetScalingFactor(IntPtr Hwnd)
        {
            IntPtr h = Graphics.FromHwnd(Hwnd).GetHdc();
            return (float)GetDeviceCaps(h, 10) / GetDeviceCaps(h, 117);
        }
        /// <summary>
        /// Obtiene la resolución horizontal de la pantalla en DPI.
        /// </summary>
        /// <returns>
        /// Un valor entero que indica la resolución horizontal de la pantalla 
        /// en Puntos Por Pulgada (DPI).
        /// </returns>
        [Thunk] public static int GetXDpi() => GetXDpi(IntPtr.Zero);
        /// <summary>
        /// Obtiene la resolución vertical de la pantalla en DPI.
        /// </summary>
        /// <returns>
        /// Un valor entero que indica la resolución vertical de la pantalla en
        /// Puntos Por Pulgada (DPI).
        /// </returns>
        [Thunk] public static int GetYDpi() => GetXDpi(IntPtr.Zero);
        /// <summary>
        /// Obtiene la resolución horizontal de la ventana en DPI.
        /// </summary>
        /// <param name="Hwnd">Identificador de ventana a verificar.</param>
        /// <returns>
        /// Un valor entero que indica la resolución horizontal de la ventana 
        /// en Puntos Por Pulgada (DPI).
        /// </returns>
        public static int GetXDpi(IntPtr Hwnd) => GetDeviceCaps(Graphics.FromHwnd(Hwnd).GetHdc(), 88);
        /// <summary>
        /// Obtiene la resolución vertical de la ventana en DPI.
        /// </summary>
        /// <param name="Hwnd">Identificador de ventana a verificar.</param>
        /// <returns>
        /// Un valor entero que indica la resolución vertical de la ventana en
        /// Puntos Por Pulgada (DPI).
        /// </returns>
        public static int GetYDpi(IntPtr Hwnd) => GetDeviceCaps(Graphics.FromHwnd(Hwnd).GetHdc(), 90);
        /// <summary>
        /// Obtiene las resolución horizontal y vertical de la ventana en DPI.
        /// </summary>
        /// <param name="Hwnd">Identificador de ventana a verificar.</param>
        /// <returns>
        /// Un <see cref="System.Drawing.Point"/> que indica la resolución de
        /// la ventana en Puntos Por Pulgada (DPI).
        /// </returns>
        public static System.Drawing.Point GetDpi(IntPtr Hwnd)
        {
            IntPtr h = Graphics.FromHwnd(Hwnd).GetHdc();
            return new System.Drawing.Point(GetDeviceCaps(h, 88), GetDeviceCaps(h, 90));
        }
        /// <summary>
        /// Obtiene las resolución horizontal y vertical de la pantalla en DPI.
        /// </summary>
        /// <returns>
        /// Un <see cref="System.Drawing.Point"/> que indica la resolución de
        /// la pantalla en Puntos Por Pulgada (DPI).
        /// </returns>
        [Thunk] public static System.Drawing.Point GetDpi() => GetDpi(IntPtr.Zero);
        /// <summary>
        /// Mezcla un color de temperatura basado en el porcentaje.
        /// </summary>
        /// <returns>El color qaue representa la temperatura del porcentaje.</returns>
        /// <param name="x">Valor porcentual utilizado para calcular la temperatura.</param>
        public static System.Windows.Media.Color BlendHeatColor(double x)
        {
            byte r = (byte)(1020 * (x + 0.5) - 1020).Clamp(255, 0);
            byte g = (byte)((-System.Math.Abs(2040 * (x - 0.5)) + 1020) / 2).Clamp(255, 0);
            byte b = (byte)(-1020 * (x + 0.5) + 1020).Clamp(255, 0);
            return System.Windows.Media.Color.FromRgb(r, g, b);
        }
        /// <summary>
        /// Mezcla un color de salud basado en el porcentaje.
        /// </summary>
        /// <returns>El color qaue representa la salud del porcentaje.</returns>
        /// <param name="x">The x coordinate.</param>
        public static System.Windows.Media.Color BlendHealthColor(double x)
        {
            byte g = (byte)(510 * x).Clamp(255, 0);
            byte r = (byte)(510 - (510 * x)).Clamp(255, 0);
            return System.Windows.Media.Color.FromRgb(r, g, 0);
        }
    }
}