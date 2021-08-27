/*
BinaryWriterExtensions.cs

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

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones útiles para la clase
    /// <see cref="BinaryWriter"/>.
    /// </summary>
    public static partial class BinaryWriterExtensions
    {
        /// <summary>
        /// Escribe un <see cref="Guid"/> en el <see cref="BinaryWriter"/>
        /// especificado.
        /// </summary>
        /// <param name="bw">
        /// Instancia sobre la cual realizar la escritura.
        /// </param>
        /// <param name="value">
        /// Valor a escribir.
        /// </param>
        public static void Write(this BinaryWriter bw, Guid value)
        {
            bw.Write(value.ToByteArray());
        }

        /// <summary>
        /// Escribe un <see cref="DateTime"/> en el
        /// <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="bw">
        /// Instancia sobre la cual realizar la escritura.
        /// </param>
        /// <param name="value">
        /// Valor a escribir.
        /// </param>
        public static void Write(this BinaryWriter bw, DateTime value)
        {
            bw.Write(value.ToBinary());
        }

        /// <summary>
        /// Escribe un <see cref="TimeSpan"/> en el
        /// <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="bw">
        /// Instancia sobre la cual realizar la escritura.
        /// </param>
        /// <param name="value">
        /// Valor a escribir.
        /// </param>
        public static void Write(this BinaryWriter bw, TimeSpan value)
        {
            bw.Write(value.Ticks);
        }

        /// <summary>
        /// Escribe un valor <see cref="Enum"/> en el
        /// <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="bw">
        /// Instancia sobre la cual realizar la escritura.
        /// </param>
        /// <param name="value">
        /// Valor a escribir.
        /// </param>
        public static void Write(this BinaryWriter bw, Enum value)
        {
            bw.Write(value.ToBytes());
        }

        /// <summary>
        /// Escribe un objeto serialziable en el
        /// <see cref="BinaryWriter"/> especificado.
        /// </summary>
        /// <param name="bw">
        /// Instancia sobre la cual realizar la escritura.
        /// </param>
        /// <param name="value">
        /// Objeto serializable a escribir.
        /// </param>
        public static void Write(this BinaryWriter bw, ISerializable value)
        {
            var d = new DataContractSerializer(value.GetType());
            using var ms = new MemoryStream();
            d.WriteObject(ms, value);
            bw.Write(BitConverter.ToString(ms.ToArray()));
        }

        /// <summary>
        /// Realiza una escritura del objeto especificado, determinando
        /// dinámicamente el método apropiado de escritura a utilizar.
        /// </summary>
        /// <param name="bw">
        /// Instancia sobre la cual realizar la escritura.
        /// </param>
        /// <param name="value">
        /// Objeto a escribir.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Se produce si no ha sido posible encontrar un método que pueda
        /// escribir el valor especificado. 
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="bw"/> o <paramref name="value"/> son
        /// <see langword="null"/>.
        /// </exception>
        public static void DynamicWrite(this BinaryWriter bw, object value)
        {
            DynamicWrite_Contract(bw, value);
            var t = value.GetType();

            if (typeof(BinaryWriter).GetMethods().FirstOrDefault(p => CanWrite(p, t)) is { } m)
            {
                m.Invoke(bw, new[] { value });
            } 
            else if (typeof(BinaryWriterExtensions).GetMethods().FirstOrDefault(p => CanExWrite(p, t)) is { } e)
            {
                e.Invoke(null, new[] { bw, value });
            }
            else if (value.GetType().IsStruct())
            {
                var sze = Marshal.SizeOf(value);
                var arr = new byte[sze];
                var ptr = Marshal.AllocHGlobal(sze);
                Marshal.StructureToPtr(value, ptr, true);
                Marshal.Copy(ptr, arr, 0, sze);
                Marshal.FreeHGlobal(ptr);
                bw.Write(arr);
            }
            else
            {
                throw Errors.CantWriteObj(value.GetType());
            }
        }

        private static bool CanWrite(MethodInfo p, Type t)
        {
            var l = p.GetParameters();
            return p.IsVoid() 
                && p.Name == "Write" 
                && l.Length == 1 
                && l.Single().ParameterType == t;
        }

        private static bool CanExWrite(MethodInfo p, Type t)
        {
            var l = p.GetParameters();
            return p.IsVoid() 
                && p.Name == "Write" 
                && l.Length == 2 
                && l.First().ParameterType == typeof(BinaryWriter)
                && l.Last().ParameterType == t;
        }
    }
}