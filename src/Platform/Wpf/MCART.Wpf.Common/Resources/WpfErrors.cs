﻿/*
WpfIcons.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using Ers = TheXDS.MCART.Wpf.Resources.Strings.WpfErrors;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Contiene una serie de miembros que instancian excepciones que pueden
/// producirse dentro de MCART.WPF.
/// </summary>
public static class WpfErrors
{
    /// <summary>
    /// Obtiene un error que ocurre cuando no se ha encontrado un Id
    /// correspondiente a un recurso solicitado.
    /// </summary>
    /// <param name="id">Id del recurso que no ha sido encontrado.</param>
    /// <param name="argName">Nombre del argumento que ha fallado.</param>
    /// <returns>
    /// Una nueva instancia de la clase <see cref="ArgumentException"/>.
    /// </returns>
    public static Exception ResourceNotFound(string id, string argName)
    {
        return new ArgumentException(string.Format(Ers.ResourceNotFound, id), argName);
    }
}
