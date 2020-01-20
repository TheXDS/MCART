using System;
using System.Reflection.Emit;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Define una serie de miembros a implementar por un descriptor de
    /// <see cref="TypeBuilder"/> que incluye información fuertemente tipeada
    /// del tipo base a heredar.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo base a heredar por el <see cref="TypeBuilder"/>.
    /// </typeparam>
    public interface ITypeBuilder<out T>
    {
        /// <summary>
        /// <see cref="TypeBuilder"/> subyacente a utilizar para la creación
        /// del nuevo tipo.
        /// </summary>
        TypeBuilder Builder { get; }

        /// <summary>
        /// Referencia al tipo base fuertemente tipeado específico del
        /// <see cref="TypeBuilder"/>.
        /// </summary>
        Type SpecificBaseType => typeof(T);

        /// <summary>
        /// Referencia al tipo base real del <see cref="TypeBuilder"/>.
        /// </summary>
        Type ActualBaseType => Builder.BaseType ?? SpecificBaseType;
    }
}