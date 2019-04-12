/*
IntRangeConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<T>, la cual permite representar rangos
de valores.

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

namespace TheXDS.MCART.Types.Converters
{
#if !CLSCompliance
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.SByte" />.
    /// </summary>
    public class SByteRangeConverter : RangeConverter<sbyte>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.UInt16" />.
    /// </summary>
    public class UShortRangeConverter : RangeConverter<ushort>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.UInt32" />.
    /// </summary>
    public class UIntRangeConverter : RangeConverter<uint>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.UInt64" />.
    /// </summary>
    public class ULongRangeConverter : RangeConverter<ulong>
    {
    }
#endif
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Byte" />.
    /// </summary>
    public class ByteRangeConverter : RangeConverter<byte>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Int16" />.
    /// </summary>
    public class ShortRangeConverter : RangeConverter<short>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Int32" />.
    /// </summary>
    public class IntRangeConverter : RangeConverter<int>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Int64" />.
    /// </summary>
    public class LongRangeConverter : RangeConverter<long>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Single" />.
    /// </summary>
    public class SingleRangeConverter : RangeConverter<float>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Double" />.
    /// </summary>
    public class DoubleRangeConverter : RangeConverter<double>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Decimal" />.
    /// </summary>
    public class DecimalRangeConverter : RangeConverter<decimal>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.Char" />.
    /// </summary>
    public class CharRangeConverter : RangeConverter<char>
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.String" />.
    /// </summary>
    public class StringRangeConverter : RangeConverter<string>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.DateTime" />.
    /// </summary>
    public class DateTimeRangeConverter : RangeConverter<DateTime>
    {
    }
    /// <inheritdoc />
    /// <summary>
    ///     Permite realizar la conversión entre tipos
    ///     <see cref="T:System.String" /> y
    ///     <see cref="T:TheXDS.MCART.Types.Range`1" /> para rangos de tipo
    ///     <see cref="T:System.TimeSpan" />.
    /// </summary>
    public class TimeSpanRangeConverter : RangeConverter<TimeSpan>
    {
    }

}