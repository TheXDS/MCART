// MvvmErrors.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Ers = TheXDS.MCART.Resources.Strings.MvvmErrors;

namespace TheXDS.MCART.Resources
{
    internal static class MvvmErrors
    {
        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="InvalidOperationException"/> con un mensaje
        /// predeterminado que indica que la llamada al método 
        /// <see cref="Types.Base.NotifyPropertyChangeBase.Change{T}(ref T, T, string)"/> 
        /// es inválida porque se ha llamado desde fuera de un bloque
        /// <see langword="set"/> de una propiedad en un objeto que hereda de
        /// <see cref="Types.Base.NotifyPropertyChangeBase"/> o de una de sus
        /// clases derivadas.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="InvalidOperationException"/>.
        /// </returns>
        public static InvalidOperationException PropSetMustCall()
        {
            return new(Ers.PropSetMustCall);
        }

        /// <summary>
        /// Crea una nueva instancia de un
        /// <see cref="InvalidOperationException"/> con un mensaje
        /// predeterminado que indica que la llamada al método 
        /// <see cref="Types.Base.NotifyPropertyChangeBase.Change{T}(ref T, T, string)"/> 
        /// es inválida porque se ha llamado desde una propiedad distinta a la que
        /// ha cambiado de valor, o bien, se ha especificado un valor para el
        /// argumento <c>propertyName</c> y este no coincide con el nombre de la
        /// propiedad actual.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase
        /// <see cref="InvalidOperationException"/>.
        /// </returns>
        public static InvalidOperationException PropChangeSame()
        {
            return new(Ers.PropChangeSame);
        }
    }
}
