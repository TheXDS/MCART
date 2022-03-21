/*
BusyIndicator.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

namespace TheXDS.MCART.Controls;
using System.Windows;

//public class Gauge : ProgressRing
//{

//    /// <summary>
//    /// Identifica a la propiedad de dependencia <see cref="StartAngle" />.
//    /// </summary>
//    public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle),
//        typeof(double), typeof(ProgressRing),
//        new FrameworkPropertyMetadata(225.0, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout), ChkAngle);

//    /// <summary>
//    /// Identifica a la propiedad de dependencia <see cref="EndAngle" />.
//    /// </summary>
//    public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle),
//        typeof(double), typeof(ProgressRing),
//        new FrameworkPropertyMetadata(135.0, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout), ChkAngle);

//    /// <summary>
//    /// Obtiene o establece el valor máximo de este
//    /// <see cref="ProgressRing"/>.
//    /// </summary>
//    public double StartAngle
//    {
//        get => (double)GetValue(StartAngleProperty);
//        set => SetValue(StartAngleProperty, value);
//    }

//    /// <summary>
//    /// Obtiene o establece el valor máximo de este
//    /// <see cref="ProgressRing"/>.
//    /// </summary>
//    public double EndAngle
//    {
//        get => (double)GetValue(EndAngleProperty);
//        set => SetValue(EndAngleProperty, value);
//    }

//}