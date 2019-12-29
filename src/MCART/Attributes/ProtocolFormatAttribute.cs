/*
ProtocolFormatAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using System;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{

    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    /// Establece un formato de protocolo para abrir un vínculo por medio
    /// del sistema operativo.
    /// </summary>
    [AttributeUsage(Property | Field)]
    [Serializable]
    public sealed class ProtocolFormatAttribute : Attribute, IValueAttribute<string>
    {
        /// <summary>
        /// Formato de llamada de protocolo.
        /// </summary>
        public string Format { get; }

        /// <inheritdoc />
        /// <summary>
        /// Obtiene el valor de este atributo.
        /// </summary>
        string IValueAttribute<string>.Value => Format;

        /// <inheritdoc />
        /// <summary>
        /// Establece un formato de protocolo para abrir un vínculo por medio del sistema operativo.
        /// </summary>
        /// <param name="format">Máscara a aplicar.</param>
        public ProtocolFormatAttribute(string format)
        {
            Format = format;
        }

        /// <summary>
        /// Abre un url con este protocolo formateado.
        /// </summary>
        /// <param name="url">
        /// URL del recurso a abrir por medio del protocolo definido por
        /// este atributo.
        /// </param>
        public void Open(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            try
            {
                System.Diagnostics.Process.Start(string.Format(Format, url));
            }
            catch
            {
#if PreferExceptions
                throw;
#endif
                /* Ignorar excepción */
            }
        }
    }
}