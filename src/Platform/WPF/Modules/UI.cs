/*
UITools.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#nullable enable
#pragma warning disable CA1401 // P/Invokes should not be visible

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Attributes;
using C = TheXDS.MCART.Math.Geometry;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using static TheXDS.MCART.Types.Extensions.WpfColorExtensions;
using TheXDS.MCART.Types.Base;

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

// ReSharper disable once CheckNamespace
namespace TheXDS.MCART
{
    /// <summary>
    /// Contiene varias herramientas de UI para utilizar en proyectos de
    /// Windows Presentation Framework.
    /// </summary>
    public static class WpfUi
    {
        private enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }

        private enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        }

        /// <summary>
        /// Estructura de control de colores originales.
        /// </summary>
        private struct OrigControlColor
        {
            /// <summary>
            /// Color primario original.
            /// </summary>
            internal Brush _fore;

            /// <summary>
            /// Color de fondo original.
            /// </summary>
            internal Brush _bacg;

            /// <summary>
            /// Referencia del control al cual se aplica.
            /// </summary>
            internal Control _rf;

            /// <summary>
            /// <see cref="ToolTip" /> original del control.
            /// </summary>
            internal ToolTip _ttip;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Margins
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        private enum WindowData
        {
            GWL_WNDPROC = -4,
            GWL_HINSTANCE = -6,
            GWL_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_EXSTYLE = -20,
            GWL_USERDATA = -21
        }

        [Flags]
        private enum WindowStyles : uint
        {
            WS_VSCROLL = 0x00200000u,
            WS_VISIBLE = 0x10000000u,
            WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_TILED = 0x00000000u,
            WS_TABSTOP = 0x00010000u,
            WS_SYSMENU = 0x00080000u,
            WS_SIZEBOX = 0x00040000u,
            WS_THICKFRAME = 0x00040000u,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

            WS_OVERLAPPEDWINDOW =
                WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUP = 0x80000000u,
            WS_OVERLAPPED = 0x00000000u,
            WS_MINIMIZE = 0x20000000u,
            WS_MAXIMIZE = 0x01000000u,
            WS_ICONIC = 0x20000000u,
            WS_HSCROLL = 0x00100000u,
            WS_GROUP = 0x00020000u,
            WS_DLGFRAME = 0x00400000u,
            WS_DISABLED = 0x08000000u,
            WS_CLIPSIBLINGS = 0x04000000u,
            WS_CLIPCHILDREN = 0x02000000u,
            WS_CHILDWINDOW = 0x40000000u,
            WS_CHILD = 0x40000000u,
            WS_CAPTION = 0x00C00000u,
            WS_BORDER = 0x00800000u,
            WS_MINIMIZEBOX = 0x00020000u,
            WS_MAXIMIZEBOX = 0x00010000u,

            WS_EX_ACCEPTFILES = 0x00000010u,
            WS_EX_APPWINDOW = 0x00040000u,
            WS_EX_CLIENTEDGE = 0x00000200u,
            WS_EX_COMPOSITED = 0x02000000u,
            WS_EX_CONTEXTHELP = 0x00000400u,
            WS_EX_CONTROLPARENT = 0x00010000u,
            WS_EX_DLGMODALFRAME = 0x00000001u,
            WS_EX_LAYERED = 0x00080000u,
            WS_EX_LAYOUTRTL = 0x00400000u,
            WS_EX_LEFT = 0x00000000u,
            WS_EX_LEFTSCROLLBAR = 0x00004000u,
            WS_EX_LTRREADING = 0x00000000u,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_NOACTIVATE = 0x08000000,
            WS_EX_NOINHERITLAYOUT = 0x00100000u,
            WS_EX_NOPARENTNOTIFY = 0x00000004u,
            WS_EX_NOREDIRECTIONBITMAP = 0x00200000u,
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
            WS_EX_RIGHT = 0x00001000u,
            WS_EX_RIGHTSCROLLBAR = 0x00000000u,
            WS_EX_RTLREADING = 0x00002000u,
            WS_EX_STATICEDGE = 0x00020000u,
            WS_EX_TOOLWINDOW = 0x00000080u,
            WS_EX_TOPMOST = 0x00000008u,
            WS_EX_TRANSPARENT = 0x00000020u,
            WS_EX_WINDOWEDGE = 0x00000100u
        }

        [Flags]
        private enum WindowChanges : uint
        {
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_FRAMECHANGED = 0x0020
        }

        private enum SysCommand
        {
            SC_CONTEXTHELP = 0xF180
        }

        private static readonly List<OrigControlColor> _origctrls = new List<OrigControlColor>();
        private static readonly List<StreamUriParser> _uriParsers = Objects.FindAllObjects<StreamUriParser>().ToList();

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="dp">Propiedad de dependencia a enlazar.</param>
        /// <param name="source">Orígen del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty dp, DependencyObject source)
        {
            Bind(obj, dp, (object) source, dp, BindingMode.TwoWay);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="dp">Propiedad de dependencia a enlazar.</param>
        /// <param name="source">Orígen del enlace.</param>
        /// <param name="mode">Modo del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty dp, DependencyObject source,
            BindingMode mode)
        {
            Bind(obj, dp, (object) source, dp, mode);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un
        /// <see cref="INotifyPropertyChanged" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="dp">Propiedad de dependencia a enlazar.</param>
        /// <param name="source">Orígen del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty dp, INotifyPropertyChanged source)
        {
            Bind(obj, dp, (object) source, dp, BindingMode.TwoWay);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="INotifyPropertyChanged" /> a un
        /// <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="dp">Propiedad de dependencia a enlazar.</param>
        /// <param name="source">Orígen del enlace.</param>
        /// <param name="mode">Modo del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty dp, INotifyPropertyChanged source,
            BindingMode mode)
        {
            Bind(obj, dp, (object) source, dp, mode);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="source">Orígen del enlace.</param>
        /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, DependencyObject source,
            DependencyProperty sourceDp)
        {
            Bind(obj, targetDp, (object) source, sourceDp, BindingMode.TwoWay);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="DependencyObject" /> a un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="source">Orígen del enlace.</param>
        /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="mode">Modo del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, DependencyObject source,
            DependencyProperty sourceDp, BindingMode mode)
        {
            Bind(obj, targetDp, (object) source, sourceDp, mode);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="INotifyPropertyChanged" /> a un
        /// <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="source">Orígen del enlace.</param>
        /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, INotifyPropertyChanged source,
            DependencyProperty sourceDp)
        {
            Bind(obj, targetDp, (object) source, sourceDp, BindingMode.TwoWay);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="INotifyPropertyChanged" /> a un
        /// <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="source">Orígen del enlace.</param>
        /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="mode">Modo del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, INotifyPropertyChanged source,
            DependencyProperty sourceDp, BindingMode mode)
        {
            Bind(obj, targetDp, (object) source, sourceDp, mode);
        }

        /// <summary>
        /// Enlaza una propiedad de dependencia de un <see cref="object" /> a un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="obj">Objeto destino del enlace</param>
        /// <param name="targetDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="source">Orígen del enlace.</param>
        /// <param name="sourceDp">Propiedad de dependencia de destino del enlace.</param>
        /// <param name="mode">Modo del enlace.</param>
        public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, object source,
            DependencyProperty sourceDp, BindingMode mode)
        {
            obj.SetBinding(targetDp, new Binding
            {
                Path = new PropertyPath(sourceDp),
                Mode = mode,
                Source = source
            });
        }

        /// <summary>
        /// Limpia el texto del control.
        /// </summary>
        /// <param name="control">Control a limpiar.</param>
        public static void Clear(this TextBox control)
        {
            control.Text = string.Empty;
        }

        /// <summary>
        /// Quita el estado de error de un control.
        /// </summary>
        /// <param name="c">Control a limpiar.</param>
        public static void ClearWarn(this Control c)
        {
            for (var k = 0; k < _origctrls.Count; k++)
            {
                var j = _origctrls[k];
                if (!j._rf.Is(c)) continue;
                c.Foreground = j._fore;
                c.Background = j._bacg;
                c.ToolTip = j._ttip;
                _origctrls.Remove(j);
                return;
            }
        }

        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility" /> a
        /// <see cref="Visibility.Collapsed" /> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a colapsar.
        /// </param>
        public static void CollapseControls(params UIElement[] ctrls)
        {
            foreach (var j in ctrls) j.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility" /> a
        /// <see cref="Visibility.Collapsed" /> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a colapsar.
        /// </param>
        [Sugar]
        public static void CollapseControls(this IEnumerable<UIElement> ctrls)
        {
            CollapseControls(ctrls.ToArray());
        }

        private static HwndSourceHook CreateHookDelegate(Window window, SysCommand syscommand,
            HandledEventHandler handler)
        {
            return (IntPtr hwnd, int msg, IntPtr param, IntPtr lParam, ref bool handled) =>
            {
                if (msg != 0x0112 || ((int) param & 0xFFF0) != (int) syscommand) return IntPtr.Zero;
                var e = new HandledEventArgs();
                handler?.Invoke(window, e);
                handled = e.Handled;
                return IntPtr.Zero;
            };
        }

        /// <summary>
        /// Deshabilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a deshabilitar.
        /// </param>
        public static void DisableControls(params UIElement[] ctrls)
        {
            foreach (var j in ctrls) j.IsEnabled = false;
        }

        /// <summary>
        /// Deshabilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a deshabilitar.
        /// </param>
        [Sugar]
        public static void DisableControls(this IEnumerable<UIElement> ctrls)
        {
            DisableControls(ctrls.ToArray());
        }

        /// <summary>
        /// Deshabilita todos los efectos de la ventana de WPF.
        /// </summary>
        /// <param name="window">Instancia de ventana a difuminar.</param>
        public static void DisableEffects(this Window window)
        {
            SetWindowEffect(window, new AccentPolicy {AccentState = AccentState.ACCENT_DISABLED});
        }

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);

        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);

        /// <summary>
        /// Establece un valor de marco interno para el recuadro de una
        /// ventana de WPF.
        /// </summary>
        /// <param name="window">
        /// Instancia de ventana para la cual configurar el marco interno.
        /// </param>
        /// <param name="padding">
        /// Grosor de los márgenes del marco interno de la ventana.
        /// </param>
        public static void SetFramePadding(this Window window, Thickness padding)
        {
            window.Padding = padding;
            if (DwmIsCompositionEnabled())
            {
                var margins = new Margins
                {
                    Top = (int)padding.Top,
                    Left = (int)padding.Left,
                    Bottom = (int)padding.Bottom,
                    Right = (int)padding.Right
                };
                DwmExtendFrameIntoClientArea(window.GetHwnd(), ref margins);

            }
        }

        /// <summary>
        /// Obtiene el Handle de la ventana de WPF.
        /// </summary>
        /// <param name="window">
        /// Ventana de la cual obtener el Handle.
        /// </param>
        /// <returns>
        /// Un <see cref="IntPtr"/> que es el Handle de la ventana.
        /// </returns>
        public static IntPtr GetHwnd(this Window window) => new WindowInteropHelper(window).Handle;

        /// <summary>
        /// Comprueba si la composición de ventanas está disponible en el
        /// sistema.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si la composición de ventanas está
        /// disponible, <see langword="false"/> en caso contrario.
        /// </returns>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        /// <summary>
        /// Habilita los efectos de difuminado de Windows 10 en la ventana de WPF.
        /// </summary>
        /// <param name="window">Instancia de ventana a difuminar.</param>
        public static void EnableBlur(this Window window)
        {
            SetWindowEffect(window, new AccentPolicy {AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND});
        }

        /// <summary>
        /// Habilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a habilitar.
        /// </param>
        public static void EnableControls(params UIElement[] ctrls)
        {
            foreach (var j in ctrls) j.IsEnabled = true;
        }

        /// <summary>
        /// Habilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a habilitar.
        /// </param>
        [Sugar]
        public static void EnableControls(this IEnumerable<UIElement> ctrls)
        {
            EnableControls(ctrls.ToArray());
        }

        /// <summary>
        /// Ejecuta una animación de destello en un
        /// <see cref="SolidColorBrush" />.
        /// </summary>
        /// <param name="brush"><see cref="SolidColorBrush" /> a animar.</param>
        /// <param name="flashColor"><see cref="System.Windows.Media.Color" /> del destello.</param>
        public static void Flash(this SolidColorBrush brush, Color flashColor)
        {
            var flash = new ColorAnimation
            {
                From = flashColor,
                To = brush.Color,
                Duration = TimeSpan.FromSeconds(1)
            };
            brush.BeginAnimation(SolidColorBrush.ColorProperty, flash);
        }

        /// <summary>
        /// Obtiene una imagen a partir de un <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">
        /// <see cref="Stream" /> con el contenido de la imagen.
        /// </param>
        /// <returns>
        /// La imagen que ha sido leída desde el <see cref="Stream" />.
        /// </returns>
        public static BitmapImage? GetBitmap(Stream? stream)
        {
            if (stream is null || (stream.CanSeek && stream.Length == 0)) return null;
            try
            {
                var retVal = new BitmapImage();
                retVal.BeginInit();
                stream.Seek(0, SeekOrigin.Begin);
                retVal.StreamSource = stream;
                retVal.EndInit();
                return retVal;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene una imagen a partir de un <see cref="Uri" />.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Stream" /> con el contenido de la imagen.
        /// </param>
        /// <returns>
        /// La imagen que ha sido leída desde el <see cref="Stream" />.
        /// </returns>
        public static BitmapImage? GetBitmap(Uri uri)
        {
            foreach (var j in _uriParsers)
            {
                if (!j.Handles(uri)) continue;
                var s = j.OpenFullTransfer(uri);
                if (s is null) break;
                //using (s)
                //{
                    return GetBitmap(s);
                //}
            }
            return null;
        }

        /// <summary>
        /// Obtiene una imagen a partir de una ruta especificada.
        /// </summary>
        /// <param name="path">
        /// <see cref="Stream" /> con el contenido de la imagen.
        /// </param>
        /// <returns>
        /// La imagen que ha sido leída desde el <see cref="Stream" />.
        /// </returns>
        public static BitmapImage? GetBitmap(string path)
        {
            using var fs = new FileStream(path, FileMode.Open);
            return GetBitmap(fs);
        }

        /// <summary>
        /// Obtiene una imagen a partir de un <see cref="Uri" /> de forma
        /// asíncrona.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Stream" /> con el contenido de la imagen.
        /// </param>
        /// <returns>
        /// La imagen que ha sido leída desde el <see cref="Stream" />.
        /// </returns>
        public static async Task<BitmapImage?> GetBitmapAsync(Uri uri)
        {
            foreach(var j in _uriParsers)
            {
                if (!j.Handles(uri)) continue;
                var s = await j.OpenFullTransferAsync(uri);
                if (s is null) return Render(Resources.WpfIcons.FileMissing, new Size(256,256), 96).ToImage();
                return GetBitmap(s);
            }
            return null;
        }

        /// <summary>
        /// Devuelve una colección de los códecs de mapas de bits disponibles.
        /// Soporta cargar códecs desde cualquier ensamblado cargado.
        /// </summary>
        /// <returns>
        /// Una lista con una nueva instancia de todos los códecs de mapa de
        /// bits disponibles.
        /// </returns>
        public static IEnumerable<BitmapEncoder> GetBitmapEncoders()
        {
            return Objects.GetTypes<BitmapEncoder>(true).Select(p => p.New<BitmapEncoder>());
        }

        /// <summary>
        /// Genera un arco de círculo que puede usarse en Windows Presentation
        /// Framework.
        /// </summary>
        /// <param name="radius">Radio del arco a generar.</param>
        /// <param name="angle">Ángulo, o tamaño del arco.</param>
        /// <param name="thickness">
        /// Grosor del trazo del arco. Ayuda a balancear el grosor del trazo y
        /// el radio para lograr un tamaño más consistente.
        /// </param>
        /// <returns>
        /// Un <see cref="PathGeometry" /> que contiene el arco generado por
        /// esta función.
        /// </returns>
        public static PathGeometry GetCircleArc(double radius, double angle, double thickness = 0)
        {
            var cp = new Point(radius + thickness / 2, radius + thickness / 2);
            var arc = new ArcSegment
            {
                IsLargeArc = angle > 180.0,
                Point = new Point(
                    cp.X + System.Math.Sin(C.DegRad * angle) * radius,
                    cp.Y - System.Math.Cos(C.DegRad * angle) * radius),
                Size = new Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise
            };
            var pth = new PathFigure
            {
                StartPoint = new Point(cp.X, cp.Y - radius),
                IsClosed = false
            };
            pth.Segments.Add(arc);
            var outp = new PathGeometry();
            outp.Figures.Add(pth);
            return outp;
        }

        /// <summary>
        /// Genera un arco de círculo que puede usarse en Windows Presentation
        /// Framework.
        /// </summary>
        /// <param name="radius">Radio del arco a generar.</param>
        /// <param name="startAngle">Ángulo inicial del arco.</param>
        /// <param name="endAngle">Ángulo final del arco.</param>
        /// <param name="thickness">
        /// Grosor del trazo del arco. Ayuda a balancear el grosor del trazo y
        /// el radio para lograr un tamaño más consistente.
        /// </param>
        /// <returns>
        /// Un <see cref="PathGeometry" /> que contiene el arco generado por
        /// esta función.
        /// </returns>
        public static PathGeometry GetCircleArc(double radius, double startAngle, double endAngle, double thickness)
        {
            var cp = new Point(radius + thickness / 2, radius + thickness / 2);
            var arc = new ArcSegment
            {
                IsLargeArc = endAngle - startAngle > 180.0,
                Point = new Point(
                    cp.X + System.Math.Sin(C.DegRad * endAngle) * radius,
                    cp.Y - System.Math.Cos(C.DegRad * endAngle) * radius),
                Size = new Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise
            };
            var pth = new PathFigure
            {
                StartPoint = new Point(
                    cp.X + System.Math.Sin(C.DegRad * startAngle) * radius,
                    cp.Y - System.Math.Cos(C.DegRad * startAngle) * radius),
                IsClosed = false
            };
            pth.Segments.Add(arc);
            var outp = new PathGeometry();
            outp.Figures.Add(pth);
            return outp;
        }

        [DllImport("user32.dll")]
        internal static extern uint GetWindowLong(IntPtr hwnd, int index);

        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility" /> a
        /// <see cref="Visibility.Hidden" /> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a ocultar.
        /// </param>
        public static void HideControls(params UIElement[] ctrls)
        {
            foreach (var j in ctrls) j.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility" /> a
        /// <see cref="Visibility.Hidden" /> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a ocultar.
        /// </param>
        [Sugar]
        public static void HideControls(this IEnumerable<UIElement> ctrls)
        {
            HideControls(ctrls.ToArray());
        }

        /// <summary>
        /// Habilita el botón de ayuda de las ventanas de Windows y conecta
        /// un manejador de eventos al mismo.
        /// </summary>
        /// <param name="window">
        /// Ventana en la cual habilitar el botón de ayuda.
        /// </param>
        /// <param name="handler">
        /// Delegado con la acción a ejecutar al hacer clic en el botón de
        /// ayuda de la ventana.
        /// </param>
        public static void HookHelp(this Window window, HandledEventHandler handler)
        {
            var hwnd = new WindowInteropHelper(window).Handle;

            // Ocultar maximizar y minimizar...
            var styles = GetWindowLong(hwnd, (int) WindowData.GWL_STYLE);
            styles &= 0xFFFFFFFF ^ (uint) (WindowStyles.WS_MINIMIZEBOX | WindowStyles.WS_MAXIMIZEBOX);
            SetWindowLong(hwnd, (int) WindowData.GWL_STYLE, styles);

            // Mostrar ayuda...
            styles = GetWindowLong(hwnd, (int) WindowData.GWL_EXSTYLE);
            styles |= (uint) WindowStyles.WS_EX_CONTEXTHELP;
            SetWindowLong(hwnd, (int) WindowData.GWL_EXSTYLE, styles);

            // Actualizar ventana...
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                (uint) (WindowChanges.SWP_NOMOVE |
                        WindowChanges.SWP_NOSIZE |
                        WindowChanges.SWP_NOZORDER |
                        WindowChanges.SWP_FRAMECHANGED));

            ((HwndSource) PresentationSource.FromVisual(window))?
                .AddHook(CreateHookDelegate(window, SysCommand.SC_CONTEXTHELP, handler));
        }

        /// <summary>
        /// Deshabilita y oculta el botón de cerrar de la ventana de Wpf.
        /// </summary>
        /// <param name="window"></param>
        public static void HideClose(this Window window)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            SetWindowLong(hwnd, (int) WindowData.GWL_STYLE, GetWindowLong(hwnd, (int) WindowData.GWL_STYLE) & (uint) ~WindowStyles.WS_SYSMENU);
        }

        /// <summary>
        /// Obtiene un valor que determina si el control está advertido.
        /// </summary>
        /// <param name="c">Control a comprobar.</param>
        /// <returns>
        /// <see langword="true" /> si el control está mostrando una advertencia;
        /// de lo contrario, <see langword="false" />.
        /// </returns>
        public static bool IsWarned(this Control c)
        {
            if (c is null) throw new ArgumentNullException(nameof(c));
            foreach (var j in _origctrls)
                if (j._rf.Is(c))
                    return true;
            return false;
        }

        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement" /> a renderizar.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
        /// renderizada de <paramref name="f" />.
        /// </returns>
        public static RenderTargetBitmap Render(this FrameworkElement f)
        {
            return Render(f, new Size((int) f.ActualWidth, (int) f.ActualHeight), WinUi.GetDpi().X);
        }

        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement" /> a renderizar.
        /// </param>
        /// <param name="dpi">
        /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
        /// renderizada de <paramref name="f" />.
        /// </returns>
        public static RenderTargetBitmap Render(this FrameworkElement f, int dpi)
        {
            return Render(f, new Size((int) f.ActualWidth, (int) f.ActualHeight), dpi);
        }

        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement" /> a renderizar.
        /// </param>
        /// <param name="size">
        /// Tamaño del canvas en donde se renderizará el control.
        /// </param>
        /// <param name="dpi">
        /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
        /// renderizada de <paramref name="f" />.
        /// </returns>
        public static RenderTargetBitmap Render(this Visual f, Size size, int dpi)
        {
            var bmp = new RenderTargetBitmap(
                (int) size.Width,
                (int) size.Height,
                dpi, dpi,
                PixelFormats.Pbgra32);
            bmp.Render(f);
            return bmp;
        }


        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement" />
        /// estableciendo el tamaño en el cual se dibujará el control, por lo
        /// que no necesita haberse mostrado en la interfaz de usuario.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement" /> a renderizar.
        /// </param>
        /// <param name="inSize">
        /// Tamaño del control a renderizar.
        /// </param>
        /// <param name="outSize">
        /// Tamaño del canvas en donde se renderizará el control.
        /// </param>
        /// <param name="dpi">
        /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
        /// renderizada de <paramref name="f" />.
        /// </returns>
        public static RenderTargetBitmap Render(this FrameworkElement f, Size inSize, Size outSize, int dpi)
        {
            f.Measure(inSize);
            f.Arrange(new Rect(inSize));
            var bmp = new RenderTargetBitmap(
                (int) outSize.Width,
                (int) outSize.Height,
                dpi, dpi,
                PixelFormats.Pbgra32);
            bmp.Render(f);
            return bmp;
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private static void SetWindowEffect(Window window, AccentPolicy accent)
        {
            var windowHelper = new WindowInteropHelper(window);
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            SetWindowCompositionAttribute(windowHelper.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hwnd, int index, uint newStyle);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width,
            int height, uint flags);

        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility" /> a
        /// <see cref="Visibility.Visible" /> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a mostrar.
        /// </param>
        public static void ShowControls(params UIElement[] ctrls)
        {
            foreach (var j in ctrls) j.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility" /> a
        /// <see cref="Visibility.Visible" /> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a mostrar.
        /// </param>
        [Sugar]
        public static void ShowControls(this IEnumerable<UIElement> ctrls)
        {
            ShowControls(ctrls.ToArray());
        }

        /// <summary>
        /// Habilita o deshabilita una lista de controles según su estado
        /// previo.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a habilitar/deshabilitar.
        /// </param>
        public static void ToggleControls(params UIElement[] ctrls)
        {
            foreach (var j in ctrls) j.IsEnabled = !j.IsEnabled;
        }

        /// <summary>
        /// Habilita o deshabilita una lista de controles según su estado
        /// previo.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement" /> a habilitar/deshabilitar.
        /// </param>
        [Sugar]
        public static void ToggleControls(this IEnumerable<UIElement> ctrls)
        {
            ToggleControls(ctrls.ToArray());
        }

        /// <summary>
        /// Convierte un <see cref="BitmapSource" /> en un
        /// <see cref="BitmapImage" />.
        /// </summary>
        /// <param name="bs"><see cref="BitmapSource" /> a convertir.</param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
        /// un <see cref="BitmapSource" />.
        /// </returns>
        public static BitmapImage ToImage(this BitmapSource bs)
        {
            var ec = new PngBitmapEncoder();
            using var ms = new MemoryStream();
            var bi = new BitmapImage();
            ec.Frames.Add(BitmapFrame.Create(bs));
            ec.Save(ms);
            ms.Position = 0;
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            ms.Close();
            return bi;
        }

        /// <summary>
        /// Convierte un <see cref="System.Drawing.Image" /> en un
        /// <see cref="BitmapImage" />.
        /// </summary>
        /// <param name="bs"><see cref="BitmapSource" /> a convertir.</param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
        /// un <see cref="System.Drawing.Image" />.
        /// </returns>
        public static BitmapImage ToImage(this System.Drawing.Image bs)
        {
            using var ms = new MemoryStream();
            var bi = new BitmapImage();
            bs.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = ms;
            bi.EndInit();
            ms.Close();
            return bi;
        }

        /// <summary>
        /// Convierte un <see cref="System.Drawing.Image" /> en un
        /// <see cref="BitmapSource" />.
        /// </summary>
        /// <param name="bs"><see cref="BitmapSource" /> a convertir.</param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
        /// un <see cref="System.Drawing.Image" />.
        /// </returns>
        public static BitmapSource ToSource(this System.Drawing.Image bs)
        {
            using var ms = new MemoryStream();
            var bitmap = new System.Drawing.Bitmap(bs);
            var bmpPt = bitmap.GetHbitmap();
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bmpPt,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            bitmapSource.Freeze();
            DeleteObject(bmpPt);

            return bitmapSource;
        }

        /// <summary>
        /// Establece un estado de error para un control.
        /// </summary>
        /// <param name="c">Control a advertir.</param>
        public static void Warn(this Control c)
        {
            Warn(c, null);
        }

        /// <summary>
        /// Establece un estado de error para un control.
        /// </summary>
        /// <param name="c">Control a advertir.</param>
        /// <param name="ttip">
        /// <see cref="ToolTip" /> con un mensaje de error para mostrar.
        /// </param>
        public static void Warn(this Control c, string? ttip)
        {
            if (c.IsWarned()) c.ClearWarn();
            _origctrls.Add(new OrigControlColor
            {
                _rf = c,
                _fore = c.Foreground,
                _bacg = c.Background,
                _ttip = (ToolTip) c.ToolTip
            });
            SolidColorBrush brush;
            if (c.Foreground is SolidColorBrush fore)
                brush = Types.Color.Blend(fore.Color.ToMcartColor(), Colors.Red.ToMcartColor()).Brush();
            else brush = new SolidColorBrush(Colors.DarkRed);
            c.Foreground = brush;
            if (c.Background is SolidColorBrush backg)
                brush = Types.Color.Blend(backg.Color.ToMcartColor(), Colors.Red.ToMcartColor()).Brush();
            else brush = new SolidColorBrush(Colors.Pink);
            c.Background = brush;
            brush.Flash(Colors.Red);
            if (!ttip.IsEmpty()) c.ToolTip = new ToolTip {Content = ttip};
        }
    }
}