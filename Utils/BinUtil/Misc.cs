/*
Misc.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

namespace TheXDS.MCARTBinUtil
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ArgAttribute : Attribute
    {
        public string Value { get; }
        public ArgAttribute(string text) { Value = text; }
    }

    public abstract class StreamGetter
    {
        public bool TryGetStream(string source, out Stream stream)
        {
            stream = null;
            return Uri.TryCreate(source, UriKind.Absolute, out var uriResult) && TryGetStream(uriResult, out stream);
        }
        public abstract bool TryGetStream(Uri source, out Stream stream);
        public abstract string InferRes(string source);
        public static string InferRes(Uri source) => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(source.Segments.Last()).Replace('.', '_').Replace('-', '_').Replace(" ", "");
    }

    public interface ICompressorGetter
    {
        Stream GetCompressor(Stream stream);
    }
}