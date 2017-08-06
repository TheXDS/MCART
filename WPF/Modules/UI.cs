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
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MCART
{
    /// <summary>
    /// Contiene varias herramientas de UI para utilizar en proyectos de
    /// Windows Presentation Framework.
    /// </summary>
    public static partial class UI
    {
        /// <summary>
        /// Contiene información de los controles modificados por la función
        /// <see cref="Warn(Control, string)"/>.
        /// </summary>
        struct OrigControlColor
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
    }
}