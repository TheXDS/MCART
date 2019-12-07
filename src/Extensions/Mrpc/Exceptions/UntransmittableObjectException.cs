using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Resources.MrpcStrings;

namespace TheXDS.MCART.Exceptions
{
    public class UntransmittableObjectException : OffendingException<object>
    {
        private static string MkMsg(object? obj = null)
        {
            return string.Format(ErrUntransmittableObj, obj?.GetType().FullName.OrNull(OfType));
        }

        public UntransmittableObjectException(string message) : base(message)
        {
        }

        public UntransmittableObjectException() : base(MkMsg())
        {
        }

        public UntransmittableObjectException(object offendingObject) : base(MkMsg(offendingObject), offendingObject)
        {
        }

        public UntransmittableObjectException(string message, object offendingObject) : base(message, offendingObject)
        {
        }

        public UntransmittableObjectException(Exception inner) : base(MkMsg(), inner)
        {
        }

        public UntransmittableObjectException(Exception inner, object offendingObject) : base(MkMsg(offendingObject),inner, offendingObject)
        {
        }

        public UntransmittableObjectException(string message, Exception inner) : base(message, inner)
        {
        }

        public UntransmittableObjectException(string message, Exception inner, object offendingObject) : base(message, inner, offendingObject)
        {
        }

        protected UntransmittableObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected UntransmittableObjectException(SerializationInfo info, StreamingContext context, object offendingObject) : base(info, context, offendingObject)
        {
        }
    }
}
