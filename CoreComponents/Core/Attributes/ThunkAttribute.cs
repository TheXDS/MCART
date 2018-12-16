using System;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento es un proveedor de Thunking (facilita la
    ///     llamada a otros elementos o miembros).
    /// </summary>
    /// <remarks>
    ///     Este atributo no debería aplicarse a sobrecargas de un método que
    ///     no sea en sí mismo un método de Thunking.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Assembly)]
    [Serializable]
    public sealed class ThunkAttribute : Attribute
    {
    }
}