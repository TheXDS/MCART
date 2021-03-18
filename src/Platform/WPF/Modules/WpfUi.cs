/*
WpfUi.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Windows.Dwm;
using TheXDS.MCART.Wpf.Component;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using static TheXDS.MCART.Types.Extensions.WpfColorExtensions;
using C = TheXDS.MCART.Math.Geometry;

namespace TheXDS.MCART
{
    /// <summary>
    /// Contiene varias herramientas de UI para utilizar en proyectos de
    /// Windows Presentation Framework.
    /// </summary>
    public static class WpfUi
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);

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

        private static readonly List<OrigControlColor> _origctrls = new();
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
        public static void HookHelp(this IWpfWindow window, HandledEventHandler handler)
        {
            window.ShowHelp();
            window.NotifyWindowFrameChange();
            HwndSource.FromHwnd(window.Handle).AddHook(CreateHookDelegate(window.Itself, 0xF180, handler));
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
            HookHelp(new WpfWindowWrap(window), handler);
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
                    cp.X + (System.Math.Sin(C.DegRad * angle) * radius),
                    cp.Y - (System.Math.Cos(C.DegRad * angle) * radius)),
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
            var cp = new Point(radius + (thickness / 2), radius + (thickness / 2));
            var arc = new ArcSegment
            {
                IsLargeArc = endAngle - startAngle > 180.0,
                Point = new Point(
                    cp.X + (System.Math.Sin(C.DegRad * endAngle) * radius),
                    cp.Y - (System.Math.Cos(C.DegRad * endAngle) * radius)),
                Size = new Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise
            };
            var pth = new PathFigure
            {
                StartPoint = new Point(
                    cp.X + (System.Math.Sin(C.DegRad * startAngle) * radius),
                    cp.Y - (System.Math.Cos(C.DegRad * startAngle) * radius)),
                IsClosed = false
            };
            pth.Segments.Add(arc);
            var outp = new PathGeometry();
            outp.Figures.Add(pth);
            return outp;
        }

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
        /// Crea un mapa de bits de un <see cref="Visual" />.
        /// </summary>
        /// <param name="visual">
        /// <see cref="Visual" /> a renderizar.
        /// </param>
        /// <param name="size">
        /// Tamaño del canvas en donde se renderizará el control.
        /// </param>
        /// <param name="dpi">
        /// Valor de puntos por pulgada a utilizar para crear el mapa de bits.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
        /// renderizada de <paramref name="visual" />.
        /// </returns>
        public static RenderTargetBitmap Render(this Visual visual, Size size, int dpi)
        {
            var bmp = new RenderTargetBitmap(
                (int) size.Width,
                (int) size.Height,
                dpi, dpi,
                PixelFormats.Pbgra32);
            bmp.Render(visual);
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
            return Render(f, outSize, dpi);
        }

        /// <summary>
        /// Crea un mapa de bits de un <see cref="UIElement" />.
        /// </summary>
        /// <param name="u">
        /// <see cref="UIElement" /> a renderizar.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="RenderTargetBitmap" /> que contiene una imagen
        /// renderizada de <paramref name="u" />.
        /// </returns>
        public static RenderTargetBitmap Render(this UIElement u)
        {
            return Render(u, new Size((int)u.RenderSize.Width, (int)u.RenderSize.Height), WinUi.GetDpi().X);
        }

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
        /// <param name="image"><see cref="System.Drawing.Image" /> a convertir.</param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
        /// un <see cref="System.Drawing.Image" />.
        /// </returns>
        public static BitmapImage ToImage(this System.Drawing.Image image)
        {
            using var ms = new MemoryStream();
            var bi = new BitmapImage();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
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
        /// <param name="image"><see cref="System.Drawing.Image" /> a convertir.</param>
        /// <returns>
        /// Un <see cref="BitmapImage" /> que contiene la imagen obtenida desde
        /// un <see cref="System.Drawing.Image" />.
        /// </returns>
        public static BitmapSource ToSource(this System.Drawing.Image image)
        {
            using var ms = new MemoryStream();
            var bitmap = new System.Drawing.Bitmap(image);
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
        /// Convierte un <see cref="System.Drawing.Image" /> en un
        /// <see cref="Visual" />.
        /// </summary>
        /// <param name="image"><see cref="System.Drawing.Image" /> a convertir.</param>
        /// <returns>
        /// Un <see cref="Visual" /> que contiene la imagen obtenida desde
        /// un <see cref="System.Drawing.Image" />.
        /// </returns>
        public static Visual ToVisual(this System.Drawing.Image image)
        {
            var v = new DrawingVisual();
            using var dc = v.RenderOpen();
            dc.DrawImage(image.ToSource(), new Rect { Width = image.Width, Height = image.Height });
            return v;
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

        private static HwndSourceHook CreateHookDelegate(Window window, int syscommand, HandledEventHandler handler)
        {
            return (IntPtr hwnd, int msg, IntPtr param, IntPtr lParam, ref bool handled) =>
            {
                if (msg != 0x0112 || ((int)param & 0xFFF0) != syscommand) return IntPtr.Zero;
                var e = new HandledEventArgs();
                handler?.Invoke(window, e);
                handled = e.Handled;
                return IntPtr.Zero;
            };
        }
    }
}