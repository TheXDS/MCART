﻿/*
TargetMCARTVersionAttribute.cs

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

using System;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Especifica la versión de MCART recomendada para el elemento.
/// </summary>
[AttributeUsage(Method | Class | Module | Assembly)]
[Serializable]
public sealed class TargetMCARTVersionAttribute : VersionAttributeBase
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TargetMCARTVersionAttribute" />.
    /// </summary>
    /// <param name="major">Número de versión mayor.</param>
    /// <param name="minor">Número de versión menor.</param>
    public TargetMCARTVersionAttribute(int major, int minor)
        : base(major, minor, int.MaxValue, int.MaxValue)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TargetMCARTVersionAttribute" />.
    /// </summary>
    /// <param name="major">Número de versión mayor.</param>
    /// <param name="minor">Número de versión menor.</param>
    /// <param name="build">Número de compilación.</param>
    /// <param name="rev">Número de revisión.</param>
    public TargetMCARTVersionAttribute(int major, int minor, int build, int rev) 
        : base(major, minor, build, rev)
    {
    }
}
