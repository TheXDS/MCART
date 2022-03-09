/*
ExDumpOptions.cs

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

namespace TheXDS.MCART.Resources.Strings;
using System;

/// <summary>
/// Especifica distintas opciones de formato de texto a generar para
/// describir una excepción.
/// </summary>
[Flags]
public enum ExDumpOptions : byte
{
    /// <summary>
    /// Incluye el nombre de la excepción.
    /// </summary>
    Name = 1,

    /// <summary>
    /// Incluye la descripción de la ubicación donde se ha producido la
    /// excepción.
    /// </summary>
    Source = 2,

    /// <summary>
    /// Incluye el mensaje de error de la excepción.
    /// </summary>
    Message = 4,

    /// <summary>
    /// Incluye el HRESULT de la excepción.
    /// </summary>
    HResult = 8,

    /// <summary>
    /// Incluye un volcado de pila de la excepción.
    /// </summary>
    StackTrace = 16,

    /// <summary>
    /// Incluye cualquier propiedad que contenga excepciones internas.
    /// </summary>
    Inner = 32,

    /// <summary>
    /// Incluye un listado de los ensamblados cargados en el AppDomain
    /// donde se produjo la excepción.
    /// </summary>
    LoadedAssemblies = 64,

    /// <summary>
    /// Formatea el texto para presentarlo en un ancho predefinido.
    /// </summary>
    TextWidthFormatted = 128,

    /// <summary>
    /// Muestra toda la información de la excepción y cualquier excepción
    /// interna producida.
    /// </summary>
    All = 127,

    /// <summary>
    /// Muestra toda la información de la excepción y cualquier excepción
    /// interna producida, formateando el texto para presentarlo en un
    /// ancho predefinido.
    /// </summary>
    AllFormatted = 255,
}
