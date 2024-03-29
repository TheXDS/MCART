﻿/*
LicenseFileAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using static System.AttributeTargets;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Establece un archivo de licencia externo a asociar con el elemento.
/// </summary>
[AttributeUsage(Class | Module | Assembly)]
[Obsolete("Utilice LicenseUriAttribute en su lugar.")]
public sealed class LicenseFileAttribute : TextAttribute
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LicenseFileAttribute" />.
    /// </summary>
    /// <param name="licenseFile">
    /// Ruta del archivo de licencia adjunto.
    /// </param>
    public LicenseFileAttribute(string licenseFile) : base(licenseFile)
    {
        // HACK: forma simple y efectiva de validad una ruta de archivo.
        _ = new FileInfo(licenseFile);
    }

    /// <summary>
    /// Lee el archivo de licencia especificado por este atributo.
    /// </summary>
    /// <returns>
    /// El contenido del archivo de licencia especificado.
    /// </returns>
    public string ReadLicense()
    {
        try
        {
            using FileStream? fs = new(Value!, FileMode.Open);
            using StreamReader? sr = new(fs);
            return sr.ReadToEnd();
        }
        catch
        {
            return St.Composition.Warn(St.Common.UnspecifiedLicense);
        }
    }
}
