/*
BinaryWriterExtensions.cs

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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Contiene extensiones útiles para la clase
    ///     <see cref="BinaryWriter"/>.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter bw, Guid value)
        {
            bw.Write(value.ToByteArray());
        }

        public static void Write(this BinaryWriter bw, DateTime value)
        {
            bw.Write(value.ToBinary());
        }

        public static void Write(this BinaryWriter bw, TimeSpan value)
        {
            bw.Write(value.Ticks);
        }

        public static void Write(this BinaryWriter bw, Enum value)
        {
            bw.Write(value.ToBytes());
        }

        public static void Write(this BinaryWriter bw, ISerializable value)
        {
            var d = new DataContractSerializer(value.GetType());
            using var ms = new MemoryStream();
            d.WriteObject(ms, value);
            bw.Write(BitConverter.ToString(ms.ToArray()));
        }

        public static void DynamicWrite(this BinaryWriter bw, object value)
        {
            var t = value.GetType();

            if (typeof(BinaryWriter).GetMethods().FirstOrDefault(p => CanWrite(p, t)) is { } m)
            {
                m.Invoke(bw, new[] { value });
            }
            if (typeof(BinaryWriterExtensions).GetMethods().FirstOrDefault(p => CanExWrite(p, t)) is { } e)
            {
                e.Invoke(null, new[] { bw, value });
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