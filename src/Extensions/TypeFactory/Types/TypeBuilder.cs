using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace TheXDS.MCART.Types
{

    public class TypeBuilder<T> : ITypeBuilder<T>
    {
        public TypeBuilder Builder { get; }
        public TypeBuilder(TypeBuilder builder)
        {
            Builder = builder;
        }
        public static implicit operator TypeBuilder(TypeBuilder<T> builder) => builder.Builder;
    }
}
