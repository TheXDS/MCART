using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// <see cref="TypeBuilder"/> que incluye información fuertemente tipeada
    /// sobre su clase base.
    /// </summary>
    /// <typeparam name="T">
    /// Clase base del tipo a construir.
    /// </typeparam>
    public class TypeBuilder<T> : ITypeBuilder<T>
    {
        /// <summary>
        /// <see cref="TypeBuilder"/> subyacente de esta instancia.
        /// </summary>
        public TypeBuilder Builder { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        ///  <see cref="TypeBuilder{T}"/> especificando al
        ///  <see cref="TypeBuilder"/> subyacente a asociar.
        /// </summary>
        /// <param name="builder">
        /// <see cref="TypeBuilder"/> subyacente a asociar.
        /// </param>
        public TypeBuilder(TypeBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="TypeBuilder{T}"/> en un
        ///  <see cref="TypeBuilder"/>.
        /// </summary>
        /// <param name="builder">
        /// <see cref="TypeBuilder"/> a convertir.
        /// </param>
        public static implicit operator TypeBuilder(TypeBuilder<T> builder) => builder.Builder;
    }
}
