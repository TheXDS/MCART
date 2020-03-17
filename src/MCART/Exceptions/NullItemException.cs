/*
EmptyCollectionException.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be
useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections;
using System.Runtime.Serialization;

namespace TheXDS.MCART.Exceptions
{
    [Serializable]
    public class NullItemException : OffendingException<IList>
    {
        public int NullIndex { get; set; }

        public NullItemException()
        {
        }

        public NullItemException(IList offendingObject) : base(offendingObject)
        {
        }

        public NullItemException(string message, IList offendingObject) : base(message, offendingObject)
        {
        }

        public NullItemException(Exception inner) : base(inner)
        {
        }

        public NullItemException(Exception inner, IList offendingObject) : base(inner, offendingObject)
        {
        }

        public NullItemException(string message, Exception inner) : base(message, inner)
        {
        }

        public NullItemException(string message, Exception inner, IList offendingObject) : base(message, inner, offendingObject)
        {
        }

        protected NullItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected NullItemException(SerializationInfo info, StreamingContext context, IList offendingObject) : base(info, context, offendingObject)
        {
        }
    }
}