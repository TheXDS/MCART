//
//  UITools.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using TheXDS.MCART.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace TheXDS.MCART
{
    /// <summary>
    /// Contiene varias herramientas de UI para utilizar en proyectos de
    /// Windows Presentation Framework.
    /// </summary>
    public static partial class UI
    {
        [DllImport("user32.dll")]
        static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        static List<OrigControlColor> origctrls = new List<OrigControlColor>();
        static List<BitmapEncoder> bEncLst;
        static void SetBlur(Window window, AccentState state)
        {
            var windowHelper = new WindowInteropHelper(window);
            var accent = new AccentPolicy { AccentState = state };
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
        enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }
        enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }
        struct OrigControlColor
        {
            /// <summary>
            /// Color primario original.
            /// </summary>
            internal Brush fore;
            /// <summary>
            /// Color de fondo original.
            /// </summary>
            internal Brush bacg;
            /// <summary>
            /// Referencia del control al cual se aplica.
            /// </summary>
            internal Control rf;
            /// <summary>
            /// <see cref="ToolTip"/> original del control.
            /// </summary>
            internal ToolTip ttip;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }
        /// <summary>
        /// Habilita los efectos de difuminado de Windows 10 en la ventana de WPF.
        /// </summary>
        /// <param name="window">Instancia de ventana a difuminar.</param>
        public static void EnableBlur(this Window window) => SetBlur(window, AccentState.ACCENT_ENABLE_BLURBEHIND);
        /// <summary>
        /// Deshabilita los efectos de difuminado de Windows 10 en la ventana de WPF.
        /// </summary>
        /// <param name="window">Instancia de ventana a difuminar.</param>
        public static void DisableBlur(this Window window) => SetBlur(window, AccentState.ACCENT_DISABLED);
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
        /// Obtiene una imagen a partir de un <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">
        /// <see cref="Stream"/> con el contenido de la imagen.
        /// </param>
        /// <returns>
        /// La imagen que ha sido leída desde el <see cref="Stream"/>.
        /// </returns>
        public static BitmapImage GetBitmap(Stream stream)
        {
            var retVal = new BitmapImage();
            retVal.BeginInit();
            stream.Seek(0, SeekOrigin.Begin);
            retVal.StreamSource = stream;
            retVal.EndInit();
            return retVal;
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
            return Render(f, new Size((int)f.ActualWidth, (int)f.ActualHeight), GetDpi().X);
        }
        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement"/>.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement"/> a renderizar.
        /// </param>
        /// <param name="dpi">
        /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap"/> que contiene una imagen
        /// renderizada de <paramref name="f"/>.
        /// </returns>
        public static RenderTargetBitmap Render(this FrameworkElement f, int dpi)
        {
            return Render(f, new Size((int)f.ActualWidth, (int)f.ActualHeight), dpi);
        }
        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement"/>.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement"/> a renderizar.
        /// </param>
        /// <param name="size">
        /// Tamaño del canvas en donde se renderizará el control.
        /// </param>
        /// <param name="dpi">
        /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap"/> que contiene una imagen
        /// renderizada de <paramref name="f"/>.
        /// </returns>
        public static RenderTargetBitmap Render(this FrameworkElement f, Size size, int dpi)
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                dpi, dpi,
                PixelFormats.Pbgra32);
            bmp.Render(f);
            return bmp;
        }
        /// <summary>
        /// Crea un mapa de bits de un <see cref="FrameworkElement"/>
        /// estableciendo el tamaño en el cual se dibujará el control, por lo
        /// que no necesita haberse mostrado en la interfaz de usuario.
        /// </summary>
        /// <param name="f">
        /// <see cref="FrameworkElement"/> a renderizar.
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
        /// Un objeto <see cref="RenderTargetBitmap"/> que contiene una imagen
        /// renderizada de <paramref name="f"/>.
        /// </returns>
        public static RenderTargetBitmap Render(this FrameworkElement f, Size inSize, Size outSize, int dpi)
        {
            f.Measure(inSize);
            f.Arrange(new Rect(inSize));
            RenderTargetBitmap bmp = new RenderTargetBitmap(
                (int)outSize.Width,
                (int)outSize.Height,
                dpi, dpi,
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
        /// el radio para lograr un tamaño más consistente.
        /// </param>
        /// <returns>
        /// Un <see cref="PathGeometry"/> que contiene el arco generado por
        /// esta función.
        /// </returns>
        public static PathGeometry GetCircleArc(double radius, double angle, double thickness = 0)
        {
            Point cp = new Point(radius + (thickness / 2), radius + (thickness / 2));
            ArcSegment arc = new ArcSegment()
            {
                IsLargeArc = angle > 180.0,
                Point = new Point(
                    cp.X + System.Math.Sin(Math.DegRad * angle) * radius,
                    cp.Y - System.Math.Cos(Math.DegRad * angle) * radius),
                Size = new Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise
            };
            PathFigure pth = new PathFigure()
            {
                StartPoint = new Point(cp.X, cp.Y - radius),
                IsClosed = false
            };
            pth.Segments.Add(arc);
            PathGeometry outp = new PathGeometry();
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
        /// Un <see cref="PathGeometry"/> que contiene el arco generado por
        /// esta función.
        /// </returns>
        public static PathGeometry GetCircleArc(double radius, double startAngle, double endAngle, double thickness)
        {
            Point cp = new Point(radius + (thickness / 2), radius + (thickness / 2));
            ArcSegment arc = new ArcSegment()
            {
                IsLargeArc = endAngle - startAngle > 180.0,
                Point = new Point(
                    cp.X + System.Math.Sin(Math.DegRad * endAngle) * radius,
                    cp.Y - System.Math.Cos(Math.DegRad * endAngle) * radius),
                Size = new Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise
            };
            PathFigure pth = new PathFigure()
            {
                StartPoint = new Point(
                    cp.X + System.Math.Sin(Math.DegRad * startAngle) * radius,
                    cp.Y - System.Math.Cos(Math.DegRad * startAngle) * radius),
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
        public static void Warn(this Control c) => Warn(c, null);
        /// <summary>
        /// Establece un estado de error para un control.
        /// </summary>
        /// <param name="c">Control a advertir.</param>
        /// <param name="ttip">
        /// <see cref="ToolTip"/> con un mensaje de error para mostrar.
        /// </param>
        public static void Warn(this Control c, string ttip)
        {
            if (c.IsWarned()) c.ClearWarn();
            origctrls.Add(new OrigControlColor
            {
                rf = c,
                fore = c.Foreground,
                bacg = c.Background,
                ttip = (ToolTip)c.ToolTip
            });
            SolidColorBrush brush;
            if (c.Foreground is SolidColorBrush fore)
                brush = new SolidColorBrush(Types.Color.Blend(fore.Color, Colors.Red));
            else brush = new SolidColorBrush(Colors.DarkRed);
            c.Foreground = brush;
            if (c.Background is SolidColorBrush backg)
                brush = new SolidColorBrush(Types.Color.Blend(backg.Color, Colors.Red));
            else brush = new SolidColorBrush(Colors.Pink);
            c.Background = brush;
            brush.Flash(Colors.Red);
            if (!ttip.IsEmpty()) c.ToolTip = new ToolTip() { Content = ttip };
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
        /// <returns><see langword="true"/> si el control está mostrando una advertencia;
        /// de lo contrario, <see langword="false"/>.</returns>
        public static bool IsWarned(this Control c)
        {
            if (c is null) throw new ArgumentNullException(nameof(c));
            foreach (OrigControlColor j in origctrls) if (j.rf.Is(c)) return true;
            return false;
        }
        /// <summary>
        /// Ejecuta una animación de destello en un
        /// <see cref="SolidColorBrush"/>.
        /// </summary>
        /// <param name="brush"><see cref="SolidColorBrush"/> a animar.</param>
        /// <param name="flashColor"><see cref="Color"/> del destello.</param>
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
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Collapsed"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a colapsar.
        /// </param>
        public static void CollapseControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Collapsed"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a colapsar.
        /// </param>
        [Thunk]
        public static void CollapseControls(this IEnumerable<UIElement> ctrls)
            => CollapseControls(ctrls.ToArray());
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Hidden"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a ocultar.
        /// </param>
        public static void HideControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Hidden"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls"
        /// >Arreglo de <see cref="UIElement"/> a ocultar.
        /// </param>
        [Thunk]
        public static void HideControls(this IEnumerable<UIElement> ctrls)
            => HideControls(ctrls.ToArray());
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Visible"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a mostrar.
        /// </param>
        public static void ShowControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Establece la propiedad <see cref="UIElement.Visibility"/> a
        /// <see cref="Visibility.Visible"/> a una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a mostrar.
        /// </param>
        [Thunk]
        public static void ShowControls(this IEnumerable<UIElement> ctrls)
            => ShowControls(ctrls.ToArray());
        /// <summary>
        /// Deshabilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a deshabilitar.
        /// </param>
        public static void DisableControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.IsEnabled = false;
        }
        /// <summary>
        /// Deshabilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a deshabilitar.
        /// </param>
        [Thunk]
        public static void DisableControls(this IEnumerable<UIElement> ctrls)
            => DisableControls(ctrls.ToArray());
        /// <summary>
        /// Habilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a habilitar.
        /// </param>
        public static void EnableControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.IsEnabled = true;
        }
        /// <summary>
        /// Habilita una lista de controles.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a habilitar.
        /// </param>
        [Thunk]
        public static void EnableControls(this IEnumerable<UIElement> ctrls)
            => EnableControls(ctrls.ToArray());
        /// <summary>
        /// Habilita o deshabilita una lista de controles según su estado
        /// previo.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a habilitar/deshabilitar.
        /// </param>
        public static void ToggleControls(params UIElement[] ctrls)
        {
            foreach (UIElement j in ctrls) j.IsEnabled = !j.IsEnabled;
        }
        /// <summary>
        /// Habilita o deshabilita una lista de controles según su estado
        /// previo.
        /// </summary>
        /// <param name="ctrls">
        /// Arreglo de <see cref="UIElement"/> a habilitar/deshabilitar.
        /// </param>
        [Thunk]
        public static void ToggleControls(this IEnumerable<UIElement> ctrls)
            => ToggleControls(ctrls.ToArray());
        /// <summary>
        /// Limpia el texto del control.
        /// </summary>
        /// <param name="control">Control a limpiar.</param>
        public static void Clear(this TextBox control)
        {
            control.Text = string.Empty;
        }
        /// <summary>
        /// Convierte un <see cref="BitmapSource"/> en un 
        /// <see cref="BitmapImage"/>.
        /// </summary>
        /// <param name="bs"><see cref="BitmapSource"/> a convertir.</param>
        /// <returns>
        /// Un <see cref="BitmapImage"/> que contiene la imagen obtenida desde
        /// un <see cref="BitmapSource"/>.
        /// </returns>
        public static BitmapImage ToImage(this BitmapSource bs)
        {
            PngBitmapEncoder ec = new PngBitmapEncoder();
            using (var ms = new MemoryStream())
            {
                BitmapImage bi = new BitmapImage();
                ec.Frames.Add(BitmapFrame.Create(bs));
                ec.Save(ms);
                ms.Position = 0;
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                ms.Close();
                return bi;
            }
        }
    }
}